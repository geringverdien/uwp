using GDWeave;
using GDWeave.Modding;
using util.LexicalTransformer;

namespace patches;

/// <summary>
/// Prevents the punch_count variable from going negative or unreasonably high.
/// </summary>
public static class ClampPunchCountPatch
{
	public static IScriptMod Create(IModInterface mi)
	{
		return new TransformationRuleScriptModBuilder()
			.ForMod(mi)
			.Named("Clamp punch_count variable")
			.Patching("res://Scenes/Entities/Player/player.gdc")
			.AddRule(
				new TransformationRuleBuilder()
					.Named("Clamp incrementation")
					.Do(Operation.ReplaceAll)
					.Matching(TransformationPatternFactory.CreateGdSnippetPattern("punch_count += 1"))
					.With(
						"""
						punch_count = clamp(punch_count + 1, 0, 20)
						""",
						1
					)
					.ExpectTimes(2)
			)
			.AddRule(
				new TransformationRuleBuilder()
					.Named("Clamp decrementation")
					.Do(Operation.ReplaceAll)
					.Matching(TransformationPatternFactory.CreateGdSnippetPattern("punch_count -= 10"))
					.With(
						"""
						punch_count = clamp(punch_count - 10, 0, 20)
						""",
						1
					)
			)
			.Build();
	}
}
