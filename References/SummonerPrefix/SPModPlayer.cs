// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.SPModPlayer
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using Microsoft.Xna.Framework;
using SummonerPrefix.Prefixes;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace SummonerPrefix;

public class SPModPlayer : ModPlayer
{
  public int HealingCd;

  public bool HealMe(int amount, int Cooldown)
  {
    if (this.HealingCd > 0)
      return false;
    this.HealingCd = Cooldown;
    this.Player.Heal(amount);
    return true;
  }

  public virtual void PostUpdateMiscEffects()
  {
    --this.HealingCd;
    float num1 = 0.0f;
    if (!this.Player.HeldItem.IsAir && this.Player.HeldItem.prefix == ModContent.PrefixType<Focused_Whip>())
    {
      ref StatModifier local = ref this.Player.GetDamage(DamageClass.Summon);
      local = StatModifier.op_Addition(local, Focused_Whip.SummonDmgAddition);
      this.Player.maxMinions -= Focused_Whip.MinionSlotDecrease;
    }
    foreach (Projectile activeProjectile in Main.ActiveProjectiles)
    {
      if ((activeProjectile.minion || activeProjectile.sentry) && activeProjectile.owner == ((Entity) this.Player).whoAmI && activeProjectile.GetGlobalProjectile<SPGlobalProj>().contract)
      {
        Player player = this.Player;
        player.statDefense = Player.DefenseStat.op_Subtraction(player.statDefense, Contract.ArmorDecrease);
      }
      if (activeProjectile.GetGlobalProjectile<SPGlobalProj>().blessing && (double) ((Entity) activeProjectile).Distance(((Entity) this.Player).Center) < (double) Blessing.range)
      {
        num1 += Blessing.Bonus;
        for (float num2 = 0.0f; (double) num2 <= 1.0; num2 += 0.01f)
        {
          Dust dust = Dust.NewDustDirect(Vector2.Lerp(((Entity) this.Player).Center, ((Entity) activeProjectile).Center, num2), 1, 1, 286, 0.0f, 0.0f, 0, new Color(), 1f);
          dust.velocity = Vector2.Zero;
          dust.noGravity = true;
          dust.scale = 0.4f;
          dust.position = Vector2.Lerp(((Entity) this.Player).Center, ((Entity) activeProjectile).Center, num2);
        }
      }
    }
    if ((double) num1 <= 0.0)
      return;
    Player player1 = this.Player;
    player1.statDefense = Player.DefenseStat.op_Addition(player1.statDefense, (int) Math.Ceiling((double) num1 * 3.0));
    this.Player.lifeRegen += (int) Math.Ceiling((double) num1 * 5.0);
    ref StatModifier local1 = ref this.Player.GetDamage(DamageClass.Generic);
    local1 = StatModifier.op_Addition(local1, num1 * 0.02f);
  }
}
