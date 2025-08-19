using System;
using System.Collections.Generic;
using QoLCompendium.Content.Projectiles.MobileStorages;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.MobileStorages
{
	// Token: 0x020001A1 RID: 417
	public class FlyingSafe : ModItem
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x0001A3BC File Offset: 0x000185BC
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.MobileStorages;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001A6B9 File Offset: 0x000188B9
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(3213);
			base.Item.shoot = ModContent.ProjectileType<FlyingSafeProjectile>();
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(0, 2, 0, 0));
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001A48C File Offset: 0x0001868C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.MobileStorages);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001A6F0 File Offset: 0x000188F0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.MobileStorages, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(346, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
