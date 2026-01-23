using GDWeave;
using patches;

namespace uwp.Unofficial_Webfishing_Patch;

public class Mod : IMod
{
	public Mod(IModInterface mi)
	{
		var config = new Config(mi.ReadConfig<ConfigFileSchema>());

		if (config.LobbyPlayerCountPatch)
		{
			mi.RegisterScriptMod(LobbyPlayerCountPatch.Create(mi));
		}

		if (config.RevertLobbySizeModdedTagsPatch)
		{
			mi.RegisterScriptMod(RevertLobbySizeModdedTagsPatch.Create(mi));
		}

		if (config.FishingProbabilityPatch)
		{
			mi.RegisterScriptMod(FishingProbabilityPatch.Create(mi));
		}

		if (config.ExtraSmallFishPatch)
		{
			mi.RegisterScriptMod(ExtraSmallFishPatch.Create(mi));
		}

		if (config.FreecamInputFix)
		{
			mi.RegisterScriptMod(FreecamInputFix.Create(mi));
		}

		if (config.GuitarQOLPatch)
		{
			mi.RegisterScriptMod(GuitarQOLPatch.Create(mi));
		}

		if (config.LettersPatch)
		{
			mi.RegisterScriptMod(LettersPatch.Create(mi));
		}

		if (config.LeftoverMenuHotkeyFix)
		{
			mi.RegisterScriptMod(LeftoverMenuHotkeyFix.Create(mi));
		}

		if (config.AntiNetworkingCrashesPatch)
		{
			mi.RegisterScriptMod(AntiNetworkingCrashesPatch.Create(mi));
		}

		if (config.OptionsPatches)
		{
			OptionsPatches.RegisterAll(mi);

		}

		if (config.FriendsOnlyLobbyPatch)
		{
			mi.RegisterScriptMod(FriendsOnlyLobbyPatch.Create(mi));
		}
	}

	public void Dispose()
	{
		// Post-injection cleanup (optional)
	}
}
