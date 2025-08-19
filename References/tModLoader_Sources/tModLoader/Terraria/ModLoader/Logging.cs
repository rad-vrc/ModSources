using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using Microsoft.Xna.Framework;
using ReLogic.OS;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Engine;
using Terraria.ModLoader.UI;
using Terraria.Utilities;

namespace Terraria.ModLoader
{
	// Token: 0x02000192 RID: 402
	public static class Logging
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x004DF755 File Offset: 0x004DD955
		// (set) Token: 0x06001F16 RID: 7958 RVA: 0x004DF75C File Offset: 0x004DD95C
		public static string LogPath { get; private set; }

		/// <summary> Available for logging when Mod.Logging is not available, such as field initialization </summary>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x004DF764 File Offset: 0x004DD964
		public static ILog PublicLogger { get; } = LogManager.GetLogger("PublicLogger");

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x004DF76B File Offset: 0x004DD96B
		internal static ILog Terraria { get; } = LogManager.GetLogger("Terraria");

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x004DF772 File Offset: 0x004DD972
		internal static ILog tML { get; } = LogManager.GetLogger("tML");

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x004DF779 File Offset: 0x004DD979
		internal static ILog FNA { get; } = LogManager.GetLogger("FNA");

		// Token: 0x06001F1B RID: 7963 RVA: 0x004DF780 File Offset: 0x004DD980
		internal static void Init(Logging.LogFile logFile)
		{
			Logging.LegacyCleanups();
			if (Program.LaunchParameters.ContainsKey("-build"))
			{
				return;
			}
			Utils.TryCreatingDirectory(Logging.LogDir);
			try
			{
				Logging.InitLogPaths(logFile);
				Logging.ConfigureAppenders(logFile);
				Logging.TryUpdatingFileCreationDate(Logging.LogPath);
			}
			catch (Exception e)
			{
				ErrorReporting.FatalExit("Failed to init logging", e);
			}
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x004DF7E8 File Offset: 0x004DD9E8
		internal static void LogStartup(bool dedServ)
		{
			if (Program.LaunchParameters.ContainsKey("-build"))
			{
				return;
			}
			ILog tML = Logging.tML;
			string text = "Starting tModLoader {0} {1} built {2}";
			object obj = dedServ ? "server" : "client";
			object buildIdentifier = BuildInfo.BuildIdentifier;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<DateTime>(BuildInfo.BuildDate, "g");
			tML.InfoFormat(text, obj, buildIdentifier, defaultInterpolatedStringHandler.ToStringAndClear());
			Logging.tML.InfoFormat("Log date: {0}", DateTime.Now.ToString("d"));
			Logging.tML.InfoFormat("Running on {0} (v{1}) {2} {3} {4}", new object[]
			{
				Platform.Current.Type,
				Environment.OSVersion.Version,
				RuntimeInformation.ProcessArchitecture,
				FrameworkVersion.Framework,
				FrameworkVersion.Version
			});
			Logging.tML.InfoFormat("CPU: {0} processors. RAM: {1}", Environment.ProcessorCount, UIMemoryBar.SizeSuffix(UIMemoryBar.GetTotalMemory(), 1));
			Logging.tML.InfoFormat("FrameworkDescription: {0}", RuntimeInformation.FrameworkDescription);
			Logging.tML.InfoFormat("Executable: {0}", Assembly.GetEntryAssembly().Location);
			Logging.tML.InfoFormat("Working Directory: {0}", Path.GetFullPath(Directory.GetCurrentDirectory()));
			string args2 = string.Join<string>(' ', Environment.GetCommandLineArgs().Skip(1));
			if (!string.IsNullOrEmpty(args2) || Program.LaunchParameters.Any<KeyValuePair<string, string>>())
			{
				Logging.tML.InfoFormat("Launch Parameters: {0}", args2);
				Logging.tML.InfoFormat("Parsed Launch Parameters: {0}", string.Join<string>(' ', from p in Program.LaunchParameters
				select (p.Key + " " + p.Value).Trim()));
			}
			Logging.DumpEnvVars();
			string stackLimit = Environment.GetEnvironmentVariable("COMPlus_DefaultStackSize");
			if (!string.IsNullOrEmpty(stackLimit))
			{
				Logging.tML.InfoFormat("Override Default Thread Stack Size Limit: {0}", stackLimit);
			}
			foreach (string line in Logging.initWarnings)
			{
				Logging.tML.Warn(line);
			}
			AppDomain.CurrentDomain.UnhandledException += delegate(object s, UnhandledExceptionEventArgs args)
			{
				Logging.tML.Error("Unhandled Exception", args.ExceptionObject as Exception);
			};
			Logging.LogFirstChanceExceptions();
			AssemblyResolving.Init();
			LoggingHooks.Init();
			LogArchiver.ArchiveLogs();
			NativeExceptionHandling.Init();
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x004DFA58 File Offset: 0x004DDC58
		private static void ConfigureAppenders(Logging.LogFile logFile)
		{
			PatternLayout layout = new PatternLayout
			{
				ConversionPattern = "[%d{HH:mm:ss.fff}] [%t/%level] [%logger]: %m%n"
			};
			layout.ActivateOptions();
			List<IAppender> appenders = new List<IAppender>();
			if (logFile == Logging.LogFile.Client)
			{
				appenders.Add(new ConsoleAppender
				{
					Name = "ConsoleAppender",
					Layout = layout
				});
			}
			appenders.Add(new DebugAppender
			{
				Name = "DebugAppender",
				Layout = layout
			});
			FileAppender fileAppender = new FileAppender
			{
				Name = "FileAppender",
				File = Logging.LogPath,
				AppendToFile = false,
				Encoding = Logging.encoding,
				Layout = layout
			};
			fileAppender.ActivateOptions();
			appenders.Add(fileAppender);
			BasicConfigurator.Configure(appenders.ToArray());
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x004DFB10 File Offset: 0x004DDD10
		private static void InitLogPaths(Logging.LogFile logFile)
		{
			string mainLogName = logFile.ToString().ToLowerInvariant();
			List<string> baseLogNames = new List<string>
			{
				mainLogName
			};
			if (logFile != Logging.LogFile.TerrariaSteamClient)
			{
				baseLogNames.Add("environment-" + mainLogName);
			}
			if (logFile == Logging.LogFile.Client)
			{
				baseLogNames.Add(Logging.LogFile.TerrariaSteamClient.ToString().ToLowerInvariant());
			}
			string logFileName = Logging.GetFreeLogFileName(baseLogNames, logFile != Logging.LogFile.TerrariaSteamClient);
			Logging.LogPath = Path.Combine(Logging.LogDir, logFileName);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x004DFB90 File Offset: 0x004DDD90
		private static string GetFreeLogFileName(List<string> baseLogNames, bool roll)
		{
			string baseLogName = baseLogNames[0];
			Regex pattern = new Regex("(?:" + string.Join<string>('|', baseLogNames) + ")(\\d*)\\.log$");
			List<string> existingLogs = (from s in Directory.GetFiles(Logging.LogDir)
			where pattern.IsMatch(Path.GetFileName(s))
			select s).ToList<string>();
			IEnumerable<string> source = existingLogs;
			Func<string, bool> predicate;
			if ((predicate = Logging.<>O.<0>__CanOpen) == null)
			{
				predicate = (Logging.<>O.<0>__CanOpen = new Func<string, bool>(Logging.CanOpen));
			}
			if (!source.All(predicate))
			{
				int i = existingLogs.Select(delegate(string s)
				{
					string tok = pattern.Match(Path.GetFileName(s)).Groups[1].Value;
					if (tok.Length != 0)
					{
						return int.Parse(tok);
					}
					return 1;
				}).Max();
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
				defaultInterpolatedStringHandler.AppendFormatted(baseLogName);
				defaultInterpolatedStringHandler.AppendFormatted<int>(i + 1);
				defaultInterpolatedStringHandler.AppendLiteral(".log");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
			if (roll)
			{
				Logging.RenameToOld(existingLogs);
			}
			else if (existingLogs.Any<string>())
			{
				IEnumerable<string> logNames = from s in existingLogs
				select Path.GetFileName(s);
				List<string> list = Logging.initWarnings;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(93, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Old log files found which should have already been archived. The ");
				defaultInterpolatedStringHandler.AppendFormatted(baseLogName);
				defaultInterpolatedStringHandler.AppendLiteral(".log will be overwritten. [");
				defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", logNames));
				defaultInterpolatedStringHandler.AppendLiteral("]");
				list.Add(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return baseLogName + ".log";
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x004DFD00 File Offset: 0x004DDF00
		private static void RenameToOld(List<string> existingLogs)
		{
			Func<string, DateTime> keySelector;
			if ((keySelector = Logging.<>O.<1>__GetCreationTime) == null)
			{
				keySelector = (Logging.<>O.<1>__GetCreationTime = new Func<string, DateTime>(File.GetCreationTime));
			}
			foreach (string existingLog in existingLogs.OrderBy(keySelector))
			{
				string oldExt = ".old";
				int i = 0;
				while (File.Exists(existingLog + oldExt))
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
					defaultInterpolatedStringHandler.AppendLiteral(".old");
					defaultInterpolatedStringHandler.AppendFormatted<int>(++i);
					oldExt = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				try
				{
					File.Move(existingLog, existingLog + oldExt);
				}
				catch (IOException e)
				{
					List<string> list = Logging.initWarnings;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 4);
					defaultInterpolatedStringHandler.AppendLiteral("Move failed during log initialization: ");
					defaultInterpolatedStringHandler.AppendFormatted(existingLog);
					defaultInterpolatedStringHandler.AppendLiteral(" -> ");
					defaultInterpolatedStringHandler.AppendFormatted(Path.GetFileName(existingLog));
					defaultInterpolatedStringHandler.AppendFormatted(oldExt);
					defaultInterpolatedStringHandler.AppendLiteral("\n");
					defaultInterpolatedStringHandler.AppendFormatted<IOException>(e);
					list.Add(defaultInterpolatedStringHandler.ToStringAndClear());
				}
			}
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x004DFE30 File Offset: 0x004DE030
		private static bool CanOpen(string fileName)
		{
			bool result;
			try
			{
				using (new FileStream(fileName, FileMode.Append))
				{
				}
				result = true;
			}
			catch (IOException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x004DFE78 File Offset: 0x004DE078
		private static void AddChatMessage(string msg)
		{
			Logging.AddChatMessage(msg, Color.OrangeRed);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x004DFE85 File Offset: 0x004DE085
		private static void AddChatMessage(string msg, Color color)
		{
			if (Main.gameMenu)
			{
				return;
			}
			float soundVolume = Main.soundVolume;
			Main.soundVolume = 0f;
			Main.NewText(msg, new Color?(color));
			Main.soundVolume = soundVolume;
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x004DFEB0 File Offset: 0x004DE0B0
		internal static void LogStatusChange(string newStatusText)
		{
			if (Logging.lastStatusLogged == null)
			{
				Logging.lastStatusLogged = string.Empty;
			}
			string newBase = newStatusText;
			Match statusMatchNew = Logging.statusRegex.Match(newStatusText);
			if (statusMatchNew != null && statusMatchNew.Success)
			{
				newBase = statusMatchNew.Groups[1].Value;
			}
			if (WorldGen.generatingWorld)
			{
				Match statusGenMatchNew = Logging.statusGeneratingWorld.Match(newStatusText);
				if (statusGenMatchNew != null && statusGenMatchNew.Success)
				{
					newBase = statusGenMatchNew.Groups[1].Value;
				}
				if (WorldGen.drunkWorldGenText && !Main.dedServ)
				{
					newBase = "Random Numbers (Drunk World)";
				}
			}
			if (newBase != Logging.lastStatusLogged && newBase.Length > 0)
			{
				LogManager.GetLogger("StatusText").Info(newBase);
				Logging.lastStatusLogged = newBase;
			}
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x004DFF69 File Offset: 0x004DE169
		internal static void ServerConsoleLine(string msg)
		{
			Logging.ServerConsoleLine(msg, Level.Info, null, null);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x004DFF78 File Offset: 0x004DE178
		internal static void ServerConsoleLine(string msg, Level level, Exception ex = null, ILog log = null)
		{
			if (level == Level.Warn)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
			}
			else if (level == Level.Error)
			{
				Console.ForegroundColor = ConsoleColor.Red;
			}
			Console.WriteLine(msg);
			Console.ResetColor();
			(log ?? Logging.Terraria).Logger.Log(null, level, msg, ex);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x004DFFD4 File Offset: 0x004DE1D4
		private static void DumpEnvVars()
		{
			try
			{
				string fileName = "environment-" + Path.GetFileName(Logging.LogPath);
				string text = Path.Combine(Logging.LogDir, fileName);
				Logging.TryUpdatingFileCreationDate(text);
				using (FileStream f = File.OpenWrite(text))
				{
					using (StreamWriter w = new StreamWriter(f))
					{
						foreach (object key in Environment.GetEnvironmentVariables().Keys)
						{
							TextWriter textWriter = w;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
							defaultInterpolatedStringHandler.AppendFormatted<object>(key);
							defaultInterpolatedStringHandler.AppendLiteral("=");
							defaultInterpolatedStringHandler.AppendFormatted(Environment.GetEnvironmentVariable((string)key));
							textWriter.WriteLine(defaultInterpolatedStringHandler.ToStringAndClear());
						}
					}
				}
			}
			catch (Exception e)
			{
				Logging.tML.Error("Failed to dump env vars", e);
			}
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x004E00F0 File Offset: 0x004DE2F0
		private static void TryUpdatingFileCreationDate(string filePath)
		{
			if (File.Exists(filePath))
			{
				using (new Logging.QuietExceptionHandle())
				{
					try
					{
						File.SetCreationTime(filePath, DateTime.Now);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x004E0140 File Offset: 0x004DE340
		private static void LegacyCleanups()
		{
			using (new Logging.QuietExceptionHandle())
			{
				foreach (string filePath in Logging.autoRemovedFiles)
				{
					string fullPath = Path.Combine(Logging.LogDir, filePath);
					if (File.Exists(fullPath))
					{
						try
						{
							File.Delete(fullPath);
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x004E01B4 File Offset: 0x004DE3B4
		public static void IgnoreExceptionSource(string source)
		{
			Logging.ignoreSources.Add(source);
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x004E01C2 File Offset: 0x004DE3C2
		public static void IgnoreExceptionContents(string source)
		{
			if (!Logging.ignoreContents.Contains(source))
			{
				Logging.ignoreContents.Add(source);
			}
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x004E01DC File Offset: 0x004DE3DC
		internal static void ResetPastExceptions()
		{
			Logging.pastExceptions.Clear();
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x004E01E8 File Offset: 0x004DE3E8
		private static void LogFirstChanceExceptions()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			EventHandler<FirstChanceExceptionEventArgs> value;
			if ((value = Logging.<>O.<2>__FirstChanceExceptionHandler) == null)
			{
				value = (Logging.<>O.<2>__FirstChanceExceptionHandler = new EventHandler<FirstChanceExceptionEventArgs>(Logging.FirstChanceExceptionHandler));
			}
			currentDomain.FirstChanceException += value;
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x004E0210 File Offset: 0x004DE410
		private static void FirstChanceExceptionHandler(object sender, FirstChanceExceptionEventArgs args)
		{
			if (Logging.quietExceptionCount > 0)
			{
				return;
			}
			using (new Logging.QuietExceptionHandle())
			{
				bool oom = args.Exception is OutOfMemoryException;
				if (oom)
				{
					Logging.TryFreeingMemory();
					Logging.tML.Info("tModLoader RAM usage during OutOfMemoryException: " + UIMemoryBar.SizeSuffix(Process.GetCurrentProcess().PrivateMemorySize64, 1));
				}
				try
				{
					Logging.<>c__DisplayClass53_1 CS$<>8__locals2 = new Logging.<>c__DisplayClass53_1();
					if (oom || (args.Exception != Logging.previousException && !(args.Exception is ThreadAbortException) && !Logging.ignoreTypes.Contains(args.Exception.GetType().FullName) && !Logging.ignoreSources.Contains(args.Exception.Source) && !Logging.ignoreMessages.Any(delegate(string str)
					{
						string message = args.Exception.Message;
						return message != null && message.Contains(str);
					}) && !Logging.ignoreThrowingMethods.Any(delegate(string str)
					{
						string stackTrace2 = args.Exception.StackTrace;
						return stackTrace2 != null && stackTrace2.Contains(str);
					})))
					{
						StackTrace stackTrace = new StackTrace(1, true);
						CS$<>8__locals2.traceString = stackTrace.ToString();
						if (oom || !Logging.ignoreContents.Any((string s) => Logging.MatchContents(CS$<>8__locals2.traceString, s)))
						{
							CS$<>8__locals2.traceString = stackTrace.ToString();
							Logging.<>c__DisplayClass53_1 CS$<>8__locals3 = CS$<>8__locals2;
							string traceString = CS$<>8__locals2.traceString;
							int num = CS$<>8__locals2.traceString.IndexOf('\n');
							CS$<>8__locals3.traceString = traceString.Substring(num, traceString.Length - num);
							Type type = args.Exception.GetType();
							string exString = ((type != null) ? type.ToString() : null) + ": " + args.Exception.Message + CS$<>8__locals2.traceString;
							HashSet<string> obj = Logging.pastExceptions;
							lock (obj)
							{
								if (!Logging.pastExceptions.Add(exString))
								{
									return;
								}
							}
							Logging.tML.Warn(Language.GetTextValue("tModLoader.RuntimeErrorSilentlyCaughtException") + "\n" + exString);
							Logging.previousException = args.Exception;
							string msg = args.Exception.Message + " " + Language.GetTextValue("tModLoader.RuntimeErrorSeeLogsForFullTrace", Path.GetFileName(Logging.LogPath));
							if (Program.SavePathShared == null || Main.dedServ)
							{
								Console.ForegroundColor = ConsoleColor.DarkMagenta;
								Console.WriteLine(msg);
								Console.ResetColor();
							}
							else if (Program.SavePathShared != null && ModCompile.activelyModding && !Main.gameMenu)
							{
								Logging.AddChatMessage(msg);
							}
							if (oom)
							{
								ErrorReporting.FatalExit(Language.GetTextValue("tModLoader.OutOfMemory"));
							}
							if (args.Exception.Data.Contains("dump"))
							{
								string opt = args.Exception.Data["dump"] as string;
								if (opt != null)
								{
									args.Exception.Data.Remove("dump");
									CrashDump.Options options;
									if (opt == "full")
									{
										options = CrashDump.Options.WithFullMemory;
									}
									else
									{
										options = CrashDump.Options.Normal;
									}
									CrashDump.WriteException(options, ".");
								}
							}
						}
					}
				}
				catch (Exception e)
				{
					Logging.tML.Warn("FirstChanceExceptionHandler exception", e);
				}
			}
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x004E059C File Offset: 0x004DE79C
		private static bool MatchContents(ReadOnlySpan<char> traceString, ReadOnlySpan<char> contentPattern)
		{
			for (;;)
			{
				int sep = contentPattern.IndexOf("..");
				ReadOnlySpan<char> i = (sep >= 0) ? contentPattern.Slice(0, sep) : contentPattern;
				int f = traceString.IndexOf(i);
				if (f < 0)
				{
					break;
				}
				if (sep < 0)
				{
					return true;
				}
				ref ReadOnlySpan<char> ptr = ref traceString;
				int num = f + i.Length;
				traceString = ptr.Slice(num, ptr.Length - num);
				ptr = ref contentPattern;
				num = sep + 2;
				contentPattern = ptr.Slice(num, ptr.Length - num);
			}
			return false;
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x004E061C File Offset: 0x004DE81C
		public static void PrettifyStackTraceSources(StackFrame[] frames)
		{
			if (frames == null)
			{
				return;
			}
			foreach (StackFrame frame in frames)
			{
				string filename = frame.GetFileName();
				MethodBase method = frame.GetMethod();
				Assembly assembly2;
				if (method == null)
				{
					assembly2 = null;
				}
				else
				{
					Type declaringType = method.DeclaringType;
					assembly2 = ((declaringType != null) ? declaringType.Assembly : null);
				}
				Assembly assembly = assembly2;
				if (filename != null && !(assembly == null))
				{
					string modName;
					string trim;
					if (AssemblyManager.GetAssemblyOwner(assembly, out modName))
					{
						trim = modName;
					}
					else
					{
						if (!(assembly == Logging.terrariaAssembly))
						{
							goto IL_90;
						}
						trim = "tModLoader";
					}
					int index = filename.LastIndexOf(trim, StringComparison.InvariantCultureIgnoreCase);
					if (index > 0)
					{
						filename = filename.Substring(index);
						Logging.f_fileName.SetValue(frame, filename);
					}
				}
				IL_90:;
			}
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x004E06C6 File Offset: 0x004DE8C6
		private static void TryFreeingMemory()
		{
			Main.tile = new Tilemap(0, 0);
			GC.Collect();
		}

		// Token: 0x04001657 RID: 5719
		public static readonly string LogDir = "tModLoader-Logs";

		// Token: 0x04001658 RID: 5720
		public static readonly string LogArchiveDir = Path.Combine(Logging.LogDir, "Old");

		// Token: 0x04001659 RID: 5721
		private static readonly Encoding encoding = new UTF8Encoding(false);

		// Token: 0x0400165A RID: 5722
		private static readonly List<string> initWarnings = new List<string>();

		// Token: 0x0400165B RID: 5723
		private static readonly Regex statusRegex = new Regex("(.+?)[: \\d]*%$");

		// Token: 0x0400165C RID: 5724
		private static readonly Regex statusGeneratingWorld = new Regex("\\d+\\.\\d% - (.+?) - \\d+\\.\\d%$");

		// Token: 0x04001662 RID: 5730
		[ThreadStatic]
		private static string lastStatusLogged;

		// Token: 0x04001663 RID: 5731
		private static readonly string[] autoRemovedFiles = new string[]
		{
			"environment-"
		};

		// Token: 0x04001664 RID: 5732
		[ThreadStatic]
		private static int quietExceptionCount;

		// Token: 0x04001665 RID: 5733
		private static readonly HashSet<string> pastExceptions = new HashSet<string>();

		// Token: 0x04001666 RID: 5734
		private static readonly HashSet<string> ignoreTypes = new HashSet<string>
		{
			"ReLogic.Peripherals.RGB.DeviceInitializationException",
			"System.Threading.Tasks.TaskCanceledException"
		};

		// Token: 0x04001667 RID: 5735
		private static readonly HashSet<string> ignoreSources = new HashSet<string>
		{
			"MP3Sharp"
		};

		// Token: 0x04001668 RID: 5736
		private static readonly List<string> ignoreContents = new List<string>
		{
			"System.Console.set_OutputEncoding",
			"Terraria.ModLoader.Core.ModCompile",
			"Delegate.CreateDelegateNoSecurityCheck",
			"MethodBase.GetMethodBody",
			"System.Int32.Parse..Terraria.Main.DedServ_PostModLoad",
			"Convert.ToInt32..Terraria.Main.DedServ_PostModLoad",
			"Terraria.Net.Sockets.TcpSocket.Terraria.Net.Sockets.ISocket.AsyncSend",
			"System.Diagnostics.Process.Kill",
			"UwUPnP",
			"System.Threading.CancellationTokenSource.Cancel",
			"System.Net.Http.HttpConnectionPool.AddHttp11ConnectionAsync",
			"ReLogic.Peripherals.RGB.SteelSeries.GameSenseConnection._sendMsg"
		};

		// Token: 0x04001669 RID: 5737
		private static readonly List<string> ignoreMessages = new List<string>
		{
			"A blocking operation was interrupted by a call to WSACancelBlockingCall",
			"The request was aborted: The request was canceled.",
			"Object name: 'System.Net.Sockets.Socket'.",
			"Object name: 'System.Net.Sockets.NetworkStream'",
			"This operation cannot be performed on a completed asynchronous result object.",
			"Object name: 'SslStream'.",
			"Unable to load DLL 'Microsoft.DiaSymReader.Native.x86.dll'"
		};

		// Token: 0x0400166A RID: 5738
		private static readonly List<string> ignoreThrowingMethods = new List<string>
		{
			"MonoMod.Utils.Interop.Unix.DlError",
			"System.Net.Sockets.Socket.AwaitableSocketAsyncEventArgs.ThrowException",
			"Terraria.Lighting.doColors_Mode",
			"System.Threading.CancellationToken.Throw"
		};

		// Token: 0x0400166B RID: 5739
		private static Exception previousException;

		// Token: 0x0400166C RID: 5740
		private const BindingFlags InstanceNonPublic = BindingFlags.Instance | BindingFlags.NonPublic;

		// Token: 0x0400166D RID: 5741
		internal static readonly FieldInfo f_fileName = typeof(StackFrame).GetField("_fileName", BindingFlags.Instance | BindingFlags.NonPublic) ?? typeof(StackFrame).GetField("strFileName", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400166E RID: 5742
		private static readonly Assembly terrariaAssembly = Assembly.GetExecutingAssembly();

		// Token: 0x0200090F RID: 2319
		public enum LogFile
		{
			// Token: 0x04006ABE RID: 27326
			Client,
			// Token: 0x04006ABF RID: 27327
			Server,
			// Token: 0x04006AC0 RID: 27328
			TerrariaSteamClient
		}

		// Token: 0x02000910 RID: 2320
		[CompilerFeatureRequired("RefStructs")]
		public readonly ref struct QuietExceptionHandle
		{
			// Token: 0x0600537A RID: 21370 RVA: 0x00699C0D File Offset: 0x00697E0D
			public QuietExceptionHandle()
			{
				Logging.quietExceptionCount++;
			}

			// Token: 0x0600537B RID: 21371 RVA: 0x00699C1B File Offset: 0x00697E1B
			public void Dispose()
			{
				Logging.quietExceptionCount--;
			}
		}

		// Token: 0x02000911 RID: 2321
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006AC1 RID: 27329
			public static Func<string, bool> <0>__CanOpen;

			// Token: 0x04006AC2 RID: 27330
			public static Func<string, DateTime> <1>__GetCreationTime;

			// Token: 0x04006AC3 RID: 27331
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static EventHandler<FirstChanceExceptionEventArgs> <2>__FirstChanceExceptionHandler;
		}
	}
}
