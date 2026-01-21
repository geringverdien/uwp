using GDWeave;
using GDWeave.Modding;
using util.LexicalTransformer;

namespace patches;

/// <summary>
/// Prevents Unlisted lobbies from being indexed by the Matchmaking api
/// </summary>
public static class UnlistedLobbyPatch
{
    public static IScriptMod Create(IModInterface mi)
    {
        return new TransformationRuleScriptModBuilder()
            .ForMod(mi)
            .Named("Expose Unlisted lobbies to friends only")
            .Patching("res://Scenes/Singletons/SteamNetwork.gdc")
            .AddRule(
                new TransformationRuleBuilder()
                    .Named("modify lobby_type enum")
                    .Do(Operation.ReplaceAll)
                    .Matching(TransformationPatternFactory.CreateGdSnippetPattern(
                        """
						1: {"name": "Unlisted", "lobby_type": Steam.LOBBY_TYPE_PUBLIC, "code_button": true, "browser_visible": false, "offline": false},
						"""))
                    .With(
                        """
                        1: {"name": "Friends Only", "lobby_type": Steam.LOBBY_TYPE_FRIENDS_ONLY, "code_button": true, "browser_visible": false, "offline": false},
                        """,
                        1
                    )
            )
            .Build();
    }
}
