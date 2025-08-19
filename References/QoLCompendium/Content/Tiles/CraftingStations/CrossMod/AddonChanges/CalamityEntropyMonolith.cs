// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Tiles.CraftingStations.CrossMod.AddonChanges.CalamityEntropyMonolith
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using CalamityEntropy.Content.Tiles;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Tiles.CraftingStations.CrossMod.AddonChanges;

[JITWhenModsEnabled(new string[] {"CalamityEntropy"})]
[ExtendsFromMod(new string[] {"CalamityEntropy"})]
public class CalamityEntropyMonolith : GlobalTile
{
  public virtual int[] AdjTiles(int type)
  {
    HashSet<int> hashSet = ((IEnumerable<int>) base.AdjTiles(type)).ToHashSet<int>();
    if (type == ModContent.TileType<CalamityMonolithTile>())
      hashSet.Add(ModContent.TileType<AbyssalAltarTile>());
    return hashSet.ToArray<int>();
  }
}
