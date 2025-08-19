using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Accessories.Construction
{
	// Token: 0x020001F0 RID: 496
	public class MiningEmblem : ModItem
	{
		// Token: 0x06000AFC RID: 2812 RVA: 0x000206F7 File Offset: 0x0001E8F7
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.ConstructionAccessories;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000209EC File Offset: 0x0001EBEC
		public override void SetDefaults()
		{
			base.Item.width = 14;
			base.Item.height = 14;
			base.Item.maxStack = 1;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 1, 0, 0));
			base.Item.accessory = true;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00020A40 File Offset: 0x0001EC40
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.pickSpeed -= 0.15f;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002078E File Offset: 0x0001E98E
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.ConstructionAccessories);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00020A54 File Offset: 0x0001EC54
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.ConstructionAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(20, 5);
			itemRecipe.AddIngredient(703, 5);
			itemRecipe.AddIngredient(22, 5);
			itemRecipe.AddIngredient(704, 5);
			itemRecipe.AddIngredient(21, 5);
			itemRecipe.AddIngredient(705, 5);
			itemRecipe.AddIngredient(19, 5);
			itemRecipe.AddIngredient(706, 5);
			itemRecipe.AddTile(283);
			itemRecipe.Register();
		}
	}
}
