using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Prefixes
{
    public class Brisk : ModPrefix
    {
    public static float MinionSpeedMult = 1.2f; // Attack speed x1.2

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
        }

        public override void ModifyValue(ref float valueMult) => valueMult *= 1.4f;

        public override void Apply(Item item)
        {
            // Reference: use MinionSpeedMult (consumed by SPGlobalProj)
            item.global().MinionSpeedMult += MinionSpeedMult - 1f;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tl = new TooltipLine(Mod, "PrefixDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = false
            };
            tl.Replace("{}", (MinionSpeedMult - 1f).ToPercent().ToString());
            yield return tl;
        }

        /// <summary>
        /// 元のSummonerPrefix方式: ローカリゼーションからツールチップテキストを取得
        /// キー: "Mods.TranslateTest2.PrefixBriskDescr"
        /// </summary>
    public LocalizedText AdditionalTooltip => Language.GetOrRegister(Mod.GetLocalizationKey($"Prefix{Name}Descr"), null);

        public override void SetStaticDefaults()
        {
            _ = AdditionalTooltip;
        }
    }
}