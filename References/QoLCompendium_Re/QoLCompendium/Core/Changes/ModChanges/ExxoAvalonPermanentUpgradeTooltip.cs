using System;
using System.Collections.Generic;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x0200024E RID: 590
	[ExtendsFromMod(new string[]
	{
		"Avalon"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"Avalon"
	})]
	public class ExxoAvalonPermanentUpgradeTooltip : GlobalItem
	{
		// Token: 0x06000DE4 RID: 3556 RVA: 0x0006F588 File Offset: 0x0006D788
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
			{
				this.UsedPermanentUpgrade(item, tooltips);
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0006F5A0 File Offset: 0x0006D7A0
		public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"));
			tooltipLine.OverrideColor = new Color?(Color.LightGreen);
			if (item.type == Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "StaminaCrystal"))
			{
				tooltipLine.Text = Common.GetTooltipValue("UsedItemCountable", new object[]
				{
					Main.LocalPlayer.GetModPlayer<AvalonStaminaPlayer>().StatStam / 30 - 1,
					9
				});
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "EnergyCrystal") && Main.LocalPlayer.GetModPlayer<AvalonStaminaPlayer>().EnergyCrystal)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
		}
	}
}
