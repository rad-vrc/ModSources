// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Tiles.Dedicated.BurgerTile
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Tiles.Dedicated;

public class BurgerTile : ModTile
{
  public virtual void SetStaticDefaults()
  {
    Main.tileSolid[(int) ((ModBlockType) this).Type] = true;
    Main.tileMergeDirt[(int) ((ModBlockType) this).Type] = false;
    Main.tileBlockLight[(int) ((ModBlockType) this).Type] = false;
    Main.tileLighted[(int) ((ModBlockType) this).Type] = false;
    ((ModBlockType) this).DustType = 2;
    this.AddMapEntry(new Color(188, 145, 73), (LocalizedText) null);
  }
}
