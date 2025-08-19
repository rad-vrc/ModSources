// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Thorium.MistletoeEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Thorium;

public class MistletoeEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.thoriumLoaded || player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "MistletoeBuff")] || PermanentBuffPlayer.PermanentThoriumBuffsBools[0])
      return;
    this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "MistletoeBuff"));
    this.buffToApply.Update(player.Player, ref this.index);
    player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "MistletoeBuff")] = true;
  }
}
