using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using log4net;
using ReLogic.OS;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Social;
using Terraria.Social.Steam;

namespace Terraria.Utilities
{
	// Token: 0x0200008D RID: 141
	public static class FileUtilities
	{
		// Token: 0x0600145E RID: 5214 RVA: 0x004A1ADD File Offset: 0x0049FCDD
		public static bool Exists(string path, bool cloud)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				return SocialAPI.Cloud.HasFile(path);
			}
			return File.Exists(path);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x004A1AFB File Offset: 0x0049FCFB
		public static void Delete(string path, bool cloud, bool forceDeleteFile = false)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				SocialAPI.Cloud.Delete(path);
				return;
			}
			if (forceDeleteFile)
			{
				File.Delete(path);
				return;
			}
			Platform.Get<IPathService>().MoveToRecycleBin(path);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x004A1B2A File Offset: 0x0049FD2A
		public static string GetFullPath(string path, bool cloud)
		{
			if (!cloud)
			{
				return Path.GetFullPath(path);
			}
			return path;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x004A1B37 File Offset: 0x0049FD37
		public static void Copy(string source, string destination, bool cloud, bool overwrite = true)
		{
			FileUtilities.CopyExtended(source, destination, cloud, overwrite, true);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x004A1B43 File Offset: 0x0049FD43
		public static void Move(string source, string destination, bool cloud, bool overwrite = true, bool forceDeleteSourceFile = false)
		{
			FileUtilities.Copy(source, destination, cloud, overwrite);
			FileUtilities.Delete(source, cloud, forceDeleteSourceFile);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x004A1B57 File Offset: 0x0049FD57
		public static int GetFileSize(string path, bool cloud)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				return SocialAPI.Cloud.GetFileSize(path);
			}
			return (int)new FileInfo(path).Length;
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x004A1B7C File Offset: 0x0049FD7C
		public static void Read(string path, byte[] buffer, int length, bool cloud)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				SocialAPI.Cloud.Read(path, buffer, length);
				return;
			}
			using (FileStream fileStream = File.OpenRead(path))
			{
				fileStream.Read(buffer, 0, length);
			}
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x004A1BD0 File Offset: 0x0049FDD0
		public static byte[] ReadAllBytes(string path, bool cloud)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				return SocialAPI.Cloud.Read(path);
			}
			return File.ReadAllBytes(path);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x004A1BEE File Offset: 0x0049FDEE
		public static void WriteAllBytes(string path, byte[] data, bool cloud)
		{
			FileUtilities.Write(path, data, data.Length, cloud);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x004A1BFC File Offset: 0x0049FDFC
		public static void Write(string path, byte[] data, int length, bool cloud)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				SocialAPI.Cloud.Write(path, data, length);
				return;
			}
			string parentFolderPath = FileUtilities.GetParentFolderPath(path, true);
			if (parentFolderPath != "")
			{
				Utils.TryCreatingDirectory(parentFolderPath);
			}
			FileUtilities.RemoveReadOnlyAttribute(path);
			using (FileStream fileStream = File.Open(path, FileMode.Create))
			{
				while (fileStream.Position < (long)length)
				{
					fileStream.Write(data, (int)fileStream.Position, Math.Min(length - (int)fileStream.Position, 2048));
				}
			}
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x004A1C98 File Offset: 0x0049FE98
		public static void RemoveReadOnlyAttribute(string path)
		{
			if (!File.Exists(path))
			{
				return;
			}
			try
			{
				FileAttributes attributes = File.GetAttributes(path);
				if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
				{
					attributes &= ~FileAttributes.ReadOnly;
					File.SetAttributes(path, attributes);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x004A1CE0 File Offset: 0x0049FEE0
		public static bool MoveToCloud(string localPath, string cloudPath)
		{
			if (SocialAPI.Cloud == null)
			{
				return false;
			}
			FileUtilities.WriteAllBytes(cloudPath, FileUtilities.ReadAllBytes(localPath, false), true);
			FileUtilities.Delete(localPath, false, false);
			return true;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x004A1D02 File Offset: 0x0049FF02
		public static bool MoveToLocal(string cloudPath, string localPath)
		{
			if (SocialAPI.Cloud == null)
			{
				return false;
			}
			FileUtilities.WriteAllBytes(localPath, FileUtilities.ReadAllBytes(cloudPath, true), false);
			FileUtilities.Delete(cloudPath, true, false);
			return true;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x004A1D24 File Offset: 0x0049FF24
		public static bool CopyToLocal(string cloudPath, string localPath)
		{
			if (SocialAPI.Cloud == null)
			{
				return false;
			}
			FileUtilities.WriteAllBytes(localPath, FileUtilities.ReadAllBytes(cloudPath, true), false);
			return true;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x004A1D40 File Offset: 0x0049FF40
		public static string GetFileName(string path, bool includeExtension = true)
		{
			Match match = FileUtilities.FileNameRegex.Match(path);
			if (match == null || match.Groups["fileName"] == null)
			{
				return "";
			}
			includeExtension &= (match.Groups["extension"] != null);
			return match.Groups["fileName"].Value + (includeExtension ? match.Groups["extension"].Value : "");
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x004A1DC4 File Offset: 0x0049FFC4
		public static string GetParentFolderPath(string path, bool includeExtension = true)
		{
			Match match = FileUtilities.FileNameRegex.Match(path);
			if (match == null || match.Groups["path"] == null)
			{
				return "";
			}
			return match.Groups["path"].Value;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x004A1E0D File Offset: 0x004A000D
		public static void CopyFolder(string sourcePath, string destinationPath)
		{
			FileUtilities.CopyFolderEXT(sourcePath, destinationPath, false, null, true, false);
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x004A1E1C File Offset: 0x004A001C
		public static void ProtectedInvoke(Action action)
		{
			bool isBackground = Thread.CurrentThread.IsBackground;
			try
			{
				Thread.CurrentThread.IsBackground = false;
				action();
			}
			finally
			{
				Thread.CurrentThread.IsBackground = isBackground;
			}
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x004A1E64 File Offset: 0x004A0064
		public static void CopyExtended(string source, string destination, bool cloud, bool overwriteAlways, bool overwriteOld = true)
		{
			bool overwrite = FileUtilities.DetermineIfShouldOverwrite(overwriteAlways, overwriteOld, source, destination);
			if (!overwrite && File.Exists(destination))
			{
				return;
			}
			if (!cloud)
			{
				try
				{
					File.Copy(source, destination, overwrite);
				}
				catch (IOException ex)
				{
					if (ex.GetType() != typeof(IOException))
					{
						throw;
					}
					using (FileStream inputstream = File.OpenRead(source))
					{
						using (FileStream outputstream = File.Create(destination))
						{
							inputstream.CopyTo(outputstream);
						}
					}
				}
				return;
			}
			string cloudSaveLocation = CoreSocialModule.GetCloudSaveLocation();
			destination = FileUtilities.ConvertToRelativePath(cloudSaveLocation, destination);
			source = FileUtilities.ConvertToRelativePath(cloudSaveLocation, source);
			if (SocialAPI.Cloud != null && (overwrite || !SocialAPI.Cloud.HasFile(destination)))
			{
				byte[] bytes = SocialAPI.Cloud.Read(source);
				SocialAPI.Cloud.Write(destination, bytes);
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x004A1F4C File Offset: 0x004A014C
		public static void CopyFolderEXT(string sourcePath, string destinationPath, bool isCloud = false, Regex excludeFilter = null, bool overwriteAlways = false, bool overwriteOld = false)
		{
			Directory.CreateDirectory(destinationPath);
			string[] directories = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories);
			for (int i = 0; i < directories.Length; i++)
			{
				string relativePath = FileUtilities.ConvertToRelativePath(sourcePath, directories[i]);
				if (excludeFilter == null || !excludeFilter.IsMatch(relativePath))
				{
					Directory.CreateDirectory(directories[i].Replace(sourcePath, destinationPath));
				}
			}
			directories = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);
			ILog tML = Logging.tML;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(68, 2);
			defaultInterpolatedStringHandler.AppendLiteral("Number of files to Copy: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(directories.Length);
			defaultInterpolatedStringHandler.AppendLiteral(". Estimated time for HDD @15 MB/s: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(directories.Length / 30);
			defaultInterpolatedStringHandler.AppendLiteral(" seconds");
			tML.Info(defaultInterpolatedStringHandler.ToStringAndClear());
			foreach (string obj in directories)
			{
				string relativePath2 = FileUtilities.ConvertToRelativePath(sourcePath, obj);
				if (excludeFilter == null || !excludeFilter.IsMatch(relativePath2))
				{
					FileUtilities.CopyExtended(obj, obj.Replace(sourcePath, destinationPath), isCloud, overwriteAlways, overwriteOld);
				}
			}
		}

		/// <summary>
		/// Converts the full 'path' to remove the base path component.
		/// Example: C://My Documents//Help Me I'm Hungry.txt is full 'path'
		/// 	basePath is C://My Documents
		/// 	Thus returns 'Help Me I'm Hungry.txt'
		/// </summary>
		// Token: 0x06001472 RID: 5234 RVA: 0x004A2050 File Offset: 0x004A0250
		public static string ConvertToRelativePath(string basePath, string fullPath)
		{
			if (!fullPath.StartsWith(basePath))
			{
				ILog tML = Logging.tML;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(50, 2);
				defaultInterpolatedStringHandler.AppendLiteral("string ");
				defaultInterpolatedStringHandler.AppendFormatted(fullPath);
				defaultInterpolatedStringHandler.AppendLiteral(" does not contain string ");
				defaultInterpolatedStringHandler.AppendFormatted(basePath);
				defaultInterpolatedStringHandler.AppendLiteral(". Is this correct?");
				tML.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
				return fullPath;
			}
			return fullPath.Substring(basePath.Length + 1);
		}

		/// <summary>
		/// DEtermines if should overwrite the file at Destination with the file at Source
		/// </summary>
		// Token: 0x06001473 RID: 5235 RVA: 0x004A20C8 File Offset: 0x004A02C8
		private static bool DetermineIfShouldOverwrite(bool overwriteAlways, bool overwriteOld, string source, string destination)
		{
			if (overwriteAlways)
			{
				return true;
			}
			if (!File.Exists(destination))
			{
				return overwriteAlways;
			}
			if (!overwriteOld)
			{
				return false;
			}
			DateTime srcFile = File.GetLastWriteTimeUtc(source);
			return File.GetLastWriteTimeUtc(destination) < srcFile;
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x004A20FC File Offset: 0x004A02FC
		[return: TupleElementNames(new string[]
		{
			"path",
			"message",
			"stabilityLevel"
		})]
		public static ValueTuple<string, string, int>[] GetAlternateSavePathFiles(string folderName)
		{
			return new ValueTuple<string, string, int>[]
			{
				new ValueTuple<string, string, int>(Path.Combine(Platform.Get<IPathService>().GetStoragePath("Terraria"), folderName ?? ""), "Click to copy \"{0}\" over from Terraria", 0),
				new ValueTuple<string, string, int>(Path.Combine(Platform.Get<IPathService>().GetStoragePath("Terraria"), "ModLoader", folderName ?? ""), "Click to copy \"{0}\" over from 1.3 tModLoader", 0),
				new ValueTuple<string, string, int>(Path.Combine(Main.SavePath, "..", "tModLoader", folderName ?? ""), "Click to copy \"{0}\" over from stable", 1),
				new ValueTuple<string, string, int>(Path.Combine(Main.SavePath, "..", "tModLoader-preview", folderName ?? ""), "Click to copy \"{0}\" over from preview", 2),
				new ValueTuple<string, string, int>(Path.Combine(Main.SavePath, "..", "tModLoader-dev", folderName ?? ""), "Click to copy \"{0}\" over from dev", 3),
				new ValueTuple<string, string, int>(Path.Combine(Main.SavePath, "..", "tModLoader-1.4.3", folderName ?? ""), "Click to copy \"{0}\" over from 1.4.3-Legacy", 0)
			};
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x004A2234 File Offset: 0x004A0434
		internal static bool WriteTagCompound(string path, bool isCloud, TagCompound tag)
		{
			MemoryStream stream = new MemoryStream();
			TagIO.ToStream(tag, stream, true);
			byte[] data = stream.ToArray();
			string fileName = Path.GetFileName(path);
			if (data[0] != 31 || data[1] != 139)
			{
				FileUtilities.Write(path + ".corr", data, data.Length, isCloud);
				throw new IOException("Detected Corrupted Save Stream attempt.\nAborting to avoid " + fileName + " corruption.\nYour last successful save will be kept. ERROR: Stream Missing NBT Header.");
			}
			FileUtilities.Write(path, data, data.Length, isCloud);
			if (FileUtilities.ReadAllBytes(path, isCloud).SequenceEqual(data))
			{
				return true;
			}
			Logging.tML.Warn("Detected failed save for " + fileName + ". Re-attempting after 2 seconds");
			Thread.Sleep(2000);
			FileUtilities.Write(path, data, data.Length, isCloud);
			if (!FileUtilities.ReadAllBytes(path, isCloud).SequenceEqual(data))
			{
				throw new IOException("Unable to save current progress.\nAborting to avoid " + fileName + " corruption.\nYour last successful save will be kept. ERROR: Stream Missing NBT Header.");
			}
			return true;
		}

		// Token: 0x040010AB RID: 4267
		private static Regex FileNameRegex = new Regex("^(?<path>.*[\\\\\\/])?(?:$|(?<fileName>.+?)(?:(?<extension>\\.[^.]*$)|$))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
