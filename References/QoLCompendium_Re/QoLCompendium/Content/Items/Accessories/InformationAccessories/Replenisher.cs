using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using QoLCompendium.Core.UI.Other;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Accessories.InformationAccessories
{
	// Token: 0x020001E5 RID: 485
	public class Replenisher : ModItem
	{
		// Token: 0x06000AAC RID: 2732 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001FF6C File Offset: 0x0001E16C
		public override void SetDefaults()
		{
			base.Item.width = 9;
			base.Item.height = 15;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 3, 0, 0));
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001FFC0 File Offset: 0x0001E1C0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(109, 1);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 8);
			itemRecipe.AddIngredient(170, 5);
			itemRecipe.Register();
			itemRecipe.AddTile(16);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00020030 File Offset: 0x0001E230
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().replenisher = true;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00020030 File Offset: 0x0001E230
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().replenisher = true;
		}
	}
}
