using System;
using System.Collections.Generic;
using ContinentOfJourney.Items;
using ContinentOfJourney.Items.Accessories.PermanentUpgradesSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x02000250 RID: 592
	[ExtendsFromMod(new string[]
	{
		"ContinentOfJourney"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"ContinentOfJourney"
	})]
	public class HomewardJourneyPermanentUpgradeTooltip : GlobalItem
	{
		// Token: 0x06000DEA RID: 3562 RVA: 0x0006F79B File Offset: 0x0006D99B
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
			{
				this.UsedPermanentUpgrade(item, tooltips);
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0006F7B4 File Offset: 0x0006D9B4
		public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"));
			tooltipLine.OverrideColor = new Color?(Color.LightGreen);
			if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "Americano") && Main.LocalPlayer.GetModPlayer<CoffeePlayer>().Americano > 0)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "Latte") && Main.LocalPlayer.GetModPlayer<CoffeePlayer>().Latte > 0)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "Mocha") && Main.LocalPlayer.GetModPlayer<CoffeePlayer>().Mocha > 0)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "Cappuccino") && Main.LocalPlayer.GetModPlayer<CoffeePlayer>().Cappuccino > 0)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "AirHandcanon") && Main.LocalPlayer.GetModPlayer<OtherUpgradesPlayer>().AirHandcanon > 0)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "HotCase") && Main.LocalPlayer.GetModPlayer<OtherUpgradesPlayer>().HotCase > 0)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "GreatCrystal") && Main.LocalPlayer.GetModPlayer<OtherUpgradesPlayer>().GreatCrystal > 0)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "WhimInABottle") && Main.LocalPlayer.GetModPlayer<OtherUpgradesPlayer>().WhimInABottle > 0)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "SunsHeart") && Main.LocalPlayer.GetModPlayer<OtherUpgradesPlayer>().SunsHeart > 0)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "TheSwitch") && Main.LocalPlayer.GetModPlayer<PermanentUpgradesPlayer>().PermanentUpgradesActivated[0] && Main.LocalPlayer.GetModPlayer<PermanentUpgradesPlayer>().PermanentUpgradesActivated[1])
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
		}
	}
}
