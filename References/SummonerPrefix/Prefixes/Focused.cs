// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Prefixes.Focused
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

public class Focused : ModPrefix
{
  public static float MinionSlotAndDmgMult = 1.4f;

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
    damageMult = Focused.MinionSlotAndDmgMult + 0.4f;
  }

  public virtual void ModifyValue(ref float valueMult) => valueMult *= 1.25f;

  public virtual void Apply(Item item)
  {
    item.global().MinionSlotMult = Focused.MinionSlotAndDmgMult;
  }

  public virtual IEnumerable<TooltipLine> GetTooltipLines(Item item)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    Focused focused = this;
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
    TooltipLine tl = new TooltipLine(((ModType) focused).Mod, "PrefixDescription", focused.AdditionalTooltip.Value)
    {
      IsModifier = true,
      IsModifierBad = false
    };
    tl.Replace("{A}", (Focused.MinionSlotAndDmgMult - 1f).ToPercent().ToString());
    Projectile projectile = new Projectile();
    projectile.SetDefaults(item.shoot);
    tl.Replace("{B}", Math.Round((double) projectile.minionSlots * ((double) Focused.MinionSlotAndDmgMult - 1.0), 2).ToString());
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
