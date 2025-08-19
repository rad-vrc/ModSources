using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zip;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Engine
{
	/// <summary>
	/// Log archiving is performed after log initialization in a separate class to avoid loading Ionic.Zip before logging initialises and it can be patched
	/// Some CLRs will load all required assemblies when the class is entered, not necessarily just the method, so you've got to watch out
	/// </summary>
	// Token: 0x020002B5 RID: 693
	internal static class LogArchiver
	{
		// Token: 0x06002D27 RID: 11559 RVA: 0x0052C8D7 File Offset: 0x0052AAD7
		static LogArchiver()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		}

		/// <summary>
		/// Attempt archiving logs.
		/// </summary>
		// Token: 0x06002D28 RID: 11560 RVA: 0x0052C8E3 File Offset: 0x0052AAE3
		internal static void ArchiveLogs()
		{
			LogArchiver.SetupLogDirs();
			LogArchiver.MoveZipsToArchiveDir();
			LogArchiver.Archive();
			LogArchiver.DeleteOldArchives();
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x0052C8FC File Offset: 0x0052AAFC
		private static IEnumerable<string> GetArchivedLogs()
		{
			IEnumerable<string> result;
			try
			{
				result = Directory.EnumerateFiles(Logging.LogArchiveDir, "*.zip");
			}
			catch (Exception e)
			{
				Logging.tML.Error(e);
				result = Enumerable.Empty<string>();
			}
			return result;
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x0052C940 File Offset: 0x0052AB40
		private static IEnumerable<string> GetOldLogs()
		{
			IEnumerable<string> result;
			try
			{
				result = Directory.EnumerateFiles(Logging.LogDir, "*.old*");
			}
			catch (Exception e)
			{
				Logging.tML.Error(e);
				result = Enumerable.Empty<string>();
			}
			return result;
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x0052C984 File Offset: 0x0052AB84
		private static void SetupLogDirs()
		{
			try
			{
				Directory.CreateDirectory(Logging.LogArchiveDir);
			}
			catch (Exception e)
			{
				Logging.tML.Error(e);
			}
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x0052C9BC File Offset: 0x0052ABBC
		private static void MoveZipsToArchiveDir()
		{
			bool justdelete = LogArchiver.GetArchivedLogs().Any<string>();
			foreach (string path in Directory.EnumerateFiles(Logging.LogDir, "*.zip"))
			{
				try
				{
					if (justdelete)
					{
						File.Delete(path);
					}
					else
					{
						File.Move(path, Path.Combine(Logging.LogArchiveDir, Path.GetFileName(path)));
					}
				}
				catch (Exception e)
				{
					Logging.tML.Error(e);
				}
			}
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x0052CA54 File Offset: 0x0052AC54
		private static void Archive()
		{
			LogArchiver.<>c__DisplayClass7_0 CS$<>8__locals1 = new LogArchiver.<>c__DisplayClass7_0();
			List<string> logFiles = LogArchiver.GetOldLogs().ToList<string>();
			if (!logFiles.Any<string>())
			{
				return;
			}
			DateTime time;
			try
			{
				IEnumerable<string> source = logFiles;
				Func<string, DateTime> selector;
				if ((selector = LogArchiver.<>O.<0>__GetCreationTime) == null)
				{
					selector = (LogArchiver.<>O.<0>__GetCreationTime = new Func<string, DateTime>(File.GetCreationTime));
				}
				time = source.Select(selector).Min<DateTime>();
			}
			catch (Exception e)
			{
				Logging.tML.Error(e);
				return;
			}
			int i = 1;
			LogArchiver.<>c__DisplayClass7_0 CS$<>8__locals2 = CS$<>8__locals1;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 1);
			defaultInterpolatedStringHandler.AppendFormatted<DateTime>(time, "yyyy-MM-dd");
			defaultInterpolatedStringHandler.AppendLiteral("-(\\d+)\\.zip");
			CS$<>8__locals2.pattern = new Regex(defaultInterpolatedStringHandler.ToStringAndClear());
			string[] existingLogArchives = new string[0];
			try
			{
				existingLogArchives = (from s in Directory.GetFiles(Logging.LogArchiveDir)
				where CS$<>8__locals1.pattern.IsMatch(Path.GetFileName(s))
				select s).ToArray<string>();
			}
			catch (Exception e2)
			{
				Logging.tML.Error(e2);
				return;
			}
			if (existingLogArchives.Length != 0)
			{
				i = (from s in existingLogArchives
				select int.Parse(CS$<>8__locals1.pattern.Match(Path.GetFileName(s)).Groups[1].Value)).Max() + 1;
			}
			try
			{
				string logArchiveDir = Logging.LogArchiveDir;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
				defaultInterpolatedStringHandler.AppendFormatted<DateTime>(time, "yyyy-MM-dd");
				defaultInterpolatedStringHandler.AppendLiteral("-");
				defaultInterpolatedStringHandler.AppendFormatted<int>(i);
				defaultInterpolatedStringHandler.AppendLiteral(".zip");
				using (ZipFile zip = new ZipFile(Path.Combine(logArchiveDir, defaultInterpolatedStringHandler.ToStringAndClear()), Encoding.UTF8))
				{
					foreach (string logFile in logFiles)
					{
						string entryName = (Path.GetExtension(logFile) == ".old") ? Path.GetFileNameWithoutExtension(logFile) : Path.GetFileName(logFile);
						using (FileStream stream = File.OpenRead(logFile))
						{
							if (stream.Length > 10000000L)
							{
								Logging.tML.Warn(logFile + " exceeds 10MB, it will be truncated for the logs archive.");
								zip.AddEntry(entryName, stream.ReadBytes(10000000));
							}
							else
							{
								zip.AddEntry(entryName, stream);
							}
							zip.Save();
						}
						File.Delete(logFile);
					}
				}
			}
			catch (Exception e3)
			{
				Logging.tML.Error(e3);
			}
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x0052CD1C File Offset: 0x0052AF1C
		private static void DeleteOldArchives()
		{
			IEnumerable<string> archivedLogs = LogArchiver.GetArchivedLogs();
			Func<string, DateTime> keySelector;
			if ((keySelector = LogArchiver.<>O.<0>__GetCreationTime) == null)
			{
				keySelector = (LogArchiver.<>O.<0>__GetCreationTime = new Func<string, DateTime>(File.GetCreationTime));
			}
			List<string> existingLogs = archivedLogs.OrderBy(keySelector).ToList<string>();
			foreach (string f in existingLogs.Take(existingLogs.Count - 20))
			{
				try
				{
					File.Delete(f);
				}
				catch (Exception e)
				{
					Logging.tML.Error(e);
				}
			}
		}

		// Token: 0x04001C2F RID: 7215
		private const int MAX_LOGS = 20;

		// Token: 0x02000A5F RID: 2655
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006CFF RID: 27903
			public static Func<string, DateTime> <0>__GetCreationTime;
		}
	}
}
