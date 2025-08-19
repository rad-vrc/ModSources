// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Tiles.CraftingStations.HardmodeMonolithTile
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace QoLCompendium.Content.Tiles.CraftingStations;

public class HardmodeMonolithTile : ModTile
{
  public virtual void SetStaticDefaults()
  {
    Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
    TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
    Main.tileNoAttach[(int) ((ModBlockType) this).Type] = true;
    TileObjectData.newTile.CoordinateHeights = new int[4]
    {
      16 /*0x10*/,
      16 /*0x10*/,
      16 /*0x10*/,
      16 /*0x10*/
    };
    TileObjectData.addTile((int) ((ModBlockType) this).Type);
    this.AddMapEntry(new Color(200, 200, 200), ((ModBlockType) this).CreateMapEntryName());
    TileID.Sets.DisableSmartCursor[(int) ((ModBlockType) this).Type] = true;
    this.AdjTiles = new int[9]
    {
      134,
      133,
      101,
      125,
      301,
      499,
      307,
      217,
      218
    };
    ((ModBlockType) this).DustType = -1;
  }
}
