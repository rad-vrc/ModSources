using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using TranslateTest2.Core;
using TranslateTest2.Config;

namespace TranslateTest2.Content.InfoDisplays
{
    public class BiomeInfoDisplay : InfoDisplay
    {
        public override bool Active()
        {
            return Main.LocalPlayer.GetModPlayer<InfoPlayer>().biomeDisplay;
        }

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            var mp = Main.LocalPlayer.GetModPlayer<InfoPlayer>();
            var list = mp.biomeNames ?? InfoPlayer.EmptyList; // 念のため null セーフ
            int count = list.Count;
            if (count == 0)
            {
                return Language.GetTextValue("Mods.TranslateTest2.Biomes.Neutral");
            }

            // 2000tick 周期を count スロットに等分 (整数割り: 参照実装の挙動維持)
            const int period = 2000;
            int slotSize = period / count; // count が 2000 超えることは実質ない想定
            if (slotSize <= 0) slotSize = 1; // 安全策
            int timerMod = mp.displayTimer % period;
            int displayIndex = timerMod / slotSize;
            if (displayIndex >= count) displayIndex = count - 1; // 端数補正

            string biomeToDisplay = list[displayIndex];
            if (string.IsNullOrEmpty(biomeToDisplay))
                biomeToDisplay = Language.GetTextValue("Mods.TranslateTest2.Biomes.Neutral");

            bool simple = ModContent.GetInstance<BiomeDisplayClientConfig>().simpleDisplay;
            if (!simple && count > 1)
                biomeToDisplay += " +";

            return biomeToDisplay;
        }
    }
}
