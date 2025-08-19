// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Explosives.Destruction
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace QoLCompendium.Content.Projectiles.Explosives;

public class Destruction : GlobalTile
{
  internal static void DestroyChest(int x, int y)
  {
    int num1 = 1;
    int chest = Chest.FindChest(x, y);
    Tile tile1;
    if (chest != -1)
    {
      for (int index = 0; index < 40; ++index)
        Main.chest[chest].item[index] = new Item();
      Main.chest[chest] = (Chest) null;
      Tile tile2 = ((Tilemap) ref Main.tile)[x, y];
      if (((Tile) ref tile2).TileType == (ushort) 467)
        num1 = 5;
      tile1 = ((Tilemap) ref Main.tile)[x, y];
      if ((int) ((Tile) ref tile1).TileType >= (int) TileID.Count)
        num1 = 101;
    }
    for (int index1 = x; index1 < x + 2; ++index1)
    {
      for (int index2 = y; index2 < y + 2; ++index2)
      {
        tile1 = ((Tilemap) ref Main.tile)[index1, index2];
        ((Tile) ref tile1).TileType = (ushort) 0;
        tile1 = ((Tilemap) ref Main.tile)[index1, index2];
        ((Tile) ref tile1).TileFrameX = (short) 0;
        tile1 = ((Tilemap) ref Main.tile)[index1, index2];
        ((Tile) ref tile1).TileFrameY = (short) 0;
      }
    }
    if (Main.netMode == 0)
      return;
    if (chest != -1)
    {
      int num2 = num1;
      double num3 = (double) x;
      double num4 = (double) y;
      int num5 = chest;
      tile1 = ((Tilemap) ref Main.tile)[x, y];
      int num6 = (int) ((Tile) ref tile1).TileType;
      NetMessage.SendData(34, -1, -1, (NetworkText) null, num2, (float) num3, (float) num4, 0.0f, num5, num6, 0);
    }
    NetMessage.SendTileSquare(-1, x, y, 3, (TileChangeType) 0);
  }

  internal static Point16 FindChestTopLeft(int x, int y, bool destroy)
  {
    Tile tile = ((Tilemap) ref Main.tile)[x, y];
    if (!TileID.Sets.BasicChest[(int) ((Tile) ref tile).TileType])
      return Point16.NegativeOne;
    TileObjectData tileData = TileObjectData.GetTileData((int) ((Tile) ref tile).TileType, 0, 0);
    x -= (int) ((Tile) ref tile).TileFrameX / 18 % tileData.Width;
    y -= (int) ((Tile) ref tile).TileFrameY / 18 % tileData.Height;
    if (destroy)
      Destruction.DestroyChest(x, y);
    return new Point16(x, y);
  }

  internal static void ClearTileAndLiquid(int x, int y, bool sendData = true)
  {
    Destruction.FindChestTopLeft(x, y, true);
    Tile tile = ((Tilemap) ref Main.tile)[x, y];
    bool flag = ((Tile) ref tile).LiquidAmount > (byte) 0;
    WorldGen.KillTile(x, y, false, false, true);
    ((Tile) ref tile).Clear((TileDataType) 1);
    ((Tile) ref tile).Clear((TileDataType) 16 /*0x10*/);
    if (Main.netMode != 2)
      return;
    if (flag)
      NetMessage.sendWater(x, y);
    if (!sendData)
      return;
    NetMessage.SendTileSquare(-1, x, y, 1, (TileChangeType) 0);
  }

  internal static void ClearEverything(int x, int y, bool sendData = true)
  {
    Destruction.FindChestTopLeft(x, y, true);
    Tile tile = ((Tilemap) ref Main.tile)[x, y];
    bool flag = ((Tile) ref tile).LiquidAmount > (byte) 0;
    WorldGen.KillTile(x, y, false, false, true);
    ((Tile) ref tile).ClearEverything();
    if (Main.netMode != 2)
      return;
    if (flag)
      NetMessage.sendWater(x, y);
    if (!sendData)
      return;
    NetMessage.SendTileSquare(-1, x, y, 1, (TileChangeType) 0);
  }
}
