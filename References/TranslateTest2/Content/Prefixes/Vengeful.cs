using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Content.Prefixes
{
    public class Vengeful : ModPrefix
    {
        public static float ExtraTagDamage = 0.03f;

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
        { }

        public override void ModifyValue(ref float valueMult) => valueMult *= 1.2f;

        public override void Apply(Item item)
        {
            int num = (int)Math.Ceiling(item.damage * ExtraTagDamage);
            if (num <= 0)
                return;
            item.global().CanGiveTag = true;
            item.global().wTag.TagDamage += num;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tl = new TooltipLine(Mod, "PrefixDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = false
            };
            tl.Replace("{A}", ExtraTagDamage.ToPercent().ToString());
            tl.Replace("{B}", ((int)Math.Ceiling(item.damage * ExtraTagDamage)).ToString());
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