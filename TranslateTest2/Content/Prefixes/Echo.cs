// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Prefixes.Echo
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
    public class Echo : ModPrefix
    {
        public static float DoubleChance = 0.25f; // Used by projectile logic, not shown here

        public override PrefixCategory Category => (PrefixCategory)3; // Original unified category

        public override float RollChance(Item item) => 0.6f;

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
            // Original had no runtime placeholder replacement (value baked in localization)
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