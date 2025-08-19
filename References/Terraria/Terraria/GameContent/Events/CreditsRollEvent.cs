using System;
using Terraria.GameContent.Skies;
using Terraria.Graphics.Effects;

namespace Terraria.GameContent.Events
{
	// Token: 0x020002A5 RID: 677
	public class CreditsRollEvent
	{
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060020F4 RID: 8436 RVA: 0x0051FF02 File Offset: 0x0051E102
		public static bool IsEventOngoing
		{
			get
			{
				return CreditsRollEvent._creditsRollRemainingTime > 0;
			}
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x0051FF0C File Offset: 0x0051E10C
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

		// Token: 0x060020F6 RID: 8438 RVA: 0x0051FF70 File Offset: 0x0051E170
		public static void SendCreditsRollRemainingTimeToPlayer(int playerIndex)
		{
			if (CreditsRollEvent._creditsRollRemainingTime == 0)
			{
				return;
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(140, playerIndex, -1, null, 0, (float)CreditsRollEvent._creditsRollRemainingTime, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x0051FFAE File Offset: 0x0051E1AE
		public static void UpdateTime()
		{
			CreditsRollEvent._creditsRollRemainingTime = Utils.Clamp<int>(CreditsRollEvent._creditsRollRemainingTime - 1, 0, 28800);
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x0051FFC7 File Offset: 0x0051E1C7
		public static void Reset()
		{
			CreditsRollEvent._creditsRollRemainingTime = 0;
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x0051FFCF File Offset: 0x0051E1CF
		public static void SetRemainingTimeDirect(int time)
		{
			CreditsRollEvent._creditsRollRemainingTime = Utils.Clamp<int>(time, 0, 28800);
		}

		// Token: 0x04004713 RID: 18195
		private const int MAX_TIME_FOR_CREDITS_ROLL_IN_FRAMES = 28800;

		// Token: 0x04004714 RID: 18196
		private static int _creditsRollRemainingTime;
	}
}
