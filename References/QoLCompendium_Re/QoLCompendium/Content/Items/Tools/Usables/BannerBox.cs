using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x02000179 RID: 377
	public class BannerBox : ModItem
	{
		// Token: 0x0600078E RID: 1934 RVA: 0x0001497F File Offset: 0x00012B7F
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.BannerBox;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00014999 File Offset: 0x00012B99
		public override void SetDefaults()
		{
			base.Item.width = 19;
			base.Item.height = 11;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 0, 20, 0));
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000149CB File Offset: 0x00012BCB
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.BannerBox);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000149E4 File Offset: 0x00012BE4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.BannerBox, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(9, 12);
			itemRecipe.AddIngredient(225, 2);
			itemRecipe.AddTile(86);
			itemRecipe.Register();
		}
	}
}
