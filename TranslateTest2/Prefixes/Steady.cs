using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Prefixes
{
    public class Steady : ModPrefix
    {
        public static float MinionSpeedMult = 0.5f;

        public override PrefixCategory Category => (PrefixCategory)3;

        public override float RollChance(Item item) => 0.8f;

        public override bool CanRoll(Item item) => item.shoot > 0 && item.isMinionSummonItem();

        public override void SetStats(
            ref float damageMult,
            ref float knockbackMult,
            ref float useTimeMult,
            ref float scaleMult,
            ref float shootSpeedMult,
            ref float manaMult,
            ref int critBonus)
        {
            damageMult = 2.3f;
        }

        public override void ModifyValue(ref float valueMult) { }

        public override void Apply(Item item)
        {
            item.global().MinionSpeedMult += MinionSpeedMult - 1f;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tl = new TooltipLine(Mod, "PrefixDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = false
            };
            tl.Replace("{}", (1f - MinionSpeedMult).ToPercent().ToString());
            yield return tl;
        }

        public LocalizedText AdditionalTooltip
        {
            get
            {
                var key = Mod.GetLocalizationKey($"Prefix{Name}Descr");
                return Language.GetOrRegister(key, null);
            }
        }

        public override void SetStaticDefaults()
        {
            _ = AdditionalTooltip;
        }
    }
}