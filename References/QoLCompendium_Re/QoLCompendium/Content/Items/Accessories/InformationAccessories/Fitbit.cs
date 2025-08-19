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
	// Token: 0x020001DA RID: 474
	public class Fitbit : ModItem
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001F500 File Offset: 0x0001D700
		public override void SetDefaults()
		{
			base.Item.width = 14;
			base.Item.height = 10;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(0, 6, 0, 0));
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001F554 File Offset: 0x0001D754
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<Kettlebell>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<ReinforcedPanel>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<WingTimer>(), 1);
			itemRecipe.AddTile(114);
			itemRecipe.Register();
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001F5C7 File Offset: 0x0001D7C7
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().kettlebell = true;
			player.GetModPlayer<InfoPlayer>().reinforcedPanel = true;
			player.GetModPlayer<InfoPlayer>().wingTimer = true;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001F5C7 File Offset: 0x0001D7C7
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().kettlebell = true;
			player.GetModPlayer<InfoPlayer>().reinforcedPanel = true;
			player.GetModPlayer<InfoPlayer>().wingTimer = true;
		}
	}
}
