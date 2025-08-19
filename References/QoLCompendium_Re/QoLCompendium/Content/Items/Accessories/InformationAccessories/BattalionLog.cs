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
	// Token: 0x020001D8 RID: 472
	public class BattalionLog : ModItem
	{
		// Token: 0x06000A44 RID: 2628 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001F340 File Offset: 0x0001D540
		public override void SetDefaults()
		{
			base.Item.width = 16;
			base.Item.height = 15;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 3, 0, 0));
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001F3AC File Offset: 0x0001D5AC
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(225, 8);
			itemRecipe.AddIngredient(9, 10);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
			itemRecipe.AddTile(86);
			itemRecipe.Register();
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001F41D File Offset: 0x0001D61D
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().battalionLog = true;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001F41D File Offset: 0x0001D61D
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().battalionLog = true;
		}
	}
}
