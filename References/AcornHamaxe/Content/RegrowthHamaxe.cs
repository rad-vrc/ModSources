using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AcornHamaxe.Content
{
	// Token: 0x02000003 RID: 3
	public class RegrowthHamaxe : ModItem
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(5295);
			base.Item.hammer = 100;
			base.Item.width = 58;
			base.Item.height = 62;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002094 File Offset: 0x00000294
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (TooltipLine tooltip in tooltips)
			{
				if (tooltip.Name == "Placeable")
				{
					tooltip.Hide();
				}
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F4 File Offset: 0x000002F4
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			int poop = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 3, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 0, default(Color), 1.2f);
			Main.dust[poop].noGravity = true;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000216F File Offset: 0x0000036F
		public override void Load()
		{
			On_Player.ItemCheck_OwnerOnlyCode += new On_Player.hook_ItemCheck_OwnerOnlyCode(this.VanillaItemCheck);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002184 File Offset: 0x00000384
		private void VanillaItemCheck(On_Player.orig_ItemCheck_OwnerOnlyCode orig, Player self, ref Player.ItemCheckContext context, Item sItem, int weaponDamage, Rectangle heldItemFrame)
		{
			if (sItem.type == ModContent.ItemType<RegrowthHamaxe>())
			{
				sItem.type = 5295;
				orig.Invoke(self, ref context, sItem, weaponDamage, heldItemFrame);
				sItem.type = ModContent.ItemType<RegrowthHamaxe>();
				return;
			}
			orig.Invoke(self, ref context, sItem, weaponDamage, heldItemFrame);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021D4 File Offset: 0x000003D4
		public override void AddRecipes()
		{
			base.CreateRecipe(1).AddIngredient(5295, 1).AddIngredient(3517, 1).AddTile(16).Register();
			base.CreateRecipe(1).AddIngredient(5295, 1).AddIngredient(3481, 1).AddTile(16).Register();
		}
	}
}
