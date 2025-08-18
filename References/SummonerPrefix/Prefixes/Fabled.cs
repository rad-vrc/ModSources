// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Prefixes.Fabled
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

public class Fabled : ModPrefix
{
  public static float MinionSlotMult = 0.8f;
  public static float Crit = 0.05f;
  public static float Kb = 1.6f;

  public virtual PrefixCategory Category => (PrefixCategory) 3;

  public virtual float RollChance(
  #nullable disable
  Item item) => 0.35f;

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

  public virtual void ModifyValue(ref float valueMult) => valueMult *= 3f;

  public virtual void Apply(Item item) => item.global().MinionSlotMult = Fabled.MinionSlotMult;

  public virtual IEnumerable<TooltipLine> GetTooltipLines(Item item)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    Fabled fabled = this;
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
    TooltipLine tl = new TooltipLine(((ModType) fabled).Mod, "PrefixDescription", fabled.AdditionalTooltip.Value)
    {
      IsModifier = true,
      IsModifierBad = false
    };
    tl.Replace("{A}", (1f - Fabled.MinionSlotMult).ToPercent().ToString());
    Projectile projectile = new Projectile();
    projectile.SetDefaults(item.shoot);
    tl.Replace("{B}", Math.Round((double) projectile.minionSlots * (1.0 - (double) Fabled.MinionSlotMult), 2).ToString());
    tl.Replace("{C}", Fabled.Crit.ToPercent().ToString());
    tl.Replace("{D}", (Fabled.Kb - 1f).ToPercent().ToString());
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
