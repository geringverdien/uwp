## Removes the canvas actor

extends Node

const DEBUG := false

## Cached local player ref
var _local_player

func _debug(msg, data = null) -> void:
	if not DEBUG:
		return
	print("[UWP]: %s" % msg)
	if data != null:
		print(JSON.print(data, "\t"))


func _get_local_player():
	if is_instance_valid(_local_player):
		# Use cache and skip search
		return _local_player
	for actor in get_tree().get_nodes_in_group("actor"):
		if actor.name == "player":
			_local_player = actor
			return _local_player
	return null


func _police_new_actor(data, sender = -1) -> void:
	var actor_type = data.actor_type
	var creator_id = data.creator_id if sender == -1 else Network.STEAM_ID
	var creator_name = Network._get_username_from_id(creator_id)
	if not actor_type == "canvas": return
	yield(get_tree().create_timer(0.125), "timeout")
	clearActor(data.actor_id)
	_debug("Auto-wiped new canvas from %s" % creator_name)


func clearActor(actorID) -> void:
	var local_player = _get_local_player()
	Network._send_actor_action(local_player.actor_id, "_wipe_actor", [actorID])
	local_player._wipe_actor(actorID)


func _ready() -> void:
	var config = get_node("/root/uwp/ConfigHandler")
	if config.gd_only_settings.get("deleteCanvas") == false:
		return
		
	Network.connect("_instance_actor", self, "_police_new_actor")
	_get_local_player()
