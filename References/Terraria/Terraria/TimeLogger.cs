using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Terraria
{
	// Token: 0x0200004A RID: 74
	public static class TimeLogger
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x003ECD64 File Offset: 0x003EAF64
		public static void Initialize()
		{
			TimeLogger.currentFrame = 0;
			TimeLogger.framesToLog = -1;
			TimeLogger.detailedDrawTimer = new Stopwatch();
			TimeLogger.renderTimes = new TimeLogger.TimeLogData[10];
			TimeLogger.drawTimes = new TimeLogger.TimeLogData[10];
			TimeLogger.lightingTimes = new TimeLogger.TimeLogData[5];
			TimeLogger.detailedDrawTimes = new TimeLogger.TimeLogData[40];
			for (int i = 0; i < TimeLogger.renderTimes.Length; i++)
			{
				TimeLogger.renderTimes[i].logText = string.Format("Render #{0}", i);
			}
			TimeLogger.drawTimes[0].logText = "Drawing Solid Tiles";
			TimeLogger.drawTimes[1].logText = "Drawing Non-Solid Tiles";
			TimeLogger.drawTimes[2].logText = "Drawing Wall Tiles";
			TimeLogger.drawTimes[3].logText = "Drawing Underground Background";
			TimeLogger.drawTimes[4].logText = "Drawing Water Tiles";
			TimeLogger.drawTimes[5].logText = "Drawing Black Tiles";
			TimeLogger.lightingTimes[0].logText = "Lighting Initialization";
			for (int j = 1; j < TimeLogger.lightingTimes.Length; j++)
			{
				TimeLogger.lightingTimes[j].logText = string.Format("Lighting Pass #{0}", j);
			}
			TimeLogger.detailedDrawTimes[0].logText = "Finding color tiles";
			TimeLogger.detailedDrawTimes[1].logText = "Initial Map Update";
			TimeLogger.detailedDrawTimes[2].logText = "Finding Waterfalls";
			TimeLogger.detailedDrawTimes[3].logText = "Map Section Update";
			TimeLogger.detailedDrawTimes[4].logText = "Map Update";
			TimeLogger.detailedDrawTimes[5].logText = "Section Framing";
			TimeLogger.detailedDrawTimes[6].logText = "Sky Background";
			TimeLogger.detailedDrawTimes[7].logText = "Sun, Moon & Stars";
			TimeLogger.detailedDrawTimes[8].logText = "Surface Background";
			TimeLogger.detailedDrawTimes[9].logText = "Map";
			TimeLogger.detailedDrawTimes[10].logText = "Player Chat";
			TimeLogger.detailedDrawTimes[11].logText = "Water Target";
			TimeLogger.detailedDrawTimes[12].logText = "Background Target";
			TimeLogger.detailedDrawTimes[13].logText = "Black Tile Target";
			TimeLogger.detailedDrawTimes[14].logText = "Wall Target";
			TimeLogger.detailedDrawTimes[15].logText = "Non Solid Tile Target";
			TimeLogger.detailedDrawTimes[16].logText = "Waterfalls";
			TimeLogger.detailedDrawTimes[17].logText = "Solid Tile Target";
			TimeLogger.detailedDrawTimes[18].logText = "NPCs (Behind Tiles)";
			TimeLogger.detailedDrawTimes[19].logText = "NPC";
			TimeLogger.detailedDrawTimes[20].logText = "Projectiles";
			TimeLogger.detailedDrawTimes[21].logText = "Players";
			TimeLogger.detailedDrawTimes[22].logText = "Items";
			TimeLogger.detailedDrawTimes[23].logText = "Rain";
			TimeLogger.detailedDrawTimes[24].logText = "Gore";
			TimeLogger.detailedDrawTimes[25].logText = "Dust";
			TimeLogger.detailedDrawTimes[26].logText = "Water Target";
			TimeLogger.detailedDrawTimes[27].logText = "Interface";
			TimeLogger.detailedDrawTimes[28].logText = "Render Solid Tiles";
			TimeLogger.detailedDrawTimes[29].logText = "Render Non Solid Tiles";
			TimeLogger.detailedDrawTimes[30].logText = "Render Black Tiles";
			TimeLogger.detailedDrawTimes[31].logText = "Render Water/Wires";
			TimeLogger.detailedDrawTimes[32].logText = "Render Walls";
			TimeLogger.detailedDrawTimes[33].logText = "Render Backgrounds";
			TimeLogger.detailedDrawTimes[34].logText = "Drawing Wires";
			TimeLogger.detailedDrawTimes[35].logText = "Render layers up to Players";
			TimeLogger.detailedDrawTimes[36].logText = "Render Items/Rain/Gore/Dust/Water/Map";
			TimeLogger.detailedDrawTimes[37].logText = "Render Interface";
			for (int k = 0; k < TimeLogger.detailedDrawTimes.Length; k++)
			{
				if (string.IsNullOrEmpty(TimeLogger.detailedDrawTimes[k].logText))
				{
					TimeLogger.detailedDrawTimes[k].logText = string.Format("Unnamed detailed draw #{0}", k);
				}
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x003ED231 File Offset: 0x003EB431
		public static void Start()
		{
			if (TimeLogger.currentlyLogging)
			{
				TimeLogger.endLoggingThisFrame = true;
				TimeLogger.startLoggingNextFrame = false;
				return;
			}
			TimeLogger.startLoggingNextFrame = true;
			TimeLogger.endLoggingThisFrame = false;
			Main.NewText("Detailed logging started", 250, 250, 0);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x003ED268 File Offset: 0x003EB468
		public static void NewDrawFrame()
		{
			for (int i = 0; i < TimeLogger.renderTimes.Length; i++)
			{
				TimeLogger.renderTimes[i].usedLastDraw = false;
			}
			for (int j = 0; j < TimeLogger.drawTimes.Length; j++)
			{
				TimeLogger.drawTimes[j].usedLastDraw = false;
			}
			for (int k = 0; k < TimeLogger.lightingTimes.Length; k++)
			{
				TimeLogger.lightingTimes[k].usedLastDraw = false;
			}
			if (TimeLogger.startLoggingNextFrame)
			{
				TimeLogger.startLoggingNextFrame = false;
				DateTime now = DateTime.Now;
				string path = Main.SavePath + Path.DirectorySeparatorChar.ToString() + "TerrariaDrawLog.7z";
				try
				{
					TimeLogger.logWriter = new StreamWriter(new GZipStream(new FileStream(path, FileMode.Create), CompressionMode.Compress));
					TimeLogger.logBuilder = new StringBuilder(5000);
					TimeLogger.framesToLog = 600;
					TimeLogger.currentFrame = 1;
					TimeLogger.currentlyLogging = true;
				}
				catch
				{
					Main.NewText("Detailed logging could not be started.", 250, 250, 0);
				}
			}
			if (TimeLogger.currentlyLogging)
			{
				TimeLogger.logBuilder.AppendLine(string.Format("Start of Frame #{0}", TimeLogger.currentFrame));
			}
			TimeLogger.detailedDrawTimer.Restart();
			TimeLogger.lastDetailedDrawTime = TimeLogger.detailedDrawTimer.Elapsed.TotalMilliseconds;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x003ED3C0 File Offset: 0x003EB5C0
		public static void EndDrawFrame()
		{
			if (TimeLogger.currentFrame <= TimeLogger.framesToLog)
			{
				TimeLogger.logBuilder.AppendLine(string.Format("End of Frame #{0}", TimeLogger.currentFrame));
				TimeLogger.logBuilder.AppendLine();
				if (TimeLogger.endLoggingThisFrame)
				{
					TimeLogger.endLoggingThisFrame = false;
					TimeLogger.logBuilder.AppendLine("Logging ended early");
					TimeLogger.currentFrame = TimeLogger.framesToLog;
				}
				if (TimeLogger.logBuilder.Length > 4000)
				{
					TimeLogger.logWriter.Write(TimeLogger.logBuilder.ToString());
					TimeLogger.logBuilder.Clear();
				}
				TimeLogger.currentFrame++;
				if (TimeLogger.currentFrame > TimeLogger.framesToLog)
				{
					Main.NewText("Detailed logging ended.", 250, 250, 0);
					TimeLogger.logWriter.Write(TimeLogger.logBuilder.ToString());
					TimeLogger.logBuilder.Clear();
					TimeLogger.logBuilder = null;
					TimeLogger.logWriter.Flush();
					TimeLogger.logWriter.Close();
					TimeLogger.logWriter = null;
					TimeLogger.framesToLog = -1;
					TimeLogger.currentFrame = 0;
					TimeLogger.currentlyLogging = false;
				}
			}
			TimeLogger.detailedDrawTimer.Stop();
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x003ED4E8 File Offset: 0x003EB6E8
		private static void UpdateTime(TimeLogger.TimeLogData[] times, int type, double time)
		{
			bool flag = false;
			if (times[type].resetMaxTime > 0)
			{
				times[type].resetMaxTime = times[type].resetMaxTime - 1;
			}
			else
			{
				times[type].timeMax = 0f;
			}
			times[type].time = (float)time;
			if ((double)times[type].timeMax < time)
			{
				flag = true;
				times[type].timeMax = (float)time;
				times[type].resetMaxTime = 100;
			}
			times[type].usedLastDraw = true;
			if (TimeLogger.currentFrame != 0)
			{
				TimeLogger.logBuilder.AppendLine(string.Format("    {0} : {1:F4}ms {2}", times[type].logText, time, flag ? " - New Maximum" : string.Empty));
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x003ED5B0 File Offset: 0x003EB7B0
		public static void RenderTime(int renderType, double timeElapsed)
		{
			if (renderType < 0 || renderType >= TimeLogger.renderTimes.Length)
			{
				return;
			}
			TimeLogger.UpdateTime(TimeLogger.renderTimes, renderType, timeElapsed);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x003ED5CD File Offset: 0x003EB7CD
		public static float GetRenderTime(int renderType)
		{
			return TimeLogger.renderTimes[renderType].time;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x003ED5DF File Offset: 0x003EB7DF
		public static float GetRenderMax(int renderType)
		{
			return TimeLogger.renderTimes[renderType].timeMax;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x003ED5F1 File Offset: 0x003EB7F1
		public static void DrawTime(int drawType, double timeElapsed)
		{
			if (drawType < 0 || drawType >= TimeLogger.drawTimes.Length)
			{
				return;
			}
			TimeLogger.UpdateTime(TimeLogger.drawTimes, drawType, timeElapsed);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x003ED60E File Offset: 0x003EB80E
		public static float GetDrawTime(int drawType)
		{
			return TimeLogger.drawTimes[drawType].time;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x003ED620 File Offset: 0x003EB820
		public static float GetDrawTotal()
		{
			float num = 0f;
			for (int i = 0; i < TimeLogger.drawTimes.Length; i++)
			{
				num += TimeLogger.drawTimes[i].time;
			}
			return num;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x003ED659 File Offset: 0x003EB859
		public static void LightingTime(int lightingType, double timeElapsed)
		{
			if (lightingType < 0 || lightingType >= TimeLogger.lightingTimes.Length)
			{
				return;
			}
			TimeLogger.UpdateTime(TimeLogger.lightingTimes, lightingType, timeElapsed);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x003ED676 File Offset: 0x003EB876
		public static float GetLightingTime(int lightingType)
		{
			return TimeLogger.lightingTimes[lightingType].time;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x003ED688 File Offset: 0x003EB888
		public static float GetLightingTotal()
		{
			float num = 0f;
			for (int i = 0; i < TimeLogger.lightingTimes.Length; i++)
			{
				num += TimeLogger.lightingTimes[i].time;
			}
			return num;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x003ED6C4 File Offset: 0x003EB8C4
		public static void DetailedDrawReset()
		{
			TimeLogger.lastDetailedDrawTime = TimeLogger.detailedDrawTimer.Elapsed.TotalMilliseconds;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x003ED6E8 File Offset: 0x003EB8E8
		public static void DetailedDrawTime(int detailedDrawType)
		{
			if (detailedDrawType < 0 || detailedDrawType >= TimeLogger.detailedDrawTimes.Length)
			{
				return;
			}
			double totalMilliseconds = TimeLogger.detailedDrawTimer.Elapsed.TotalMilliseconds;
			double time = totalMilliseconds - TimeLogger.lastDetailedDrawTime;
			TimeLogger.lastDetailedDrawTime = totalMilliseconds;
			TimeLogger.UpdateTime(TimeLogger.detailedDrawTimes, detailedDrawType, time);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x003ED72F File Offset: 0x003EB92F
		public static float GetDetailedDrawTime(int detailedDrawType)
		{
			return TimeLogger.detailedDrawTimes[detailedDrawType].time;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x003ED744 File Offset: 0x003EB944
		public static float GetDetailedDrawTotal()
		{
			float num = 0f;
			for (int i = 0; i < TimeLogger.detailedDrawTimes.Length; i++)
			{
				if (TimeLogger.detailedDrawTimes[i].usedLastDraw)
				{
					num += TimeLogger.detailedDrawTimes[i].time;
				}
			}
			return num;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x003ED78F File Offset: 0x003EB98F
		public static void MenuDrawTime(double timeElapsed)
		{
			if (TimeLogger.currentlyLogging)
			{
				TimeLogger.logBuilder.AppendLine(string.Format("Menu Render Time : {0:F4}", timeElapsed));
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x003ED7B3 File Offset: 0x003EB9B3
		public static void SplashDrawTime(double timeElapsed)
		{
			if (TimeLogger.currentlyLogging)
			{
				TimeLogger.logBuilder.AppendLine(string.Format("Splash Render Time : {0:F4}", timeElapsed));
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x003ED7D7 File Offset: 0x003EB9D7
		public static void MapDrawTime(double timeElapsed)
		{
			if (TimeLogger.currentlyLogging)
			{
				TimeLogger.logBuilder.AppendLine(string.Format("Full Screen Map Render Time : {0:F4}", timeElapsed));
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x003ED7FB File Offset: 0x003EB9FB
		public static void DrawException(Exception e)
		{
			if (TimeLogger.currentlyLogging)
			{
				TimeLogger.logBuilder.AppendLine(e.ToString());
			}
		}

		// Token: 0x04000D9C RID: 3484
		private static StreamWriter logWriter;

		// Token: 0x04000D9D RID: 3485
		private static StringBuilder logBuilder;

		// Token: 0x04000D9E RID: 3486
		private static int framesToLog;

		// Token: 0x04000D9F RID: 3487
		private static int currentFrame;

		// Token: 0x04000DA0 RID: 3488
		private static bool startLoggingNextFrame;

		// Token: 0x04000DA1 RID: 3489
		private static bool endLoggingThisFrame;

		// Token: 0x04000DA2 RID: 3490
		private static bool currentlyLogging;

		// Token: 0x04000DA3 RID: 3491
		private static Stopwatch detailedDrawTimer;

		// Token: 0x04000DA4 RID: 3492
		private static double lastDetailedDrawTime;

		// Token: 0x04000DA5 RID: 3493
		private static TimeLogger.TimeLogData[] renderTimes;

		// Token: 0x04000DA6 RID: 3494
		private static TimeLogger.TimeLogData[] drawTimes;

		// Token: 0x04000DA7 RID: 3495
		private static TimeLogger.TimeLogData[] lightingTimes;

		// Token: 0x04000DA8 RID: 3496
		private static TimeLogger.TimeLogData[] detailedDrawTimes;

		// Token: 0x04000DA9 RID: 3497
		private const int maxTimeDelay = 100;

		// Token: 0x020004DB RID: 1243
		private struct TimeLogData
		{
			// Token: 0x04005705 RID: 22277
			public float time;

			// Token: 0x04005706 RID: 22278
			public float timeMax;

			// Token: 0x04005707 RID: 22279
			public int resetMaxTime;

			// Token: 0x04005708 RID: 22280
			public bool usedLastDraw;

			// Token: 0x04005709 RID: 22281
			public string logText;
		}
	}
}
