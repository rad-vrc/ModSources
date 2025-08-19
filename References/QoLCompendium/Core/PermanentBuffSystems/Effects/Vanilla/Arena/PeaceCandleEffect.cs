// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena.PeaceCandleEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena;

public class PeaceCandleEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[157] || PermanentBuffPlayer.PermanentBuffsBools[5])
      return;
    player.Player.ZonePeaceCandle = true;
    if (Main.myPlayer == ((Entity) player.Player).whoAmI)
      Main.SceneMetrics.PeaceCandleCount = 0;
    player.Player.buffImmune[157] = true;
  }
}
