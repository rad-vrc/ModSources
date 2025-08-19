using System;
using System.Collections.Generic;
using FargowiltasSouls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x0200024F RID: 591
	[ExtendsFromMod(new string[]
	{
		"FargowiltasSouls"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"FargowiltasSouls"
	})]
	public class FargosPermanentUpgradeTooltip : GlobalItem
	{
		// Token: 0x06000DE7 RID: 3559 RVA: 0x0006F661 File Offset: 0x0006D861
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
			{
				this.UsedPermanentUpgrade(item, tooltips);
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0006F678 File Offset: 0x0006D878
		public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"));
			tooltipLine.OverrideColor = new Color?(Color.LightGreen);
			if (item.type == Common.GetModItem(ModConditions.fargosSoulsMod, "DeerSinew") && FargoExtensionMethods.FargoSouls(Main.LocalPlayer).DeerSinew)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.fargosSoulsMod, "MutantsCreditCard") && FargoExtensionMethods.FargoSouls(Main.LocalPlayer).MutantsCreditCard)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.fargosSoulsMod, "MutantsDiscountCard") && FargoExtensionMethods.FargoSouls(Main.LocalPlayer).MutantsDiscountCard)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.fargosSoulsMod, "MutantsPact") && FargoExtensionMethods.FargoSouls(Main.LocalPlayer).MutantsPactSlot)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.fargosSoulsMod, "RabiesVaccine") && FargoExtensionMethods.FargoSouls(Main.LocalPlayer).RabiesVaccine)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
		}
	}
}
