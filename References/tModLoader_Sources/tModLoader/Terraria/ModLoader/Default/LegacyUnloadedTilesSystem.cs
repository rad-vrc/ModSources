using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Default
{
	/// <summary>
	/// Handles conversion of legacy 1.3 world unloaded tile TML TagCompound data to the newer 1.4+ systems.
	/// </summary>
	// Token: 0x020002C4 RID: 708
	[LegacyName(new string[]
	{
		"MysteryTilesWorld",
		"UnloadedTilesSystem"
	})]
	internal class LegacyUnloadedTilesSystem : ModSystem
	{
		// Token: 0x06002D7D RID: 11645 RVA: 0x0052E444 File Offset: 0x0052C644
		public override void ClearWorld()
		{
			LegacyUnloadedTilesSystem.infos.Clear();
			LegacyUnloadedTilesSystem.converted.Clear();
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x0052E45A File Offset: 0x0052C65A
		public override void SaveWorldData(TagCompound tag)
		{
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x0052E45C File Offset: 0x0052C65C
		public override void LoadWorldData(TagCompound tag)
		{
			foreach (TagCompound infoTag in tag.GetList<TagCompound>("list"))
			{
				if (!infoTag.ContainsKey("mod"))
				{
					LegacyUnloadedTilesSystem.infos.Add(LegacyUnloadedTilesSystem.TileInfo.Invalid);
				}
				else
				{
					string modName = infoTag.GetString("mod");
					string name = infoTag.GetString("name");
					LegacyUnloadedTilesSystem.TileInfo info = infoTag.ContainsKey("frameX") ? new LegacyUnloadedTilesSystem.TileInfo(modName, name, infoTag.GetShort("frameX"), infoTag.GetShort("frameY")) : new LegacyUnloadedTilesSystem.TileInfo(modName, name);
					LegacyUnloadedTilesSystem.infos.Add(info);
				}
			}
			if (LegacyUnloadedTilesSystem.infos.Count > 0)
			{
				this.ConvertTiles();
			}
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x0052E534 File Offset: 0x0052C734
		internal unsafe void ConvertTiles()
		{
			List<TileEntry> legacyEntries = TileIO.Tiles.entries.ToList<TileEntry>();
			PosData<ushort>.OrderedSparseLookupBuilder builder = new PosData<ushort>.OrderedSparseLookupBuilder(1048576, true, false);
			PosData<ushort>.OrderedSparseLookupReader unloadedReader = new PosData<ushort>.OrderedSparseLookupReader(TileIO.Tiles.unloadedEntryLookup, true);
			for (int x = 0; x < Main.maxTilesX; x++)
			{
				for (int y = 0; y < Main.maxTilesY; y++)
				{
					Tile tile = Main.tile[x, y];
					if (tile.active() && *tile.type >= TileID.Count)
					{
						ushort type = *tile.type;
						if (TileIO.Tiles.entries[(int)type] == null)
						{
							type = unloadedReader.Get(x, y);
							if (legacyEntries[(int)type].modName.Equals("ModLoader"))
							{
								this.ConvertTile(tile, legacyEntries, out type);
							}
							builder.Add(x, y, type);
						}
					}
				}
			}
			TileIO.Tiles.entries = legacyEntries.ToArray();
			TileIO.Tiles.unloadedEntryLookup = builder.Build();
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x0052E63C File Offset: 0x0052C83C
		internal unsafe void ConvertTile(Tile tile, List<TileEntry> entries, out ushort type)
		{
			LegacyUnloadedTilesSystem.TileFrame frame = new LegacyUnloadedTilesSystem.TileFrame(*tile.frameX, *tile.frameY);
			int frameID = frame.FrameID;
			if (LegacyUnloadedTilesSystem.converted.TryGetValue(frameID, out type))
			{
				return;
			}
			LegacyUnloadedTilesSystem.TileInfo info = LegacyUnloadedTilesSystem.infos[frameID];
			TileEntry entry = new TileEntry(TileLoader.GetTile((int)(*tile.type)))
			{
				name = info.name,
				modName = info.modName,
				frameImportant = (info.frameX > -1),
				type = (type = (ushort)entries.Count)
			};
			entries.Add(entry);
			LegacyUnloadedTilesSystem.converted.Add(frameID, type);
		}

		// Token: 0x04001C51 RID: 7249
		private static readonly List<LegacyUnloadedTilesSystem.TileInfo> infos = new List<LegacyUnloadedTilesSystem.TileInfo>();

		// Token: 0x04001C52 RID: 7250
		private static readonly Dictionary<int, ushort> converted = new Dictionary<int, ushort>();

		// Token: 0x02000A75 RID: 2677
		private struct TileFrame
		{
			// Token: 0x1700090D RID: 2317
			// (get) Token: 0x060058FF RID: 22783 RVA: 0x006A0836 File Offset: 0x0069EA36
			public short FrameX
			{
				get
				{
					return this.frameX;
				}
			}

			// Token: 0x1700090E RID: 2318
			// (get) Token: 0x06005900 RID: 22784 RVA: 0x006A083E File Offset: 0x0069EA3E
			public short FrameY
			{
				get
				{
					return this.frameY;
				}
			}

			// Token: 0x1700090F RID: 2319
			// (get) Token: 0x06005901 RID: 22785 RVA: 0x006A0846 File Offset: 0x0069EA46
			// (set) Token: 0x06005902 RID: 22786 RVA: 0x006A085B File Offset: 0x0069EA5B
			public int FrameID
			{
				get
				{
					return (int)this.frameY * 32768 + (int)this.frameX;
				}
				set
				{
					this.frameX = (short)(value % 32768);
					this.frameY = (short)(value / 32768);
				}
			}

			// Token: 0x06005903 RID: 22787 RVA: 0x006A0879 File Offset: 0x0069EA79
			public TileFrame(int value)
			{
				this.frameX = 0;
				this.frameY = 0;
				this.FrameID = value;
			}

			// Token: 0x06005904 RID: 22788 RVA: 0x006A0890 File Offset: 0x0069EA90
			public TileFrame(short frameX, short frameY)
			{
				this.frameX = frameX;
				this.frameY = frameY;
			}

			// Token: 0x04006D1F RID: 27935
			private short frameX;

			// Token: 0x04006D20 RID: 27936
			private short frameY;
		}

		// Token: 0x02000A76 RID: 2678
		private struct TileInfo
		{
			// Token: 0x06005905 RID: 22789 RVA: 0x006A08A0 File Offset: 0x0069EAA0
			public TileInfo(string modName, string name)
			{
				this.modName = modName;
				this.name = name;
				this.frameImportant = false;
				this.frameX = -1;
				this.frameY = -1;
			}

			// Token: 0x06005906 RID: 22790 RVA: 0x006A08C5 File Offset: 0x0069EAC5
			public TileInfo(string modName, string name, short frameX, short frameY)
			{
				this.modName = modName;
				this.name = name;
				this.frameX = frameX;
				this.frameY = frameY;
				this.frameImportant = true;
			}

			// Token: 0x06005907 RID: 22791 RVA: 0x006A08EC File Offset: 0x0069EAEC
			public override bool Equals(object obj)
			{
				if (obj is LegacyUnloadedTilesSystem.TileInfo)
				{
					LegacyUnloadedTilesSystem.TileInfo other = (LegacyUnloadedTilesSystem.TileInfo)obj;
					return !(this.modName != other.modName) && !(this.name != other.name) && this.frameImportant == other.frameImportant && (!this.frameImportant || (this.frameX == other.frameX && this.frameY == other.frameY));
				}
				return false;
			}

			// Token: 0x06005908 RID: 22792 RVA: 0x006A096C File Offset: 0x0069EB6C
			public override int GetHashCode()
			{
				int hash = this.name.GetHashCode() + this.modName.GetHashCode();
				if (this.frameImportant)
				{
					hash += (int)(this.frameX + this.frameY);
				}
				return hash;
			}

			// Token: 0x06005909 RID: 22793 RVA: 0x006A09AC File Offset: 0x0069EBAC
			public TagCompound Save()
			{
				TagCompound tagCompound = new TagCompound();
				tagCompound["mod"] = this.modName;
				tagCompound["name"] = this.name;
				TagCompound tag = tagCompound;
				if (this.frameImportant)
				{
					tag.Set("frameX", this.frameX, false);
					tag.Set("frameY", this.frameY, false);
				}
				return tag;
			}

			// Token: 0x04006D21 RID: 27937
			public static readonly LegacyUnloadedTilesSystem.TileInfo Invalid = new LegacyUnloadedTilesSystem.TileInfo("UnknownMod", "UnknownTile");

			// Token: 0x04006D22 RID: 27938
			public readonly string modName;

			// Token: 0x04006D23 RID: 27939
			public readonly string name;

			// Token: 0x04006D24 RID: 27940
			public readonly bool frameImportant;

			// Token: 0x04006D25 RID: 27941
			public readonly short frameX;

			// Token: 0x04006D26 RID: 27942
			public readonly short frameY;
		}
	}
}
