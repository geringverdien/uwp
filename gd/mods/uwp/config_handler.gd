extends Node

######################################################
### IF IN EDITOR, SET PATH TO UWP CONFIG FILE HERE ###
######################################################

const json_editor_path := "/home/alexander/.config/r2modmanPlus-local/WEBFISHING/profiles/Default/GDWeave/configs/"

######################################################
### IF IN EDITOR, SET PATH TO UWP CONFIG FILE HERE ###
######################################################

onready var options_buttons = OptionsMenu.get_node("Control/Panel/buttons")
onready var options_tabs = OptionsMenu.get_node("Control/Panel/tabs_main")

var option_uwp_tab = preload("res://mods/uwp/Scenes/uwpConfigTab/UWP_tab.tscn")
var options_uwp_container = preload("res://mods/uwp/Scenes/uwpConfigTab/uwp_settings_container.tscn").instance()
var options_uwp_setting = preload("res://mods/uwp/Scenes/uwpConfigTab/uwp_setting.tscn")

var gdweave_settings
var gd_only_settings
var merged_settings: Dictionary
var config_path


func _ready():
	OptionsMenu.connect("_options_update", self, "on_options_update")
	var gd_only_file = File.new() #only needed because on game restart my gdscript only keys get purged from the gdweave json
	if gd_only_file.file_exists("user://uwp_gd_settings.json") == false:
		if gd_only_file.open("user://uwp_gd_settings.json", File.WRITE) == OK:
			gd_only_file.store_string(JSON.print($PatchDataHolder.gdscript_only_patches, "\t"))
			gd_only_file.close()
			
		else:
			print("we couldnt get write access to create a second json")
		
	var gdweave_json_file = get_config_file()
	var gdweave_parsed = JSON.parse(gdweave_json_file.get_as_text())
	if gdweave_parsed.error != OK:
		print("uh oh spaghetti o's we couldnt read the setting file lets bail")
		return

	var gdweave_parse_results = gdweave_parsed.result
	gdweave_settings = gdweave_parse_results.duplicate()
	
	var gd_only_parsed
	if gd_only_file.open("user://uwp_gd_settings.json", File.READ) == OK:
		gd_only_parsed = JSON.parse(gd_only_file.get_as_text())
	else:
		print("couldnt read second json")
	
	var gd_only_result
	if gd_only_parsed.error == OK:
		gd_only_result = gd_only_parsed.result
		gd_only_settings = gd_only_result.duplicate()
		
	gd_only_file.close()
	
	#merged_settings = gdweave_settings.duplicate().merge(gd_only_settings)
	
	options_buttons.add_child(option_uwp_tab.instance())


	for setting in gd_only_settings.keys():
		if not setting in $PatchDataHolder.patch_names:
			continue
		create_setting_container(setting)
		
	for weave_setting in gdweave_settings.keys():
		if not weave_setting in $PatchDataHolder.patch_names:
			continue
		create_setting_container(weave_setting)


func create_setting_container(key):
	var toggles_container = options_uwp_container.get_node("ScrollContainer/HBoxContainer/toggles_container")
	var setting_value
	if key in gdweave_settings:
		setting_value = gdweave_settings.get(key)
		
	else:
		setting_value = gd_only_settings.get(key)

	var new_setting = options_uwp_setting.instance()
	new_setting.name = key

	var label = new_setting.get_node("setting_container/Label")
	label.text = $PatchDataHolder.patch_names.get(key) + ":"
	label.hint_tooltip = $PatchDataHolder.patch_descriptions.get(key)

	var button = new_setting.get_node("setting_container/OptionButton")
	button.add_item("Disabled", 0)
	button.add_item("Enabled", 1)
	button.selected = int(setting_value)

	toggles_container.add_child(new_setting)

	options_tabs.add_child(options_uwp_container)


func get_config_file() -> File:
	if OS.is_debug_build():
		config_path = json_editor_path + "uwp.json"

	else:
		for arg in OS.get_cmdline_args():
			if arg.begins_with("--gdweave-folder-override="):
				var incomplete_path = arg.trim_prefix("--gdweave-folder-override=")
				config_path = incomplete_path + "/configs/uwp.json"

	var json_file = File.new()
	json_file.open(config_path, File.READ)

	return json_file


func on_options_update():
	var uwp_options = OptionsMenu.get_node(
		"Control/Panel/tabs_main/uwp/ScrollContainer/HBoxContainer/toggles_container"
	)
	
	var new_gdweave = gdweave_settings.duplicate()
	var new_gd_only = gd_only_settings.duplicate()

	for child in uwp_options.get_children():
		var patch_name = child.name
		var option_button = child.get_node("setting_container/OptionButton")
		var patch_value = option_button.selected
		if patch_name in new_gd_only:
			new_gd_only[patch_name] = bool(patch_value)
			
		else:
			new_gdweave[patch_name] = bool(patch_value)

	var new_file = File.new()
	if new_file.open(config_path, File.WRITE) == OK:
		new_file.store_string(JSON.print(new_gdweave, "\t"))
		new_file.close()
		
	if new_file.open("user://uwp_gd_settings.json", File.WRITE) == OK:
		new_file.store_string(JSON.print(new_gd_only, "\t"))
		new_file.close()

	print(gdweave_settings)
