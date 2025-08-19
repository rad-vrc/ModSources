// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.TileChanges.FastTreeGrowth
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.TileChanges;

public class FastTreeGrowth : GlobalTile
{
  public virtual void RandomUpdate(int i, int j, int type)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.FastTreeGrowth)
      return;
    Tile tile1 = ((Tilemap) ref Main.tile)[i, j];
    if (!((Tile) ref tile1).HasTile)
      return;
    for (int index = 0; index < 4; ++index)
    {
      switch (type)
      {
        case 20:
          Tile tile2 = ((Tilemap) ref Main.tile)[i, j];
          int num;
          if (((Tile) ref tile2).TileFrameX >= (short) 324)
          {
            Tile tile3 = ((Tilemap) ref Main.tile)[i, j];
            if (((Tile) ref tile3).TileFrameX < (short) 540)
            {
              num = !WorldGen.GrowPalmTree(i, j) ? 0 : (WorldGen.PlayerLOS(i, j) ? 1 : 0);
              goto label_11;
            }
          }
          num = WorldGen.GrowTree(i, j) ? 1 : 0;
label_11:
          if (num == 0)
            return;
          WorldGen.TreeGrowFXCheck(i, j);
          return;
        case 590:
          if (!Utils.NextBool(WorldGen.genRand, 5))
            return;
          Tile tile4 = ((Tilemap) ref Main.tile)[i, j];
          if (!WorldGen.TryGrowingTreeByType(583 + (int) ((Tile) ref tile4).TileFrameX / 54, i, j) || !WorldGen.PlayerLOS(i, j))
            return;
          WorldGen.TreeGrowFXCheck(i, j);
          return;
        case 595:
        case 615:
          if (!Utils.NextBool(WorldGen.genRand, 5) || !WorldGen.TryGrowingTreeByType(type + 1, i, j) || !WorldGen.PlayerLOS(i, j))
            return;
          WorldGen.TreeGrowFXCheck(i, j);
          return;
        default:
          if (TileID.Sets.TreeSapling[type])
          {
            ModTile tile5 = TileLoader.GetTile(type);
            if (tile5 != null)
            {
              ((ModBlockType) tile5).RandomUpdate(i, j);
              continue;
            }
            continue;
          }
          continue;
      }
    }
  }
}
