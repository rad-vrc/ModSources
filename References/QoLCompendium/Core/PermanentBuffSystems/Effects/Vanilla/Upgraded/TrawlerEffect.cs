// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded.TrawlerEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;

public class TrawlerEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    new CrateEffect().ApplyEffect(player);
    new FishingEffect().ApplyEffect(player);
    new SonarEffect().ApplyEffect(player);
  }
}
