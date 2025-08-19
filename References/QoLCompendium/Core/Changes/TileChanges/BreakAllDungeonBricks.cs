// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.TileChanges.BreakAllDungeonBricks
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.TileChanges;

public class BreakAllDungeonBricks : GlobalTile
{
  public virtual void KillTile(
    int i,
    int j,
    int type,
    ref bool fail,
    ref bool effectOnly,
    ref bool noItem)
  {
    EntitySource_TileBreak entitySourceTileBreak = new EntitySource_TileBreak(i, j, (string) null);
    Vector2 vector2;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2).\u002Ector((float) (Common.ToPixels((float) i) + 8), (float) (Common.ToPixels((float) j) + 8));
    if (!QoLCompendium.QoLCompendium.mainConfig.BreakAllDungeonBricks || !Main.tileCracked[type] || Main.netMode == 1)
      return;
    for (int index = 0; index < 8; ++index)
    {
      int num1 = i;
      int num2 = j;
      switch (index)
      {
        case 0:
          --num1;
          break;
        case 1:
          ++num1;
          break;
        case 2:
          --num2;
          break;
        case 3:
          ++num2;
          break;
        case 4:
          --num1;
          --num2;
          break;
        case 5:
          ++num1;
          --num2;
          break;
        case 6:
          --num1;
          ++num2;
          break;
        case 7:
          ++num1;
          ++num2;
          break;
      }
      Tile tile1 = ((Tilemap) ref Main.tile)[num1, num2];
      if (((Tile) ref tile1).HasTile && Main.tileCracked[(int) ((Tile) ref tile1).TileType])
      {
        Tile tile2 = ((Tilemap) ref Main.tile)[i, j];
        ((TileWallWireStateData) ref ((Tile) ref tile2).Get<TileWallWireStateData>()).HasTile = false;
        WorldGen.KillTile(num1, num2, false, false, true);
        if (Main.netMode == 2)
          NetMessage.TrySendData(17, -1, -1, (NetworkText) null, 20, (float) num1, (float) num2, 0.0f, 0, 0, 0);
      }
    }
    int num = type - 481 + 736;
    if (Main.netMode == 0)
    {
      Projectile.NewProjectile((IEntitySource) entitySourceTileBreak, vector2, Vector2.Zero, num, 20, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }
    else
    {
      if (Main.netMode != 2)
        return;
      Projectile.NewProjectileDirect((IEntitySource) entitySourceTileBreak, vector2, Vector2.Zero, num, 20, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f).netUpdate = true;
    }
  }
}
