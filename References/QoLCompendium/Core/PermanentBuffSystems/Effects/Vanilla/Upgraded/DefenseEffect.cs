// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded.DefenseEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;

public class DefenseEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    new EnduranceEffect().ApplyEffect(player);
    new ExquisitelyStuffedEffect().ApplyEffect(player);
    new HeartreachEffect().ApplyEffect(player);
    new InfernoEffect().ApplyEffect(player);
    new IronskinEffect().ApplyEffect(player);
    new LifeforceEffect().ApplyEffect(player);
    new ObsidianSkinEffect().ApplyEffect(player);
    new RegenerationEffect().ApplyEffect(player);
    new ThornsEffect().ApplyEffect(player);
    new WarmthEffect().ApplyEffect(player);
  }
}
