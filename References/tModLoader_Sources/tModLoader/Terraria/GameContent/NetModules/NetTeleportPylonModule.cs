using System;
using System.IO;
using Terraria.DataStructures;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020005DB RID: 1499
	public class NetTeleportPylonModule : NetModule
	{
		// Token: 0x06004310 RID: 17168 RVA: 0x005FC3C0 File Offset: 0x005FA5C0
		public static NetPacket SerializePylonWasAddedOrRemoved(TeleportPylonInfo info, NetTeleportPylonModule.SubPacketType packetType)
		{
			NetPacket result = NetModule.CreatePacket<NetTeleportPylonModule>(6);
			result.Writer.Write((byte)packetType);
			result.Writer.Write(info.PositionInTiles.X);
			result.Writer.Write(info.PositionInTiles.Y);
			result.Writer.Write((byte)info.TypeOfPylon);
			return result;
		}

		// Token: 0x06004311 RID: 17169 RVA: 0x005FC424 File Offset: 0x005FA624
		public static NetPacket SerializeUseRequest(TeleportPylonInfo info)
		{
			NetPacket result = NetModule.CreatePacket<NetTeleportPylonModule>(6);
			result.Writer.Write(2);
			result.Writer.Write(info.PositionInTiles.X);
			result.Writer.Write(info.PositionInTiles.Y);
			result.Writer.Write((byte)info.TypeOfPylon);
			return result;
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x005FC488 File Offset: 0x005FA688
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			switch (reader.ReadByte())
			{
			case 0:
			{
				if (Main.dedServ)
				{
					return false;
				}
				TeleportPylonInfo info3 = default(TeleportPylonInfo);
				info3.PositionInTiles = new Point16(reader.ReadInt16(), reader.ReadInt16());
				info3.TypeOfPylon = (TeleportPylonType)reader.ReadByte();
				Main.PylonSystem.AddForClient(info3);
				break;
			}
			case 1:
			{
				if (Main.dedServ)
				{
					return false;
				}
				TeleportPylonInfo info4 = default(TeleportPylonInfo);
				info4.PositionInTiles = new Point16(reader.ReadInt16(), reader.ReadInt16());
				info4.TypeOfPylon = (TeleportPylonType)reader.ReadByte();
				Main.PylonSystem.RemoveForClient(info4);
				break;
			}
			case 2:
			{
				TeleportPylonInfo info5 = default(TeleportPylonInfo);
				info5.PositionInTiles = new Point16(reader.ReadInt16(), reader.ReadInt16());
				info5.TypeOfPylon = (TeleportPylonType)reader.ReadByte();
				Main.PylonSystem.HandleTeleportRequest(info5, userId);
				break;
			}
			}
			return true;
		}

		// Token: 0x02000C6E RID: 3182
		public enum SubPacketType : byte
		{
			// Token: 0x040079A0 RID: 31136
			PylonWasAdded,
			// Token: 0x040079A1 RID: 31137
			PylonWasRemoved,
			// Token: 0x040079A2 RID: 31138
			PlayerRequestsTeleport
		}
	}
}
