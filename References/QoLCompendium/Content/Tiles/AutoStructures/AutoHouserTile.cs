// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Tiles.AutoStructures.AutoHouserTile
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QoLCompendium.Content.Projectiles.Explosives;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Tiles.AutoStructures;

public class AutoHouserTile : ModTile
{
  public virtual void SetStaticDefaults()
  {
    Main.tileSolid[(int) ((ModBlockType) this).Type] = true;
    Main.tileMergeDirt[(int) ((ModBlockType) this).Type] = true;
    Main.tileBlockLight[(int) ((ModBlockType) this).Type] = true;
    Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
  }

  public virtual void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
  {
    noItem = true;
    if (Main.netMode == 1)
      return;
    int closest = (int) Player.FindClosest(new Vector2((float) (i * 16 /*0x10*/ + 8), (float) (j * 16 /*0x10*/ + 8)), 0, 0);
    if (closest == -1)
      return;
    Projectile.NewProjectile((IEntitySource) new EntitySource_TileBreak(i, j, (string) null), (float) (i * 16 /*0x10*/ + 8), (float) ((j + 2) * 16 /*0x10*/), 0.0f, 0.0f, ModContent.ProjectileType<AutoHouserProj>(), 0, 0.0f, closest, 0.0f, 0.0f, 0.0f);
  }

  public virtual void NearbyEffects(int i, int j, bool closer)
  {
    WorldGen.KillTile(i, j, false, false, false);
    if (Main.netMode == 0)
      return;
    NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) i, (float) j, 0.0f, 0, 0, 0);
  }

  public virtual bool PreDraw(int i, int j, SpriteBatch spriteBatch) => false;
}
