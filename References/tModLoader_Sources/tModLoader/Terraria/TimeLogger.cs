using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;

namespace Terraria
{
	// Token: 0x02000066 RID: 102
	public static class TimeLogger
	{
		// Token: 0x06001001 RID: 4097 RVA: 0x003FF5EC File Offset: 0x003FD7EC
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
				TimeLogger.TimeLogData[] array = TimeLogger.renderTimes;
				int num = i;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Render #");
				defaultInterpolatedStringHandler.AppendFormatted<int>(i);
				array[num].logText = defaultInterpolatedStringHandler.ToStringAndClear();
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
				TimeLogger.TimeLogData[] array2 = TimeLogger.lightingTimes;
				int num2 = j;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Lighting Pass #");
				defaultInterpolatedStringHandler.AppendFormatted<int>(j);
				array2[num2].logText = defaultInterpolatedStringHandler.ToStringAndClear();
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
					TimeLogger.TimeLogData[] array3 = TimeLogger.detailedDrawTimes;
					int num3 = k;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Unnamed detailed draw #");
					defaultInterpolatedStringHandler.AppendFormatted<int>(k);
					array3[num3].logText = defaultInterpolatedStringHandler.ToStringAndClear();
				}
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x003FFAF7 File Offset: 0x003FDCF7
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

		// Token: 0x06001003 RID: 4099 RVA: 0x003FFB30 File Offset: 0x003FDD30
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
				StringBuilder stringBuilder = TimeLogger.logBuilder;
				StringBuilder stringBuilder2 = stringBuilder;
				StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(16, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("Start of Frame #");
				appendInterpolatedStringHandler.AppendFormatted<int>(TimeLogger.currentFrame);
				stringBuilder2.AppendLine(ref appendInterpolatedStringHandler);
			}
			TimeLogger.detailedDrawTimer.Restart();
			TimeLogger.lastDetailedDrawTime = TimeLogger.detailedDrawTimer.Elapsed.TotalMilliseconds;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x003FFC98 File Offset: 0x003FDE98
		public static void EndDrawFrame()
		{
			if (TimeLogger.currentFrame <= TimeLogger.framesToLog)
			{
				StringBuilder stringBuilder = TimeLogger.logBuilder;
				StringBuilder stringBuilder2 = stringBuilder;
				StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(14, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("End of Frame #");
				appendInterpolatedStringHandler.AppendFormatted<int>(TimeLogger.currentFrame);
				stringBuilder2.AppendLine(ref appendInterpolatedStringHandler);
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

		// Token: 0x06001005 RID: 4101 RVA: 0x003FFDD0 File Offset: 0x003FDFD0
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

		// Token: 0x06001006 RID: 4102 RVA: 0x003FFE98 File Offset: 0x003FE098
		public static void RenderTime(int renderType, double timeElapsed)
		{
			if (renderType >= 0 && renderType < TimeLogger.renderTimes.Length)
			{
				TimeLogger.UpdateTime(TimeLogger.renderTimes, renderType, timeElapsed);
			}
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x003FFEB4 File Offset: 0x003FE0B4
		public static float GetRenderTime(int renderType)
		{
			return TimeLogger.renderTimes[renderType].time;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x003FFEC6 File Offset: 0x003FE0C6
		public static float GetRenderMax(int renderType)
		{
			return TimeLogger.renderTimes[renderType].timeMax;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x003FFED8 File Offset: 0x003FE0D8
		public static void DrawTime(int drawType, double timeElapsed)
		{
			if (drawType >= 0 && drawType < TimeLogger.drawTimes.Length)
			{
				TimeLogger.UpdateTime(TimeLogger.drawTimes, drawType, timeElapsed);
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x003FFEF4 File Offset: 0x003FE0F4
		public static float GetDrawTime(int drawType)
		{
			return TimeLogger.drawTimes[drawType].time;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x003FFF08 File Offset: 0x003FE108
		public static float GetDrawTotal()
		{
			float num = 0f;
			for (int i = 0; i < TimeLogger.drawTimes.Length; i++)
			{
				num += TimeLogger.drawTimes[i].time;
			}
			return num;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x003FFF41 File Offset: 0x003FE141
		public static void LightingTime(int lightingType, double timeElapsed)
		{
			if (lightingType >= 0 && lightingType < TimeLogger.lightingTimes.Length)
			{
				TimeLogger.UpdateTime(TimeLogger.lightingTimes, lightingType, timeElapsed);
			}
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x003FFF5D File Offset: 0x003FE15D
		public static float GetLightingTime(int lightingType)
		{
			return TimeLogger.lightingTimes[lightingType].time;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x003FFF70 File Offset: 0x003FE170
		public static float GetLightingTotal()
		{
			float num = 0f;
			for (int i = 0; i < TimeLogger.lightingTimes.Length; i++)
			{
				num += TimeLogger.lightingTimes[i].time;
			}
			return num;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x003FFFAC File Offset: 0x003FE1AC
		public static void DetailedDrawReset()
		{
			TimeLogger.lastDetailedDrawTime = TimeLogger.detailedDrawTimer.Elapsed.TotalMilliseconds;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x003FFFD0 File Offset: 0x003FE1D0
		public static void DetailedDrawTime(int detailedDrawType)
		{
			if (detailedDrawType >= 0 && detailedDrawType < TimeLogger.detailedDrawTimes.Length)
			{
				double totalMilliseconds = TimeLogger.detailedDrawTimer.Elapsed.TotalMilliseconds;
				double time = totalMilliseconds - TimeLogger.lastDetailedDrawTime;
				TimeLogger.lastDetailedDrawTime = totalMilliseconds;
				TimeLogger.UpdateTime(TimeLogger.detailedDrawTimes, detailedDrawType, time);
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00400016 File Offset: 0x003FE216
		public static float GetDetailedDrawTime(int detailedDrawType)
		{
			return TimeLogger.detailedDrawTimes[detailedDrawType].time;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00400028 File Offset: 0x003FE228
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

		// Token: 0x06001013 RID: 4115 RVA: 0x00400074 File Offset: 0x003FE274
		public static void MenuDrawTime(double timeElapsed)
		{
			if (TimeLogger.currentlyLogging)
			{
				StringBuilder stringBuilder = TimeLogger.logBuilder;
				StringBuilder stringBuilder2 = stringBuilder;
				StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(19, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("Menu Render Time : ");
				appendInterpolatedStringHandler.AppendFormatted<double>(timeElapsed, "F4");
				stringBuilder2.AppendLine(ref appendInterpolatedStringHandler);
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x004000BC File Offset: 0x003FE2BC
		public static void SplashDrawTime(double timeElapsed)
		{
			if (TimeLogger.currentlyLogging)
			{
				StringBuilder stringBuilder = TimeLogger.logBuilder;
				StringBuilder stringBuilder2 = stringBuilder;
				StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(21, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("Splash Render Time : ");
				appendInterpolatedStringHandler.AppendFormatted<double>(timeElapsed, "F4");
				stringBuilder2.AppendLine(ref appendInterpolatedStringHandler);
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00400104 File Offset: 0x003FE304
		public static void MapDrawTime(double timeElapsed)
		{
			if (TimeLogger.currentlyLogging)
			{
				StringBuilder stringBuilder = TimeLogger.logBuilder;
				StringBuilder stringBuilder2 = stringBuilder;
				StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(30, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("Full Screen Map Render Time : ");
				appendInterpolatedStringHandler.AppendFormatted<double>(timeElapsed, "F4");
				stringBuilder2.AppendLine(ref appendInterpolatedStringHandler);
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0040014B File Offset: 0x003FE34B
		public static void DrawException(Exception e)
		{
			if (TimeLogger.currentlyLogging)
			{
				TimeLogger.logBuilder.AppendLine(e.ToString());
			}
		}

		// Token: 0x04000EE7 RID: 3815
		private static StreamWriter logWriter;

		// Token: 0x04000EE8 RID: 3816
		private static StringBuilder logBuilder;

		// Token: 0x04000EE9 RID: 3817
		private static int framesToLog;

		// Token: 0x04000EEA RID: 3818
		private static int currentFrame;

		// Token: 0x04000EEB RID: 3819
		private static bool startLoggingNextFrame;

		// Token: 0x04000EEC RID: 3820
		private static bool endLoggingThisFrame;

		// Token: 0x04000EED RID: 3821
		private static bool currentlyLogging;

		// Token: 0x04000EEE RID: 3822
		private static Stopwatch detailedDrawTimer;

		// Token: 0x04000EEF RID: 3823
		private static double lastDetailedDrawTime;

		// Token: 0x04000EF0 RID: 3824
		private static TimeLogger.TimeLogData[] renderTimes;

		// Token: 0x04000EF1 RID: 3825
		private static TimeLogger.TimeLogData[] drawTimes;

		// Token: 0x04000EF2 RID: 3826
		private static TimeLogger.TimeLogData[] lightingTimes;

		// Token: 0x04000EF3 RID: 3827
		private static TimeLogger.TimeLogData[] detailedDrawTimes;

		// Token: 0x04000EF4 RID: 3828
		private const int maxTimeDelay = 100;

		// Token: 0x020007F2 RID: 2034
		private struct TimeLogData
		{
			// Token: 0x040067A9 RID: 26537
			public float time;

			// Token: 0x040067AA RID: 26538
			public float timeMax;

			// Token: 0x040067AB RID: 26539
			public int resetMaxTime;

			// Token: 0x040067AC RID: 26540
			public bool usedLastDraw;

			// Token: 0x040067AD RID: 26541
			public string logText;
		}
	}
}
