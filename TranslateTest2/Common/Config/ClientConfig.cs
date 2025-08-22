using System.ComponentModel;
using Terraria.ModLoader.Config;
using TranslateTest2.Core;

namespace TranslateTest2.Config
{
    public enum TranslatorMode
    {
        Auto,   // DeepLが使えればDeepL、ダメなら辞書、なければOff
        DeepL,  // 強制DeepL（キー必須）
        Dict,   // 辞書のみ
        Off     // すべて無効
    }

    public class ClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue("")]
        public string DeepLApiKey { get; set; } = "";

        [DefaultValue("ja")]
        public string TargetLang { get; set; } = "ja";

        [DefaultValue(true)]
        public bool UseDeepL { get; set; } = true;

        [DefaultValue(true)]
        public bool CacheDeepL { get; set; } = true;

        [DefaultValue(true)]
        public bool ShowShiftIndicator { get; set; } = true;

        [DefaultValue(true)]
        public bool EnableNameReconstruction { get; set; } = true;

    [DefaultValue(true)]
    public bool SkipBracketContentInNames { get; set; } = true;

        // DeepL 高速化のための設定
        [DefaultValue(1500)]
        [Range(250, 30000)] // 0.25s ～ 30s
        [Increment(250)]
        public int DeepLTimeoutMs { get; set; } = 1500;

        [DefaultValue(150)]
        [Range(50, 2000)] // 50ms ～ 2s
        [Increment(50)]
        public int DeepLBatchIntervalMs { get; set; } = 150;

        [DefaultValue(10)]
        [Range(1, 50)]
        public int DeepLBatchMax { get; set; } = 10;

        // DeepL 接続安定化用
        [DefaultValue("auto")] // auto | free | paid
        public string DeepLEndpointPreference { get; set; } = "auto";

        [DefaultValue(2)]
        [Range(0, 8)]
        public int DeepLRetryCount { get; set; } = 2;

        [DefaultValue(200)]
        [Range(0, 5000)]
        [Increment(100)]
        public int DeepLRetryInitialDelayMs { get; set; } = 200;

        [DefaultValue("")]
        public string ProxyUrl { get; set; } = "";

    [DefaultValue(TranslatorMode.Auto)]
    public TranslatorMode TranslationMode { get; set; } = TranslatorMode.Auto;

        public override void OnChanged()
        {
            // 設定変更が保存されたら即時反映
            TranslationService.Configure(this);
        }

        public override void OnLoaded()
        {
            // 起動時/リロード時に反映
            TranslationService.Configure(this);
        }
    }
}
