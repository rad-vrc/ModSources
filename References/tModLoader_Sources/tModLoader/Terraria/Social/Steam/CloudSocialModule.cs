using System;
using System.Collections.Generic;
using Steamworks;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x020000E4 RID: 228
	public class CloudSocialModule : CloudSocialModule
	{
		// Token: 0x060017C8 RID: 6088 RVA: 0x004B940E File Offset: 0x004B760E
		public override void Initialize()
		{
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x004B9410 File Offset: 0x004B7610
		public override void Shutdown()
		{
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x004B9414 File Offset: 0x004B7614
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

		// Token: 0x060017CB RID: 6091 RVA: 0x004B9480 File Offset: 0x004B7680
		public override bool Write(string path, byte[] data, int length)
		{
			object obj = this.ioLock;
			bool result;
			lock (obj)
			{
				UGCFileWriteStreamHandle_t writeHandle = SteamRemoteStorage.FileWriteStreamOpen(path);
				uint num = 0U;
				while ((ulong)num < (ulong)((long)length))
				{
					int num2 = (int)Math.Min(1024L, (long)length - (long)((ulong)num));
					Array.Copy(data, (long)((ulong)num), this.writeBuffer, 0L, (long)num2);
					SteamRemoteStorage.FileWriteStreamWriteChunk(writeHandle, this.writeBuffer, num2);
					num += 1024U;
				}
				result = SteamRemoteStorage.FileWriteStreamClose(writeHandle);
			}
			return result;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x004B9514 File Offset: 0x004B7714
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

		// Token: 0x060017CD RID: 6093 RVA: 0x004B9558 File Offset: 0x004B7758
		public override void Read(string path, byte[] buffer, int size)
		{
			object obj = this.ioLock;
			lock (obj)
			{
				SteamRemoteStorage.FileRead(path, buffer, size);
			}
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x004B959C File Offset: 0x004B779C
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

		// Token: 0x060017CF RID: 6095 RVA: 0x004B95E0 File Offset: 0x004B77E0
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

		// Token: 0x060017D0 RID: 6096 RVA: 0x004B9624 File Offset: 0x004B7824
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

		// Token: 0x04001326 RID: 4902
		private const uint WRITE_CHUNK_SIZE = 1024U;

		// Token: 0x04001327 RID: 4903
		private object ioLock = new object();

		// Token: 0x04001328 RID: 4904
		private byte[] writeBuffer = new byte[1024];
	}
}
