// Decompiled with JetBrains decompiler
// Type: InventoryDrag.Config.ThrowDragging
// Assembly: InventoryDrag, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0AEBC7B1-9360-4E40-BFA4-A14C9A81EEA1
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\InventoryDrag\InventoryDrag.dll

using System;
using Terraria.ModLoader.Config;

#nullable disable
namespace InventoryDrag.Config;

public class ThrowDragging
{
  public bool PlaySound = true;
  [Range(0, 100)]
  public int ThrowDelay = 10;

  public override bool Equals(object obj)
  {
    return obj is ThrowDragging throwDragging && this.PlaySound == throwDragging.PlaySound && this.ThrowDelay == throwDragging.ThrowDelay;
  }

  public override int GetHashCode() => HashCode.Combine<bool, int>(this.PlaySound, this.ThrowDelay);
}
