using System;
using System.IO;

namespace Terraria.Net
{
	// Token: 0x02000120 RID: 288
	public abstract class NetModule
	{
		// Token: 0x06001A29 RID: 6697
		public abstract bool Deserialize(BinaryReader reader, int userId);

		// Token: 0x06001A2A RID: 6698 RVA: 0x004CAFA3 File Offset: 0x004C91A3
		protected static NetPacket CreatePacket<T>(int maxSize) where T : NetModule
		{
			return new NetPacket(NetManager.Instance.GetId<T>(), maxSize);
		}
	}
}
