using System;
using System.Collections.Generic;
using rail;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000153 RID: 339
	public class CloudSocialModule : CloudSocialModule
	{
		// Token: 0x06001936 RID: 6454 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Initialize()
		{
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Shutdown()
		{
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x004E0528 File Offset: 0x004DE728
		public override IEnumerable<string> GetFiles()
		{
			object obj = this.ioLock;
			IEnumerable<string> result;
			lock (obj)
			{
				uint fileCount = rail_api.RailFactory().RailStorageHelper().GetFileCount();
				List<string> list = new List<string>((int)fileCount);
				ulong num = 0UL;
				for (uint num2 = 0U; num2 < fileCount; num2 += 1U)
				{
					string item;
					rail_api.RailFactory().RailStorageHelper().GetFileNameAndSize(num2, ref item, ref num);
					list.Add(item);
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x004E05B0 File Offset: 0x004DE7B0
		public override bool Write(string path, byte[] data, int length)
		{
			object obj = this.ioLock;
			bool result;
			lock (obj)
			{
				bool flag2 = true;
				IRailFile railFile;
				if (rail_api.RailFactory().RailStorageHelper().IsFileExist(path))
				{
					railFile = rail_api.RailFactory().RailStorageHelper().OpenFile(path);
				}
				else
				{
					railFile = rail_api.RailFactory().RailStorageHelper().CreateFile(path);
				}
				if (railFile != null)
				{
					railFile.Write(data, (uint)length);
					railFile.Close();
				}
				else
				{
					flag2 = false;
				}
				result = flag2;
			}
			return result;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x004E0640 File Offset: 0x004DE840
		public override int GetFileSize(string path)
		{
			object obj = this.ioLock;
			int result;
			lock (obj)
			{
				IRailFile railFile = rail_api.RailFactory().RailStorageHelper().OpenFile(path);
				if (railFile != null)
				{
					int size = (int)railFile.GetSize();
					railFile.Close();
					result = size;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x004E06A8 File Offset: 0x004DE8A8
		public override void Read(string path, byte[] buffer, int size)
		{
			object obj = this.ioLock;
			lock (obj)
			{
				IRailFile railFile = rail_api.RailFactory().RailStorageHelper().OpenFile(path);
				if (railFile != null)
				{
					railFile.Read(buffer, (uint)size);
					railFile.Close();
				}
			}
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x004E0708 File Offset: 0x004DE908
		public override bool HasFile(string path)
		{
			object obj = this.ioLock;
			bool result;
			lock (obj)
			{
				result = rail_api.RailFactory().RailStorageHelper().IsFileExist(path);
			}
			return result;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x004E0754 File Offset: 0x004DE954
		public override bool Delete(string path)
		{
			object obj = this.ioLock;
			bool result;
			lock (obj)
			{
				RailResult railResult = rail_api.RailFactory().RailStorageHelper().RemoveFile(path);
				result = (railResult == 0);
			}
			return result;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x004E07A8 File Offset: 0x004DE9A8
		public override bool Forget(string path)
		{
			return this.Delete(path);
		}

		// Token: 0x04001534 RID: 5428
		private object ioLock = new object();
	}
}
