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
	// Token: 0x020001A0 RID: 416
	public class EtherianConstruct : ModItem
	{
		// Token: 0x060008CE RID: 2254 RVA: 0x0001A3BC File Offset: 0x000185BC
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.MobileStorages;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001A628 File Offset: 0x00018828
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(3213);
			base.Item.shoot = ModContent.ProjectileType<EtherianConstructProjectile>();
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(0, 2, 0, 0));
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001A48C File Offset: 0x0001868C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.MobileStorages);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001A660 File Offset: 0x00018860
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.MobileStorages, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(3813, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
