// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations.SliceOfCakeEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations;

public class SliceOfCakeEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[192 /*0xC0*/] || PermanentBuffPlayer.PermanentBuffsBools[56])
      return;
    player.Player.pickSpeed -= 0.2f;
    player.Player.moveSpeed += 0.2f;
    player.Player.buffImmune[192 /*0xC0*/] = true;
  }
}
