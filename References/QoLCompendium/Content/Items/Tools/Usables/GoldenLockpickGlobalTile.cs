// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.GoldenLockpickGlobalTile
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Usables;

public class GoldenLockpickGlobalTile : GlobalTile
{
  public virtual void RightClick(int i, int j, int type)
  {
    if (type != 21)
      return;
    Tile tile1 = ((Tilemap) ref Main.tile)[i, j];
    if (((Tile) ref tile1).TileFrameX < (short) 72)
      return;
    Tile tile2 = ((Tilemap) ref Main.tile)[i, j];
    if (((Tile) ref tile2).TileFrameX > (short) 108 || !Main.LocalPlayer.GetModPlayer<QoLCPlayer>().HasGoldenLockpick)
      return;
    int num1 = i;
    Tile tile3 = ((Tilemap) ref Main.tile)[i, j];
    int num2 = (int) ((Tile) ref tile3).TileFrameX % 36 / 18;
    i = num1 - num2;
    int num3 = j;
    Tile tile4 = ((Tilemap) ref Main.tile)[i, j];
    int num4 = (int) ((Tile) ref tile4).TileFrameY % 36 / 18;
    j = num3 - num4;
    Chest.Unlock(i, j);
  }
}
