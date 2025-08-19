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
	// Token: 0x020001A3 RID: 419
	public class CursedMirror : ModItem
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x0001A84F File Offset: 0x00018A4F
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Mirrors;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001A869 File Offset: 0x00018A69
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(50);
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 2, 0, 0));
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001A88D File Offset: 0x00018A8D
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Mirrors);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001A8A8 File Offset: 0x00018AA8
		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (Main.rand.NextBool())
			{
				Dust.NewDust(player.position, player.width, player.height, 15, 0f, 0f, 150, Color.Yellow, 1.1f);
			}
			if (player.itemTime == 0)
			{
				player.ApplyItemTime(base.Item, 1f, null);
				return;
			}
			if (player.itemTime == player.itemTimeMax / 2)
			{
				for (int i = 0; i < 70; i++)
				{
					Dust.NewDust(player.position, player.width, player.height, 15, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, default(Color), 1.5f);
				}
				player.grappling[0] = -1;
				player.grapCount = 0;
				for (int j = 0; j < 1000; j++)
				{
					if (Main.projectile[j].active && Main.projectile[j].owner == player.whoAmI && Main.projectile[j].aiStyle == 7)
					{
						Main.projectile[j].Kill();
					}
				}
				if (player.lastDeathPostion.X != 0f && player.lastDeathPostion.Y != 0f)
				{
					Vector2 vector;
					vector..ctor(player.lastDeathPostion.X - 16f, player.lastDeathPostion.Y - 24f);
					player.Teleport(vector, 0, 0);
				}
				else if (player == Main.player[Main.myPlayer])
				{
					Main.NewText("No sign of recent death appears in the mirror", byte.MaxValue, byte.MaxValue, byte.MaxValue);
				}
				for (int k = 0; k < 70; k++)
				{
					Dust.NewDust(player.position, player.width, player.height, 15, 0f, 0f, 150, default(Color), 1.5f);
				}
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001AAAC File Offset: 0x00018CAC
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.Mirrors, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(170, 10);
			itemRecipe.AddRecipeGroup("QoLCompendium:GoldBars", 8);
			itemRecipe.AddRecipeGroup("QoLCompendium:AnyTombstone", 3);
			itemRecipe.AddTile(17);
			itemRecipe.Register();
		}
	}
}
