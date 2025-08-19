using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	// Token: 0x02000221 RID: 545
	public sealed class BlizzardInABottleJump : VanillaExtraJump
	{
		// Token: 0x06002845 RID: 10309 RVA: 0x0050A59C File Offset: 0x0050879C
		public override float GetDurationMultiplier(Player player)
		{
			return 1.5f;
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x0050A5A4 File Offset: 0x005087A4
		public override void ShowVisuals(Player player)
		{
			int num12 = player.height - 6;
			if (player.gravDir == -1f)
			{
				num12 = 6;
			}
			for (int i = 0; i < 2; i++)
			{
				int num13 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)num12), player.width, 12, 76, player.velocity.X * 0.3f, player.velocity.Y * 0.3f, 0, default(Color), 1f);
				Main.dust[num13].velocity *= 0.1f;
				if (i == 0)
				{
					Main.dust[num13].velocity += player.velocity * 0.03f;
				}
				else
				{
					Main.dust[num13].velocity -= player.velocity * 0.03f;
				}
				Main.dust[num13].velocity -= player.velocity * 0.1f;
				Main.dust[num13].noGravity = true;
				Main.dust[num13].noLight = true;
			}
			for (int j = 0; j < 3; j++)
			{
				int num14 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)num12), player.width, 12, 76, player.velocity.X * 0.3f, player.velocity.Y * 0.3f, 0, default(Color), 1f);
				Main.dust[num14].fadeIn = 1.5f;
				Main.dust[num14].velocity *= 0.6f;
				Main.dust[num14].velocity += player.velocity * 0.8f;
				Main.dust[num14].noGravity = true;
				Main.dust[num14].noLight = true;
			}
			for (int k = 0; k < 3; k++)
			{
				int num15 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)num12), player.width, 12, 76, player.velocity.X * 0.3f, player.velocity.Y * 0.3f, 0, default(Color), 1f);
				Main.dust[num15].fadeIn = 1.5f;
				Main.dust[num15].velocity *= 0.6f;
				Main.dust[num15].velocity -= player.velocity * 0.8f;
				Main.dust[num15].noGravity = true;
				Main.dust[num15].noLight = true;
			}
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x0050A8BC File Offset: 0x00508ABC
		public override void UpdateHorizontalSpeeds(Player player)
		{
			player.runAcceleration *= 3f;
			player.maxRunSpeed *= 1.5f;
		}
	}
}
