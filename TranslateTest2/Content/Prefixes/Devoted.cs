// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Prefixes.Devoted
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Content.Prefixes
{
    public class Devoted : ModPrefix
    {
    public static float Increase = 0.15f;
    public static float Decrease = 0.2f;

    public override PrefixCategory Category => (PrefixCategory)3;

    public override float RollChance(Item item) => 1f;

    public override bool CanRoll(Item item) => item.shoot != ProjectileID.None && ProjectileID.Sets.IsAWhip[item.shoot];

        public override void SetStats(
            ref float damageMult,
            ref float knockbackMult,
            ref float useTimeMult,
            ref float scaleMult,
            ref float shootSpeedMult,
            ref float manaMult,
            ref int critBonus)
        {
        }

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.2f;

        public override void Apply(Item item)
        {
            item.global().CanGiveTag = true;
            item.global().wTag.SpecialType = nameof(Devoted);
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tl = new TooltipLine(Mod, "PrefixDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = false
            };
            tl.Replace("{A}", Increase.ToPercent().ToString());
            tl.Replace("{B}", Decrease.ToPercent().ToString());
            yield return tl;
        }

        /// <summary>
        /// 元のSummonerPrefix方式: ローカリゼーションからツールチップテキストを取得
        /// </summary>
    public LocalizedText AdditionalTooltip => Language.GetOrRegister(Mod.GetLocalizationKey($"Prefix{Name}Descr"), null);

        public override void SetStaticDefaults()
        {
            _ = AdditionalTooltip;
        }
    }
}