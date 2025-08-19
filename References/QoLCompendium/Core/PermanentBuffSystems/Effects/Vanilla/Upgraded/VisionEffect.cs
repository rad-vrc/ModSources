// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded.VisionEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;

public class VisionEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    new BiomeSightEffect().ApplyEffect(player);
    new DangersenseEffect().ApplyEffect(player);
    new HunterEffect().ApplyEffect(player);
    new InvisibilityEffect().ApplyEffect(player);
    new NightOwlEffect().ApplyEffect(player);
    new ShineEffect().ApplyEffect(player);
    new SpelunkerEffect().ApplyEffect(player);
  }
}
