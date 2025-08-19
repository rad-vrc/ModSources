using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameInput;

namespace Terraria
{
	// Token: 0x02000057 RID: 87
	public class TestHighFPSIssues
	{
		// Token: 0x06000F12 RID: 3858 RVA: 0x003FC1AC File Offset: 0x003FA3AC
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

		// Token: 0x06000F13 RID: 3859 RVA: 0x003FC204 File Offset: 0x003FA404
		public static void TapUpdateEnd(GameTime gt)
		{
			TestHighFPSIssues._tapUpdateEnds.Add(gt.TotalGameTime.TotalMilliseconds);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x003FC22C File Offset: 0x003FA42C
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

		// Token: 0x06000F15 RID: 3861 RVA: 0x003FC284 File Offset: 0x003FA484
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
					break;
				}
				TestHighFPSIssues._tapUpdates.RemoveAt(0);
			}
			while (TestHighFPSIssues._tapDraws.Count > 0)
			{
				if (TestHighFPSIssues._tapDraws[0] >= num)
				{
					break;
				}
				TestHighFPSIssues._tapDraws.RemoveAt(0);
			}
			while (TestHighFPSIssues._tapUpdateEnds.Count > 0 && TestHighFPSIssues._tapUpdateEnds[0] < num)
			{
				TestHighFPSIssues._tapUpdateEnds.RemoveAt(0);
			}
			Main.versionNumber = string.Concat(new string[]
			{
				"total (u/d)   ",
				TestHighFPSIssues._tapUpdates.Count.ToString(),
				" ",
				TestHighFPSIssues._tapUpdateEnds.Count.ToString(),
				"  ",
				TestHighFPSIssues.race.ToString(),
				" ",
				TestHighFPSIssues.conUH.ToString(),
				" ",
				TestHighFPSIssues.conDH.ToString()
			});
			Main.NewText(Main.versionNumber, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}

		// Token: 0x04000EC1 RID: 3777
		private static List<double> _tapUpdates = new List<double>();

		// Token: 0x04000EC2 RID: 3778
		private static List<double> _tapUpdateEnds = new List<double>();

		// Token: 0x04000EC3 RID: 3779
		private static List<double> _tapDraws = new List<double>();

		// Token: 0x04000EC4 RID: 3780
		private static int conU;

		// Token: 0x04000EC5 RID: 3781
		private static int conUH;

		// Token: 0x04000EC6 RID: 3782
		private static int conD;

		// Token: 0x04000EC7 RID: 3783
		private static int conDH;

		// Token: 0x04000EC8 RID: 3784
		private static int race;
	}
}
