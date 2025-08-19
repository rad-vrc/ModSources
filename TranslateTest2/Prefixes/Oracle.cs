using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Prefixes
{
    public class Oracle : ModPrefix
    {
        public static float TagCrit = 0.08f;
        public static float TagDmg = 0.07f;
        public static float RangeMult = 1.35f;

        public override PrefixCategory Category => (PrefixCategory)3; // Unified category

        public override float RollChance(Item item) => 0.35f;

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
            damageMult = 1.2f;
            useTimeMult *= 0.8f;
        }

        public override void ModifyValue(ref float valueMult) => valueMult *= 3f;

        public override void Apply(Item item)
        {
            var g = item.global();
            g.CanGiveTag = true;
            g.wTag.CritAdd += TagCrit;
            g.wTag.TagDamage = (int)Math.Ceiling(item.damage * TagDmg);
            g.WhipRangeMult += RangeMult - 1f;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tl = new TooltipLine(Mod, "PrefixDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = false
            };
            tl.Replace("{A}", TagDmg.ToPercent().ToString());
            tl.Replace("{B}", Math.Ceiling(item.damage * TagDmg).ToString());
            tl.Replace("{C}", TagCrit.ToPercent().ToString());
            tl.Replace("{D}", (RangeMult - 1f).ToPercent().ToString());
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