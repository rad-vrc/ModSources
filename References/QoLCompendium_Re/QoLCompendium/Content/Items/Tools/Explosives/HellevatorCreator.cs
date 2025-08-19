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
	// Token: 0x020001BF RID: 447
	public class HellevatorCreator : ModItem
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x0001D74B File Offset: 0x0001B94B
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.AutoStructures;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001DCBC File Offset: 0x0001BEBC
		public override void SetDefaults()
		{
			base.Item.width = 13;
			base.Item.height = 31;
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.useStyle = 1;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.noUseGraphic = true;
			base.Item.noMelee = true;
			base.Item.shoot = ModContent.ProjectileType<HellevatorCreatorProj>();
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001D808 File Offset: 0x0001BA08
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.AutoStructures);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0001DD74 File Offset: 0x0001BF74
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 mouse = Main.MouseWorld;
			Projectile.NewProjectile(player.GetSource_ItemUse(source.Item, null), mouse, Vector2.Zero, type, 0, 0f, player.whoAmI, 0f, 0f, 0f);
			return false;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0001DDC0 File Offset: 0x0001BFC0
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.AutoStructures, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(167, 25);
			itemRecipe.AddIngredient(182, 5);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001DE35 File Offset: 0x0001C035
		public override void HoldItem(Player player)
		{
			this.HandleShadow(player);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001DE40 File Offset: 0x0001C040
		public void HandleShadow(Player player)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<BuildIndicatorProjectile>()] > 400)
			{
				return;
			}
			if (player.direction < 0)
			{
				for (int x = -6; x <= 0; x++)
				{
					for (int y = 0; y <= 100; y++)
					{
						Vector2 mouse = Main.MouseWorld;
						mouse.X += (float)(x * 16);
						mouse.Y += (float)(y * 16);
						Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), mouse + new Vector2(48f, 16f), Vector2.Zero, ModContent.ProjectileType<BuildIndicatorProjectile>(), 0, 0f, player.whoAmI, 0f, 0f, 0f);
					}
				}
				return;
			}
			for (int x2 = 0; x2 <= 6; x2++)
			{
				for (int y2 = 0; y2 <= 100; y2++)
				{
					Vector2 mouse2 = Main.MouseWorld;
					mouse2.X += (float)(x2 * 16);
					mouse2.Y += (float)(y2 * 16);
					Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), mouse2 + new Vector2(-48f, 16f), Vector2.Zero, ModContent.ProjectileType<BuildIndicatorProjectile>(), 0, 0f, player.whoAmI, 0f, 0f, 0f);
				}
			}
		}
	}
}
