// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.TileChanges.DangersenseChanges
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.TileChanges;

public class DangersenseChanges : GlobalTile
{
  public virtual bool? IsTileDangerous(int i, int j, int type, Player player)
  {
    if (QoLCompendium.QoLCompendium.mainConfig.DangersenseHighlightsSiltAndSlush && (type == 123 || type == 224 /*0xE0*/))
      return new bool?(true);
    return QoLCompendium.QoLCompendium.mainConfig.DangersenseIgnoresThinIce && type == 162 ? new bool?(false) : base.IsTileDangerous(i, j, type, player);
  }
}
