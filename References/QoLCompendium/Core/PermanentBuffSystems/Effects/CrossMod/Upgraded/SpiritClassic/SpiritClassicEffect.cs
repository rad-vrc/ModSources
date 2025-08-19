// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic.SpiritClassicEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic;

public class SpiritClassicEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.spiritLoaded)
      return;
    new SpiritClassicArenaEffect().ApplyEffect(player);
    new SpiritClassicCandyEffect().ApplyEffect(player);
    new SpiritClassicDamageEffect().ApplyEffect(player);
    new SpiritClassicDefenseEffect().ApplyEffect(player);
    new SpiritClassicMovementEffect().ApplyEffect(player);
  }
}
