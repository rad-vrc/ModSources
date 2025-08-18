using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Prefixes
{
    public class Eager : ModPrefix
    {
        public static float MinionSpeedMult = 1.3f;

        public override PrefixCategory Category => (PrefixCategory)3; // Unified category per original mod

        public override float RollChance(Item item) => 0.6f; // Original probability

        public override bool CanRoll(Item item) => item.shoot > 0 && item.isMinionSummonItem();

        public override void SetStats(
            ref float damageMult,
            ref float knockbackMult,
            ref float useTimeMult,
            ref float scaleMult,
            ref float shootSpeedMult,
            ref float manaMult,
            ref int critBonus) { }

        public override void ModifyValue(ref float valueMult) => valueMult *= 1.6f; // Original value multiplier

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
            // Original placeholder pattern
            tl.Replace("{}", (MinionSpeedMult - 1f).ToPercent().ToString());
            yield return tl;
        }

        public LocalizedText AdditionalTooltip
        {
            get
            {
                var key = Mod.GetLocalizationKey($"Prefix{Name}Descr");
                return Language.GetOrRegister(key, (Func<string>)null);
            }
        }

        public override void SetStaticDefaults() => _ = AdditionalTooltip; // Force key registration only
    }
}