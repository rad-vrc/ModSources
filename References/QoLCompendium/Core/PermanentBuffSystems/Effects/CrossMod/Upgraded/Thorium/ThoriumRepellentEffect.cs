// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium.ThoriumRepellentEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;

public class ThoriumRepellentEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.thoriumLoaded)
      return;
    new BatRepellentEffect().ApplyEffect(player);
    new FishRepellentEffect().ApplyEffect(player);
    new InsectRepellentEffect().ApplyEffect(player);
    new SkeletonRepellentEffect().ApplyEffect(player);
    new ZombieRepellentEffect().ApplyEffect(player);
  }
}
