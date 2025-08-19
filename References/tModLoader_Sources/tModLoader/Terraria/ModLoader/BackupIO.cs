using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zip;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria.ModLoader
{
	// Token: 0x02000183 RID: 387
	internal static class BackupIO
	{
		// Token: 0x06001E3F RID: 7743 RVA: 0x004D5668 File Offset: 0x004D3868
		private static bool IsArchiveOlder(DateTime time, TimeSpan thresholdAge)
		{
			return DateTime.Now - time > thresholdAge;
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x004D567B File Offset: 0x004D387B
		private static string GetArchiveName(string name, bool isCloudSave)
		{
			return name + (isCloudSave ? "-cloud" : "");
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x004D5694 File Offset: 0x004D3894
		private static string TodaysBackup(string name, bool isCloudSave)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
			defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, "yyyy-MM-dd");
			defaultInterpolatedStringHandler.AppendLiteral("-");
			defaultInterpolatedStringHandler.AppendFormatted(BackupIO.GetArchiveName(name, isCloudSave));
			defaultInterpolatedStringHandler.AppendLiteral(".zip");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x004D56E8 File Offset: 0x004D38E8
		private static bool TryGetTime(string file, out DateTime result)
		{
			Match match = BackupIO.dateRegex.Match(file);
			result = default(DateTime);
			return match.Success && DateTime.TryParse(match.Value, out result);
		}

		/// <summary>
		/// Run a given archiving task, which will archive to a backup .zip file
		/// Zip entries added will be compressed
		/// </summary>
		// Token: 0x06001E43 RID: 7747 RVA: 0x004D5720 File Offset: 0x004D3920
		private static void RunArchiving(Action<ZipFile, bool, string> saveAction, bool isCloudSave, string dir, string name, string path)
		{
			try
			{
				Directory.CreateDirectory(dir);
				BackupIO.DeleteOldArchives(dir, isCloudSave, name);
				using (ZipFile zip = new ZipFile(Path.Combine(dir, BackupIO.TodaysBackup(name, isCloudSave)), Encoding.UTF8))
				{
					zip.UseZip64WhenSaving = 1;
					zip.ZipErrorAction = 0;
					saveAction(zip, isCloudSave, path);
					zip.Save();
				}
			}
			catch (Exception e)
			{
				Logging.tML.Error("A problem occurred when trying to create a backup file.", e);
			}
		}

		/// <summary>
		/// Adds a new entry to the archive .zip file
		/// Will use the best compression level using Deflate
		/// Some files are already compressed and will not be compressed further
		/// </summary>
		// Token: 0x06001E44 RID: 7748 RVA: 0x004D57B0 File Offset: 0x004D39B0
		private static void AddZipEntry(this ZipFile zip, string path, bool isCloud = false)
		{
			zip.CompressionMethod = 8;
			zip.CompressionLevel = 9;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Archived on $");
			defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now);
			defaultInterpolatedStringHandler.AppendLiteral(" by tModLoader");
			zip.Comment = defaultInterpolatedStringHandler.ToStringAndClear();
			if (!isCloud && (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
			{
				zip.AddFiles(Directory.GetFiles(path), false, Path.GetFileNameWithoutExtension(path));
				return;
			}
			if (isCloud)
			{
				zip.AddEntry(Path.GetFileName(path), FileUtilities.ReadAllBytes(path, true));
				return;
			}
			zip.AddFile(path, "");
		}

		/// <summary>
		/// Will delete old archive files
		/// Algorithm details:
		/// - One backup per day for the last week
		/// - One backup per week for the last month
		/// - One backup per month for all time
		/// </summary>
		// Token: 0x06001E45 RID: 7749 RVA: 0x004D5850 File Offset: 0x004D3A50
		private static void DeleteOldArchives(string dir, bool isCloudSave, string name)
		{
			string path = Path.Combine(dir, BackupIO.TodaysBackup(name, isCloudSave));
			if (File.Exists(path))
			{
				BackupIO.DeleteArchive(path);
			}
			ValueTuple<FileInfo, DateTime>[] array = (from tuple in new DirectoryInfo(dir).GetFiles("*" + BackupIO.GetArchiveName(name, isCloudSave) + "*.zip", SearchOption.TopDirectoryOnly).Select(delegate(FileInfo f)
			{
				DateTime date;
				return new ValueTuple<FileInfo, DateTime>(f, BackupIO.TryGetTime(f.Name, out date) ? date : default(DateTime));
			})
			where tuple.Item2 != default(DateTime)
			orderby tuple.Item2
			select tuple).ToArray<ValueTuple<FileInfo, DateTime>>();
			ValueTuple<FileInfo, DateTime>? previous = null;
			foreach (ValueTuple<FileInfo, DateTime> archived in array)
			{
				if (previous == null)
				{
					previous = new ValueTuple<FileInfo, DateTime>?(archived);
				}
				else
				{
					int freshness;
					if (BackupIO.IsArchiveOlder(archived.Item2, TimeSpan.FromDays(30.0)))
					{
						freshness = 30;
					}
					else if (BackupIO.IsArchiveOlder(archived.Item2, TimeSpan.FromDays(7.0)))
					{
						freshness = 7;
					}
					else
					{
						freshness = 1;
					}
					if ((archived.Item2 - previous.Value.Item2).Days < freshness)
					{
						BackupIO.DeleteArchive(previous.Value.Item1.FullName);
					}
					previous = new ValueTuple<FileInfo, DateTime>?(archived);
				}
			}
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x004D59D4 File Offset: 0x004D3BD4
		private static void DeleteArchive(string path)
		{
			try
			{
				File.Delete(path);
			}
			catch (Exception e)
			{
				Logging.tML.Error("Problem deleting old archive file", e);
			}
		}

		// Token: 0x040015D0 RID: 5584
		public static bool archiveLock = false;

		// Token: 0x040015D1 RID: 5585
		private static readonly Regex dateRegex = new Regex("\\d+-\\d\\d*-\\d\\d*", RegexOptions.Compiled);

		/// <summary>
		/// Responsible for archiving world backups
		/// </summary>
		// Token: 0x020008D9 RID: 2265
		public static class World
		{
			// Token: 0x06005287 RID: 21127 RVA: 0x00698ECF File Offset: 0x006970CF
			internal static void ArchiveWorld(string path, bool isCloudSave)
			{
				Action<ZipFile, bool, string> saveAction;
				if ((saveAction = BackupIO.World.<>O.<0>__WriteArchive) == null)
				{
					saveAction = (BackupIO.World.<>O.<0>__WriteArchive = new Action<ZipFile, bool, string>(BackupIO.World.WriteArchive));
				}
				BackupIO.RunArchiving(saveAction, isCloudSave, BackupIO.World.WorldBackupDir, Path.GetFileNameWithoutExtension(path), path);
			}

			// Token: 0x06005288 RID: 21128 RVA: 0x00698EFE File Offset: 0x006970FE
			private static void WriteArchive(ZipFile zip, bool isCloudSave, string path)
			{
				if (FileUtilities.Exists(path, isCloudSave))
				{
					zip.AddZipEntry(path, isCloudSave);
				}
				path = Path.ChangeExtension(path, ".twld");
				if (FileUtilities.Exists(path, isCloudSave))
				{
					zip.AddZipEntry(path, isCloudSave);
				}
			}

			// Token: 0x04006A7E RID: 27262
			public static readonly string WorldDir = Path.Combine(Main.SavePath, "Worlds");

			// Token: 0x04006A7F RID: 27263
			public static readonly string WorldBackupDir = Path.Combine(BackupIO.World.WorldDir, "Backups");

			// Token: 0x02000E1C RID: 3612
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04007C3C RID: 31804
				public static Action<ZipFile, bool, string> <0>__WriteArchive;
			}
		}

		/// <summary>
		/// Responsible for archiving player backups
		/// </summary>
		// Token: 0x020008DA RID: 2266
		public static class Player
		{
			// Token: 0x0600528A RID: 21130 RVA: 0x00698F59 File Offset: 0x00697159
			public static void ArchivePlayer(string path, bool isCloudSave)
			{
				Action<ZipFile, bool, string> saveAction;
				if ((saveAction = BackupIO.Player.<>O.<0>__WriteArchive) == null)
				{
					saveAction = (BackupIO.Player.<>O.<0>__WriteArchive = new Action<ZipFile, bool, string>(BackupIO.Player.WriteArchive));
				}
				BackupIO.RunArchiving(saveAction, isCloudSave, BackupIO.Player.PlayerBackupDir, Path.GetFileNameWithoutExtension(path), path);
			}

			/// <summary>
			/// Write the archive. Writes the .plr and .tplr files, then writes the player directory
			/// </summary>
			// Token: 0x0600528B RID: 21131 RVA: 0x00698F88 File Offset: 0x00697188
			private static void WriteArchive(ZipFile zip, bool isCloudSave, string path)
			{
				if (FileUtilities.Exists(path, isCloudSave))
				{
					zip.AddZipEntry(path, isCloudSave);
				}
				path = Path.ChangeExtension(path, ".tplr");
				if (FileUtilities.Exists(path, isCloudSave))
				{
					zip.AddZipEntry(path, isCloudSave);
				}
				if (isCloudSave)
				{
					BackupIO.Player.WriteCloudFiles(zip, path);
					return;
				}
				BackupIO.Player.WriteLocalFiles(zip, path);
			}

			/// <summary>
			/// Write cloud files, which will get the relevant part of the path and write map &amp; tmap files
			/// </summary>
			// Token: 0x0600528C RID: 21132 RVA: 0x00698FD8 File Offset: 0x006971D8
			private static void WriteCloudFiles(ZipFile zip, string path)
			{
				string name = Path.GetFileNameWithoutExtension(path);
				path = Path.ChangeExtension(path, "");
				path = path.Substring(0, path.Length - 1);
				foreach (string cloudPath in from p in SocialAPI.Cloud.GetFiles()
				where p.StartsWith(path, StringComparison.CurrentCultureIgnoreCase) && (p.EndsWith(".map", StringComparison.CurrentCultureIgnoreCase) || p.EndsWith(".tmap", StringComparison.CurrentCultureIgnoreCase))
				select p)
				{
					zip.AddEntry(name + "/" + Path.GetFileName(cloudPath), FileUtilities.ReadAllBytes(cloudPath, true));
				}
			}

			/// <summary>
			/// Write local files, which simply writes the entire player dir
			/// </summary>
			// Token: 0x0600528D RID: 21133 RVA: 0x006990A0 File Offset: 0x006972A0
			private static void WriteLocalFiles(ZipFile zip, string path)
			{
				string plrDir = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
				if (Directory.Exists(plrDir))
				{
					zip.AddZipEntry(plrDir, false);
				}
			}

			// Token: 0x04006A80 RID: 27264
			public static readonly string PlayerDir = Path.Combine(Main.SavePath, "Players");

			// Token: 0x04006A81 RID: 27265
			public static readonly string PlayerBackupDir = Path.Combine(BackupIO.Player.PlayerDir, "Backups");

			// Token: 0x02000E1D RID: 3613
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04007C3D RID: 31805
				public static Action<ZipFile, bool, string> <0>__WriteArchive;
			}
		}
	}
}
