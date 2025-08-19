using System;
using System.Collections.Generic;
using System.IO;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002F1 RID: 753
	public class NPCKillsTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x06002386 RID: 9094 RVA: 0x00556C18 File Offset: 0x00554E18
		public NPCKillsTracker()
		{
			this._killCountsByNpcId = new Dictionary<string, int>();
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x00556C38 File Offset: 0x00554E38
		public void RegisterKill(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			int num;
			this._killCountsByNpcId.TryGetValue(bestiaryCreditId, out num);
			num++;
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				this._killCountsByNpcId[bestiaryCreditId] = Utils.Clamp<int>(num, 0, 999999999);
			}
			if (Main.netMode == 2)
			{
				NetManager.Instance.Broadcast(NetBestiaryModule.SerializeKillCount(npc.netID, num), -1);
			}
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x00556CC4 File Offset: 0x00554EC4
		public int GetKillCount(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			return this.GetKillCount(bestiaryCreditId);
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x00556CE0 File Offset: 0x00554EE0
		public void SetKillCountDirectly(string persistentId, int killCount)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				this._killCountsByNpcId[persistentId] = Utils.Clamp<int>(killCount, 0, 999999999);
			}
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x00556D34 File Offset: 0x00554F34
		public int GetKillCount(string persistentId)
		{
			int result;
			this._killCountsByNpcId.TryGetValue(persistentId, out result);
			return result;
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x00556D54 File Offset: 0x00554F54
		public void Save(BinaryWriter writer)
		{
			Dictionary<string, int> killCountsByNpcId = this._killCountsByNpcId;
			lock (killCountsByNpcId)
			{
				writer.Write(this._killCountsByNpcId.Count);
				foreach (KeyValuePair<string, int> keyValuePair in this._killCountsByNpcId)
				{
					writer.Write(keyValuePair.Key);
					writer.Write(keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x00556DF4 File Offset: 0x00554FF4
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

		// Token: 0x0600238D RID: 9101 RVA: 0x00556E30 File Offset: 0x00555030
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				reader.ReadString();
				reader.ReadInt32();
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x00556E5E File Offset: 0x0055505E
		public void Reset()
		{
			this._killCountsByNpcId.Clear();
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x00556E6C File Offset: 0x0055506C
		public void OnPlayerJoining(int playerIndex)
		{
			foreach (KeyValuePair<string, int> keyValuePair in this._killCountsByNpcId)
			{
				int npcNetId;
				if (ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(keyValuePair.Key, out npcNetId))
				{
					NetManager.Instance.SendToClient(NetBestiaryModule.SerializeKillCount(npcNetId, keyValuePair.Value), playerIndex);
				}
			}
		}

		// Token: 0x0400482E RID: 18478
		private object _entryCreationLock = new object();

		// Token: 0x0400482F RID: 18479
		public const int POSITIVE_KILL_COUNT_CAP = 999999999;

		// Token: 0x04004830 RID: 18480
		private Dictionary<string, int> _killCountsByNpcId;
	}
}
