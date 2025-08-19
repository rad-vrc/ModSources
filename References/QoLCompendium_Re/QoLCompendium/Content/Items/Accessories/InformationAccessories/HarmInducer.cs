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
	// Token: 0x020001DB RID: 475
	public class HarmInducer : ModItem
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001F5F0 File Offset: 0x0001D7F0
		public override void SetDefaults()
		{
			base.Item.width = 14;
			base.Item.height = 17;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 3, 0, 0));
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001F644 File Offset: 0x0001D844
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(9, 12);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 6);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().harmInducer = true;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().harmInducer = true;
		}
	}
}
