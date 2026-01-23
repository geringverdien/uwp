using GDWeave;
using GDWeave.Modding;
using util.LexicalTransformer;

namespace patches;

/// <summary>
/// Adds a Friends Only lobby type
/// </summary>
public static class FriendsOnlyLobbyPatch
{
	public static IScriptMod Create(IModInterface mi)
	{
		return new TransformationRuleScriptModBuilder()
			.ForMod(mi)
			.Named("Add Friends Only Enum")
			.Patching("res://Scenes/Singletons/SteamNetwork.gdc")
			.AddRule(
				new TransformationRuleBuilder()
					.Named("modify lobby_type enum")
					.Do(Operation.Append)
					.Matching(
						TransformationPatternFactory.CreateGdSnippetPattern(
							"""
							3: {"name": "Offline", "lobby_type": Steam.LOBBY_TYPE_PRIVATE, "code_button": true, "browser_visible": false, "offline": true},
							"""
						)
					)
					.With(
						"""

						4: {"name": "Friends Only", "lobby_type": Steam.LOBBY_TYPE_FRIENDS_ONLY, "code_button": false, "browser_visible": false, "offline": false},
						""",
						1
					)
			)
			.Build();
	}
}
