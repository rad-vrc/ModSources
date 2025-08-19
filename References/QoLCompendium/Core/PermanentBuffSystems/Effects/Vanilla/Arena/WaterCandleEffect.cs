// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena.WaterCandleEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena;

public class WaterCandleEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[86] || PermanentBuffPlayer.PermanentBuffsBools[9])
      return;
    player.Player.ZoneWaterCandle = true;
    if (Main.myPlayer == ((Entity) player.Player).whoAmI)
      Main.SceneMetrics.WaterCandleCount = 0;
    player.Player.buffImmune[86] = true;
  }
}
