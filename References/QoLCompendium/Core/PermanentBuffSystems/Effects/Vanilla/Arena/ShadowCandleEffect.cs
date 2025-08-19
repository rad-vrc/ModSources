// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena.ShadowCandleEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena;

public class ShadowCandleEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[350] || PermanentBuffPlayer.PermanentBuffsBools[6])
      return;
    player.Player.ZoneShadowCandle = true;
    if (Main.myPlayer == ((Entity) player.Player).whoAmI)
      Main.SceneMetrics.ShadowCandleCount = 0;
    player.Player.buffImmune[350] = true;
  }
}
