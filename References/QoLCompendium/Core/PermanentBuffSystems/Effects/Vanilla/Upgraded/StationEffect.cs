// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded.StationEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;

public class StationEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    new AmmoBoxEffect().ApplyEffect(player);
    new BewitchingTableEffect().ApplyEffect(player);
    new CrystalBallEffect().ApplyEffect(player);
    new SharpeningStationEffect().ApplyEffect(player);
    new SliceOfCakeEffect().ApplyEffect(player);
    new WarTableEffect().ApplyEffect(player);
  }
}
