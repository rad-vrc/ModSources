using System;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Items.Tools.Fishing;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Fishing
{
	// Token: 0x0200002A RID: 42
	public class LegendaryBobber : ModProjectile
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00004E20 File Offset: 0x00003020
		public override void SetDefaults()
		{
			base.Projectile.width = 14;
			base.Projectile.height = 14;
			base.Projectile.aiStyle = 61;
			base.Projectile.bobber = true;
			base.Projectile.penetrate = -1;
			base.DrawOriginOffsetY = -8;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004E74 File Offset: 0x00003074
		public override bool PreDrawExtras()
		{
			int num = 45;
			float num2 = 29f;
			Player val = Main.player[base.Projectile.owner];
			if (!base.Projectile.bobber || val.inventory[val.selectedItem].holdStyle <= 0)
			{
				return false;
			}
			Vector2 mountedCenter = val.MountedCenter;
			mountedCenter.Y += val.gfxOffY;
			int type = val.inventory[val.selectedItem].type;
			float gravDir = val.gravDir;
			if (type == ModContent.ItemType<LegendaryCatcher>())
			{
				mountedCenter.X += (float)(num * val.direction);
				if (val.direction < 0)
				{
					mountedCenter.X -= 13f;
				}
				mountedCenter.Y -= num2 * gravDir;
			}
			if (gravDir == -1f)
			{
				mountedCenter.Y -= 12f;
			}
			mountedCenter = val.RotatedRelativePoint(mountedCenter + new Vector2(8f), true, true) - new Vector2(8f);
			Vector2 val2 = base.Projectile.Center - mountedCenter;
			bool flag = true;
			if (val2.X == 0f && val2.Y == 0f)
			{
				return false;
			}
			float num3 = val2.Length();
			num3 = 12f / num3;
			val2 *= num3;
			mountedCenter -= val2;
			val2 = base.Projectile.Center - mountedCenter;
			while (flag)
			{
				float num4 = 12f;
				float num5 = val2.Length();
				if (float.IsNaN(num5) || float.IsNaN(num5))
				{
					break;
				}
				if (num5 < 20f)
				{
					num4 = num5 - 8f;
					flag = false;
				}
				val2 *= 12f / num5;
				mountedCenter += val2;
				val2.X = base.Projectile.position.X + (float)base.Projectile.width * 0.5f - mountedCenter.X;
				val2.Y = base.Projectile.position.Y + (float)base.Projectile.height * 0.1f - mountedCenter.Y;
				if (num5 > 12f)
				{
					float num6 = 0.3f;
					float num7 = Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y);
					if (num7 > 16f)
					{
						num7 = 16f;
					}
					num7 = 1f - num7 / 16f;
					num6 *= num7;
					num7 = num5 / 80f;
					if (num7 > 1f)
					{
						num7 = 1f;
					}
					num6 *= num7;
					if (num6 < 0f)
					{
						num6 = 0f;
					}
					num7 = 1f - base.Projectile.localAI[0] / 100f;
					num6 *= num7;
					if (val2.Y > 0f)
					{
						val2.Y *= 1f + num6;
						val2.X *= 1f - num6;
					}
					else
					{
						num7 = Math.Abs(base.Projectile.velocity.X) / 3f;
						if (num7 > 1f)
						{
							num7 = 1f;
						}
						num7 -= 0.5f;
						num6 *= num7;
						if (num6 > 0f)
						{
							num6 *= 2f;
						}
						val2.Y *= 1f + num6;
						val2.X *= 1f - num6;
					}
				}
				float num8 = val2.ToRotation() - 1.5707964f;
				Main.EntitySpriteDraw(TextureAssets.FishingLine.Value, new Vector2(mountedCenter.X - Main.screenPosition.X + (float)TextureAssets.FishingLine.Width() * 0.5f, mountedCenter.Y - Main.screenPosition.Y + (float)TextureAssets.FishingLine.Height() * 0.5f), new Rectangle?(new Rectangle(0, 0, TextureAssets.FishingLine.Width(), (int)num4)), Color.White, num8, new Vector2((float)TextureAssets.FishingLine.Width() * 0.5f, 0f), 1f, 0, 0f);
			}
			return false;
		}
	}
}
