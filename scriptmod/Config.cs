using System.Text.Json.Serialization;

namespace uwp.Unofficial_Webfishing_Patch;

public class Config(ConfigFileSchema configFile)
{
	[JsonInclude]
	public bool BigFishMutations = configFile.rareBigFishMutations;

	[JsonInclude]
	public bool DeleteCanvas = configFile.deleteCanvas;
}
