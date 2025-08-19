using System;
using System.Collections.Generic;
using QoLCompendium.Content.Tiles.CraftingStations.CrossMod;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Placeables.CraftingStations.CrossMod
{
	// Token: 0x020001D4 RID: 468
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class BasicThoriumCraftingMonolith : ModItem
	{
		// Token: 0x06000A2C RID: 2604 RVA: 0x0001E9B2 File Offset: 0x0001CBB2
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.CraftingStations;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0001F000 File Offset: 0x0001D200
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<BasicThoriumMonolithTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CraftingStations);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001F028 File Offset: 0x0001D228
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CraftingStations, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.thoriumMod, "ThoriumAnvil"), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.thoriumMod, "ArcaneArmorFabricator"), 1);
			itemRecipe.AddRecipeGroup("QoLCompendium:GrimPedestals", 1);
			itemRecipe.Register();
		}
	}
}
