// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Tiles.Other.AsphaltPlatformTile
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace QoLCompendium.Content.Tiles.Other;

public class AsphaltPlatformTile : ModTile
{
  public virtual void SetStaticDefaults()
  {
    Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
    Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
    Main.tileSolidTop[(int) ((ModBlockType) this).Type] = true;
    Main.tileSolid[(int) ((ModBlockType) this).Type] = true;
    Main.tileNoAttach[(int) ((ModBlockType) this).Type] = true;
    Main.tileTable[(int) ((ModBlockType) this).Type] = true;
    TileID.Sets.Platforms[(int) ((ModBlockType) this).Type] = true;
    TileObjectData.newTile.CoordinateHeights = new int[1]
    {
      16 /*0x10*/
    };
    TileObjectData.newTile.CoordinateWidth = 16 /*0x10*/;
    TileObjectData.newTile.CoordinatePadding = 2;
    TileObjectData.newTile.StyleHorizontal = true;
    TileObjectData.newTile.StyleMultiplier = 27;
    TileObjectData.newTile.StyleWrapLimit = 27;
    TileObjectData.newTile.UsesCustomCanPlace = false;
    TileObjectData.newTile.LavaDeath = false;
    TileObjectData.addTile((int) ((ModBlockType) this).Type);
    this.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
    this.AddMapEntry(new Color(47, 51, 58), (LocalizedText) null);
    ((ModBlockType) this).DustType = 54;
    this.AdjTiles = new int[1]{ 19 };
  }

  public virtual void PostSetDefaults()
  {
    Main.tileNoSunLight[(int) ((ModBlockType) this).Type] = false;
  }

  public virtual void NumDust(int i, int j, bool fail, ref int num) => num = 10;

  public virtual void FloorVisuals(Player player) => player.powerrun = true;
}
