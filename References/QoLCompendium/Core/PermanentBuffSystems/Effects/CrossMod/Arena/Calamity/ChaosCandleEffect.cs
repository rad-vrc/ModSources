// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity.ChaosCandleEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity;

public class ChaosCandleEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.calamityLoaded || player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "ChaosCandleBuff")] || PermanentBuffPlayer.PermanentCalamityBuffsBools[0])
      return;
    this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "ChaosCandleBuff"));
    this.buffToApply.Update(player.Player, ref this.index);
    player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "ChaosCandleBuff")] = true;
  }
}
