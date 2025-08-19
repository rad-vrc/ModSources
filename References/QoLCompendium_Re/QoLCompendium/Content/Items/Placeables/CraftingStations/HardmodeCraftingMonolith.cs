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
	// Token: 0x020001D0 RID: 464
	public class HardmodeCraftingMonolith : ModItem
	{
		// Token: 0x06000A14 RID: 2580 RVA: 0x0001E9B2 File Offset: 0x0001CBB2
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.CraftingStations;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0001EC4D File Offset: 0x0001CE4D
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<HardmodeMonolithTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.Pink5, Item.buyPrice(0, 10, 0, 0));
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CraftingStations);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001EC78 File Offset: 0x0001CE78
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CraftingStations, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddRecipeGroup("QoLCompendium:HardmodeAnvils", 1);
			itemRecipe.AddRecipeGroup("QoLCompendium:HardmodeForges", 1);
			itemRecipe.AddRecipeGroup("QoLCompendium:AnyBookcase", 1);
			itemRecipe.AddIngredient(487, 1);
			itemRecipe.AddIngredient(2193, 1);
			itemRecipe.AddIngredient(4142, 1);
			itemRecipe.AddIngredient(2203, 1);
			itemRecipe.AddIngredient(995, 1);
			itemRecipe.AddIngredient(996, 1);
			itemRecipe.Register();
		}
	}
}
