extends Object


const patch_names := {
	"AntiNetworkingCrashesPatch": "Networking Crashes Patch",
	"ExtraSmallFishPatch": "Fish Size Tag Patch",
	"FishingProbabilityPatch": "Fishing Probablity Patch",
	"FreecamInputFix": "Freecam Fix Patch",
	"FriendsOnlyLobbyPatch": "Friends Only Lobby Patch",
	"GuitarQOLPatch": "Guitar Patch",
	"LeftoverMenuHotkeyFix": "Hotkey Fix Patch",
	"LettersPatch": "Letters Patch",
	"LobbyPlayerCountPatch": "Lobby Visibility Patch",
	"OptionsPatches": "Options Patch",
	"RevertLobbySizeModdedTagsPatch": '"Modded" Tag Patch',
}

const patch_descriptions := {
	"AntiNetworkingCrashesPatch": "Adds some safeguards against networking-related crashes",
	"ExtraSmallFishPatch": "Fixes some fishes with unusual sixes not getting their tag and worth multiplier applied correctly",
	"FishingProbabilityPatch": "Fixes the size distribution probabilities ",
	"FreecamInputFix": "Fixes chat input moving the freecam when active",
	"FriendsOnlyLobbyPatch": "Adds a Friends Only lobby type (no lobby code, can only be joined via Steam)",
	"GuitarQOLPatch": "Improves guitar audio",
	"LeftoverMenuHotkeyFix": "Fixes the menu closing while typing",
	"LettersPatch": "Tweaks letter notifications",
	"LobbyPlayerCountPatch": "Fixes the visibility of lobbies with a max player cap above 12",
	"OptionsPatches": "Replaces the options entries for FPS, pixelization and view distance to more granular sliders",
	"RevertLobbySizeModdedTagsPatch": 'Removes the "Modded" tag from lobbies',
}

const options := {
	"rareBigFishMutations": {
		"prop": "rareBigFishMutations",
		"name": "Rare Big Fish Mutations",
		"description": "Adds extremely rare chance (1/5000) when catching any fish that it mutates into a BIG fish",
	}
}
