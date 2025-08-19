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
	// Token: 0x020001D9 RID: 473
	public class DeteriorationDisplay : ModItem
	{
		// Token: 0x06000A4C RID: 2636 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001F42C File Offset: 0x0001D62C
		public override void SetDefaults()
		{
			base.Item.width = 17;
			base.Item.height = 12;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 3, 0, 0));
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001F480 File Offset: 0x0001D680
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(38, 3);
			itemRecipe.AddIngredient(4413, 1);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 8);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001F4F0 File Offset: 0x0001D6F0
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().deteriorationDisplay = true;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001F4F0 File Offset: 0x0001D6F0
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().deteriorationDisplay = true;
		}
	}
}
