using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using QoLCompendium.Core.UI.Other;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Accessories.Fishing
{
	// Token: 0x020001EC RID: 492
	public class AnglersDream : ModItem
	{
		// Token: 0x06000AE3 RID: 2787 RVA: 0x00020485 File Offset: 0x0001E685
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.FishingAccessories;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002051C File Offset: 0x0001E71C
		public override void SetDefaults()
		{
			base.Item.width = 14;
			base.Item.height = 17;
			base.Item.maxStack = 1;
			base.Item.SetShopValues(ItemRarityColor.Yellow8, Item.buyPrice(0, 8, 0, 0));
			base.Item.accessory = true;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00020570 File Offset: 0x0001E770
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.accFishingLine = true;
			player.accTackleBox = true;
			player.fishingSkill += 10;
			player.accLavaFishing = true;
			player.accFishingBobber = true;
			player.sonarPotion = true;
			player.GetModPlayer<InfoPlayer>().anglerRadar = true;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002050C File Offset: 0x0001E70C
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().anglerRadar = true;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000204F4 File Offset: 0x0001E6F4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.FishingAccessories);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000205B0 File Offset: 0x0001E7B0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.FishingAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(5064, 1);
			itemRecipe.AddRecipeGroup("QoLCompendium:FishingBobbers", 1);
			itemRecipe.AddIngredient(ModContent.ItemType<SonarDevice>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<AnglerRadar>(), 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
