// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.MobileStorages.MobileStorageFollowing
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.MobileStorages;

public class MobileStorageFollowing : GlobalProjectile
{
  public virtual bool InstancePerEntity => true;

  public virtual bool AppliesToEntity(Projectile entity, bool lateInstantiation)
  {
    return entity.type == 525 || entity.type == 734 || entity.type == ModContent.ProjectileType<EtherianConstructProjectile>() || entity.type == ModContent.ProjectileType<FlyingSafeProjectile>();
  }

  public virtual bool PreAI(Projectile projectile)
  {
    if (QoLCompendium.QoLCompendium.mainConfig.MobileStoragesFollowThePlayer)
    {
      Player player = Main.player[projectile.owner];
      float num = Vector2.Distance(((Entity) projectile).Center, ((Entity) player).Center);
      if ((double) num > 3000.0)
        ((Entity) projectile).Center = ((Entity) player).Top;
      else if (Vector2.op_Inequality(((Entity) projectile).Center, ((Entity) player).Center))
      {
        Vector2 vector2 = Vector2.op_Division(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) projectile).DirectionFrom(((Entity) player).Center), 3f), 16f)), ((Entity) projectile).Center), (double) num < 48.0 ? 30f : 60f);
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, vector2);
      }
      if (projectile.timeLeft < 2 && projectile.timeLeft > 0)
        projectile.timeLeft = 2;
    }
    return base.PreAI(projectile);
  }
}
