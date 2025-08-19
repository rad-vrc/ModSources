using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Ionic.Zip;
using ReLogic.OS;
using Terraria.ModLoader;

namespace Terraria.Utilities
{
	// Token: 0x0200008A RID: 138
	public static class CrashDump
	{
		// Token: 0x06001439 RID: 5177 RVA: 0x004A151E File Offset: 0x0049F71E
		public static bool WriteException(CrashDump.Options options, string outputDirectory = ".")
		{
			return CrashDump.Write(options, CrashDump.ExceptionInfo.Present, outputDirectory);
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x004A1528 File Offset: 0x0049F728
		public static bool Write(CrashDump.Options options, string outputDirectory = ".")
		{
			return CrashDump.Write(options, CrashDump.ExceptionInfo.None, outputDirectory);
		}

		/// <summary>
		/// Writes a dump to the logs folder, zips it, and deletes old dumps. Intended to preserve a single dump for client and server to not waste harddrive space.
		/// </summary>
		// Token: 0x0600143B RID: 5179 RVA: 0x004A1534 File Offset: 0x0049F734
		public static string WriteExceptionAsZipAndClearOld(CrashDump.Options options, IntPtr exceptionPointers)
		{
			if (!Platform.IsWindows)
			{
				return null;
			}
			string fileName = CrashDump.CreateDumpName();
			string path = Path.Combine(Logging.LogDir, fileName);
			if (!Utils.TryCreatingDirectory(Logging.LogDir))
			{
				return null;
			}
			string prefix = fileName.Split('_', StringSplitOptions.None)[0];
			string[] extensions = new string[]
			{
				".dmp.zip",
				".dmp"
			};
			FileInfo[] files = new DirectoryInfo(Logging.LogDir).GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				FileInfo file = files[i];
				if (extensions.Any((string ext) => file.Extension == ext) && file.Name.StartsWith(prefix))
				{
					Logging.tML.Info("Deleting old dump file: " + file.Name);
					file.Delete();
				}
			}
			using (FileStream fileStream = File.Create(path))
			{
				CrashDump.Write(fileStream.SafeFileHandle, options, CrashDump.ExceptionInfo.None, exceptionPointers);
			}
			string zipFilename = Path.ChangeExtension(path, ".dmp.zip");
			using (ZipFile zip = new ZipFile(zipFilename, Encoding.UTF8))
			{
				zip.AddFile(path, "");
				zip.Save();
				File.Delete(path);
			}
			return zipFilename;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x004A16A4 File Offset: 0x0049F8A4
		private static string CreateDumpName()
		{
			DateTime dateTime = DateTime.Now.ToLocalTime();
			return string.Format("{0}_{1}_{2}_{3}.dmp", new object[]
			{
				Main.dedServ ? "server" : "client",
				BuildInfo.versionTag,
				dateTime.ToString("MM-dd-yy_HH-mm-ss-ffff", CultureInfo.InvariantCulture),
				Thread.CurrentThread.ManagedThreadId
			});
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x004A1714 File Offset: 0x0049F914
		private static bool Write(CrashDump.Options options, CrashDump.ExceptionInfo exceptionInfo, string outputDirectory)
		{
			if (!Platform.IsWindows)
			{
				return false;
			}
			string path = Path.Combine(outputDirectory, CrashDump.CreateDumpName());
			if (!Utils.TryCreatingDirectory(outputDirectory))
			{
				return false;
			}
			bool result;
			using (FileStream fileStream = File.Create(path))
			{
				result = CrashDump.Write(fileStream.SafeFileHandle, options, exceptionInfo, (IntPtr)0);
			}
			return result;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x004A1774 File Offset: 0x0049F974
		private static bool Write(SafeHandle fileHandle, CrashDump.Options options, CrashDump.ExceptionInfo exceptionInfo, IntPtr exceptionPointers = 0)
		{
			if (!Platform.IsWindows)
			{
				return false;
			}
			Process currentProcess = Process.GetCurrentProcess();
			IntPtr handle = currentProcess.Handle;
			uint id = (uint)currentProcess.Id;
			CrashDump.MiniDumpExceptionInformation expParam = default(CrashDump.MiniDumpExceptionInformation);
			expParam.ThreadId = CrashDump.GetCurrentThreadId();
			expParam.ClientPointers = false;
			expParam.ExceptionPointers = IntPtr.Zero;
			if (exceptionInfo == CrashDump.ExceptionInfo.Present)
			{
				expParam.ExceptionPointers = Marshal.GetExceptionPointers();
			}
			if (exceptionPointers != IntPtr.Zero)
			{
				expParam.ExceptionPointers = exceptionPointers;
			}
			if (expParam.ExceptionPointers == IntPtr.Zero)
			{
				return CrashDump.MiniDumpWriteDump(handle, id, fileHandle, (uint)options, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			}
			return CrashDump.MiniDumpWriteDump(handle, id, fileHandle, (uint)options, ref expParam, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x0600143F RID: 5183
		[DllImport("dbghelp.dll", CallingConvention = 3, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, ref CrashDump.MiniDumpExceptionInformation expParam, IntPtr userStreamParam, IntPtr callbackParam);

		// Token: 0x06001440 RID: 5184
		[DllImport("dbghelp.dll", CallingConvention = 3, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, IntPtr expParam, IntPtr userStreamParam, IntPtr callbackParam);

		// Token: 0x06001441 RID: 5185
		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern uint GetCurrentThreadId();

		// Token: 0x02000856 RID: 2134
		[Flags]
		public enum Options : uint
		{
			// Token: 0x040068D9 RID: 26841
			Normal = 0U,
			// Token: 0x040068DA RID: 26842
			WithDataSegs = 1U,
			// Token: 0x040068DB RID: 26843
			WithFullMemory = 2U,
			// Token: 0x040068DC RID: 26844
			WithHandleData = 4U,
			// Token: 0x040068DD RID: 26845
			FilterMemory = 8U,
			// Token: 0x040068DE RID: 26846
			ScanMemory = 16U,
			// Token: 0x040068DF RID: 26847
			WithUnloadedModules = 32U,
			// Token: 0x040068E0 RID: 26848
			WithIndirectlyReferencedMemory = 64U,
			// Token: 0x040068E1 RID: 26849
			FilterModulePaths = 128U,
			// Token: 0x040068E2 RID: 26850
			WithProcessThreadData = 256U,
			// Token: 0x040068E3 RID: 26851
			WithPrivateReadWriteMemory = 512U,
			// Token: 0x040068E4 RID: 26852
			WithoutOptionalData = 1024U,
			// Token: 0x040068E5 RID: 26853
			WithFullMemoryInfo = 2048U,
			// Token: 0x040068E6 RID: 26854
			WithThreadInfo = 4096U,
			// Token: 0x040068E7 RID: 26855
			WithCodeSegs = 8192U,
			// Token: 0x040068E8 RID: 26856
			WithoutAuxiliaryState = 16384U,
			// Token: 0x040068E9 RID: 26857
			WithFullAuxiliaryState = 32768U,
			// Token: 0x040068EA RID: 26858
			WithPrivateWriteCopyMemory = 65536U,
			// Token: 0x040068EB RID: 26859
			IgnoreInaccessibleMemory = 131072U,
			// Token: 0x040068EC RID: 26860
			ValidTypeFlags = 262143U
		}

		// Token: 0x02000857 RID: 2135
		private enum ExceptionInfo
		{
			// Token: 0x040068EE RID: 26862
			None,
			// Token: 0x040068EF RID: 26863
			Present
		}

		// Token: 0x02000858 RID: 2136
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		private struct MiniDumpExceptionInformation
		{
			// Token: 0x040068F0 RID: 26864
			public uint ThreadId;

			// Token: 0x040068F1 RID: 26865
			public IntPtr ExceptionPointers;

			// Token: 0x040068F2 RID: 26866
			[MarshalAs(UnmanagedType.Bool)]
			public bool ClientPointers;
		}
	}
}
