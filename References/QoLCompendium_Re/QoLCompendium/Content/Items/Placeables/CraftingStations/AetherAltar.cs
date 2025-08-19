using System;
using System.Collections.Generic;
using QoLCompendium.Content.Tiles.CraftingStations;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Placeables.CraftingStations
{
	// Token: 0x020001CC RID: 460
	public class AetherAltar : ModItem
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x0001E9B2 File Offset: 0x0001CBB2
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.CraftingStations;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0001E9CC File Offset: 0x0001CBCC
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<AetherAltarTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CraftingStations);
		}
	}
}
