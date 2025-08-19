using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Magnets
{
	// Token: 0x020001AF RID: 431
	public class SpectreMagnet : ModItem
	{
		// Token: 0x06000938 RID: 2360 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Magnets;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001BF11 File Offset: 0x0001A111
		public override void SetDefaults()
		{
			base.Item.width = 13;
			base.Item.height = 13;
			base.Item.maxStack = 1;
			base.Item.SetShopValues(ItemRarityColor.Yellow8, Item.buyPrice(0, 4, 0, 0));
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001B94F File Offset: 0x00019B4F
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Magnets);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001BF50 File Offset: 0x0001A150
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.Magnets, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<SoulMagnet>(), 1);
			itemRecipe.AddIngredient(3261, 10);
			itemRecipe.AddTile(134);
			itemRecipe.Register();
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001BFBA File Offset: 0x0001A1BA
		public override void UpdateInventory(Player player)
		{
			if (base.Item.favorited)
			{
				player.GetModPlayer<QoLCPlayer>().activeItems.Add(base.Item.type);
				player.GetModPlayer<MagnetPlayer>().SpectreMagnet = true;
			}
		}
	}
}
