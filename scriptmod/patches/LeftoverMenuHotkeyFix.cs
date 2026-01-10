using GDWeave;
using GDWeave.Modding;
using util.LexicalTransformer;

namespace patches;

/// <summary>
/// Fixes a dumb leftover hotkey that can cause players to accidentally close their menu when typing
/// </summary>
public static class LeftoverMenuHotkeyFix
{
	public static IScriptMod Create(IModInterface mi)
	{
		return new TransformationRuleScriptModBuilder()
			.ForMod(mi)
			.Named("Leftover menu hotkey fix")
			.Patching("res://Scenes/HUD/playerhud.gdc")
			.AddRule(
				new TransformationRuleBuilder()
					.Named("Disable rogue E key handler")
					.Do(Operation.Append)
					.Matching(
						TransformationPatternFactory.CreateGdSnippetPattern(
							"if menu != MENUS.DEFAULT and menu != MENUS.BACKPACK"
						)
					)
					.With("and false")
			)
			.Build();
	}
}
