// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Prefixes.Oracle
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable enable
namespace SummonerPrefix.Prefixes;

public class Oracle : ModPrefix
{
  public static float TagCrit = 0.08f;
  public static float TagDmg = 0.07f;
  public static float RangeMult = 1.35f;

  public virtual PrefixCategory Category => (PrefixCategory) 3;

  public virtual float RollChance(
  #nullable disable
  Item item) => 0.35f;

  public virtual bool CanRoll(Item item) => item.shoot > 0 && ProjectileID.Sets.IsAWhip[item.shoot];

  public virtual void SetStats(
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

  public virtual void ModifyValue(ref float valueMult) => valueMult *= 3f;

  public virtual void Apply(Item item)
  {
    item.global().CanGiveTag = true;
    item.global().wTag.CritAdd += Oracle.TagCrit;
    item.global().wTag.TagDamage = (int) Math.Ceiling((double) item.damage * (double) Oracle.TagDmg);
    item.global().WhipRangeMult += Oracle.RangeMult - 1f;
  }

  public virtual IEnumerable<TooltipLine> GetTooltipLines(Item item)
  {
    // ISSUE: reference to a compiler-generated field
    int num1 = this.\u003C\u003E1__state;
    Oracle oracle = this;
    if (num1 != 0)
    {
      if (num1 != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    TooltipLine tl1 = new TooltipLine(((ModType) oracle).Mod, "PrefixDescription", oracle.AdditionalTooltip.Value)
    {
      IsModifier = true,
      IsModifierBad = false
    };
    tl1.Replace("{A}", Oracle.TagDmg.ToPercent().ToString());
    TooltipLine tl2 = tl1;
    int num2 = (int) Math.Ceiling((double) item.damage * (double) Oracle.TagDmg);
    string to1 = num2.ToString();
    tl2.Replace("{B}", to1);
    TooltipLine tl3 = tl1;
    num2 = Oracle.TagCrit.ToPercent();
    string to2 = num2.ToString();
    tl3.Replace("{C}", to2);
    TooltipLine tl4 = tl1;
    num2 = (Oracle.RangeMult - 1f).ToPercent();
    string to3 = num2.ToString();
    tl4.Replace("{D}", to3);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = tl1;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  public LocalizedText AdditionalTooltip
  {
    get
    {
      return Language.GetOrRegister(((ModType) this).Mod.GetLocalizationKey($"Prefix{((ModType) this).Name}Descr"), (Func<string>) null);
    }
  }

  public virtual void SetStaticDefaults()
  {
    LocalizedText additionalTooltip = this.AdditionalTooltip;
  }
}
