extends VBoxContainer

onready var label = get_node("fps_label")
onready var slider = get_node("fps_slider")

var FPS = 0


func _ready():
	var monitor_hz = OS.get_screen_refresh_rate()
	slider.set_max(ceil(monitor_hz))
	OptionsMenu.connect("_options_update", self, "_on_options_update")


func _on_options_update():
	var previous_fps = PlayerData.player_options.fps_limit
	_on_fps_slider_value_changed(previous_fps)
	slider.value = previous_fps


func _on_fps_slider_value_changed(value):
	if value == 0:
		label.text = "Unlimited FPS"
	else:
		var format_string = "%d FPS"
		label.text = format_string % value
	FPS = value
