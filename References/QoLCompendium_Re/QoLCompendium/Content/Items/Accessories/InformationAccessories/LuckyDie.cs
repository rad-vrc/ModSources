using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using QoLCompendium.Core.UI.Other;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Accessories.InformationAccessories
{
	// Token: 0x020001E0 RID: 480
	public class LuckyDie : ModItem
	{
		// Token: 0x06000A84 RID: 2692 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001FB50 File Offset: 0x0001DD50
		public override void SetDefaults()
		{
			base.Item.width = 13;
			base.Item.height = 14;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 3, 0, 0));
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001FBA4 File Offset: 0x0001DDA4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(3066, 12);
			itemRecipe.AddIngredient(1050, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001FC0B File Offset: 0x0001DE0B
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().luckyDie = true;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001FC0B File Offset: 0x0001DE0B
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().luckyDie = true;
		}
	}
}
