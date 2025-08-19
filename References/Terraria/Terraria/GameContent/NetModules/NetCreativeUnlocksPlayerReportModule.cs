using System;
using System.IO;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x0200026E RID: 622
	public class NetCreativeUnlocksPlayerReportModule : NetModule
	{
		// Token: 0x06001FB1 RID: 8113 RVA: 0x005166D0 File Offset: 0x005148D0
		public static NetPacket SerializeSacrificeRequest(int itemId, int amount)
		{
			NetPacket result = NetModule.CreatePacket<NetCreativeUnlocksPlayerReportModule>(5);
			result.Writer.Write(0);
			result.Writer.Write((ushort)itemId);
			result.Writer.Write((ushort)amount);
			return result;
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00516710 File Offset: 0x00514910
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			if (reader.ReadByte() == 0)
			{
				reader.ReadUInt16();
				reader.ReadUInt16();
			}
			return true;
		}

		// Token: 0x04004692 RID: 18066
		private const byte _requestItemSacrificeId = 0;
	}
}
