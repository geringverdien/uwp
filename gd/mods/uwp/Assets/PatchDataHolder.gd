extends Object

const gdscript_only_patches = { #wanted name, default value
	"deleteCanvas": true
}

const patch_names := {
	"rareBigFishMutations": "Rare Big Fish Mutations",
	"deleteCanvas": "Prevent Canvas Props"
}

const patch_descriptions := {
	"rareBigFishMutations": "Adds extremely rare chance (1/5000) when catching any fish that it mutates into a BIG fish",
	"deleteCanvas": "Prevents extra chalk spaces from being spawned by players"
}
