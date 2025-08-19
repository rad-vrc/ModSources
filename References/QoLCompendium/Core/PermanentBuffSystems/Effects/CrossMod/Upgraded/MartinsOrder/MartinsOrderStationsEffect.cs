// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder.MartinsOrderStationsEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.MartinsOrder;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder;

public class MartinsOrderStationsEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.martainsOrderLoaded)
      return;
    new ArcheologyEffect().ApplyEffect(player);
    new SporeFarmEffect().ApplyEffect(player);
  }
}
