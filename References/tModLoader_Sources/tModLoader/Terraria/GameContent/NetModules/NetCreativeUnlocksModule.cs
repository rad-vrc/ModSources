using System;
using System.IO;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020005D6 RID: 1494
	public class NetCreativeUnlocksModule : NetModule
	{
		// Token: 0x060042F9 RID: 17145 RVA: 0x005FBD40 File Offset: 0x005F9F40
		public static NetPacket SerializeItemSacrifice(int itemId, int sacrificeCount)
		{
			NetPacket result = NetModule.CreatePacket<NetCreativeUnlocksModule>(3);
			result.Writer.Write((short)itemId);
			result.Writer.Write((ushort)sacrificeCount);
			return result;
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x005FBD74 File Offset: 0x005F9F74
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			if (Main.dedServ)
			{
				return false;
			}
			short key = reader.ReadInt16();
			string persistentId = ContentSamples.ItemPersistentIdsByNetIds[(int)key];
			ushort sacrificeCount = reader.ReadUInt16();
			Main.LocalPlayerCreativeTracker.ItemSacrifices.SetSacrificeCountDirectly(persistentId, (int)sacrificeCount);
			return true;
		}
	}
}
