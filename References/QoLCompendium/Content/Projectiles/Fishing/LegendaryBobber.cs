// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Fishing.LegendaryBobber
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QoLCompendium.Content.Items.Tools.Fishing;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.Fishing;

public class LegendaryBobber : ModProjectile
{
  public virtual void SetDefaults()
  {
    ((Entity) this.Projectile).width = 14;
    ((Entity) this.Projectile).height = 14;
    this.Projectile.aiStyle = 61;
    this.Projectile.bobber = true;
    this.Projectile.penetrate = -1;
    this.DrawOriginOffsetY = -8;
  }

  public virtual bool PreDrawExtras()
  {
    int num1 = 45;
    float num2 = 29f;
    Player player = Main.player[this.Projectile.owner];
    if (!this.Projectile.bobber || player.inventory[player.selectedItem].holdStyle <= 0)
      return false;
    Vector2 vector2_1 = player.MountedCenter;
    vector2_1.Y += player.gfxOffY;
    int type = player.inventory[player.selectedItem].type;
    float gravDir = player.gravDir;
    int num3 = ModContent.ItemType<LegendaryCatcher>();
    if (type == num3)
    {
      vector2_1.X += (float) (num1 * ((Entity) player).direction);
      if (((Entity) player).direction < 0)
        vector2_1.X -= 13f;
      vector2_1.Y -= num2 * gravDir;
    }
    if ((double) gravDir == -1.0)
      vector2_1.Y -= 12f;
    vector2_1 = Vector2.op_Subtraction(player.RotatedRelativePoint(Vector2.op_Addition(vector2_1, new Vector2(8f)), true, true), new Vector2(8f));
    Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, vector2_1);
    bool flag = true;
    if ((double) vector2_2.X == 0.0 && (double) vector2_2.Y == 0.0)
      return false;
    float num4 = 12f / ((Vector2) ref vector2_2).Length();
    Vector2 vector2_3 = Vector2.op_Multiply(vector2_2, num4);
    vector2_1 = Vector2.op_Subtraction(vector2_1, vector2_3);
    Vector2 vector2_4 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, vector2_1);
    while (flag)
    {
      float num5 = 12f;
      float f = ((Vector2) ref vector2_4).Length();
      if (!float.IsNaN(f) && !float.IsNaN(f))
      {
        if ((double) f < 20.0)
        {
          num5 = f - 8f;
          flag = false;
        }
        vector2_4 = Vector2.op_Multiply(vector2_4, 12f / f);
        vector2_1 = Vector2.op_Addition(vector2_1, vector2_4);
        vector2_4.X = ((Entity) this.Projectile).position.X + (float) ((Entity) this.Projectile).width * 0.5f - vector2_1.X;
        vector2_4.Y = ((Entity) this.Projectile).position.Y + (float) ((Entity) this.Projectile).height * 0.1f - vector2_1.Y;
        if ((double) f > 12.0)
        {
          float num6 = 0.3f;
          float num7 = Math.Abs(((Entity) this.Projectile).velocity.X) + Math.Abs(((Entity) this.Projectile).velocity.Y);
          if ((double) num7 > 16.0)
            num7 = 16f;
          float num8 = (float) (1.0 - (double) num7 / 16.0);
          float num9 = num6 * num8;
          float num10 = f / 80f;
          if ((double) num10 > 1.0)
            num10 = 1f;
          float num11 = num9 * num10;
          if ((double) num11 < 0.0)
            num11 = 0.0f;
          float num12 = (float) (1.0 - (double) this.Projectile.localAI[0] / 100.0);
          float num13 = num11 * num12;
          if ((double) vector2_4.Y > 0.0)
          {
            vector2_4.Y *= 1f + num13;
            vector2_4.X *= 1f - num13;
          }
          else
          {
            float num14 = Math.Abs(((Entity) this.Projectile).velocity.X) / 3f;
            if ((double) num14 > 1.0)
              num14 = 1f;
            float num15 = num14 - 0.5f;
            float num16 = num13 * num15;
            if ((double) num16 > 0.0)
              num16 *= 2f;
            vector2_4.Y *= 1f + num16;
            vector2_4.X *= 1f - num16;
          }
        }
        float num17 = Utils.ToRotation(vector2_4) - 1.57079637f;
        Main.EntitySpriteDraw(TextureAssets.FishingLine.Value, new Vector2((float) ((double) vector2_1.X - (double) Main.screenPosition.X + (double) Utils.Width(TextureAssets.FishingLine) * 0.5), (float) ((double) vector2_1.Y - (double) Main.screenPosition.Y + (double) Utils.Height(TextureAssets.FishingLine) * 0.5)), new Rectangle?(new Rectangle(0, 0, Utils.Width(TextureAssets.FishingLine), (int) num5)), Color.White, num17, new Vector2((float) Utils.Width(TextureAssets.FishingLine) * 0.5f, 0.0f), 1f, (SpriteEffects) 0, 0.0f);
      }
      else
        break;
    }
    return false;
  }
}
