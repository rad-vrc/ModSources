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
	// Token: 0x020001E7 RID: 487
	public class ToleranceDetector : ModItem
	{
		// Token: 0x06000ABC RID: 2748 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00020110 File Offset: 0x0001E310
		public override void SetDefaults()
		{
			base.Item.width = 14;
			base.Item.height = 10;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(0, 6, 0, 0));
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00020164 File Offset: 0x0001E364
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<HarmInducer>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<LuckyDie>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<PlateCracker>(), 1);
			itemRecipe.AddTile(114);
			itemRecipe.Register();
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000201D7 File Offset: 0x0001E3D7
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().harmInducer = true;
			player.GetModPlayer<InfoPlayer>().luckyDie = true;
			player.GetModPlayer<InfoPlayer>().plateCracker = true;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000201D7 File Offset: 0x0001E3D7
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().harmInducer = true;
			player.GetModPlayer<InfoPlayer>().luckyDie = true;
			player.GetModPlayer<InfoPlayer>().plateCracker = true;
		}
	}
}
