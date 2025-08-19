// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity.CalamityAbyssEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;

public class CalamityAbyssEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.calamityLoaded)
      return;
    new AnechoicCoatingEffect().ApplyEffect(player);
    new OmniscienceEffect().ApplyEffect(player);
    new SulphurskinEffect().ApplyEffect(player);
  }
}
