using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	// Token: 0x02000223 RID: 547
	public sealed class TsunamiInABottleJump : VanillaExtraJump
	{
		// Token: 0x0600284E RID: 10318 RVA: 0x0050AE3A File Offset: 0x0050903A
		public override float GetDurationMultiplier(Player player)
		{
			return 1.25f;
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x0050AE44 File Offset: 0x00509044
		public override void OnStarted(Player player, ref bool playSound)
		{
			int num5 = player.height;
			if (player.gravDir == -1f)
			{
				num5 = 0;
			}
			for (int i = 0; i < 30; i++)
			{
				int num6 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)num5), player.width, 12, 253, player.velocity.X * 0.3f, player.velocity.Y * 0.3f, 100, default(Color), 1.5f);
				if (i % 2 == 0)
				{
					Dust dust = Main.dust[num6];
					dust.velocity.X = dust.velocity.X + (float)Main.rand.Next(30, 71) * 0.1f;
				}
				else
				{
					Dust dust2 = Main.dust[num6];
					dust2.velocity.X = dust2.velocity.X - (float)Main.rand.Next(30, 71) * 0.1f;
				}
				Dust dust3 = Main.dust[num6];
				dust3.velocity.Y = dust3.velocity.Y + (float)Main.rand.Next(-10, 31) * 0.1f;
				Main.dust[num6].noGravity = true;
				Main.dust[num6].scale += (float)Main.rand.Next(-10, 41) * 0.01f;
				Main.dust[num6].velocity *= Main.dust[num6].scale * 0.7f;
				Vector2 vector;
				vector..ctor((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				vector.Normalize();
				vector *= (float)Main.rand.Next(81) * 0.1f;
			}
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x0050B008 File Offset: 0x00509208
		public override void ShowVisuals(Player player)
		{
			int num9 = 1;
			if (player.jump > 0)
			{
				num9 = 2;
			}
			int num10 = player.height - 6;
			if (player.gravDir == -1f)
			{
				num10 = 6;
			}
			for (int i = 0; i < num9; i++)
			{
				int num11 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)num10), player.width, 12, 253, player.velocity.X * 0.3f, player.velocity.Y * 0.3f, 100, default(Color), 1.5f);
				Main.dust[num11].scale += (float)Main.rand.Next(-5, 3) * 0.1f;
				if (player.jump <= 0)
				{
					Main.dust[num11].scale *= 0.8f;
				}
				else
				{
					Main.dust[num11].velocity -= player.velocity / 5f;
				}
				Main.dust[num11].noGravity = true;
				Vector2 vector;
				vector..ctor((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				vector.Normalize();
				vector *= (float)Main.rand.Next(81) * 0.1f;
			}
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x0050B176 File Offset: 0x00509376
		public override void UpdateHorizontalSpeeds(Player player)
		{
			player.runAcceleration *= 1.5f;
			player.maxRunSpeed *= 1.25f;
		}
	}
}
