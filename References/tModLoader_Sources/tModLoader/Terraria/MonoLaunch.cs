using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Threading;
using log4net;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Engine;

namespace Terraria
{
	// Token: 0x0200003C RID: 60
	internal static class MonoLaunch
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x0014C430 File Offset: 0x0014A630
		private static void Main(string[] args)
		{
			int i = Array.IndexOf<string>(args, "-tModPorter");
			if (i >= 0)
			{
				tModPorterLaunch.Launch(args.Skip(i).ToArray<string>());
				return;
			}
			Thread.CurrentThread.Name = "Entry Thread";
			NativeLibraries.SetNativeLibraryPath(MonoLaunch.NativesDir);
			AssemblyLoadContext @default = AssemblyLoadContext.Default;
			Func<Assembly, string, IntPtr> value;
			if ((value = MonoLaunch.<>O.<0>__ResolveNativeLibrary) == null)
			{
				value = (MonoLaunch.<>O.<0>__ResolveNativeLibrary = new Func<Assembly, string, IntPtr>(MonoLaunch.ResolveNativeLibrary));
			}
			@default.ResolvingUnmanagedDll += value;
			Environment.SetEnvironmentVariable("FNA_WORKAROUND_WINDOW_RESIZABLE", "1");
			if (File.Exists("cli-argsConfig.txt"))
			{
				args = args.Concat(File.ReadAllLines("cli-argsConfig.txt").SelectMany((string a) => a.Split(" ", 2, StringSplitOptions.None))).ToArray<string>();
			}
			if (File.Exists("env-argsConfig.txt"))
			{
				foreach (string[] environmentVar in from text in File.ReadAllLines("env-argsConfig.txt")
				select text.Split("=", StringSplitOptions.None) into envVar
				where envVar.Length == 2
				select envVar)
				{
					Environment.SetEnvironmentVariable(environmentVar[0], environmentVar[1]);
				}
			}
			Action LocalLaunchGame = delegate()
			{
				MonoLaunch.Main_End(args);
			};
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				new Thread(new ThreadStart(LocalLaunchGame.Invoke)).Start();
				Thread.CurrentThread.IsBackground = true;
				return;
			}
			LocalLaunchGame();
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0014C5F8 File Offset: 0x0014A7F8
		private static void Main_End(string[] args)
		{
			Program.LaunchGame(args, true);
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0014C604 File Offset: 0x0014A804
		private static string NativePlatformDir
		{
			get
			{
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					return "Windows";
				}
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				{
					return "Linux";
				}
				if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
				{
					throw new InvalidOperationException("Unknown OS.");
				}
				return "OSX";
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0014C654 File Offset: 0x0014A854
		private static IntPtr ResolveNativeLibrary(Assembly assembly, string name)
		{
			object obj = MonoLaunch.resolverLock;
			IntPtr result;
			lock (obj)
			{
				IntPtr handle;
				if (MonoLaunch.assemblies.TryGetValue(name, out handle))
				{
					result = handle;
				}
				else
				{
					Logging.tML.Debug("Native Resolve: " + assembly.FullName + " -> " + name);
					if (name.StartsWith("/tmp/"))
					{
						result = IntPtr.Zero;
					}
					else
					{
						string[] files;
						try
						{
							files = Directory.GetFiles(MonoLaunch.NativesDir, "*" + name + "*", SearchOption.AllDirectories);
						}
						catch
						{
							files = Array.Empty<string>();
						}
						string path = files.FirstOrDefault<string>();
						if (path == null)
						{
							Logging.tML.Debug("\tnot found");
							result = IntPtr.Zero;
						}
						else
						{
							Logging.tML.Debug("\tattempting load " + path);
							try
							{
								handle = NativeLibrary.Load(path);
							}
							catch (Exception e)
							{
								ILog tML = Logging.tML;
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(19, 2);
								defaultInterpolatedStringHandler.AppendLiteral("\t\tFailed to load ");
								defaultInterpolatedStringHandler.AppendFormatted(name);
								defaultInterpolatedStringHandler.AppendLiteral(".\n");
								defaultInterpolatedStringHandler.AppendFormatted<Exception>(e);
								tML.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
								return IntPtr.Zero;
							}
							Logging.tML.Debug("\tsuccess");
							result = (MonoLaunch.assemblies[name] = handle);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x040007B4 RID: 1972
		private static readonly Dictionary<string, IntPtr> assemblies = new Dictionary<string, IntPtr>();

		// Token: 0x040007B5 RID: 1973
		public static readonly string NativesDir = Path.Combine(Environment.CurrentDirectory, "Libraries", "Native", MonoLaunch.NativePlatformDir);

		// Token: 0x040007B6 RID: 1974
		public static readonly object resolverLock = new object();

		// Token: 0x020007B4 RID: 1972
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400664D RID: 26189
			[Nullable(new byte[]
			{
				0,
				1,
				1
			})]
			public static Func<Assembly, string, IntPtr> <0>__ResolveNativeLibrary;
		}
	}
}
