using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	// Token: 0x0200021E RID: 542
	public sealed class SantankMountJump : VanillaExtraJump
	{
		// Token: 0x06002838 RID: 10296 RVA: 0x00509DD8 File Offset: 0x00507FD8
		public override float GetDurationMultiplier(Player player)
		{
			return 2f;
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x00509DE0 File Offset: 0x00507FE0
		public override void OnStarted(Player player, ref bool playSound)
		{
			int num17 = player.height;
			if (player.gravDir == -1f)
			{
				num17 = 0;
			}
			for (int num18 = 0; num18 < 15; num18++)
			{
				int num19 = Dust.NewDust(new Vector2(player.position.X - 34f, player.position.Y + (float)num17 - 16f), 102, 32, 4, (0f - player.velocity.X) * 0.5f, player.velocity.Y * 0.5f, 100, new Color(250, 230, 230, 150), 1.5f);
				Main.dust[num19].velocity.X = Main.dust[num19].velocity.X * 0.5f - player.velocity.X * 0.1f;
				Main.dust[num19].velocity.Y = Main.dust[num19].velocity.Y * 0.5f - player.velocity.Y * 0.3f;
				Main.dust[num19].noGravity = true;
				num19 = Dust.NewDust(new Vector2(player.position.X - 34f, player.position.Y + (float)num17 - 16f), 102, 32, 6, (0f - player.velocity.X) * 0.5f, player.velocity.Y * 0.5f, 20, default(Color), 1.5f);
				Dust dust = Main.dust[num19];
				dust.velocity.Y = dust.velocity.Y - 1f;
				if (num18 % 2 == 0)
				{
					Main.dust[num19].fadeIn = Main.rand.NextFloat() * 2f;
				}
			}
			float y = player.Bottom.Y - 22f;
			for (int num20 = 0; num20 < 3; num20++)
			{
				Vector2 vector8 = player.Center;
				switch (num20)
				{
				case 0:
					vector8..ctor(player.Center.X - 16f, y);
					break;
				case 1:
					vector8..ctor(player.position.X - 36f, y);
					break;
				case 2:
					vector8..ctor(player.Right.X + 4f, y);
					break;
				}
				int num21 = Gore.NewGore(vector8, new Vector2(0f - player.velocity.X, 0f - player.velocity.Y), Main.rand.Next(61, 63), 1f);
				Main.gore[num21].velocity *= 0.1f;
				Gore gore = Main.gore[num21];
				gore.velocity.X = gore.velocity.X - player.velocity.X * 0.1f;
				Gore gore2 = Main.gore[num21];
				gore2.velocity.Y = gore2.velocity.Y - player.velocity.Y * 0.05f;
				Main.gore[num21].velocity += Main.rand.NextVector2Circular(1f, 1f) * 0.5f;
			}
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x0050A135 File Offset: 0x00508335
		public override void UpdateHorizontalSpeeds(Player player)
		{
			player.runAcceleration *= 3f;
			player.maxRunSpeed *= 1.5f;
		}
	}
}
