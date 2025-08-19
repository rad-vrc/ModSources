// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded.DamageEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;

public class DamageEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    new AmmoReservationEffect().ApplyEffect(player);
    new ArcheryEffect().ApplyEffect(player);
    new BattleEffect().ApplyEffect(player);
    new LuckyEffect().ApplyEffect(player);
    new MagicPowerEffect().ApplyEffect(player);
    new ManaRegenerationEffect().ApplyEffect(player);
    new SummoningEffect().ApplyEffect(player);
    new TipsyEffect().ApplyEffect(player);
    new TitanEffect().ApplyEffect(player);
    new RageEffect().ApplyEffect(player);
    new WrathEffect().ApplyEffect(player);
  }
}
