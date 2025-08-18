using InventoryDrag.Config;
using InventoryDrag.Compatability;
using Microsoft.Xna.Framework;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace InventoryDrag
{
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
        internal static MethodInfo ItemSlot_OverrideLeftClick = typeof(ItemSlot).GetMethod("OverrideLeftClick", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        internal static MethodInfo ItemSlot_LeftClick_SellOrTrash = typeof(ItemSlot).GetMethod("LeftClick_SellOrTrash", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        public bool OverrideHover(Item[] inventory, int context, int slot)
        {
            if (Player.whoAmI != Main.LocalPlayer.whoAmI) return false;
            hovering = true;
            bool itemChanged = itemCache != inventory[slot].type && context == 29;
            bool changed = noSlot || contextCache != context || slotCache != slot || itemChanged || AndroLib.DidBagSlotChange();
            contextCache = context;
            slotCache = slot;
            itemCache = inventory[slot].type;
            AndroLib.UpdateBagSlotCache();
            bool isCoinOrTrash = context == 9 || context == 11;
            if (inventory[slot].IsAir && !isCoinOrTrash) return false;
            if (Main.mouseLeft && changed) return HandleLeftClick(inventory, context, slot);
            if (Main.mouseRight && changed) return HandleRightClick(inventory, context, slot);
            return false;
        }
        private bool HandleLeftClick(Item[] inventory, int context, int slot)
        {
            bool release = Main.mouseLeftRelease;
            if (release != leftClickCache) TranslateTest2.TranslateTest2.DebugInChat($"mouseLeftRelease == leftClickCache ({release == leftClickCache})");
            if (release || AndroLib.PreventDoubleClickInJourneyMode(context, overrideShiftLeftClick))
            {
                overrideShiftLeftClick = false;
                TranslateTest2.TranslateTest2.DebugInChat($"vanilla left click context: {context}, slot: {slot} item: {inventory[slot].type}");
                return false;
            }
            var left = InventoryConfig.Instance.LeftMouse;
            if (!left.Enabled || !left.ModifierOptions.IsSatisfied(Player)) return false;
            TranslateTest2.TranslateTest2.DebugInChat($"custom left click context: {context}, slot: {slot} item: {inventory[slot].type}");
            if (HandleDragThrowing(inventory, context, slot) || VanillaLeftClick(inventory, context, slot)) return true;
            Main.mouseLeftRelease = true;
            ItemSlot.LeftClick(inventory, context, slot);
            leftClickCache = false;
            Main.mouseLeftRelease = release;
            return true;
        }
        private bool HandleRightClick(Item[] inventory, int context, int slot)
        {
            bool release = Main.mouseRightRelease;
            bool canRightClick = context == 0 && ItemLoader.CanRightClick(inventory[slot]);
            bool contextSpecial = context == 7 || context == 22;
            if ((release || (rightClickCache && canRightClick)) || contextSpecial)
            {
                TranslateTest2.TranslateTest2.DebugInChat($"vanilla right click context: {context}, slot: {slot} release: {Main.mouseRightRelease} cache: {rightClickCache}");
                return false;
            }
            var right = InventoryConfig.Instance.RightMouse;
            if (!right.Enabled || !right.ModifierOptions.IsSatisfied(Player)) return false;
            TranslateTest2.TranslateTest2.DebugInChat($"custom right click context: {context}, slot: {slot} item: {inventory[slot].type}");
            if (HandleDragThrowing(inventory, context, slot)) return true;
            Main.mouseRightRelease = true;
            ItemSlot.RightClick(inventory, context, slot);
            Main.mouseRightRelease = release;
            return true;
        }
        private bool HandleDragThrowing(Item[] inventory, int context, int slot)
        {
            if (!(Player.controlThrow && context != 29)) return false;
            var cfg = InventoryConfig.Instance.ThrowDragging;
            Player.DropSelectedItem(0, ref inventory[slot]);
            Player.SetItemAnimation(cfg.ThrowDelay);
            if (cfg.PlaySound) SoundEngine.PlaySound(in SoundID.Grab, new Vector2());
            return true;
        }
        private static bool VanillaLeftClick(Item[] inventory, int context, int slot)
        {
            Player lp = Main.LocalPlayer;
            if (Main.mouseLeft)
            {
                if (ItemSlot_OverrideLeftClick != null && (bool)ItemSlot_OverrideLeftClick.Invoke(null, new object[]{inventory, context, slot})) return true;
                inventory[slot].newAndShiny = false;
                if (ItemSlot_LeftClick_SellOrTrash != null && (bool)ItemSlot_LeftClick_SellOrTrash.Invoke(null, new object[]{inventory, context, slot})) return true;
                if (lp.itemAnimation != 0 || lp.itemTime != 0) return true;
            }
            return false;
        }
        public override bool ShiftClickSlot(Item[] inventory, int context, int slot) => base.ShiftClickSlot(inventory, context, slot);
        public override void ResetEffects() { }
    }
}
