using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Prefixes
{
    public class Loyal : ModPrefix
    {
        public static float MinionSlotAndDmgMult = 3f;
        public static float Crit = 0.05f;

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
            damageMult = MinionSlotAndDmgMult;
        }

        public override void ModifyValue(ref float valueMult) => valueMult *= 1.6f;

        public override void Apply(Item item) => item.global().MinionSlotMult = MinionSlotAndDmgMult;

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tl = new TooltipLine(Mod, "PrefixDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = false
            };
            tl.Replace("{A}", (MinionSlotAndDmgMult - 1f).ToPercent().ToString());
            var proj = new Projectile();
            proj.SetDefaults(item.shoot);
            tl.Replace("{B}", Math.Round(proj.minionSlots * (MinionSlotAndDmgMult - 1f), 2).ToString());
            tl.Replace("{C}", Crit.ToPercent().ToString());
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