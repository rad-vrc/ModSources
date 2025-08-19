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
	// Token: 0x020001E9 RID: 489
	public class VitalDisplay : ModItem
	{
		// Token: 0x06000ACC RID: 2764 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000202CC File Offset: 0x0001E4CC
		public override void SetDefaults()
		{
			base.Item.width = 17;
			base.Item.height = 16;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(0, 6, 0, 0));
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00020320 File Offset: 0x0001E520
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<MetallicClover>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<Regenerator>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<Replenisher>(), 1);
			itemRecipe.AddTile(114);
			itemRecipe.Register();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00020393 File Offset: 0x0001E593
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().metallicClover = true;
			player.GetModPlayer<InfoPlayer>().regenerator = true;
			player.GetModPlayer<InfoPlayer>().replenisher = true;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00020393 File Offset: 0x0001E593
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().metallicClover = true;
			player.GetModPlayer<InfoPlayer>().regenerator = true;
			player.GetModPlayer<InfoPlayer>().replenisher = true;
		}
	}
}
