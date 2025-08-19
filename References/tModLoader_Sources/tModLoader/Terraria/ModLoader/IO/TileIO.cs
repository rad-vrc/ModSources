using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.Exceptions;

namespace Terraria.ModLoader.IO
{
	// Token: 0x0200029A RID: 666
	internal static class TileIO
	{
		// Token: 0x06002C8D RID: 11405 RVA: 0x00527BF0 File Offset: 0x00525DF0
		internal unsafe static void VanillaSaveFrames(Tile tile, ref short frameX)
		{
			if (*tile.type == 128 || *tile.type == 269)
			{
				int slot = (int)(*tile.frameX / 100);
				int position = (int)(*tile.frameY / 18);
				if (TileIO.HasModArmor(slot, position))
				{
					frameX %= 100;
				}
			}
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x00527C44 File Offset: 0x00525E44
		internal unsafe static TagCompound SaveContainers()
		{
			MemoryStream ms = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(ms);
			byte[] flags = new byte[1];
			byte numFlags = 0;
			ISet<int> headSlots = new HashSet<int>();
			ISet<int> bodySlots = new HashSet<int>();
			ISet<int> legSlots = new HashSet<int>();
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile.active() && (*tile.type == 128 || *tile.type == 269))
					{
						int slot = (int)(*tile.frameX / 100);
						int position = (int)(*tile.frameY / 18);
						if (TileIO.HasModArmor(slot, position))
						{
							if (position == 0)
							{
								headSlots.Add(slot);
							}
							else if (position == 1)
							{
								bodySlots.Add(slot);
							}
							else if (position == 2)
							{
								legSlots.Add(slot);
							}
							byte[] array = flags;
							int num = 0;
							array[num] |= 1;
							numFlags = 1;
						}
					}
				}
			}
			int tileEntity = 0;
			List<TagCompound> itemFrames = new List<TagCompound>();
			foreach (KeyValuePair<int, TileEntity> entity in TileEntity.ByID)
			{
				TEItemFrame itemFrame = entity.Value as TEItemFrame;
				if (itemFrame != null)
				{
					List<TagCompound> globalData = ItemIO.SaveGlobals(itemFrame.item);
					if (globalData != null || ItemLoader.NeedsModSaving(itemFrame.item))
					{
						List<TagCompound> list = itemFrames;
						TagCompound tagCompound = new TagCompound();
						tagCompound["id"] = tileEntity;
						tagCompound["item"] = ItemIO.Save(itemFrame.item, globalData);
						list.Add(tagCompound);
						numFlags = 1;
					}
				}
				if (!(entity.Value is ModTileEntity))
				{
					tileEntity++;
				}
			}
			if (numFlags == 0)
			{
				return null;
			}
			writer.Write(numFlags);
			writer.Write(flags, 0, (int)numFlags);
			if ((flags[0] & 1) == 1)
			{
				writer.Write((ushort)headSlots.Count);
				foreach (int slot2 in headSlots)
				{
					writer.Write((ushort)slot2);
					ModItem item = ItemLoader.GetItem(EquipLoader.slotToId[EquipType.Head][slot2]);
					writer.Write(item.Mod.Name);
					writer.Write(item.Name);
				}
				writer.Write((ushort)bodySlots.Count);
				foreach (int slot3 in bodySlots)
				{
					writer.Write((ushort)slot3);
					ModItem item2 = ItemLoader.GetItem(EquipLoader.slotToId[EquipType.Body][slot3]);
					writer.Write(item2.Mod.Name);
					writer.Write(item2.Name);
				}
				writer.Write((ushort)legSlots.Count);
				foreach (int slot4 in legSlots)
				{
					writer.Write((ushort)slot4);
					ModItem item3 = ItemLoader.GetItem(EquipLoader.slotToId[EquipType.Legs][slot4]);
					writer.Write(item3.Mod.Name);
					writer.Write(item3.Name);
				}
				TileIO.WriteContainerData(writer);
			}
			TagCompound tag = new TagCompound();
			tag.Set("data", ms.ToArray(), false);
			if (itemFrames.Count > 0)
			{
				tag.Set("itemFrames", itemFrames, false);
			}
			return tag;
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x0052800C File Offset: 0x0052620C
		internal static void LoadContainers(TagCompound tag)
		{
			if (tag.ContainsKey("data"))
			{
				TileIO.ReadContainers(new BinaryReader(tag.GetByteArray("data").ToMemoryStream(false)));
			}
			foreach (TagCompound frameTag in tag.GetList<TagCompound>("itemFrames"))
			{
				TileEntity tileEntity;
				if (TileEntity.ByID.TryGetValue(frameTag.GetInt("id"), out tileEntity))
				{
					TEItemFrame itemFrame = tileEntity as TEItemFrame;
					if (itemFrame != null)
					{
						ItemIO.Load(itemFrame.item, frameTag.GetCompound("item"));
						continue;
					}
				}
				Logging.tML.Warn("Due to a bug in previous versions of tModLoader, the following ItemFrame data has been lost: " + frameTag.ToString());
			}
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x005280D4 File Offset: 0x005262D4
		internal static void ReadContainers(BinaryReader reader)
		{
			byte[] flags = new byte[1];
			reader.Read(flags, 0, (int)reader.ReadByte());
			if ((flags[0] & 1) == 1)
			{
				TileIO.ContainerTables tables = TileIO.ContainerTables.Create();
				int count = (int)reader.ReadUInt16();
				for (int i = 0; i < count; i++)
				{
					ModItem item;
					tables.headSlots[(int)reader.ReadUInt16()] = (ModContent.TryFind<ModItem>(reader.ReadString(), reader.ReadString(), out item) ? item.Item.headSlot : 0);
				}
				count = (int)reader.ReadUInt16();
				for (int j = 0; j < count; j++)
				{
					ModItem item2;
					tables.bodySlots[(int)reader.ReadUInt16()] = (ModContent.TryFind<ModItem>(reader.ReadString(), reader.ReadString(), out item2) ? item2.Item.bodySlot : 0);
				}
				count = (int)reader.ReadUInt16();
				for (int k = 0; k < count; k++)
				{
					ModItem item3;
					tables.legSlots[(int)reader.ReadUInt16()] = (ModContent.TryFind<ModItem>(reader.ReadString(), reader.ReadString(), out item3) ? item3.Item.legSlot : 0);
				}
				TileIO.ReadContainerData(reader, tables);
			}
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x005281F0 File Offset: 0x005263F0
		internal unsafe static void WriteContainerData(BinaryWriter writer)
		{
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile.active() && (*tile.type == 128 || *tile.type == 269))
					{
						int slot = (int)(*tile.frameX / 100);
						int frameX = (int)(*tile.frameX % 100);
						int position = (int)(*tile.frameY / 18);
						if (TileIO.HasModArmor(slot, position) && frameX % 36 == 0)
						{
							writer.Write(i);
							writer.Write(j);
							writer.Write((byte)position);
							writer.Write((ushort)slot);
						}
					}
				}
			}
			writer.Write(-1);
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x005282BC File Offset: 0x005264BC
		internal unsafe static void ReadContainerData(BinaryReader reader, TileIO.ContainerTables tables)
		{
			for (int i = reader.ReadInt32(); i > 0; i = reader.ReadInt32())
			{
				int j = reader.ReadInt32();
				int position = (int)reader.ReadByte();
				int slot = (int)reader.ReadUInt16();
				Tile left = Main.tile[i, j];
				Tile right = Main.tile[i + 1, j];
				if (left.active() && right.active() && (*left.type == 128 || *left.type == 269) && *left.type == *right.type && (*left.frameX == 0 || *left.frameX == 36) && *right.frameX == *left.frameX + 18 && (int)(*left.frameY / 18) == position && *left.frameY == *right.frameY)
				{
					if (position == 0)
					{
						slot = tables.headSlots[slot];
					}
					else if (position == 1)
					{
						slot = tables.bodySlots[slot];
					}
					else if (position == 2)
					{
						slot = tables.legSlots[slot];
					}
					ref short frameX = ref left.frameX;
					frameX += (short)(100 * slot);
				}
			}
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x005283F6 File Offset: 0x005265F6
		private static bool HasModArmor(int slot, int position)
		{
			if (position == 0)
			{
				return slot >= ArmorIDs.Head.Count;
			}
			if (position == 1)
			{
				return slot >= ArmorIDs.Body.Count;
			}
			return position == 2 && slot >= ArmorIDs.Legs.Count;
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x00528428 File Offset: 0x00526628
		internal static void LoadBasics(TagCompound tag)
		{
			TileEntry[] tileEntriesLookup;
			TileIO.Tiles.LoadEntries(tag, out tileEntriesLookup);
			WallEntry[] wallEntriesLookup;
			TileIO.Walls.LoadEntries(tag, out wallEntriesLookup);
			if (!tag.ContainsKey("wallData"))
			{
				TileIO.LoadLegacy(tag, tileEntriesLookup, wallEntriesLookup);
			}
			else
			{
				TileIO.Tiles.LoadData(tag, tileEntriesLookup);
				TileIO.Walls.LoadData(tag, wallEntriesLookup);
			}
			WorldIO.ValidateSigns();
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x00528484 File Offset: 0x00526684
		internal static TagCompound SaveBasics()
		{
			TagCompound tag = new TagCompound();
			TileIO.Tiles.Save(tag);
			TileIO.Walls.Save(tag);
			return tag;
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x005284AE File Offset: 0x005266AE
		internal static void ResetUnloadedTypes()
		{
			TileIO.Tiles.unloadedTypes.Clear();
			TileIO.Walls.unloadedTypes.Clear();
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x005284CE File Offset: 0x005266CE
		internal static void ClearWorld()
		{
			TileIO.Tiles.Clear();
			TileIO.Walls.Clear();
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x005284E4 File Offset: 0x005266E4
		internal static void LoadLegacy(TagCompound tag, TileEntry[] tileEntriesLookup, WallEntry[] wallEntriesLookup)
		{
			if (!tag.ContainsKey("data"))
			{
				return;
			}
			using (BinaryReader reader = new BinaryReader(tag.GetByteArray("data").ToMemoryStream(false)))
			{
				List<PosData<ushort>> tilePosMapList;
				List<PosData<ushort>> wallPosMapList;
				TileIO.ReadTileData(reader, tileEntriesLookup, wallEntriesLookup, out tilePosMapList, out wallPosMapList);
				TileIO.Tiles.unloadedEntryLookup = tilePosMapList.ToArray();
				TileIO.Walls.unloadedEntryLookup = wallPosMapList.ToArray();
			}
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x00528560 File Offset: 0x00526760
		internal static void ReadTileData(BinaryReader reader, TileEntry[] tileEntriesLookup, WallEntry[] wallEntriesLookup, out List<PosData<ushort>> wallPosMapList, out List<PosData<ushort>> tilePosMapList)
		{
			int i = 0;
			int j = 0;
			bool nextModTile = false;
			tilePosMapList = new List<PosData<ushort>>();
			wallPosMapList = new List<PosData<ushort>>();
			for (;;)
			{
				if (!nextModTile)
				{
					byte skip;
					for (skip = reader.ReadByte(); skip == 255; skip = reader.ReadByte())
					{
						for (byte k = 0; k < 255; k += 1)
						{
							if (!TileIO.NextLocation(ref i, ref j))
							{
								return;
							}
						}
					}
					for (byte l = 0; l < skip; l += 1)
					{
						if (!TileIO.NextLocation(ref i, ref j))
						{
							return;
						}
					}
				}
				else
				{
					nextModTile = false;
				}
				TileIO.ReadModTile(ref i, ref j, reader, ref nextModTile, tilePosMapList, wallPosMapList, tileEntriesLookup, wallEntriesLookup);
				if (!TileIO.NextLocation(ref i, ref j))
				{
					return;
				}
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x00528600 File Offset: 0x00526800
		internal unsafe static void ReadModTile(ref int i, ref int j, BinaryReader reader, ref bool nextModTile, List<PosData<ushort>> wallPosMapList, List<PosData<ushort>> tilePosMapList, TileEntry[] tileEntriesLookup, WallEntry[] wallEntriesLookup)
		{
			byte flags = reader.ReadByte();
			Tile tile = Main.tile[i, j];
			if ((flags & 1) == 1)
			{
				tile.active(true);
				ushort saveType = reader.ReadUInt16();
				TileEntry tEntry = tileEntriesLookup[(int)saveType];
				*tile.type = tEntry.loadedType;
				if (tEntry.frameImportant)
				{
					if ((flags & 2) == 2)
					{
						*tile.frameX = reader.ReadInt16();
					}
					else
					{
						*tile.frameX = (short)reader.ReadByte();
					}
					if ((flags & 4) == 4)
					{
						*tile.frameY = reader.ReadInt16();
					}
					else
					{
						*tile.frameY = (short)reader.ReadByte();
					}
				}
				else
				{
					*tile.frameX = -1;
					*tile.frameY = -1;
				}
				if ((flags & 8) == 8)
				{
					tile.color(reader.ReadByte());
				}
				if (tEntry.IsUnloaded)
				{
					tilePosMapList.Add(new PosData<ushort>(PosData.CoordsToPos(i, j), tEntry.type));
				}
				WorldGen.tileCounts[(int)(*tile.type)] += (((double)j <= Main.worldSurface) ? 5 : 1);
			}
			if ((flags & 16) == 16)
			{
				ushort saveType = reader.ReadUInt16();
				WallEntry wEntry = wallEntriesLookup[(int)saveType];
				*tile.wall = wEntry.loadedType;
				if (wEntry.IsUnloaded)
				{
					wallPosMapList.Add(new PosData<ushort>(PosData.CoordsToPos(i, j), wEntry.type));
				}
				if ((flags & 32) == 32)
				{
					tile.wallColor(reader.ReadByte());
				}
			}
			if ((flags & 64) == 64)
			{
				byte sameCount = reader.ReadByte();
				for (byte k = 0; k < sameCount; k += 1)
				{
					TileIO.NextLocation(ref i, ref j);
					Main.tile[i, j].CopyFrom(tile);
					WorldGen.tileCounts[(int)(*tile.type)] += (((double)j <= Main.worldSurface) ? 5 : 1);
				}
			}
			if ((flags & 128) == 128)
			{
				nextModTile = true;
			}
		}

		/// <summary>
		/// Increases the provided x and y coordinates to the next location in accordance with order-sensitive position IDs.
		/// Typically used in clustering duplicate data across multiple consecutive locations, such as in ModLoader.TileIO
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns> False if x and y cannot be increased further (end of the world)  </returns>
		// Token: 0x06002C9B RID: 11419 RVA: 0x005287E1 File Offset: 0x005269E1
		private static bool NextLocation(ref int x, ref int y)
		{
			y++;
			if (y >= Main.maxTilesY)
			{
				y = 0;
				x++;
				if (x >= Main.maxTilesX)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x00528808 File Offset: 0x00526A08
		internal static List<TagCompound> SaveTileEntities()
		{
			List<TagCompound> list = new List<TagCompound>();
			TagCompound saveData = new TagCompound();
			foreach (KeyValuePair<int, TileEntity> pair in TileEntity.ByID)
			{
				TileEntity tileEntity = pair.Value;
				ModTileEntity modTileEntity = tileEntity as ModTileEntity;
				tileEntity.SaveData(saveData);
				TagCompound tagCompound = new TagCompound();
				tagCompound["mod"] = (((modTileEntity != null) ? modTileEntity.Mod.Name : null) ?? "Terraria");
				tagCompound["name"] = (((modTileEntity != null) ? modTileEntity.Name : null) ?? tileEntity.GetType().Name);
				tagCompound["X"] = tileEntity.Position.X;
				tagCompound["Y"] = tileEntity.Position.Y;
				TagCompound tag = tagCompound;
				if (saveData.Count != 0)
				{
					tag["data"] = saveData;
					saveData = new TagCompound();
				}
				list.Add(tag);
			}
			return list;
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x00528934 File Offset: 0x00526B34
		internal static void LoadTileEntities(IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				string modName = tag.GetString("mod");
				string name = tag.GetString("name");
				Point16 point = new Point16(tag.GetShort("X"), tag.GetShort("Y"));
				ModTileEntity baseModTileEntity = null;
				bool foundTE = true;
				TileEntity tileEntity;
				if (modName != "Terraria")
				{
					if (!ModContent.TryFind<ModTileEntity>(modName, name, out baseModTileEntity))
					{
						foundTE = false;
						baseModTileEntity = ModContent.GetInstance<UnloadedTileEntity>();
					}
					tileEntity = ModTileEntity.ConstructFromBase(baseModTileEntity);
					tileEntity.type = (byte)baseModTileEntity.Type;
					tileEntity.Position = point;
					if (!foundTE)
					{
						UnloadedTileEntity unloadedTileEntity = tileEntity as UnloadedTileEntity;
						if (unloadedTileEntity != null)
						{
							unloadedTileEntity.SetData(tag);
						}
					}
				}
				else if (!TileEntity.ByPosition.TryGetValue(point, out tileEntity))
				{
					continue;
				}
				if (tag.ContainsKey("data"))
				{
					try
					{
						if (foundTE)
						{
							tileEntity.LoadData(tag.GetCompound("data"));
						}
						UnloadedTileEntity unloadedTE = tileEntity as UnloadedTileEntity;
						ModTileEntity restoredTE;
						if (unloadedTE != null && unloadedTE.TryRestore(out restoredTE))
						{
							tileEntity = restoredTE;
						}
					}
					catch (Exception e)
					{
						ModTileEntity modTileEntity = tileEntity as ModTileEntity;
						throw new CustomModDataException((modTileEntity != null) ? modTileEntity.Mod : null, "Error in reading " + name + " tile entity data for " + modName, e);
					}
				}
				if (baseModTileEntity != null && tileEntity.IsTileValidForEntity((int)tileEntity.Position.X, (int)tileEntity.Position.Y))
				{
					tileEntity.ID = TileEntity.AssignNewID();
					TileEntity.ByID[tileEntity.ID] = tileEntity;
					TileEntity other;
					if (TileEntity.ByPosition.TryGetValue(tileEntity.Position, out other))
					{
						TileEntity.ByID.Remove(other.ID);
					}
					TileEntity.ByPosition[tileEntity.Position] = tileEntity;
				}
			}
		}

		// Token: 0x04001C08 RID: 7176
		internal static TileIO.TileIOImpl Tiles = new TileIO.TileIOImpl();

		// Token: 0x04001C09 RID: 7177
		internal static TileIO.WallIOImpl Walls = new TileIO.WallIOImpl();

		// Token: 0x02000A43 RID: 2627
		internal struct ContainerTables
		{
			// Token: 0x06005848 RID: 22600 RVA: 0x0069F56C File Offset: 0x0069D76C
			internal static TileIO.ContainerTables Create()
			{
				return new TileIO.ContainerTables
				{
					headSlots = new Dictionary<int, int>(),
					bodySlots = new Dictionary<int, int>(),
					legSlots = new Dictionary<int, int>()
				};
			}

			// Token: 0x04006CBB RID: 27835
			internal IDictionary<int, int> headSlots;

			// Token: 0x04006CBC RID: 27836
			internal IDictionary<int, int> bodySlots;

			// Token: 0x04006CBD RID: 27837
			internal IDictionary<int, int> legSlots;
		}

		// Token: 0x02000A44 RID: 2628
		public abstract class IOImpl<TBlock, TEntry> where TBlock : ModBlockType where TEntry : ModBlockEntry
		{
			// Token: 0x06005849 RID: 22601 RVA: 0x0069F5A6 File Offset: 0x0069D7A6
			protected IOImpl(string entriesKey, string dataKey)
			{
				this.entriesKey = entriesKey;
				this.dataKey = dataKey;
			}

			// Token: 0x170008FE RID: 2302
			// (get) Token: 0x0600584A RID: 22602
			protected abstract int LoadedBlockCount { get; }

			// Token: 0x170008FF RID: 2303
			// (get) Token: 0x0600584B RID: 22603
			protected abstract IEnumerable<TBlock> LoadedBlocks { get; }

			// Token: 0x0600584C RID: 22604
			protected abstract TEntry ConvertBlockToEntry(TBlock block);

			// Token: 0x0600584D RID: 22605 RVA: 0x0069F5C8 File Offset: 0x0069D7C8
			private List<TEntry> CreateEntries()
			{
				List<TEntry> entries = Enumerable.Repeat<TEntry>(default(TEntry), this.LoadedBlockCount).ToList<TEntry>();
				foreach (TBlock block in this.LoadedBlocks)
				{
					if (!this.unloadedTypes.Contains(block.Type))
					{
						entries[(int)block.Type] = this.ConvertBlockToEntry(block);
					}
				}
				return entries;
			}

			// Token: 0x0600584E RID: 22606 RVA: 0x0069F65C File Offset: 0x0069D85C
			public void LoadEntries(TagCompound tag, out TEntry[] savedEntryLookup)
			{
				IList<TEntry> savedEntryList = tag.GetList<TEntry>(this.entriesKey);
				List<TEntry> entries = this.CreateEntries();
				if (savedEntryList.Count == 0)
				{
					savedEntryLookup = null;
				}
				else
				{
					savedEntryLookup = new TEntry[(int)(savedEntryList.Max((TEntry e) => e.type) + 1)];
					foreach (TEntry entry in savedEntryList)
					{
						savedEntryLookup[(int)entry.type] = entry;
						TBlock block;
						if (ModContent.TryFind<TBlock>(entry.modName, entry.name, out block))
						{
							entry.type = (entry.loadedType = block.Type);
						}
						else
						{
							entry.type = (ushort)entries.Count;
							TBlock unloadedBlock;
							entry.loadedType = (ModContent.TryFind<TBlock>(entry.unloadedType, out unloadedBlock) ? unloadedBlock : entry.DefaultUnloadedPlaceholder).Type;
							entries.Add(entry);
						}
					}
				}
				this.entries = entries.ToArray();
			}

			// Token: 0x0600584F RID: 22607
			protected abstract void ReadData(Tile tile, TEntry entry, BinaryReader reader);

			// Token: 0x06005850 RID: 22608 RVA: 0x0069F7B0 File Offset: 0x0069D9B0
			public void LoadData(TagCompound tag, TEntry[] savedEntryLookup)
			{
				if (!tag.ContainsKey(this.dataKey))
				{
					return;
				}
				using (BinaryReader reader = new BinaryReader(tag.GetByteArray(this.dataKey).ToMemoryStream(false)))
				{
					PosData<ushort>.OrderedSparseLookupBuilder builder = new PosData<ushort>.OrderedSparseLookupBuilder(1048576, true, false);
					for (int x = 0; x < Main.maxTilesX; x++)
					{
						for (int y = 0; y < Main.maxTilesY; y++)
						{
							ushort saveType = reader.ReadUInt16();
							if (saveType != 0)
							{
								TEntry entry = savedEntryLookup[(int)saveType];
								this.ReadData(Main.tile[x, y], entry, reader);
								if (entry.IsUnloaded)
								{
									builder.Add(x, y, entry.type);
								}
							}
						}
					}
					this.unloadedEntryLookup = builder.Build();
				}
			}

			// Token: 0x06005851 RID: 22609 RVA: 0x0069F888 File Offset: 0x0069DA88
			public void Save(TagCompound tag)
			{
				if (this.entries == null)
				{
					this.entries = this.CreateEntries().ToArray();
				}
				bool[] hasBlocks;
				tag[this.dataKey] = this.SaveData(out hasBlocks);
				tag[this.entriesKey] = this.SelectEntries(hasBlocks, this.entries).ToList<TEntry>();
			}

			// Token: 0x06005852 RID: 22610 RVA: 0x0069F8E0 File Offset: 0x0069DAE0
			private IEnumerable<TEntry> SelectEntries(bool[] select, TEntry[] entries)
			{
				int num;
				for (int i = 0; i < select.Length; i = num + 1)
				{
					if (select[i])
					{
						yield return entries[i];
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x06005853 RID: 22611
			protected abstract ushort GetModBlockType(Tile tile);

			// Token: 0x06005854 RID: 22612
			protected abstract void WriteData(BinaryWriter writer, Tile tile, TEntry entry);

			// Token: 0x06005855 RID: 22613 RVA: 0x0069F8F8 File Offset: 0x0069DAF8
			public byte[] SaveData(out bool[] hasObj)
			{
				byte[] result;
				using (MemoryStream ms = new MemoryStream())
				{
					BinaryWriter writer = new BinaryWriter(ms);
					PosData<ushort>.OrderedSparseLookupReader unloadedReader = new PosData<ushort>.OrderedSparseLookupReader(this.unloadedEntryLookup, true);
					hasObj = new bool[this.entries.Length];
					for (int x = 0; x < Main.maxTilesX; x++)
					{
						for (int y = 0; y < Main.maxTilesY; y++)
						{
							Tile tile = Main.tile[x, y];
							int type = (int)this.GetModBlockType(tile);
							if (type == 0)
							{
								writer.Write(0);
							}
							else
							{
								if (this.entries[type] == null)
								{
									type = (int)unloadedReader.Get(x, y);
								}
								hasObj[type] = true;
								this.WriteData(writer, tile, this.entries[type]);
							}
						}
					}
					result = ms.ToArray();
				}
				return result;
			}

			// Token: 0x06005856 RID: 22614 RVA: 0x0069F9DC File Offset: 0x0069DBDC
			public void Clear()
			{
				this.entries = null;
				this.unloadedEntryLookup = null;
			}

			// Token: 0x04006CBE RID: 27838
			public readonly string entriesKey;

			// Token: 0x04006CBF RID: 27839
			public readonly string dataKey;

			// Token: 0x04006CC0 RID: 27840
			public TEntry[] entries;

			// Token: 0x04006CC1 RID: 27841
			public PosData<ushort>[] unloadedEntryLookup;

			// Token: 0x04006CC2 RID: 27842
			public List<ushort> unloadedTypes = new List<ushort>();
		}

		// Token: 0x02000A45 RID: 2629
		public class TileIOImpl : TileIO.IOImpl<ModTile, TileEntry>
		{
			// Token: 0x06005857 RID: 22615 RVA: 0x0069F9EC File Offset: 0x0069DBEC
			public TileIOImpl() : base("tileMap", "tileData")
			{
			}

			// Token: 0x17000900 RID: 2304
			// (get) Token: 0x06005858 RID: 22616 RVA: 0x0069F9FE File Offset: 0x0069DBFE
			protected override int LoadedBlockCount
			{
				get
				{
					return TileLoader.TileCount;
				}
			}

			// Token: 0x17000901 RID: 2305
			// (get) Token: 0x06005859 RID: 22617 RVA: 0x0069FA05 File Offset: 0x0069DC05
			protected override IEnumerable<ModTile> LoadedBlocks
			{
				get
				{
					return TileLoader.tiles;
				}
			}

			// Token: 0x0600585A RID: 22618 RVA: 0x0069FA0C File Offset: 0x0069DC0C
			protected override TileEntry ConvertBlockToEntry(ModTile tile)
			{
				return new TileEntry(tile);
			}

			// Token: 0x0600585B RID: 22619 RVA: 0x0069FA14 File Offset: 0x0069DC14
			protected unsafe override ushort GetModBlockType(Tile tile)
			{
				if (!tile.active() || *tile.type < TileID.Count)
				{
					return 0;
				}
				return *tile.type;
			}

			// Token: 0x0600585C RID: 22620 RVA: 0x0069FA38 File Offset: 0x0069DC38
			protected unsafe override void ReadData(Tile tile, TileEntry entry, BinaryReader reader)
			{
				*tile.type = entry.loadedType;
				tile.color(reader.ReadByte());
				tile.active(true);
				if (entry.frameImportant)
				{
					*tile.frameX = reader.ReadInt16();
					*tile.frameY = reader.ReadInt16();
				}
			}

			// Token: 0x0600585D RID: 22621 RVA: 0x0069FA8C File Offset: 0x0069DC8C
			protected unsafe override void WriteData(BinaryWriter writer, Tile tile, TileEntry entry)
			{
				writer.Write(entry.type);
				writer.Write(tile.color());
				if (entry.frameImportant)
				{
					writer.Write(*tile.frameX);
					writer.Write(*tile.frameY);
				}
			}
		}

		// Token: 0x02000A46 RID: 2630
		public class WallIOImpl : TileIO.IOImpl<ModWall, WallEntry>
		{
			// Token: 0x0600585E RID: 22622 RVA: 0x0069FACB File Offset: 0x0069DCCB
			public WallIOImpl() : base("wallMap", "wallData")
			{
			}

			// Token: 0x17000902 RID: 2306
			// (get) Token: 0x0600585F RID: 22623 RVA: 0x0069FADD File Offset: 0x0069DCDD
			protected override int LoadedBlockCount
			{
				get
				{
					return WallLoader.WallCount;
				}
			}

			// Token: 0x17000903 RID: 2307
			// (get) Token: 0x06005860 RID: 22624 RVA: 0x0069FAE4 File Offset: 0x0069DCE4
			protected override IEnumerable<ModWall> LoadedBlocks
			{
				get
				{
					return WallLoader.walls;
				}
			}

			// Token: 0x06005861 RID: 22625 RVA: 0x0069FAEB File Offset: 0x0069DCEB
			protected override WallEntry ConvertBlockToEntry(ModWall wall)
			{
				return new WallEntry(wall);
			}

			// Token: 0x06005862 RID: 22626 RVA: 0x0069FAF3 File Offset: 0x0069DCF3
			protected unsafe override ushort GetModBlockType(Tile tile)
			{
				if (*tile.wall < WallID.Count)
				{
					return 0;
				}
				return *tile.wall;
			}

			// Token: 0x06005863 RID: 22627 RVA: 0x0069FB0E File Offset: 0x0069DD0E
			protected unsafe override void ReadData(Tile tile, WallEntry entry, BinaryReader reader)
			{
				*tile.wall = entry.loadedType;
				tile.wallColor(reader.ReadByte());
			}

			// Token: 0x06005864 RID: 22628 RVA: 0x0069FB2B File Offset: 0x0069DD2B
			protected override void WriteData(BinaryWriter writer, Tile tile, WallEntry entry)
			{
				writer.Write(entry.type);
				writer.Write(tile.wallColor());
			}
		}

		// Token: 0x02000A47 RID: 2631
		internal static class TileIOFlags
		{
			// Token: 0x04006CC3 RID: 27843
			internal const byte None = 0;

			// Token: 0x04006CC4 RID: 27844
			internal const byte ModTile = 1;

			// Token: 0x04006CC5 RID: 27845
			internal const byte FrameXInt16 = 2;

			// Token: 0x04006CC6 RID: 27846
			internal const byte FrameYInt16 = 4;

			// Token: 0x04006CC7 RID: 27847
			internal const byte TileColor = 8;

			// Token: 0x04006CC8 RID: 27848
			internal const byte ModWall = 16;

			// Token: 0x04006CC9 RID: 27849
			internal const byte WallColor = 32;

			// Token: 0x04006CCA RID: 27850
			internal const byte NextTilesAreSame = 64;

			// Token: 0x04006CCB RID: 27851
			internal const byte NextModTile = 128;
		}
	}
}
