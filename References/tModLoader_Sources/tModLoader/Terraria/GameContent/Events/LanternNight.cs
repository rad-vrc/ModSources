using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;

namespace Terraria.GameContent.Events
{
	// Token: 0x0200062E RID: 1582
	public class LanternNight
	{
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x00612A43 File Offset: 0x00610C43
		public static bool LanternsUp
		{
			get
			{
				return LanternNight.GenuineLanterns || LanternNight.ManualLanterns;
			}
		}

		// Token: 0x06004569 RID: 17769 RVA: 0x00612A53 File Offset: 0x00610C53
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

		// Token: 0x0600456A RID: 17770 RVA: 0x00612A6F File Offset: 0x00610C6F
		public static void CheckNight()
		{
			LanternNight.NaturalAttempt();
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x00612A76 File Offset: 0x00610C76
		public static bool LanternsCanPersist()
		{
			return !Main.dayTime && LanternNight.LanternsCanStart();
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x00612A86 File Offset: 0x00610C86
		public static bool LanternsCanStart()
		{
			return !Main.bloodMoon && !Main.pumpkinMoon && !Main.snowMoon && Main.invasionType == 0 && NPC.MoonLordCountdown == 0 && !LanternNight.BossIsActive();
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x00612AB8 File Offset: 0x00610CB8
		private static bool BossIsActive()
		{
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC.active && (nPC.boss || (nPC.type >= 13 && nPC.type <= 15)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x00612B04 File Offset: 0x00610D04
		private static void NaturalAttempt()
		{
			if (Main.netMode != 1 && LanternNight.LanternsCanStart())
			{
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
				if (flag)
				{
					LanternNight.GenuineLanterns = true;
					LanternNight.LanternNightsOnCooldown = Main.rand.Next(5, 11);
				}
			}
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x00612B84 File Offset: 0x00610D84
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

		// Token: 0x06004570 RID: 17776 RVA: 0x00612BD6 File Offset: 0x00610DD6
		public static void WorldClear()
		{
			LanternNight.ManualLanterns = false;
			LanternNight.GenuineLanterns = false;
			LanternNight.LanternNightsOnCooldown = 0;
			LanternNight._wasLanternNight = false;
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x00612BF0 File Offset: 0x00610DF0
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
						SkyManager.Instance.Activate("Lantern", default(Vector2), Array.Empty<object>());
					}
					else
					{
						SkyManager.Instance.Deactivate("Lantern", Array.Empty<object>());
					}
				}
				else
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			LanternNight._wasLanternNight = LanternNight.LanternsUp;
		}

		// Token: 0x04005AEC RID: 23276
		public static bool ManualLanterns;

		// Token: 0x04005AED RID: 23277
		public static bool GenuineLanterns;

		// Token: 0x04005AEE RID: 23278
		public static bool NextNightIsLanternNight;

		// Token: 0x04005AEF RID: 23279
		public static int LanternNightsOnCooldown;

		// Token: 0x04005AF0 RID: 23280
		private static bool _wasLanternNight;
	}
}
