// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.All.EverythingEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SOTS;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.All;

public class EverythingEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    new VanillaEffect().ApplyEffect(player);
    if (ModConditions.calamityLoaded)
      new CalamityEffect().ApplyEffect(player);
    if (ModConditions.martainsOrderLoaded)
      new MartinsOrderEffect().ApplyEffect(player);
    if (ModConditions.secretsOfTheShadowsLoaded)
      new SecretsOfTheShadowsEffect().ApplyEffect(player);
    if (ModConditions.spiritLoaded)
      new SpiritClassicEffect().ApplyEffect(player);
    if (!ModConditions.thoriumLoaded)
      return;
    new ThoriumEffect().ApplyEffect(player);
  }
}
