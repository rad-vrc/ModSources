// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Explosives.SuperbomberProj
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.Explosives;

public class SuperbomberProj : ModProjectile
{
  public virtual string Texture => "QoLCompendium/Content/Items/Tools/Explosives/Superbomber";

  public virtual void SetDefaults()
  {
    ((Entity) this.Projectile).width = 29;
    ((Entity) this.Projectile).height = 29;
    this.Projectile.aiStyle = 16 /*0x10*/;
    this.Projectile.friendly = true;
    this.Projectile.penetrate = -1;
    this.Projectile.timeLeft = 300;
    this.DrawOffsetX = -13;
    this.DrawOriginOffsetY = -20;
  }

  public virtual bool? CanDamage() => new bool?(false);

  public virtual bool OnTileCollide(Vector2 oldVelocity)
  {
    ((Entity) this.Projectile).velocity.X = 0.0f;
    return base.OnTileCollide(oldVelocity);
  }

  public virtual void OnKill(int timeLeft)
  {
    SoundEngine.PlaySound(ref SoundID.Item15, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    if (Main.netMode == 1)
      return;
    Vector2 center = ((Entity) this.Projectile).Center;
    int num = 64 /*0x40*/;
    for (int index1 = -num; index1 <= num; ++index1)
    {
      for (int index2 = -num * 2; index2 <= 0; ++index2)
      {
        int x = (int) ((double) index1 + (double) center.X / 16.0);
        int y = (int) ((double) index2 + (double) center.Y / 16.0);
        if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
        {
          Tile tile = ((Tilemap) ref Main.tile)[x, y];
          if (!Tile.op_Equality(tile, (ArgumentException) null) && CheckDestruction.OkayToDestroyTile(tile) && !CheckDestruction.TileIsLiterallyAir(tile))
            Destruction.ClearTileAndLiquid(x, y);
        }
      }
    }
    Main.refreshMap = true;
  }
}
