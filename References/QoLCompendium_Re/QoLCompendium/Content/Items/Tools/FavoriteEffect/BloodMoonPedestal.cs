using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.FavoriteEffect
{
	// Token: 0x020001B4 RID: 436
	public class BloodMoonPedestal : ModItem
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x0001C409 File Offset: 0x0001A609
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.MoonPedestals;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001C424 File Offset: 0x0001A624
		public override void SetDefaults()
		{
			base.Item.width = 10;
			base.Item.height = 18;
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 0, 90, 0));
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0001C479 File Offset: 0x0001A679
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.MoonPedestals);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0001C494 File Offset: 0x0001A694
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.MoonPedestals, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(129, 10);
			itemRecipe.AddIngredient(3, 8);
			itemRecipe.AddIngredient(178, 3);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0001C504 File Offset: 0x0001A704
		public override void UpdateInventory(Player player)
		{
			if (base.Item.favorited)
			{
				player.GetModPlayer<QoLCPlayer>().activeItems.Add(base.Item.type);
				player.GetModPlayer<QoLCPlayer>().bloodMoonPedestal = true;
			}
		}
	}
}
