using System;
using System.IO;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x0200026C RID: 620
	public class NetCreativeUnlocksModule : NetModule
	{
		// Token: 0x06001FAB RID: 8107 RVA: 0x00516604 File Offset: 0x00514804
		public static NetPacket SerializeItemSacrifice(int itemId, int sacrificeCount)
		{
			NetPacket result = NetModule.CreatePacket<NetCreativeUnlocksModule>(3);
			result.Writer.Write((short)itemId);
			result.Writer.Write((ushort)sacrificeCount);
			return result;
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x00516638 File Offset: 0x00514838
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
