extends VBoxContainer

onready var label = get_node("fps_label")
onready var slider = get_node("fps_slider")

var FPS = 0
# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	var monitor_hz = OS.get_screen_refresh_rate()
	print("THIS IS MONITOR HZ AIUHJAWDIUHAWUID" + str(monitor_hz))
	slider.set_max(ceil(monitor_hz))
	OptionsMenu.connect("_options_update", self, "_on_options_update")


func _on_options_update():
	var previous_fps = PlayerData.player_options.fps_limit
	_on_fps_slider_value_changed(previous_fps)
	slider.value = previous_fps


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_fps_slider_value_changed(value):
	if value == 0:
		label.text = "Unlimited FPS" # Replace with function body.
	else:
		var format_string = "%d FPS"
		label.text = format_string % value
	FPS = value
