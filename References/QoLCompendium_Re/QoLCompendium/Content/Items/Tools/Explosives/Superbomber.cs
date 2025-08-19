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
	// Token: 0x020001C3 RID: 451
	public class Superbomber : ModItem
	{
		// Token: 0x060009CE RID: 2510 RVA: 0x0001D74B File Offset: 0x0001B94B
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.AutoStructures;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001E5DC File Offset: 0x0001C7DC
		public override void SetDefaults()
		{
			base.Item.width = 29;
			base.Item.height = 29;
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.useStyle = 1;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.noUseGraphic = true;
			base.Item.noMelee = true;
			base.Item.shoot = ModContent.ProjectileType<SuperbomberProj>();
			base.Item.shootSpeed = 5f;
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0001D808 File Offset: 0x0001BA08
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.AutoStructures);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0001E6A4 File Offset: 0x0001C8A4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.AutoStructures, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(166, 50);
			itemRecipe.AddIngredient(182, 5);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
