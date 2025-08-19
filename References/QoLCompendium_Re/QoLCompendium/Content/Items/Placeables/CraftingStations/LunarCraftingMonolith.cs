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
	// Token: 0x020001D1 RID: 465
	public class LunarCraftingMonolith : ModItem
	{
		// Token: 0x06000A1A RID: 2586 RVA: 0x0001E9B2 File Offset: 0x0001CBB2
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.CraftingStations;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001ED30 File Offset: 0x0001CF30
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<LunarMonolithTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 20, 0, 0));
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CraftingStations);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001ED5C File Offset: 0x0001CF5C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CraftingStations, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<BasicCraftingMonolith>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<SpecializedCraftingMonolith>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<HardmodeCraftingMonolith>(), 1);
			itemRecipe.AddIngredient(3549, 1);
			itemRecipe.AddIngredient(1551, 1);
			itemRecipe.AddIngredient(2195, 1);
			itemRecipe.AddRecipeGroup("QoLCompendium:Altars", 1);
			itemRecipe.AddIngredient(ModContent.ItemType<AetherAltar>(), 1);
			itemRecipe.Register();
		}
	}
}
