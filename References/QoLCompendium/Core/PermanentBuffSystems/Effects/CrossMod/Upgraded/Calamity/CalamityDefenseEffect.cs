// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity.CalamityDefenseEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;

public class CalamityDefenseEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.calamityLoaded)
      return;
    new BaguetteEffect().ApplyEffect(player);
    new BloodfinEffect().ApplyEffect(player);
    new PhotosynthesisEffect().ApplyEffect(player);
    new TeslaEffect().ApplyEffect(player);
  }
}
