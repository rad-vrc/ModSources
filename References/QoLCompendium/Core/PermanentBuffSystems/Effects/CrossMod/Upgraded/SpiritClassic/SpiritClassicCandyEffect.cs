// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic.SpiritClassicCandyEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic.Candies;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic;

public class SpiritClassicCandyEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.spiritLoaded)
      return;
    new CandyEffect().ApplyEffect(player);
    new ChocolateBarEffect().ApplyEffect(player);
    new HealthCandyEffect().ApplyEffect(player);
    new LollipopEffect().ApplyEffect(player);
    new ManaCandyEffect().ApplyEffect(player);
    new TaffyEffect().ApplyEffect(player);
  }
}
