using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using ReLogic.OS;
using Terraria.Social;

namespace Terraria.Utilities
{
	// Token: 0x0200014A RID: 330
	public static class FileUtilities
	{
		// Token: 0x0600190B RID: 6411 RVA: 0x004DFCFA File Offset: 0x004DDEFA
		public static bool Exists(string path, bool cloud)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				return SocialAPI.Cloud.HasFile(path);
			}
			return File.Exists(path);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x004DFD18 File Offset: 0x004DDF18
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

		// Token: 0x0600190D RID: 6413 RVA: 0x004DFD47 File Offset: 0x004DDF47
		public static string GetFullPath(string path, bool cloud)
		{
			if (!cloud)
			{
				return Path.GetFullPath(path);
			}
			return path;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x004DFD54 File Offset: 0x004DDF54
		public static void Copy(string source, string destination, bool cloud, bool overwrite = true)
		{
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
					using (FileStream fileStream = File.OpenRead(source))
					{
						using (FileStream fileStream2 = File.Create(destination))
						{
							fileStream.CopyTo(fileStream2);
						}
					}
				}
				return;
			}
			if (SocialAPI.Cloud == null || (!overwrite && SocialAPI.Cloud.HasFile(destination)))
			{
				return;
			}
			SocialAPI.Cloud.Write(destination, SocialAPI.Cloud.Read(source));
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x004DFE10 File Offset: 0x004DE010
		public static void Move(string source, string destination, bool cloud, bool overwrite = true, bool forceDeleteSourceFile = false)
		{
			FileUtilities.Copy(source, destination, cloud, overwrite);
			FileUtilities.Delete(source, cloud, forceDeleteSourceFile);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x004DFE24 File Offset: 0x004DE024
		public static int GetFileSize(string path, bool cloud)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				return SocialAPI.Cloud.GetFileSize(path);
			}
			return (int)new FileInfo(path).Length;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x004DFE48 File Offset: 0x004DE048
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

		// Token: 0x06001912 RID: 6418 RVA: 0x004DFE9C File Offset: 0x004DE09C
		public static byte[] ReadAllBytes(string path, bool cloud)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				return SocialAPI.Cloud.Read(path);
			}
			return File.ReadAllBytes(path);
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x004DFEBA File Offset: 0x004DE0BA
		public static void WriteAllBytes(string path, byte[] data, bool cloud)
		{
			FileUtilities.Write(path, data, data.Length, cloud);
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x004DFEC8 File Offset: 0x004DE0C8
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

		// Token: 0x06001915 RID: 6421 RVA: 0x004DFF64 File Offset: 0x004DE164
		public static void RemoveReadOnlyAttribute(string path)
		{
			if (!File.Exists(path))
			{
				return;
			}
			try
			{
				FileAttributes fileAttributes = File.GetAttributes(path);
				if ((fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
				{
					fileAttributes &= ~FileAttributes.ReadOnly;
					File.SetAttributes(path, fileAttributes);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x004DFFAC File Offset: 0x004DE1AC
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

		// Token: 0x06001917 RID: 6423 RVA: 0x004DFFCE File Offset: 0x004DE1CE
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

		// Token: 0x06001918 RID: 6424 RVA: 0x004DFFF0 File Offset: 0x004DE1F0
		public static bool CopyToLocal(string cloudPath, string localPath)
		{
			if (SocialAPI.Cloud == null)
			{
				return false;
			}
			FileUtilities.WriteAllBytes(localPath, FileUtilities.ReadAllBytes(cloudPath, true), false);
			return true;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x004E000C File Offset: 0x004DE20C
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

		// Token: 0x0600191A RID: 6426 RVA: 0x004E0090 File Offset: 0x004DE290
		public static string GetParentFolderPath(string path, bool includeExtension = true)
		{
			Match match = FileUtilities.FileNameRegex.Match(path);
			if (match == null || match.Groups["path"] == null)
			{
				return "";
			}
			return match.Groups["path"].Value;
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x004E00DC File Offset: 0x004DE2DC
		public static void CopyFolder(string sourcePath, string destinationPath)
		{
			Directory.CreateDirectory(destinationPath);
			string[] array = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories);
			for (int i = 0; i < array.Length; i++)
			{
				Directory.CreateDirectory(array[i].Replace(sourcePath, destinationPath));
			}
			foreach (string text in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
			{
				File.Copy(text, text.Replace(sourcePath, destinationPath), true);
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x004E0148 File Offset: 0x004DE348
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

		// Token: 0x04001520 RID: 5408
		private static Regex FileNameRegex = new Regex("^(?<path>.*[\\\\\\/])?(?:$|(?<fileName>.+?)(?:(?<extension>\\.[^.]*$)|$))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
