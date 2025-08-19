// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ProjectileChanges.NoFallingBlockDamage
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ProjectileChanges;

public class NoFallingBlockDamage : GlobalProjectile
{
  public virtual bool AppliesToEntity(Projectile entity, bool lateInstantiation)
  {
    return Common.FallingBlocks.Contains(entity.type);
  }

  public virtual void SetDefaults(Projectile entity)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.NoFallingSandDamage)
      return;
    entity.friendly = true;
    entity.hostile = false;
  }
}
