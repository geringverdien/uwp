using GDWeave;
using GDWeave.Modding;
using util.LexicalTransformer;

namespace patches;

/// <summary>
/// Prevents Unlisted lobbies from being joined by strangers
/// </summary>
public static class UnlistedLobbyPatch
{
	public static IScriptMod Create(IModInterface mi)
	{
		return new TransformationRuleScriptModBuilder()
			.ForMod(mi)
			.Named("Fix: Public 'Unlisted lobbies'")
			.Patching("res://Scenes/Singletons/SteamNetwork.gdc")
			.AddRule(
				new TransformationRuleBuilder()
					.Named("Change Unlisted lobby_type to not-public and disable incompatible code button")
					.Do(Operation.ReplaceAll)
					.Matching(
						TransformationPatternFactory.CreateGdSnippetPattern(
							"""
							1: {"name": "Unlisted", "lobby_type": Steam.LOBBY_TYPE_PUBLIC, "code_button": true, "browser_visible": false, "offline": false},
							"""
						)
					)
					.With(
						"""
						1: {"name": "Unlisted", "lobby_type": Steam.LOBBY_TYPE_FRIENDS_ONLY, "code_button": false, "browser_visible": false, "offline": false},
						""",
						1
					)
			)
			.Build();
	}
}
