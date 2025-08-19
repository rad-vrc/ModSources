using System;
using System.Collections.Generic;
using rail;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000CC RID: 204
	public class CloudSocialModule : CloudSocialModule
	{
		// Token: 0x060016C5 RID: 5829 RVA: 0x004B5B00 File Offset: 0x004B3D00
		public override void Initialize()
		{
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x004B5B02 File Offset: 0x004B3D02
		public override void Shutdown()
		{
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x004B5B04 File Offset: 0x004B3D04
		public override IEnumerable<string> GetFiles()
		{
			object obj = this.ioLock;
			IEnumerable<string> result;
			lock (obj)
			{
				uint fileCount = rail_api.RailFactory().RailStorageHelper().GetFileCount();
				List<string> list = new List<string>((int)fileCount);
				ulong file_size = 0UL;
				for (uint num = 0U; num < fileCount; num += 1U)
				{
					string filename;
					rail_api.RailFactory().RailStorageHelper().GetFileNameAndSize(num, ref filename, ref file_size);
					list.Add(filename);
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x004B5B8C File Offset: 0x004B3D8C
		public override bool Write(string path, byte[] data, int length)
		{
			object obj = this.ioLock;
			bool result2;
			lock (obj)
			{
				bool result = true;
				IRailFile railFile = (!rail_api.RailFactory().RailStorageHelper().IsFileExist(path)) ? rail_api.RailFactory().RailStorageHelper().CreateFile(path) : rail_api.RailFactory().RailStorageHelper().OpenFile(path);
				if (railFile != null)
				{
					railFile.Write(data, (uint)length);
					railFile.Close();
				}
				else
				{
					result = false;
				}
				result2 = result;
			}
			return result2;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x004B5C1C File Offset: 0x004B3E1C
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

		// Token: 0x060016CA RID: 5834 RVA: 0x004B5C84 File Offset: 0x004B3E84
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

		// Token: 0x060016CB RID: 5835 RVA: 0x004B5CE4 File Offset: 0x004B3EE4
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

		// Token: 0x060016CC RID: 5836 RVA: 0x004B5D30 File Offset: 0x004B3F30
		public override bool Delete(string path)
		{
			object obj = this.ioLock;
			bool result;
			lock (obj)
			{
				result = (rail_api.RailFactory().RailStorageHelper().RemoveFile(path) == 0);
			}
			return result;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x004B5D80 File Offset: 0x004B3F80
		public override bool Forget(string path)
		{
			return this.Delete(path);
		}

		// Token: 0x040012CD RID: 4813
		private object ioLock = new object();
	}
}
