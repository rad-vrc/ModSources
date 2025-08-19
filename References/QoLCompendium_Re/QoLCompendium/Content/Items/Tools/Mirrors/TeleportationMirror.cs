using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Mirrors
{
	// Token: 0x020001A6 RID: 422
	public class TeleportationMirror : ModItem
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x0001A84F File Offset: 0x00018A4F
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Mirrors;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001A869 File Offset: 0x00018A69
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(50);
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 2, 0, 0));
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001A88D File Offset: 0x00018A8D
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Mirrors);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001B550 File Offset: 0x00019750
		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (Main.rand.NextBool())
			{
				Dust.NewDustDirect(player.position, player.width, player.height, 15, 0f, 0f, 150, Color.Cyan, 1.1f).velocity *= 0.5f;
			}
			if (player.ItemTimeIsZero)
			{
				player.ApplyItemTime(base.Item, 1f, null);
			}
			if (player.itemTime == player.itemTimeMax / 2)
			{
				for (int i = 0; i < 70; i++)
				{
					Dust.NewDustDirect(player.position, player.width, player.height, 15, 0f, 0f, 150, Color.Cyan, 1.5f).velocity *= 0.5f;
				}
				player.grappling[0] = -1;
				player.grapCount = 0;
				for (int j = 0; j < Main.projectile.Length; j++)
				{
					if (Main.projectile[j].active && Main.projectile[j].owner == player.whoAmI && Main.projectile[j].aiStyle == 7)
					{
						Main.projectile[j].Kill();
					}
				}
				player.TeleportationPotion();
				for (int k = 0; k < 70; k++)
				{
					Dust.NewDustDirect(player.position, player.width, player.height, 15, 0f, 0f, 150, Color.Cyan, 1.5f).velocity *= 0.5f;
				}
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001B6F4 File Offset: 0x000198F4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.Mirrors, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(170, 10);
			itemRecipe.AddRecipeGroup("QoLCompendium:GoldBars", 8);
			itemRecipe.AddIngredient(2351, 3);
			itemRecipe.AddTile(17);
			itemRecipe.Register();
		}
	}
}
