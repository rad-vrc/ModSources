// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.SummonerPrefix
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using System.Reflection;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace SummonerPrefix;

public class SummonerPrefix : Mod
{
  public virtual void Load()
  {
    // ISSUE: method pointer
    On_Projectile.GetWhipSettings += new On_Projectile.hook_GetWhipSettings((object) this, __methodptr(hook_get_whip_settings));
    // ISSUE: method pointer
    On_Projectile.AI += new On_Projectile.hook_AI((object) this, __methodptr(hook_projectile_ai));
  }

  private void hook_projectile_ai(On_Projectile.orig_AI orig, Projectile self)
  {
    if (self.minion || self.sentry)
    {
      SPGlobalProj globalProjectile = self.GetGlobalProjectile<SPGlobalProj>();
      globalProjectile.UpdateCounter += globalProjectile.MinionUpdateSpeed;
      int num = 0;
      while ((double) globalProjectile.UpdateCounter >= 1.0)
      {
        ++num;
        --globalProjectile.UpdateCounter;
        orig.Invoke(self);
        if ((double) globalProjectile.UpdateCounter >= 1.0)
        {
          MethodInfo method = typeof (Projectile).GetMethod("HandleMovement", (BindingFlags) 36);
          if (MethodInfo.op_Inequality(method, (MethodInfo) null))
          {
            object[] objArray = new object[3]
            {
              (object) ((Entity) self).velocity,
              null,
              null
            };
            ((MethodBase) method).Invoke((object) self, objArray);
          }
        }
      }
    }
    else
      orig.Invoke(self);
  }

  public virtual void Unload()
  {
    // ISSUE: method pointer
    On_Projectile.GetWhipSettings -= new On_Projectile.hook_GetWhipSettings((object) this, __methodptr(hook_get_whip_settings));
    // ISSUE: method pointer
    On_Projectile.AI -= new On_Projectile.hook_AI((object) this, __methodptr(hook_projectile_ai));
  }

  private void hook_get_whip_settings(
    On_Projectile.orig_GetWhipSettings orig,
    Projectile proj,
    out float timeToFlyOut,
    out int segments,
    out float rangeMultiplier)
  {
    orig.Invoke(proj, ref timeToFlyOut, ref segments, ref rangeMultiplier);
    if (!proj.friendly || proj.owner == (int) byte.MaxValue)
      return;
    Player player = Main.player[proj.owner];
    rangeMultiplier *= player.HeldItem.global().WhipRangeMult;
  }
}
