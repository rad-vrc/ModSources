// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Other.LifeformLocator
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.Other;

internal class LifeformLocator : ModProjectile
{
  public virtual void SetDefaults()
  {
    ((Entity) this.Projectile).width = 22;
    ((Entity) this.Projectile).height = 28;
    this.Projectile.light = 0.75f;
    this.Projectile.friendly = false;
    this.Projectile.hostile = false;
    this.Projectile.timeLeft = 61;
    this.Projectile.penetrate = -1;
    this.Projectile.tileCollide = false;
  }

  public virtual void AI()
  {
    int index = (int) this.Projectile.ai[0];
    Vector2 vector2_1;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_1).\u002Ector((float) (int) ((Entity) Main.npc[index]).Center.X, (float) (int) ((Entity) Main.npc[index]).Center.Y);
    Player player = Main.player[this.Projectile.owner];
    if (this.Projectile.owner == Main.myPlayer)
    {
      Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) player).Center);
      ((Vector2) ref vector2_2).Normalize();
      ((Entity) this.Projectile).velocity = vector2_2;
      ((Entity) this.Projectile).direction = (double) vector2_1.X > (double) ((Entity) player).Center.X ? 1 : -1;
      this.Projectile.netUpdate = true;
    }
    ((Entity) this.Projectile).position = Vector2.op_Addition(((Entity) player).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 45f));
    this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + MathHelper.ToRadians(90f);
  }
}
