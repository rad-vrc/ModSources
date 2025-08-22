using Terraria.ModLoader;
using Terraria;

namespace TranslateTest2.Common.Compatability
{
    [Autoload(false)]
    public class AndroLibPlayer : ModPlayer
    {
        internal int slotIdCache;
        internal int slotIndexCache;
        internal bool didSlotChange;
        public void UpdateSlotChange(int slotId, int slotIndex)
        {
            if (Main.LocalPlayer.whoAmI != Player.whoAmI) return;
            didSlotChange = slotId != slotIdCache || slotIndex != slotIndexCache;
            slotIdCache = slotId;
            slotIndexCache = slotIndex;
        }
    }
}
