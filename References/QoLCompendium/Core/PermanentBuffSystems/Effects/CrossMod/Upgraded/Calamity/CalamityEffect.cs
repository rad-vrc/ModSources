// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity.CalamityEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Catalyst;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.CalamityEntropy;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Clamity;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;

public class CalamityEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.calamityLoaded)
      return;
    new CalamityAbyssEffect().ApplyEffect(player);
    new CalamityArenaEffect().ApplyEffect(player);
    new CalamityDamageEffect().ApplyEffect(player);
    new CalamityDefenseEffect().ApplyEffect(player);
    new CalamityFarmingEffect().ApplyEffect(player);
    new CalamityMovementEffect().ApplyEffect(player);
    if (ModConditions.catalystLoaded)
      new AstracolaEffect().ApplyEffect(player);
    if (ModConditions.clamityAddonLoaded)
      new ClamityEffect().ApplyEffect(player);
    if (!ModConditions.calamityEntropyLoaded)
      return;
    new CalamityEntropyEffect().ApplyEffect(player);
  }
}
