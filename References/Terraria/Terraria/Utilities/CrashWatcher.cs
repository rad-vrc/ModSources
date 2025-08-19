using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Terraria.Utilities
{
	// Token: 0x02000142 RID: 322
	public static class CrashWatcher
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x004DEFD2 File Offset: 0x004DD1D2
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x004DEFD9 File Offset: 0x004DD1D9
		public static bool LogAllExceptions { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x004DEFE1 File Offset: 0x004DD1E1
		// (set) Token: 0x060018C6 RID: 6342 RVA: 0x004DEFE8 File Offset: 0x004DD1E8
		public static bool DumpOnException { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x004DEFF0 File Offset: 0x004DD1F0
		// (set) Token: 0x060018C8 RID: 6344 RVA: 0x004DEFF7 File Offset: 0x004DD1F7
		public static bool DumpOnCrash { get; private set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x004DEFFF File Offset: 0x004DD1FF
		// (set) Token: 0x060018CA RID: 6346 RVA: 0x004DF006 File Offset: 0x004DD206
		public static CrashDump.Options CrashDumpOptions { get; private set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x004DF00E File Offset: 0x004DD20E
		private static string DumpPath
		{
			get
			{
				return Path.Combine(Main.SavePath, "Dumps");
			}
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x004DF020 File Offset: 0x004DD220
		public static void Inititialize()
		{
			Console.WriteLine("Error Logging Enabled.");
			AppDomain.CurrentDomain.FirstChanceException += delegate(object sender, FirstChanceExceptionEventArgs exceptionArgs)
			{
				if (CrashWatcher.LogAllExceptions && !false)
				{
					string text = CrashWatcher.PrintException(exceptionArgs.Exception);
					Console.Write("================\r\n" + string.Format("{0}: First-Chance Exception\r\nThread: {1} [{2}]\r\nCulture: {3}\r\nException: {4}\r\n", new object[]
					{
						DateTime.Now,
						Thread.CurrentThread.ManagedThreadId,
						Thread.CurrentThread.Name,
						Thread.CurrentThread.CurrentCulture.Name,
						text
					}) + "================\r\n\r\n");
				}
			};
			AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs exceptionArgs)
			{
				string text = CrashWatcher.PrintException((Exception)exceptionArgs.ExceptionObject);
				Console.Write("================\r\n" + string.Format("{0}: Unhandled Exception\r\nThread: {1} [{2}]\r\nCulture: {3}\r\nException: {4}\r\n", new object[]
				{
					DateTime.Now,
					Thread.CurrentThread.ManagedThreadId,
					Thread.CurrentThread.Name,
					Thread.CurrentThread.CurrentCulture.Name,
					text
				}) + "================\r\n");
				if (CrashWatcher.DumpOnCrash)
				{
					CrashDump.WriteException(CrashWatcher.CrashDumpOptions, CrashWatcher.DumpPath);
				}
			};
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x004DF08C File Offset: 0x004DD28C
		private static string PrintException(Exception ex)
		{
			string text = ex.ToString();
			try
			{
				int num = (int)typeof(Exception).GetProperty("HResult", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetGetMethod(true).Invoke(ex, null);
				if (num != 0)
				{
					text = text + "\nHResult: " + num;
				}
			}
			catch
			{
			}
			if (ex is ReflectionTypeLoadException)
			{
				foreach (Exception ex2 in ((ReflectionTypeLoadException)ex).LoaderExceptions)
				{
					text = text + "\n+--> " + CrashWatcher.PrintException(ex2);
				}
			}
			return text;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x004DF130 File Offset: 0x004DD330
		public static void EnableCrashDumps(CrashDump.Options options)
		{
			CrashWatcher.DumpOnCrash = true;
			CrashWatcher.CrashDumpOptions = options;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x004DF13E File Offset: 0x004DD33E
		public static void DisableCrashDumps()
		{
			CrashWatcher.DumpOnCrash = false;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		[Conditional("DEBUG")]
		private static void HookDebugExceptionDialog()
		{
		}
	}
}
