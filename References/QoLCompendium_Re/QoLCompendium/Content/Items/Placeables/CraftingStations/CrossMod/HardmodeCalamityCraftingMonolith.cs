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
	// Token: 0x020001D6 RID: 470
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class HardmodeCalamityCraftingMonolith : ModItem
	{
		// Token: 0x06000A38 RID: 2616 RVA: 0x0001E9B2 File Offset: 0x0001CBB2
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.CraftingStations;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0001F1A0 File Offset: 0x0001D3A0
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<HardmodeCalamityMonolithTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.Yellow8, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CraftingStations);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001F1C8 File Offset: 0x0001D3C8
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CraftingStations, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "AncientAltar"), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "AshenAltar"), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "MonolithAmalgam"), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "PlagueInfuser"), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "VoidCondenser"), 1);
			itemRecipe.Register();
		}
	}
}
