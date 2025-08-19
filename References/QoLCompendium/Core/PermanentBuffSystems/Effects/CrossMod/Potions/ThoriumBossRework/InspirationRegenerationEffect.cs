// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.ThoriumBossRework.InspirationRegenerationEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria.ModLoader;
using ThoriumRework;
using ThoriumRework.Buffs;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.ThoriumBossRework;

[JITWhenModsEnabled(new string[] {"ThoriumRework"})]
[ExtendsFromMod(new string[] {"ThoriumRework"})]
public class InspirationRegenerationEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.thoriumBossReworkLoaded || !ModContent.GetInstance<CompatConfig>().extraPotions || player.Player.buffImmune[ModContent.BuffType<Inspired>()] || PermanentBuffPlayer.PermanentThoriumBuffsBools[27])
      return;
    this.buffToApply = BuffLoader.GetBuff(ModContent.BuffType<Inspired>());
    this.buffToApply.Update(player.Player, ref this.index);
    player.Player.buffImmune[ModContent.BuffType<Inspired>()] = true;
  }
}
