using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002F2 RID: 754
	public class NPCWasNearPlayerTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x06002390 RID: 9104 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void PrepareSamplesBasedOptimizations()
		{
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x00556EE8 File Offset: 0x005550E8
		public NPCWasNearPlayerTracker()
		{
			this._wasNearPlayer = new HashSet<string>();
			this._playerHitboxesForBestiary = new List<Rectangle>();
			this._wasSeenNearPlayerByNetId = new List<int>();
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x00556F1C File Offset: 0x0055511C
		public void RegisterWasNearby(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			bool flag = !this._wasNearPlayer.Contains(bestiaryCreditId);
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				this._wasNearPlayer.Add(bestiaryCreditId);
			}
			if (Main.netMode == 2 && flag)
			{
				NetManager.Instance.Broadcast(NetBestiaryModule.SerializeSight(npc.netID), -1);
			}
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x00556F9C File Offset: 0x0055519C
		public void SetWasSeenDirectly(string persistentId)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				this._wasNearPlayer.Add(persistentId);
			}
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x00556FE4 File Offset: 0x005551E4
		public bool GetWasNearbyBefore(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			return this.GetWasNearbyBefore(bestiaryCreditId);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x00556FFF File Offset: 0x005551FF
		public bool GetWasNearbyBefore(string persistentIdentifier)
		{
			return this._wasNearPlayer.Contains(persistentIdentifier);
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x00557010 File Offset: 0x00555210
		public void Save(BinaryWriter writer)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				writer.Write(this._wasNearPlayer.Count);
				foreach (string value in this._wasNearPlayer)
				{
					writer.Write(value);
				}
			}
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x0055709C File Offset: 0x0055529C
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string item = reader.ReadString();
				this._wasNearPlayer.Add(item);
			}
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x005570D0 File Offset: 0x005552D0
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				reader.ReadString();
			}
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x005570F7 File Offset: 0x005552F7
		public void Reset()
		{
			this._wasNearPlayer.Clear();
			this._playerHitboxesForBestiary.Clear();
			this._wasSeenNearPlayerByNetId.Clear();
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x0055711C File Offset: 0x0055531C
		public void ScanWorldForFinds()
		{
			this._playerHitboxesForBestiary.Clear();
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active)
				{
					this._playerHitboxesForBestiary.Add(player.HitboxForBestiaryNearbyCheck);
				}
			}
			for (int j = 0; j < 200; j++)
			{
				NPC npc = Main.npc[j];
				if (npc.active && npc.CountsAsACritter && !this._wasSeenNearPlayerByNetId.Contains(npc.netID))
				{
					Rectangle hitbox = npc.Hitbox;
					for (int k = 0; k < this._playerHitboxesForBestiary.Count; k++)
					{
						Rectangle value = this._playerHitboxesForBestiary[k];
						if (hitbox.Intersects(value))
						{
							this._wasSeenNearPlayerByNetId.Add(npc.netID);
							this.RegisterWasNearby(npc);
						}
					}
				}
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x005571FC File Offset: 0x005553FC
		public void OnPlayerJoining(int playerIndex)
		{
			foreach (string key in this._wasNearPlayer)
			{
				int npcNetId;
				if (ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(key, out npcNetId))
				{
					NetManager.Instance.SendToClient(NetBestiaryModule.SerializeSight(npcNetId), playerIndex);
				}
			}
		}

		// Token: 0x04004831 RID: 18481
		private object _entryCreationLock = new object();

		// Token: 0x04004832 RID: 18482
		private HashSet<string> _wasNearPlayer;

		// Token: 0x04004833 RID: 18483
		private List<Rectangle> _playerHitboxesForBestiary;

		// Token: 0x04004834 RID: 18484
		private List<int> _wasSeenNearPlayerByNetId;
	}
}
