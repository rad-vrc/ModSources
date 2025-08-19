using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;

namespace Terraria.ModLoader
{
	// Token: 0x02000196 RID: 406
	internal static class MapLoader
	{
		// Token: 0x06001F47 RID: 8007 RVA: 0x004E0E48 File Offset: 0x004DF048
		internal static int modTileOptions(ushort type)
		{
			return MapLoader.tileEntries[type].Count;
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x004E0E5A File Offset: 0x004DF05A
		internal static int modWallOptions(ushort type)
		{
			return MapLoader.wallEntries[type].Count;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x004E0E6C File Offset: 0x004DF06C
		internal static void FinishSetup()
		{
			if (Main.dedServ)
			{
				return;
			}
			Array.Resize<ushort>(ref MapHelper.tileLookup, TileLoader.TileCount);
			Array.Resize<ushort>(ref MapHelper.wallLookup, WallLoader.WallCount);
			IList<Color> colors = new List<Color>();
			IList<LocalizedText> names = new List<LocalizedText>();
			foreach (ushort type in MapLoader.tileEntries.Keys)
			{
				MapHelper.tileLookup[(int)type] = (ushort)((int)MapHelper.modPosition + colors.Count);
				foreach (MapEntry entry in MapLoader.tileEntries[type])
				{
					ushort mapType = (ushort)((int)MapHelper.modPosition + colors.Count);
					MapLoader.entryToTile[mapType] = type;
					MapLoader.nameFuncs[mapType] = entry.getName;
					colors.Add(entry.color);
					if (entry.name == null)
					{
						throw new Exception("How did this happen?");
					}
					names.Add(entry.name);
				}
			}
			foreach (ushort type2 in MapLoader.wallEntries.Keys)
			{
				MapHelper.wallLookup[(int)type2] = (ushort)((int)MapHelper.modPosition + colors.Count);
				foreach (MapEntry entry2 in MapLoader.wallEntries[type2])
				{
					ushort mapType2 = (ushort)((int)MapHelper.modPosition + colors.Count);
					MapLoader.entryToWall[mapType2] = type2;
					MapLoader.nameFuncs[mapType2] = entry2.getName;
					colors.Add(entry2.color);
					if (entry2.name == null)
					{
						throw new Exception("How did this happen?");
					}
					names.Add(entry2.name);
				}
			}
			Array.Resize<Color>(ref MapHelper.colorLookup, (int)MapHelper.modPosition + colors.Count);
			Lang._mapLegendCache.Resize((int)MapHelper.modPosition + names.Count);
			for (int i = 0; i < colors.Count; i++)
			{
				MapHelper.colorLookup[(int)MapHelper.modPosition + i] = colors[i];
				Lang._mapLegendCache[(int)MapHelper.modPosition + i] = names[i];
			}
			MapLoader.initialized = true;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x004E1114 File Offset: 0x004DF314
		internal static void UnloadModMap()
		{
			MapLoader.tileEntries.Clear();
			MapLoader.wallEntries.Clear();
			if (Main.dedServ)
			{
				return;
			}
			MapLoader.nameFuncs.Clear();
			MapLoader.entryToTile.Clear();
			MapLoader.entryToWall.Clear();
			Array.Resize<ushort>(ref MapHelper.tileLookup, (int)TileID.Count);
			Array.Resize<ushort>(ref MapHelper.wallLookup, (int)WallID.Count);
			Array.Resize<Color>(ref MapHelper.colorLookup, (int)MapHelper.modPosition);
			Lang._mapLegendCache.Resize((int)MapHelper.modPosition);
			MapLoader.initialized = false;
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x004E11A0 File Offset: 0x004DF3A0
		internal static void ModMapOption(ref ushort mapType, int i, int j)
		{
			if (!MapLoader.entryToTile.ContainsKey(mapType))
			{
				if (MapLoader.entryToWall.ContainsKey(mapType))
				{
					ModWall wall = WallLoader.GetWall((int)MapLoader.entryToWall[mapType]);
					ushort option = wall.GetMapOption(i, j);
					if (option < 0 || (int)option >= MapLoader.modWallOptions(wall.Type))
					{
						throw new ArgumentOutOfRangeException("Bad map option for wall " + wall.Name + " from mod " + wall.Mod.Name);
					}
					mapType += option;
				}
				return;
			}
			ModTile tile = TileLoader.GetTile((int)MapLoader.entryToTile[mapType]);
			ushort option2 = tile.GetMapOption(i, j);
			if (option2 < 0 || (int)option2 >= MapLoader.modTileOptions(tile.Type))
			{
				throw new ArgumentOutOfRangeException("Bad map option for tile " + tile.Name + " from mod " + tile.Mod.Name);
			}
			mapType += option2;
		}

		// Token: 0x04001675 RID: 5749
		internal static bool initialized = false;

		// Token: 0x04001676 RID: 5750
		internal static readonly IDictionary<ushort, IList<MapEntry>> tileEntries = new Dictionary<ushort, IList<MapEntry>>();

		// Token: 0x04001677 RID: 5751
		internal static readonly IDictionary<ushort, IList<MapEntry>> wallEntries = new Dictionary<ushort, IList<MapEntry>>();

		// Token: 0x04001678 RID: 5752
		internal static readonly IDictionary<ushort, Func<string, int, int, string>> nameFuncs = new Dictionary<ushort, Func<string, int, int, string>>();

		// Token: 0x04001679 RID: 5753
		internal static readonly IDictionary<ushort, ushort> entryToTile = new Dictionary<ushort, ushort>();

		// Token: 0x0400167A RID: 5754
		internal static readonly IDictionary<ushort, ushort> entryToWall = new Dictionary<ushort, ushort>();
	}
}
