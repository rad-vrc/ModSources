using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using ReLogic.OS;

namespace Terraria.Utilities
{
	// Token: 0x02000149 RID: 329
	public static class CrashDump
	{
		// Token: 0x06001903 RID: 6403 RVA: 0x004DFB75 File Offset: 0x004DDD75
		public static bool WriteException(CrashDump.Options options, string outputDirectory = ".")
		{
			return CrashDump.Write(options, CrashDump.ExceptionInfo.Present, outputDirectory);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x004DFB7F File Offset: 0x004DDD7F
		public static bool Write(CrashDump.Options options, string outputDirectory = ".")
		{
			return CrashDump.Write(options, CrashDump.ExceptionInfo.None, outputDirectory);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x004DFB8C File Offset: 0x004DDD8C
		private static string CreateDumpName()
		{
			DateTime dateTime = DateTime.Now.ToLocalTime();
			return string.Format("{0}_{1}_{2}_{3}.dmp", new object[]
			{
				Main.dedServ ? "TerrariaServer" : "Terraria",
				Main.versionNumber,
				dateTime.ToString("MM-dd-yy_HH-mm-ss-ffff", CultureInfo.InvariantCulture),
				Thread.CurrentThread.ManagedThreadId
			});
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x004DFBFC File Offset: 0x004DDDFC
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
				result = CrashDump.Write(fileStream.SafeFileHandle, options, exceptionInfo);
			}
			return result;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x004DFC5C File Offset: 0x004DDE5C
		private static bool Write(SafeHandle fileHandle, CrashDump.Options options, CrashDump.ExceptionInfo exceptionInfo)
		{
			if (!Platform.IsWindows)
			{
				return false;
			}
			Process currentProcess = Process.GetCurrentProcess();
			IntPtr handle = currentProcess.Handle;
			uint id = (uint)currentProcess.Id;
			CrashDump.MiniDumpExceptionInformation miniDumpExceptionInformation;
			miniDumpExceptionInformation.ThreadId = CrashDump.GetCurrentThreadId();
			miniDumpExceptionInformation.ClientPointers = false;
			miniDumpExceptionInformation.ExceptionPointers = IntPtr.Zero;
			if (exceptionInfo == CrashDump.ExceptionInfo.Present)
			{
				miniDumpExceptionInformation.ExceptionPointers = Marshal.GetExceptionPointers();
			}
			bool result;
			if (miniDumpExceptionInformation.ExceptionPointers == IntPtr.Zero)
			{
				result = CrashDump.MiniDumpWriteDump(handle, id, fileHandle, (uint)options, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			}
			else
			{
				result = CrashDump.MiniDumpWriteDump(handle, id, fileHandle, (uint)options, ref miniDumpExceptionInformation, IntPtr.Zero, IntPtr.Zero);
			}
			return result;
		}

		// Token: 0x06001908 RID: 6408
		[DllImport("dbghelp.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, ref CrashDump.MiniDumpExceptionInformation expParam, IntPtr userStreamParam, IntPtr callbackParam);

		// Token: 0x06001909 RID: 6409
		[DllImport("dbghelp.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, IntPtr expParam, IntPtr userStreamParam, IntPtr callbackParam);

		// Token: 0x0600190A RID: 6410
		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern uint GetCurrentThreadId();

		// Token: 0x020005AC RID: 1452
		[Flags]
		public enum Options : uint
		{
			// Token: 0x04005A54 RID: 23124
			Normal = 0U,
			// Token: 0x04005A55 RID: 23125
			WithDataSegs = 1U,
			// Token: 0x04005A56 RID: 23126
			WithFullMemory = 2U,
			// Token: 0x04005A57 RID: 23127
			WithHandleData = 4U,
			// Token: 0x04005A58 RID: 23128
			FilterMemory = 8U,
			// Token: 0x04005A59 RID: 23129
			ScanMemory = 16U,
			// Token: 0x04005A5A RID: 23130
			WithUnloadedModules = 32U,
			// Token: 0x04005A5B RID: 23131
			WithIndirectlyReferencedMemory = 64U,
			// Token: 0x04005A5C RID: 23132
			FilterModulePaths = 128U,
			// Token: 0x04005A5D RID: 23133
			WithProcessThreadData = 256U,
			// Token: 0x04005A5E RID: 23134
			WithPrivateReadWriteMemory = 512U,
			// Token: 0x04005A5F RID: 23135
			WithoutOptionalData = 1024U,
			// Token: 0x04005A60 RID: 23136
			WithFullMemoryInfo = 2048U,
			// Token: 0x04005A61 RID: 23137
			WithThreadInfo = 4096U,
			// Token: 0x04005A62 RID: 23138
			WithCodeSegs = 8192U,
			// Token: 0x04005A63 RID: 23139
			WithoutAuxiliaryState = 16384U,
			// Token: 0x04005A64 RID: 23140
			WithFullAuxiliaryState = 32768U,
			// Token: 0x04005A65 RID: 23141
			WithPrivateWriteCopyMemory = 65536U,
			// Token: 0x04005A66 RID: 23142
			IgnoreInaccessibleMemory = 131072U,
			// Token: 0x04005A67 RID: 23143
			ValidTypeFlags = 262143U
		}

		// Token: 0x020005AD RID: 1453
		private enum ExceptionInfo
		{
			// Token: 0x04005A69 RID: 23145
			None,
			// Token: 0x04005A6A RID: 23146
			Present
		}

		// Token: 0x020005AE RID: 1454
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		private struct MiniDumpExceptionInformation
		{
			// Token: 0x04005A6B RID: 23147
			public uint ThreadId;

			// Token: 0x04005A6C RID: 23148
			public IntPtr ExceptionPointers;

			// Token: 0x04005A6D RID: 23149
			[MarshalAs(UnmanagedType.Bool)]
			public bool ClientPointers;
		}
	}
}
