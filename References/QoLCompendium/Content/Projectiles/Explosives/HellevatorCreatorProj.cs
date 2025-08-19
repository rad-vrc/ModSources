// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Explosives.HellevatorCreatorProj
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

public class HellevatorCreatorProj : ModProjectile
{
  public virtual string Texture => "QoLCompendium/Content/Items/Tools/Explosives/HellevatorCreator";

  public virtual void SetDefaults()
  {
    ((Entity) this.Projectile).width = 13;
    ((Entity) this.Projectile).height = 31 /*0x1F*/;
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
    for (int index = -3; index <= 3; ++index)
    {
      for (int y = (int) (1.0 + (double) center.Y / 16.0); y <= Main.maxTilesY - 40; ++y)
      {
        int x = (int) ((double) index + (double) center.X / 16.0);
        if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
        {
          Tile tile = ((Tilemap) ref Main.tile)[x, y];
          if (!Tile.op_Equality(tile, (ArgumentException) null) && CheckDestruction.OkayToDestroyTile(tile))
          {
            Destruction.ClearEverything(x, y, false);
            switch (index)
            {
              case -3:
              case 3:
                WorldGen.PlaceTile(x, y, 38, false, false, -1, 0);
                break;
              case -2:
              case -1:
              case 1:
              case 2:
                WorldGen.PlaceWall(x, y, 5, false);
                break;
              case 0:
                WorldGen.PlaceTile(x, y, 213, false, false, -1, 0);
                WorldGen.PlaceWall(x, y, 155, false);
                break;
            }
            NetMessage.SendTileSquare(-1, x, y, 1, 0, (TileChangeType) 0);
          }
        }
      }
    }
  }
}
