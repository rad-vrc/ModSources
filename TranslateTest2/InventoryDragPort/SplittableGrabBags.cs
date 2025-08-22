// Disabled: migrated to Common/Config
#if false
namespace InventoryDrag.Config
{
    public class SplittableGrabBags
    {
        public bool Enabled = true;
        public bool ShowTooltip = true;
        public override bool Equals(object obj) => obj is SplittableGrabBags s && Enabled == s.Enabled && ShowTooltip == s.ShowTooltip;
        public override int GetHashCode() => System.HashCode.Combine(Enabled, ShowTooltip);
    }
}
#endif
