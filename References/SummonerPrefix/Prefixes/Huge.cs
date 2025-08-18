// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Prefixes.Huge
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable enable
namespace SummonerPrefix.Prefixes;

public class Huge : ModPrefix
{
  public static float MinionScaleMult = 0.4f;

  public virtual PrefixCategory Category => (PrefixCategory) 3;

  public virtual float RollChance(
  #nullable disable
  Item item) => 1f;

  public virtual bool CanRoll(Item item) => item.shoot > 0 && item.isMinionSummonItem();

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
  }

  public virtual void ModifyValue(ref float valueMult) => valueMult *= 1.2f;

  public virtual void Apply(Item item) => item.global().MinionScaleMult += Huge.MinionScaleMult;

  public virtual IEnumerable<TooltipLine> GetTooltipLines(Item item)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    Huge huge = this;
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
    TooltipLine tl = new TooltipLine(((ModType) huge).Mod, "PrefixDescription", huge.AdditionalTooltip.Value)
    {
      IsModifier = true,
      IsModifierBad = false
    };
    tl.Replace("{}", Huge.MinionScaleMult.ToPercent().ToString());
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = tl;
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
