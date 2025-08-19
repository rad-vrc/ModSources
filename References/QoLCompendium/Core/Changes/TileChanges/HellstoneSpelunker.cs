// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.TileChanges.HellstoneSpelunker
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.TileChanges;

public class HellstoneSpelunker : GlobalTile
{
  public virtual bool? IsTileSpelunkable(int i, int j, int type)
  {
    return QoLCompendium.QoLCompendium.mainConfig.HellstoneSpelunker && type == 58 ? new bool?(true) : base.IsTileSpelunkable(i, j, type);
  }
}
