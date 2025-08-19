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
	// Token: 0x020001DD RID: 477
	public class HeartbeatSensor : ModItem
	{
		// Token: 0x06000A6C RID: 2668 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0001F78C File Offset: 0x0001D98C
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 12;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(0, 6, 0, 0));
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001F7E0 File Offset: 0x0001D9E0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<BattalionLog>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<HeadCounter>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<TrackingDevice>(), 1);
			itemRecipe.AddTile(114);
			itemRecipe.Register();
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001F853 File Offset: 0x0001DA53
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().battalionLog = true;
			player.GetModPlayer<InfoPlayer>().headCounter = true;
			player.GetModPlayer<InfoPlayer>().trackingDevice = true;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001F853 File Offset: 0x0001DA53
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().battalionLog = true;
			player.GetModPlayer<InfoPlayer>().headCounter = true;
			player.GetModPlayer<InfoPlayer>().trackingDevice = true;
		}
	}
}
