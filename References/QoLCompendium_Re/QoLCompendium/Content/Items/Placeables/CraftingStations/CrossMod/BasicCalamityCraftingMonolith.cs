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
	// Token: 0x020001D3 RID: 467
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class BasicCalamityCraftingMonolith : ModItem
	{
		// Token: 0x06000A26 RID: 2598 RVA: 0x0001E9B2 File Offset: 0x0001CBB2
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.CraftingStations;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001EF50 File Offset: 0x0001D150
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<BasicCalamityMonolithTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CraftingStations);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001EF78 File Offset: 0x0001D178
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CraftingStations, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "WulfrumLabstationItem"), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "EutrophicShelf"), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "StaticRefiner"), 1);
			itemRecipe.Register();
		}
	}
}
