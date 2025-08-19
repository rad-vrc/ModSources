// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ProjectileChanges.NoTombstoneLitter
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ProjectileChanges;

public class NoTombstoneLitter : GlobalProjectile
{
  private static readonly Dictionary<int, int> _graveMarkerProjectileTypeToItemType = new Dictionary<int, int>()
  {
    {
      43,
      321
    },
    {
      201,
      1173
    },
    {
      202,
      1174
    },
    {
      203,
      1175
    },
    {
      204,
      1176
    },
    {
      205,
      1177
    },
    {
      527,
      3229
    },
    {
      528,
      3230
    },
    {
      529,
      3231
    },
    {
      530,
      3232
    },
    {
      531,
      3233
    }
  };

  public virtual bool AppliesToEntity(Projectile entity, bool lateInstantiation)
  {
    return NoTombstoneLitter._graveMarkerProjectileTypeToItemType.ContainsKey(entity.type);
  }

  public virtual bool PreAI(Projectile projectile)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.NoLittering)
      return base.PreAI(projectile);
    if (projectile.owner == Main.myPlayer)
    {
      int index = Item.NewItem(((Entity) projectile).GetSource_DropAsItem((string) null), ((Entity) projectile).Hitbox, NoTombstoneLitter._graveMarkerProjectileTypeToItemType[projectile.type], 1, false, 0, false, false);
      Main.item[index].noGrabDelay = 0;
      if (Main.netMode == 2)
        NetMessage.SendData(21, -1, -1, (NetworkText) null, index, 1f, 0.0f, 0.0f, 0, 0, 0);
    }
    projectile.Kill();
    return false;
  }
}
