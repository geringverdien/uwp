extends Button


func _ready():
	connect("pressed", self, "on_tab_press")


func _enter_tree():
	var parent_node = get_node("..")
	parent_node.move_child(self, 4)


func on_tab_press():
	OptionsMenu._open_tab(3)
