// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity.CalamityArenaEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;

public class CalamityArenaEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.calamityLoaded)
      return;
    new CorruptionEffigyEffect().ApplyEffect(player);
    new CrimsonEffigyEffect().ApplyEffect(player);
    new EffigyOfDecayEffect().ApplyEffect(player);
    new ResilientCandleEffect().ApplyEffect(player);
    new SpitefulCandleEffect().ApplyEffect(player);
    new VigorousCandleEffect().ApplyEffect(player);
    new WeightlessCandleEffect().ApplyEffect(player);
  }
}
