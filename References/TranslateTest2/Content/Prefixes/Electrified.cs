// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Prefixes.Electrified
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Content.Prefixes
{
    public class Electrified : ModPrefix
    {
        public static float DmgMultByPlayerMaxHealth = 0.08f;

        public override PrefixCategory Category => (PrefixCategory)3; // Unified category

        public override float RollChance(Item item) => 0.4f;

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

        public override void Apply(Item item) => item.global().electrified = true;

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tl = new TooltipLine(Mod, "PrefixDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = false
            };
            yield return tl; // Original had no dynamic replacement
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