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
	// Token: 0x020001BC RID: 444
	public class AsphaltAutohighway : ModItem
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x0001D425 File Offset: 0x0001B625
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || (QoLCompendium.itemConfig.AutoStructures && QoLCompendium.itemConfig.AsphaltPlatform);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001D450 File Offset: 0x0001B650
		public override void SetDefaults()
		{
			base.Item.width = 37;
			base.Item.height = 26;
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.useStyle = 1;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.noUseGraphic = true;
			base.Item.noMelee = true;
			base.Item.shoot = ModContent.ProjectileType<AsphaltAutohighwayProj>();
			base.Item.shootSpeed = 5f;
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001D517 File Offset: 0x0001B717
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.AutoStructures && QoLCompendium.itemConfig.AsphaltPlatform);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00002430 File Offset: 0x00000630
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001D540 File Offset: 0x0001B740
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 mouse = Main.MouseWorld;
			if (player.altFunctionUse == 2)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(source.Item, null), mouse, Vector2.Zero, ModContent.ProjectileType<AsphaltAutohighwaySingleProj>(), 0, 0f, player.whoAmI, 0f, 0f, 0f);
			}
			else
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(source.Item, null), mouse, Vector2.Zero, type, 0, 0f, player.whoAmI, 0f, 0f, 0f);
			}
			return false;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001D5D0 File Offset: 0x0001B7D0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.AutoStructures && QoLCompendium.itemConfig.AsphaltPlatform, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(775, 25);
			itemRecipe.AddIngredient(167, 25);
			itemRecipe.AddIngredient(182, 5);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001D653 File Offset: 0x0001B853
		public override void HoldItem(Player player)
		{
			this.HandleShadow(player);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001D65C File Offset: 0x0001B85C
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
				Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), mouse2 - new Vector2(0f, 480f), Vector2.Zero, ModContent.ProjectileType<BuildIndicatorProjectile>(), 0, 0f, player.whoAmI, 0f, 0f, 0f);
			}
		}
	}
}
