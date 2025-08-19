// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.AFKPetsMiracleFruitFix
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

public class AFKPetsMiracleFruitFix : GlobalTile
{
  public virtual void SetStaticDefaults()
  {
    if (!ModConditions.afkpetsLoaded)
      return;
    Main.tileCut[Common.GetModTile(ModConditions.afkpetsMod, "Plants")] = false;
  }

  public virtual void RandomUpdate(int i, int j, int type)
  {
    if (!ModConditions.afkpetsLoaded || type != Common.GetModTile(ModConditions.afkpetsMod, "Plants"))
      return;
    Tile tile = ((Tilemap) ref Main.tile)[i, j];
    if ((int) ((Tile) ref tile).TileFrameX / 18 != 6)
      return;
    ((Tile) ref tile).TileFrameY = (short) 18;
  }
}
