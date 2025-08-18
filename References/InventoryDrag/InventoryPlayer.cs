// Decompiled with JetBrains decompiler
// Type: InventoryDrag.InventoryPlayer
// Assembly: InventoryDrag, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0AEBC7B1-9360-4E40-BFA4-A14C9A81EEA1
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\InventoryDrag\InventoryDrag.dll

using InventoryDrag.Compatability;
using InventoryDrag.Config;
using Microsoft.Xna.Framework;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace InventoryDrag;

public class InventoryPlayer : ModPlayer
{
  internal int contextCache = -1;
  internal int slotCache = -1;
  internal int itemCache;
  internal bool hovering;
  internal bool noSlot = true;
  internal bool rightClickCache = Main.mouseRightRelease;
  internal bool leftClickCache = Main.mouseLeftRelease;
  internal bool overrideShiftLeftClick;
  internal static MethodInfo ItemSlot_OverrideLeftClick = typeof (ItemSlot).GetMethod("OverrideLeftClick", (BindingFlags) 40);
  internal static MethodInfo ItemSlot_LeftClick_SellOrTrash = typeof (ItemSlot).GetMethod("LeftClick_SellOrTrash", (BindingFlags) 40);

  public bool OverrideHover(Item[] inventory, int context, int slot)
  {
    if (((Entity) this.Player).whoAmI != ((Entity) Main.LocalPlayer).whoAmI)
      return false;
    this.hovering = true;
    bool flag1 = this.itemCache != inventory[slot].type && context == 29;
    bool flag2 = ((this.noSlot || this.contextCache != context ? 1 : (this.slotCache != slot ? 1 : 0)) | (flag1 ? 1 : 0)) != 0 || AndroLib.DidBagSlotChange();
    this.contextCache = context;
    this.slotCache = slot;
    this.itemCache = inventory[slot].type;
    AndroLib.UpdateBagSlotCache();
    bool flag3 = context == 9 || context == 11;
    if (inventory[slot].IsAir && !flag3)
      return false;
    if (Main.mouseLeft & flag2)
      return this.HandleLeftClick(inventory, context, slot);
    return Main.mouseRight & flag2 && this.HandleRightClick(inventory, context, slot);
  }

  private bool HandleLeftClick(Item[] inventory, int context, int slot)
  {
    bool mouseLeftRelease = Main.mouseLeftRelease;
    if (mouseLeftRelease != this.leftClickCache)
      InventoryDrag.InventoryDrag.DebugInChat($"mouseLeftRelease == leftClickCache ({mouseLeftRelease == this.leftClickCache})");
    if (mouseLeftRelease || AndroLib.PreventDoubleClickInJourneyMode(context, this.overrideShiftLeftClick))
    {
      this.overrideShiftLeftClick = false;
      InventoryDrag.InventoryDrag.DebugInChat($"vanilla left click context: {context}, slot: {slot} item: {inventory[slot].type}");
      return false;
    }
    LeftMouseOptions leftMouse = InventoryConfig.Instance.LeftMouse;
    if (!leftMouse.Enabled || !leftMouse.ModifierOptions.IsSatisfied(this.Player))
      return false;
    InventoryDrag.InventoryDrag.DebugInChat($"custom left click context: {context}, slot: {slot} item: {inventory[slot].type}");
    if (this.HandleDragThrowing(inventory, context, slot) || InventoryPlayer.VanillaLeftClick(inventory, context, slot))
      return true;
    Main.mouseLeftRelease = true;
    ItemSlot.LeftClick(inventory, context, slot);
    this.leftClickCache = false;
    Main.mouseLeftRelease = mouseLeftRelease;
    return true;
  }

  private bool HandleRightClick(Item[] inventory, int context, int slot)
  {
    bool mouseRightRelease = Main.mouseRightRelease;
    bool flag1 = context == 0 && ItemLoader.CanRightClick(inventory[slot]);
    bool flag2 = context == 7 || context == 22;
    if (((mouseRightRelease ? 1 : (this.rightClickCache & flag1 ? 1 : 0)) | (flag2 ? 1 : 0)) != 0)
    {
      InventoryDrag.InventoryDrag.DebugInChat($"vanilla right click context: {context}, slot: {slot} release: {Main.mouseRightRelease} cache: {this.rightClickCache}");
      return false;
    }
    RightMouseOptions rightMouse = InventoryConfig.Instance.RightMouse;
    if (!rightMouse.Enabled || !rightMouse.ModifierOptions.IsSatisfied(this.Player))
      return false;
    InventoryDrag.InventoryDrag.DebugInChat($"custom right click context: {context}, slot: {slot} item: {inventory[slot].type}");
    if (this.HandleDragThrowing(inventory, context, slot))
      return true;
    Main.mouseRightRelease = true;
    ItemSlot.RightClick(inventory, context, slot);
    Main.mouseRightRelease = mouseRightRelease;
    return true;
  }

  private bool HandleDragThrowing(Item[] inventory, int context, int slot)
  {
    if (!(this.Player.controlThrow & context != 29))
      return false;
    ThrowDragging throwDragging = InventoryConfig.Instance.ThrowDragging;
    this.Player.DropSelectedItem(0, ref inventory[slot]);
    this.Player.SetItemAnimation(throwDragging.ThrowDelay);
    if (throwDragging.PlaySound)
      SoundEngine.PlaySound(ref SoundID.Grab, new Vector2?(), (SoundUpdateCallback) null);
    return true;
  }

  private static bool VanillaLeftClick(Item[] inventory, int context, int slot)
  {
    Player localPlayer = Main.LocalPlayer;
    if (Main.mouseLeft)
    {
      MethodInfo overrideLeftClick = InventoryPlayer.ItemSlot_OverrideLeftClick;
      object obj1;
      if (overrideLeftClick == null)
        obj1 = (object) null;
      else
        obj1 = ((MethodBase) overrideLeftClick).Invoke((object) null, new object[3]
        {
          (object) inventory,
          (object) context,
          (object) slot
        });
      if ((bool) obj1)
        return true;
      inventory[slot].newAndShiny = false;
      MethodInfo clickSellOrTrash = InventoryPlayer.ItemSlot_LeftClick_SellOrTrash;
      object obj2;
      if (clickSellOrTrash == null)
        obj2 = (object) null;
      else
        obj2 = ((MethodBase) clickSellOrTrash).Invoke((object) null, new object[3]
        {
          (object) inventory,
          (object) context,
          (object) slot
        });
      if ((bool) obj2 || localPlayer.itemAnimation != 0 || localPlayer.itemTime != 0)
        return true;
    }
    return false;
  }

  public virtual bool ShiftClickSlot(Item[] inventory, int context, int slot)
  {
    return base.ShiftClickSlot(inventory, context, slot);
  }

  public virtual void ResetEffects()
  {
  }
}
