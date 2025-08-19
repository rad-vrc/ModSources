using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ItemChanges
{
	// Token: 0x02000260 RID: 608
	public class NotConsumableBossSummons : GlobalItem
	{
		// Token: 0x06000E22 RID: 3618 RVA: 0x00071D1C File Offset: 0x0006FF1C
		public override bool AppliesToEntity(Item entity, bool lateInstantiation)
		{
			return (Common.VanillaBossAndEventSummons.Contains(entity.type) && QoLCompendium.mainConfig.EndlessBossSummons && !ModConditions.calamityLoaded) || (Common.VanillaRightClickBossAndEventSummons.Contains(entity.type) && QoLCompendium.mainConfig.EndlessBossSummons) || (Common.ModdedBossAndEventSummons.Contains(entity.type) && QoLCompendium.mainConfig.EndlessBossSummons) || (Common.FargosBossAndEventSummons.Contains(entity.type) && QoLCompendium.mainConfig.EndlessBossSummons);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00071DB1 File Offset: 0x0006FFB1
		public override void SetDefaults(Item item)
		{
			item.consumable = false;
			if (!Common.FargosBossAndEventSummons.Contains(item.type))
			{
				item.maxStack = 1;
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0000404D File Offset: 0x0000224D
		public override bool ConsumeItem(Item item, Player player)
		{
			return false;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00071DD4 File Offset: 0x0006FFD4
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine tip = tooltips.Find((TooltipLine l) => l.Name == "Tooltip0");
			TooltipLine text = new TooltipLine(base.Mod, "NotConsumable", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.NotConsumable"));
			tooltips.Insert(tooltips.IndexOf(tip), text);
		}
	}
}
