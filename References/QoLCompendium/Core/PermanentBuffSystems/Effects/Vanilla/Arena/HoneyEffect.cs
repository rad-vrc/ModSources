// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena.HoneyEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena;

public class HoneyEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[48 /*0x30*/] || PermanentBuffPlayer.PermanentBuffsBools[4])
      return;
    player.Player.lifeRegenTime += 2f;
    player.Player.lifeRegen += 2;
    player.Player.buffImmune[48 /*0x30*/] = true;
  }
}
