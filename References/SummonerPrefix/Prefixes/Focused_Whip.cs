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

#nullable enable
namespace SummonerPrefix.Prefixes;

public class Focused_Whip : ModPrefix
{
  public static float SummonDmgAddition = 0.1f;
  public static int MinionSlotDecrease = 1;

  public virtual PrefixCategory Category => (PrefixCategory) 3;

  public virtual float RollChance(
  #nullable disable
  Item item) => 0.8f;

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

  public virtual void ModifyValue(ref float valueMult)
  {
  }

  public virtual void Apply(Item item)
  {
  }

  public virtual IEnumerable<TooltipLine> GetTooltipLines(Item item)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    Focused_Whip focusedWhip = this;
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
    TooltipLine tl = new TooltipLine(((ModType) focusedWhip).Mod, "PrefixDescription", focusedWhip.AdditionalTooltip.Value)
    {
      IsModifier = true,
      IsModifierBad = false
    };
    tl.Replace("{A}", Focused_Whip.SummonDmgAddition.ToPercent().ToString());
    tl.Replace("{B}", Focused_Whip.MinionSlotDecrease.ToString());
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
