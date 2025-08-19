using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria.ModLoader.Engine
{
	// Token: 0x020002B7 RID: 695
	internal static class NativeExceptionHandling
	{
		// Token: 0x06002D37 RID: 11575 RVA: 0x0052D05F File Offset: 0x0052B25F
		internal static void Init()
		{
			if (!OperatingSystem.IsWindows())
			{
				return;
			}
			NativeExceptionHandling.UnhandledExceptionFilter unhandledExceptionFilter;
			if ((unhandledExceptionFilter = NativeExceptionHandling.<>O.<0>__OurUnhandledExceptionFilter) == null)
			{
				unhandledExceptionFilter = (NativeExceptionHandling.<>O.<0>__OurUnhandledExceptionFilter = new NativeExceptionHandling.UnhandledExceptionFilter(NativeExceptionHandling.OurUnhandledExceptionFilter));
			}
			NativeExceptionHandling.SetUnhandledExceptionFilter(unhandledExceptionFilter);
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x0052D08C File Offset: 0x0052B28C
		private static IntPtr OurUnhandledExceptionFilter(IntPtr exceptionInfo)
		{
			Logging.tML.Fatal("Native exception has occurred, attempting to determine erroring module...");
			IntPtr exceptionAddress = Marshal.PtrToStructure<NativeExceptionHandling.EXCEPTION_RECORD>(Marshal.PtrToStructure<NativeExceptionHandling.EXCEPTION_POINTERS>(exceptionInfo).ExceptionRecord).ExceptionAddress;
			IntPtr moduleHandle;
			if (NativeExceptionHandling.GetModuleHandleEx(6U, exceptionAddress, out moduleHandle) && moduleHandle != IntPtr.Zero)
			{
				StringBuilder moduleName = new StringBuilder(260);
				NativeExceptionHandling.GetModuleFileName(moduleHandle, moduleName, moduleName.Capacity);
				Logging.tML.Fatal("Exception occurred in module: " + moduleName.ToString());
			}
			else
			{
				Logging.tML.Fatal("Failed to retrieve module information.");
			}
			Logging.tML.Fatal("Attempting to save minidump...");
			CrashDump.Options dumpOptions = CrashDump.Options.WithThreadInfo;
			Main instance = Main.instance;
			bool flag;
			if (instance == null)
			{
				flag = false;
			}
			else
			{
				LaunchParameters launchParameters = instance.LaunchParameters;
				bool? flag2 = (launchParameters != null) ? new bool?(launchParameters.ContainsKey("-fulldump")) : null;
				bool flag3 = true;
				flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
			}
			if (flag)
			{
				dumpOptions = CrashDump.Options.WithFullMemory;
			}
			string minidumpPath = CrashDump.WriteExceptionAsZipAndClearOld(dumpOptions, exceptionInfo);
			if (minidumpPath == null)
			{
				Logging.tML.Fatal("Minidump saving failed, either this isn't Windows or the logs folder could not be created.");
			}
			else
			{
				Logging.tML.Fatal("Minidump saved to: '" + Path.GetFullPath(minidumpPath) + "'");
				Logging.tML.Fatal("This file can be provided to tModLoader developers to help diagnose the issue.");
			}
			return (IntPtr)1;
		}

		// Token: 0x06002D39 RID: 11577
		[DllImport("kernel32.dll")]
		private static extern IntPtr SetUnhandledExceptionFilter(NativeExceptionHandling.UnhandledExceptionFilter filter);

		// Token: 0x06002D3A RID: 11578
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool GetModuleHandleEx(uint dwFlags, IntPtr lpModuleName, out IntPtr phModule);

		// Token: 0x06002D3B RID: 11579
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern uint GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);

		// Token: 0x04001C35 RID: 7221
		private const uint GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS = 4U;

		// Token: 0x04001C36 RID: 7222
		private const uint GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT = 2U;

		// Token: 0x02000A6A RID: 2666
		private struct EXCEPTION_RECORD
		{
			// Token: 0x04006D05 RID: 27909
			public uint ExceptionCode;

			// Token: 0x04006D06 RID: 27910
			public uint ExceptionFlags;

			// Token: 0x04006D07 RID: 27911
			public IntPtr ExceptionRecord;

			// Token: 0x04006D08 RID: 27912
			public IntPtr ExceptionAddress;

			// Token: 0x04006D09 RID: 27913
			public uint NumberParameters;

			// Token: 0x04006D0A RID: 27914
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
			public IntPtr[] ExceptionInformation;
		}

		// Token: 0x02000A6B RID: 2667
		private struct EXCEPTION_POINTERS
		{
			// Token: 0x04006D0B RID: 27915
			public IntPtr ExceptionRecord;

			// Token: 0x04006D0C RID: 27916
			public IntPtr ContextRecord;
		}

		// Token: 0x02000A6C RID: 2668
		// (Invoke) Token: 0x060058E8 RID: 22760
		private delegate IntPtr UnhandledExceptionFilter(IntPtr exceptionInfo);

		// Token: 0x02000A6D RID: 2669
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006D0D RID: 27917
			public static NativeExceptionHandling.UnhandledExceptionFilter <0>__OurUnhandledExceptionFilter;
		}
	}
}
