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
	// Token: 0x020001E8 RID: 488
	public class TrackingDevice : ModItem
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00020200 File Offset: 0x0001E400
		public override void SetDefaults()
		{
			base.Item.width = 13;
			base.Item.height = 13;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 3, 0, 0));
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00020254 File Offset: 0x0001E454
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(178, 4);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 12);
			itemRecipe.Register();
			itemRecipe.AddTile(16);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000202BB File Offset: 0x0001E4BB
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().trackingDevice = true;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000202BB File Offset: 0x0001E4BB
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().trackingDevice = true;
		}
	}
}
