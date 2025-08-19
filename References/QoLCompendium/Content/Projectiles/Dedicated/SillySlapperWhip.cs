// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Dedicated.SillySlapperWhip
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.Dedicated;

public class SillySlapperWhip : WhipProjectile
{
  public override void SetStaticDefaults() => ProjectileID.Sets.IsAWhip[this.Type] = true;

  public override void SetWhipStats()
  {
    ((Entity) this.Projectile).width = 26;
    ((Entity) this.Projectile).height = 36;
    this.Projectile.WhipSettings.RangeMultiplier = 1f;
    this.Projectile.WhipSettings.Segments = 30;
    this.Projectile.DamageType = DamageClass.Generic;
    this.Projectile.damage = 100;
    this.fishingLineColor = Color.Green;
    this.dustAmount = 0;
    this.swingDust = new int?(0);
    this.tagDuration = 0;
    this.whipCrackSound = new SoundStyle?(new SoundStyle("QoLCompendium/Assets/Sounds/SillySlapperSFX", (SoundType) 0));
    this.multihitModifier = 1f;
  }
}
