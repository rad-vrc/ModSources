using System;
using System.Collections.Generic;
using QoLCompendium.Core.Changes.TooltipChanges;
using QoLCompendium.Core.UI.Other;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Accessories.Fishing
{
	// Token: 0x020001EB RID: 491
	public class AnglerRadar : ModItem
	{
		// Token: 0x06000ADC RID: 2780 RVA: 0x00020485 File Offset: 0x0001E685
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.FishingAccessories;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000204A0 File Offset: 0x0001E6A0
		public override void SetDefaults()
		{
			base.Item.width = 12;
			base.Item.height = 15;
			base.Item.maxStack = 1;
			base.Item.accessory = true;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 3, 0, 0));
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000204F4 File Offset: 0x0001E6F4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.FishingAccessories);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002050C File Offset: 0x0001E70C
		public override void UpdateInfoAccessory(Player player)
		{
			player.GetModPlayer<InfoPlayer>().anglerRadar = true;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002050C File Offset: 0x0001E70C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<InfoPlayer>().anglerRadar = true;
		}
	}
}
