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
	// Token: 0x020001DE RID: 478
	public class IAH : ModItem
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x0001F326 File Offset: 0x0001D526
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.InformationAccessories;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001F87C File Offset: 0x0001DA7C
		public override void SetDefaults()
		{
			base.Item.width = 17;
			base.Item.height = 14;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Pink5, Item.buyPrice(0, 9, 0, 0));
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001F394 File Offset: 0x0001D594
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.InformationAccessories);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001F8D4 File Offset: 0x0001DAD4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.InformationAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<Fitbit>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<HeartbeatSensor>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<ToleranceDetector>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<VitalDisplay>(), 1);
			itemRecipe.AddTile(114);
			itemRecipe.Register();
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001F954 File Offset: 0x0001DB54
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().battalionLog = true;
			player.GetModPlayer<InfoPlayer>().harmInducer = true;
			player.GetModPlayer<InfoPlayer>().headCounter = true;
			player.GetModPlayer<InfoPlayer>().kettlebell = true;
			player.GetModPlayer<InfoPlayer>().luckyDie = true;
			player.GetModPlayer<InfoPlayer>().metallicClover = true;
			player.GetModPlayer<InfoPlayer>().plateCracker = true;
			player.GetModPlayer<InfoPlayer>().regenerator = true;
			player.GetModPlayer<InfoPlayer>().reinforcedPanel = true;
			player.GetModPlayer<InfoPlayer>().replenisher = true;
			player.GetModPlayer<InfoPlayer>().trackingDevice = true;
			player.GetModPlayer<InfoPlayer>().wingTimer = true;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001F9F4 File Offset: 0x0001DBF4
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().battalionLog = true;
			player.GetModPlayer<InfoPlayer>().harmInducer = true;
			player.GetModPlayer<InfoPlayer>().headCounter = true;
			player.GetModPlayer<InfoPlayer>().kettlebell = true;
			player.GetModPlayer<InfoPlayer>().luckyDie = true;
			player.GetModPlayer<InfoPlayer>().metallicClover = true;
			player.GetModPlayer<InfoPlayer>().plateCracker = true;
			player.GetModPlayer<InfoPlayer>().regenerator = true;
			player.GetModPlayer<InfoPlayer>().reinforcedPanel = true;
			player.GetModPlayer<InfoPlayer>().replenisher = true;
			player.GetModPlayer<InfoPlayer>().trackingDevice = true;
			player.GetModPlayer<InfoPlayer>().wingTimer = true;
		}
	}
}
