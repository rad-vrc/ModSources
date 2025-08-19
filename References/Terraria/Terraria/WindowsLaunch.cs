using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Terraria.Social;

namespace Terraria
{
	// Token: 0x02000020 RID: 32
	public static class WindowsLaunch
	{
		// Token: 0x0600015F RID: 351 RVA: 0x0000EF74 File Offset: 0x0000D174
		private static bool ConsoleCtrlCheck(WindowsLaunch.CtrlTypes ctrlType)
		{
			bool flag = false;
			switch (ctrlType)
			{
			case WindowsLaunch.CtrlTypes.CTRL_C_EVENT:
				flag = true;
				break;
			case WindowsLaunch.CtrlTypes.CTRL_BREAK_EVENT:
				flag = true;
				break;
			case WindowsLaunch.CtrlTypes.CTRL_CLOSE_EVENT:
				flag = true;
				break;
			case WindowsLaunch.CtrlTypes.CTRL_LOGOFF_EVENT:
			case WindowsLaunch.CtrlTypes.CTRL_SHUTDOWN_EVENT:
				flag = true;
				break;
			}
			if (flag)
			{
				SocialAPI.Shutdown();
			}
			return true;
		}

		// Token: 0x06000160 RID: 352
		[DllImport("Kernel32")]
		public static extern bool SetConsoleCtrlHandler(WindowsLaunch.HandlerRoutine handler, bool add);

		// Token: 0x06000161 RID: 353 RVA: 0x0000EFBE File Offset: 0x0000D1BE
		[STAThread]
		private static void Main(string[] args)
		{
			AppDomain.CurrentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs sargs)
			{
				string resourceName = new AssemblyName(sargs.Name).Name + ".dll";
				string text = Array.Find<string>(typeof(Program).Assembly.GetManifestResourceNames(), (string element) => element.EndsWith(resourceName));
				if (text == null)
				{
					return null;
				}
				Assembly result;
				using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(text))
				{
					byte[] array = new byte[manifestResourceStream.Length];
					manifestResourceStream.Read(array, 0, array.Length);
					result = Assembly.Load(array);
				}
				return result;
			};
			Program.LaunchGame(args, false);
		}

		// Token: 0x040000FF RID: 255
		private static WindowsLaunch.HandlerRoutine _handleRoutine;

		// Token: 0x0200049B RID: 1179
		// (Invoke) Token: 0x06002EAA RID: 11946
		public delegate bool HandlerRoutine(WindowsLaunch.CtrlTypes ctrlType);

		// Token: 0x0200049C RID: 1180
		public enum CtrlTypes
		{
			// Token: 0x040055BF RID: 21951
			CTRL_C_EVENT,
			// Token: 0x040055C0 RID: 21952
			CTRL_BREAK_EVENT,
			// Token: 0x040055C1 RID: 21953
			CTRL_CLOSE_EVENT,
			// Token: 0x040055C2 RID: 21954
			CTRL_LOGOFF_EVENT = 5,
			// Token: 0x040055C3 RID: 21955
			CTRL_SHUTDOWN_EVENT
		}
	}
}
