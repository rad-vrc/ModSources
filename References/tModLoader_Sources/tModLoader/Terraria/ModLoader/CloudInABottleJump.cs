using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	// Token: 0x02000224 RID: 548
	public sealed class CloudInABottleJump : VanillaExtraJump
	{
		// Token: 0x06002853 RID: 10323 RVA: 0x0050B1A4 File Offset: 0x005093A4
		public override float GetDurationMultiplier(Player player)
		{
			return 0.75f;
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x0050B1AC File Offset: 0x005093AC
		public override void OnStarted(Player player, ref bool playSound)
		{
			int num22 = player.height;
			if (player.gravDir == -1f)
			{
				num22 = 0;
			}
			for (int num23 = 0; num23 < 10; num23++)
			{
				int num24 = Dust.NewDust(new Vector2(player.position.X - 34f, player.position.Y + (float)num22 - 16f), 102, 32, 16, (0f - player.velocity.X) * 0.5f, player.velocity.Y * 0.5f, 100, default(Color), 1.5f);
				Main.dust[num24].velocity.X = Main.dust[num24].velocity.X * 0.5f - player.velocity.X * 0.1f;
				Main.dust[num24].velocity.Y = Main.dust[num24].velocity.Y * 0.5f - player.velocity.Y * 0.3f;
			}
			int num25 = Gore.NewGore(new Vector2(player.position.X + (float)(player.width / 2) - 16f, player.position.Y + (float)num22 - 16f), new Vector2(0f - player.velocity.X, 0f - player.velocity.Y), Main.rand.Next(11, 14), 1f);
			Main.gore[num25].velocity.X = Main.gore[num25].velocity.X * 0.1f - player.velocity.X * 0.1f;
			Main.gore[num25].velocity.Y = Main.gore[num25].velocity.Y * 0.1f - player.velocity.Y * 0.05f;
			num25 = Gore.NewGore(new Vector2(player.position.X - 36f, player.position.Y + (float)num22 - 16f), new Vector2(0f - player.velocity.X, 0f - player.velocity.Y), Main.rand.Next(11, 14), 1f);
			Main.gore[num25].velocity.X = Main.gore[num25].velocity.X * 0.1f - player.velocity.X * 0.1f;
			Main.gore[num25].velocity.Y = Main.gore[num25].velocity.Y * 0.1f - player.velocity.Y * 0.05f;
			num25 = Gore.NewGore(new Vector2(player.position.X + (float)player.width + 4f, player.position.Y + (float)num22 - 16f), new Vector2(0f - player.velocity.X, 0f - player.velocity.Y), Main.rand.Next(11, 14), 1f);
			Main.gore[num25].velocity.X = Main.gore[num25].velocity.X * 0.1f - player.velocity.X * 0.1f;
			Main.gore[num25].velocity.Y = Main.gore[num25].velocity.Y * 0.1f - player.velocity.Y * 0.05f;
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x0050B574 File Offset: 0x00509774
		public override void ShowVisuals(Player player)
		{
			int num = player.height;
			if (player.gravDir == -1f)
			{
				num = -6;
			}
			int num2 = Dust.NewDust(new Vector2(player.position.X - 4f, player.position.Y + (float)num), player.width + 8, 4, 16, (0f - player.velocity.X) * 0.5f, player.velocity.Y * 0.5f, 100, default(Color), 1.5f);
			Main.dust[num2].velocity.X = Main.dust[num2].velocity.X * 0.5f - player.velocity.X * 0.1f;
			Main.dust[num2].velocity.Y = Main.dust[num2].velocity.Y * 0.5f - player.velocity.Y * 0.3f;
		}
	}
}
