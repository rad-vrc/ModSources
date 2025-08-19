// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium.SkeletonRepellentEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium;

public class SkeletonRepellentEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.thoriumLoaded || player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "SkeletonRepellentBuff")] || PermanentBuffPlayer.PermanentThoriumBuffsBools[21])
      return;
    this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "SkeletonRepellentBuff"));
    this.buffToApply.Update(player.Player, ref this.index);
    player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "SkeletonRepellentBuff")] = true;
  }
}
