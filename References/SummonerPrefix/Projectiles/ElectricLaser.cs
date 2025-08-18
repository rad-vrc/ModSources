// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Projectiles.ElectricLaser
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace SummonerPrefix.Projectiles;

public class ElectricLaser : ModProjectile
{
  public int frame;
  public int framecounter;

  public Vector2 endPos => new Vector2(this.Projectile.ai[0], this.Projectile.ai[1]);

  public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 1;

  public virtual void SetDefaults()
  {
    this.Projectile.DamageType = DamageClass.Summon;
    ((Entity) this.Projectile).width = 2;
    ((Entity) this.Projectile).height = 2;
    this.Projectile.friendly = true;
    this.Projectile.penetrate = -1;
    this.Projectile.tileCollide = false;
    this.Projectile.light = 0.0f;
    this.Projectile.usesLocalNPCImmunity = true;
    this.Projectile.localNPCHitCooldown = 40;
    this.Projectile.ArmorPenetration = 256 /*0x0100*/;
    this.Projectile.timeLeft = 16 /*0x10*/;
  }

  public virtual bool ShouldUpdatePosition() => false;

  public virtual void AI()
  {
    ++this.framecounter;
    if (this.framecounter % 2 != 0)
      return;
    ++this.frame;
  }

  public virtual bool? CanHitNPC(NPC target)
  {
    return this.Projectile.timeLeft < 10 ? new bool?(false) : base.CanHitNPC(target);
  }

  public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
  {
    return new bool?(Utils.LineThroughRect(((Entity) this.Projectile).Center, this.endPos, targetHitbox, 6));
  }

  public virtual bool PreDraw(ref Color lightColor)
  {
    Main.spriteBatch.UseBlendState(BlendState.AlphaBlend, SamplerState.LinearWrap);
    Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
    Rectangle rectangle;
    // ISSUE: explicit constructor call
    ((Rectangle) ref rectangle).\u002Ector(64 /*0x40*/ * this.frame, 0, 64 /*0x40*/, (int) Utils.getDistance(((Entity) this.Projectile).Center, this.endPos));
    Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition);
    Rectangle? nullable = new Rectangle?(rectangle);
    Color color = Color.op_Multiply(Color.White, 0.6f);
    double num = (double) Utils.ToRotation(Vector2.op_Subtraction(this.endPos, ((Entity) this.Projectile).Center)) - 1.5707963705062866;
    Vector2 vector2_2 = new Vector2(32f, 0.0f);
    Main.EntitySpriteDraw(texture2D, vector2_1, nullable, color, (float) num, vector2_2, 1f, (SpriteEffects) 0, 0.0f);
    Main.spriteBatch.UseBlendState(BlendState.AlphaBlend);
    return false;
  }
}
