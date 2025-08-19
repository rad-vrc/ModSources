// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic.SpiritClassicDamageEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic;

public class SpiritClassicDamageEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.spiritLoaded)
      return;
    new RunescribeEffect().ApplyEffect(player);
    new SoulguardEffect().ApplyEffect(player);
    new SpiritEffect().ApplyEffect(player);
    new StarburnEffect().ApplyEffect(player);
    new ToxinEffect().ApplyEffect(player);
  }
}
