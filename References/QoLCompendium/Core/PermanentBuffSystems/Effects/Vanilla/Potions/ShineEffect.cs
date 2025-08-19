// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions.ShineEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

public class ShineEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[11] || PermanentBuffPlayer.PermanentBuffsBools[40])
      return;
    Lighting.AddLight((int) ((double) ((Entity) player.Player).position.X + (double) (((Entity) player.Player).width / 2)) / 16 /*0x10*/, (int) ((double) ((Entity) player.Player).position.Y + (double) (((Entity) player.Player).height / 2)) / 16 /*0x10*/, 0.8f, 0.95f, 1f);
    player.Player.buffImmune[11] = true;
  }
}
