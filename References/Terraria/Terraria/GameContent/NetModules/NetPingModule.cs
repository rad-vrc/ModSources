using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x02000271 RID: 625
	public class NetPingModule : NetModule
	{
		// Token: 0x06001FBA RID: 8122 RVA: 0x0051683C File Offset: 0x00514A3C
		public static NetPacket Serialize(Vector2 position)
		{
			NetPacket result = NetModule.CreatePacket<NetPingModule>(8);
			result.Writer.WriteVector2(position);
			return result;
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00516860 File Offset: 0x00514A60
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
