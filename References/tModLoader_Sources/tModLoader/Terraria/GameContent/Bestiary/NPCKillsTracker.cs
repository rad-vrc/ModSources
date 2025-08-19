using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200069D RID: 1693
	public class NPCKillsTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x06004822 RID: 18466 RVA: 0x00647748 File Offset: 0x00645948
		public NPCKillsTracker()
		{
			this._killCountsByNpcId = new Dictionary<string, int>();
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x00647768 File Offset: 0x00645968
		public void RegisterKill(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			int value;
			this._killCountsByNpcId.TryGetValue(bestiaryCreditId, out value);
			value++;
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				this._killCountsByNpcId[bestiaryCreditId] = Utils.Clamp<int>(value, 0, 999999999);
			}
			if (Main.netMode == 2)
			{
				NetManager.Instance.Broadcast(NetBestiaryModule.SerializeKillCount(npc.netID, value), -1);
			}
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x006477F4 File Offset: 0x006459F4
		public int GetKillCount(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			return this.GetKillCount(bestiaryCreditId);
		}

		// Token: 0x06004825 RID: 18469 RVA: 0x00647810 File Offset: 0x00645A10
		public void SetKillCountDirectly(string persistentId, int killCount)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				this._killCountsByNpcId[persistentId] = Utils.Clamp<int>(killCount, 0, 999999999);
			}
		}

		// Token: 0x06004826 RID: 18470 RVA: 0x00647864 File Offset: 0x00645A64
		public int GetKillCount(string persistentId)
		{
			int value;
			this._killCountsByNpcId.TryGetValue(persistentId, out value);
			return value;
		}

		// Token: 0x06004827 RID: 18471 RVA: 0x00647884 File Offset: 0x00645A84
		public void Save(BinaryWriter writer)
		{
			Dictionary<string, int> killCountsByNpcId = this._killCountsByNpcId;
			lock (killCountsByNpcId)
			{
				IEnumerable<KeyValuePair<string, int>> vanillaOnly = this._killCountsByNpcId.Where(delegate(KeyValuePair<string, int> pair)
				{
					int netId;
					return ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(pair.Key, out netId) && netId < (int)NPCID.Count;
				});
				writer.Write(vanillaOnly.Count<KeyValuePair<string, int>>());
				foreach (KeyValuePair<string, int> item in vanillaOnly)
				{
					writer.Write(item.Key);
					writer.Write(item.Value);
				}
			}
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x00647940 File Offset: 0x00645B40
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string key = reader.ReadString();
				int value = reader.ReadInt32();
				this._killCountsByNpcId[key] = value;
			}
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x0064797C File Offset: 0x00645B7C
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				reader.ReadString();
				reader.ReadInt32();
			}
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x006479AA File Offset: 0x00645BAA
		public void Reset()
		{
			this._killCountsByNpcId.Clear();
		}

		// Token: 0x0600482B RID: 18475 RVA: 0x006479B8 File Offset: 0x00645BB8
		public void OnPlayerJoining(int playerIndex)
		{
			foreach (KeyValuePair<string, int> item in this._killCountsByNpcId)
			{
				int value;
				if (ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(item.Key, out value))
				{
					NetManager.Instance.SendToClient(NetBestiaryModule.SerializeKillCount(value, item.Value), playerIndex);
				}
			}
		}

		// Token: 0x04005C17 RID: 23575
		private object _entryCreationLock = new object();

		// Token: 0x04005C18 RID: 23576
		public const int POSITIVE_KILL_COUNT_CAP = 999999999;

		// Token: 0x04005C19 RID: 23577
		private Dictionary<string, int> _killCountsByNpcId;
	}
}
