using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020005DA RID: 1498
	public class NetPingModule : NetModule
	{
		// Token: 0x0600430D RID: 17165 RVA: 0x005FC358 File Offset: 0x005FA558
		public static NetPacket Serialize(Vector2 position)
		{
			NetPacket result = NetModule.CreatePacket<NetPingModule>(8);
			result.Writer.WriteVector2(position);
			return result;
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x005FC37C File Offset: 0x005FA57C
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			Vector2 position = reader.ReadVector2();
			if (Main.dedServ)
			{
				NetManager.Instance.Broadcast(NetPingModule.Serialize(position), userId);
			}
			else
			{
				Main.Pings.Add(position);
			}
			return true;
		}
	}
}
