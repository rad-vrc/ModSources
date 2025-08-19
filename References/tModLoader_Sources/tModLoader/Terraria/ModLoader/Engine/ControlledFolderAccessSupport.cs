using System;
using System.Runtime.Versioning;
using Microsoft.Win32;

namespace Terraria.ModLoader.Engine
{
	// Token: 0x020002AB RID: 683
	internal class ControlledFolderAccessSupport
	{
		// Token: 0x06002CFB RID: 11515 RVA: 0x0052B3A4 File Offset: 0x005295A4
		internal static void CheckFileSystemAccess()
		{
			try
			{
				if (OperatingSystem.IsWindows() && Environment.OSVersion.Version.Major >= 10)
				{
					ControlledFolderAccessSupport.CheckRegistryValues();
				}
			}
			catch
			{
				ControlledFolderAccessSupport.ControlledFolderAccessDetectionPrevented = true;
			}
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x0052B3EC File Offset: 0x005295EC
		[SupportedOSPlatform("windows")]
		private static void CheckRegistryValues()
		{
			object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows Defender\\Windows Defender Exploit Guard\\Controlled Folder Access", "EnableControlledFolderAccess", -1);
			if (value is int)
			{
				int EnableControlledFolderAccessValue = (int)value;
				ControlledFolderAccessSupport.ControlledFolderAccessDetected = (EnableControlledFolderAccessValue == 1);
			}
		}

		// Token: 0x04001C16 RID: 7190
		internal static bool ControlledFolderAccessDetected;

		// Token: 0x04001C17 RID: 7191
		internal static bool ControlledFolderAccessDetectionPrevented;
	}
}
