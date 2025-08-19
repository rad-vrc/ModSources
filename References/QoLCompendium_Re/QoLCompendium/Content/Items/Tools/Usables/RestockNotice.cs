using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x02000182 RID: 386
	public class RestockNotice : ModItem
	{
		// Token: 0x060007DC RID: 2012 RVA: 0x00015CCA File Offset: 0x00013ECA
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.RestockNotice;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00015CE4 File Offset: 0x00013EE4
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 15;
			base.Item.useStyle = 4;
			base.Item.UseSound = new SoundStyle?(SoundID.Item4);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 0, 50, 0));
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00015D5C File Offset: 0x00013F5C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.RestockNotice);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00015D74 File Offset: 0x00013F74
		public override bool? UseItem(Player player)
		{
			if (Main.netMode == 0)
			{
				Chest.SetupTravelShop();
			}
			if (Main.netMode == 1)
			{
				Chest.SetupTravelShop();
				NetMessage.SendTravelShop(Main.myPlayer);
			}
			if (Main.netMode == 2)
			{
				Chest.SetupTravelShop();
				NetMessage.SendTravelShop(Main.myPlayer);
			}
			return new bool?(true);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00015DC4 File Offset: 0x00013FC4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.RestockNotice, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(225, 12);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
