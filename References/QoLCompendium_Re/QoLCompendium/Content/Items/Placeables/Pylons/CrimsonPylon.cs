using System;
using System.Collections.Generic;
using QoLCompendium.Content.Tiles.Pylons;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Placeables.Pylons
{
	// Token: 0x020001C6 RID: 454
	public class CrimsonPylon : ModItem
	{
		// Token: 0x060009DE RID: 2526 RVA: 0x0001E719 File Offset: 0x0001C919
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Pylons;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001E7C1 File Offset: 0x0001C9C1
		public override void SetStaticDefaults()
		{
			ItemID.Sets.ShimmerTransformToItem[base.Item.type] = ModContent.ItemType<CorruptionPylon>();
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0001E7E5 File Offset: 0x0001C9E5
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<CrimsonPylonTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 10, 0, 0));
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0001E75C File Offset: 0x0001C95C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Pylons);
		}
	}
}
