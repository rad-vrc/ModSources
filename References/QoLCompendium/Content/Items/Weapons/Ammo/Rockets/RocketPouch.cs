// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Weapons.Ammo.Rockets.RocketPouch
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Weapons.Ammo.Rockets;

public abstract class RocketPouch : BaseAmmo
{
  public abstract int RocketProjectile { get; }

  public abstract int SnowmanProjectile { get; }

  public abstract int GrenadeProjectile { get; }

  public abstract int MineProjectile { get; }

  public virtual void PickAmmo(
    Item weapon,
    Player player,
    ref int type,
    ref float speed,
    ref StatModifier damage,
    ref float knockback)
  {
    switch (weapon.type)
    {
      case 758:
        type = this.GrenadeProjectile;
        break;
      case 759:
        type = this.RocketProjectile;
        break;
      case 760:
        type = this.MineProjectile;
        break;
      case 1946:
        type = this.SnowmanProjectile;
        break;
    }
  }
}
