// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions.RegenerationEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

public class RegenerationEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[2] || PermanentBuffPlayer.PermanentBuffsBools[39])
      return;
    player.Player.lifeRegen += 4;
    player.Player.buffImmune[2] = true;
  }
}
