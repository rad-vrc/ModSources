using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x02000255 RID: 597
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod",
		"RagnarokMod"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod",
		"RagnarokMod"
	})]
	public class RagnarokPermanentUpgradeTooltip : GlobalItem
	{
		// Token: 0x06000DF9 RID: 3577 RVA: 0x000702E9 File Offset: 0x0006E4E9
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
			{
				this.UsedPermanentUpgrade(item, tooltips);
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00070300 File Offset: 0x0006E500
		public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"))
			{
				OverrideColor = new Color?(Color.LightGreen)
			};
			int bardResourceMax = (int)ModConditions.thoriumMod.Call(new object[]
			{
				"GetBardInspirationMax",
				Main.LocalPlayer
			});
			int essenceMax = 40;
			if (item.type == Common.GetModItem(ModConditions.ragnarokMod, "InspirationEssence"))
			{
				tooltipLine.Text = Common.GetTooltipValue("UsedItemCountable", new object[]
				{
					Math.Clamp(Math.Max(bardResourceMax - essenceMax, 0), 0, 10),
					10
				});
				Common.AddLastTooltip(tooltips, tooltipLine);
			}
		}
	}
}
