using System;
using System.IO;

namespace Terraria.Net
{
	// Token: 0x020000C0 RID: 192
	public abstract class NetModule
	{
		// Token: 0x0600141A RID: 5146
		public abstract bool Deserialize(BinaryReader reader, int userId);

		// Token: 0x0600141B RID: 5147 RVA: 0x004A2614 File Offset: 0x004A0814
		protected static NetPacket CreatePacket<T>(int maxSize) where T : NetModule
		{
			ushort id = NetManager.Instance.GetId<T>();
			return new NetPacket(id, maxSize);
		}
	}
}
