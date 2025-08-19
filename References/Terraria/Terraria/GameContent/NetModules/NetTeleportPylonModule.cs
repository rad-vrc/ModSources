using System;
using System.IO;
using Terraria.DataStructures;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x02000272 RID: 626
	public class NetTeleportPylonModule : NetModule
	{
		// Token: 0x06001FBD RID: 8125 RVA: 0x0051689C File Offset: 0x00514A9C
		public static NetPacket SerializePylonWasAddedOrRemoved(TeleportPylonInfo info, NetTeleportPylonModule.SubPacketType packetType)
		{
			NetPacket result = NetModule.CreatePacket<NetTeleportPylonModule>(6);
			result.Writer.Write((byte)packetType);
			result.Writer.Write(info.PositionInTiles.X);
			result.Writer.Write(info.PositionInTiles.Y);
			result.Writer.Write((byte)info.TypeOfPylon);
			return result;
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00516900 File Offset: 0x00514B00
		public static NetPacket SerializeUseRequest(TeleportPylonInfo info)
		{
			NetPacket result = NetModule.CreatePacket<NetTeleportPylonModule>(6);
			result.Writer.Write(2);
			result.Writer.Write(info.PositionInTiles.X);
			result.Writer.Write(info.PositionInTiles.Y);
			result.Writer.Write((byte)info.TypeOfPylon);
			return result;
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00516964 File Offset: 0x00514B64
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
				TeleportPylonInfo info = default(TeleportPylonInfo);
				info.PositionInTiles = new Point16(reader.ReadInt16(), reader.ReadInt16());
				info.TypeOfPylon = (TeleportPylonType)reader.ReadByte();
				Main.PylonSystem.AddForClient(info);
				break;
			}
			case 1:
			{
				if (Main.dedServ)
				{
					return false;
				}
				TeleportPylonInfo info2 = default(TeleportPylonInfo);
				info2.PositionInTiles = new Point16(reader.ReadInt16(), reader.ReadInt16());
				info2.TypeOfPylon = (TeleportPylonType)reader.ReadByte();
				Main.PylonSystem.RemoveForClient(info2);
				break;
			}
			case 2:
			{
				TeleportPylonInfo info3 = default(TeleportPylonInfo);
				info3.PositionInTiles = new Point16(reader.ReadInt16(), reader.ReadInt16());
				info3.TypeOfPylon = (TeleportPylonType)reader.ReadByte();
				Main.PylonSystem.HandleTeleportRequest(info3, userId);
				break;
			}
			}
			return true;
		}

		// Token: 0x02000644 RID: 1604
		public enum SubPacketType : byte
		{
			// Token: 0x0400615B RID: 24923
			PylonWasAdded,
			// Token: 0x0400615C RID: 24924
			PylonWasRemoved,
			// Token: 0x0400615D RID: 24925
			PlayerRequestsTeleport
		}
	}
}
