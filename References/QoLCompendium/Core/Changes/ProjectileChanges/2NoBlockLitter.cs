// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ProjectileChanges.NoSandGunLitter
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ProjectileChanges;

public class NoSandGunLitter : GlobalProjectile
{
  private static readonly Dictionary<int, int> _sandGunProjectileToItem = new Dictionary<int, int>()
  {
    {
      42,
      169
    },
    {
      65,
      370
    },
    {
      68,
      408
    },
    {
      354,
      1246
    }
  };

  public virtual bool AppliesToEntity(Projectile entity, bool lateInstantiation)
  {
    return NoSandGunLitter._sandGunProjectileToItem.ContainsKey(entity.type);
  }

  public virtual bool PreKill(Projectile projectile, int timeLeft)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.NoLittering)
      return base.PreKill(projectile, timeLeft);
    projectile.noDropItem = true;
    int index = Item.NewItem(((Entity) projectile).GetSource_DropAsItem((string) null), ((Entity) projectile).Hitbox, NoSandGunLitter._sandGunProjectileToItem[projectile.type], 1, false, 0, false, false);
    Main.item[index].noGrabDelay = 0;
    if (Main.netMode == 2)
      NetMessage.SendData(21, -1, -1, (NetworkText) null, index, 1f, 0.0f, 0.0f, 0, 0, 0);
    return base.PreKill(projectile, timeLeft);
  }
}
