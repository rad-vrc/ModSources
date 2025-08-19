// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium.ThoriumCoatingEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Flasks.Thorium;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;

public class ThoriumCoatingEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.thoriumLoaded)
      return;
    if (player.Player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 0)
      new ThrownWeaponImbueDeepFreezeEffect().ApplyEffect(player);
    if (player.Player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 1)
      new ThrownWeaponImbueExplosiveEffect().ApplyEffect(player);
    if (player.Player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 2)
      new ThrownWeaponImbueGorgonJuiceEffect().ApplyEffect(player);
    if (player.Player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 3)
      new ThrownWeaponImbueSporesEffect().ApplyEffect(player);
    if (player.Player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode != 4)
      return;
    new ThrownWeaponImbueToxicEffect().ApplyEffect(player);
  }
}
