using GDWeave;
using GDWeave.Modding;
using util.LexicalTransformer;

namespace patches;

/// <summary>
/// Replace calls to the _hammer_guitar signal with calls to _strum_string to fix hammer-ons while sprinting.
/// </summary>
public static class GuitarHammerPatch
{
	public static IScriptMod Create(IModInterface mi)
	{
		return new TransformationRuleScriptModBuilder()
			.ForMod(mi)
			.Named("Fix Guitar Hammer On Sprint")
			.Patching("res://Scenes/Minigames/Guitar/guitar_minigame.gdc")
			.AddRule(
				new TransformationRuleBuilder()
					.Named("Replace _hammer_guitar signal call with _strum_string call")
					.Do(Operation.ReplaceAll)
					.Matching(TransformationPatternFactory.CreateGdSnippetPattern(
						"""
						PlayerData.emit_signal("_hammer_guitar", i, string_frets[i])
						"""))
					.With(
						"""
						_strum_string(0, i)
						""",
						3
					)
			)
			.Build();
	}
}
