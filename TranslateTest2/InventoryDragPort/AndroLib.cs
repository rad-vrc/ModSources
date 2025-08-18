using Terraria.ModLoader;
using Terraria;
using System; // for Type
using System.Reflection; // for BindingFlags

namespace InventoryDrag.Compatability
{
    public static class AndroLib
    {
        internal const string androLib = "androLib";
        public static Mod Instance = null;
        public static bool Enabled = ModLoader.TryGetMod(androLib, out Instance);
        // Reflection cache
        private static Type _masterUIManagerType;
        private static PropertyInfo _propNoUIBeingHovered;
        private static PropertyInfo _propUIBeingHovered;
        private static bool _uiReflectionTried;
        private static void EnsureUIReflection()
        {
            if (_uiReflectionTried || !Enabled) return;
            _uiReflectionTried = true;
            _masterUIManagerType = Instance?.Code?.GetType("androLib.UI.MasterUIManager");
            if (_masterUIManagerType != null)
            {
                _propNoUIBeingHovered = _masterUIManagerType.GetProperty("NoUIBeingHovered", BindingFlags.Public|BindingFlags.Static);
                _propUIBeingHovered = _masterUIManagerType.GetProperty("UIBeingHovered", BindingFlags.Public|BindingFlags.Static);
            }
        }
        public static bool PreventDoubleClickInJourneyMode(int context, bool overrideShiftLeftClick) => Enabled && context == 29 & overrideShiftLeftClick;
        public static void Load(Mod mod)
        {
            if (!Enabled) return;
            AndroLibReference.Load();
            mod.AddContent<AndroLibPlayer>();
        }
        public static void Unload(Mod mod) => AndroLibReference.Unload();
    [JITWhenModsEnabled(new string[]{"androLib"})]
        public static bool DidBagSlotChange()
        {
            if (!Enabled || NoBagsHovered) return false;
            if (Main.LocalPlayer.TryGetModPlayer<AndroLibPlayer>(out var p)) return p.didSlotChange;
            return false;
        }
        // Access through reflection to avoid hard compile dependency when androLib not present.
    [JITWhenModsEnabled(new string[]{"androLib"})] public static bool NoBagsHovered
        {
            get
            {
                if (!Enabled) return true;
                EnsureUIReflection();
                return _propNoUIBeingHovered is null ? true : (bool)_propNoUIBeingHovered.GetValue(null);
            }
        }
    [JITWhenModsEnabled(new string[]{"androLib"})] public static int HoverId
        {
            get
            {
                if (!Enabled) return -1;
                EnsureUIReflection();
                return _propUIBeingHovered is null ? -1 : (int)_propUIBeingHovered.GetValue(null);
            }
        }
    [JITWhenModsEnabled(new string[]{"androLib"})] public static void UpdateBagSlotCache() { if (!Enabled) return; AndroLibReference.UpdateItemSlot(); }
    }
}
