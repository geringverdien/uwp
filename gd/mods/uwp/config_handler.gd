extends Node

onready var options_tabs = OptionsMenu.get_node("Control/Panel/buttons")

var option_uwp_tab = preload("res://mods/uwp/Scenes/uwpConfigTab/UWP_tab.tscn")

func _init():
	self.name = "ConfigHandler"

func _ready():
	options_tabs.add_child(option_uwp_tab.instance())
