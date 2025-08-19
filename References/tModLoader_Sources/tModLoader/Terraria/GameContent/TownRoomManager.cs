using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x020004BF RID: 1215
	public class TownRoomManager
	{
		// Token: 0x06003A38 RID: 14904 RVA: 0x005A697B File Offset: 0x005A4B7B
		public void AddOccupantsToList(int x, int y, List<int> occupantsList)
		{
			this.AddOccupantsToList(new Point(x, y), occupantsList);
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x005A698C File Offset: 0x005A4B8C
		public void AddOccupantsToList(Point tilePosition, List<int> occupants)
		{
			foreach (Tuple<int, Point> roomLocationPair in this._roomLocationPairs)
			{
				if (roomLocationPair.Item2 == tilePosition)
				{
					occupants.Add(roomLocationPair.Item1);
				}
			}
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x005A69F4 File Offset: 0x005A4BF4
		public bool HasRoomQuick(int npcID)
		{
			return this._hasRoom[npcID];
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x005A6A00 File Offset: 0x005A4C00
		public bool HasRoom(int npcID, out Point roomPosition)
		{
			if (!this._hasRoom[npcID])
			{
				roomPosition = new Point(0, 0);
				return false;
			}
			foreach (Tuple<int, Point> roomLocationPair in this._roomLocationPairs)
			{
				if (roomLocationPair.Item1 == npcID)
				{
					roomPosition = roomLocationPair.Item2;
					return true;
				}
			}
			roomPosition = new Point(0, 0);
			return false;
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x005A6A90 File Offset: 0x005A4C90
		public void SetRoom(int npcID, int x, int y)
		{
			this._hasRoom[npcID] = true;
			this.SetRoom(npcID, new Point(x, y));
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x005A6AAC File Offset: 0x005A4CAC
		public void SetRoom(int npcID, Point pt)
		{
			object entityCreationLock = TownRoomManager.EntityCreationLock;
			lock (entityCreationLock)
			{
				this._roomLocationPairs.RemoveAll((Tuple<int, Point> x) => x.Item1 == npcID);
				this._roomLocationPairs.Add(Tuple.Create<int, Point>(npcID, pt));
			}
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x005A6B24 File Offset: 0x005A4D24
		public void KickOut(NPC n)
		{
			this.KickOut(n.type);
			this._hasRoom[n.type] = false;
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x005A6B40 File Offset: 0x005A4D40
		public void KickOut(int npcType)
		{
			object entityCreationLock = TownRoomManager.EntityCreationLock;
			lock (entityCreationLock)
			{
				this._roomLocationPairs.RemoveAll((Tuple<int, Point> x) => x.Item1 == npcType);
			}
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x005A6BA0 File Offset: 0x005A4DA0
		public void DisplayRooms()
		{
			foreach (Tuple<int, Point> roomLocationPair in this._roomLocationPairs)
			{
				Dust.QuickDust(roomLocationPair.Item2, Main.hslToRgb((float)roomLocationPair.Item1 * 0.05f % 1f, 1f, 0.5f, byte.MaxValue));
			}
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x005A6C20 File Offset: 0x005A4E20
		public void Save(BinaryWriter writer)
		{
			object entityCreationLock = TownRoomManager.EntityCreationLock;
			lock (entityCreationLock)
			{
				int count = 0;
				using (List<Tuple<int, Point>>.Enumerator enumerator = this._roomLocationPairs.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Item1 < (int)NPCID.Count)
						{
							count++;
						}
					}
				}
				writer.Write(count);
				foreach (Tuple<int, Point> roomLocationPair in this._roomLocationPairs)
				{
					if (roomLocationPair.Item1 < (int)NPCID.Count)
					{
						writer.Write(roomLocationPair.Item1);
						writer.Write(roomLocationPair.Item2.X);
						writer.Write(roomLocationPair.Item2.Y);
					}
				}
			}
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x005A6D28 File Offset: 0x005A4F28
		public void Load(BinaryReader reader)
		{
			this.Clear();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				int num2 = reader.ReadInt32();
				Point item;
				item..ctor(reader.ReadInt32(), reader.ReadInt32());
				this._roomLocationPairs.Add(Tuple.Create<int, Point>(num2, item));
				this._hasRoom[num2] = true;
			}
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x005A6D84 File Offset: 0x005A4F84
		public void Clear()
		{
			this._roomLocationPairs.Clear();
			for (int i = 0; i < this._hasRoom.Length; i++)
			{
				this._hasRoom[i] = false;
			}
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x005A6DB8 File Offset: 0x005A4FB8
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

		// Token: 0x06003A45 RID: 14917 RVA: 0x005A6DE4 File Offset: 0x005A4FE4
		public bool CanNPCsLiveWithEachOther(int npc1ByType, NPC npc2)
		{
			NPC value;
			return !ContentSamples.NpcsByNetId.TryGetValue(npc1ByType, out value) || this.CanNPCsLiveWithEachOther(value, npc2);
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x005A6E0A File Offset: 0x005A500A
		public bool CanNPCsLiveWithEachOther(NPC npc1, NPC npc2)
		{
			return npc1.housingCategory != npc2.housingCategory;
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x005A6E1D File Offset: 0x005A501D
		public bool CanNPCsLiveWithEachOther_ShopHelper(NPC npc1, NPC npc2)
		{
			return this.CanNPCsLiveWithEachOther(npc1, npc2);
		}

		// Token: 0x040053D4 RID: 21460
		public static object EntityCreationLock = new object();

		// Token: 0x040053D5 RID: 21461
		internal List<Tuple<int, Point>> _roomLocationPairs = new List<Tuple<int, Point>>();

		// Token: 0x040053D6 RID: 21462
		internal bool[] _hasRoom = new bool[NPCLoader.NPCCount];
	}
}
