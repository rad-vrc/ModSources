using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020006A1 RID: 1697
	public class NPCWasChatWithTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x0600483F RID: 18495 RVA: 0x006488D4 File Offset: 0x00646AD4
		public NPCWasChatWithTracker()
		{
			this._chattedWithPlayer = new HashSet<string>();
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x006488F4 File Offset: 0x00646AF4
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

		// Token: 0x06004841 RID: 18497 RVA: 0x00648974 File Offset: 0x00646B74
		public void SetWasChatWithDirectly(string persistentId)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				this._chattedWithPlayer.Add(persistentId);
			}
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x006489BC File Offset: 0x00646BBC
		public bool GetWasChatWith(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			return this._chattedWithPlayer.Contains(bestiaryCreditId);
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x006489DC File Offset: 0x00646BDC
		public bool GetWasChatWith(string persistentId)
		{
			return this._chattedWithPlayer.Contains(persistentId);
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x006489EC File Offset: 0x00646BEC
		public void Save(BinaryWriter writer)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				IEnumerable<string> vanillaOnly = this._chattedWithPlayer.Where(delegate(string persistentId)
				{
					int netId;
					return ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(persistentId, out netId) && netId < (int)NPCID.Count;
				});
				writer.Write(vanillaOnly.Count<string>());
				foreach (string item in vanillaOnly)
				{
					writer.Write(item);
				}
			}
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x00648A94 File Offset: 0x00646C94
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string item = reader.ReadString();
				this._chattedWithPlayer.Add(item);
			}
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x00648AC8 File Offset: 0x00646CC8
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				reader.ReadString();
			}
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x00648AEF File Offset: 0x00646CEF
		public void Reset()
		{
			this._chattedWithPlayer.Clear();
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x00648AFC File Offset: 0x00646CFC
		public void OnPlayerJoining(int playerIndex)
		{
			foreach (string item in this._chattedWithPlayer)
			{
				int value;
				if (ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(item, out value))
				{
					NetManager.Instance.SendToClient(NetBestiaryModule.SerializeChat(value), playerIndex);
				}
			}
		}

		// Token: 0x04005C24 RID: 23588
		private object _entryCreationLock = new object();

		// Token: 0x04005C25 RID: 23589
		private HashSet<string> _chattedWithPlayer;
	}
}
