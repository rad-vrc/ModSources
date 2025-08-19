using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.FavoriteEffect
{
	// Token: 0x020001B8 RID: 440
	public class MoonPedestal : ModItem
	{
		// Token: 0x06000974 RID: 2420 RVA: 0x0001C409 File Offset: 0x0001A609
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.MoonPedestals;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001D038 File Offset: 0x0001B238
		public override void SetDefaults()
		{
			base.Item.width = 10;
			base.Item.height = 18;
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 0, 90, 0));
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001C479 File Offset: 0x0001A679
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.MoonPedestals);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001D090 File Offset: 0x0001B290
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.MoonPedestals, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(129, 10);
			itemRecipe.AddIngredient(3, 8);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001D0F3 File Offset: 0x0001B2F3
		public override void UpdateInventory(Player player)
		{
			if (base.Item.favorited)
			{
				player.GetModPlayer<QoLCPlayer>().activeItems.Add(base.Item.type);
				player.GetModPlayer<QoLCPlayer>().moonPedestal = true;
			}
		}
	}
}
