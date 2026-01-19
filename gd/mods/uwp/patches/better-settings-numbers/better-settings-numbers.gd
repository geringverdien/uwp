extends Node


#this is all ugly
onready var main_options_container = get_node("/root/OptionsMenu/Control/Panel/tabs_main/main/ScrollContainer/HBoxContainer/VBoxContainer")
onready var options_separator = main_options_container.get_node("HSeparator")
onready var fps_limit = get_node("/root/OptionsMenu/Control/Panel/tabs_main/main/ScrollContainer/HBoxContainer/VBoxContainer/fps_limit")
onready var view_distance = get_node("/root/OptionsMenu/Control/Panel/tabs_main/main/ScrollContainer/HBoxContainer/VBoxContainer/view_distance")
onready var pixel_amount = get_node("/root/OptionsMenu/Control/Panel/tabs_main/main/ScrollContainer/HBoxContainer/VBoxContainer/pixel_amount")

onready var fps_slider = preload("res://mods/uwp/patches/better-settings-numbers/fps_slider.tscn")
onready var pixel_slider = preload("res://mods/uwp/patches/better-settings-numbers/pixel_slider.tscn")
onready var new_view = preload("res://mods/uwp/patches/better-settings-numbers/view_dist_slider.tscn")

# Called when the node enters the scene tree for the first time.
func _ready():
	var slider_position = options_separator.get_position_in_parent()
	
	var old_fps_dropdown = fps_limit.get_node("fpslmit")
	old_fps_dropdown.visible = false
	fps_limit.add_child(fps_slider.instance())
	main_options_container.move_child(fps_limit, slider_position)
	
	
	var old_pixel_dropwdown = pixel_amount.get_node("pixel")
	old_pixel_dropwdown.visible = false
	pixel_amount.add_child(pixel_slider.instance())
	main_options_container.move_child(pixel_amount, slider_position)
	
	var old_viewd_dropdown = view_distance.get_node("viewd")
	old_viewd_dropdown.visible = false
	view_distance.add_child(new_view.instance())
	main_options_container.move_child(view_distance, slider_position)
	
	
	 # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
