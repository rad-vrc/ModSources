// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder.MartinsOrderDamageEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder;

public class MartinsOrderDamageEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.martainsOrderLoaded)
      return;
    new DefenderEffect().ApplyEffect(player);
    new EmpowermentEffect().ApplyEffect(player);
    new EvocationEffect().ApplyEffect(player);
    new HasteEffect().ApplyEffect(player);
    new ShooterEffect().ApplyEffect(player);
    new SpellCasterEffect().ApplyEffect(player);
    new StarreachEffect().ApplyEffect(player);
    new SweeperEffect().ApplyEffect(player);
    new ThrowerEffect().ApplyEffect(player);
    new WhipperEffect().ApplyEffect(player);
  }
}
