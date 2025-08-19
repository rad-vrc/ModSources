using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.ModLoader
{
	// Token: 0x0200021F RID: 543
	public sealed class UnicornMountJump : VanillaExtraJump
	{
		// Token: 0x0600283C RID: 10300 RVA: 0x0050A163 File Offset: 0x00508363
		public override float GetDurationMultiplier(Player player)
		{
			return 2f;
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x0050A16C File Offset: 0x0050836C
		public override void OnStarted(Player player, ref bool playSound)
		{
			Vector2 center = player.Center;
			Vector2 vector2;
			vector2..ctor(50f, 20f);
			float num10 = 6.2831855f * Main.rand.NextFloat();
			for (int i = 0; i < 5; i++)
			{
				for (float num11 = 0f; num11 < 14f; num11 += 1f)
				{
					Dust dust = Main.dust[Dust.NewDust(center, 0, 0, Utils.SelectRandom<int>(Main.rand, new int[]
					{
						176,
						177,
						179
					}), 0f, 0f, 0, default(Color), 1f)];
					Vector2 vector3 = Vector2.UnitY.RotatedBy((double)(num11 * 6.2831855f / 14f + num10), default(Vector2));
					vector3 *= 0.2f * (float)i;
					dust.position = center + vector3 * vector2;
					dust.velocity = vector3 + new Vector2(0f, player.gravDir * 4f);
					dust.noGravity = true;
					dust.scale = 1f + Main.rand.NextFloat() * 0.8f;
					dust.fadeIn = Main.rand.NextFloat() * 2f;
					dust.shader = GameShaders.Armor.GetSecondaryShader(player.cMount, player);
				}
			}
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x0050A2D8 File Offset: 0x005084D8
		public override void ShowVisuals(Player player)
		{
			Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, Utils.SelectRandom<int>(Main.rand, new int[]
			{
				176,
				177,
				179
			}), 0f, 0f, 0, default(Color), 1f)];
			dust.velocity = Vector2.Zero;
			dust.noGravity = true;
			dust.scale = 0.5f + Main.rand.NextFloat() * 0.8f;
			dust.fadeIn = 1f + Main.rand.NextFloat() * 2f;
			dust.shader = GameShaders.Armor.GetSecondaryShader(player.cMount, player);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x0050A396 File Offset: 0x00508596
		public override void UpdateHorizontalSpeeds(Player player)
		{
			player.runAcceleration *= 3f;
			player.maxRunSpeed *= 1.5f;
		}
	}
}
