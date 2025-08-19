using System;
using System.Collections.Generic;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Fishing
{
	// Token: 0x020001B0 RID: 432
	public class BottomlessChumBucket : ModItem
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x00014A48 File Offset: 0x00012C48
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.BottomlessBuckets;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001BFF0 File Offset: 0x0001A1F0
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(4608);
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.width = 15;
			base.Item.height = 14;
			base.Item.SetShopValues(ItemRarityColor.Lime7, Item.buyPrice(0, 10, 0, 0));
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00014C08 File Offset: 0x00012E08
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.BottomlessBuckets);
		}
	}
}
