using GDWeave;
using GDWeave.Modding;
using util.LexicalTransformer;

namespace patches;

/// <summary>
/// A template patch to use as a starting point for new patches targeting multiple files.
/// </summary>
public static class ExampleMultiPatch
{
	public static void RegisterAll(IModInterface mi)
	{
		mi.RegisterScriptMod(
			new TransformationRuleScriptModBuilder()
				.ForMod(mi)
				.Named("Example Patch")
				.Patching("res://something.gdc")
				.AddRule(
					new TransformationRuleBuilder()
						.Named("Example Patch")
						.Do(Operation.Append)
						.Matching(
							TransformationPatternFactory.CreateGdSnippetPattern(""))
						.With(
							"""

							"""
						)
				)
				.Build()
		);

		mi.RegisterScriptMod(
			new TransformationRuleScriptModBuilder()
				.ForMod(mi)
				.Named("Another Example Patch")
				.Patching("res://something_else.gdc")
				.AddRule(
					new TransformationRuleBuilder()
						.Named("Another Example Patch")
						.Do(Operation.ReplaceAll)
						.Matching(
							TransformationPatternFactory.CreateGdSnippetPattern(""))
						.With(
							"""

							"""
						)
				)
				.Build()
		);

	}
}
