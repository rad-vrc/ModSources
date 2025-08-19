using System;
using System.Collections.Generic;
using QoLCompendium.Content.Tiles.CraftingStations;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Placeables.CraftingStations
{
	// Token: 0x020001D2 RID: 466
	public class SpecializedCraftingMonolith : ModItem
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x0001E9B2 File Offset: 0x0001CBB2
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.CraftingStations;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001EE07 File Offset: 0x0001D007
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<SpecializedMonolithTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(0, 10, 0, 0));
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CraftingStations);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001EE30 File Offset: 0x0001D030
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CraftingStations, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(2192, 1);
			itemRecipe.AddIngredient(2194, 1);
			itemRecipe.AddIngredient(2204, 1);
			itemRecipe.AddIngredient(2198, 1);
			itemRecipe.AddIngredient(2196, 1);
			itemRecipe.AddIngredient(2197, 1);
			itemRecipe.AddIngredient(998, 1);
			itemRecipe.AddIngredient(5008, 1);
			itemRecipe.AddIngredient(3000, 1);
			itemRecipe.AddIngredient(398, 1);
			itemRecipe.AddIngredient(1430, 1);
			itemRecipe.AddIngredient(1120, 1);
			itemRecipe.AddIngredient(221, 1);
			itemRecipe.AddIngredient(206, 1);
			itemRecipe.AddIngredient(207, 1);
			itemRecipe.AddIngredient(1128, 1);
			itemRecipe.AddRecipeGroup("QoLCompendium:AnyTombstone", 1);
			itemRecipe.Register();
		}
	}
}
