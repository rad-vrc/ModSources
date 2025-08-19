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
	// Token: 0x020001D5 RID: 469
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class CalamityCraftingMonolith : ModItem
	{
		// Token: 0x06000A32 RID: 2610 RVA: 0x0001E9B2 File Offset: 0x0001CBB2
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.CraftingStations;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001F0A6 File Offset: 0x0001D2A6
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<CalamityMonolithTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CraftingStations);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001F0D0 File Offset: 0x0001D2D0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CraftingStations, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<BasicCalamityCraftingMonolith>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<HardmodeCalamityCraftingMonolith>(), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "ProfanedCrucible"), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "BotanicPlanter"), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "SilvaBasin"), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "AltarOfTheAccursedItem"), 1);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "DraedonsForge"), 1);
			itemRecipe.Register();
		}
	}
}
