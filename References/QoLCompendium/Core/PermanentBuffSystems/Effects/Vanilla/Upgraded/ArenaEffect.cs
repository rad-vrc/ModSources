// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded.ArenaEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;

public class ArenaEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    new BastStatueEffect().ApplyEffect(player);
    new CampfireEffect().ApplyEffect(player);
    new GardenGnomeEffect().ApplyEffect(player);
    new HeartLanternEffect().ApplyEffect(player);
    new HoneyEffect().ApplyEffect(player);
    new PeaceCandleEffect().ApplyEffect(player);
    new ShadowCandleEffect().ApplyEffect(player);
    new StarInABottleEffect().ApplyEffect(player);
    new SunflowerEffect().ApplyEffect(player);
    new WaterCandleEffect().ApplyEffect(player);
  }
}
