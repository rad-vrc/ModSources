using System;
using System.IO;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020005D7 RID: 1495
	public class NetCreativeUnlocksPlayerReportModule : NetModule
	{
		// Token: 0x060042FC RID: 17148 RVA: 0x005FBDC0 File Offset: 0x005F9FC0
		public static NetPacket SerializeSacrificeRequest(int itemId, int amount)
		{
			NetPacket result = NetModule.CreatePacket<NetCreativeUnlocksPlayerReportModule>(5);
			result.Writer.Write(0);
			result.Writer.Write((ushort)itemId);
			result.Writer.Write((ushort)amount);
			return result;
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x005FBDFE File Offset: 0x005F9FFE
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			if (reader.ReadByte() == 0)
			{
				reader.ReadUInt16();
				reader.ReadUInt16();
			}
			return true;
		}

		// Token: 0x040059E2 RID: 23010
		private const byte _requestItemSacrificeId = 0;
	}
}
