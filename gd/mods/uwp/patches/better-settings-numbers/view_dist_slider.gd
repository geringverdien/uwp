extends VBoxContainer


onready var label = get_node("viewd_label")
onready var slider = get_node("viewd_slider")
# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var dist = 64

# Called when the node enters the scene tree for the first time.
func _ready():
	OptionsMenu.connect("_options_update", self, "_on_option_update")
	
func _on_option_update():
	var prev_dist = PlayerData.player_options.view_distance
	_on_viewd_slider_value_changed(prev_dist)
	slider.value = prev_dist
	 # Replace with function body.




func _on_viewd_slider_value_changed(value):
	if value == 8192:
		label.text = "Uncapped distance"
	else:
		var format_string = "%d Units"
		label.text = format_string % value
		
	dist = value
