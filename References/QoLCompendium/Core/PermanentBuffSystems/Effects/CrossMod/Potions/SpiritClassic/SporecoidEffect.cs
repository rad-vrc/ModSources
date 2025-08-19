// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic.SporecoidEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic;

public class SporecoidEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.spiritLoaded || player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "MushroomPotionBuff")] || PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[11])
      return;
    this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "MushroomPotionBuff"));
    this.buffToApply.Update(player.Player, ref this.index);
    player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "MushroomPotionBuff")] = true;
  }
}
