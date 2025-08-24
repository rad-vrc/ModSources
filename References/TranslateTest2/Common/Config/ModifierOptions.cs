using Terraria;
using Terraria.UI;

namespace TranslateTest2.Common.Config
{
    public class ModifierOptions
    {
        public bool AllowCtrl = true;
        public bool AllowShift = true;
        public bool AllowAlt = true;
        public bool AllowThrow = true;
        public bool RequireModifier;
        public ModifierOptions() { }
        public ModifierOptions(bool allowCtrl, bool allowShift, bool allowAlt, bool allowThrow, bool requireModifier)
        {
            AllowCtrl = allowCtrl; AllowShift = allowShift; AllowAlt = allowAlt; AllowThrow = allowThrow; RequireModifier = requireModifier;
        }
        public bool IsSatisfied(Player player)
        {
            bool alt = Main.keyState.IsKeyDown(Main.FavoriteKey);
            bool ctrl = ItemSlot.ControlInUse;
            bool shift = ItemSlot.ShiftInUse;
            bool thr = player.controlThrow;
            if (!AllowAlt && alt) return false;
            if (!AllowCtrl && ctrl) return false;
            if (!AllowShift && shift) return false;
            if (!AllowThrow && thr) return false;
            if (RequireModifier && !(alt || ctrl || shift || thr)) return false;
            return true;
        }
        public override bool Equals(object obj)
        {
            return obj is ModifierOptions m && AllowCtrl == m.AllowCtrl && AllowShift == m.AllowShift && AllowAlt == m.AllowAlt && AllowThrow == m.AllowThrow && RequireModifier == m.RequireModifier;
        }
        public override int GetHashCode() => System.HashCode.Combine(AllowCtrl, AllowShift, AllowAlt, AllowThrow, RequireModifier);
    }
}
