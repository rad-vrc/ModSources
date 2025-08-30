using DragonLens.Content.Themes.BoxProviders;
using DragonLens.Content.Themes.IconProviders;
using DragonLens.Core.Systems.ThemeSystem;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace DragonLens.Configs
{
	public enum BoxStyle
	{
		[LabelKey("$Mods.DragonLens.BoxStyle.simple")] simple,
		[LabelKey("$Mods.DragonLens.BoxStyle.vanilla")] vanilla
	}

	public enum IconStyle
	{
		[LabelKey("$Mods.DragonLens.IconStyle.basic")] basic,
		[LabelKey("$Mods.DragonLens.IconStyle.HEROs")] HEROs
	}

	// Title is auto-localized; class-level Label/LabelKey not required in 1.4.4+
	public class GUIConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[LabelKey("$Mods.DragonLens.GUIConfig.backgroundColor.Label")]
		[TooltipKey("$Mods.DragonLens.GUIConfig.backgroundColor.Tooltip")]
		[DefaultValue("25, 35, 100, 200")]
		public Color backgroundColor;

		[LabelKey("$Mods.DragonLens.GUIConfig.buttonColor.Label")]
		[TooltipKey("$Mods.DragonLens.GUIConfig.buttonColor.Tooltip")]
		[DefaultValue("45, 55, 130, 200")]
		public Color buttonColor;

		//Placeholder for better theme menu later. Ignore this shitcode!
		[LabelKey("$Mods.DragonLens.GUIConfig.boxStyle.Label")]
		[TooltipKey("$Mods.DragonLens.GUIConfig.boxStyle.Tooltip")]
		public BoxStyle boxStyle
		{
			get => ThemeHandler.currentBoxProvider is SimpleBoxes ? BoxStyle.simple : BoxStyle.vanilla;
			set
			{
				if (Main.gameMenu)
					return;
				else
					ThemeHandler.currentBoxProvider = value == BoxStyle.simple ? new SimpleBoxes() : new VanillaBoxes();
			}
		}

		[LabelKey("$Mods.DragonLens.GUIConfig.iconStyle.Label")]
		[TooltipKey("$Mods.DragonLens.GUIConfig.iconStyle.Tooltip")]
		public IconStyle iconStyle
		{
			get => ThemeHandler.currentIconProvider is DefaultIcons ? IconStyle.basic : IconStyle.HEROs;
			set
			{
				if (Main.gameMenu)
					return;
				else
					ThemeHandler.currentIconProvider = value == IconStyle.basic ? new DefaultIcons() : new HEROsIcons();
			}
		}
	}
}
