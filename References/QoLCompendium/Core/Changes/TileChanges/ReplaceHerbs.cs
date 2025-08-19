// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.TileChanges.ReplaceHerbs
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

public class ReplaceHerbs : ModPlayer
{
  public virtual void PostItemCheck()
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.RegrowthAutoReplant || !this.Player.controlUseItem || this.Player.HeldItem.type != 213 && this.Player.HeldItem.type != 5295 && (Main.mouseItem == null || Main.mouseItem.type != 213 && Main.mouseItem.type != 5295))
      return;
    ReplaceHerbs.GetHerbDrops(((Tilemap) ref Main.tile)[Player.tileTargetX, Player.tileTargetY]);
  }

  public static void GetHerbDrops(Tile tile)
  {
    if (!((Tile) ref tile).HasTile || ((Tile) ref tile).TileType == (ushort) 82 || ((Tile) ref tile).TileType != (ushort) 83 && ((Tile) ref tile).TileType != (ushort) 84)
      return;
    int style = (int) ((Tile) ref tile).TileFrameX / 18;
    if (style == 0)
    {
      ReplaceHerbs.DropItems(tile, 313, 307);
      ReplaceHerbs.ResetTileFrame(tile, style);
    }
    if (style == 1)
    {
      ReplaceHerbs.DropItems(tile, 314, 308);
      ReplaceHerbs.ResetTileFrame(tile, style);
    }
    if (style == 2)
    {
      ReplaceHerbs.DropItems(tile, 315, 309);
      ReplaceHerbs.ResetTileFrame(tile, style);
    }
    if (style == 3)
    {
      ReplaceHerbs.DropItems(tile, 316, 310);
      ReplaceHerbs.ResetTileFrame(tile, style);
    }
    if (style == 4)
    {
      ReplaceHerbs.DropItems(tile, 317, 311);
      ReplaceHerbs.ResetTileFrame(tile, style);
    }
    if (style == 5)
    {
      ReplaceHerbs.DropItems(tile, 318, 312);
      ReplaceHerbs.ResetTileFrame(tile, style);
    }
    if (style != 6)
      return;
    ReplaceHerbs.DropItems(tile, 2358, 2357);
    ReplaceHerbs.ResetTileFrame(tile, style);
  }

  public static void DropItems(Tile tile, int herbID, int seedID)
  {
    int num1 = 0;
    int num2 = 0;
    if (((Tile) ref tile).TileType == (ushort) 83)
    {
      num1 = 1;
      num2 = Main.rand.Next(1, 3);
    }
    if (((Tile) ref tile).TileType == (ushort) 84)
    {
      num1 = Main.rand.Next(1, 3);
      num2 = Main.rand.Next(1, 6);
    }
    Item.NewItem((IEntitySource) new EntitySource_TileBreak(Player.tileTargetX, Player.tileTargetY, (string) null), new Vector2((float) (Player.tileTargetX * 16 /*0x10*/), (float) (Player.tileTargetY * 16 /*0x10*/)), 8, 8, herbID, num1, false, 0, false, false);
    Item.NewItem((IEntitySource) new EntitySource_TileBreak(Player.tileTargetX, Player.tileTargetY, (string) null), new Vector2((float) (Player.tileTargetX * 16 /*0x10*/), (float) (Player.tileTargetY * 16 /*0x10*/)), 8, 8, seedID, num2, false, 0, false, false);
  }

  public static void ResetTileFrame(Tile tile, int style)
  {
    ((Tile) ref tile).TileType = (ushort) 82;
    ((Tile) ref tile).TileFrameX = (short) (18 * style);
    NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) Player.tileTargetX, (float) Player.tileTargetY, 82f, style, 0, 0);
  }
}
