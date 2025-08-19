// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.TileChanges.RegrowGemTrees
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace QoLCompendium.Core.Changes.TileChanges;

public class RegrowGemTrees : ModSystem
{
  public virtual void Load()
  {
    // ISSUE: method pointer
    On_Player.ItemCheck_UseMiningTools_ActuallyUseMiningTool += new On_Player.hook_ItemCheck_UseMiningTools_ActuallyUseMiningTool((object) this, __methodptr(OnPlayerMiningToolUse));
  }

  private void OnPlayerMiningToolUse(
    On_Player.orig_ItemCheck_UseMiningTools_ActuallyUseMiningTool orig,
    Player self,
    Item sItem,
    out bool canHitWalls,
    int x,
    int y)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.RegrowthAutoReplant)
    {
      orig.Invoke(self, sItem, ref canHitWalls, x, y);
    }
    else
    {
      Tile tile = ((Tilemap) ref Main.tile)[x, y];
      int tileType = (int) ((Tile) ref tile).TileType;
      int num1;
      int num2;
      WorldGen.GetTreeBottom(x, y, ref num1, ref num2);
      orig.Invoke(self, sItem, ref canHitWalls, x, y);
      if (x != num1 || y != num2 - 1 || ((Tile) ref tile).TileType != (ushort) 0)
        return;
      Player playerForTile = RegrowGemTrees.GetPlayerForTile(x, y);
      if (!RegrowGemTrees.ShouldReplantGemcorn(tileType, playerForTile))
        return;
      RegrowGemTrees.GetSeedItemFromGemTile(tileType);
      int treeStyleFromTile = RegrowGemTrees.GetGemTreeStyleFromTile(tileType);
      RegrowGemTrees.TryReplantingGemTree(self, x, y, treeStyleFromTile);
    }
  }

  private static void TryReplantingGemTree(Player player, int x, int y, int gemStyle = 0)
  {
    int num1 = 590;
    int num2 = gemStyle;
    PlantLoader.CheckAndInjectModSapling(x, y, ref num1, ref num2);
    TileObject tileObject;
    if (!TileObject.CanPlace(Player.tileTargetX, Player.tileTargetY, num1, num2, ((Entity) player).direction, ref tileObject, false, new int?(), false))
      return;
    int num3 = TileObject.Place(tileObject) ? 1 : 0;
    WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
    if (num3 == 0)
      return;
    TileObjectData.CallPostPlacementPlayerHook(Player.tileTargetX, Player.tileTargetY, num1, num2, ((Entity) player).direction, tileObject.alternate, tileObject);
    if (Main.netMode != 1)
      return;
    NetMessage.SendObjectPlacement(-1, Player.tileTargetX, Player.tileTargetY, tileObject.type, tileObject.style, tileObject.alternate, tileObject.random, ((Entity) player).direction);
  }

  public static bool ShouldReplantGemcorn(int tileType, Player player)
  {
    return player != null && QoLCompendium.QoLCompendium.mainConfig.RegrowthAutoReplant && player.inventory[player.selectedItem].type == 5295 && TileID.Sets.CountsAsGemTree[tileType];
  }

  private static Player GetPlayerForTile(int x, int y)
  {
    return Main.player[(int) Player.FindClosest(Vector2.op_Multiply(new Vector2((float) x, (float) y), 16f), 16 /*0x10*/, 16 /*0x10*/)];
  }

  private static int GetGemTreeStyleFromTile(int tileType)
  {
    switch (tileType)
    {
      case 583:
        return 0;
      case 584:
        return 1;
      case 585:
        return 2;
      case 586:
        return 3;
      case 587:
        return 4;
      case 588:
        return 5;
      case 589:
        return 6;
      default:
        return -1;
    }
  }

  public static int GetSeedItemFromGemTile(int tileType)
  {
    switch (tileType)
    {
      case 583:
        return 4851;
      case 584:
        return 4852;
      case 585:
        return 4853;
      case 586:
        return 4854;
      case 587:
        return 4855;
      case 588:
        return 4856;
      case 589:
        return 4857;
      default:
        return -1;
    }
  }

  public enum GemTreeStyle
  {
    Topaz,
    Amethyst,
    Sapphire,
    Emerald,
    Ruby,
    Diamond,
    Amber,
  }
}
