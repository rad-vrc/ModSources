using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Win32;

namespace Terraria.ModLoader.Engine
{
	// Token: 0x020002AF RID: 687
	internal class FileAssociationSupport
	{
		// Token: 0x06002D08 RID: 11528 RVA: 0x0052BA44 File Offset: 0x00529C44
		internal static void HandleFileAssociation(string file)
		{
			Console.WriteLine("Attempting to install " + file);
			if (File.Exists(file))
			{
				string modName = Path.GetFileNameWithoutExtension(file);
				if (ModLoader.ModPath != Path.GetDirectoryName(file))
				{
					File.Copy(file, Path.Combine(ModLoader.ModPath, Path.GetFileName(file)), true);
					File.Delete(file);
					Console.WriteLine(modName + " installed successfully");
				}
				ModLoader.EnableMod(modName);
				Console.WriteLine(modName + " enabled");
			}
			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
			Environment.Exit(0);
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x0052BADC File Offset: 0x00529CDC
		internal static void UpdateFileAssociation()
		{
			if (OperatingSystem.IsWindows() && Environment.OSVersion.Version.Major >= 6)
			{
				try
				{
					FileAssociationSupport.EnsureAssociationsSet();
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06002D0A RID: 11530
		[DllImport("Shell32.dll")]
		private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

		// Token: 0x06002D0B RID: 11531 RVA: 0x0052BB1C File Offset: 0x00529D1C
		[SupportedOSPlatform("windows")]
		private static void EnsureAssociationsSet()
		{
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "tModLoader.dll");
			FileAssociationSupport.EnsureAssociationsSet(new FileAssociationSupport.FileAssociation[]
			{
				new FileAssociationSupport.FileAssociation
				{
					Extension = ".tmod",
					ProgId = "tModLoader_Mod_File",
					FileTypeDescription = "tModLoader Mod",
					ExecutableFilePath = filePath
				}
			});
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x0052BB74 File Offset: 0x00529D74
		[SupportedOSPlatform("windows")]
		private static void EnsureAssociationsSet(params FileAssociationSupport.FileAssociation[] associations)
		{
			bool madeChanges = false;
			foreach (FileAssociationSupport.FileAssociation association in associations)
			{
				madeChanges |= FileAssociationSupport.SetAssociation(association.Extension, association.ProgId, association.FileTypeDescription, association.ExecutableFilePath);
			}
			if (madeChanges)
			{
				FileAssociationSupport.SHChangeNotify(134217728, 4096, IntPtr.Zero, IntPtr.Zero);
			}
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x0052BBD4 File Offset: 0x00529DD4
		[SupportedOSPlatform("windows")]
		private static bool SetAssociation(string extension, string progId, string fileTypeDescription, string applicationFilePath)
		{
			return false | FileAssociationSupport.SetKeyDefaultValue("Software\\Classes\\" + extension, progId) | FileAssociationSupport.SetKeyDefaultValue("Software\\Classes\\" + progId, fileTypeDescription) | FileAssociationSupport.SetKeyDefaultValue("Software\\Classes\\" + progId + "\\shell\\open\\command", "dotnet \"" + applicationFilePath + "\" -server -install \"%1\"");
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x0052BC2C File Offset: 0x00529E2C
		[SupportedOSPlatform("windows")]
		private static bool SetKeyDefaultValue(string keyPath, string value)
		{
			using (RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath))
			{
				if (key.GetValue(null) as string != value)
				{
					key.SetValue(null, value);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001C1C RID: 7196
		private const int SHCNE_ASSOCCHANGED = 134217728;

		// Token: 0x04001C1D RID: 7197
		private const int SHCNF_FLUSH = 4096;

		// Token: 0x02000A55 RID: 2645
		private class FileAssociation
		{
			// Token: 0x17000904 RID: 2308
			// (get) Token: 0x06005885 RID: 22661 RVA: 0x0069FF93 File Offset: 0x0069E193
			// (set) Token: 0x06005886 RID: 22662 RVA: 0x0069FF9B File Offset: 0x0069E19B
			public string Extension { get; set; }

			// Token: 0x17000905 RID: 2309
			// (get) Token: 0x06005887 RID: 22663 RVA: 0x0069FFA4 File Offset: 0x0069E1A4
			// (set) Token: 0x06005888 RID: 22664 RVA: 0x0069FFAC File Offset: 0x0069E1AC
			public string ProgId { get; set; }

			// Token: 0x17000906 RID: 2310
			// (get) Token: 0x06005889 RID: 22665 RVA: 0x0069FFB5 File Offset: 0x0069E1B5
			// (set) Token: 0x0600588A RID: 22666 RVA: 0x0069FFBD File Offset: 0x0069E1BD
			public string FileTypeDescription { get; set; }

			// Token: 0x17000907 RID: 2311
			// (get) Token: 0x0600588B RID: 22667 RVA: 0x0069FFC6 File Offset: 0x0069E1C6
			// (set) Token: 0x0600588C RID: 22668 RVA: 0x0069FFCE File Offset: 0x0069E1CE
			public string ExecutableFilePath { get; set; }
		}
	}
}
