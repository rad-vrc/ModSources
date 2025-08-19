using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Fishing
{
	// Token: 0x020001B1 RID: 433
	public class Eightworm : ModItem
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x0001C055 File Offset: 0x0001A255
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Eightworm;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001C070 File Offset: 0x0001A270
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 9;
			base.Item.bait = 100;
			base.Item.consumable = false;
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001C0C6 File Offset: 0x0001A2C6
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Eightworm);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001C0E0 File Offset: 0x0001A2E0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.Eightworm, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(2002, 8);
			itemRecipe.AddIngredient(74, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
