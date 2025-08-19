using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SOTS.Void;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x02000257 RID: 599
	[ExtendsFromMod(new string[]
	{
		"SOTS"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"SOTS"
	})]
	public class SOTSPermanentUpgradeTooltip : GlobalItem
	{
		// Token: 0x06000DFF RID: 3583 RVA: 0x00070466 File Offset: 0x0006E666
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
			{
				this.UsedPermanentUpgrade(item, tooltips);
			}
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0007047C File Offset: 0x0006E67C
		public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"));
			tooltipLine.OverrideColor = new Color?(Color.LightGreen);
			VoidPlayer voidPlayer = VoidPlayer.ModPlayer(Main.LocalPlayer);
			if (item.type == Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ScarletStar") && voidPlayer.voidStar > 0)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "VioletStar") && voidPlayer.voidStar > 0)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "SoulHeart") && voidPlayer.voidSoul > 0)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "VoidenAnkh"))
			{
				tooltipLine.Text = Common.GetTooltipValue("UsedItemCountable", new object[]
				{
					voidPlayer.voidAnkh,
					5
				});
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
		}
	}
}
