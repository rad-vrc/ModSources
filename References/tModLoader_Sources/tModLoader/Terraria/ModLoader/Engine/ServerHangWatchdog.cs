using System;
using System.Diagnostics;
using System.Threading;
using log4net.Core;

namespace Terraria.ModLoader.Engine
{
	// Token: 0x020002B9 RID: 697
	internal static class ServerHangWatchdog
	{
		// Token: 0x06002D42 RID: 11586 RVA: 0x0052D308 File Offset: 0x0052B508
		internal static void Checkin()
		{
			if (Debugger.IsAttached)
			{
				return;
			}
			bool flag = ServerHangWatchdog.lastCheckin != null;
			ServerHangWatchdog.lastCheckin = new Ref<DateTime>(DateTime.Now);
			if (!flag)
			{
				ServerHangWatchdog.Start();
			}
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x0052D334 File Offset: 0x0052B534
		private static void Start()
		{
			Thread mainThread = Thread.CurrentThread;
			new Thread(delegate()
			{
				ServerHangWatchdog.Run(mainThread);
			})
			{
				Name = "Server Hang Watchdog",
				IsBackground = true
			}.Start();
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x0052D370 File Offset: 0x0052B570
		private static void Run(Thread mainThread)
		{
			for (;;)
			{
				Thread.Sleep(1000);
				if (DateTime.Now - ServerHangWatchdog.lastCheckin.Value > ServerHangWatchdog.TIMEOUT)
				{
					Logging.ServerConsoleLine("Server hung for more than 10 seconds. Cannot determine cause from watchdog thread", Level.Warn, null, Logging.tML);
					ServerHangWatchdog.Checkin();
				}
			}
		}

		// Token: 0x04001C3B RID: 7227
		public static readonly TimeSpan TIMEOUT = TimeSpan.FromSeconds(10.0);

		// Token: 0x04001C3C RID: 7228
		private static volatile Ref<DateTime> lastCheckin;
	}
}
