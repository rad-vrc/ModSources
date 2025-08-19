// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.PlayerChanges.AutofishPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.PlayerChanges;

public class AutofishPlayer : ModPlayer
{
  internal bool Lockcast;
  internal Point CastPosition;
  internal int PullTimer;
  internal bool ActivatedByMod;
  internal bool Autocast;
  internal int AutocastDelay;

  public virtual void PreUpdate()
  {
    if (((Entity) this.Player).whoAmI != Main.myPlayer || !QoLCompendium.QoLCompendium.mainConfig.AutoFishing)
      return;
    this.ActivatedByMod = false;
    if (this.PullTimer > 0)
    {
      --this.PullTimer;
      if (this.PullTimer == 0)
      {
        this.Player.controlUseItem = true;
        this.Player.releaseUseItem = true;
        this.ActivatedByMod = true;
        this.Player.ItemCheck();
      }
    }
    if (!this.Autocast)
      return;
    --this.AutocastDelay;
    if (this.Player.HeldItem.fishingPole == 0)
    {
      this.Autocast = false;
    }
    else
    {
      if (this.AutocastDelay > 0 || AutofishPlayer.CheckBobbersActive(((Entity) this.Player).whoAmI))
        return;
      int mouseX = Main.mouseX;
      int mouseY = Main.mouseY;
      if (this.Lockcast)
      {
        Main.mouseX = this.CastPosition.X - (int) Main.screenPosition.X;
        Main.mouseY = this.CastPosition.Y - (int) Main.screenPosition.Y;
      }
      this.Player.controlUseItem = true;
      this.Player.releaseUseItem = true;
      this.ActivatedByMod = true;
      this.Player.ItemCheck();
      this.AutocastDelay = 10;
      if (!this.Lockcast)
        return;
      Main.mouseX = mouseX;
      Main.mouseY = mouseY;
    }
  }

  public static bool CheckBobbersActive(int whoAmI)
  {
    using (IEnumerator<Projectile> enumerator = ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.owner == whoAmI && p.bobber)).GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return false;
      Projectile current = enumerator.Current;
      return true;
    }
  }

  public virtual void OnEnterWorld()
  {
    this.Lockcast = false;
    this.CastPosition = new Point();
    this.Autocast = false;
  }

  public virtual void Load()
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.AutoFishing)
      return;
    // ISSUE: method pointer
    On_Player.ItemCheck_CheckFishingBobbers += new On_Player.hook_ItemCheck_CheckFishingBobbers((object) this, __methodptr(Player_ItemCheck_CheckFishingBobbers));
    // ISSUE: method pointer
    On_Player.ItemCheck_Shoot += new On_Player.hook_ItemCheck_Shoot((object) this, __methodptr(Player_ItemCheck_Shoot));
    // ISSUE: method pointer
    IL_Projectile.FishingCheck += new ILContext.Manipulator((object) this, __methodptr(Projectile_FishingCheck));
  }

  private bool Player_ItemCheck_CheckFishingBobbers(
    On_Player.orig_ItemCheck_CheckFishingBobbers orig,
    Player player,
    bool canUse)
  {
    int num = orig.Invoke(player, canUse) ? 1 : 0;
    if (num != 0)
      return num != 0;
    if (((Entity) player).whoAmI != Main.myPlayer)
      return num != 0;
    AutofishPlayer autofishPlayer;
    if (!player.TryGetModPlayer<AutofishPlayer>(ref autofishPlayer))
      return num != 0;
    if (autofishPlayer.ActivatedByMod)
      return num != 0;
    autofishPlayer.Autocast = false;
    return num != 0;
  }

  private void Player_ItemCheck_Shoot(
    On_Player.orig_ItemCheck_Shoot orig,
    Player player,
    int i,
    Item sItem,
    int weaponDamage)
  {
    AutofishPlayer autofishPlayer;
    if (((Entity) player).whoAmI == Main.myPlayer && player.TryGetModPlayer<AutofishPlayer>(ref autofishPlayer) && !autofishPlayer.ActivatedByMod && sItem.fishingPole > 0)
      autofishPlayer.Autocast = true;
    orig.Invoke(player, i, sItem, weaponDamage);
  }

  private void Projectile_FishingCheck(ILContext il)
  {
    ILCursor ilCursor1 = new ILCursor(il);
    if (!ilCursor1.TryGotoNext((MoveType) 2, new Func<Instruction, bool>[1]
    {
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.MatchLdfld(i, typeof (FishingAttempt), "rolledItemDrop"))
    }))
      throw new Exception("Hook location not found, if (fisher.rolledItemDrop > 0)");
    ilCursor1.Emit(OpCodes.Ldarg_0);
    ilCursor1.EmitDelegate<Func<int, Projectile, int>>((Func<int, Projectile, int>) ((caughtType, projectile) =>
    {
      if (projectile.owner != Main.myPlayer || !((Entity) Main.player[projectile.owner]).active || Main.player[projectile.owner].dead)
        return caughtType;
      AutofishPlayer modPlayer = Main.player[projectile.owner].GetModPlayer<AutofishPlayer>();
      if (modPlayer.PullTimer != 0 || caughtType <= 0)
        return caughtType;
      modPlayer.PullTimer = 31 /*0x1F*/;
      return caughtType;
    }));
    ILCursor ilCursor2 = new ILCursor(il);
    if (!ilCursor2.TryGotoNext((MoveType) 2, new Func<Instruction, bool>[1]
    {
      (Func<Instruction, bool>) (i => ILPatternMatchingExt.MatchLdfld(i, typeof (FishingAttempt), "rolledEnemySpawn"))
    }))
      throw new Exception("Hook location not found, if (fisher.rolledEnemySpawn > 0)");
    ilCursor2.Emit(OpCodes.Ldarg_0);
    ilCursor2.EmitDelegate<Func<int, Projectile, int>>((Func<int, Projectile, int>) ((caughtType, projectile) =>
    {
      if (projectile.owner != Main.myPlayer || !((Entity) Main.player[projectile.owner]).active || Main.player[projectile.owner].dead)
        return caughtType;
      AutofishPlayer modPlayer = Main.player[projectile.owner].GetModPlayer<AutofishPlayer>();
      if (caughtType > 0 && modPlayer.PullTimer == 0)
        modPlayer.PullTimer = 31 /*0x1F*/;
      return caughtType;
    }));
  }
}
