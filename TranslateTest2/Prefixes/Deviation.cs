// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Prefixes.Deviation
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Prefixes
{
    public class Deviation : ModPrefix
    {
    public override PrefixCategory Category => (PrefixCategory)3;

    public override float RollChance(Item item) => 0.8f;

    public override bool CanRoll(Item item) => item.shoot != ProjectileID.None && item.isMinionSummonItem();

        public override void SetStats(
            ref float damageMult,
            ref float knockbackMult,
            ref float useTimeMult,
            ref float scaleMult,
            ref float shootSpeedMult,
            ref float manaMult,
            ref int critBonus)
        {
            damageMult = 1.1f;
        }

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.4f;

        public override void Apply(Item item)
        {
        }

        /// <summary>
        /// 元のSummonerPrefix方式: ローカリゼーションからツールチップテキストを取得
        /// </summary>
    public LocalizedText AdditionalTooltip => Language.GetOrRegister(Mod.GetLocalizationKey($"Prefix{Name}Descr"), null);

        public override void SetStaticDefaults()
        {
            _ = AdditionalTooltip;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            // ローカライズはAdditionalTooltipのみを使用
            var tl = new TooltipLine(Mod, "PrefixDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = true
            };

            // Deviationはプレースホルダーなし（固定テキスト）
            yield return tl;
        }
    }
}