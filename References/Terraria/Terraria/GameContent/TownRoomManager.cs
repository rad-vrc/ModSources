using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x02000200 RID: 512
	public class TownRoomManager
	{
		// Token: 0x06001D3F RID: 7487 RVA: 0x00501509 File Offset: 0x004FF709
		public void AddOccupantsToList(int x, int y, List<int> occupantsList)
		{
			this.AddOccupantsToList(new Point(x, y), occupantsList);
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x0050151C File Offset: 0x004FF71C
		public void AddOccupantsToList(Point tilePosition, List<int> occupants)
		{
			foreach (Tuple<int, Point> tuple in this._roomLocationPairs)
			{
				if (tuple.Item2 == tilePosition)
				{
					occupants.Add(tuple.Item1);
				}
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x00501584 File Offset: 0x004FF784
		public bool HasRoomQuick(int npcID)
		{
			return this._hasRoom[npcID];
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x00501590 File Offset: 0x004FF790
		public bool HasRoom(int npcID, out Point roomPosition)
		{
			if (!this._hasRoom[npcID])
			{
				roomPosition = new Point(0, 0);
				return false;
			}
			foreach (Tuple<int, Point> tuple in this._roomLocationPairs)
			{
				if (tuple.Item1 == npcID)
				{
					roomPosition = tuple.Item2;
					return true;
				}
			}
			roomPosition = new Point(0, 0);
			return false;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x00501620 File Offset: 0x004FF820
		public void SetRoom(int npcID, int x, int y)
		{
			this._hasRoom[npcID] = true;
			this.SetRoom(npcID, new Point(x, y));
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0050163C File Offset: 0x004FF83C
		public void SetRoom(int npcID, Point pt)
		{
			object entityCreationLock = TownRoomManager.EntityCreationLock;
			lock (entityCreationLock)
			{
				this._roomLocationPairs.RemoveAll((Tuple<int, Point> x) => x.Item1 == npcID);
				this._roomLocationPairs.Add(Tuple.Create<int, Point>(npcID, pt));
			}
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x005016B4 File Offset: 0x004FF8B4
		public void KickOut(NPC n)
		{
			this.KickOut(n.type);
			this._hasRoom[n.type] = false;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x005016D0 File Offset: 0x004FF8D0
		public void KickOut(int npcType)
		{
			object entityCreationLock = TownRoomManager.EntityCreationLock;
			lock (entityCreationLock)
			{
				this._roomLocationPairs.RemoveAll((Tuple<int, Point> x) => x.Item1 == npcType);
			}
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00501730 File Offset: 0x004FF930
		public void DisplayRooms()
		{
			foreach (Tuple<int, Point> tuple in this._roomLocationPairs)
			{
				Dust.QuickDust(tuple.Item2, Main.hslToRgb((float)tuple.Item1 * 0.05f % 1f, 1f, 0.5f, byte.MaxValue));
			}
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x005017B0 File Offset: 0x004FF9B0
		public void Save(BinaryWriter writer)
		{
			object entityCreationLock = TownRoomManager.EntityCreationLock;
			lock (entityCreationLock)
			{
				writer.Write(this._roomLocationPairs.Count);
				foreach (Tuple<int, Point> tuple in this._roomLocationPairs)
				{
					writer.Write(tuple.Item1);
					writer.Write(tuple.Item2.X);
					writer.Write(tuple.Item2.Y);
				}
			}
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x00501864 File Offset: 0x004FFA64
		public void Load(BinaryReader reader)
		{
			this.Clear();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				int num2 = reader.ReadInt32();
				Point item = new Point(reader.ReadInt32(), reader.ReadInt32());
				this._roomLocationPairs.Add(Tuple.Create<int, Point>(num2, item));
				this._hasRoom[num2] = true;
			}
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x005018C0 File Offset: 0x004FFAC0
		public void Clear()
		{
			this._roomLocationPairs.Clear();
			for (int i = 0; i < this._hasRoom.Length; i++)
			{
				this._hasRoom[i] = false;
			}
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x005018F4 File Offset: 0x004FFAF4
		public byte GetHouseholdStatus(NPC n)
		{
			byte result = 0;
			if (n.homeless)
			{
				result = 1;
			}
			else if (this.HasRoomQuick(n.type))
			{
				result = 2;
			}
			return result;
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x00501920 File Offset: 0x004FFB20
		public bool CanNPCsLiveWithEachOther(int npc1ByType, NPC npc2)
		{
			NPC npc3;
			return !ContentSamples.NpcsByNetId.TryGetValue(npc1ByType, out npc3) || this.CanNPCsLiveWithEachOther(npc3, npc2);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x00501946 File Offset: 0x004FFB46
		public bool CanNPCsLiveWithEachOther(NPC npc1, NPC npc2)
		{
			return npc1.housingCategory != npc2.housingCategory;
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x00501959 File Offset: 0x004FFB59
		public bool CanNPCsLiveWithEachOther_ShopHelper(NPC npc1, NPC npc2)
		{
			return this.CanNPCsLiveWithEachOther(npc1, npc2);
		}

		// Token: 0x0400441E RID: 17438
		public static object EntityCreationLock = new object();

		// Token: 0x0400441F RID: 17439
		private List<Tuple<int, Point>> _roomLocationPairs = new List<Tuple<int, Point>>();

		// Token: 0x04004420 RID: 17440
		private bool[] _hasRoom = new bool[(int)NPCID.Count];
	}
}
