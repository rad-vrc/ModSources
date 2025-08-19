// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder.MartinsOrderDefenseEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder;

public class MartinsOrderDefenseEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.martainsOrderLoaded)
      return;
    new BlackHoleEffect().ApplyEffect(player);
    new ChargingEffect().ApplyEffect(player);
    new GourmetFlavorEffect().ApplyEffect(player);
    new HealingEffect().ApplyEffect(player);
    new RockskinEffect().ApplyEffect(player);
    new SoulEffect().ApplyEffect(player);
    new ZincPillEffect().ApplyEffect(player);
  }
}
