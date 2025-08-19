using System;
using System.Collections.Generic;
using System.IO;
using Terraria.Map;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000283 RID: 643
	internal static class MapIO
	{
		// Token: 0x06002BD5 RID: 11221 RVA: 0x00524294 File Offset: 0x00522494
		internal static void WriteModFile(string path, bool isCloudSave)
		{
			path = Path.ChangeExtension(path, ".tmap");
			bool hasModData;
			byte[] data;
			using (MemoryStream stream = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(stream))
				{
					hasModData = MapIO.WriteModMap(writer);
					writer.Flush();
					data = stream.ToArray();
				}
			}
			if (hasModData)
			{
				FileUtilities.WriteAllBytes(path, data, isCloudSave);
				return;
			}
			if (isCloudSave && SocialAPI.Cloud != null)
			{
				SocialAPI.Cloud.Delete(path);
				return;
			}
			File.Delete(path);
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x0052432C File Offset: 0x0052252C
		internal static void ReadModFile(string path, bool isCloudSave)
		{
			path = Path.ChangeExtension(path, ".tmap");
			if (!FileUtilities.Exists(path, isCloudSave))
			{
				return;
			}
			MapIO.ReadModMap(new BinaryReader(FileUtilities.ReadAllBytes(path, isCloudSave).ToMemoryStream(false)));
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x0052435C File Offset: 0x0052255C
		internal static bool WriteModMap(BinaryWriter writer)
		{
			ISet<ushort> types = new HashSet<ushort>();
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					ushort type = Main.Map[i, j].Type;
					if (type >= MapHelper.modPosition)
					{
						types.Add(type);
					}
				}
			}
			if (types.Count == 0)
			{
				return false;
			}
			writer.Write((ushort)types.Count);
			foreach (ushort type2 in types)
			{
				writer.Write(type2);
				if (MapLoader.entryToTile.ContainsKey(type2))
				{
					ModTile tile = TileLoader.GetTile((int)MapLoader.entryToTile[type2]);
					writer.Write(true);
					writer.Write(tile.Mod.Name);
					writer.Write(tile.Name);
					writer.Write(type2 - MapHelper.tileLookup[(int)tile.Type]);
				}
				else if (MapLoader.entryToWall.ContainsKey(type2))
				{
					ModWall wall = WallLoader.GetWall((int)MapLoader.entryToWall[type2]);
					writer.Write(false);
					writer.Write(wall.Mod.Name);
					writer.Write(wall.Name);
					writer.Write(type2 - MapHelper.wallLookup[(int)wall.Type]);
				}
				else
				{
					writer.Write(true);
					writer.Write("");
					writer.Write("");
					writer.Write(0);
				}
			}
			MapIO.WriteMapData(writer);
			return true;
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x0052450C File Offset: 0x0052270C
		internal static void ReadModMap(BinaryReader reader)
		{
			IDictionary<ushort, ushort> table = new Dictionary<ushort, ushort>();
			ushort count = reader.ReadUInt16();
			for (ushort i = 0; i < count; i += 1)
			{
				ushort type = reader.ReadUInt16();
				bool flag = reader.ReadBoolean();
				string modName = reader.ReadString();
				string name = reader.ReadString();
				ushort option = reader.ReadUInt16();
				ushort newType = 0;
				ModWall wall;
				if (flag)
				{
					ModTile tile;
					if (ModContent.TryFind<ModTile>(modName, name, out tile))
					{
						if ((int)option >= MapLoader.modTileOptions(tile.Type))
						{
							option = 0;
						}
						newType = (ushort)MapHelper.TileToLookup((int)tile.Type, (int)option);
					}
				}
				else if (ModContent.TryFind<ModWall>(modName, name, out wall))
				{
					if ((int)option >= MapLoader.modWallOptions(wall.Type))
					{
						option = 0;
					}
					newType = MapHelper.wallLookup[(int)wall.Type] + option;
				}
				table[type] = newType;
			}
			MapIO.ReadMapData(reader, table);
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x005245DC File Offset: 0x005227DC
		internal static void WriteMapData(BinaryWriter writer)
		{
			byte skip = 0;
			bool nextModTile = false;
			int i = 0;
			int j = 0;
			do
			{
				MapTile tile = Main.Map[i, j];
				if (tile.Type >= MapHelper.modPosition && tile.Light > 18)
				{
					if (!nextModTile)
					{
						writer.Write(skip);
						skip = 0;
					}
					else
					{
						nextModTile = false;
					}
					MapIO.WriteMapTile(ref i, ref j, writer, ref nextModTile);
				}
				else
				{
					skip += 1;
					if (skip == 255)
					{
						writer.Write(skip);
						skip = 0;
					}
				}
			}
			while (MapIO.NextTile(ref i, ref j));
			if (skip > 0)
			{
				writer.Write(skip);
			}
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x00524664 File Offset: 0x00522864
		internal static void ReadMapData(BinaryReader reader, IDictionary<ushort, ushort> table)
		{
			int i = 0;
			int j = 0;
			bool nextModTile = false;
			for (;;)
			{
				if (!nextModTile)
				{
					byte skip;
					for (skip = reader.ReadByte(); skip == 255; skip = reader.ReadByte())
					{
						for (byte k = 0; k < 255; k += 1)
						{
							if (!MapIO.NextTile(ref i, ref j))
							{
								return;
							}
						}
					}
					for (byte l = 0; l < skip; l += 1)
					{
						if (!MapIO.NextTile(ref i, ref j))
						{
							return;
						}
					}
				}
				else
				{
					nextModTile = false;
				}
				MapIO.ReadMapTile(ref i, ref j, table, reader, ref nextModTile);
				if (!MapIO.NextTile(ref i, ref j))
				{
					return;
				}
			}
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x005246EC File Offset: 0x005228EC
		internal static void WriteMapTile(ref int i, ref int j, BinaryWriter writer, ref bool nextModTile)
		{
			MapTile tile = Main.Map[i, j];
			byte flags = 0;
			byte[] data = new byte[9];
			int index = 1;
			data[index] = (byte)tile.Type;
			index++;
			data[index] = (byte)(tile.Type >> 8);
			index++;
			if (tile.Light < 255)
			{
				flags |= 1;
				data[index] = tile.Light;
				index++;
			}
			if (tile.Color > 0)
			{
				flags |= 2;
				data[index] = tile.Color;
				index++;
			}
			int nextI = i;
			int nextJ = j;
			uint sameCount = 0U;
			while (MapIO.NextTile(ref nextI, ref nextJ))
			{
				MapTile nextTile = Main.Map[nextI, nextJ];
				if (tile.Equals(ref nextTile) && sameCount < 4294967295U)
				{
					sameCount += 1U;
					i = nextI;
					j = nextJ;
				}
				else
				{
					if (nextTile.Type >= MapHelper.modPosition && nextTile.Light > 18)
					{
						flags |= 32;
						nextModTile = true;
						break;
					}
					break;
				}
			}
			if (sameCount > 0U)
			{
				flags |= 4;
				data[index] = (byte)sameCount;
				index++;
				if (sameCount > 255U)
				{
					flags |= 8;
					data[index] = (byte)(sameCount >> 8);
					index++;
					if (sameCount > 65535U)
					{
						flags |= 16;
						data[index] = (byte)(sameCount >> 16);
						index++;
						data[index] = (byte)(sameCount >> 24);
						index++;
					}
				}
			}
			data[0] = flags;
			writer.Write(data, 0, index);
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x00524840 File Offset: 0x00522A40
		internal static void ReadMapTile(ref int i, ref int j, IDictionary<ushort, ushort> table, BinaryReader reader, ref bool nextModTile)
		{
			byte flags = reader.ReadByte();
			ushort type = table[reader.ReadUInt16()];
			byte light = ((flags & 1) == 1) ? reader.ReadByte() : byte.MaxValue;
			byte color = ((flags & 2) == 2) ? reader.ReadByte() : 0;
			MapTile tile = MapTile.Create(type, light, color);
			Main.Map.SetTile(i, j, ref tile);
			if ((flags & 4) == 4)
			{
				uint sameCount;
				if ((flags & 16) == 16)
				{
					sameCount = reader.ReadUInt32();
				}
				else if ((flags & 8) == 8)
				{
					sameCount = (uint)reader.ReadUInt16();
				}
				else
				{
					sameCount = (uint)reader.ReadByte();
				}
				for (uint k = 0U; k < sameCount; k += 1U)
				{
					MapIO.NextTile(ref i, ref j);
					tile = MapTile.Create(type, light, color);
					Main.Map.SetTile(i, j, ref tile);
				}
			}
			if ((flags & 32) == 32)
			{
				nextModTile = true;
			}
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x0052490E File Offset: 0x00522B0E
		private static bool NextTile(ref int i, ref int j)
		{
			j++;
			if (j >= Main.maxTilesY)
			{
				j = 0;
				i++;
				if (i >= Main.maxTilesX)
				{
					return false;
				}
			}
			return true;
		}
	}
}
