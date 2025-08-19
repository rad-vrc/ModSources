using System;
using System.Collections.Generic;
using Steamworks;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x02000174 RID: 372
	public class CloudSocialModule : CloudSocialModule
	{
		// Token: 0x06001A75 RID: 6773 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Initialize()
		{
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Shutdown()
		{
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x004E4A08 File Offset: 0x004E2C08
		public override IEnumerable<string> GetFiles()
		{
			object obj = this.ioLock;
			IEnumerable<string> result;
			lock (obj)
			{
				int fileCount = SteamRemoteStorage.GetFileCount();
				List<string> list = new List<string>(fileCount);
				for (int i = 0; i < fileCount; i++)
				{
					int num;
					list.Add(SteamRemoteStorage.GetFileNameAndSize(i, ref num));
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x004E4A74 File Offset: 0x004E2C74
		public override bool Write(string path, byte[] data, int length)
		{
			object obj = this.ioLock;
			bool result;
			lock (obj)
			{
				UGCFileWriteStreamHandle_t ugcfileWriteStreamHandle_t = SteamRemoteStorage.FileWriteStreamOpen(path);
				uint num = 0U;
				while ((ulong)num < (ulong)((long)length))
				{
					int num2 = (int)Math.Min(1024L, (long)length - (long)((ulong)num));
					Array.Copy(data, (long)((ulong)num), this.writeBuffer, 0L, (long)num2);
					SteamRemoteStorage.FileWriteStreamWriteChunk(ugcfileWriteStreamHandle_t, this.writeBuffer, num2);
					num += 1024U;
				}
				result = SteamRemoteStorage.FileWriteStreamClose(ugcfileWriteStreamHandle_t);
			}
			return result;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x004E4B08 File Offset: 0x004E2D08
		public override int GetFileSize(string path)
		{
			object obj = this.ioLock;
			int fileSize;
			lock (obj)
			{
				fileSize = SteamRemoteStorage.GetFileSize(path);
			}
			return fileSize;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x004E4B4C File Offset: 0x004E2D4C
		public override void Read(string path, byte[] buffer, int size)
		{
			object obj = this.ioLock;
			lock (obj)
			{
				SteamRemoteStorage.FileRead(path, buffer, size);
			}
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x004E4B90 File Offset: 0x004E2D90
		public override bool HasFile(string path)
		{
			object obj = this.ioLock;
			bool result;
			lock (obj)
			{
				result = SteamRemoteStorage.FileExists(path);
			}
			return result;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x004E4BD4 File Offset: 0x004E2DD4
		public override bool Delete(string path)
		{
			object obj = this.ioLock;
			bool result;
			lock (obj)
			{
				result = SteamRemoteStorage.FileDelete(path);
			}
			return result;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x004E4C18 File Offset: 0x004E2E18
		public override bool Forget(string path)
		{
			object obj = this.ioLock;
			bool result;
			lock (obj)
			{
				result = SteamRemoteStorage.FileForget(path);
			}
			return result;
		}

		// Token: 0x0400159C RID: 5532
		private const uint WRITE_CHUNK_SIZE = 1024U;

		// Token: 0x0400159D RID: 5533
		private object ioLock = new object();

		// Token: 0x0400159E RID: 5534
		private byte[] writeBuffer = new byte[1024];
	}
}
