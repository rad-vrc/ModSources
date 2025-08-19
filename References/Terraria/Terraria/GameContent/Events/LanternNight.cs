using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;

namespace Terraria.GameContent.Events
{
	// Token: 0x020002A6 RID: 678
	public class LanternNight
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x0051FFE2 File Offset: 0x0051E1E2
		public static bool LanternsUp
		{
			get
			{
				return LanternNight.GenuineLanterns || LanternNight.ManualLanterns;
			}
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x0051FFF4 File Offset: 0x0051E1F4
		public static void CheckMorning()
		{
			if (LanternNight.GenuineLanterns)
			{
				LanternNight.GenuineLanterns = false;
			}
			if (LanternNight.ManualLanterns)
			{
				LanternNight.ManualLanterns = false;
			}
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x00520023 File Offset: 0x0051E223
		public static void CheckNight()
		{
			LanternNight.NaturalAttempt();
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x0052002A File Offset: 0x0051E22A
		public static bool LanternsCanPersist()
		{
			return !Main.dayTime && LanternNight.LanternsCanStart();
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0052003A File Offset: 0x0051E23A
		public static bool LanternsCanStart()
		{
			return !Main.bloodMoon && !Main.pumpkinMoon && !Main.snowMoon && Main.invasionType == 0 && NPC.MoonLordCountdown == 0 && !LanternNight.BossIsActive();
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x0052006C File Offset: 0x0051E26C
		private static bool BossIsActive()
		{
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && (npc.boss || (npc.type >= 13 && npc.type <= 15)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x005200B8 File Offset: 0x0051E2B8
		private static void NaturalAttempt()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			if (!LanternNight.LanternsCanStart())
			{
				return;
			}
			bool flag = false;
			if (LanternNight.LanternNightsOnCooldown > 0)
			{
				LanternNight.LanternNightsOnCooldown--;
			}
			if (LanternNight.LanternNightsOnCooldown == 0 && NPC.downedMoonlord && Main.rand.Next(14) == 0)
			{
				flag = true;
			}
			if (!flag && LanternNight.NextNightIsLanternNight)
			{
				LanternNight.NextNightIsLanternNight = false;
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			LanternNight.GenuineLanterns = true;
			LanternNight.LanternNightsOnCooldown = Main.rand.Next(5, 11);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x00520138 File Offset: 0x0051E338
		public static void ToggleManualLanterns()
		{
			bool lanternsUp = LanternNight.LanternsUp;
			if (Main.netMode != 1)
			{
				LanternNight.ManualLanterns = !LanternNight.ManualLanterns;
			}
			if (lanternsUp != LanternNight.LanternsUp && Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0052018A File Offset: 0x0051E38A
		public static void WorldClear()
		{
			LanternNight.ManualLanterns = false;
			LanternNight.GenuineLanterns = false;
			LanternNight.LanternNightsOnCooldown = 0;
			LanternNight._wasLanternNight = false;
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x005201A4 File Offset: 0x0051E3A4
		public static void UpdateTime()
		{
			if (LanternNight.GenuineLanterns && !LanternNight.LanternsCanPersist())
			{
				LanternNight.GenuineLanterns = false;
			}
			if (LanternNight._wasLanternNight != LanternNight.LanternsUp)
			{
				if (Main.netMode != 2)
				{
					if (LanternNight.LanternsUp)
					{
						SkyManager.Instance.Activate("Lantern", default(Vector2), new object[0]);
					}
					else
					{
						SkyManager.Instance.Deactivate("Lantern", new object[0]);
					}
				}
				else
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			LanternNight._wasLanternNight = LanternNight.LanternsUp;
		}

		// Token: 0x04004715 RID: 18197
		public static bool ManualLanterns;

		// Token: 0x04004716 RID: 18198
		public static bool GenuineLanterns;

		// Token: 0x04004717 RID: 18199
		public static bool NextNightIsLanternNight;

		// Token: 0x04004718 RID: 18200
		public static int LanternNightsOnCooldown;

		// Token: 0x04004719 RID: 18201
		private static bool _wasLanternNight;
	}
}
