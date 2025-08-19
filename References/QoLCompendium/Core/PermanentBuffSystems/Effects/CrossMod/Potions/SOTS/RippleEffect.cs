// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS.RippleEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS;

public class RippleEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.secretsOfTheShadowsLoaded || player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "RippleBuff")] || PermanentBuffPlayer.PermanentSOTSBuffsBools[6])
      return;
    player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "RippleBuff")] = true;
  }
}
