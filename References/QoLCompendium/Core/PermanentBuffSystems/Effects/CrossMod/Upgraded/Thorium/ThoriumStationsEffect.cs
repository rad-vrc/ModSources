// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium.ThoriumStationsEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Thorium;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.Thorium;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;

public class ThoriumStationsEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.thoriumLoaded)
      return;
    new AltarEffect().ApplyEffect(player);
    new ConductorsStandEffect().ApplyEffect(player);
    new MistletoeEffect().ApplyEffect(player);
    new NinjaRackEffect().ApplyEffect(player);
  }
}
