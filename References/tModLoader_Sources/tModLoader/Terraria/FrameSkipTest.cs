using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;

namespace Terraria
{
	// Token: 0x0200002D RID: 45
	public class FrameSkipTest
	{
		// Token: 0x06000202 RID: 514 RVA: 0x00021CF0 File Offset: 0x0001FEF0
		public static void Update(GameTime gameTime)
		{
			float num = 60f;
			float num3 = 1f / num;
			float num2 = (float)gameTime.ElapsedGameTime.TotalSeconds;
			Thread.Sleep((int)MathHelper.Clamp((num3 - num2) * 1000f + 1f, 0f, 1000f));
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00021D40 File Offset: 0x0001FF40
		public static void CheckReset(GameTime gameTime)
		{
			if (FrameSkipTest.LastRecordedSecondNumber != gameTime.TotalGameTime.Seconds)
			{
				FrameSkipTest.DeltaSamples.Add(FrameSkipTest.DeltasThisSecond / FrameSkipTest.CallsThisSecond);
				if (FrameSkipTest.DeltaSamples.Count > 5)
				{
					FrameSkipTest.DeltaSamples.RemoveAt(0);
				}
				FrameSkipTest.CallsThisSecond = 0f;
				FrameSkipTest.DeltasThisSecond = 0f;
				FrameSkipTest.LastRecordedSecondNumber = gameTime.TotalGameTime.Seconds;
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00021DB6 File Offset: 0x0001FFB6
		public static void UpdateServerTest()
		{
			FrameSkipTest.serverFramerateTest.Record("frame time");
			FrameSkipTest.serverFramerateTest.StopAndPrint();
			FrameSkipTest.serverFramerateTest.Start();
		}

		// Token: 0x040001CC RID: 460
		private static int LastRecordedSecondNumber;

		// Token: 0x040001CD RID: 461
		private static float CallsThisSecond;

		// Token: 0x040001CE RID: 462
		private static float DeltasThisSecond;

		// Token: 0x040001CF RID: 463
		private static List<float> DeltaSamples = new List<float>();

		// Token: 0x040001D0 RID: 464
		private const int SamplesCount = 5;

		// Token: 0x040001D1 RID: 465
		private static MultiTimer serverFramerateTest = new MultiTimer(60);
	}
}
