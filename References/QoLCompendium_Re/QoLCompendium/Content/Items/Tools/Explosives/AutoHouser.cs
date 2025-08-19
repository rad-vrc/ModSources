using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Projectiles.Other;
using QoLCompendium.Content.Tiles.AutoStructures;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Explosives
{
	// Token: 0x020001BD RID: 445
	public class AutoHouser : ModItem
	{
		// Token: 0x0600099A RID: 2458 RVA: 0x0001D74B File Offset: 0x0001B94B
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.AutoStructures;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001D768 File Offset: 0x0001B968
		public override void SetDefaults()
		{
			base.Item.width = 17;
			base.Item.height = 13;
			base.Item.maxStack = 1;
			base.Item.useStyle = 1;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.consumable = false;
			base.Item.createTile = ModContent.TileType<AutoHouserTile>();
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 0, 50, 0));
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001D808 File Offset: 0x0001BA08
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.AutoStructures);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001D820 File Offset: 0x0001BA20
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.AutoStructures, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(9, 100);
			itemRecipe.AddIngredient(8, 1);
			itemRecipe.AddTile(18);
			itemRecipe.Register();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001D880 File Offset: 0x0001BA80
		public override void HoldItem(Player player)
		{
			this.HandleShadow(player);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001D88C File Offset: 0x0001BA8C
		public void HandleShadow(Player player)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<BuildIndicatorProjectile>()] > 50)
			{
				return;
			}
			if (player.direction < 0)
			{
				for (int x = -9; x <= 0; x++)
				{
					for (int y = -5; y <= 0; y++)
					{
						Vector2 mouse = Main.MouseWorld;
						mouse.X += (float)(x * 16);
						mouse.Y += (float)(y * 16);
						Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), mouse + new Vector2(0f, 16f), Vector2.Zero, ModContent.ProjectileType<BuildIndicatorProjectile>(), 0, 0f, player.whoAmI, 0f, 0f, 0f);
					}
				}
				return;
			}
			for (int x2 = 0; x2 <= 9; x2++)
			{
				for (int y2 = -5; y2 <= 0; y2++)
				{
					Vector2 mouse2 = Main.MouseWorld;
					mouse2.X += (float)(x2 * 16);
					mouse2.Y += (float)(y2 * 16);
					Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), mouse2 + new Vector2(0f, 16f), Vector2.Zero, ModContent.ProjectileType<BuildIndicatorProjectile>(), 0, 0f, player.whoAmI, 0f, 0f, 0f);
				}
			}
		}
	}
}
