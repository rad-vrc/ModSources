// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions.AmmoReservationEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

public class AmmoReservationEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[112 /*0x70*/] || PermanentBuffPlayer.PermanentBuffsBools[10])
      return;
    player.Player.ammoPotion = true;
    player.Player.buffImmune[112 /*0x70*/] = true;
  }
}
