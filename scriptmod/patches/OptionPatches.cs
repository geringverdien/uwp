using GDWeave;
using GDWeave.Modding;
using util.LexicalTransformer;

namespace patches;

/// <summary>
/// A template patch to use as a starting point for new patches.
/// </summary>
public static class OptionsPatches
{
	public static void RegisterAll(IModInterface mi)
	{
		mi.RegisterScriptMod(
			new TransformationRuleScriptModBuilder()
				.ForMod(mi)
				.Named("Options patches")
				.Patching("res://Scenes/Singletons/OptionsMenu/options_menu.gdc")
				.AddRule(
					new TransformationRuleBuilder()
						.Named("FPS apply to slider")
						.Do(Operation.ReplaceAll)
						.Matching(
							TransformationPatternFactory.CreateGdSnippetPattern(
								"""PlayerData.player_options.fps_limit = [30, 60, 120, 0][$"%fpslmit".selected]"""
							)
						)
						.With(
							"""

							PlayerData.player_options.fps_limit = get_node("Control/Panel/tabs_main/main/ScrollContainer/HBoxContainer/VBoxContainer/fps_limit/fps_vbox").FPS

							""",
							1
						)
				)
				.AddRule(
					new TransformationRuleBuilder()
						.Named("Pixel apply to slider")
						.Do(Operation.ReplaceAll)
						.Matching(
							TransformationPatternFactory.CreateGdSnippetPattern(
								"""PlayerData.player_options.pixel = $"%pixel".selected"""
							)
						)
						.With(
							"""

							PlayerData.player_options.pixel = get_node("Control/Panel/tabs_main/main/ScrollContainer/HBoxContainer/VBoxContainer/pixel_amount/pixel_vbox").pixel_amount

							""",
							1
						)
				)
				.AddRule(
					new TransformationRuleBuilder()
						.Named("Uncap pixel Globals call")
						.Do(Operation.ReplaceAll)
						.Matching(
							TransformationPatternFactory.CreateGdSnippetPattern(
								"""Globals.pixelize_amount = [1.0, 2.25, 6.0, 16.0][PlayerData.player_options.pixel]"""
							)
						)
						.With(
							"""

							Globals.pixelize_amount = PlayerData.player_options.pixel

							""",
							1
						)
				)
				.AddRule(
					new TransformationRuleBuilder()
						.Named("View distance apply to slider")
						.Do(Operation.ReplaceAll)
						.Matching(
							TransformationPatternFactory.CreateGdSnippetPattern(
								"""PlayerData.player_options.view_distance = $"%viewd".selected"""
							)
						)
						.With(
							"""

							PlayerData.player_options.view_distance = get_node("Control/Panel/tabs_main/main/ScrollContainer/HBoxContainer/VBoxContainer/view_distance/viewd_vbox").dist

							""",
							1
						)
				)
				.Build()
		);

		mi.RegisterScriptMod(
			new TransformationRuleScriptModBuilder()
				.ForMod(mi)
				.Named("Camera view distance patch")
				.Patching("res://Scenes/Entities/Player/player_camera.gdc")
				.AddRule(
					new TransformationRuleBuilder()
						.Named("Uncap camera view distance")
						.Do(Operation.ReplaceAll)
						.Matching(
							TransformationPatternFactory.CreateGdSnippetPattern(
								"""far = [8192, 250, 120, 25][PlayerData.player_options.view_distance]"""
							)
						)
						.With(
							"""

							far = PlayerData.player_options.view_distance

							""",
							1
						)
				)
				.Build()
		);

		mi.RegisterScriptMod(
			new TransformationRuleScriptModBuilder()
				.ForMod(mi)
				.Named("World env view distance patch")
				.Patching("res://Scenes/Map/game_worldenv.gdc")
				.AddRule(
					new TransformationRuleBuilder()
						.Named("Uncap env view distance")
						.Do(Operation.ReplaceAll)
						.Matching(
							TransformationPatternFactory.CreateGdSnippetPattern(
								"""
								default_fog = [125, 50, 15, 6][PlayerData.player_options.view_distance]
								default_fog_end = [500, 200, 75, 12][PlayerData.player_options.view_distance]
								"""
							)
						)
						.With(
							"""

							default_fog = PlayerData.player_options.view_distance
							default_fog_end = PlayerData.player_options.view_distance * 4

							""",
							1
						)
				)
				.Build()
		);
	}
}
