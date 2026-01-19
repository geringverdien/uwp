extends VBoxContainer

onready var label = get_node("viewd_label")
onready var slider = get_node("viewd_slider")

var dist = 64


func _ready():
	OptionsMenu.connect("_options_update", self, "_on_option_update")


func _on_option_update():
	var prev_dist = PlayerData.player_options.view_distance
	_on_viewd_slider_value_changed(prev_dist)
	slider.value = prev_dist


func _on_viewd_slider_value_changed(value):
	if value == 8192:
		label.text = "Uncapped distance"
	else:
		var format_string = "%d Units"
		label.text = format_string % value

	dist = value
