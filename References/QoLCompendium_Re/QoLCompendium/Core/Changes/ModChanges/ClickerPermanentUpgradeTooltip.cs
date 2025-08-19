using System;
using System.Collections.Generic;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x0200024B RID: 587
	[ExtendsFromMod(new string[]
	{
		"ClickerClass"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"ClickerClass"
	})]
	public class ClickerPermanentUpgradeTooltip : GlobalItem
	{
		// Token: 0x06000DDD RID: 3549 RVA: 0x0006F2BD File Offset: 0x0006D4BD
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
			{
				this.UsedPermanentUpgrade(item, tooltips);
			}
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0006F2D4 File Offset: 0x0006D4D4
		public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"));
			tooltipLine.OverrideColor = new Color?(Color.LightGreen);
			if (item.type == Common.GetModItem(ModConditions.clickerClassMod, "HeavenlyChip") && Main.LocalPlayer.GetModPlayer<ClickerPlayer>().consumedHeavenlyChip)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
		}
	}
}
