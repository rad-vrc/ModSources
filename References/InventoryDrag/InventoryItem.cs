// Decompiled with JetBrains decompiler
// Type: InventoryDrag.InventoryItem
// Assembly: InventoryDrag, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0AEBC7B1-9360-4E40-BFA4-A14C9A81EEA1
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\InventoryDrag\InventoryDrag.dll

using InventoryDrag.Config;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace InventoryDrag;

public class InventoryItem : GlobalItem
{
  private static LocalizedText _toolipText = Language.GetText("Mods.InventoryDrag.GrabBagTooltip");

  public static bool CanShiftStack(Item item)
  {
    return Main.ItemDropsDB.GetRulesForItemID(item.type).Count > 0;
  }

  public virtual void ModifyTooltips(Item item, List<TooltipLine> tooltips)
  {
    SplittableGrabBags splittableGrabBags = InventoryConfig.Instance.SplittableGrabBags;
    if ((!splittableGrabBags.Enabled ? 0 : (splittableGrabBags.ShowTooltip ? 1 : 0)) == 0 || !InventoryItem.CanShiftStack(item))
      return;
    int index1 = tooltips.FindIndex((Predicate<TooltipLine>) (x => x.Name == "Tooltip0"));
    int index2 = index1 == -1 ? tooltips.Count : index1;
    tooltips.Insert(index2, new TooltipLine(((ModType) this).Mod, "Shift", InventoryItem._toolipText.Value));
  }
}
