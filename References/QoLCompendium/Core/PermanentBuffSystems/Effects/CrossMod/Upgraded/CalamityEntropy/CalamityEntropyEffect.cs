// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.CalamityEntropy.CalamityEntropyEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.CalamityEntropy;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.CalamityEntropy;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.CalamityEntropy;

public class CalamityEntropyEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.calamityEntropyLoaded)
      return;
    new VoidCandleEffect().ApplyEffect(player);
    new YharimsStimulantsEffect().ApplyEffect(player);
    new SoyMilkEffect().ApplyEffect(player);
  }
}
