using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Content.Prefixes
{
    public class Huge : ModPrefix
    {
        public static float MinionScaleMult = 0.4f;

        public override PrefixCategory Category => (PrefixCategory)3; // Unified original category

        public override float RollChance(Item item) => 1f;

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
            damageMult = 1.2f;
        }

        public override void ModifyValue(ref float valueMult) => valueMult *= 1.2f;

        public override void Apply(Item item) => item.global().MinionScaleMult += MinionScaleMult;

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tl = new TooltipLine(Mod, "PrefixDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = false
            };
            tl.Replace("{}", MinionScaleMult.ToPercent().ToString());
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

        public override void SetStaticDefaults() => _ = AdditionalTooltip;
    }
}