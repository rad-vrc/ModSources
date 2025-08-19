// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ProjectileChanges.ExtraLures
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ProjectileChanges;

public class ExtraLures : GlobalProjectile
{
  public virtual void OnSpawn(Projectile projectile, IEntitySource source)
  {
    if (!projectile.bobber || projectile.owner != Main.myPlayer || QoLCompendium.QoLCompendium.mainConfig.ExtraLures <= 1 || !(source is EntitySource_ItemUse))
      return;
    int number = QoLCompendium.QoLCompendium.mainConfig.ExtraLures + 1;
    if (Main.player[projectile.owner].HasBuff(121))
      ++number;
    if (number <= 1)
      return;
    ExtraLures.SplitProj(projectile, number);
  }

  public static void SplitProj(Projectile projectile, int number)
  {
    double num1 = 0.2 / (double) number;
    for (int index1 = 0; index1 < number / 2; ++index1)
    {
      for (int index2 = 0; index2 < 2; ++index2)
      {
        int num2 = index2 == 0 ? 1 : -1;
        Projectile projectile1 = ExtraLures.NewProjectileDirectSafe(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, Utils.RotatedBy(((Entity) projectile).velocity, (double) num2 * num1 * (double) (index1 + 1), new Vector2()), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1]);
        if (projectile1 != null)
          projectile1.friendly = true;
      }
    }
    if (number % 2 != 0)
      return;
    ((Entity) projectile).active = false;
  }

  public static Projectile NewProjectileDirectSafe(
    IEntitySource source,
    Vector2 pos,
    Vector2 vel,
    int type,
    int damage,
    float knockback,
    int owner = 255 /*0xFF*/,
    float ai0 = 0.0f,
    float ai1 = 0.0f)
  {
    int index = Projectile.NewProjectile(source, pos, vel, type, damage, knockback, owner, ai0, ai1, 0.0f);
    return index >= Main.maxProjectiles ? (Projectile) null : Main.projectile[index];
  }
}
