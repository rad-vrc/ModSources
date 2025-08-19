using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Projectiles.Explosives;
using QoLCompendium.Content.Projectiles.Other;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Explosives
{
	// Token: 0x020001C1 RID: 449
	public class Minibridge : ModItem
	{
		// Token: 0x060009BB RID: 2491 RVA: 0x0001D74B File Offset: 0x0001B94B
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.AutoStructures;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		public override void SetDefaults()
		{
			base.Item.width = 25;
			base.Item.height = 17;
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.useStyle = 1;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.noUseGraphic = true;
			base.Item.noMelee = true;
			base.Item.shoot = ModContent.ProjectileType<MinibridgeProj>();
			base.Item.shootSpeed = 5f;
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001D808 File Offset: 0x0001BA08
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.AutoStructures);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001E1B4 File Offset: 0x0001C3B4
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 mouse = Main.MouseWorld;
			Projectile.NewProjectile(player.GetSource_ItemUse(source.Item, null), mouse, Vector2.Zero, type, 0, 0f, player.whoAmI, 0f, 0f, 0f);
			return false;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0001E200 File Offset: 0x0001C400
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.AutoStructures, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(9, 10);
			itemRecipe.AddIngredient(167, 1);
			itemRecipe.AddIngredient(182, 2);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001E27E File Offset: 0x0001C47E
		public override void HoldItem(Player player)
		{
			this.HandleShadow(player);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001E288 File Offset: 0x0001C488
		public void HandleShadow(Player player)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<BuildIndicatorProjectile>()] > 200)
			{
				return;
			}
			for (int i = -100; i <= 100; i++)
			{
				Vector2 mouse = Main.MouseWorld;
				mouse.X += (float)(i * 16);
				Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), mouse, Vector2.Zero, ModContent.ProjectileType<BuildIndicatorProjectile>(), 0, 0f, player.whoAmI, 0f, 0f, 0f);
			}
		}
	}
}
