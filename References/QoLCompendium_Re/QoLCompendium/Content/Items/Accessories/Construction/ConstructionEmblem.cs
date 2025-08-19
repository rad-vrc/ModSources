using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Accessories.Construction
{
	// Token: 0x020001EE RID: 494
	public class ConstructionEmblem : ModItem
	{
		// Token: 0x06000AF0 RID: 2800 RVA: 0x000206F7 File Offset: 0x0001E8F7
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.ConstructionAccessories;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00020714 File Offset: 0x0001E914
		public override void SetDefaults()
		{
			base.Item.width = 14;
			base.Item.height = 14;
			base.Item.maxStack = 1;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 1, 0, 0));
			base.Item.accessory = true;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00020768 File Offset: 0x0001E968
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.tileSpeed += 0.15f;
			player.wallSpeed += 0.15f;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002078E File Offset: 0x0001E98E
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.ConstructionAccessories);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000207A8 File Offset: 0x0001E9A8
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.ConstructionAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(2, 50);
			itemRecipe.AddIngredient(3, 50);
			itemRecipe.AddIngredient(133, 50);
			itemRecipe.AddIngredient(169, 50);
			itemRecipe.AddIngredient(593, 50);
			itemRecipe.AddTile(283);
			itemRecipe.Register();
		}
	}
}
