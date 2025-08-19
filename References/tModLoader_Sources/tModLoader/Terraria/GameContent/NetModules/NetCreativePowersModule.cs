using System;
using System.IO;
using Terraria.GameContent.Creative;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020005D5 RID: 1493
	public class NetCreativePowersModule : NetModule
	{
		// Token: 0x060042F6 RID: 17142 RVA: 0x005FBCE4 File Offset: 0x005F9EE4
		public static NetPacket PreparePacket(ushort powerId, int specificInfoBytesInPacketCount)
		{
			NetPacket result = NetModule.CreatePacket<NetCreativePowersModule>(specificInfoBytesInPacketCount + 2);
			result.Writer.Write(powerId);
			return result;
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x005FBD08 File Offset: 0x005F9F08
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			ushort id = reader.ReadUInt16();
			ICreativePower power;
			if (!CreativePowerManager.Instance.TryGetPower(id, out power))
			{
				return false;
			}
			power.DeserializeNetMessage(reader, userId);
			return true;
		}
	}
}
