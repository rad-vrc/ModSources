// Disabled: migrated to Common/GlobalItems
#if false
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using InventoryDrag.Config;

namespace InventoryDrag
{
    public class InventoryItem : GlobalItem
    {
        private static LocalizedText _toolipText = Language.GetText("Mods.TranslateTest2.GrabBagTooltip");
        public static bool CanShiftStack(Item item) => Main.ItemDropsDB.GetRulesForItemID(item.type).Count > 0;
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            var cfg = InventoryConfig.Instance.SplittableGrabBags;
            if (!cfg.Enabled || !cfg.ShowTooltip || !CanShiftStack(item)) return;
            int idx = tooltips.FindIndex(x => x.Name == "Tooltip0");
            if (idx == -1) idx = tooltips.Count;
            tooltips.Insert(idx, new TooltipLine(Mod, "Shift", _toolipText.Value));
        }
    }
}
#endif
