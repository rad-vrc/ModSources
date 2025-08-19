using System;
using System.IO;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020005D3 RID: 1491
	public class NetBestiaryModule : NetModule
	{
		// Token: 0x060042EE RID: 17134 RVA: 0x005FBAE8 File Offset: 0x005F9CE8
		public static NetPacket SerializeKillCount(int npcNetId, int killcount)
		{
			NetPacket result = NetModule.CreatePacket<NetBestiaryModule>(5);
			result.Writer.Write(0);
			result.Writer.Write((short)npcNetId);
			result.Writer.Write((ushort)killcount);
			return result;
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x005FBB28 File Offset: 0x005F9D28
		public static NetPacket SerializeSight(int npcNetId)
		{
			NetPacket result = NetModule.CreatePacket<NetBestiaryModule>(3);
			result.Writer.Write(1);
			result.Writer.Write((short)npcNetId);
			return result;
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x005FBB58 File Offset: 0x005F9D58
		public static NetPacket SerializeChat(int npcNetId)
		{
			NetPacket result = NetModule.CreatePacket<NetBestiaryModule>(3);
			result.Writer.Write(2);
			result.Writer.Write((short)npcNetId);
			return result;
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x005FBB88 File Offset: 0x005F9D88
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
				short key3 = reader.ReadInt16();
				string bestiaryCreditId3 = ContentSamples.NpcsByNetId[(int)key3].GetBestiaryCreditId();
				ushort killCount = reader.ReadUInt16();
				Main.BestiaryTracker.Kills.SetKillCountDirectly(bestiaryCreditId3, (int)killCount);
				break;
			}
			case 1:
			{
				short key4 = reader.ReadInt16();
				string bestiaryCreditId4 = ContentSamples.NpcsByNetId[(int)key4].GetBestiaryCreditId();
				Main.BestiaryTracker.Sights.SetWasSeenDirectly(bestiaryCreditId4);
				break;
			}
			case 2:
			{
				short key5 = reader.ReadInt16();
				string bestiaryCreditId5 = ContentSamples.NpcsByNetId[(int)key5].GetBestiaryCreditId();
				Main.BestiaryTracker.Chats.SetWasChatWithDirectly(bestiaryCreditId5);
				break;
			}
			}
			return true;
		}

		// Token: 0x02000C6C RID: 3180
		private enum BestiaryUnlockType : byte
		{
			// Token: 0x04007999 RID: 31129
			Kill,
			// Token: 0x0400799A RID: 31130
			Sight,
			// Token: 0x0400799B RID: 31131
			Chat
		}
	}
}
