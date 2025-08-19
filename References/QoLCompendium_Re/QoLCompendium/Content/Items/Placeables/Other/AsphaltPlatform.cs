using System;
using System.Collections.Generic;
using QoLCompendium.Content.Tiles.Other;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Placeables.Other
{
	// Token: 0x020001CB RID: 459
	public class AsphaltPlatform : ModItem
	{
		// Token: 0x060009F7 RID: 2551 RVA: 0x0001E8B2 File Offset: 0x0001CAB2
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.AsphaltPlatform;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0001E8CC File Offset: 0x0001CACC
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 200;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0001E8DE File Offset: 0x0001CADE
		public override void SetDefaults()
		{
			base.Item.DefaultToPlaceableTile(ModContent.TileType<AsphaltPlatformTile>(), 0);
			base.Item.SetShopValues(ItemRarityColor.White0, Item.buyPrice(0, 0, 0, 0));
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0001E906 File Offset: 0x0001CB06
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.AsphaltPlatform);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0001E920 File Offset: 0x0001CB20
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.AsphaltPlatform, base.Type, 2, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(775, 1);
			itemRecipe.Register();
			Recipe itemRecipe2 = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.AsphaltPlatform, 775, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe2.AddIngredient(ModContent.ItemType<AsphaltPlatform>(), 2);
			itemRecipe2.Register();
		}
	}
}
