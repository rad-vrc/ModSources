using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace InventoryDrag.Config
{
    public class InventoryConfig : ModConfig
    {
        [Header("MainFeatures")] public LeftMouseOptions LeftMouse = new();
        public RightMouseOptions RightMouse = new();
        [Header("ExtraFeatures")] public SplittableGrabBags SplittableGrabBags = new();
        public ThrowDragging ThrowDragging = new();
        public bool DebugMessages;
        public static InventoryConfig Instance => ModContent.GetInstance<InventoryConfig>();
        public override ConfigScope Mode => ConfigScope.ClientSide;
    }
}
