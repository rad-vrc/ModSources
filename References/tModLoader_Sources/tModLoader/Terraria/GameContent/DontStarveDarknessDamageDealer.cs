using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x02000494 RID: 1172
	public class DontStarveDarknessDamageDealer
	{
		// Token: 0x06003907 RID: 14599 RVA: 0x0059618D File Offset: 0x0059438D
		public static void Reset()
		{
			DontStarveDarknessDamageDealer.ResetTimer();
			DontStarveDarknessDamageDealer.saidMessage = false;
			DontStarveDarknessDamageDealer.lastFrameWasTooBright = true;
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x005961A0 File Offset: 0x005943A0
		private static void ResetTimer()
		{
			DontStarveDarknessDamageDealer.darknessTimer = -1;
			DontStarveDarknessDamageDealer.darknessHitTimer = 0;
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x005961AE File Offset: 0x005943AE
		private static int GetDarknessDamagePerHit()
		{
			return 250;
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x005961B5 File Offset: 0x005943B5
		private static int GetDarknessTimeBeforeStartingHits()
		{
			return 120;
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x005961B9 File Offset: 0x005943B9
		private static int GetDarknessTimeForMessage()
		{
			return 60;
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x005961C0 File Offset: 0x005943C0
		public static void Update(Player player)
		{
			if (player.DeadOrGhost || Main.gameInactive || player.shimmering)
			{
				DontStarveDarknessDamageDealer.ResetTimer();
				return;
			}
			DontStarveDarknessDamageDealer.UpdateDarknessState(player);
			int darknessTimeBeforeStartingHits = DontStarveDarknessDamageDealer.GetDarknessTimeBeforeStartingHits();
			if (DontStarveDarknessDamageDealer.darknessTimer >= darknessTimeBeforeStartingHits)
			{
				DontStarveDarknessDamageDealer.darknessTimer = darknessTimeBeforeStartingHits;
				DontStarveDarknessDamageDealer.darknessHitTimer++;
				if (DontStarveDarknessDamageDealer.darknessHitTimer > 60 && !player.immune)
				{
					int darknessDamagePerHit = DontStarveDarknessDamageDealer.GetDarknessDamagePerHit();
					SoundEngine.PlaySound(SoundID.Item1, new Vector2?(player.Center), null);
					player.Hurt(PlayerDeathReason.ByOther(17, -1), darknessDamagePerHit, 0, false, false, -1, true, 0f, 0f, 4.5f);
					DontStarveDarknessDamageDealer.darknessHitTimer = 0;
				}
			}
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x00596268 File Offset: 0x00594468
		private static void UpdateDarknessState(Player player)
		{
			if (DontStarveDarknessDamageDealer.lastFrameWasTooBright = DontStarveDarknessDamageDealer.IsPlayerSafe(player))
			{
				if (DontStarveDarknessDamageDealer.saidMessage)
				{
					if (!Main.getGoodWorld)
					{
						Main.NewText(Language.GetTextValue("Game.DarknessSafe"), 50, 200, 50);
					}
					DontStarveDarknessDamageDealer.saidMessage = false;
				}
				DontStarveDarknessDamageDealer.ResetTimer();
				return;
			}
			int darknessTimeForMessage = DontStarveDarknessDamageDealer.GetDarknessTimeForMessage();
			if (DontStarveDarknessDamageDealer.darknessTimer >= darknessTimeForMessage && !DontStarveDarknessDamageDealer.saidMessage)
			{
				if (!Main.getGoodWorld)
				{
					Main.NewText(Language.GetTextValue("Game.DarknessDanger"), 200, 50, 50);
				}
				DontStarveDarknessDamageDealer.saidMessage = true;
			}
			DontStarveDarknessDamageDealer.darknessTimer++;
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x005962FC File Offset: 0x005944FC
		private static bool IsPlayerSafe(Player player)
		{
			Vector3 vector = Lighting.GetColor((int)player.Center.X / 16, (int)player.Center.Y / 16).ToVector3();
			if (Main.LocalGolfState != null && (Main.LocalGolfState.ShouldCameraTrackBallLastKnownLocation || Main.LocalGolfState.IsTrackingBall))
			{
				return DontStarveDarknessDamageDealer.lastFrameWasTooBright;
			}
			if (Main.DroneCameraTracker != null && Main.DroneCameraTracker.IsInUse())
			{
				return DontStarveDarknessDamageDealer.lastFrameWasTooBright;
			}
			return vector.Length() >= 0.1f;
		}

		// Token: 0x04005236 RID: 21046
		public const int DARKNESS_HIT_TIMER_MAX_BEFORE_HIT = 60;

		// Token: 0x04005237 RID: 21047
		public static int darknessTimer = -1;

		// Token: 0x04005238 RID: 21048
		public static int darknessHitTimer = 0;

		// Token: 0x04005239 RID: 21049
		public static bool saidMessage = false;

		// Token: 0x0400523A RID: 21050
		public static bool lastFrameWasTooBright = true;
	}
}
