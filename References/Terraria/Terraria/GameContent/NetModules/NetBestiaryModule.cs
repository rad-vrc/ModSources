using System;
using System.IO;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x0200026B RID: 619
	public class NetBestiaryModule : NetModule
	{
		// Token: 0x06001FA6 RID: 8102 RVA: 0x005164A0 File Offset: 0x005146A0
		public static NetPacket SerializeKillCount(int npcNetId, int killcount)
		{
			NetPacket result = NetModule.CreatePacket<NetBestiaryModule>(5);
			result.Writer.Write(0);
			result.Writer.Write((short)npcNetId);
			result.Writer.Write((ushort)killcount);
			return result;
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x005164E0 File Offset: 0x005146E0
		public static NetPacket SerializeSight(int npcNetId)
		{
			NetPacket result = NetModule.CreatePacket<NetBestiaryModule>(3);
			result.Writer.Write(1);
			result.Writer.Write((short)npcNetId);
			return result;
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00516510 File Offset: 0x00514710
		public static NetPacket SerializeChat(int npcNetId)
		{
			NetPacket result = NetModule.CreatePacket<NetBestiaryModule>(3);
			result.Writer.Write(2);
			result.Writer.Write((short)npcNetId);
			return result;
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x00516540 File Offset: 0x00514740
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			if (Main.dedServ)
			{
				return false;
			}
			switch (reader.ReadByte())
			{
			case 0:
			{
				short key = reader.ReadInt16();
				string bestiaryCreditId = ContentSamples.NpcsByNetId[(int)key].GetBestiaryCreditId();
				ushort killCount = reader.ReadUInt16();
				Main.BestiaryTracker.Kills.SetKillCountDirectly(bestiaryCreditId, (int)killCount);
				break;
			}
			case 1:
			{
				short key2 = reader.ReadInt16();
				string bestiaryCreditId2 = ContentSamples.NpcsByNetId[(int)key2].GetBestiaryCreditId();
				Main.BestiaryTracker.Sights.SetWasSeenDirectly(bestiaryCreditId2);
				break;
			}
			case 2:
			{
				short key3 = reader.ReadInt16();
				string bestiaryCreditId3 = ContentSamples.NpcsByNetId[(int)key3].GetBestiaryCreditId();
				Main.BestiaryTracker.Chats.SetWasChatWithDirectly(bestiaryCreditId3);
				break;
			}
			}
			return true;
		}

		// Token: 0x02000643 RID: 1603
		private enum BestiaryUnlockType : byte
		{
			// Token: 0x04006157 RID: 24919
			Kill,
			// Token: 0x04006158 RID: 24920
			Sight,
			// Token: 0x04006159 RID: 24921
			Chat
		}
	}
}
