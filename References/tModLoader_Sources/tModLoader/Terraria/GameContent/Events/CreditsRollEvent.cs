using System;
using Terraria.GameContent.Skies;
using Terraria.Graphics.Effects;

namespace Terraria.GameContent.Events
{
	// Token: 0x0200062B RID: 1579
	public class CreditsRollEvent
	{
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600451A RID: 17690 RVA: 0x0060EC44 File Offset: 0x0060CE44
		public static bool IsEventOngoing
		{
			get
			{
				return CreditsRollEvent._creditsRollRemainingTime > 0;
			}
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x0060EC50 File Offset: 0x0060CE50
		public static void TryStartingCreditsRoll()
		{
			CreditsRollEvent._creditsRollRemainingTime = 28800;
			CreditsRollSky creditsRollSky = SkyManager.Instance["CreditsRoll"] as CreditsRollSky;
			if (creditsRollSky != null)
			{
				CreditsRollEvent._creditsRollRemainingTime = creditsRollSky.AmountOfTimeNeededForFullPlay;
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(140, -1, -1, null, 0, (float)CreditsRollEvent._creditsRollRemainingTime, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x0060ECB4 File Offset: 0x0060CEB4
		public static void SendCreditsRollRemainingTimeToPlayer(int playerIndex)
		{
			if (CreditsRollEvent._creditsRollRemainingTime != 0 && Main.netMode == 2)
			{
				NetMessage.SendData(140, playerIndex, -1, null, 0, (float)CreditsRollEvent._creditsRollRemainingTime, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x0060ECF1 File Offset: 0x0060CEF1
		public static void UpdateTime()
		{
			CreditsRollEvent._creditsRollRemainingTime = Utils.Clamp<int>(CreditsRollEvent._creditsRollRemainingTime - 1, 0, 28800);
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x0060ED0A File Offset: 0x0060CF0A
		public static void Reset()
		{
			CreditsRollEvent._creditsRollRemainingTime = 0;
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x0060ED12 File Offset: 0x0060CF12
		public static void SetRemainingTimeDirect(int time)
		{
			CreditsRollEvent._creditsRollRemainingTime = Utils.Clamp<int>(time, 0, 28800);
		}

		// Token: 0x04005ACE RID: 23246
		private const int MAX_TIME_FOR_CREDITS_ROLL_IN_FRAMES = 28800;

		// Token: 0x04005ACF RID: 23247
		private static int _creditsRollRemainingTime;
	}
}
