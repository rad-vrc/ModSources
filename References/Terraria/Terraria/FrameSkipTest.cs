using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;

namespace Terraria
{
	// Token: 0x02000016 RID: 22
	public class FrameSkipTest
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000B818 File Offset: 0x00009A18
		public static void Update(GameTime gameTime)
		{
			float num = 60f;
			float num2 = 1f / num;
			float num3 = (float)gameTime.ElapsedGameTime.TotalSeconds;
			Thread.Sleep((int)MathHelper.Clamp((num2 - num3) * 1000f + 1f, 0f, 1000f));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000B868 File Offset: 0x00009A68
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

		// Token: 0x060000C7 RID: 199 RVA: 0x0000B8DE File Offset: 0x00009ADE
		public static void UpdateServerTest()
		{
			FrameSkipTest.serverFramerateTest.Record("frame time");
			FrameSkipTest.serverFramerateTest.StopAndPrint();
			FrameSkipTest.serverFramerateTest.Start();
		}

		// Token: 0x04000075 RID: 117
		private static int LastRecordedSecondNumber;

		// Token: 0x04000076 RID: 118
		private static float CallsThisSecond;

		// Token: 0x04000077 RID: 119
		private static float DeltasThisSecond;

		// Token: 0x04000078 RID: 120
		private static List<float> DeltaSamples = new List<float>();

		// Token: 0x04000079 RID: 121
		private const int SamplesCount = 5;

		// Token: 0x0400007A RID: 122
		private static MultiTimer serverFramerateTest = new MultiTimer(60);
	}
}
