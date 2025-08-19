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
	// Token: 0x020001E6 RID: 486
	public class SkullWatch : ModItem
	{
		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00020040 File Offset: 0x0001E240
		public override void SetDefaults()
		{
			base.Item.width = 15;
			base.Item.height = 16;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 3, 0, 0));
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00020094 File Offset: 0x0001E294
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
			itemRecipe.AddIngredient(85, 8);
			itemRecipe.AddIngredient(38, 1);
			itemRecipe.Register();
			itemRecipe.AddTile(16);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00020102 File Offset: 0x0001E302
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().skullWatch = true;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00020102 File Offset: 0x0001E302
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().skullWatch = true;
		}
	}
}
