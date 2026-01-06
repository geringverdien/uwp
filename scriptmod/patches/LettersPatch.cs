using GDWeave;
using GDWeave.Modding;
using util.LexicalTransformer;

namespace patches;

/// <summary>
/// Tweaks to Letters UI/UX
/// </summary>
public static class LettersPatch
{
	public static IScriptMod Create(IModInterface mi)
	{
		return new TransformationRuleScriptModBuilder()
			.ForMod(mi)
			.Named("Letters Patch")
			.Patching("res://Scenes/Singletons/playerdata.gdc")
			.AddRule(
				new TransformationRuleBuilder()
					.Named("Full mailbox notification")
					.Do(Operation.Append)
					.Matching(
						TransformationPatternFactory.CreateGdSnippetPattern(
							"""_send_notification("got a letter, but couldn't fit letter in mailbox!", 1)"""
						)
					)
					.With(
						"""

						Network._update_chat("Someone tried to send you a letter but [b]your mailbox is full! Oh no![/b]")

						""",
						2
					)
			)
			.AddRule(
				new TransformationRuleBuilder()
					.Named("Letter received notifications")
					.Do(Operation.Append)
					.Matching(
						TransformationPatternFactory.CreateGdSnippetPattern(
							"""_send_notification("letter recieved!")"""
						)
					)
					.With(
						"""

						Network._update_chat("[i]You just received a letter from someone![/i]")

						""",
						1
					)
			)
			.Build();
	}
}
