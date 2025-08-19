using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.ModLoader
{
	// Token: 0x0200021D RID: 541
	public sealed class BasiliskMountJump : VanillaExtraJump
	{
		// Token: 0x06002834 RID: 10292 RVA: 0x00509C4F File Offset: 0x00507E4F
		public override float GetDurationMultiplier(Player player)
		{
			return 0.75f;
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x00509C58 File Offset: 0x00507E58
		public override void OnStarted(Player player, ref bool playSound)
		{
			Vector2 center2 = player.Center;
			Vector2 vector4;
			vector4..ctor(50f, 20f);
			float num12 = 6.2831855f * Main.rand.NextFloat();
			for (int i = 0; i < 5; i++)
			{
				for (float num13 = 0f; num13 < 14f; num13 += 1f)
				{
					Dust dust = Main.dust[Dust.NewDust(center2, 0, 0, 31, 0f, 0f, 0, default(Color), 1f)];
					Vector2 vector5 = Vector2.UnitY.RotatedBy((double)(num13 * 6.2831855f / 14f + num12), default(Vector2));
					vector5 *= 0.2f * (float)i;
					dust.position = center2 + vector5 * vector4;
					dust.velocity = vector5 + new Vector2(0f, player.gravDir * 4f);
					dust.noGravity = true;
					dust.scale = 1f + Main.rand.NextFloat() * 0.8f;
					dust.fadeIn = Main.rand.NextFloat() * 2f;
					dust.shader = GameShaders.Armor.GetSecondaryShader(player.cMount, player);
				}
			}
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x00509DAA File Offset: 0x00507FAA
		public override void UpdateHorizontalSpeeds(Player player)
		{
			player.runAcceleration *= 3f;
			player.maxRunSpeed *= 1.5f;
		}
	}
}
