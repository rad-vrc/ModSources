using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.FavoriteEffect
{
	// Token: 0x020001B9 RID: 441
	public class Paperweight : ModItem
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x0001D129 File Offset: 0x0001B329
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Paperweight;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0001D143 File Offset: 0x0001B343
		public override void SetDefaults()
		{
			base.Item.width = 11;
			base.Item.height = 8;
			base.Item.maxStack = 1;
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 1, 0, 0));
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001D17F File Offset: 0x0001B37F
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Paperweight);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001D198 File Offset: 0x0001B398
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.Paperweight, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 6);
			itemRecipe.AddIngredient(170, 5);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001D1FE File Offset: 0x0001B3FE
		public override void UpdateInventory(Player player)
		{
			if (base.Item.favorited)
			{
				player.GetModPlayer<QoLCPlayer>().activeItems.Add(base.Item.type);
				if (!Main.playerInventory)
				{
					player.controlUseItem = true;
				}
			}
		}
	}
}
