// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Prefixes.Devoted
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

public class Devoted : ModPrefix
{
  public static float Increase = 0.15f;
  public static float Decrease = 0.2f;

  public virtual PrefixCategory Category => (PrefixCategory) 3;

  public virtual float RollChance(
  #nullable disable
  Item item) => 1f;

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
  }

  public virtual void ModifyValue(ref float valueMult) => valueMult *= 1.2f;

  public virtual void Apply(Item item)
  {
    item.global().CanGiveTag = true;
    item.global().wTag.SpecialType = nameof (Devoted);
  }

  public virtual IEnumerable<TooltipLine> GetTooltipLines(Item item)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    Devoted devoted = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    TooltipLine tooltipLine = new TooltipLine(((ModType) devoted).Mod, "PrefixDescription", devoted.AdditionalTooltip.Value)
    {
      IsModifier = true,
      IsModifierBad = false
    };
    TooltipLine tl1 = tooltipLine;
    int percent = Devoted.Increase.ToPercent();
    string to1 = percent.ToString();
    tl1.Replace("{A}", to1);
    TooltipLine tl2 = tooltipLine;
    percent = Devoted.Decrease.ToPercent();
    string to2 = percent.ToString();
    tl2.Replace("{B}", to2);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = tooltipLine;
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
