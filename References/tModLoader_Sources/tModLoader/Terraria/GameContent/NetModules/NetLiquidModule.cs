using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020005D8 RID: 1496
	public class NetLiquidModule : NetModule
	{
		// Token: 0x060042FF RID: 17151 RVA: 0x005FBE20 File Offset: 0x005FA020
		public unsafe static NetPacket Serialize(HashSet<int> changes)
		{
			NetPacket result = NetModule.CreatePacket<NetLiquidModule>(changes.Count * 6 + 2);
			result.Writer.Write((ushort)changes.Count);
			foreach (int change in changes)
			{
				int num = change >> 16 & 65535;
				int num2 = change & 65535;
				result.Writer.Write(change);
				result.Writer.Write(*Main.tile[num, num2].liquid);
				result.Writer.Write(Main.tile[num, num2].liquidType());
			}
			return result;
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x005FBEF4 File Offset: 0x005FA0F4
		public unsafe static NetPacket SerializeForPlayer(int playerIndex)
		{
			NetLiquidModule._changesForPlayerCache.Clear();
			foreach (KeyValuePair<Point, NetLiquidModule.ChunkChanges> changesByChunkCoord in NetLiquidModule._changesByChunkCoords)
			{
				if (changesByChunkCoord.Value.BroadcastingCondition(playerIndex))
				{
					NetLiquidModule._changesForPlayerCache.AddRange(changesByChunkCoord.Value.DirtiedPackedTileCoords);
				}
			}
			NetPacket result = NetModule.CreatePacket<NetLiquidModule>(NetLiquidModule._changesForPlayerCache.Count * 6 + 2);
			result.Writer.Write((ushort)NetLiquidModule._changesForPlayerCache.Count);
			foreach (int item in NetLiquidModule._changesForPlayerCache)
			{
				int num = item >> 16 & 65535;
				int num2 = item & 65535;
				result.Writer.Write(item);
				result.Writer.Write(*Main.tile[num, num2].liquid);
				result.Writer.Write(Main.tile[num, num2].liquidType());
			}
			return result;
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x005FC044 File Offset: 0x005FA244
		public unsafe override bool Deserialize(BinaryReader reader, int userId)
		{
			int num = (int)reader.ReadUInt16();
			for (int i = 0; i < num; i++)
			{
				int num4 = reader.ReadInt32();
				byte liquid = reader.ReadByte();
				byte liquidType = reader.ReadByte();
				int num2 = num4 >> 16 & 65535;
				int num3 = num4 & 65535;
				Tile tile = Main.tile[num2, num3];
				if (tile != null)
				{
					*tile.liquid = liquid;
					tile.liquidType((int)liquidType);
				}
			}
			return true;
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x005FC0B8 File Offset: 0x005FA2B8
		public static void CreateAndBroadcastByChunk(HashSet<int> dirtiedPackedTileCoords)
		{
			NetLiquidModule.PrepareChunks(dirtiedPackedTileCoords);
			NetLiquidModule.PrepareAndSendToEachPlayerSeparately();
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x005FC0C8 File Offset: 0x005FA2C8
		private static void PrepareAndSendToEachPlayerSeparately()
		{
			for (int i = 0; i < 256; i++)
			{
				if (Netplay.Clients[i].IsConnected())
				{
					NetManager.Instance.SendToClient(NetLiquidModule.SerializeForPlayer(i), i);
				}
			}
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x005FC104 File Offset: 0x005FA304
		private static void BroadcastEachChunkSeparately()
		{
			foreach (KeyValuePair<Point, NetLiquidModule.ChunkChanges> changesByChunkCoord in NetLiquidModule._changesByChunkCoords)
			{
				NetManager.Instance.Broadcast(NetLiquidModule.Serialize(changesByChunkCoord.Value.DirtiedPackedTileCoords), new NetManager.BroadcastCondition(changesByChunkCoord.Value.BroadcastingCondition), -1);
			}
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x005FC180 File Offset: 0x005FA380
		private static void PrepareChunks(HashSet<int> dirtiedPackedTileCoords)
		{
			foreach (KeyValuePair<Point, NetLiquidModule.ChunkChanges> changesByChunkCoord in NetLiquidModule._changesByChunkCoords)
			{
				changesByChunkCoord.Value.DirtiedPackedTileCoords.Clear();
			}
			NetLiquidModule.DistributeChangesIntoChunks(dirtiedPackedTileCoords);
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x005FC1E4 File Offset: 0x005FA3E4
		private static void BroadcastAllChanges(HashSet<int> dirtiedPackedTileCoords)
		{
			NetManager.Instance.Broadcast(NetLiquidModule.Serialize(dirtiedPackedTileCoords), -1);
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x005FC1F8 File Offset: 0x005FA3F8
		private static void DistributeChangesIntoChunks(HashSet<int> dirtiedPackedTileCoords)
		{
			Point key = default(Point);
			foreach (int dirtiedPackedTileCoord in dirtiedPackedTileCoords)
			{
				int x = dirtiedPackedTileCoord >> 16 & 65535;
				int y = dirtiedPackedTileCoord & 65535;
				key.X = Netplay.GetSectionX(x);
				key.Y = Netplay.GetSectionY(y);
				NetLiquidModule.ChunkChanges value;
				if (!NetLiquidModule._changesByChunkCoords.TryGetValue(key, out value))
				{
					value = new NetLiquidModule.ChunkChanges(key.X, key.Y);
					NetLiquidModule._changesByChunkCoords[key] = value;
				}
				value.DirtiedPackedTileCoords.Add(dirtiedPackedTileCoord);
			}
		}

		// Token: 0x040059E3 RID: 23011
		private static List<int> _changesForPlayerCache = new List<int>();

		// Token: 0x040059E4 RID: 23012
		private static Dictionary<Point, NetLiquidModule.ChunkChanges> _changesByChunkCoords = new Dictionary<Point, NetLiquidModule.ChunkChanges>();

		// Token: 0x02000C6D RID: 3181
		private class ChunkChanges
		{
			// Token: 0x06006000 RID: 24576 RVA: 0x006D1C04 File Offset: 0x006CFE04
			public ChunkChanges(int x, int y)
			{
				this.ChunkX = x;
				this.ChunkY = y;
				this.DirtiedPackedTileCoords = new HashSet<int>();
			}

			// Token: 0x06006001 RID: 24577 RVA: 0x006D1C25 File Offset: 0x006CFE25
			public bool BroadcastingCondition(int clientIndex)
			{
				return Netplay.Clients[clientIndex].TileSections[this.ChunkX, this.ChunkY];
			}

			// Token: 0x0400799C RID: 31132
			public HashSet<int> DirtiedPackedTileCoords;

			// Token: 0x0400799D RID: 31133
			public int ChunkX;

			// Token: 0x0400799E RID: 31134
			public int ChunkY;
		}
	}
}
