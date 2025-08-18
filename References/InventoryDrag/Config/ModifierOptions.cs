// Decompiled with JetBrains decompiler
// Type: InventoryDrag.Config.ModifierOptions
// Assembly: InventoryDrag, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0AEBC7B1-9360-4E40-BFA4-A14C9A81EEA1
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\InventoryDrag\InventoryDrag.dll

using Microsoft.Xna.Framework.Input;
using System;
using Terraria;
using Terraria.UI;

#nullable disable
namespace InventoryDrag.Config;

public class ModifierOptions
{
  public bool AllowCtrl = true;
  public bool AllowShift = true;
  public bool AllowAlt = true;
  public bool AllowThrow = true;
  public bool RequireModifier;

  public ModifierOptions()
  {
    this.AllowCtrl = true;
    this.AllowShift = true;
    this.AllowAlt = true;
    this.AllowThrow = true;
    this.RequireModifier = false;
  }

  public ModifierOptions(
    bool allowCtrl,
    bool allowShift,
    bool allowAlt,
    bool allowThrow,
    bool requireModifier)
  {
    this.AllowCtrl = allowCtrl;
    this.AllowShift = allowShift;
    this.AllowAlt = allowAlt;
    this.AllowThrow = allowThrow;
    this.RequireModifier = requireModifier;
  }

  public bool IsSatisfied(Player player)
  {
    bool flag = ((KeyboardState) ref Main.keyState).IsKeyDown(Main.FavoriteKey);
    bool controlInUse = ItemSlot.ControlInUse;
    bool shiftInUse = ItemSlot.ShiftInUse;
    bool controlThrow = player.controlThrow;
    return !(!this.AllowAlt & flag) && !(!this.AllowCtrl & controlInUse) && !(!this.AllowShift & shiftInUse) && !(!this.AllowThrow & controlThrow) && (!this.RequireModifier || flag | controlInUse | shiftInUse | controlThrow);
  }

  public override bool Equals(object obj)
  {
    return obj is ModifierOptions modifierOptions && this.AllowCtrl == modifierOptions.AllowCtrl && this.AllowShift == modifierOptions.AllowShift && this.AllowAlt == modifierOptions.AllowAlt && this.AllowThrow == modifierOptions.AllowThrow && this.RequireModifier == modifierOptions.RequireModifier;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine<bool, bool, bool, bool, bool>(this.AllowCtrl, this.AllowShift, this.AllowAlt, this.AllowThrow, this.RequireModifier);
  }
}
