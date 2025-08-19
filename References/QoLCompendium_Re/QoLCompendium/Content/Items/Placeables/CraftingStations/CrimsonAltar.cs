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
	// Token: 0x020001CE RID: 462
	public class CrimsonAltar : ModItem
	{
		// Token: 0x06000A08 RID: 2568 RVA: 0x0001E9B2 File Offset: 0x0001CBB2
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.CraftingStations;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0001EB10 File Offset: 0x0001CD10
		public override void SetStaticDefaults()
		{
			ItemID.Sets.ShimmerTransformToItem[base.Item.type] = ModContent.ItemType<AetherAltar>();
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0001EB34 File Offset: 0x0001CD34
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<CrimsonAltarTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 1, 0, 0));
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CraftingStations);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0001EB5C File Offset: 0x0001CD5C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CraftingStations, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(1257, 5);
			itemRecipe.AddIngredient(836, 12);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
