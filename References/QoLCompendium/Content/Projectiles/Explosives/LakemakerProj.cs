// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Explosives.LakemakerProj
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

public class LakemakerProj : ModProjectile
{
  public virtual string Texture => "QoLCompendium/Content/Items/Tools/Explosives/Lakemaker";

  public virtual void SetDefaults()
  {
    ((Entity) this.Projectile).width = 19;
    ((Entity) this.Projectile).height = 31 /*0x1F*/;
    this.Projectile.aiStyle = 16 /*0x10*/;
    this.Projectile.friendly = true;
    this.Projectile.penetrate = -1;
    this.Projectile.timeLeft = 170;
  }

  public virtual bool? CanDamage() => new bool?(false);

  public virtual bool TileCollideStyle(
    ref int width,
    ref int height,
    ref bool fallThrough,
    ref Vector2 hitboxCenterFrac)
  {
    fallThrough = false;
    return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
  }

  public virtual bool OnTileCollide(Vector2 oldVelocity)
  {
    this.Projectile.Kill();
    return true;
  }

  public virtual void OnKill(int timeLeft)
  {
    SoundEngine.PlaySound(ref SoundID.Item15, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    if (Main.netMode == 1)
      return;
    Vector2 center = ((Entity) this.Projectile).Center;
    int num1 = 50;
    int num2 = 50;
    for (int index1 = -num1 / 2; index1 <= num1 / 2; ++index1)
    {
      for (int index2 = 0; index2 <= num2; ++index2)
      {
        int x = (int) ((double) index1 + (double) center.X / 16.0);
        int y = (int) ((double) index2 + (double) center.Y / 16.0);
        if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
        {
          Tile tile = ((Tilemap) ref Main.tile)[x, y];
          if (!Tile.op_Equality(tile, (ArgumentException) null) && CheckDestruction.OkayToDestroyTileAt(x, y) && !CheckDestruction.TileIsLiterallyAir(tile))
          {
            Destruction.ClearTileAndLiquid(x, y);
            if (index2 == num2 || Math.Abs(index1) == num1 / 2)
              WorldGen.PlaceTile(x, y, 38, false, false, -1, 0);
            else
              WorldGen.PlaceLiquid(x, y, (byte) 0, byte.MaxValue);
          }
        }
      }
    }
    Main.refreshMap = true;
  }
}
