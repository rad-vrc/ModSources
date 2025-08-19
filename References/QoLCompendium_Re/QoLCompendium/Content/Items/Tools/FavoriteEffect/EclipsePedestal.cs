using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.FavoriteEffect
{
	// Token: 0x020001B6 RID: 438
	public class EclipsePedestal : ModItem
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x0001C409 File Offset: 0x0001A609
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.MoonPedestals;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001C8CC File Offset: 0x0001AACC
		public override void SetDefaults()
		{
			base.Item.width = 12;
			base.Item.height = 20;
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.SetShopValues(ItemRarityColor.Pink5, Item.buyPrice(0, 2, 0, 0));
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0001C479 File Offset: 0x0001A679
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.MoonPedestals);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001C920 File Offset: 0x0001AB20
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.MoonPedestals, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(129, 10);
			itemRecipe.AddIngredient(3, 4);
			itemRecipe.AddIngredient(824, 4);
			itemRecipe.AddIngredient(1225, 5);
			itemRecipe.AddTile(134);
			itemRecipe.Register();
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001C9A0 File Offset: 0x0001ABA0
		public override void UpdateInventory(Player player)
		{
			if (base.Item.favorited)
			{
				player.GetModPlayer<QoLCPlayer>().activeItems.Add(base.Item.type);
				player.GetModPlayer<QoLCPlayer>().eclipsePedestal = true;
			}
		}
	}
}
