using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.BaseExtension;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x02000256 RID: 598
	[ExtendsFromMod(new string[]
	{
		"Redemption"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"Redemption"
	})]
	public class RedemptionPermanentUpgradeTooltip : GlobalItem
	{
		// Token: 0x06000DFC RID: 3580 RVA: 0x000703B8 File Offset: 0x0006E5B8
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
			{
				this.UsedPermanentUpgrade(item, tooltips);
			}
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x000703D0 File Offset: 0x0006E5D0
		public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"));
			tooltipLine.OverrideColor = new Color?(Color.LightGreen);
			if (item.type == Common.GetModItem(ModConditions.redemptionMod, "GalaxyHeart") && BaseExtension.Redemption(Main.LocalPlayer).galaxyHeart)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.redemptionMod, "MedicKit") && BaseExtension.Redemption(Main.LocalPlayer).medKit)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
		}
	}
}
