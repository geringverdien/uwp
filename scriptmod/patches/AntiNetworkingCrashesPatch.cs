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
					.Named("define crash checker node")
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
			.Build();
	}
}
