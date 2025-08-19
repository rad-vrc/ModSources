using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ReLogic.IO;
using ReLogic.OS;
using Terraria.Initializers;
using Terraria.Localization;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x02000047 RID: 71
	public static class Program
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0033E1E7 File Offset: 0x0033C3E7
		public static float LoadedPercentage
		{
			get
			{
				if (Program.ThingsToLoad == 0)
				{
					return 1f;
				}
				return (float)Program.ThingsLoaded / (float)Program.ThingsToLoad;
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0033E203 File Offset: 0x0033C403
		public static void StartForceLoad()
		{
			if (!Main.SkipAssemblyLoad)
			{
				new Thread(new ParameterizedThreadStart(Program.ForceLoadThread))
				{
					IsBackground = true
				}.Start();
				return;
			}
			Program.LoadedEverything = true;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0033E230 File Offset: 0x0033C430
		public static void ForceLoadThread(object threadContext)
		{
			Program.ForceLoadAssembly(Assembly.GetExecutingAssembly(), true);
			Program.LoadedEverything = true;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0033E244 File Offset: 0x0033C444
		private static void ForceJITOnAssembly(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypes())
			{
				foreach (MethodInfo methodInfo in Program.IsMono ? type.GetMethods() : type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (!methodInfo.IsAbstract && !methodInfo.ContainsGenericParameters && methodInfo.GetMethodBody() != null)
					{
						if (Program.IsMono)
						{
							Program.JitForcedMethodCache = methodInfo.MethodHandle.GetFunctionPointer();
						}
						else
						{
							RuntimeHelpers.PrepareMethod(methodInfo.MethodHandle);
						}
					}
				}
				Program.ThingsLoaded++;
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0033E2F4 File Offset: 0x0033C4F4
		private static void ForceStaticInitializers(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypes())
			{
				if (!type.IsGenericType)
				{
					RuntimeHelpers.RunClassConstructor(type.TypeHandle);
				}
			}
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0033E32D File Offset: 0x0033C52D
		private static void ForceLoadAssembly(Assembly assembly, bool initializeStaticMembers)
		{
			Program.ThingsToLoad = assembly.GetTypes().Length;
			Program.ForceJITOnAssembly(assembly);
			if (initializeStaticMembers)
			{
				Program.ForceStaticInitializers(assembly);
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0033E34C File Offset: 0x0033C54C
		private static void ForceLoadAssembly(string name, bool initializeStaticMembers)
		{
			Assembly assembly = null;
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				if (assemblies[i].GetName().Name.Equals(name))
				{
					assembly = assemblies[i];
					break;
				}
			}
			if (assembly == null)
			{
				assembly = Assembly.Load(name);
			}
			Program.ForceLoadAssembly(assembly, initializeStaticMembers);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0033E3A8 File Offset: 0x0033C5A8
		private static void SetupLogging()
		{
			if (Program.LaunchParameters.ContainsKey("-logfile"))
			{
				string text = Program.LaunchParameters["-logfile"];
				if (text == null || text.Trim() == "")
				{
					text = Path.Combine(Program.SavePath, "Logs", string.Format("Log_{0:yyyyMMddHHmmssfff}.log", DateTime.Now));
				}
				else
				{
					text = Path.Combine(text, string.Format("Log_{0:yyyyMMddHHmmssfff}.log", DateTime.Now));
				}
				ConsoleOutputMirror.ToFile(text);
			}
			CrashWatcher.Inititialize();
			CrashWatcher.DumpOnException = Program.LaunchParameters.ContainsKey("-minidump");
			CrashWatcher.LogAllExceptions = Program.LaunchParameters.ContainsKey("-logerrors");
			if (Program.LaunchParameters.ContainsKey("-fulldump"))
			{
				CrashDump.Options options = CrashDump.Options.WithFullMemory;
				Console.WriteLine("Full Dump logs enabled.");
				CrashWatcher.EnableCrashDumps(options);
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0033E480 File Offset: 0x0033C680
		private static void InitializeConsoleOutput()
		{
			if (Debugger.IsAttached)
			{
				return;
			}
			try
			{
				Console.OutputEncoding = Encoding.UTF8;
				if (Platform.IsWindows)
				{
					Console.InputEncoding = Encoding.Unicode;
				}
				else
				{
					Console.InputEncoding = Encoding.UTF8;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0033E4D4 File Offset: 0x0033C6D4
		public static void LaunchGame(string[] args, bool monoArgs = false)
		{
			Thread.CurrentThread.Name = "Main Thread";
			if (monoArgs)
			{
				args = Utils.ConvertMonoArgsToDotNet(args);
			}
			if (Program.IsFna)
			{
				Program.TrySettingFNAToOpenGL(args);
			}
			Program.LaunchParameters = Utils.ParseArguements(args);
			Program.SavePath = (Program.LaunchParameters.ContainsKey("-savedirectory") ? Program.LaunchParameters["-savedirectory"] : Platform.Get<IPathService>().GetStoragePath("Terraria"));
			ThreadPool.SetMinThreads(8, 8);
			Program.InitializeConsoleOutput();
			Program.SetupLogging();
			Platform.Get<IWindowService>().SetQuickEditEnabled(false);
			Program.RunGame();
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0033E56C File Offset: 0x0033C76C
		public static void RunGame()
		{
			LanguageManager.Instance.SetLanguage(GameCulture.DefaultCulture);
			if (Platform.IsOSX)
			{
				Main.OnEngineLoad += delegate()
				{
					Main.instance.IsMouseVisible = false;
				};
			}
			using (Main main = new Main())
			{
				try
				{
					Lang.InitializeLegacyLocalization();
					SocialAPI.Initialize(null);
					LaunchInitializer.LoadParameters(main);
					Main.OnEnginePreload += Program.StartForceLoad;
					if (Main.dedServ)
					{
						main.DedServ();
					}
					main.Run();
				}
				catch (Exception e)
				{
					Program.DisplayException(e);
				}
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0033E628 File Offset: 0x0033C828
		private static void TrySettingFNAToOpenGL(string[] args)
		{
			bool flag = false;
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].Contains("gldevice"))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Environment.SetEnvironmentVariable("FNA3D_FORCE_DRIVER", "OpenGL");
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0033E66C File Offset: 0x0033C86C
		private static void DisplayException(Exception e)
		{
			try
			{
				string text = e.ToString();
				if (WorldGen.gen)
				{
					try
					{
						text = string.Format("Creating world - Seed: {0} Width: {1}, Height: {2}, Evil: {3}, IsExpert: {4}\n{5}", new object[]
						{
							Main.ActiveWorldFileData.Seed,
							Main.maxTilesX,
							Main.maxTilesY,
							WorldGen.WorldGenParam_Evil,
							Main.expertMode,
							text
						});
					}
					catch
					{
					}
				}
				using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", true))
				{
					streamWriter.WriteLine(DateTime.Now);
					streamWriter.WriteLine(text);
					streamWriter.WriteLine("");
				}
				if (Main.dedServ)
				{
					Console.WriteLine(Language.GetTextValue("Error.ServerCrash"), DateTime.Now, text);
				}
				MessageBox.Show(text, "Terraria: Error");
			}
			catch
			{
			}
		}

		// Token: 0x04000922 RID: 2338
		public static bool IsXna = true;

		// Token: 0x04000923 RID: 2339
		public static bool IsFna = false;

		// Token: 0x04000924 RID: 2340
		public static bool IsMono = Type.GetType("Mono.Runtime") != null;

		// Token: 0x04000925 RID: 2341
		public const bool IsDebug = false;

		// Token: 0x04000926 RID: 2342
		public static Dictionary<string, string> LaunchParameters = new Dictionary<string, string>();

		// Token: 0x04000927 RID: 2343
		public static string SavePath;

		// Token: 0x04000928 RID: 2344
		public const string TerrariaSaveFolderPath = "Terraria";

		// Token: 0x04000929 RID: 2345
		private static int ThingsToLoad;

		// Token: 0x0400092A RID: 2346
		private static int ThingsLoaded;

		// Token: 0x0400092B RID: 2347
		public static bool LoadedEverything;

		// Token: 0x0400092C RID: 2348
		public static IntPtr JitForcedMethodCache;
	}
}
