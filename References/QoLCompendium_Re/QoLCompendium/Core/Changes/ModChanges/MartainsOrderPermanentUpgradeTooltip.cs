using System;
using System.Collections.Generic;
using MartainsOrder;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x02000251 RID: 593
	[ExtendsFromMod(new string[]
	{
		"MartainsOrder"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"MartainsOrder"
	})]
	public class MartainsOrderPermanentUpgradeTooltip : GlobalItem
	{
		// Token: 0x06000DED RID: 3565 RVA: 0x0006F9E0 File Offset: 0x0006DBE0
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
			{
				this.UsedPermanentUpgrade(item, tooltips);
			}
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0006F9F8 File Offset: 0x0006DBF8
		public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"));
			tooltipLine.OverrideColor = new Color?(Color.LightGreen);
			if (item.type == Common.GetModItem(ModConditions.martainsOrderMod, "FishOfPurity") && Main.LocalPlayer.GetModPlayer<MyPlayer>().fishOfPurity)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.martainsOrderMod, "FishOfSpirit") && Main.LocalPlayer.GetModPlayer<MyPlayer>().fishOfSpirit)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.martainsOrderMod, "FishOfWrath") && Main.LocalPlayer.GetModPlayer<MyPlayer>().fishOfWrath)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.martainsOrderMod, "ShimmerFish") && Main.LocalPlayer.GetModPlayer<MyPlayer>().shimmerFish)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.martainsOrderMod, "MerchantBag") && Main.LocalPlayer.GetModPlayer<MyPlayer>().shimmerMerchBag)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.martainsOrderMod, "FirstAidTreatments") && MartainWorld.firstAidTreatments)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.martainsOrderMod, "MartiniteBless") && MartainWorld.martiniteBless)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
		}
	}
}
