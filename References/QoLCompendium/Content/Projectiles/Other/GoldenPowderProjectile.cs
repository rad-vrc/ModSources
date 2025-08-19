// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Other.GoldenPowderProjectile
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.Other;

public class GoldenPowderProjectile : ModProjectile
{
  public virtual void SetDefaults()
  {
    this.Projectile.CloneDefaults(10);
    this.Projectile.aiStyle = -1;
  }

  public virtual void AI()
  {
    Projectile projectile = this.Projectile;
    ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.95f);
    ++this.Projectile.ai[0];
    if ((double) this.Projectile.ai[0] == 180.0)
      this.Projectile.Kill();
    if ((double) this.Projectile.ai[1] != 0.0)
      return;
    this.Projectile.ai[1] = 1f;
    for (int index = 0; index < 30; ++index)
      Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 246, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, 50, new Color(), 1f);
  }

  public virtual void ModifyDamageHitbox(ref Rectangle hitbox)
  {
    if (Main.netMode == 1)
      return;
    Rectangle myHitbox = hitbox;
    foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && ((Rectangle) ref myHitbox).Intersects(n.getRect()))))
    {
      if (Common.NormalBunnies[npc.type])
        npc.Transform(443);
      if (Common.NormalSquirrels[npc.type])
        npc.Transform(539);
      if (Common.NormalButterflies[npc.type])
        npc.Transform(444);
      if (Common.NormalBirds[npc.type])
        npc.Transform(442);
      if (NPCID.Sets.IsDragonfly[npc.type] && npc.type != 601)
        npc.Transform(601);
      if (npc.type == 361)
        npc.Transform(445);
      if (npc.type == 55)
        npc.Transform(592);
      if (npc.type == 230)
        npc.Transform(593);
      if (npc.type == 377)
        npc.Transform(446);
      if (npc.type == 604)
        npc.Transform(605);
      if (npc.type == 300)
        npc.Transform(447);
      if (npc.type == 626)
        npc.Transform(627);
      if (npc.type == 612)
        npc.Transform(613);
      if (npc.type == 357 || npc.type == 374 || npc.type == 375)
        npc.Transform(448);
    }
  }
}
