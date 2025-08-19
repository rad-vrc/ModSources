using System;
using System.Collections.Generic;
using QoLCompendium.Content.Projectiles.Explosives;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Explosives
{
	// Token: 0x020001C0 RID: 448
	public class Lakemaker : ModItem
	{
		// Token: 0x060009B5 RID: 2485 RVA: 0x0001D74B File Offset: 0x0001B94B
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.AutoStructures;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001DFA0 File Offset: 0x0001C1A0
		public override void SetDefaults()
		{
			base.Item.width = 19;
			base.Item.height = 31;
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.useStyle = 1;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.noUseGraphic = true;
			base.Item.noMelee = true;
			base.Item.shoot = ModContent.ProjectileType<LakemakerProj>();
			base.Item.shootSpeed = 5f;
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001D808 File Offset: 0x0001BA08
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.AutoStructures);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0001E068 File Offset: 0x0001C268
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.AutoStructures, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(206, 10);
			itemRecipe.AddIngredient(167, 25);
			itemRecipe.AddIngredient(182, 5);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
