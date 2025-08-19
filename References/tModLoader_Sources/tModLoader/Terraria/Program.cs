using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using log4net;
using Newtonsoft.Json;
using ReLogic.IO;
using ReLogic.OS;
using SDL2;
using Terraria.Initializers;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Engine;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x02000047 RID: 71
	public static class Program
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x002E2DDD File Offset: 0x002E0FDD
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x002E2DE4 File Offset: 0x002E0FE4
		public static Thread MainThread { get; private set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x002E2DEC File Offset: 0x002E0FEC
		public static bool IsMainThread
		{
			get
			{
				return Thread.CurrentThread == Program.MainThread;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x002E2DFA File Offset: 0x002E0FFA
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

		// Token: 0x06000CBA RID: 3258 RVA: 0x002E2E16 File Offset: 0x002E1016
		public static void StartForceLoad()
		{
			if (!Main.SkipAssemblyLoad)
			{
				ParameterizedThreadStart start;
				if ((start = Program.<>O.<0>__ForceLoadThread) == null)
				{
					start = (Program.<>O.<0>__ForceLoadThread = new ParameterizedThreadStart(Program.ForceLoadThread));
				}
				new Thread(start)
				{
					IsBackground = true
				}.Start();
				return;
			}
			Program.LoadedEverything = true;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x002E2E52 File Offset: 0x002E1052
		public static void ForceLoadThread(object threadContext)
		{
			Program.ForceLoadAssembly(Assembly.GetExecutingAssembly(), true);
			Program.LoadedEverything = true;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x002E2E68 File Offset: 0x002E1068
		private static void ForceJITOnAssembly(IEnumerable<Type> types)
		{
			IEnumerable<MethodInfo> methodsToJIT = Program.CollectMethodsToJIT(types);
			if (Environment.ProcessorCount > 1)
			{
				ParallelQuery<MethodInfo> source = methodsToJIT.AsParallel<MethodInfo>().AsUnordered<MethodInfo>();
				Action<MethodInfo> action;
				if ((action = Program.<>O.<1>__ForceJITOnMethod) == null)
				{
					action = (Program.<>O.<1>__ForceJITOnMethod = new Action<MethodInfo>(Program.ForceJITOnMethod));
				}
				source.ForAll(action);
				return;
			}
			foreach (MethodInfo method in methodsToJIT)
			{
				Program.ForceJITOnMethod(method);
			}
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x002E2EE8 File Offset: 0x002E10E8
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

		// Token: 0x06000CBE RID: 3262 RVA: 0x002E2F24 File Offset: 0x002E1124
		private static void ForceLoadAssembly(Assembly assembly, bool initializeStaticMembers)
		{
			Type[] types = assembly.GetTypes();
			Program.ThingsToLoad = (from type in types
			select Program.GetAllMethods(type).Count<MethodInfo>()).Sum();
			Program.ForceJITOnAssembly(types);
			if (initializeStaticMembers)
			{
				Program.ForceStaticInitializers(assembly);
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x002E2F74 File Offset: 0x002E1174
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

		// Token: 0x06000CC0 RID: 3264 RVA: 0x002E2FD0 File Offset: 0x002E11D0
		private static void SetupLogging()
		{
			if (Program.LaunchParameters.ContainsKey("-logfile"))
			{
				string text = Program.LaunchParameters["-logfile"];
				string text2;
				if (text == null || text.Trim() == "")
				{
					string savePath = Program.SavePath;
					string path = "Logs";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Log_");
					defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, "yyyyMMddHHmmssfff");
					defaultInterpolatedStringHandler.AppendLiteral(".log");
					text2 = Path.Combine(savePath, path, defaultInterpolatedStringHandler.ToStringAndClear());
				}
				else
				{
					string path2 = text;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Log_");
					defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, "yyyyMMddHHmmssfff");
					defaultInterpolatedStringHandler.AppendLiteral(".log");
					text2 = Path.Combine(path2, defaultInterpolatedStringHandler.ToStringAndClear());
				}
				text = text2;
				ConsoleOutputMirror.ToFile(text);
			}
			CrashWatcher.Inititialize();
			CrashWatcher.DumpOnException = Program.LaunchParameters.ContainsKey("-minidump");
			CrashWatcher.LogAllExceptions = Program.LaunchParameters.ContainsKey("-logerrors");
			if (Program.LaunchParameters.ContainsKey("-fulldump"))
			{
				Console.WriteLine("Full Dump logs enabled.");
				CrashWatcher.EnableCrashDumps(CrashDump.Options.WithFullMemory);
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x002E30F4 File Offset: 0x002E12F4
		private static void InitializeConsoleOutput()
		{
			if (Debugger.IsAttached)
			{
				return;
			}
			if (!Main.dedServ && !Program.LaunchParameters.ContainsKey("-console"))
			{
				Platform.Get<IWindowService>().HideConsole();
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

		// Token: 0x06000CC2 RID: 3266 RVA: 0x002E3168 File Offset: 0x002E1368
		public static void LaunchGame(string[] args, bool monoArgs = false)
		{
			Program.MainThread = Thread.CurrentThread;
			Program.MainThread.Name = "Main Thread";
			bool isServer;
			Program.ProcessLaunchArgs(args, monoArgs, out isServer);
			Program.StartupSequenceTml(isServer);
			Program.LaunchGame_(isServer);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x002E31A4 File Offset: 0x002E13A4
		public static void LaunchGame_(bool isServer)
		{
			Main.dedServ = isServer;
			if (isServer)
			{
				Main.myPlayer = 255;
			}
			string build = LaunchInitializer.TryParameter(new string[]
			{
				"-build"
			});
			if (build != null)
			{
				ModCompile.BuildModCommandLine(build);
			}
			if (Main.dedServ)
			{
				Environment.SetEnvironmentVariable("FNA_PLATFORM_BACKEND", "NONE");
			}
			ThreadPool.SetMinThreads(8, 8);
			Program.InitializeConsoleOutput();
			Platform.Get<IWindowService>().SetQuickEditEnabled(false);
			Program.RunGame();
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x002E3214 File Offset: 0x002E1414
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
			InstallVerifier.Startup();
			try
			{
				ModLoader.EngineInit();
				using (Main main = new Main())
				{
					Lang.InitializeLegacyLocalization();
					SocialAPI.Initialize(null);
					LaunchInitializer.LoadParameters(main);
					Action value;
					if ((value = Program.<>O.<2>__StartForceLoad) == null)
					{
						value = (Program.<>O.<2>__StartForceLoad = new Action(Program.StartForceLoad));
					}
					Main.OnEnginePreload += value;
					if (Main.dedServ)
					{
						main.DedServ();
					}
					else
					{
						main.Run();
					}
				}
			}
			catch (Exception e)
			{
				ErrorReporting.FatalExit("Main engine crash", e);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x002E32F4 File Offset: 0x002E14F4
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x002E32FB File Offset: 0x002E14FB
		public static string SavePathShared { get; private set; }

		// Token: 0x06000CC7 RID: 3271 RVA: 0x002E3303 File Offset: 0x002E1503
		private static IEnumerable<MethodInfo> GetAllMethods(Type type)
		{
			return type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x002E3310 File Offset: 0x002E1510
		private static IEnumerable<MethodInfo> CollectMethodsToJIT(IEnumerable<Type> types)
		{
			return from type in types
			from method in Program.GetAllMethods(type)
			where !method.IsAbstract && !method.ContainsGenericParameters && method.GetMethodBody() != null
			select method;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x002E33A9 File Offset: 0x002E15A9
		private static void ForceJITOnMethod(MethodInfo method)
		{
			RuntimeHelpers.PrepareMethod(method.MethodHandle);
			Interlocked.Increment(ref Program.ThingsLoaded);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x002E33C4 File Offset: 0x002E15C4
		private static void ForceStaticInitializers(Type[] types)
		{
			foreach (Type type in types)
			{
				if (!type.IsGenericType)
				{
					RuntimeHelpers.RunClassConstructor(type.TypeHandle);
				}
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x002E33F8 File Offset: 0x002E15F8
		public static string SaveFolderName
		{
			get
			{
				if (BuildInfo.IsStable)
				{
					return "tModLoader";
				}
				if (!BuildInfo.IsPreview)
				{
					return "tModLoader-dev";
				}
				return "tModLoader-preview";
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x002E341C File Offset: 0x002E161C
		private static void PortOldSaveDirectories(string savePath)
		{
			string oldBetas = Path.Combine(savePath, "ModLoader", "Beta");
			if (!Directory.Exists(oldBetas))
			{
				return;
			}
			Logging.tML.Info("Old tModLoader alpha folder \"" + oldBetas + "\" found, attempting folder migration");
			string newPath = Path.Combine(savePath, "tModLoader");
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			if (Directory.Exists(newPath))
			{
				ILog tML = Logging.tML;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(85, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Both \"");
				defaultInterpolatedStringHandler.AppendFormatted(oldBetas);
				defaultInterpolatedStringHandler.AppendLiteral("\" and \"");
				defaultInterpolatedStringHandler.AppendFormatted(newPath);
				defaultInterpolatedStringHandler.AppendLiteral("\" exist, assuming user launched old tModLoader alpha, aborting migration");
				tML.Warn(defaultInterpolatedStringHandler.ToStringAndClear());
				return;
			}
			ILog tML2 = Logging.tML;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 2);
			defaultInterpolatedStringHandler.AppendLiteral("Migrating from \"");
			defaultInterpolatedStringHandler.AppendFormatted(oldBetas);
			defaultInterpolatedStringHandler.AppendLiteral("\" to \"");
			defaultInterpolatedStringHandler.AppendFormatted(newPath);
			defaultInterpolatedStringHandler.AppendLiteral("\"");
			tML2.Info(defaultInterpolatedStringHandler.ToStringAndClear());
			Directory.Move(oldBetas, newPath);
			Logging.tML.Info("Old alpha folder to new location migration success");
			foreach (string subDir in new string[]
			{
				"Mod Reader",
				"Mod Sources",
				"Mod Configs"
			})
			{
				string newSaveOriginalSubDirPath = Path.Combine(newPath, subDir);
				if (Directory.Exists(newSaveOriginalSubDirPath))
				{
					string newSaveNewSubDirPath = Path.Combine(newPath, subDir.Replace(" ", ""));
					ILog tML3 = Logging.tML;
					defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Renaming from \"");
					defaultInterpolatedStringHandler.AppendFormatted(newSaveOriginalSubDirPath);
					defaultInterpolatedStringHandler.AppendLiteral("\" to \"");
					defaultInterpolatedStringHandler.AppendFormatted(newSaveNewSubDirPath);
					defaultInterpolatedStringHandler.AppendLiteral("\"");
					tML3.Info(defaultInterpolatedStringHandler.ToStringAndClear());
					Directory.Move(newSaveOriginalSubDirPath, newSaveNewSubDirPath);
				}
			}
			Logging.tML.Info("Folder Renames Success");
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x002E35F8 File Offset: 0x002E17F8
		private static void PortCommonFilesToStagingBranches(string savePath)
		{
			if (BuildInfo.IsStable)
			{
				return;
			}
			string releasePath = Path.Combine(savePath, "tModLoader");
			string newPath = Path.Combine(savePath, Program.SaveFolderName);
			if (Directory.Exists(releasePath) && !Directory.Exists(newPath))
			{
				Directory.CreateDirectory(newPath);
				Logging.tML.Info("Cloning common files from Stable to preview and dev.");
				if (File.Exists(Path.Combine(releasePath, "config.json")))
				{
					File.Copy(Path.Combine(releasePath, "config.json"), Path.Combine(newPath, "config.json"));
				}
				if (File.Exists(Path.Combine(releasePath, "input profiles.json")))
				{
					File.Copy(Path.Combine(releasePath, "input profiles.json"), Path.Combine(newPath, "input profiles.json"));
				}
			}
		}

		/// <summary>
		/// Super Save Path is the parent directory containing both folders. Usually Program.SavePath or Steam Cloud
		/// Source is of variety StableFolder, PreviewFolder... etc
		/// Destination is of variety StableFolder, PreviewFolder... etc
		/// maxVersionOfSource is used to determine if we even should port the files. Example: 1.4.3-Legacy has maxVersion of 2022.9
		/// isAtomicLockable could be expressed as CopyToNewlyCreatedDestinationFolderViaTempFolder if that makes more sense to the reader.
		/// </summary>
		// Token: 0x06000CCE RID: 3278 RVA: 0x002E36A8 File Offset: 0x002E18A8
		private static void PortFilesFromXtoY(string superSavePath, string source, string destination, string maxVersionOfSource, bool isCloud, bool isAtomicLockable, DateTime migrationDay)
		{
			string newFolderPath = Path.Combine(superSavePath, destination);
			string newFolderPathTemp = Path.Combine(superSavePath, destination + "-temp");
			string oldFolderPath = Path.Combine(superSavePath, source);
			string cloudName = isCloud ? "Steam Cloud" : "Local Files";
			if (!Directory.Exists(oldFolderPath))
			{
				return;
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			string path;
			if (!(maxVersionOfSource == "2022.9"))
			{
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 3);
				defaultInterpolatedStringHandler.AppendFormatted(maxVersionOfSource);
				defaultInterpolatedStringHandler.AppendFormatted(destination);
				defaultInterpolatedStringHandler.AppendLiteral("ported_");
				defaultInterpolatedStringHandler.AppendFormatted(cloudName);
				defaultInterpolatedStringHandler.AppendLiteral(".txt");
				path = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			else
			{
				path = "143ported_" + cloudName + ".txt";
			}
			string portFilePath = Path.Combine(superSavePath, destination, path);
			if ((isAtomicLockable && Directory.Exists(newFolderPath)) || (!isAtomicLockable && File.Exists(portFilePath)))
			{
				return;
			}
			if (newFolderPath.Contains("OneDrive"))
			{
				Logging.tML.Info("Ensuring OneDrive is running before starting to Migrate Files");
				try
				{
					string oneDrivePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft\\OneDrive\\OneDrive.exe");
					string oneDrivePath2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Microsoft OneDrive\\OneDrive.exe");
					ProcessStartInfo oneDriveInfo = new ProcessStartInfo
					{
						Arguments = "/background",
						UseShellExecute = false
					};
					if (File.Exists(oneDrivePath))
					{
						oneDriveInfo.FileName = oneDrivePath;
						Process.Start(oneDriveInfo);
					}
					else if (File.Exists(oneDrivePath2))
					{
						oneDriveInfo.FileName = oneDrivePath2;
						Process.Start(oneDriveInfo);
					}
					Thread.Sleep(3000);
				}
				catch
				{
				}
			}
			string sourceFolderConfig = Path.Combine(Program.LaunchParameters.ContainsKey("-savedirectory") ? Program.LaunchParameters["-savedirectory"] : Platform.Get<IPathService>().GetStoragePath("Terraria"), source, "config.json");
			if (!File.Exists(sourceFolderConfig))
			{
				Logging.tML.Info("No config.json found at " + sourceFolderConfig + "\nAssuming nothing to port");
				return;
			}
			string lastLaunchedTml = null;
			try
			{
				object lastLaunchedTmlObject;
				if (JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(sourceFolderConfig)).TryGetValue("LastLaunchedTModLoaderVersion", out lastLaunchedTmlObject))
				{
					lastLaunchedTml = (string)lastLaunchedTmlObject;
				}
			}
			catch (Exception e)
			{
				if (File.GetLastWriteTime(sourceFolderConfig) > migrationDay)
				{
					lastLaunchedTml = BuildInfo.tMLVersion.ToString();
				}
				else
				{
					Program.<PortFilesFromXtoY>g__PromptUserForNewestTMLVersionLaunched|45_0(ref lastLaunchedTml);
					if (string.IsNullOrEmpty(lastLaunchedTml))
					{
						e.HelpLink = "https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Usage-FAQ#configjson-corrupted";
						defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(64, 3);
						defaultInterpolatedStringHandler.AppendLiteral("Attempt to Port from \"");
						defaultInterpolatedStringHandler.AppendFormatted(oldFolderPath);
						defaultInterpolatedStringHandler.AppendLiteral("\" to \"");
						defaultInterpolatedStringHandler.AppendFormatted(newFolderPath);
						defaultInterpolatedStringHandler.AppendLiteral("\" aborted, the \"");
						defaultInterpolatedStringHandler.AppendFormatted(sourceFolderConfig);
						defaultInterpolatedStringHandler.AppendLiteral("\" file is corrupted.");
						ErrorReporting.FatalExit(defaultInterpolatedStringHandler.ToStringAndClear(), e);
					}
				}
			}
			Program.<PortFilesFromXtoY>g__PromptUserForNewestTMLVersionLaunched|45_0(ref lastLaunchedTml);
			if (string.IsNullOrEmpty(lastLaunchedTml))
			{
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(243, 3);
				defaultInterpolatedStringHandler.AppendLiteral("Attempt to Port from \"");
				defaultInterpolatedStringHandler.AppendFormatted(oldFolderPath);
				defaultInterpolatedStringHandler.AppendLiteral("\" to \"");
				defaultInterpolatedStringHandler.AppendFormatted(newFolderPath);
				defaultInterpolatedStringHandler.AppendLiteral("\" aborted, the \"");
				defaultInterpolatedStringHandler.AppendFormatted(sourceFolderConfig);
				defaultInterpolatedStringHandler.AppendLiteral("\" file is missing the \"LastLaunchedTModLoaderVersion\" entry. If porting is desired, follow the instructions at \"https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Usage-FAQ#manually-port\"");
				ErrorReporting.FatalExit(defaultInterpolatedStringHandler.ToStringAndClear());
				return;
			}
			if (new Version(lastLaunchedTml).MajorMinor() > new Version(maxVersionOfSource))
			{
				ILog tML = Logging.tML;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(61, 3);
				defaultInterpolatedStringHandler.AppendLiteral("Attempt to Port from \"");
				defaultInterpolatedStringHandler.AppendFormatted(oldFolderPath);
				defaultInterpolatedStringHandler.AppendLiteral("\" to \"");
				defaultInterpolatedStringHandler.AppendFormatted(newFolderPath);
				defaultInterpolatedStringHandler.AppendLiteral("\" aborted, \"");
				defaultInterpolatedStringHandler.AppendFormatted(lastLaunchedTml);
				defaultInterpolatedStringHandler.AppendLiteral("\" is a newer version.");
				tML.Info(defaultInterpolatedStringHandler.ToStringAndClear());
				return;
			}
			ILog tML2 = Logging.tML;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(106, 3);
			defaultInterpolatedStringHandler.AppendLiteral("Cloning current ");
			defaultInterpolatedStringHandler.AppendFormatted(source);
			defaultInterpolatedStringHandler.AppendLiteral(" files to ");
			defaultInterpolatedStringHandler.AppendFormatted(destination);
			defaultInterpolatedStringHandler.AppendLiteral(" save folder. Porting ");
			defaultInterpolatedStringHandler.AppendFormatted(cloudName);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendLiteral("\nThis may take a few minutes for a large amount of files.");
			tML2.Info(defaultInterpolatedStringHandler.ToStringAndClear());
			try
			{
				FileUtilities.CopyFolderEXT(oldFolderPath, isAtomicLockable ? newFolderPathTemp : newFolderPath, isCloud, new Regex("(Workshop|ModSources)($|/|\\\\)"), false, true);
			}
			catch (Exception e2)
			{
				e2.HelpLink = "https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Usage-FAQ#migration-failed";
				ErrorReporting.FatalExit("Migration Failed, please consult the instructions in the \"Migration Failed\" section at \"" + e2.HelpLink + "\" for more information.", e2);
			}
			if (isAtomicLockable)
			{
				Directory.Move(newFolderPathTemp, newFolderPath);
			}
			else if (isCloud)
			{
				if (SocialAPI.Cloud != null)
				{
					SocialAPI.Cloud.Write(FileUtilities.ConvertToRelativePath(superSavePath, portFilePath), new byte[0]);
				}
			}
			else
			{
				File.Create(portFilePath);
			}
			Logging.tML.Info("Porting " + cloudName + " finished");
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x002E3B6C File Offset: 0x002E1D6C
		internal static void PortFilesMaster(string savePath, bool isCloud)
		{
			Program.PortOldSaveDirectories(savePath);
			Program.PortCommonFilesToStagingBranches(savePath);
			Program.PortFilesFromXtoY(savePath, "tModLoader", "tModLoader-1.4.3", "2022.9", isCloud, !isCloud, new DateTime(2023, 9, 1));
			if (BuildInfo.IsStable)
			{
				Program.PortFilesFromXtoY(savePath, "tModLoader-preview", "tModLoader", "2023.6", isCloud, false, new DateTime(2023, 9, 1));
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x002E3BD8 File Offset: 0x002E1DD8
		private static void SetSavePath()
		{
			if (ControlledFolderAccessSupport.ControlledFolderAccessDetectionPrevented)
			{
				Logging.tML.Info("Controlled Folder Access detection failed, something is preventing the game from accessing the registry.");
			}
			if (ControlledFolderAccessSupport.ControlledFolderAccessDetected)
			{
				Logging.tML.Info("Controlled Folder Access feature detected. If game fails to launch make sure to add \"" + Environment.ProcessPath + "\" to the \"Allow an app through Controlled folder access\" menu found in the \"Ransomware protection\" menu.");
			}
			string customSavePath;
			if (Program.LaunchParameters.TryGetValue("-tmlsavedirectory", out customSavePath))
			{
				Program.SavePathShared = customSavePath;
				Program.SavePath = customSavePath;
			}
			else if (File.Exists("savehere.txt"))
			{
				Program.SavePathShared = "tModLoader";
				Program.SavePath = Program.SaveFolderName;
			}
			else
			{
				Program.SavePathShared = Path.Combine(Program.SavePath, "tModLoader");
				string savePathCopy = Program.SavePath;
				Program.SavePath = Path.Combine(Program.SavePath, Program.SaveFolderName);
				try
				{
					Program.PortFilesMaster(savePathCopy, false);
				}
				catch (Exception e)
				{
					bool controlledFolderAccessMightBeRelevant = (e is COMException || e is FileNotFoundException) && ControlledFolderAccessSupport.ControlledFolderAccessDetected;
					ErrorReporting.FatalExit("An error occurred migrating files and folders to the new structure" + (controlledFolderAccessMightBeRelevant ? ("\n\nControlled Folder Access feature detected, this might be the cause of this error.\n\nMake sure to add \"" + Environment.ProcessPath + "\" to the \"Allow an app through Controlled folder access\" menu found in the \"Ransomware protection\" menu.") : ""), e);
				}
			}
			if (Platform.IsWindows)
			{
				string SavePathFixed = Program.SavePath.Replace('/', Path.DirectorySeparatorChar);
				string SavePathSharedFixed = Program.SavePathShared.Replace('/', Path.DirectorySeparatorChar);
				if (Program.SavePath != SavePathFixed || Program.SavePathShared != SavePathSharedFixed)
				{
					ILog tML = Logging.tML;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(57, 4);
					defaultInterpolatedStringHandler.AppendLiteral("Saves paths had incorrect slashes somehow: \"");
					defaultInterpolatedStringHandler.AppendFormatted(Program.SavePath);
					defaultInterpolatedStringHandler.AppendLiteral("\"=>\"");
					defaultInterpolatedStringHandler.AppendFormatted(SavePathFixed);
					defaultInterpolatedStringHandler.AppendLiteral("\", \"");
					defaultInterpolatedStringHandler.AppendFormatted(Program.SavePathShared);
					defaultInterpolatedStringHandler.AppendLiteral("\"=>\"");
					defaultInterpolatedStringHandler.AppendFormatted(SavePathSharedFixed);
					defaultInterpolatedStringHandler.AppendLiteral("\"");
					tML.Warn(defaultInterpolatedStringHandler.ToStringAndClear());
					Program.SavePath = SavePathFixed;
					Program.SavePathShared = SavePathSharedFixed;
				}
			}
			Logging.tML.Info("Saves Are Located At: " + Path.GetFullPath(Program.SavePath));
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x002E3DF8 File Offset: 0x002E1FF8
		private static void StartupSequenceTml(bool isServer)
		{
			try
			{
				ControlledFolderAccessSupport.CheckFileSystemAccess();
				Logging.Init(isServer ? Logging.LogFile.Server : Logging.LogFile.Client);
				if (Platform.Current.Type == null && RuntimeInformation.ProcessArchitecture != Architecture.X64)
				{
					if (Program.LaunchParameters.ContainsKey("-build"))
					{
						Console.WriteLine("Warning: Building mods requires the 64 bit dotnet SDK to be installed, but the 32 bit dotnet SDK appears to be running. It is likely that you accidentally installed the 32 bit dotnet SDK and it is taking priority. To fix this, follow the instructions at https://github.com/tModLoader/tModLoader/wiki/tModLoader-guide-for-developers#net-sdk");
					}
					ErrorReporting.FatalExit("The current Windows Architecture of your System is CURRENTLY unsupported. Aborting...");
				}
				if (Platform.Current.Type == 1 && Environment.OSVersion.Version < new Version(10, 15))
				{
					ErrorReporting.FatalExit("tModLoader requires macOS v10.15 (Catalina) or higher to run as of tModLoader v2024.03+. Please update macOS.\nIf updating is not possible, manually downgrading (https://github.com/tModLoader/tModLoader/wiki/tModLoader-guide-for-players#manual-installation) to tModLoader v2024.02.3.0 (https://github.com/tModLoader/tModLoader/releases/tag/v2024.02.3.0) is an option to keep playing.\nAborting...");
				}
				Logging.LogStartup(isServer);
				Program.SetSavePath();
				if (ModCompile.DeveloperMode)
				{
					Logging.tML.Info("Developer mode enabled");
				}
				Program.AttemptSupportHighDPI(isServer);
				if (!isServer)
				{
					NativeLibraries.CheckNativeFAudioDependencies();
					FNALogging.RedirectLogs();
				}
			}
			catch (Exception ex)
			{
				ErrorReporting.FatalExit("An unexpected error occurred during tML startup", ex);
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x002E3ED4 File Offset: 0x002E20D4
		private static void ProcessLaunchArgs(string[] args, bool monoArgs, out bool isServer)
		{
			isServer = false;
			try
			{
				if (monoArgs)
				{
					args = Utils.ConvertMonoArgsToDotNet(args);
				}
				Program.LaunchParameters = Utils.ParseArguements(args);
				if (Program.LaunchParameters.ContainsKey("-terrariasteamclient"))
				{
					TerrariaSteamClient.Run();
					Environment.Exit(1);
				}
				Program.SavePath = (Program.LaunchParameters.ContainsKey("-savedirectory") ? Program.LaunchParameters["-savedirectory"] : Platform.Get<IPathService>().GetStoragePath("Terraria"));
				isServer = Program.LaunchParameters.ContainsKey("-server");
			}
			catch (Exception e)
			{
				ErrorReporting.FatalExit("Unhandled Issue with Launch Arguments. Please verify sources such as Steam Launch Options, cli-ArgsConfig, and VS profiles", e);
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x002E3F80 File Offset: 0x002E2180
		private static void AttemptSupportHighDPI(bool isServer)
		{
			if (isServer)
			{
				return;
			}
			if (Platform.IsWindows)
			{
				Program.<AttemptSupportHighDPI>g__SetProcessDPIAware|51_0();
			}
			SDL.SDL_VideoInit(null);
			float ddpi;
			float hdpi;
			float vdpi;
			SDL.SDL_GetDisplayDPI(0, ref ddpi, ref hdpi, ref vdpi);
			ILog tML = Logging.tML;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(67, 3);
			defaultInterpolatedStringHandler.AppendLiteral("Display DPI: Diagonal DPI is ");
			defaultInterpolatedStringHandler.AppendFormatted<float>(ddpi);
			defaultInterpolatedStringHandler.AppendLiteral(". Vertical DPI is ");
			defaultInterpolatedStringHandler.AppendFormatted<float>(vdpi);
			defaultInterpolatedStringHandler.AppendLiteral(". Horizontal DPI is ");
			defaultInterpolatedStringHandler.AppendFormatted<float>(hdpi);
			tML.Info(defaultInterpolatedStringHandler.ToStringAndClear());
			if (ddpi >= 96f || hdpi >= 96f || vdpi >= 96f)
			{
				Environment.SetEnvironmentVariable("FNA_GRAPHICS_ENABLE_HIGHDPI", "1");
				Logging.tML.Info("High DPI Display detected: setting FNA to highdpi mode");
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x002E406C File Offset: 0x002E226C
		[CompilerGenerated]
		internal static void <PortFilesFromXtoY>g__PromptUserForNewestTMLVersionLaunched|45_0(ref string lastLaunchedTml)
		{
			if (string.IsNullOrEmpty(lastLaunchedTml))
			{
				int num = ErrorReporting.ShowMessageBoxWithChoices("Failed to read config.json configuration file", "Your config.json file is incomplete.\n\nPlease select one of the following options and the game will resume loading:\n\nWhat is the highest version of tModLoader that you have launched?", new string[]
				{
					"1.4.4",
					"1.4.3",
					"Cancel"
				});
				if (num == 0)
				{
					lastLaunchedTml = BuildInfo.tMLVersion.ToString();
				}
				if (num == 1)
				{
					lastLaunchedTml = "2022.09";
				}
			}
		}

		// Token: 0x06000CD6 RID: 3286
		[CompilerGenerated]
		[DllImport("user32.dll", EntryPoint = "SetProcessDPIAware")]
		internal static extern bool <AttemptSupportHighDPI>g__SetProcessDPIAware|51_0();

		// Token: 0x04000DA6 RID: 3494
		public static bool IsXna = false;

		// Token: 0x04000DA7 RID: 3495
		public static bool IsFna = true;

		// Token: 0x04000DA8 RID: 3496
		public static bool IsMono = Type.GetType("Mono.Runtime") != null;

		// Token: 0x04000DA9 RID: 3497
		public static Dictionary<string, string> LaunchParameters = new Dictionary<string, string>();

		// Token: 0x04000DAA RID: 3498
		public static string SavePath;

		// Token: 0x04000DAB RID: 3499
		public const string TerrariaSaveFolderPath = "Terraria";

		// Token: 0x04000DAC RID: 3500
		private static int ThingsToLoad;

		// Token: 0x04000DAD RID: 3501
		private static int ThingsLoaded;

		// Token: 0x04000DAE RID: 3502
		public static bool LoadedEverything;

		// Token: 0x04000DAF RID: 3503
		public static IntPtr JitForcedMethodCache;

		// Token: 0x04000DB2 RID: 3506
		public const string PreviewFolder = "tModLoader-preview";

		// Token: 0x04000DB3 RID: 3507
		public const string ReleaseFolder = "tModLoader";

		// Token: 0x04000DB4 RID: 3508
		public const string DevFolder = "tModLoader-dev";

		// Token: 0x04000DB5 RID: 3509
		public const string Legacy143Folder = "tModLoader-1.4.3";

		// Token: 0x04000DB6 RID: 3510
		private const int HighDpiThreshold = 96;

		// Token: 0x020007E1 RID: 2017
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006763 RID: 26467
			public static ParameterizedThreadStart <0>__ForceLoadThread;

			// Token: 0x04006764 RID: 26468
			public static Action<MethodInfo> <1>__ForceJITOnMethod;

			// Token: 0x04006765 RID: 26469
			public static Action <2>__StartForceLoad;
		}
	}
}
