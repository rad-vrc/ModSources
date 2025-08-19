// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded.VanillaEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;

public class VanillaEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    new AquaticEffect().ApplyEffect(player);
    new ArenaEffect().ApplyEffect(player);
    new ConstructionEffect().ApplyEffect(player);
    new DamageEffect().ApplyEffect(player);
    new DefenseEffect().ApplyEffect(player);
    new MovementEffect().ApplyEffect(player);
    new StationEffect().ApplyEffect(player);
    new TrawlerEffect().ApplyEffect(player);
    new VisionEffect().ApplyEffect(player);
  }
}
