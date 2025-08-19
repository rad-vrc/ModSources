using System.Collections.Generic;

namespace Terraria {
    public class Item {
        public bool active;
    }
}

namespace Terraria.ModLoader {
    public class TooltipLine {
        public string Text { get; set; } = string.Empty;
        public string Mod { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class GlobalItem {
        public virtual void ModifyTooltips(Terraria.Item item, List<TooltipLine> tooltips) { }
    }
}

namespace TranslateTest2 {
    public class TranslateTest2 {
        public static TranslateTest2? Instance => null;
        public dynamic? Logger => null;
    }
}

namespace Microsoft.Xna.Framework.Input {
    public enum Keys { LeftShift, RightShift }
    public struct KeyboardState { public bool IsKeyDown(Keys key) => false; }
    public static class Keyboard { public static KeyboardState GetState() => new KeyboardState(); }
}

namespace TranslateTest2.Core {
    public static class TooltipTranslator {
        public static string TranslateLine(string line) => line;
    }

    public static class DeepLTranslator {
        public static bool IsEnabled => false;
        public static bool TryGetCached(string text, out string? cached) { cached = null; return false; }
        public static void RequestIfMissing(string text) {}
    }

    public static class TextLangHelper {
        public static bool NeedsTranslation(string text) => false;
    }

    public static class NameComposer {
        public static bool TryCompose(string text, out string? composed) { composed = null; return false; }
    }
}
