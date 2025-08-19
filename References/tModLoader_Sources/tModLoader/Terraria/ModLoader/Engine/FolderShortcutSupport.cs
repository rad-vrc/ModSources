using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Versioning;
using System.Text;
using log4net;

namespace Terraria.ModLoader.Engine
{
	// Token: 0x020002B2 RID: 690
	internal class FolderShortcutSupport
	{
		// Token: 0x06002D1A RID: 11546 RVA: 0x0052C234 File Offset: 0x0052A434
		internal static void UpdateFolderShortcuts()
		{
			if (OperatingSystem.IsWindows())
			{
				try
				{
					FolderShortcutSupport.CreateLogsFolderShortcut();
				}
				catch (Exception ex) when (ex is COMException || ex is FileNotFoundException)
				{
					if (ControlledFolderAccessSupport.ControlledFolderAccessDetected)
					{
						Utils.OpenToURL("https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Usage-FAQ#controlled-folder-access");
						throw new Exception("\n\nControlled Folder Access feature detected.\n\nIf game fails to launch make sure to add \"" + Environment.ProcessPath + "\" to the \"Allow an app through Controlled folder access\" menu found in the \"Ransomware protection\" menu.\n\nMore instructions can be found in the website that just opened.\n\n");
					}
				}
				catch (Exception e)
				{
					ILog tML = Logging.tML;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(79, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Unable to create logs folder shortcuts - an exception of type ");
					defaultInterpolatedStringHandler.AppendFormatted(e.GetType().Name);
					defaultInterpolatedStringHandler.AppendLiteral(" was thrown:\r\n'");
					defaultInterpolatedStringHandler.AppendFormatted(e.Message);
					defaultInterpolatedStringHandler.AppendLiteral("'.");
					tML.Warn(defaultInterpolatedStringHandler.ToStringAndClear());
				}
			}
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x0052C328 File Offset: 0x0052A528
		[SupportedOSPlatform("windows")]
		private static void CreateLogsFolderShortcut()
		{
			FolderShortcutSupport.IShellLink shellLink = (FolderShortcutSupport.IShellLink)new FolderShortcutSupport.ShellLink();
			shellLink.SetDescription("tModLoader Logs Folder");
			string path = Path.GetFullPath(Logging.LogDir);
			shellLink.SetPath(path);
			IPersistFile persistFile = (IPersistFile)shellLink;
			string fullSavePath = Path.GetFullPath(Program.SavePath);
			Directory.CreateDirectory(fullSavePath);
			persistFile.Save(Path.Combine(fullSavePath, "Logs.lnk"), false);
		}

		// Token: 0x02000A5C RID: 2652
		[Guid("00021401-0000-0000-C000-000000000046")]
		[ComImport]
		internal class ShellLink
		{
			// Token: 0x060058A4 RID: 22692
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern ShellLink();
		}

		// Token: 0x02000A5D RID: 2653
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("000214F9-0000-0000-C000-000000000046")]
		[ComImport]
		internal interface IShellLink
		{
			// Token: 0x060058A5 RID: 22693
			void GetPath([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);

			// Token: 0x060058A6 RID: 22694
			void GetIDList(out IntPtr ppidl);

			// Token: 0x060058A7 RID: 22695
			void SetIDList(IntPtr pidl);

			// Token: 0x060058A8 RID: 22696
			void GetDescription([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszName, int cchMaxName);

			// Token: 0x060058A9 RID: 22697
			void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

			// Token: 0x060058AA RID: 22698
			void GetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszDir, int cchMaxPath);

			// Token: 0x060058AB RID: 22699
			void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

			// Token: 0x060058AC RID: 22700
			void GetArguments([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszArgs, int cchMaxPath);

			// Token: 0x060058AD RID: 22701
			void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

			// Token: 0x060058AE RID: 22702
			void GetHotkey(out short pwHotkey);

			// Token: 0x060058AF RID: 22703
			void SetHotkey(short wHotkey);

			// Token: 0x060058B0 RID: 22704
			void GetShowCmd(out int piShowCmd);

			// Token: 0x060058B1 RID: 22705
			void SetShowCmd(int iShowCmd);

			// Token: 0x060058B2 RID: 22706
			void GetIconLocation([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

			// Token: 0x060058B3 RID: 22707
			void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

			// Token: 0x060058B4 RID: 22708
			void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);

			// Token: 0x060058B5 RID: 22709
			void Resolve(IntPtr hwnd, int fFlags);

			// Token: 0x060058B6 RID: 22710
			void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
		}
	}
}
