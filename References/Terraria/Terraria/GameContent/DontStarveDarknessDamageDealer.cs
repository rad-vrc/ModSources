using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x020001CF RID: 463
	public class DontStarveDarknessDamageDealer
	{
		// Token: 0x06001BEA RID: 7146 RVA: 0x004F0A41 File Offset: 0x004EEC41
		public static void Reset()
		{
			DontStarveDarknessDamageDealer.ResetTimer();
			DontStarveDarknessDamageDealer.saidMessage = false;
			DontStarveDarknessDamageDealer.lastFrameWasTooBright = true;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x004F0A54 File Offset: 0x004EEC54
		private static void ResetTimer()
		{
			DontStarveDarknessDamageDealer.darknessTimer = -1;
			DontStarveDarknessDamageDealer.darknessHitTimer = 0;
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x004F0A62 File Offset: 0x004EEC62
		private static int GetDarknessDamagePerHit()
		{
			return 250;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x004F0A69 File Offset: 0x004EEC69
		private static int GetDarknessTimeBeforeStartingHits()
		{
			return 120;
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x004F0A6D File Offset: 0x004EEC6D
		private static int GetDarknessTimeForMessage()
		{
			return 60;
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x004F0A74 File Offset: 0x004EEC74
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
					SoundEngine.PlaySound(SoundID.Item1, player.Center);
					player.Hurt(PlayerDeathReason.ByOther(17), darknessDamagePerHit, 0, false, false, false, -1, true);
					DontStarveDarknessDamageDealer.darknessHitTimer = 0;
				}
			}
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x004F0B08 File Offset: 0x004EED08
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

		// Token: 0x06001BF1 RID: 7153 RVA: 0x004F0B9C File Offset: 0x004EED9C
		private static bool IsPlayerSafe(Player player)
		{
			Vector3 vector = Lighting.GetColor((int)player.Center.X / 16, (int)player.Center.Y / 16).ToVector3();
			bool result;
			if (Main.LocalGolfState != null && (Main.LocalGolfState.ShouldCameraTrackBallLastKnownLocation || Main.LocalGolfState.IsTrackingBall))
			{
				result = DontStarveDarknessDamageDealer.lastFrameWasTooBright;
			}
			else if (Main.DroneCameraTracker != null && Main.DroneCameraTracker.IsInUse())
			{
				result = DontStarveDarknessDamageDealer.lastFrameWasTooBright;
			}
			else
			{
				result = (vector.Length() >= 0.1f);
			}
			return result;
		}

		// Token: 0x04004355 RID: 17237
		public const int DARKNESS_HIT_TIMER_MAX_BEFORE_HIT = 60;

		// Token: 0x04004356 RID: 17238
		public static int darknessTimer = -1;

		// Token: 0x04004357 RID: 17239
		public static int darknessHitTimer = 0;

		// Token: 0x04004358 RID: 17240
		public static bool saidMessage = false;

		// Token: 0x04004359 RID: 17241
		public static bool lastFrameWasTooBright = true;
	}
}
