// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Catalyst.AstraJellyEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Catalyst;

public class AstraJellyEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.catalystLoaded || player.Player.buffImmune[Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff")] || PermanentBuffPlayer.PermanentCalamityBuffsBools[25])
      return;
    this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff"));
    this.buffToApply.Update(player.Player, ref this.index);
    player.Player.buffImmune[Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff")] = true;
  }
}
