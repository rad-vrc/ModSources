using System;
using System.IO;
using Terraria.GameContent.Creative;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x0200026D RID: 621
	public class NetCreativePowersModule : NetModule
	{
		// Token: 0x06001FAE RID: 8110 RVA: 0x0051667C File Offset: 0x0051487C
		public static NetPacket PreparePacket(ushort powerId, int specificInfoBytesInPacketCount)
		{
			NetPacket result = NetModule.CreatePacket<NetCreativePowersModule>(specificInfoBytesInPacketCount + 2);
			result.Writer.Write(powerId);
			return result;
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x005166A0 File Offset: 0x005148A0
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			ushort id = reader.ReadUInt16();
			ICreativePower creativePower;
			if (!CreativePowerManager.Instance.TryGetPower(id, out creativePower))
			{
				return false;
			}
			creativePower.DeserializeNetMessage(reader, userId);
			return true;
		}
	}
}
