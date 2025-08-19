using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Dedicated
{
	// Token: 0x020001F6 RID: 502
	public class RangedAbsolution : ModItem
	{
		// Token: 0x06000B27 RID: 2855 RVA: 0x00020AFF File Offset: 0x0001ECFF
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.DedicatedItems;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002115C File Offset: 0x0001F35C
		public override void SetDefaults()
		{
			base.Item.width = 26;
			base.Item.height = 26;
			base.Item.useTime = 12;
			base.Item.useAnimation = 12;
			base.Item.useStyle = 1;
			base.Item.UseSound = new SoundStyle?(SoundID.Item11);
			base.Item.autoReuse = true;
			base.Item.damage = 26;
			base.Item.DamageType = DamageClass.Ranged;
			base.Item.knockBack = 7f;
			base.Item.shoot = 10;
			base.Item.useAmmo = AmmoID.Bullet;
			base.Item.shootSpeed = 16f;
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 4, 0, 0));
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00002430 File Offset: 0x00000630
		public override bool RangedPrefix()
		{
			return true;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002123A File Offset: 0x0001F43A
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			this.shootType = type;
			return false;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00021248 File Offset: 0x0001F448
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.1) || player.itemAnimation == (int)((double)player.itemAnimationMax * 0.3) || player.itemAnimation == (int)((double)player.itemAnimationMax * 0.5) || player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7) || player.itemAnimation == (int)((double)player.itemAnimationMax * 0.9))
			{
				float speedY = 0f;
				float speedX = 0f;
				float posY = 0f;
				float posX = 0f;
				if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.9))
				{
					speedY = -7f;
					if (player.direction == -1)
					{
						posX -= 8f;
					}
				}
				if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7))
				{
					speedY = -6f;
					speedX = 2f;
					if (player.direction == -1)
					{
						posX -= 6f;
					}
				}
				if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.5))
				{
					speedY = -4f;
					speedX = 4f;
				}
				if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.3))
				{
					speedY = -2f;
					speedX = 6f;
				}
				if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.1))
				{
					speedX = 7f;
				}
				if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7))
				{
					posX = 26f;
				}
				if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.3))
				{
					posX -= 4f;
					posY -= 20f;
				}
				if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.1))
				{
					posY += 6f;
				}
				speedY *= 1.5f;
				speedX *= 1.5f;
				float direction = posX * (float)player.direction;
				float yDirection = posY * player.gravDir;
				Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), (float)(hitbox.X + hitbox.Width / 2) + direction, (float)(hitbox.Y + hitbox.Height / 2) + yDirection, (float)player.direction * speedX, speedY * player.gravDir, this.shootType, base.Item.damage, 0f, player.whoAmI, 0f, 0f, 0f);
			}
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000214CC File Offset: 0x0001F6CC
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine dedicated = new TooltipLine(base.Mod, "Dedicated", Language.GetTextValue("Mods.QoLCompendium.DedicatedTooltips.Nobisyu"))
			{
				OverrideColor = new Color?(Common.ColorSwap(Color.LightSeaGreen, Color.Aquamarine, 3f))
			};
			tooltips.Add(dedicated);
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.DedicatedItems);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00021530 File Offset: 0x0001F730
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.DedicatedItems, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(1257, 10);
			itemRecipe.AddIngredient(173, 5);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x0400004B RID: 75
		public int shootType;
	}
}
