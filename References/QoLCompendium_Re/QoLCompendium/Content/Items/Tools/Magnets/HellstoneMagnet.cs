using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Magnets
{
	// Token: 0x020001A9 RID: 425
	public class HellstoneMagnet : ModItem
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Magnets;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001B912 File Offset: 0x00019B12
		public override void SetDefaults()
		{
			base.Item.width = 13;
			base.Item.height = 13;
			base.Item.maxStack = 1;
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(0, 2, 0, 0));
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001B94F File Offset: 0x00019B4F
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Magnets);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001B968 File Offset: 0x00019B68
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.Magnets, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<Magnet>(), 1);
			itemRecipe.AddIngredient(175, 10);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001B9CF File Offset: 0x00019BCF
		public override void UpdateInventory(Player player)
		{
			if (base.Item.favorited)
			{
				player.GetModPlayer<QoLCPlayer>().activeItems.Add(base.Item.type);
				player.GetModPlayer<MagnetPlayer>().HellstoneMagnet = true;
			}
		}
	}
}
