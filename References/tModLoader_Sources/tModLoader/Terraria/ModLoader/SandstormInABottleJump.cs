using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	// Token: 0x02000220 RID: 544
	public sealed class SandstormInABottleJump : VanillaExtraJump
	{
		// Token: 0x06002841 RID: 10305 RVA: 0x0050A3C4 File Offset: 0x005085C4
		public override float GetDurationMultiplier(Player player)
		{
			return 3f;
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x0050A3CC File Offset: 0x005085CC
		public override void ShowVisuals(Player player)
		{
			int num3 = player.height;
			if (player.gravDir == -1f)
			{
				num3 = -6;
			}
			float num4 = ((float)player.jump / 75f + 1f) / 2f;
			for (int i = 0; i < 3; i++)
			{
				int num5 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)(num3 / 2)), player.width, 32, 124, player.velocity.X * 0.3f, player.velocity.Y * 0.3f, 150, default(Color), 1f * num4);
				Main.dust[num5].velocity *= 0.5f * num4;
				Main.dust[num5].fadeIn = 1.5f * num4;
			}
			player.sandStorm = true;
			if (player.miscCounter % 3 == 0)
			{
				int num6 = Gore.NewGore(new Vector2(player.position.X + (float)(player.width / 2) - 18f, player.position.Y + (float)(num3 / 2)), new Vector2(0f - player.velocity.X, 0f - player.velocity.Y), Main.rand.Next(220, 223), num4);
				Main.gore[num6].velocity = player.velocity * 0.3f * num4;
				Main.gore[num6].alpha = 100;
			}
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x0050A56E File Offset: 0x0050876E
		public override void UpdateHorizontalSpeeds(Player player)
		{
			player.runAcceleration *= 1.5f;
			player.maxRunSpeed *= 2f;
		}
	}
}
