// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium.ThoriumEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;

public class ThoriumEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.thoriumLoaded)
      return;
    new ThoriumBardEffect().ApplyEffect(player);
    new ThoriumDamageEffect().ApplyEffect(player);
    new ThoriumHealerEffect().ApplyEffect(player);
    new ThoriumMovementEffect().ApplyEffect(player);
    new ThoriumRepellentEffect().ApplyEffect(player);
    new ThoriumStationsEffect().ApplyEffect(player);
    new ThoriumThrowerEffect().ApplyEffect(player);
  }
}
