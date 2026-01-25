using GDWeave;
using GDWeave.Modding;
using util.LexicalTransformer;

namespace patches;

/// <summary>
/// Fixes various methods of crashing the game
/// </summary>
public static class AntiNetworkingCrashesPatch
{
	public static IScriptMod Create(IModInterface mi)
	{
		return new TransformationRuleScriptModBuilder()
			.ForMod(mi)
			.Named("Crash method prevention")
			.Patching("res://Scenes/Singletons/SteamNetwork.gdc")
			.AddRule(
				new TransformationRuleBuilder()
					.Named("define crash checker node, add debounce vars")
					.Matching(TransformationPatternFactory.CreateGlobalsPattern())
					.With(
						"""

						func array_is_safe(arr = []):
							for v in arr:
								if typeof(v) == TYPE_NIL or (typeof(v) == TYPE_REAL and is_nan(v)):
									return false

								if typeof(v) == TYPE_ARRAY:
									return array_is_safe(v)

							return true

						func is_packet_safe(type, DATA):
							var is_safe = true

							if type == "actor_action" and DATA.get("action") == "_play_sfx":
								return true

							var params = DATA.get("params")

							if typeof(params) == TYPE_ARRAY:
								is_safe = array_is_safe(params)
							elif typeof(params) == TYPE_DICTIONARY:
								is_safe = array_is_safe(params.values())

							return is_safe

						var last_letter_accepted_time = Time.get_ticks_msec()
						var last_letter_denied_time = Time.get_ticks_msec()

						"""
					)
			)
			.AddRule(
				new TransformationRuleBuilder()
					.Named("Add packet validation and early return for invalid packets")
					.Matching(
						TransformationPatternFactory.CreateGdSnippetPattern(
							"FLUSH_PACKET_INFORMATION[PACKET_SENDER] += 1"
						)
					)
					.With(
						"""

						if DATA.has("params"):
							var is_safe = is_packet_safe(type, DATA)

							if not is_safe:
								return
						""",
						2
					)
			)
			.AddRule(
				new TransformationRuleBuilder()
					.Named("Add throttling for letter notifications")
					.Do(Operation.ReplaceAll)
					.Matching(
						TransformationPatternFactory.CreateGdSnippetPattern(
							"""
							"letter_was_accepted":
								PlayerData._letter_was_accepted()
							"letter_was_denied":
								PlayerData._letter_was_denied()
							"""
						)
					)
					.With(
						"""
						"letter_was_accepted":
							if Time.get_ticks_msec() - last_letter_accepted_time > 1000:
								last_letter_accepted_time = Time.get_ticks_msec()
								PlayerData._letter_was_accepted()
						"letter_was_denied":
							if Time.get_ticks_msec() - last_letter_denied_time > 1000:
								last_letter_denied_time = Time.get_ticks_msec()
								PlayerData._letter_was_denied()
						""",
						3
					)
			)
			.Build();
	}
}
