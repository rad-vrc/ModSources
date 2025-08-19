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
	// Token: 0x020001BE RID: 446
	public class Hellbridger : ModItem
	{
		// Token: 0x060009A2 RID: 2466 RVA: 0x0001D74B File Offset: 0x0001B94B
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.AutoStructures;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001D9E8 File Offset: 0x0001BBE8
		public override void SetDefaults()
		{
			base.Item.width = 37;
			base.Item.height = 19;
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.useStyle = 1;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.noUseGraphic = true;
			base.Item.noMelee = true;
			base.Item.shoot = ModContent.ProjectileType<HellbridgerProj>();
			base.Item.shootSpeed = 5f;
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001D808 File Offset: 0x0001BA08
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.AutoStructures);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00002430 File Offset: 0x00000630
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001DAB0 File Offset: 0x0001BCB0
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 mouse = Main.MouseWorld;
			if (player.altFunctionUse == 2)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(source.Item, null), mouse, Vector2.Zero, ModContent.ProjectileType<HellbridgerSingleProj>(), 0, 0f, player.whoAmI, 0f, 0f, 0f);
			}
			else
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(source.Item, null), mouse, Vector2.Zero, type, 0, 0f, player.whoAmI, 0f, 0f, 0f);
			}
			return false;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0001DB40 File Offset: 0x0001BD40
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.AutoStructures, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(173, 25);
			itemRecipe.AddIngredient(167, 25);
			itemRecipe.AddIngredient(182, 5);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001DBC3 File Offset: 0x0001BDC3
		public override void HoldItem(Player player)
		{
			this.HandleShadow(player);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001DBCC File Offset: 0x0001BDCC
		public void HandleShadow(Player player)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<BuildIndicatorProjectile>()] > 400)
			{
				return;
			}
			for (int i = -100; i <= 100; i++)
			{
				Vector2 mouse = Main.MouseWorld;
				mouse.X += (float)(i * 16);
				Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), mouse, Vector2.Zero, ModContent.ProjectileType<BuildIndicatorProjectile>(), 0, 0f, player.whoAmI, 0f, 0f, 0f);
			}
			for (int j = -100; j <= 100; j++)
			{
				Vector2 mouse2 = Main.MouseWorld;
				mouse2.X += (float)(j * 16);
				Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), mouse2 - new Vector2(0f, 320f), Vector2.Zero, ModContent.ProjectileType<BuildIndicatorProjectile>(), 0, 0f, player.whoAmI, 0f, 0f, 0f);
			}
		}
	}
}
