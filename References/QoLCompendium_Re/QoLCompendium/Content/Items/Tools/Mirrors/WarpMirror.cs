using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Mirrors
{
	// Token: 0x020001A7 RID: 423
	public class WarpMirror : ModItem
	{
		// Token: 0x06000903 RID: 2307 RVA: 0x0001A84F File Offset: 0x00018A4F
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Mirrors;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001A869 File Offset: 0x00018A69
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(50);
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 2, 0, 0));
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001A88D File Offset: 0x00018A8D
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Mirrors);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001B768 File Offset: 0x00019968
		public override void UpdateInventory(Player player)
		{
			player.GetModPlayer<QoLCPlayer>().warpMirror = true;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0000404D File Offset: 0x0000224D
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001B778 File Offset: 0x00019978
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.Mirrors, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(170, 10);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 8);
			itemRecipe.AddIngredient(73, 3);
			itemRecipe.AddTile(17);
			itemRecipe.Register();
		}
	}
}
