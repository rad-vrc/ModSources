// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SOTS.SecretsOfTheShadowsEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.SOTS;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SOTS;

public class SecretsOfTheShadowsEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.secretsOfTheShadowsLoaded)
      return;
    new AssassinationEffect().ApplyEffect(player);
    new BluefireEffect().ApplyEffect(player);
    new BrittleEffect().ApplyEffect(player);
    new DoubleVisionEffect().ApplyEffect(player);
    new HarmonyEffect().ApplyEffect(player);
    new NightmareEffect().ApplyEffect(player);
    new RippleEffect().ApplyEffect(player);
    new RoughskinEffect().ApplyEffect(player);
    new SoulAccessEffect().ApplyEffect(player);
    new VibeEffect().ApplyEffect(player);
    new DigitalDisplayEffect().ApplyEffect(player);
  }
}
