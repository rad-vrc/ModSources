using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace TranslateTest2.Common.Compatibility
{
    [JITWhenModsEnabled(new string[]{"androLib"})]
    public static class AndroLibReference
    {
        internal static FieldInfo BagUI_drawnUIData;
        private static Type BagUIType;
        private static FieldInfo _bagUIsField;
        private static PropertyInfo _hoveringProp;
        private static PropertyInfo _idProp;
        private static FieldInfo _slotDataField;
        private static PropertyInfo _isMouseHoveringProp; // reused dynamically if consistent
        private static bool _bagReflectionTried;
        private static void EnsureBagReflection()
        {
            if (_bagReflectionTried || !AndroLib.Enabled) return;
            _bagReflectionTried = true;
            var storageManagerType = AndroLib.Instance?.Code?.GetType("androLib.StorageManager");
            _bagUIsField = storageManagerType?.GetField("BagUIs", BindingFlags.Public|BindingFlags.Static);
            BagUIType = AndroLib.Instance?.Code?.GetType("androLib.UI.BagUI");
            if (BagUIType != null)
            {
                _hoveringProp = BagUIType.GetProperty("Hovering", BindingFlags.Public|BindingFlags.Instance);
                _idProp = BagUIType.GetProperty("ID", BindingFlags.Public|BindingFlags.Instance);
                BagUI_drawnUIData = BagUIType.GetField("drawnUIData", BindingFlags.NonPublic | BindingFlags.Instance);
            }
        }
        public static void UpdateItemSlot()
        {
            if (!AndroLib.Enabled) return;
            EnsureBagReflection();
            var bagUIs = _bagUIsField?.GetValue(null) as System.Collections.IEnumerable;
            object bagUi = null;
            if (bagUIs != null)
            {
                foreach (var ui in bagUIs)
                {
                    if (_hoveringProp != null && _hoveringProp.GetValue(ui) is bool hv && hv)
                    {
                        bagUi = ui; break;
                    }
                }
            }
            if (bagUi == null) return;
            var dataObj = BagUI_drawnUIData?.GetValue(bagUi);
            if (dataObj == null) return;
            if (_slotDataField == null)
                _slotDataField = dataObj.GetType().GetField("slotData", BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance);
            var slotArray = _slotDataField?.GetValue(dataObj) as Array;
            if (slotArray == null) return;
            int idx = -1;
            for (int i=0;i<slotArray.Length;i++)
            {
                var elem = slotArray.GetValue(i);
                if (elem == null) continue;
                var t = elem.GetType();
                var isHoverProp = _isMouseHoveringProp ?? t.GetProperty("IsMouseHovering", BindingFlags.Public|BindingFlags.Instance);
                if (_isMouseHoveringProp == null && isHoverProp != null) _isMouseHoveringProp = isHoverProp; // cache first found
                if (isHoverProp != null && isHoverProp.GetValue(elem) is bool b && b) { idx = i; break; }
            }
            if (idx == -1) return;
            int id = _idProp != null ? (int)_idProp.GetValue(bagUi) : -1;
            if (Main.LocalPlayer.TryGetModPlayer<AndroLibPlayer>(out var p)) p.UpdateSlotChange(id, idx);
        }
        public static void Load()
        {
            if (!AndroLib.Enabled) return;
            EnsureBagReflection();
        }
        public static void Unload()
        {
            BagUI_drawnUIData = null;
            BagUIType = null;
            _bagUIsField = null;
            _hoveringProp = null;
            _idProp = null;
            _slotDataField = null;
            _isMouseHoveringProp = null;
            _bagReflectionTried = false;
        }
    }
}
