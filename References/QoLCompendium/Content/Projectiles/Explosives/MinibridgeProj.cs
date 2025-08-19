// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Explosives.MinibridgeProj
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.Explosives;

public class MinibridgeProj : ModProjectile
{
  public virtual string Texture => "QoLCompendium/Content/Items/Tools/Explosives/Minibridge";

  public virtual void SetDefaults()
  {
    ((Entity) this.Projectile).width = 20;
    ((Entity) this.Projectile).height = 20;
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
    bool flag = (double) ((Entity) this.Projectile).Center.X < (double) ((Entity) Main.player[this.Projectile.owner]).Center.X;
    int[] source = new int[5]
    {
      80 /*0x50*/,
      5,
      32 /*0x20*/,
      352,
      69
    };
    Tile tile1 = ((Tilemap) ref Main.tile)[(int) ((double) center.X / 16.0), (int) ((double) center.Y / 16.0)];
    if (!Tile.op_Equality(tile1, (ArgumentException) null))
    {
      if (((IEnumerable<int>) source).Contains<int>((int) ((Tile) ref tile1).TileType))
        Destruction.ClearEverything((int) ((double) center.X / 16.0), (int) ((double) center.Y / 16.0));
      WorldGen.PlaceTile((int) ((double) center.X / 16.0), (int) ((double) center.Y / 16.0), 19, false, false, -1, 0);
      NetMessage.SendTileSquare(-1, (int) ((double) center.X / 16.0), (int) ((double) center.Y / 16.0), 1, (TileChangeType) 0);
    }
    int num1 = flag ? -100 : 0;
    int num2 = !flag ? 100 : 0;
    for (int index = num1; index < num2; ++index)
    {
      int x = (int) ((double) index + (double) center.X / 16.0);
      int y = (int) ((double) center.Y / 16.0);
      if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
      {
        Tile tile2 = ((Tilemap) ref Main.tile)[x, y];
        if (!Tile.op_Equality(tile2, (ArgumentException) null))
        {
          if (((IEnumerable<int>) source).Contains<int>((int) ((Tile) ref tile2).TileType))
            Destruction.ClearEverything(x, y);
          WorldGen.PlaceTile(x, y, 19, false, false, -1, 0);
          NetMessage.SendTileSquare(-1, x, y, 1, (TileChangeType) 0);
        }
      }
    }
    int num3 = flag ? 100 : 0;
    int num4 = !flag ? -100 : 0;
    for (int index = num3; index > num4; --index)
    {
      int x = (int) ((double) index + (double) center.X / 16.0);
      int y = (int) ((double) center.Y / 16.0);
      if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
      {
        Tile tile3 = ((Tilemap) ref Main.tile)[x, y];
        if (!Tile.op_Equality(tile3, (ArgumentException) null))
        {
          if (((IEnumerable<int>) source).Contains<int>((int) ((Tile) ref tile3).TileType))
            Destruction.ClearEverything(x, y);
          WorldGen.PlaceTile(x, y, 19, false, false, -1, 0);
          NetMessage.SendTileSquare(-1, x, y, 1, (TileChangeType) 0);
        }
      }
    }
  }
}
