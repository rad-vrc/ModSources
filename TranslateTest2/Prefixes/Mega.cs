using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Prefixes
{
    public class Mega : ModPrefix
    {
        public static float MinionSlotMult = 2f;
        public static float MinionDmgAddition = 1.2f;

        public override PrefixCategory Category => (PrefixCategory)3; // Unified category

        public override float RollChance(Item item) => 1f;

        public override bool CanRoll(Item item) => item.shoot > 0 && item.isMinionSummonItem(false);

        public override void SetStats(
            ref float damageMult,
            ref float knockbackMult,
            ref float useTimeMult,
            ref float scaleMult,
            ref float shootSpeedMult,
            ref float manaMult,
            ref int critBonus)
        {
            damageMult = 1f + MinionDmgAddition; // Original adds MinionDmgAddition
        }

        public override void ModifyValue(ref float valueMult) => valueMult *= 1.4f;

        public override void Apply(Item item)
        {
            item.global().MinionSlotMult = MinionSlotMult;
            item.global().MinionScaleMult += 1f;
            item.global().MinionKnockbackMult += 1f;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tl = new TooltipLine(Mod, "PrefixDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = false
            };
            yield return tl; // No runtime replacements in original
        }

        public LocalizedText AdditionalTooltip
        {
            get
            {
                var key = Mod.GetLocalizationKey($"Prefix{Name}Descr");
                return Language.GetOrRegister(key, (Func<string>)null);
            }
        }

        public override void SetStaticDefaults() => _ = AdditionalTooltip;
    }
}