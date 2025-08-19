// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Other.BuildIndicatorProjectile
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.Other;

public class BuildIndicatorProjectile : ModProjectile
{
  private Vector2 oldMouse;
  private Item item;
  private bool previouslyLookingLeft;

  public virtual void SetDefaults()
  {
    ((Entity) this.Projectile).width = 16 /*0x10*/;
    ((Entity) this.Projectile).height = 16 /*0x10*/;
    this.Projectile.ignoreWater = true;
    this.Projectile.tileCollide = false;
    this.Projectile.timeLeft = 10;
  }

  public virtual Color? GetAlpha(Color lightColor) => new Color?(new Color(0, 0, 0, 100));

  public virtual void OnSpawn(IEntitySource source)
  {
    this.oldMouse = Main.MouseWorld;
    if (!(source is EntitySource_ItemUse entitySourceItemUse))
      return;
    this.item = entitySourceItemUse.Item;
    this.previouslyLookingLeft = ((Entity) entitySourceItemUse.Player).direction < 0;
  }

  public virtual void AI()
  {
    Player player = Main.player[this.Projectile.owner];
    Vector2 mouseWorld = Main.MouseWorld;
    Vector2 vector2 = Vector2.op_Subtraction(mouseWorld, this.oldMouse);
    Projectile projectile = this.Projectile;
    ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, vector2);
    this.oldMouse = mouseWorld;
    if (this.previouslyLookingLeft && ((Entity) player).direction == 1)
    {
      ((Entity) this.Projectile).position.X += ((Entity) this.Projectile).position.X + this.oldMouse.X;
      this.previouslyLookingLeft = false;
    }
    else if (!this.previouslyLookingLeft && ((Entity) player).direction == -1)
    {
      ((Entity) this.Projectile).position.X += ((Entity) this.Projectile).position.X + this.oldMouse.X;
      this.previouslyLookingLeft = true;
    }
    ++this.Projectile.timeLeft;
    if (player.HeldItem.type != this.item.type)
      this.Projectile.Kill();
    this.Projectile.hide = this.Projectile.owner != Main.myPlayer;
  }
}
