using System.Text.Json.Serialization;

namespace uwp.Unofficial_Webfishing_Patch;

/// <summary>
/// </summary>
public class ConfigFileSchema { 
    [JsonInclude] public bool LobbyPlayerCountPatch = true;
    [JsonInclude] public bool RevertLobbySizeModdedTagsPatch = true;
    [JsonInclude] public bool FishingProbabilityPatch = true;
    [JsonInclude] public bool ExtraSmallFishPatch = true;
    [JsonInclude] public bool FreecamInputFix = true;
    [JsonInclude] public bool GuitarQOLPatch = true;
    [JsonInclude] public bool LettersPatch = true;
    [JsonInclude] public bool LeftoverMenuHotkeyFix = true;
    [JsonInclude] public bool AntiNetworkingCrashesPatch = true;
    [JsonInclude] public bool OptionsPatches = true;
}
