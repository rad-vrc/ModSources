using System;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;
using CoreNS = global::TranslateTest2.Core; // avoid type/namespace ambiguity with Mod class name
using ConfigNS = global::TranslateTest2.Configs;

namespace TranslateTest2.Content.Items.Tools
{
    public static class AiPhoneInfo
    {
        // MosaicMirrorのInfoPlayerフラグ名とConfigプロパティ名の対応
        static readonly (string Field, string ConfigProp)[] MosaicInfoMap = new[] {
            ("battalionLog",    nameof(ConfigNS.AiPhoneConfig.Mosaic_BattalionLog)),
            ("harmInducer",     nameof(ConfigNS.AiPhoneConfig.Mosaic_HarmInducer)),
            ("headCounter",     nameof(ConfigNS.AiPhoneConfig.Mosaic_HeadCounter)),
            ("kettlebell",      nameof(ConfigNS.AiPhoneConfig.Mosaic_Kettlebell)),
            ("luckyDie",        nameof(ConfigNS.AiPhoneConfig.Mosaic_LuckyDie)),
            ("metallicClover",  nameof(ConfigNS.AiPhoneConfig.Mosaic_MetallicClover)),
            ("plateCracker",    nameof(ConfigNS.AiPhoneConfig.Mosaic_PlateCracker)),
            ("regenerator",     nameof(ConfigNS.AiPhoneConfig.Mosaic_Regenerator)),
            ("reinforcedPanel", nameof(ConfigNS.AiPhoneConfig.Mosaic_ReinforcedPanel)),
            ("replenisher",     nameof(ConfigNS.AiPhoneConfig.Mosaic_Replenisher)),
            ("trackingDevice",  nameof(ConfigNS.AiPhoneConfig.Mosaic_TrackingDevice)),
            ("wingTimer",       nameof(ConfigNS.AiPhoneConfig.Mosaic_WingTimer))
        };

        public static void Apply(Player p)
        {
            var c = ModContent.GetInstance<ConfigNS.AiPhoneConfig>();
            if (!c.EnableInfo) return;

            // Ensure our InfoDisplay opt-in flag is set so BiomeInfoDisplay can activate
            try { p.GetModPlayer<CoreNS.InfoPlayer>().biomeDisplay = true; } catch { }

            // Optionally force-unhide info icons so they actually render
            if (c.UnhideInfo)
            {
                try
                {
                    for (int i = 0; i < p.hideInfo.Length; i++)
                        p.hideInfo[i] = false;
                }
                catch { }
            }

            // ---- Shellphone系（バニラ情報）----
            if (c.Watch && p.accWatch < 3) p.accWatch = 3;
            if (c.Depth)   p.accDepthMeter = 1;
            if (c.Compass) p.accCompass = 1;

            if (c.FishFinder)   p.accFishFinder = true;
            if (c.Weather)      p.accWeatherRadio = true;
            if (c.Calendar)     p.accCalendar = true;
            if (c.OreFinder)    p.accOreFinder = true;
            if (c.DreamCatcher) p.accDreamCatcher = true;

            // QoL-ish 視覚情報（危険/敵/財宝）
            if (c.DangerSense)    p.dangerSense = true;
            if (c.DetectCreature) p.detectCreature = true;
            if (c.FindTreasure)   p.findTreasure = true;

            // ---- MosaicMirror系（QoLCompendium の InfoPlayer に反映）----
            // Mosaic_All が true の場合は全ON。false の場合は個別トグルで反映。

            if (ModLoader.TryGetMod("QoLCompendium", out var ql))
            {
                try
                {
                    // （任意）QoL側の InformationAccessories 設定がOFFなら抜ける
                    bool allow = true;
                    try {
                        var root = ql.Code?.GetType("QoLCompendium.QoLCompendium");
                        var fiItemConfig = root?.GetField("itemConfig", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                        var itemCfg = fiItemConfig?.GetValue(null);
                        var piInfoAcc = itemCfg?.GetType().GetProperty("InformationAccessories", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        if (piInfoAcc != null) allow = (bool)piInfoAcc.GetValue(itemCfg);
                    } catch { /* 読めなければ許可扱い */ }
                    if (!allow) return;

                    // InfoPlayer を Player からジェネリック反射で取得
                    var infoType = ql.Code?.GetType("QoLCompendium.Core.InfoPlayer");
                    if (infoType == null) return;

                    var miGeneric = typeof(Player).GetMethods(BindingFlags.Instance | BindingFlags.Public)
                        .FirstOrDefault(m => m.Name == "GetModPlayer" && m.IsGenericMethodDefinition && m.GetParameters().Length == 0);
                    var mi = miGeneric?.MakeGenericMethod(infoType);
                    var mp = mi?.Invoke(p, null);
                    if (mp == null) return;

                    foreach (var (fieldName, cfgName) in MosaicInfoMap)
                    {
                        bool enable = c.Mosaic_All;
                        if (!enable)
                        {
                            try
                            {
                                var pi = typeof(ConfigNS.AiPhoneConfig).GetProperty(cfgName, BindingFlags.Instance | BindingFlags.Public);
                                if (pi != null) enable = (bool)(pi.GetValue(c) ?? false);
                            }
                            catch { enable = false; }
                        }

                        if (!enable) continue;

                        var field = infoType.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        field?.SetValue(mp, true);
                    }
                }
                catch { /* 失敗しても落とさない */ }
            }
        }
    }
}
