using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace TranslateTest2.Config
{
    // 元は ServerSide だったが Mod 全体 side=Client のため ClientSide に変更
    public class BiomeDisplayClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

    // Header はスペース不可の識別子/キーに変更
    [Header("BiomeDisplay")] 
        [DefaultValue(false)]
        public bool simpleDisplay;
    }
}
