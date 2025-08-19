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
	// Token: 0x020001E1 RID: 481
	public class MetallicClover : ModItem
	{
		// Token: 0x06000A8C RID: 2700 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001FC1C File Offset: 0x0001DE1C
		public override void SetDefaults()
		{
			base.Item.width = 15;
			base.Item.height = 15;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 3, 0, 0));
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001FC70 File Offset: 0x0001DE70
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(210, 4);
			itemRecipe.AddIngredient(4412, 1);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 8);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001FCE3 File Offset: 0x0001DEE3
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().metallicClover = true;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001FCE3 File Offset: 0x0001DEE3
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().metallicClover = true;
		}
	}
}
