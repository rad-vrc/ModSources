using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Magnets
{
	// Token: 0x020001AA RID: 426
	public class LunarMagnet : ModItem
	{
		// Token: 0x0600091C RID: 2332 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Magnets;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001BA05 File Offset: 0x00019C05
		public override void SetDefaults()
		{
			base.Item.width = 13;
			base.Item.height = 13;
			base.Item.maxStack = 1;
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 8, 0, 0));
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001B94F File Offset: 0x00019B4F
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Magnets);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001BA44 File Offset: 0x00019C44
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.Magnets, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<SpectreMagnet>(), 1);
			itemRecipe.AddIngredient(3467, 10);
			itemRecipe.AddTile(412);
			itemRecipe.Register();
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001BAAE File Offset: 0x00019CAE
		public override void UpdateInventory(Player player)
		{
			if (base.Item.favorited)
			{
				player.GetModPlayer<QoLCPlayer>().activeItems.Add(base.Item.type);
				player.GetModPlayer<MagnetPlayer>().LunarMagnet = true;
			}
		}
	}
}
