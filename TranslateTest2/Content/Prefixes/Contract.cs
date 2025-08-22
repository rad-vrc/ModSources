using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Content.Prefixes
{
    public class Contract : ModPrefix
    {
    public static int ArmorDecrease = 6;
    public static float LifeSteal = 0.02f;

    public override PrefixCategory Category => (PrefixCategory)3;

    public override float RollChance(Item item) => 0.7f;

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
            // Reference: no direct damage bonus here
        }

    public override void ModifyValue(ref float valueMult) { }

        public override void Apply(Item item)
        {
            item.global().MinionLifeSteal += LifeSteal;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tl = new TooltipLine(Mod, "PrefixDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = false
            };
            tl.Replace("{A}", LifeSteal.ToPercent().ToString());
            tl.Replace("{B}", ArmorDecrease.ToString());
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