// Disabled: migrated to Common/Config
#if false
using System.Collections.Generic;

namespace InventoryDrag.Config
{
    public class RightMouseOptions
    {
        public bool Enabled = true;
        public ModifierOptions ModifierOptions = new()
        {
            AllowCtrl = true,
            AllowShift = true,
            AllowAlt = true,
            AllowThrow = false,
            RequireModifier = false
        };
        public override bool Equals(object obj) => obj is RightMouseOptions other && Enabled == other.Enabled && EqualityComparer<ModifierOptions>.Default.Equals(ModifierOptions, other.ModifierOptions);
        public override int GetHashCode() => System.HashCode.Combine(Enabled, ModifierOptions);
    }
}
#endif
