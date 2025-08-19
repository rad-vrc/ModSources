using System;
using System.Collections.Generic;
using CalamityMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x0200024A RID: 586
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	public class CalamityPermanentUpgradeTooltip : GlobalItem
	{
		// Token: 0x06000DDA RID: 3546 RVA: 0x0006F0D5 File Offset: 0x0006D2D5
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
			{
				this.UsedPermanentUpgrade(item, tooltips);
			}
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0006F0EC File Offset: 0x0006D2EC
		public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"))
			{
				OverrideColor = new Color?(Color.LightGreen)
			};
			if (item.type == Common.GetModItem(ModConditions.calamityMod, "EnchantedStarfish"))
			{
				tooltipLine.Text = Common.GetTooltipValue("UsedItemCountable", new object[]
				{
					Main.LocalPlayer.ConsumedManaCrystals,
					9
				});
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.calamityMod, "MushroomPlasmaRoot") && CalamityUtils.Calamity(Main.LocalPlayer).rageBoostOne)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.calamityMod, "InfernalBlood") && CalamityUtils.Calamity(Main.LocalPlayer).rageBoostTwo)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.calamityMod, "RedLightningContainer") && CalamityUtils.Calamity(Main.LocalPlayer).rageBoostThree)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.calamityMod, "ElectrolyteGelPack") && CalamityUtils.Calamity(Main.LocalPlayer).adrenalineBoostOne)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.calamityMod, "StarlightFuelCell") && CalamityUtils.Calamity(Main.LocalPlayer).adrenalineBoostTwo)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.calamityMod, "Ectoheart") && CalamityUtils.Calamity(Main.LocalPlayer).adrenalineBoostThree)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.calamityMod, "CelestialOnion") && CalamityUtils.Calamity(Main.LocalPlayer).extraAccessoryML)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
		}
	}
}
