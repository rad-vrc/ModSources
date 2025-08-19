using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Weapons.Ammo
{
	// Token: 0x0200003E RID: 62
	public abstract class BaseAmmo : ModItem
	{
		// Token: 0x06000120 RID: 288 RVA: 0x000086BD File Offset: 0x000068BD
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.EndlessAmmo;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000121 RID: 289
		public abstract int AmmunitionItem { get; }

		// Token: 0x06000122 RID: 290 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000086E8 File Offset: 0x000068E8
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(this.AmmunitionItem);
			base.Item.width = 26;
			base.Item.height = 26;
			base.Item.consumable = false;
			base.Item.maxStack = 1;
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.sellPrice(0, 1, 0, 0));
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000874D File Offset: 0x0000694D
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.EndlessAmmo);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00008768 File Offset: 0x00006968
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.EndlessAmmo, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(this.AmmunitionItem, 3996);
			itemRecipe.AddTile(220);
			itemRecipe.Register();
		}
	}
}
