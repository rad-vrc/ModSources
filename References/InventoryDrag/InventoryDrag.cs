// Decompiled with JetBrains decompiler
// Type: InventoryDrag.InventoryDrag
// Assembly: InventoryDrag, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0AEBC7B1-9360-4E40-BFA4-A14C9A81EEA1
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\InventoryDrag\InventoryDrag.dll

using InventoryDrag.Compatability;
using InventoryDrag.Config;
using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace InventoryDrag;

public class InventoryDrag : Mod
{
  internal static MethodInfo ItemLoader_CanRightClick = typeof (ItemLoader).GetMethod("CanRightClick", (BindingFlags) 24);
  internal static MethodInfo PlayerLoader_ShiftClickSlot = typeof (PlayerLoader).GetMethod("ShiftClickSlot", (BindingFlags) 24);

  public virtual void Load()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_ItemSlot.MouseHover_ItemArray_int_int += InventoryDrag.InventoryDrag.\u003C\u003EO.\u003C0\u003E__On_ItemSlot_MouseHover_ItemArray_int_int ?? (InventoryDrag.InventoryDrag.\u003C\u003EO.\u003C0\u003E__On_ItemSlot_MouseHover_ItemArray_int_int = new On_ItemSlot.hook_MouseHover_ItemArray_int_int((object) null, __methodptr(On_ItemSlot_MouseHover_ItemArray_int_int)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_ItemSlot.RightClick_ItemArray_int_int += InventoryDrag.InventoryDrag.\u003C\u003EO.\u003C1\u003E__On_ItemSlot_RightClick_ItemArray_int_int ?? (InventoryDrag.InventoryDrag.\u003C\u003EO.\u003C1\u003E__On_ItemSlot_RightClick_ItemArray_int_int = new On_ItemSlot.hook_RightClick_ItemArray_int_int((object) null, __methodptr(On_ItemSlot_RightClick_ItemArray_int_int)));
    // ISSUE: method pointer
    On_ItemSlot.Handle_refItem_int += new On_ItemSlot.hook_Handle_refItem_int((object) this, __methodptr(On_ItemSlot_Handle_refItem_int));
    // ISSUE: method pointer
    On_ItemSlot.LeftClick_ItemArray_int_int += new On_ItemSlot.hook_LeftClick_ItemArray_int_int((object) this, __methodptr(On_ItemSlot_LeftClick_ItemArray_int_int));
    // ISSUE: method pointer
    On_Main.DrawInventory += new On_Main.hook_DrawInventory((object) this, __methodptr(On_Main_DrawInventory));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    MonoModHooks.Add((MethodBase) InventoryDrag.InventoryDrag.ItemLoader_CanRightClick, (Delegate) (InventoryDrag.InventoryDrag.\u003C\u003EO.\u003C2\u003E__On_ItemLoader_CanRightClick_Item ?? (InventoryDrag.InventoryDrag.\u003C\u003EO.\u003C2\u003E__On_ItemLoader_CanRightClick_Item = new Func<InventoryDrag.InventoryDrag.orig_ItemLoader_CanRightClick, Item, bool>(InventoryDrag.InventoryDrag.On_ItemLoader_CanRightClick_Item))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    MonoModHooks.Add((MethodBase) InventoryDrag.InventoryDrag.PlayerLoader_ShiftClickSlot, (Delegate) (InventoryDrag.InventoryDrag.\u003C\u003EO.\u003C3\u003E__On_PlayerLoader_ShiftClickSlot ?? (InventoryDrag.InventoryDrag.\u003C\u003EO.\u003C3\u003E__On_PlayerLoader_ShiftClickSlot = new Func<InventoryDrag.InventoryDrag.orig_PlayerLoader_ShiftClickSlot, Player, Item[], int, int, bool>(InventoryDrag.InventoryDrag.On_PlayerLoader_ShiftClickSlot))));
    AndroLib.Load((Mod) this);
  }

  private void On_ItemSlot_Handle_refItem_int(
    On_ItemSlot.orig_Handle_refItem_int orig,
    ref Item inv,
    int context)
  {
    InventoryPlayer inventoryPlayer;
    if (Main.LocalPlayer.TryGetModPlayer<InventoryPlayer>(ref inventoryPlayer))
      inventoryPlayer.noSlot = false;
    orig.Invoke(ref inv, context);
  }

  public virtual void Unload() => AndroLib.Unload((Mod) this);

  private void On_Main_DrawInventory(On_Main.orig_DrawInventory orig, Main self)
  {
    InventoryPlayer modPlayer = Main.LocalPlayer.GetModPlayer<InventoryPlayer>();
    modPlayer.hovering = false;
    orig.Invoke(self);
    modPlayer.noSlot = !modPlayer.hovering;
  }

  private static bool On_ItemLoader_CanRightClick_Item(
    InventoryDrag.InventoryDrag.orig_ItemLoader_CanRightClick orig,
    Item item)
  {
    bool flag = orig(item);
    return (!InventoryConfig.Instance.SplittableGrabBags.Enabled || !ItemSlot.ShiftInUse || Main.ItemDropsDB.GetRulesForItemID(item.type).Count <= 0) && flag;
  }

  private static bool On_PlayerLoader_ShiftClickSlot(
    InventoryDrag.InventoryDrag.orig_PlayerLoader_ShiftClickSlot orig,
    Player player,
    Item[] inventory,
    int context,
    int slot)
  {
    bool flag = orig(player, inventory, context, slot);
    InventoryPlayer inventoryPlayer;
    if (player.TryGetModPlayer<InventoryPlayer>(ref inventoryPlayer))
      inventoryPlayer.overrideShiftLeftClick = flag;
    return flag;
  }

  private void On_ItemSlot_LeftClick_ItemArray_int_int(
    On_ItemSlot.orig_LeftClick_ItemArray_int_int orig,
    Item[] inv,
    int context,
    int slot)
  {
    InventoryPlayer inventoryPlayer;
    if (Main.LocalPlayer.TryGetModPlayer<InventoryPlayer>(ref inventoryPlayer))
      inventoryPlayer.leftClickCache = Main.mouseLeftRelease;
    orig.Invoke(inv, context, slot);
  }

  private static void On_ItemSlot_RightClick_ItemArray_int_int(
    On_ItemSlot.orig_RightClick_ItemArray_int_int orig,
    Item[] inv,
    int context,
    int slot)
  {
    InventoryPlayer inventoryPlayer;
    if (Main.LocalPlayer.TryGetModPlayer<InventoryPlayer>(ref inventoryPlayer))
      inventoryPlayer.rightClickCache = Main.mouseRightRelease;
    orig.Invoke(inv, context, slot);
  }

  private static void On_ItemSlot_MouseHover_ItemArray_int_int(
    On_ItemSlot.orig_MouseHover_ItemArray_int_int orig,
    Item[] inv,
    int context,
    int slot)
  {
    InventoryPlayer inventoryPlayer;
    if (Main.LocalPlayer.TryGetModPlayer<InventoryPlayer>(ref inventoryPlayer))
      Main.LocalPlayer.GetModPlayer<InventoryPlayer>().OverrideHover(inv, context, slot);
    orig.Invoke(inv, context, slot);
  }

  public static void DebugInChat(string text)
  {
    if (!InventoryConfig.Instance.DebugMessages)
      return;
    Main.NewText(text, byte.MaxValue, byte.MaxValue, byte.MaxValue);
  }

  private delegate bool orig_ItemLoader_CanRightClick(Item item);

  private delegate bool orig_PlayerLoader_ShiftClickSlot(
    Player player,
    Item[] inventory,
    int context,
    int slot);
}
