// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Prefixes.Focused_Whip
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TranslateTest2.Prefixes
{
    public class Focused_Whip : ModPrefix
    {
        public static float SummonDmgAddition = 0.1f;
        public static int MinionSlotDecrease = 1;

        public override PrefixCategory Category => (PrefixCategory)3; // Unified category

        public override float RollChance(Item item) => 0.8f; // Original value

        public override bool CanRoll(Item item) => item.shoot > 0 && ProjectileID.Sets.IsAWhip[item.shoot];

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

    public override void ModifyValue(ref float valueMult) { }

        public override void Apply(Item item)
        {
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            var tl = new TooltipLine(Mod, "PrefixDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = false
            };
            tl.Replace("{A}", SummonDmgAddition.ToPercent().ToString());
            tl.Replace("{B}", MinionSlotDecrease.ToString());
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