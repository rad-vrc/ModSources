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
	// Token: 0x02000189 RID: 393
	public class WatchingEye : ModItem
	{
		// Token: 0x06000812 RID: 2066 RVA: 0x000174AB File Offset: 0x000156AB
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.WatchingEye;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000174C8 File Offset: 0x000156C8
		public override void SetDefaults()
		{
			base.Item.width = 9;
			base.Item.height = 14;
			base.Item.useStyle = 4;
			base.Item.UseSound = new SoundStyle?(SoundID.Item4);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 0, 80, 0));
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00017540 File Offset: 0x00015740
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.WatchingEye);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00017558 File Offset: 0x00015758
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.WatchingEye, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(38, 4);
			itemRecipe.AddIngredient(179, 2);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000175BC File Offset: 0x000157BC
		public override bool? UseItem(Player player)
		{
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					if (WorldGen.InWorld(i, j, 0))
					{
						Main.Map.Update(i, j, byte.MaxValue);
					}
				}
			}
			Main.refreshMap = true;
			return new bool?(true);
		}
	}
}
