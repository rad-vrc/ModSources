using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x02000274 RID: 628
	public class NetLiquidModule : NetModule
	{
		// Token: 0x06001FC8 RID: 8136 RVA: 0x00516B38 File Offset: 0x00514D38
		public static NetPacket Serialize(HashSet<int> changes)
		{
			NetPacket result = NetModule.CreatePacket<NetLiquidModule>(changes.Count * 6 + 2);
			result.Writer.Write((ushort)changes.Count);
			foreach (int num in changes)
			{
				int num2 = num >> 16 & 65535;
				int num3 = num & 65535;
				result.Writer.Write(num);
				result.Writer.Write(Main.tile[num2, num3].liquid);
				result.Writer.Write(Main.tile[num2, num3].liquidType());
			}
			return result;
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x00516C00 File Offset: 0x00514E00
		public static NetPacket SerializeForPlayer(int playerIndex)
		{
			NetLiquidModule._changesForPlayerCache.Clear();
			foreach (KeyValuePair<Point, NetLiquidModule.ChunkChanges> keyValuePair in NetLiquidModule._changesByChunkCoords)
			{
				if (keyValuePair.Value.BroadcastingCondition(playerIndex))
				{
					NetLiquidModule._changesForPlayerCache.AddRange(keyValuePair.Value.DirtiedPackedTileCoords);
				}
			}
			NetPacket result = NetModule.CreatePacket<NetLiquidModule>(NetLiquidModule._changesForPlayerCache.Count * 6 + 2);
			result.Writer.Write((ushort)NetLiquidModule._changesForPlayerCache.Count);
			foreach (int num in NetLiquidModule._changesForPlayerCache)
			{
				int num2 = num >> 16 & 65535;
				int num3 = num & 65535;
				result.Writer.Write(num);
				result.Writer.Write(Main.tile[num2, num3].liquid);
				result.Writer.Write(Main.tile[num2, num3].liquidType());
			}
			return result;
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x00516D44 File Offset: 0x00514F44
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			int num = (int)reader.ReadUInt16();
			for (int i = 0; i < num; i++)
			{
				int num2 = reader.ReadInt32();
				byte liquid = reader.ReadByte();
				byte liquidType = reader.ReadByte();
				int num3 = num2 >> 16 & 65535;
				int num4 = num2 & 65535;
				Tile tile = Main.tile[num3, num4];
				if (tile != null)
				{
					tile.liquid = liquid;
					tile.liquidType((int)liquidType);
				}
			}
			return true;
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x00516DB1 File Offset: 0x00514FB1
		public static void CreateAndBroadcastByChunk(HashSet<int> dirtiedPackedTileCoords)
		{
			NetLiquidModule.PrepareChunks(dirtiedPackedTileCoords);
			NetLiquidModule.PrepareAndSendToEachPlayerSeparately();
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x00516DC0 File Offset: 0x00514FC0
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

		// Token: 0x06001FCD RID: 8141 RVA: 0x00516DFC File Offset: 0x00514FFC
		private static void BroadcastEachChunkSeparately()
		{
			foreach (KeyValuePair<Point, NetLiquidModule.ChunkChanges> keyValuePair in NetLiquidModule._changesByChunkCoords)
			{
				NetManager.Instance.Broadcast(NetLiquidModule.Serialize(keyValuePair.Value.DirtiedPackedTileCoords), new NetManager.BroadcastCondition(keyValuePair.Value.BroadcastingCondition), -1);
			}
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x00516E78 File Offset: 0x00515078
		private static void PrepareChunks(HashSet<int> dirtiedPackedTileCoords)
		{
			foreach (KeyValuePair<Point, NetLiquidModule.ChunkChanges> keyValuePair in NetLiquidModule._changesByChunkCoords)
			{
				keyValuePair.Value.DirtiedPackedTileCoords.Clear();
			}
			NetLiquidModule.DistributeChangesIntoChunks(dirtiedPackedTileCoords);
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x00516EDC File Offset: 0x005150DC
		private static void BroadcastAllChanges(HashSet<int> dirtiedPackedTileCoords)
		{
			NetManager.Instance.Broadcast(NetLiquidModule.Serialize(dirtiedPackedTileCoords), -1);
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x00516EF0 File Offset: 0x005150F0
		private static void DistributeChangesIntoChunks(HashSet<int> dirtiedPackedTileCoords)
		{
			foreach (int num in dirtiedPackedTileCoords)
			{
				int x = num >> 16 & 65535;
				int y = num & 65535;
				Point point;
				point.X = Netplay.GetSectionX(x);
				point.Y = Netplay.GetSectionY(y);
				NetLiquidModule.ChunkChanges chunkChanges;
				if (!NetLiquidModule._changesByChunkCoords.TryGetValue(point, out chunkChanges))
				{
					chunkChanges = new NetLiquidModule.ChunkChanges(point.X, point.Y);
					NetLiquidModule._changesByChunkCoords[point] = chunkChanges;
				}
				chunkChanges.DirtiedPackedTileCoords.Add(num);
			}
		}

		// Token: 0x04004694 RID: 18068
		private static List<int> _changesForPlayerCache = new List<int>();

		// Token: 0x04004695 RID: 18069
		private static Dictionary<Point, NetLiquidModule.ChunkChanges> _changesByChunkCoords = new Dictionary<Point, NetLiquidModule.ChunkChanges>();

		// Token: 0x02000645 RID: 1605
		private class ChunkChanges
		{
			// Token: 0x060033E3 RID: 13283 RVA: 0x00607614 File Offset: 0x00605814
			public ChunkChanges(int x, int y)
			{
				this.ChunkX = x;
				this.ChunkY = y;
				this.DirtiedPackedTileCoords = new HashSet<int>();
			}

			// Token: 0x060033E4 RID: 13284 RVA: 0x00607635 File Offset: 0x00605835
			public bool BroadcastingCondition(int clientIndex)
			{
				return Netplay.Clients[clientIndex].TileSections[this.ChunkX, this.ChunkY];
			}

			// Token: 0x0400615E RID: 24926
			public HashSet<int> DirtiedPackedTileCoords;

			// Token: 0x0400615F RID: 24927
			public int ChunkX;

			// Token: 0x04006160 RID: 24928
			public int ChunkY;
		}
	}
}
