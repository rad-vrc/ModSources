using System;
using QoLCompendium.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Explosives
{
	// Token: 0x0200002E RID: 46
	public class CheckDestruction : GlobalProjectile
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000064D8 File Offset: 0x000046D8
		public unsafe static bool OkayToDestroyTile(Tile tile, bool ignoreModdedTiles = false)
		{
			bool flag = !NPC.downedBoss3 && (*tile.TileType == 41 || *tile.TileType == 43 || *tile.TileType == 44 || *tile.WallType == 94 || *tile.WallType == 95 || *tile.WallType == 7 || *tile.WallType == 98 || *tile.WallType == 99 || *tile.WallType == 8 || *tile.WallType == 96 || *tile.WallType == 97 || *tile.WallType == 9);
			bool noHMOre = (*tile.TileType == 107 || *tile.TileType == 221 || *tile.TileType == 108 || *tile.TileType == 222 || *tile.TileType == 111 || *tile.TileType == 223) && !NPC.downedMechBossAny;
			bool noChloro = *tile.TileType == 211 && (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3);
			bool noLihzahrd = (*tile.TileType == 226 || *tile.WallType == 87) && !NPC.downedGolemBoss;
			return !flag && !noHMOre && !noChloro && !noLihzahrd && (!ignoreModdedTiles || !CheckDestruction.TileBelongsToMod(tile));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00006650 File Offset: 0x00004850
		public static bool OkayToDestroyTileAt(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return !(tile == null) && CheckDestruction.OkayToDestroyTile(tile, false);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000667C File Offset: 0x0000487C
		public unsafe static bool TileIsLiterallyAir(Tile tile)
		{
			return *tile.TileType == 0 && *tile.WallType == 0 && *tile.LiquidAmount == 0 && *tile.TileFrameX == 0 && *tile.TileFrameY == 0;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000066B4 File Offset: 0x000048B4
		public unsafe static bool TileBelongsToMod(Tile tile)
		{
			return tile.HasTile && *tile.TileType > TileID.Count && !Common.IgnoredTilesForExplosives.Contains((int)(*tile.TileType)) && (Common.IgnoredModsForExplosives == null || !Common.IgnoredModsForExplosives.Contains(TileLoader.GetTile((int)(*tile.TileType)).Mod));
		}
	}
}
