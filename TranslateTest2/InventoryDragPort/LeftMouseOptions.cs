using System.Collections.Generic;

namespace InventoryDrag.Config
{
    public class LeftMouseOptions
    {
        public bool Enabled = true;
        public ModifierOptions ModifierOptions = new(true, true, true, true, true);
        public override bool Equals(object obj) => obj is LeftMouseOptions other && Enabled == other.Enabled && EqualityComparer<ModifierOptions>.Default.Equals(ModifierOptions, other.ModifierOptions);
        public override int GetHashCode() => System.HashCode.Combine(Enabled, ModifierOptions);
    }
}
