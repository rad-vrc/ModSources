using System;
using System.Collections.Generic;
using QoLCompendium.Content.Tiles.Pylons;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Placeables.Pylons
{
	// Token: 0x020001C9 RID: 457
	public class SkyPylon : ModItem
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x0001E719 File Offset: 0x0001C919
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Pylons;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0001E860 File Offset: 0x0001CA60
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<SkyPylonTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 10, 0, 0));
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0001E75C File Offset: 0x0001C95C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Pylons);
		}
	}
}
