// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ProjectileChanges.NoSandLitter
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ProjectileChanges;

public class NoSandLitter : GlobalProjectile
{
  public virtual bool AppliesToEntity(Projectile entity, bool lateInstantiation)
  {
    return entity.type == 31 /*0x1F*/;
  }

  public virtual bool PreKill(Projectile projectile, int timeLeft)
  {
    if (!projectile.friendly && QoLCompendium.QoLCompendium.mainConfig.NoLittering)
      projectile.noDropItem = true;
    return base.PreKill(projectile, timeLeft);
  }

  public virtual void OnKill(Projectile projectile, int timeLeft)
  {
    if (projectile.friendly || !QoLCompendium.QoLCompendium.mainConfig.NoLittering || projectile.owner != Main.myPlayer)
      return;
    int index = Item.NewItem(((Entity) projectile).GetSource_DropAsItem((string) null), ((Entity) projectile).Hitbox, 169, 1, false, 0, false, false);
    Main.item[index].noGrabDelay = 0;
    if (Main.netMode != 2)
      return;
    NetMessage.SendData(21, -1, -1, (NetworkText) null, index, 1f, 0.0f, 0.0f, 0, 0, 0);
  }
}
