using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod.Utilities;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x02000259 RID: 601
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	public class ThoriumPermanentUpgradeTooltip : GlobalItem
	{
		// Token: 0x06000E06 RID: 3590 RVA: 0x00070746 File Offset: 0x0006E946
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
			{
				this.UsedPermanentUpgrade(item, tooltips);
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0007075C File Offset: 0x0006E95C
		public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"));
			tooltipLine.OverrideColor = new Color?(Color.LightGreen);
			if (item.type == Common.GetModItem(ModConditions.thoriumMod, "CrystalWave"))
			{
				tooltipLine.Text = Common.GetTooltipValue("UsedItemCountable", new object[]
				{
					PlayerHelper.GetThoriumPlayer(Main.LocalPlayer).consumedCrystalWaveCount,
					5
				});
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.thoriumMod, "AstralWave") && PlayerHelper.GetThoriumPlayer(Main.LocalPlayer).consumedAstralWave)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.thoriumMod, "InspirationGem") && PlayerHelper.GetThoriumPlayer(Main.LocalPlayer).consumedInspirationGem)
			{
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			int bardResourceMax = (int)ModConditions.thoriumMod.Call(new object[]
			{
				"GetBardInspirationMax",
				Main.LocalPlayer
			});
			int fragmentMax = 10;
			int shardMax = 20;
			int crystalMax = 30;
			if (item.type == Common.GetModItem(ModConditions.thoriumMod, "InspirationFragment"))
			{
				tooltipLine.Text = Common.GetTooltipValue("UsedItemCountable", new object[]
				{
					Math.Clamp(Math.Max(bardResourceMax - fragmentMax, 0), 0, 10),
					10
				});
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.thoriumMod, "InspirationShard"))
			{
				tooltipLine.Text = Common.GetTooltipValue("UsedItemCountable", new object[]
				{
					Math.Clamp(Math.Max(bardResourceMax - shardMax, 0), 0, 10),
					10
				});
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
			if (item.type == Common.GetModItem(ModConditions.thoriumMod, "InspirationCrystalNew"))
			{
				tooltipLine.Text = Common.GetTooltipValue("UsedItemCountable", new object[]
				{
					Math.Clamp(Math.Max(bardResourceMax - crystalMax, 0), 0, 10),
					10
				});
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
		}
	}
}
