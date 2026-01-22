using System.Text.Json.Serialization;

namespace uwp.Unofficial_Webfishing_Patch;

public class Config(ConfigFileSchema configFile) {
    [JsonInclude] public bool LobbyPlayerCountPatch = configFile.LobbyPlayerCountPatch;
    [JsonInclude] public bool RevertLobbySizeModdedTagsPatch = configFile.RevertLobbySizeModdedTagsPatch;
    [JsonInclude] public bool FishingProbabilityPatch = configFile.FishingProbabilityPatch;
    [JsonInclude] public bool ExtraSmallFishPatch = configFile.ExtraSmallFishPatch;
    [JsonInclude] public bool FreecamInputFix = configFile.FreecamInputFix;
    [JsonInclude] public bool GuitarQOLPatch = configFile.GuitarQOLPatch;
    [JsonInclude] public bool LettersPatch = configFile.LettersPatch;
    [JsonInclude] public bool LeftoverMenuHotkeyFix = configFile.LeftoverMenuHotkeyFix;
    [JsonInclude] public bool AntiNetworkingCrashesPatch = configFile.AntiNetworkingCrashesPatch;
    [JsonInclude] public bool OptionsPatches = configFile.OptionsPatches;
 }
