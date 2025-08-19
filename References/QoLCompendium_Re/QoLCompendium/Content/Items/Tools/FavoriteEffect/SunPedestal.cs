using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.FavoriteEffect
{
	// Token: 0x020001BB RID: 443
	public class SunPedestal : ModItem
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x0001C409 File Offset: 0x0001A609
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.MoonPedestals;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001D330 File Offset: 0x0001B530
		public override void SetDefaults()
		{
			base.Item.width = 12;
			base.Item.height = 20;
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 0, 90, 0));
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001C479 File Offset: 0x0001A679
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.MoonPedestals);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001D388 File Offset: 0x0001B588
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.MoonPedestals, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(129, 10);
			itemRecipe.AddIngredient(824, 8);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001D3EF File Offset: 0x0001B5EF
		public override void UpdateInventory(Player player)
		{
			if (base.Item.favorited)
			{
				player.GetModPlayer<QoLCPlayer>().activeItems.Add(base.Item.type);
				player.GetModPlayer<QoLCPlayer>().sunPedestal = true;
			}
		}
	}
}
