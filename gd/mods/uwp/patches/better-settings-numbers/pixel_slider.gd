extends VBoxContainer

onready var label = get_node("pixel_label")
onready var slider = get_node("pixel_slider")

var pixel_amount = 0


# Called when the node enters the scene tree for the first time.
func _ready():
	OptionsMenu.connect("_options_update", self, "_on_options_update")

func _on_options_update():
	var prev_pixel = PlayerData.player_options.pixel
	_on_pixel_slider_value_changed(prev_pixel)
	slider.value = prev_pixel




func _on_pixel_slider_value_changed(value):
	if value == 1:
		label.text = "Pixelization disabled"  # Replace with function body.
		
	else:
		var format_string = "%.1f Strength"
		label.text = format_string % value
		
	pixel_amount = value
