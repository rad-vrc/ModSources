using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace DragonLens.Configs
{
	public class ToolConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[LabelKey("$Mods.DragonLens.ToolConfig.preloadSpawners.Label")]
		[TooltipKey("$Mods.DragonLens.ToolConfig.preloadSpawners.Tooltip")]
		[DefaultValue(true)]
		public bool preloadSpawners;

		[LabelKey("$Mods.DragonLens.ToolConfig.preloadAssets.Label")]
		[TooltipKey("$Mods.DragonLens.ToolConfig.preloadAssets.Tooltip")]
		[DefaultValue(true)]
		public bool preloadAssets;
	}
}
