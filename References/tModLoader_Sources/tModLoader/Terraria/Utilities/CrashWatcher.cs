using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Terraria.Utilities
{
	// Token: 0x0200008B RID: 139
	public static class CrashWatcher
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x004A181F File Offset: 0x0049FA1F
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x004A1826 File Offset: 0x0049FA26
		public static bool LogAllExceptions { get; set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x004A182E File Offset: 0x0049FA2E
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x004A1835 File Offset: 0x0049FA35
		public static bool DumpOnException { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x004A183D File Offset: 0x0049FA3D
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x004A1844 File Offset: 0x0049FA44
		public static bool DumpOnCrash { get; private set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x004A184C File Offset: 0x0049FA4C
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x004A1853 File Offset: 0x0049FA53
		public static CrashDump.Options CrashDumpOptions { get; private set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x004A185B File Offset: 0x0049FA5B
		private static string DumpPath
		{
			get
			{
				return Path.Combine(Main.SavePath, "Dumps");
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x004A186C File Offset: 0x0049FA6C
		public static void Inititialize()
		{
			Console.WriteLine("Error Logging Enabled.");
			AppDomain.CurrentDomain.FirstChanceException += delegate(object sender, FirstChanceExceptionEventArgs exceptionArgs)
			{
				if (CrashWatcher.LogAllExceptions)
				{
					string text2 = CrashWatcher.PrintException(exceptionArgs.Exception);
					string str = "================\r\n";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(63, 5);
					defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now);
					defaultInterpolatedStringHandler.AppendLiteral(": First-Chance Exception\r\nThread: ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(Thread.CurrentThread.ManagedThreadId);
					defaultInterpolatedStringHandler.AppendLiteral(" [");
					defaultInterpolatedStringHandler.AppendFormatted(Thread.CurrentThread.Name);
					defaultInterpolatedStringHandler.AppendLiteral("]\r\nCulture: ");
					defaultInterpolatedStringHandler.AppendFormatted(Thread.CurrentThread.CurrentCulture.Name);
					defaultInterpolatedStringHandler.AppendLiteral("\r\nException: ");
					defaultInterpolatedStringHandler.AppendFormatted(text2);
					defaultInterpolatedStringHandler.AppendLiteral("\r\n");
					Console.Write(str + defaultInterpolatedStringHandler.ToStringAndClear() + "================\r\n\r\n");
				}
			};
			AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs exceptionArgs)
			{
				string text = CrashWatcher.PrintException((Exception)exceptionArgs.ExceptionObject);
				string str = "================\r\n";
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(60, 5);
				defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now);
				defaultInterpolatedStringHandler.AppendLiteral(": Unhandled Exception\r\nThread: ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(Thread.CurrentThread.ManagedThreadId);
				defaultInterpolatedStringHandler.AppendLiteral(" [");
				defaultInterpolatedStringHandler.AppendFormatted(Thread.CurrentThread.Name);
				defaultInterpolatedStringHandler.AppendLiteral("]\r\nCulture: ");
				defaultInterpolatedStringHandler.AppendFormatted(Thread.CurrentThread.CurrentCulture.Name);
				defaultInterpolatedStringHandler.AppendLiteral("\r\nException: ");
				defaultInterpolatedStringHandler.AppendFormatted(text);
				defaultInterpolatedStringHandler.AppendLiteral("\r\n");
				Console.Write(str + defaultInterpolatedStringHandler.ToStringAndClear() + "================\r\n");
				if (CrashWatcher.DumpOnCrash)
				{
					CrashDump.WriteException(CrashWatcher.CrashDumpOptions, CrashWatcher.DumpPath);
				}
			};
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x004A18D8 File Offset: 0x0049FAD8
		private static string PrintException(Exception ex)
		{
			string text = ex.ToString();
			try
			{
				int num = (int)typeof(Exception).GetProperty("HResult", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetGetMethod(true).Invoke(ex, null);
				if (num != 0)
				{
					text = text + "\nHResult: " + num.ToString();
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

		// Token: 0x0600144D RID: 5197 RVA: 0x004A197C File Offset: 0x0049FB7C
		public static void EnableCrashDumps(CrashDump.Options options)
		{
			CrashWatcher.DumpOnCrash = true;
			CrashWatcher.CrashDumpOptions = options;
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x004A198A File Offset: 0x0049FB8A
		public static void DisableCrashDumps()
		{
			CrashWatcher.DumpOnCrash = false;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x004A1992 File Offset: 0x0049FB92
		[Conditional("DEBUG")]
		private static void HookDebugExceptionDialog()
		{
		}
	}
}
