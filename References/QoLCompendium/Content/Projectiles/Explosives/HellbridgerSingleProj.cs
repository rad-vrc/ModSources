// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Explosives.HellbridgerSingleProj
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

public class HellbridgerSingleProj : ModProjectile
{
  public virtual string Texture => "QoLCompendium/Content/Items/Tools/Explosives/Hellbridger";

  public virtual void SetDefaults()
  {
    ((Entity) this.Projectile).width = 37;
    ((Entity) this.Projectile).height = 19;
    this.Projectile.aiStyle = 16 /*0x10*/;
    this.Projectile.friendly = true;
    this.Projectile.penetrate = -1;
    this.Projectile.timeLeft = 1;
  }

  public virtual bool? CanDamage() => new bool?(false);

  public virtual void OnKill(int timeLeft)
  {
    Vector2 center = ((Entity) this.Projectile).Center;
    SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(center), (SoundUpdateCallback) null);
    if (Main.netMode == 1)
      return;
    bool flag1 = false;
    bool flag2 = false;
    for (int index1 = Main.maxTilesX / 2; index1 < Main.maxTilesX && !flag1; ++index1)
    {
      for (int index2 = -30; index2 <= 0; ++index2)
      {
        int x = index1;
        int y = (int) ((double) index2 + (double) center.Y / 16.0);
        if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
        {
          Tile tile = ((Tilemap) ref Main.tile)[x, y];
          if (!Tile.op_Equality(tile, (ArgumentException) null))
          {
            if (!CheckDestruction.OkayToDestroyTile(tile, true))
              flag1 = true;
            if (index2 == 0)
            {
              Destruction.ClearEverything(x, y, false);
              WorldGen.PlaceTile(x, y, 19, false, false, -1, 13);
              if (Main.netMode == 2)
                NetMessage.SendTileSquare(-1, x, y, 1, (TileChangeType) 0);
            }
            else if (!CheckDestruction.TileIsLiterallyAir(tile))
              Destruction.ClearEverything(x, y);
          }
        }
      }
    }
    for (int index3 = Main.maxTilesX / 2; index3 > 0 && !flag2; --index3)
    {
      for (int index4 = -30; index4 <= 0; ++index4)
      {
        int x = index3;
        int y = (int) ((double) index4 + (double) center.Y / 16.0);
        if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
        {
          Tile tile = ((Tilemap) ref Main.tile)[x, y];
          if (!Tile.op_Equality(tile, (ArgumentException) null))
          {
            if (!CheckDestruction.OkayToDestroyTile(tile, true))
              flag2 = true;
            if (index4 == 0)
            {
              Destruction.ClearEverything(x, y, false);
              WorldGen.PlaceTile(x, y, 19, false, false, -1, 13);
              if (Main.netMode == 2)
                NetMessage.SendTileSquare(-1, x, y, 1, (TileChangeType) 0);
            }
            else if (!CheckDestruction.TileIsLiterallyAir(tile))
              Destruction.ClearEverything(x, y);
          }
        }
      }
    }
  }
}
