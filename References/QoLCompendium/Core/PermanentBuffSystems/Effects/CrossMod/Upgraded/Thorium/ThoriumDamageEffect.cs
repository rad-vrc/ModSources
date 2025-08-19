// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium.ThoriumDamageEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;

public class ThoriumDamageEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.thoriumLoaded)
      return;
    new ArtilleryEffect().ApplyEffect(player);
    new BouncingFlameEffect().ApplyEffect(player);
    new CactusFruitEffect().ApplyEffect(player);
    new ConflagrationEffect().ApplyEffect(player);
    new FrenzyEffect().ApplyEffect(player);
    new WarmongerEffect().ApplyEffect(player);
  }
}
