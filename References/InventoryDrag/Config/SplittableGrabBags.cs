// Decompiled with JetBrains decompiler
// Type: InventoryDrag.Config.SplittableGrabBags
// Assembly: InventoryDrag, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0AEBC7B1-9360-4E40-BFA4-A14C9A81EEA1
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\InventoryDrag\InventoryDrag.dll

using System;

#nullable disable
namespace InventoryDrag.Config;

public class SplittableGrabBags
{
  public bool Enabled = true;
  public bool ShowTooltip = true;

  public override bool Equals(object obj)
  {
    return obj is SplittableGrabBags splittableGrabBags && this.Enabled == splittableGrabBags.Enabled && this.ShowTooltip == splittableGrabBags.ShowTooltip;
  }

  public override int GetHashCode() => HashCode.Combine<bool, bool>(this.Enabled, this.ShowTooltip);
}
