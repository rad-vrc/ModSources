using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameInput;

namespace Terraria
{
	// Token: 0x02000017 RID: 23
	public class TestHighFPSIssues
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0000B924 File Offset: 0x00009B24
		public static void TapUpdate(GameTime gt)
		{
			TestHighFPSIssues._tapUpdates.Add(gt.TotalGameTime.TotalMilliseconds);
			TestHighFPSIssues.conD = 0;
			TestHighFPSIssues.race--;
			if (++TestHighFPSIssues.conU > TestHighFPSIssues.conUH)
			{
				TestHighFPSIssues.conUH = TestHighFPSIssues.conU;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000B97C File Offset: 0x00009B7C
		public static void TapUpdateEnd(GameTime gt)
		{
			TestHighFPSIssues._tapUpdateEnds.Add(gt.TotalGameTime.TotalMilliseconds);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		public static void TapDraw(GameTime gt)
		{
			TestHighFPSIssues._tapDraws.Add(gt.TotalGameTime.TotalMilliseconds);
			TestHighFPSIssues.conU = 0;
			TestHighFPSIssues.race++;
			if (++TestHighFPSIssues.conD > TestHighFPSIssues.conDH)
			{
				TestHighFPSIssues.conDH = TestHighFPSIssues.conD;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000B9FC File Offset: 0x00009BFC
		public static void Update(GameTime gt)
		{
			if (PlayerInput.Triggers.Current.Down)
			{
				TestHighFPSIssues.race = (TestHighFPSIssues.conUH = (TestHighFPSIssues.conDH = 0));
			}
			double num = gt.TotalGameTime.TotalMilliseconds - 5000.0;
			while (TestHighFPSIssues._tapUpdates.Count > 0)
			{
				if (TestHighFPSIssues._tapUpdates[0] >= num)
				{
					IL_7F:
					while (TestHighFPSIssues._tapDraws.Count > 0)
					{
						if (TestHighFPSIssues._tapDraws[0] >= num)
						{
							IL_A7:
							while (TestHighFPSIssues._tapUpdateEnds.Count > 0 && TestHighFPSIssues._tapUpdateEnds[0] < num)
							{
								TestHighFPSIssues._tapUpdateEnds.RemoveAt(0);
							}
							Main.versionNumber = string.Concat(new object[]
							{
								"total (u/d)   ",
								TestHighFPSIssues._tapUpdates.Count,
								" ",
								TestHighFPSIssues._tapUpdateEnds.Count,
								"  ",
								TestHighFPSIssues.race,
								" ",
								TestHighFPSIssues.conUH,
								" ",
								TestHighFPSIssues.conDH
							});
							Main.NewText(Main.versionNumber, byte.MaxValue, byte.MaxValue, byte.MaxValue);
							return;
						}
						TestHighFPSIssues._tapDraws.RemoveAt(0);
					}
					goto IL_A7;
				}
				TestHighFPSIssues._tapUpdates.RemoveAt(0);
			}
			goto IL_7F;
		}

		// Token: 0x0400007B RID: 123
		private static List<double> _tapUpdates = new List<double>();

		// Token: 0x0400007C RID: 124
		private static List<double> _tapUpdateEnds = new List<double>();

		// Token: 0x0400007D RID: 125
		private static List<double> _tapDraws = new List<double>();

		// Token: 0x0400007E RID: 126
		private static int conU;

		// Token: 0x0400007F RID: 127
		private static int conUH;

		// Token: 0x04000080 RID: 128
		private static int conD;

		// Token: 0x04000081 RID: 129
		private static int conDH;

		// Token: 0x04000082 RID: 130
		private static int race;
	}
}
