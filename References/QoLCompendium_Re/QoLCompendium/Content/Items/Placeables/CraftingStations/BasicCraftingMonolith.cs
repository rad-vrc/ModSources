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
	// Token: 0x020001CD RID: 461
	public class BasicCraftingMonolith : ModItem
	{
		// Token: 0x06000A02 RID: 2562 RVA: 0x0001E9B2 File Offset: 0x0001CBB2
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.CraftingStations;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0001EA0C File Offset: 0x0001CC0C
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<BasicMonolithTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CraftingStations);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0001EA34 File Offset: 0x0001CC34
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CraftingStations, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddRecipeGroup("QoLCompendium:AnyWorkBench", 1);
			itemRecipe.AddIngredient(33, 1);
			itemRecipe.AddRecipeGroup("QoLCompendium:Anvils", 1);
			itemRecipe.AddRecipeGroup("QoLCompendium:AnyTable", 1);
			itemRecipe.AddRecipeGroup("QoLCompendium:AnyChair", 1);
			itemRecipe.AddRecipeGroup("QoLCompendium:AnyCookingPot", 1);
			itemRecipe.AddIngredient(2172, 1);
			itemRecipe.AddIngredient(363, 1);
			itemRecipe.AddIngredient(332, 1);
			itemRecipe.AddIngredient(352, 1);
			itemRecipe.AddRecipeGroup("QoLCompendium:AnySink", 1);
			itemRecipe.AddRecipeGroup("QoLCompendium:AnyBottle", 1);
			itemRecipe.Register();
		}
	}
}
