// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Explosives.CheckDestruction
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.Explosives;

public class CheckDestruction : GlobalProjectile
{
  public static bool OkayToDestroyTile(Tile tile, bool ignoreModdedTiles = false)
  {
    int num1 = NPC.downedBoss3 ? 0 : (((Tile) ref tile).TileType == (ushort) 41 || ((Tile) ref tile).TileType == (ushort) 43 || ((Tile) ref tile).TileType == (ushort) 44 || ((Tile) ref tile).WallType == (ushort) 94 || ((Tile) ref tile).WallType == (ushort) 95 || ((Tile) ref tile).WallType == (ushort) 7 || ((Tile) ref tile).WallType == (ushort) 98 || ((Tile) ref tile).WallType == (ushort) 99 || ((Tile) ref tile).WallType == (ushort) 8 || ((Tile) ref tile).WallType == (ushort) 96 /*0x60*/ || ((Tile) ref tile).WallType == (ushort) 97 ? 1 : (((Tile) ref tile).WallType == (ushort) 9 ? 1 : 0));
    bool flag1 = (((Tile) ref tile).TileType == (ushort) 107 || ((Tile) ref tile).TileType == (ushort) 221 || ((Tile) ref tile).TileType == (ushort) 108 || ((Tile) ref tile).TileType == (ushort) 222 || ((Tile) ref tile).TileType == (ushort) 111 || ((Tile) ref tile).TileType == (ushort) 223) && !NPC.downedMechBossAny;
    bool flag2 = ((Tile) ref tile).TileType == (ushort) 211 && (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3);
    bool flag3 = (((Tile) ref tile).TileType == (ushort) 226 || ((Tile) ref tile).WallType == (ushort) 87) && !NPC.downedGolemBoss;
    int num2 = flag1 ? 1 : 0;
    return (num1 | num2 | (flag2 ? 1 : 0) | (flag3 ? 1 : 0)) == 0 && (!ignoreModdedTiles || !CheckDestruction.TileBelongsToMod(tile));
  }

  public static bool OkayToDestroyTileAt(int x, int y)
  {
    Tile tile = ((Tilemap) ref Main.tile)[x, y];
    return !Tile.op_Equality(tile, (ArgumentException) null) && CheckDestruction.OkayToDestroyTile(tile);
  }

  public static bool TileIsLiterallyAir(Tile tile)
  {
    return ((Tile) ref tile).TileType == (ushort) 0 && ((Tile) ref tile).WallType == (ushort) 0 && ((Tile) ref tile).LiquidAmount == (byte) 0 && ((Tile) ref tile).TileFrameX == (short) 0 && ((Tile) ref tile).TileFrameY == (short) 0;
  }

  public static bool TileBelongsToMod(Tile tile)
  {
    return ((Tile) ref tile).HasTile && (int) ((Tile) ref tile).TileType > (int) TileID.Count && !Common.IgnoredTilesForExplosives.Contains((int) ((Tile) ref tile).TileType) && (Common.IgnoredModsForExplosives == null || !Common.IgnoredModsForExplosives.Contains(((ModType) TileLoader.GetTile((int) ((Tile) ref tile).TileType)).Mod));
  }
}
