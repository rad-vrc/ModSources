using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Accessories.Fishing
{
	// Token: 0x020001ED RID: 493
	public class SonarDevice : ModItem
	{
		// Token: 0x06000AEA RID: 2794 RVA: 0x00020485 File Offset: 0x0001E685
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.FishingAccessories;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00020630 File Offset: 0x0001E830
		public override void SetDefaults()
		{
			base.Item.width = 19;
			base.Item.height = 15;
			base.Item.maxStack = 1;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 1, 0, 0));
			base.Item.accessory = true;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00020684 File Offset: 0x0001E884
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.sonarPotion = true;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000204F4 File Offset: 0x0001E6F4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.FishingAccessories);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00020690 File Offset: 0x0001E890
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.FishingAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 12);
			itemRecipe.AddIngredient(2355, 5);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
