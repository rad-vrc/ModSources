// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Tiles.CraftingStations.CrimsonAltarTile
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

public class CrimsonAltarTile : ModTile
{
  public virtual string Texture => "Terraria/Images/Tiles_26";

  public virtual void SetStaticDefaults()
  {
    Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
    Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
    Main.tileLavaDeath[(int) ((ModBlockType) this).Type] = true;
    TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
    Main.tileNoAttach[(int) ((ModBlockType) this).Type] = true;
    TileObjectData.newTile.CoordinateHeights = new int[2]
    {
      16 /*0x10*/,
      16 /*0x10*/
    };
    TileObjectData.newTile.StyleHorizontal = true;
    TileObjectData.newTile.DrawStyleOffset = 1;
    TileObjectData.addTile((int) ((ModBlockType) this).Type);
    this.AddMapEntry(new Color(200, 200, 200), ((ModBlockType) this).CreateMapEntryName());
    TileID.Sets.DisableSmartCursor[(int) ((ModBlockType) this).Type] = true;
    this.AdjTiles = new int[1]{ 26 };
    ((ModBlockType) this).DustType = -1;
  }

  public virtual void AnimateIndividualTile(
    int type,
    int i,
    int j,
    ref int frameXOffset,
    ref int frameYOffset)
  {
    frameXOffset = 54;
  }
}
