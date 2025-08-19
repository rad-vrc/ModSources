// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.ThoriumBossRework.DeathsingerEffect
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
public class DeathsingerEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.thoriumBossReworkLoaded || !ModContent.GetInstance<CompatConfig>().extraPotions || player.Player.buffImmune[ModContent.BuffType<Deathsinger>()] || PermanentBuffPlayer.PermanentThoriumBuffsBools[26])
      return;
    this.buffToApply = BuffLoader.GetBuff(ModContent.BuffType<Deathsinger>());
    this.buffToApply.Update(player.Player, ref this.index);
    player.Player.buffImmune[ModContent.BuffType<Deathsinger>()] = true;
  }
}
