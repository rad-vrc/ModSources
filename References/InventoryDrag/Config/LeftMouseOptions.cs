// Decompiled with JetBrains decompiler
// Type: InventoryDrag.Config.LeftMouseOptions
// Assembly: InventoryDrag, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0AEBC7B1-9360-4E40-BFA4-A14C9A81EEA1
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\InventoryDrag\InventoryDrag.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace InventoryDrag.Config;

public class LeftMouseOptions
{
  public bool Enabled = true;
  public ModifierOptions ModifierOptions = new ModifierOptions(true, true, true, true, true);

  public override bool Equals(object obj)
  {
    return obj is LeftMouseOptions leftMouseOptions && this.Enabled == leftMouseOptions.Enabled && EqualityComparer<ModifierOptions>.Default.Equals(this.ModifierOptions, leftMouseOptions.ModifierOptions);
  }

  public override int GetHashCode()
  {
    return HashCode.Combine<bool, ModifierOptions>(this.Enabled, this.ModifierOptions);
  }
}
