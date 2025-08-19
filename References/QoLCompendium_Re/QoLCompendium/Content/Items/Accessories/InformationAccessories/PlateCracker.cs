using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using QoLCompendium.Core.UI.Other;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Accessories.InformationAccessories
{
	// Token: 0x020001E2 RID: 482
	public class PlateCracker : ModItem
	{
		// Token: 0x06000A94 RID: 2708 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001FCF4 File Offset: 0x0001DEF4
		public override void SetDefaults()
		{
			base.Item.width = 11;
			base.Item.height = 14;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 3, 0, 0));
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001FD48 File Offset: 0x0001DF48
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(178, 4);
			itemRecipe.AddIngredient(323, 4);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().plateCracker = true;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().plateCracker = true;
		}
	}
}
