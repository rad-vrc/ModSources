using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Content.Prefixes
{
    public class Affable : ModPrefix
    {
        public static float MinionSlotMult = 0.8f;

    public override PrefixCategory Category => (PrefixCategory)3;

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
        }

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.2f;

    public override void Apply(Item item) => item.global().MinionSlotMult = MinionSlotMult;

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tl = new TooltipLine(Mod, "PrefixDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = false
            };
            tl.Replace("{A}", (1f - MinionSlotMult).ToPercent().ToString());
            Projectile projectile = new Projectile();
            projectile.SetDefaults(item.shoot);
            tl.Replace("{B}", Math.Round(projectile.minionSlots * (1 - MinionSlotMult), 2).ToString());
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