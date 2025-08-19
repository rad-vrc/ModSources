using System;
using System.Collections.Generic;
using System.IO;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002F3 RID: 755
	public class NPCWasChatWithTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x0600239C RID: 9116 RVA: 0x00557268 File Offset: 0x00555468
		public NPCWasChatWithTracker()
		{
			this._chattedWithPlayer = new HashSet<string>();
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x00557288 File Offset: 0x00555488
		public void RegisterChatStartWith(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			bool flag = !this._chattedWithPlayer.Contains(bestiaryCreditId);
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				this._chattedWithPlayer.Add(bestiaryCreditId);
			}
			if (Main.netMode == 2 && flag)
			{
				NetManager.Instance.Broadcast(NetBestiaryModule.SerializeChat(npc.netID), -1);
			}
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x00557308 File Offset: 0x00555508
		public void SetWasChatWithDirectly(string persistentId)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				this._chattedWithPlayer.Add(persistentId);
			}
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x00557350 File Offset: 0x00555550
		public bool GetWasChatWith(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			return this._chattedWithPlayer.Contains(bestiaryCreditId);
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x00557370 File Offset: 0x00555570
		public bool GetWasChatWith(string persistentId)
		{
			return this._chattedWithPlayer.Contains(persistentId);
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x00557380 File Offset: 0x00555580
		public void Save(BinaryWriter writer)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				writer.Write(this._chattedWithPlayer.Count);
				foreach (string value in this._chattedWithPlayer)
				{
					writer.Write(value);
				}
			}
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x0055740C File Offset: 0x0055560C
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string item = reader.ReadString();
				this._chattedWithPlayer.Add(item);
			}
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x00557440 File Offset: 0x00555640
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				reader.ReadString();
			}
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x00557467 File Offset: 0x00555667
		public void Reset()
		{
			this._chattedWithPlayer.Clear();
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x00557474 File Offset: 0x00555674
		public void OnPlayerJoining(int playerIndex)
		{
			foreach (string key in this._chattedWithPlayer)
			{
				int npcNetId;
				if (ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(key, out npcNetId))
				{
					NetManager.Instance.SendToClient(NetBestiaryModule.SerializeChat(npcNetId), playerIndex);
				}
			}
		}

		// Token: 0x04004835 RID: 18485
		private object _entryCreationLock = new object();

		// Token: 0x04004836 RID: 18486
		private HashSet<string> _chattedWithPlayer;
	}
}
