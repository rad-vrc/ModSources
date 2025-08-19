// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic.MirrorCoatEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic;

public class MirrorCoatEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.spiritLoaded || player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "MirrorCoatBuff")] || PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[5])
      return;
    this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "MirrorCoatBuff"));
    this.buffToApply.Update(player.Player, ref this.index);
    player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "MirrorCoatBuff")] = true;
  }
}
