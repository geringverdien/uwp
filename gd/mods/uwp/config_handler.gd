extends Node

######################################################
### IF IN EDITOR, SET PATH TO UWP CONFIG FILE HERE ###
######################################################

var json_path := "/home/alexander/.config/r2modmanPlus-local/WEBFISHING/profiles/leer/GDWeave/configs/"

######################################################
### IF IN EDITOR, SET PATH TO UWP CONFIG FILE HERE ###
######################################################



onready var options_buttons = OptionsMenu.get_node("Control/Panel/buttons")
onready var options_tabs = OptionsMenu.get_node("Control/Panel/tabs_main")

var option_uwp_tab = preload("res://mods/uwp/Scenes/uwpConfigTab/UWP_tab.tscn")
var options_uwp_container = preload("res://mods/uwp/Scenes/uwpConfigTab/uwp_settings_container.tscn").instance()
var options_uwp_setting = preload("res://mods/uwp/Scenes/uwpConfigTab/uwp_setting.tscn")

var parse_results

func _ready():
	var json_file = get_config_file()
	var parsed = JSON.parse(json_file.get_as_text())
	if parsed.error != OK:
		print("uh oh spaghetti o's we couldnt read the setting file lets bail")
		return
	
	parse_results = parsed.result
	options_buttons.add_child(option_uwp_tab.instance())
	var toggles_container = options_uwp_container.get_node("ScrollContainer/HBoxContainer/toggles_container")
	
	for setting in parse_results.keys():
		var setting_value = parse_results.get(setting)
		var new_setting = options_uwp_setting.instance()
		new_setting.name = setting
		
		var label = new_setting.get_node("setting_container/Label")
		label.text = $PatchDataHolder.patch_names.get(setting) + ":"
		label.hint_tooltip = $PatchDataHolder.patch_descriptions.get(setting)
		
		var button = new_setting.get_node("setting_container/OptionButton")
		button.add_item("Disabled", 0)
		button.add_item("Enabled", 1)
		button.selected = int(setting_value)
		
		toggles_container.add_child(new_setting)
	
	options_tabs.add_child(options_uwp_container)
	
		
		
func get_config_file() -> File:
	var config_path
	if OS.is_debug_build():
		config_path = json_path + "uwp.json"
		
	else:
		for arg in OS.get_cmdline_args():
			if arg.begins_with("--gdweave-folder-override="):
				var incomplete_path = arg.trim_prefix("--gdweave-folder-override=")
				config_path = incomplete_path + "/configs/uwp.json"

	var json_file = File.new()
	json_file.open(config_path, File.READ)
	
	return json_file
	
