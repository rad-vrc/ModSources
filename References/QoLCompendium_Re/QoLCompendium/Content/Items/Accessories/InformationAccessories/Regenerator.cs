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
	// Token: 0x020001E3 RID: 483
	public class Regenerator : ModItem
	{
		// Token: 0x06000A9C RID: 2716 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0001FDCC File Offset: 0x0001DFCC
		public override void SetDefaults()
		{
			base.Item.width = 9;
			base.Item.height = 15;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 3, 0, 0));
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001FE20 File Offset: 0x0001E020
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(29, 1);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 8);
			itemRecipe.AddIngredient(170, 5);
			itemRecipe.Register();
			itemRecipe.AddTile(16);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0001FE90 File Offset: 0x0001E090
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().regenerator = true;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001FE90 File Offset: 0x0001E090
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().regenerator = true;
		}
	}
}
