using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Magnets
{
	// Token: 0x020001AB RID: 427
	public class Magnet : ModItem
	{
		// Token: 0x06000923 RID: 2339 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Magnets;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001BAE4 File Offset: 0x00019CE4
		public override void SetDefaults()
		{
			base.Item.width = 13;
			base.Item.height = 13;
			base.Item.maxStack = 1;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 1, 0, 0));
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001B94F File Offset: 0x00019B4F
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Magnets);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001BB24 File Offset: 0x00019D24
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.Magnets, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 12);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001BB7E File Offset: 0x00019D7E
		public override void UpdateInventory(Player player)
		{
			if (base.Item.favorited)
			{
				player.GetModPlayer<QoLCPlayer>().activeItems.Add(base.Item.type);
				player.GetModPlayer<MagnetPlayer>().BaseMagnet = true;
			}
		}
	}
}
