using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using MonoMod.RuntimeDetour;

namespace Terraria.ModLoader.Engine
{
	// Token: 0x020002B6 RID: 694
	internal static class LoggingHooks
	{
		// Token: 0x06002D2F RID: 11567 RVA: 0x0052CDB8 File Offset: 0x0052AFB8
		internal static void Init()
		{
			LoggingHooks.FixBrokenConsolePipeError();
			LoggingHooks.PrettifyStackTraceSources();
			LoggingHooks.HookWebRequests();
			LoggingHooks.HookProcessStart();
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x0052CDD0 File Offset: 0x0052AFD0
		private static void FixBrokenConsolePipeError()
		{
			if (!OperatingSystem.IsWindows())
			{
				return;
			}
			LoggingHooks.writeFileNativeHook = new Hook(typeof(Console).Assembly.GetType("System.ConsolePal").GetNestedType("WindowsConsoleStream", BindingFlags.NonPublic).GetMethod("WriteFileNative", BindingFlags.Static | BindingFlags.NonPublic), new LoggingHooks.hook_WriteFileNative(delegate(LoggingHooks.orig_WriteFileNative orig, IntPtr hFile, ReadOnlySpan<byte> bytes, bool useFileAPIs)
			{
				int ret = orig(hFile, bytes, useFileAPIs);
				if (ret == 233)
				{
					return 0;
				}
				return ret;
			}));
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x0052CE40 File Offset: 0x0052B040
		private static void HookProcessStart()
		{
			LoggingHooks.processStartHook = new Hook(typeof(Process).GetMethod("Start", BindingFlags.Instance | BindingFlags.Public), new Func<Func<Process, bool>, Process, bool>(delegate(Func<Process, bool> orig, Process self)
			{
				ILog tML = Logging.tML;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 3);
				defaultInterpolatedStringHandler.AppendLiteral("Process.Start (UseShellExecute = ");
				defaultInterpolatedStringHandler.AppendFormatted<bool>(self.StartInfo.UseShellExecute);
				defaultInterpolatedStringHandler.AppendLiteral("): \"");
				defaultInterpolatedStringHandler.AppendFormatted(self.StartInfo.FileName);
				defaultInterpolatedStringHandler.AppendLiteral("\" ");
				defaultInterpolatedStringHandler.AppendFormatted(self.StartInfo.Arguments);
				tML.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
				return orig(self);
			}));
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x0052CE8C File Offset: 0x0052B08C
		private static void Hook_StackTrace_CaptureStackTrace(LoggingHooks.orig_StackTrace_CaptureStackTrace orig, StackTrace self, int skipFrames, bool fNeedFileInfo, Exception e)
		{
			if (e == null)
			{
				skipFrames += 3;
			}
			orig(self, skipFrames, fNeedFileInfo, e);
			if (fNeedFileInfo)
			{
				Logging.PrettifyStackTraceSources(self.GetFrames());
			}
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x0052CEB0 File Offset: 0x0052B0B0
		private static void Hook_GetSourceLineInfo(LoggingHooks.orig_GetSourceLineInfo orig, object self, Assembly assembly, string assemblyPath, IntPtr loadedPeAddress, int loadedPeSize, bool isFileLayout, IntPtr inMemoryPdbAddress, int inMemoryPdbSize, int methodToken, int ilOffset, [Nullable(2)] out string sourceFile, out int sourceLine, out int sourceColumn)
		{
			try
			{
				using (new Logging.QuietExceptionHandle())
				{
					orig(self, assembly, assemblyPath, loadedPeAddress, loadedPeSize, isFileLayout, inMemoryPdbAddress, inMemoryPdbSize, methodToken, ilOffset, out sourceFile, out sourceLine, out sourceColumn);
				}
			}
			catch (BadImageFormatException obj) when (assembly.FullName.StartsWith("MonoMod.Utils"))
			{
				sourceFile = null;
				sourceLine = 0;
				sourceColumn = 0;
			}
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x0052CF3C File Offset: 0x0052B13C
		private static void PrettifyStackTraceSources()
		{
			LoggingHooks.stackTrace_CaptureStackTrace = new Hook(typeof(StackTrace).GetMethod("CaptureStackTrace", BindingFlags.Instance | BindingFlags.NonPublic), new LoggingHooks.hook_StackTrace_CaptureStackTrace(LoggingHooks.Hook_StackTrace_CaptureStackTrace));
			LoggingHooks.stackTraceSymbols_GetSourceLineInfo = new Hook(Assembly.Load("System.Diagnostics.StackTrace").GetType("System.Diagnostics.StackTraceSymbols").GetMethod("GetSourceLineInfo", BindingFlags.Instance | BindingFlags.NonPublic), new LoggingHooks.hook_GetSourceLineInfo(LoggingHooks.Hook_GetSourceLineInfo));
		}

		/// <summary>
		/// Attempt to hook the .NET internal methods to log when requests are sent to web addresses.
		/// Use the right internal methods to capture redirects
		/// </summary>
		// Token: 0x06002D35 RID: 11573 RVA: 0x0052CFAC File Offset: 0x0052B1AC
		private static void HookWebRequests()
		{
			try
			{
				Type type = typeof(HttpClient).Assembly.GetType("System.Net.Http.HttpConnectionPoolManager");
				MethodInfo sendAsyncCoreMethodInfo = (type != null) ? type.GetMethod("SendAsyncCore", BindingFlags.Instance | BindingFlags.Public) : null;
				if (sendAsyncCoreMethodInfo != null)
				{
					LoggingHooks.httpSendAsyncHook = new Hook(sendAsyncCoreMethodInfo, new LoggingHooks.hook_SendAsyncCore(delegate(LoggingHooks.orig_SendAsyncCore orig, object self, HttpRequestMessage request, [Nullable(2)] Uri proxyUri, bool async, bool doRequestAuth, bool isProxyConnect, CancellationToken cancellationToken)
					{
						if (LoggingHooks.IncludeURIInRequestLogging(request.RequestUri))
						{
							ILog tML = Logging.tML;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 1);
							defaultInterpolatedStringHandler.AppendLiteral("Web Request: ");
							defaultInterpolatedStringHandler.AppendFormatted<Uri>(request.RequestUri);
							tML.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
						}
						return orig(self, request, proxyUri, async, doRequestAuth, isProxyConnect, cancellationToken);
					}));
					return;
				}
			}
			catch
			{
			}
			Logging.tML.Warn("HttpWebRequest send/submit method not found");
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x0052D040 File Offset: 0x0052B240
		private static bool IncludeURIInRequestLogging(Uri uri)
		{
			return !uri.IsLoopback || !uri.LocalPath.Contains("game_");
		}

		// Token: 0x04001C30 RID: 7216
		private static Hook writeFileNativeHook;

		// Token: 0x04001C31 RID: 7217
		private static Hook processStartHook;

		// Token: 0x04001C32 RID: 7218
		private static Hook stackTrace_CaptureStackTrace;

		// Token: 0x04001C33 RID: 7219
		private static Hook stackTraceSymbols_GetSourceLineInfo;

		// Token: 0x04001C34 RID: 7220
		private static Hook httpSendAsyncHook;

		// Token: 0x02000A61 RID: 2657
		// (Invoke) Token: 0x060058C3 RID: 22723
		private delegate int orig_WriteFileNative(IntPtr hFile, ReadOnlySpan<byte> bytes, bool useFileAPIs);

		// Token: 0x02000A62 RID: 2658
		// (Invoke) Token: 0x060058C7 RID: 22727
		private delegate int hook_WriteFileNative(LoggingHooks.orig_WriteFileNative orig, IntPtr hFile, ReadOnlySpan<byte> bytes, bool useFileAPIs);

		// Token: 0x02000A63 RID: 2659
		// (Invoke) Token: 0x060058CB RID: 22731
		private delegate void orig_StackTrace_CaptureStackTrace(StackTrace self, int skipFrames, bool fNeedFileInfo, Exception e);

		// Token: 0x02000A64 RID: 2660
		// (Invoke) Token: 0x060058CF RID: 22735
		private delegate void hook_StackTrace_CaptureStackTrace(LoggingHooks.orig_StackTrace_CaptureStackTrace orig, StackTrace self, int skipFrames, bool fNeedFileInfo, Exception e);

		// Token: 0x02000A65 RID: 2661
		// (Invoke) Token: 0x060058D3 RID: 22739
		private delegate void orig_GetSourceLineInfo(object self, Assembly assembly, string assemblyPath, IntPtr loadedPeAddress, int loadedPeSize, bool isFileLayout, IntPtr inMemoryPdbAddress, int inMemoryPdbSize, int methodToken, int ilOffset, [Nullable(2)] out string sourceFile, out int sourceLine, out int sourceColumn);

		// Token: 0x02000A66 RID: 2662
		// (Invoke) Token: 0x060058D7 RID: 22743
		private delegate void hook_GetSourceLineInfo(LoggingHooks.orig_GetSourceLineInfo orig, object self, Assembly assembly, string assemblyPath, IntPtr loadedPeAddress, int loadedPeSize, bool isFileLayout, IntPtr inMemoryPdbAddress, int inMemoryPdbSize, int methodToken, int ilOffset, [Nullable(2)] out string sourceFile, out int sourceLine, out int sourceColumn);

		// Token: 0x02000A67 RID: 2663
		// (Invoke) Token: 0x060058DB RID: 22747
		private delegate ValueTask<HttpResponseMessage> orig_SendAsyncCore(object self, HttpRequestMessage request, [Nullable(2)] Uri proxyUri, bool async, bool doRequestAuth, bool isProxyConnect, CancellationToken cancellationToken);

		// Token: 0x02000A68 RID: 2664
		// (Invoke) Token: 0x060058DF RID: 22751
		private delegate ValueTask<HttpResponseMessage> hook_SendAsyncCore(LoggingHooks.orig_SendAsyncCore orig, object self, HttpRequestMessage request, [Nullable(2)] Uri proxyUri, bool async, bool doRequestAuth, bool isProxyConnect, CancellationToken cancellationToken);
	}
}
