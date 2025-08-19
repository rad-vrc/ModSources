// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions.WaterWalkingEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

public class WaterWalkingEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[15] || PermanentBuffPlayer.PermanentBuffsBools[49])
      return;
    player.Player.waterWalk = true;
    player.Player.buffImmune[15] = true;
  }
}
