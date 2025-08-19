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
	// Token: 0x020001C5 RID: 453
	public class CorruptionPylon : ModItem
	{
		// Token: 0x060009D9 RID: 2521 RVA: 0x0001E719 File Offset: 0x0001C919
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Pylons;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001E774 File Offset: 0x0001C974
		public override void SetStaticDefaults()
		{
			ItemID.Sets.ShimmerTransformToItem[base.Item.type] = ModContent.ItemType<CrimsonPylon>();
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001E798 File Offset: 0x0001C998
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<CorruptionPylonTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 10, 0, 0));
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001E75C File Offset: 0x0001C95C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Pylons);
		}
	}
}
