using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020006A2 RID: 1698
	public class NPCWasNearPlayerTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x06004849 RID: 18505 RVA: 0x00648B68 File Offset: 0x00646D68
		public void PrepareSamplesBasedOptimizations()
		{
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x00648B6A File Offset: 0x00646D6A
		public NPCWasNearPlayerTracker()
		{
			this._wasNearPlayer = new HashSet<string>();
			this._playerHitboxesForBestiary = new List<Rectangle>();
			this._wasSeenNearPlayerByNetId = new List<int>();
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x00648BA0 File Offset: 0x00646DA0
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

		// Token: 0x0600484C RID: 18508 RVA: 0x00648C20 File Offset: 0x00646E20
		public void SetWasSeenDirectly(string persistentId)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				this._wasNearPlayer.Add(persistentId);
			}
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x00648C68 File Offset: 0x00646E68
		public bool GetWasNearbyBefore(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			return this.GetWasNearbyBefore(bestiaryCreditId);
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x00648C83 File Offset: 0x00646E83
		public bool GetWasNearbyBefore(string persistentIdentifier)
		{
			return this._wasNearPlayer.Contains(persistentIdentifier);
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x00648C94 File Offset: 0x00646E94
		public void Save(BinaryWriter writer)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				IEnumerable<string> vanillaOnly = this._wasNearPlayer.Where(delegate(string persistentId)
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

		// Token: 0x06004850 RID: 18512 RVA: 0x00648D3C File Offset: 0x00646F3C
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string item = reader.ReadString();
				this._wasNearPlayer.Add(item);
			}
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x00648D70 File Offset: 0x00646F70
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				reader.ReadString();
			}
		}

		// Token: 0x06004852 RID: 18514 RVA: 0x00648D97 File Offset: 0x00646F97
		public void Reset()
		{
			this._wasNearPlayer.Clear();
			this._playerHitboxesForBestiary.Clear();
			this._wasSeenNearPlayerByNetId.Clear();
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x00648DBC File Offset: 0x00646FBC
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
				NPC nPC = Main.npc[j];
				if (nPC.active && nPC.CountsAsACritter && !this._wasSeenNearPlayerByNetId.Contains(nPC.netID))
				{
					Rectangle hitbox = nPC.Hitbox;
					for (int k = 0; k < this._playerHitboxesForBestiary.Count; k++)
					{
						Rectangle value = this._playerHitboxesForBestiary[k];
						if (hitbox.Intersects(value))
						{
							this._wasSeenNearPlayerByNetId.Add(nPC.netID);
							this.RegisterWasNearby(nPC);
						}
					}
				}
			}
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x00648E9C File Offset: 0x0064709C
		public void OnPlayerJoining(int playerIndex)
		{
			foreach (string item in this._wasNearPlayer)
			{
				int value;
				if (ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(item, out value))
				{
					NetManager.Instance.SendToClient(NetBestiaryModule.SerializeSight(value), playerIndex);
				}
			}
		}

		// Token: 0x04005C26 RID: 23590
		private object _entryCreationLock = new object();

		// Token: 0x04005C27 RID: 23591
		private HashSet<string> _wasNearPlayer;

		// Token: 0x04005C28 RID: 23592
		private List<Rectangle> _playerHitboxesForBestiary;

		// Token: 0x04005C29 RID: 23593
		private List<int> _wasSeenNearPlayerByNetId;
	}
}
