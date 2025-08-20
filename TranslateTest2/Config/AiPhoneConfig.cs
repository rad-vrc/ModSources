using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace TranslateTest2.Configs
{
    public class AiPhoneConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

    [Header("Master")] // identifier OK
        [DefaultValue(true)] public bool EnableInfo { get; set; } = true;

    [Header("VanillaInfo")] // identifiers must not contain spaces; auto-localized at Mods.TranslateTest2.Configs.AiPhoneConfig.Headers.VanillaInfo
        [DefaultValue(true)] public bool Watch { get; set; } = true;
        [DefaultValue(true)] public bool Depth { get; set; } = true;
        [DefaultValue(true)] public bool Compass { get; set; } = true;
        [DefaultValue(true)] public bool FishFinder { get; set; } = true;
        [DefaultValue(true)] public bool Weather { get; set; } = true;
        [DefaultValue(true)] public bool Calendar { get; set; } = true;
        [DefaultValue(true)] public bool OreFinder { get; set; } = true;
        [DefaultValue(true)] public bool DreamCatcher { get; set; } = true;

    [Header("QoLCompendium")] // use identifier; explain in localized text
        [DefaultValue(true)] public bool DangerSense { get; set; } = true;
        [DefaultValue(true)] public bool DetectCreature { get; set; } = true;
        [DefaultValue(true)] public bool FindTreasure { get; set; } = true;

        // MosaicMirrorのInfoPlayer拡張（全部ON）
        [DefaultValue(true)] public bool Mosaic_All { get; set; } = true;

    // MosaicMirror 相当（QoLCompendium.InfoPlayer の各フィールド）個別トグル
    [DefaultValue(true)] public bool Mosaic_BattalionLog { get; set; } = true;
    [DefaultValue(true)] public bool Mosaic_HarmInducer { get; set; } = true;
    [DefaultValue(true)] public bool Mosaic_HeadCounter { get; set; } = true;
    [DefaultValue(true)] public bool Mosaic_Kettlebell { get; set; } = true;
    [DefaultValue(true)] public bool Mosaic_LuckyDie { get; set; } = true;
    [DefaultValue(true)] public bool Mosaic_MetallicClover { get; set; } = true;
    [DefaultValue(true)] public bool Mosaic_PlateCracker { get; set; } = true;
    [DefaultValue(true)] public bool Mosaic_Regenerator { get; set; } = true;
    [DefaultValue(true)] public bool Mosaic_ReinforcedPanel { get; set; } = true;
    [DefaultValue(true)] public bool Mosaic_Replenisher { get; set; } = true;
    [DefaultValue(true)] public bool Mosaic_TrackingDevice { get; set; } = true;
    [DefaultValue(true)] public bool Mosaic_WingTimer { get; set; } = true;
    }
}
