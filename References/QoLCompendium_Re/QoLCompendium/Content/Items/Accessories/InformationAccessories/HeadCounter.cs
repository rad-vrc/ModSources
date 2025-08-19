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
	// Token: 0x020001DC RID: 476
	public class HeadCounter : ModItem
	{
		// Token: 0x06000A64 RID: 2660 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001F6B8 File Offset: 0x0001D8B8
		public override void SetDefaults()
		{
			base.Item.width = 19;
			base.Item.height = 16;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 3, 0, 0));
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001F70C File Offset: 0x0001D90C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(170, 8);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 12);
			itemRecipe.AddIngredient(38, 2);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001F77D File Offset: 0x0001D97D
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().headCounter = true;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001F77D File Offset: 0x0001D97D
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().headCounter = true;
		}
	}
}
