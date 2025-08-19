using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Terraria.ModLoader.Engine
{
	// Token: 0x020002B8 RID: 696
	internal class NativeLibraries
	{
		// Token: 0x06002D3C RID: 11580 RVA: 0x0052D1C8 File Offset: 0x0052B3C8
		internal static void CheckNativeFAudioDependencies()
		{
			if (!OperatingSystem.IsWindows())
			{
				return;
			}
			try
			{
				NativeLibrary.Load("vcruntime140.dll", Assembly.GetExecutingAssembly(), new DllImportSearchPath?(DllImportSearchPath.System32));
			}
			catch (DllNotFoundException e)
			{
				e.HelpLink = "https://www.microsoft.com/en-us/download/details.aspx?id=53587";
				ErrorReporting.FatalExit("Microsoft Visual C++ 2015 Redistributable Update 3 is missing. You will need to download and install it from the Microsoft website.", e);
			}
			catch (Exception e2)
			{
				ErrorReporting.FatalExit("vcruntime140.dll: Unexpected failure in verifying dependency. Please reach out in the tModLoader Discord for support", e2);
			}
			try
			{
				NativeLibrary.Load("mfplat.dll", Assembly.GetExecutingAssembly(), new DllImportSearchPath?(DllImportSearchPath.System32));
			}
			catch (DllNotFoundException e3)
			{
				e3.HelpLink = "https://support.microsoft.com/en-us/topic/media-feature-pack-list-for-windows-n-editions-c1c6fffa-d052-8338-7a79-a4bb980a700a";
				ErrorReporting.FatalExit("Windows Versions N and KN are missing some media features.\n\nFollow the instructions in the Microsoft website\n\nSearch \"Media Feature Pack list for Windows N editions\" if the page doesn't open automatically.", e3);
			}
			catch (BadImageFormatException ex)
			{
				ex.HelpLink = "https://support.microsoft.com/en-us/topic/media-feature-pack-list-for-windows-n-editions-c1c6fffa-d052-8338-7a79-a4bb980a700a";
				ErrorReporting.FatalExit("https://support.microsoft.com/en-us/topic/media-feature-pack-list-for-windows-n-editions-c1c6fffa-d052-8338-7a79-a4bb980a700a\n\nIf this doesn't work try Search \"MFPlat.DLL is either not designed to run on Windows\" and follow those instructions");
			}
			catch (Exception e4)
			{
				ErrorReporting.FatalExit("mfplat.dll: Unexpected failure in verifying dependency. Please reach out in the tModLoader Discord for support", e4);
			}
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x0052D2BC File Offset: 0x0052B4BC
		internal static void SetNativeLibraryPath(string nativesDir)
		{
			if (!OperatingSystem.IsWindows())
			{
				return;
			}
			try
			{
				NativeLibraries.SetDefaultDllDirectories(4096);
				NativeLibraries.AddDllDirectory(nativesDir);
			}
			catch
			{
				NativeLibraries.SetDllDirectory(nativesDir);
			}
		}

		// Token: 0x06002D3E RID: 11582
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetDefaultDllDirectories(int directoryFlags);

		// Token: 0x06002D3F RID: 11583
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern void AddDllDirectory(string lpPathName);

		// Token: 0x06002D40 RID: 11584
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetDllDirectory(string lpPathName);

		// Token: 0x04001C37 RID: 7223
		private const string WindowsVersionNDesc = "Windows Versions N and KN are missing some media features.\n\nFollow the instructions in the Microsoft website\n\nSearch \"Media Feature Pack list for Windows N editions\" if the page doesn't open automatically.";

		// Token: 0x04001C38 RID: 7224
		private const string WindowsVersionNUrl = "https://support.microsoft.com/en-us/topic/media-feature-pack-list-for-windows-n-editions-c1c6fffa-d052-8338-7a79-a4bb980a700a";

		// Token: 0x04001C39 RID: 7225
		private const string FailedDependency = "Unexpected failure in verifying dependency. Please reach out in the tModLoader Discord for support";

		// Token: 0x04001C3A RID: 7226
		private const int LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 4096;
	}
}
