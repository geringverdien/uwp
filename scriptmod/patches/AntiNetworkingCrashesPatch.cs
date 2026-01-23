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
					.Do(Operation.Append)
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

						var accept_debounce = false
						var accept_debounce_timer = Time.get_ticks_msec()
						var deny_debounce = false
						var deny_debounce_timer = Time.get_ticks_msec()

						""",
						0
					)
			)
			.AddRule(
				new TransformationRuleBuilder()
					.Named("filter incoming packets for crash methods")
					.Do(Operation.Append)
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
								print("blocked potential crash packet by " + Steam.getFriendPersonaName(PACKET_SENDER))
								print("data: " + str(DATA.params))
								return
						""",
						2
					)
			)
			.AddRule(
				new TransformationRuleBuilder()
					.Named("prevent unibomber (letter notif debounce)")
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
						accept_debounce = true
						if Time.get_ticks_msec() - accept_debounce_timer > 1000:
							accept_debounce = false
							accept_debounce_timer = Time.get_ticks_msec()
						if accept_debounce: return
						PlayerData._letter_was_accepted()
					"letter_was_denied":
						deny_debounce = true
						if Time.get_ticks_msec() - deny_debounce_timer > 1000:
							deny_debounce = false
							deny_debounce_timer = Time.get_ticks_msec()
						if deny_debounce: return
						PlayerData._letter_was_denied()
					""",
					3)
			)
			.Build();
	}
}
