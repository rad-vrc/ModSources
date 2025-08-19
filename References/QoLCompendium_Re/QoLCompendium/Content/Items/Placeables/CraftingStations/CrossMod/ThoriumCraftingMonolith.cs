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
	// Token: 0x020001D7 RID: 471
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class ThoriumCraftingMonolith : ModItem
	{
		// Token: 0x06000A3E RID: 2622 RVA: 0x0001E9B2 File Offset: 0x0001CBB2
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.CraftingStations;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001F27E File Offset: 0x0001D47E
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<ThoriumMonolithTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CraftingStations);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CraftingStations, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<BasicThoriumCraftingMonolith>(), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.thoriumMod, "SoulForge"), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.thoriumMod, "GuidesFinalGift"), 1);
			itemRecipe.Register();
		}
	}
}
