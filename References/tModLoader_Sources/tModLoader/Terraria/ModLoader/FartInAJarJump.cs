using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;

namespace Terraria.ModLoader
{
	// Token: 0x02000222 RID: 546
	public sealed class FartInAJarJump : VanillaExtraJump
	{
		// Token: 0x06002849 RID: 10313 RVA: 0x0050A8EA File Offset: 0x00508AEA
		public override float GetDurationMultiplier(Player player)
		{
			return 2f;
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x0050A8F4 File Offset: 0x00508AF4
		public override void OnStarted(Player player, ref bool playSound)
		{
			int num7 = player.height;
			if (player.gravDir == -1f)
			{
				num7 = 0;
			}
			playSound = false;
			SoundEngine.PlaySound(SoundID.Item16, new Vector2?(player.position), null);
			for (int i = 0; i < 10; i++)
			{
				int num8 = Dust.NewDust(new Vector2(player.position.X - 34f, player.position.Y + (float)num7 - 16f), 102, 32, 188, (0f - player.velocity.X) * 0.5f, player.velocity.Y * 0.5f, 100, default(Color), 1.5f);
				Main.dust[num8].velocity.X = Main.dust[num8].velocity.X * 0.5f - player.velocity.X * 0.1f;
				Main.dust[num8].velocity.Y = Main.dust[num8].velocity.Y * 0.5f - player.velocity.Y * 0.3f;
			}
			int num9 = Gore.NewGore(new Vector2(player.position.X + (float)(player.width / 2) - 16f, player.position.Y + (float)num7 - 16f), new Vector2(0f - player.velocity.X, 0f - player.velocity.Y), Main.rand.Next(435, 438), 1f);
			Main.gore[num9].velocity.X = Main.gore[num9].velocity.X * 0.1f - player.velocity.X * 0.1f;
			Main.gore[num9].velocity.Y = Main.gore[num9].velocity.Y * 0.1f - player.velocity.Y * 0.05f;
			num9 = Gore.NewGore(new Vector2(player.position.X - 36f, player.position.Y + (float)num7 - 16f), new Vector2(0f - player.velocity.X, 0f - player.velocity.Y), Main.rand.Next(435, 438), 1f);
			Main.gore[num9].velocity.X = Main.gore[num9].velocity.X * 0.1f - player.velocity.X * 0.1f;
			Main.gore[num9].velocity.Y = Main.gore[num9].velocity.Y * 0.1f - player.velocity.Y * 0.05f;
			num9 = Gore.NewGore(new Vector2(player.position.X + (float)player.width + 4f, player.position.Y + (float)num7 - 16f), new Vector2(0f - player.velocity.X, 0f - player.velocity.Y), Main.rand.Next(435, 438), 1f);
			Main.gore[num9].velocity.X = Main.gore[num9].velocity.X * 0.1f - player.velocity.X * 0.1f;
			Main.gore[num9].velocity.Y = Main.gore[num9].velocity.Y * 0.1f - player.velocity.Y * 0.05f;
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x0050ACE8 File Offset: 0x00508EE8
		public override void ShowVisuals(Player player)
		{
			int num7 = player.height;
			if (player.gravDir == -1f)
			{
				num7 = -6;
			}
			int num8 = Dust.NewDust(new Vector2(player.position.X - 4f, player.position.Y + (float)num7), player.width + 8, 4, 188, (0f - player.velocity.X) * 0.5f, player.velocity.Y * 0.5f, 100, default(Color), 1.5f);
			Main.dust[num8].velocity.X = Main.dust[num8].velocity.X * 0.5f - player.velocity.X * 0.1f;
			Main.dust[num8].velocity.Y = Main.dust[num8].velocity.Y * 0.5f - player.velocity.Y * 0.3f;
			Main.dust[num8].velocity *= 0.5f;
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x0050AE0C File Offset: 0x0050900C
		public override void UpdateHorizontalSpeeds(Player player)
		{
			player.runAcceleration *= 3f;
			player.maxRunSpeed *= 1.75f;
		}
	}
}
