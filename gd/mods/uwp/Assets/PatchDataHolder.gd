extends Object


const patch_names := {
	"LobbyPlayerCountPatch": "Lobby Visibility Patch",
	"RevertLobbySizeModdedTagsPatch": '"Modded" Tag Patch',
	"FishingProbabilityPatch": "Fishing Probablity Patch",
	"ExtraSmallFishPatch": "Fish Size Tag Patch",
	"FreecamInputFix": "Freecam Fix Patch",
	"GuitarQOLPatch": "Guitar Patch",
	"LettersPatch": "Letters Patch",
	"LeftoverMenuHotkeyFix": "Hotkey Fix Patch",
	"AntiNetworkingCrashesPatch": "Networking Crashes Patch",
	"OptionsPatches": "Options Patch",
	"FriendsOnlyLobbyPatch": "Friends Only Lobby Patch",
}

const patch_descriptions := {
	"LobbyPlayerCountPatch": "Fixes the visibility of lobbies with a max player cap above 12",
	"RevertLobbySizeModdedTagsPatch": 'Removes the "Modded" tag from lobbies',
	"FishingProbabilityPatch": "Fixes the size distribution probabilities for new catches",
	"ExtraSmallFishPatch": "Fixes some fishes with unusual sixes not getting their tag and worth multiplier applied correctly",
	"FreecamInputFix": "Fixes chat input moving the freecam when active",
	"GuitarQOLPatch": "Improves guitar audio",
	"LettersPatch": "Tweaks letter notifications",
	"LeftoverMenuHotkeyFix": "Fixes the menu closing while typing",
	"AntiNetworkingCrashesPatch": "Adds some safeguards against networking-related crashes",
	"OptionsPatches": "Replaces the options entries for FPS, pixelization and view distance to more granular sliders",
	"FriendsOnlyLobbyPatch": "Adds a Friends Only lobby type (no lobby code, can only be joined via Steam)",
}
