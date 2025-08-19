using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002CA RID: 714
	internal class PurgeUnloadedCommand : ModCommand
	{
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x0052F991 File Offset: 0x0052DB91
		public override string Command
		{
			get
			{
				return "purgeunloaded";
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x0052F998 File Offset: 0x0052DB98
		public override CommandType Type
		{
			get
			{
				return CommandType.Chat;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x0052F99B File Offset: 0x0052DB9B
		public override string Description
		{
			get
			{
				return Language.GetTextValue("tModLoader.CommandPurgeUnloadedDescription");
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x0052F9A7 File Offset: 0x0052DBA7
		public override string Usage
		{
			get
			{
				return Language.GetTextValue("tModLoader.CommandPurgeUnloadedUsage");
			}
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x0052F9B4 File Offset: 0x0052DBB4
		public unsafe override void Action(CommandCaller caller, string input, string[] args)
		{
			if (Main.netMode != 0)
			{
				caller.Reply("This command can only be called in Single Player mode.", default(Color));
				return;
			}
			bool purgeTiles = false;
			bool purgeWalls = false;
			bool actuallyPurge = false;
			string modName = null;
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == "-h")
				{
					caller.Reply(this.Usage, default(Color));
					return;
				}
				if (args[i] == "-t")
				{
					purgeTiles = true;
				}
				else if (args[i] == "-w")
				{
					purgeWalls = true;
				}
				else if (args[i] == "-p")
				{
					actuallyPurge = true;
				}
				else
				{
					modName = args[i];
				}
			}
			if (!purgeTiles && !purgeWalls)
			{
				caller.Reply("At least one of '-w' or '-t' must be set", default(Color));
				caller.Reply(this.Usage, default(Color));
				return;
			}
			Dictionary<string, int> purgedTiles = new Dictionary<string, int>();
			Dictionary<string, int> purgedWalls = new Dictionary<string, int>();
			bool[] unloadedTileTypes = TileID.Sets.Factory.CreateBoolSet((from x in TileIO.Tiles.unloadedTypes
			select (int)x).ToArray<int>());
			int unloadedWallType = ModContent.WallType<UnloadedWall>();
			for (int j = 0; j < Main.maxTilesX; j++)
			{
				for (int k = 0; k < Main.maxTilesY; k++)
				{
					Tile t = Main.tile[j, k];
					if (purgeTiles && t.HasTile && unloadedTileTypes[(int)(*t.TileType)])
					{
						ushort type = TileIO.Tiles.unloadedEntryLookup.Lookup(j, k);
						TileEntry info = TileIO.Tiles.entries[(int)type];
						if (modName == null || info.modName.Equals(modName, StringComparison.OrdinalIgnoreCase))
						{
							string key = info.modName + "/" + info.name;
							int currentCount;
							purgedTiles.TryGetValue(key, out currentCount);
							purgedTiles[key] = currentCount + 1;
							if (actuallyPurge)
							{
								*t.TileType = info.vanillaReplacementType;
							}
						}
					}
					if (purgeWalls && (int)(*t.WallType) == unloadedWallType)
					{
						ushort type2 = TileIO.Walls.unloadedEntryLookup.Lookup(j, k);
						WallEntry info2 = TileIO.Walls.entries[(int)type2];
						if (modName == null || info2.modName.Equals(modName, StringComparison.OrdinalIgnoreCase))
						{
							string key2 = info2.modName + "/" + info2.name;
							int currentCount2;
							purgedWalls.TryGetValue(key2, out currentCount2);
							purgedWalls[key2] = currentCount2 + 1;
							if (actuallyPurge)
							{
								*t.WallType = info2.vanillaReplacementType;
							}
						}
					}
				}
			}
			if (actuallyPurge && (purgedTiles.Count != 0 || purgedWalls.Count != 0))
			{
				Main.sectionManager.SetAllSectionsLoaded();
			}
			if (modName != null)
			{
				caller.Reply("Only removing unloaded content belonging to the mod \"" + modName + "\".", default(Color));
			}
			else
			{
				caller.Reply("Removing unloaded content from any mod.", default(Color));
			}
			if (purgedTiles.Count != 0)
			{
				caller.Reply("Unloaded tiles: " + string.Join(", ", purgedTiles.Select(delegate(KeyValuePair<string, int> x)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler.AppendFormatted(x.Key);
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted<int>(x.Value);
					defaultInterpolatedStringHandler.AppendLiteral(")");
					return defaultInterpolatedStringHandler.ToStringAndClear();
				})), default(Color));
			}
			if (purgedWalls.Count != 0)
			{
				caller.Reply("Unloaded walls: " + string.Join(", ", purgedWalls.Select(delegate(KeyValuePair<string, int> x)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler.AppendFormatted(x.Key);
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted<int>(x.Value);
					defaultInterpolatedStringHandler.AppendLiteral(")");
					return defaultInterpolatedStringHandler.ToStringAndClear();
				})), default(Color));
			}
			if (!actuallyPurge)
			{
				caller.Reply("The '-p' flag was not set, this is a dry run, no tiles or walls were actually removed.", default(Color));
			}
			if (purgedTiles.Count == 0 && purgedWalls.Count == 0)
			{
				caller.Reply("No unloaded content found to remove.", default(Color));
				return;
			}
			if (actuallyPurge)
			{
				caller.Reply("If this was unintentional please restore to a backup or force close the game right not to prevent the world changes from being saved.", default(Color));
			}
		}
	}
}
