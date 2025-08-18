// Decompiled with JetBrains decompiler
// Type: InventoryDrag.Compatability.AndroLibReference
// Assembly: InventoryDrag, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0AEBC7B1-9360-4E40-BFA4-A14C9A81EEA1
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\InventoryDrag\InventoryDrag.dll

using androLib;
using androLib.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace InventoryDrag.Compatability;

[JITWhenModsEnabled(new string[] {"androLib"})]
public static class AndroLibReference
{
  internal static FieldInfo BagUI_drawnUIData;

  public static void UpdateItemSlot()
  {
    BagUI bagUi = Enumerable.FirstOrDefault<BagUI>((IEnumerable<BagUI>) StorageManager.BagUIs, (Func<BagUI, bool>) (x => x.Hovering), (BagUI) null);
    if (bagUi == null || !(AndroLibReference.BagUI_drawnUIData?.GetValue((object) bagUi) is BagUI.DrawnUIData drawnUiData))
      return;
    int index = Array.FindIndex<UIItemSlotData>(drawnUiData.slotData, (Predicate<UIItemSlotData>) (x => ((UIItemSlotData) ref x).IsMouseHovering));
    AndroLibPlayer androLibPlayer;
    if (index == -1 || !Main.LocalPlayer.TryGetModPlayer<AndroLibPlayer>(ref androLibPlayer))
      return;
    androLibPlayer.UpdateSlotChange(bagUi.ID, index);
  }

  public static void Load()
  {
    AndroLibReference.BagUI_drawnUIData = typeof (BagUI).GetField("drawnUIData", (BindingFlags) 36);
  }

  public static void Unload() => AndroLibReference.BagUI_drawnUIData = (FieldInfo) null;
}
