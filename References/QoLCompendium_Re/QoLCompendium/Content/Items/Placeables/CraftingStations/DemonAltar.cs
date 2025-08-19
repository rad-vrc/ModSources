using System;
using System.Collections.Generic;
using QoLCompendium.Content.Tiles.CraftingStations;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Placeables.CraftingStations
{
	// Token: 0x020001CF RID: 463
	public class DemonAltar : ModItem
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x0001E9B2 File Offset: 0x0001CBB2
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.CraftingStations;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001EB10 File Offset: 0x0001CD10
		public override void SetStaticDefaults()
		{
			ItemID.Sets.ShimmerTransformToItem[base.Item.type] = ModContent.ItemType<AetherAltar>();
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001EBC3 File Offset: 0x0001CDC3
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<DemonAltarTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 1, 0, 0));
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CraftingStations);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001EBEC File Offset: 0x0001CDEC
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CraftingStations, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(57, 5);
			itemRecipe.AddIngredient(61, 12);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
