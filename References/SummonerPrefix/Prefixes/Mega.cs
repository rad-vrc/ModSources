// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Prefixes.Mega
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

public class Mega : ModPrefix
{
  public static float MinionSlotMult = 2f;
  public static float MinionDmgAddition = 1.2f;

  public virtual PrefixCategory Category => (PrefixCategory) 3;

  public virtual float RollChance(
  #nullable disable
  Item item) => 1f;

  public virtual bool CanRoll(Item item) => item.shoot > 0 && item.isMinionSummonItem(false);

  public virtual void SetStats(
    ref float damageMult,
    ref float knockbackMult,
    ref float useTimeMult,
    ref float scaleMult,
    ref float shootSpeedMult,
    ref float manaMult,
    ref int critBonus)
  {
    damageMult = 1f + Mega.MinionDmgAddition;
  }

  public virtual void ModifyValue(ref float valueMult) => valueMult *= 1.4f;

  public virtual void Apply(Item item)
  {
    item.global().MinionSlotMult = Mega.MinionSlotMult;
    ++item.global().MinionScaleMult;
    ++item.global().MinionKnockbackMult;
  }

  public virtual IEnumerable<TooltipLine> GetTooltipLines(Item item)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    Mega mega = this;
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
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = new TooltipLine(((ModType) mega).Mod, "PrefixDescription", mega.AdditionalTooltip.Value)
    {
      IsModifier = true,
      IsModifierBad = false
    };
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
