using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Magnets
{
	// Token: 0x020001AE RID: 430
	public class SoulMagnet : ModItem
	{
		// Token: 0x06000931 RID: 2353 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Magnets;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001BE29 File Offset: 0x0001A029
		public override void SetDefaults()
		{
			base.Item.width = 13;
			base.Item.height = 13;
			base.Item.maxStack = 1;
			base.Item.SetShopValues(ItemRarityColor.Pink5, Item.buyPrice(0, 3, 0, 0));
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001B94F File Offset: 0x00019B4F
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Magnets);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0001BE68 File Offset: 0x0001A068
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.Magnets, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<HellstoneMagnet>(), 1);
			itemRecipe.AddIngredient(521, 5);
			itemRecipe.AddIngredient(520, 5);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001BEDB File Offset: 0x0001A0DB
		public override void UpdateInventory(Player player)
		{
			if (base.Item.favorited)
			{
				player.GetModPlayer<QoLCPlayer>().activeItems.Add(base.Item.type);
				player.GetModPlayer<MagnetPlayer>().SoulMagnet = true;
			}
		}
	}
}
