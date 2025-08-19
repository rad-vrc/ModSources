// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.BuffChanges.ItemInfo
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;

#nullable disable
namespace QoLCompendium.Core.Changes.BuffChanges;

internal readonly struct ItemInfo
{
  public readonly int type;
  public readonly int buffType;
  public readonly int stack;
  public readonly int createTile;
  public readonly int placeStyle;

  public ItemInfo(Item item)
  {
    this.type = item.type;
    this.buffType = item.buffType;
    this.stack = item.stack;
    this.createTile = item.createTile;
    this.placeStyle = item.placeStyle;
  }
}
