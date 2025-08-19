// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions.ExquisitelyStuffedEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

public class ExquisitelyStuffedEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[207] || PermanentBuffPlayer.PermanentBuffsBools[19])
      return;
    player.Player.wellFed = true;
    Player player1 = player.Player;
    player1.statDefense = Player.DefenseStat.op_Addition(player1.statDefense, 4);
    player.Player.GetCritChance(DamageClass.Generic) += 4f;
    player.Player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
    ref StatModifier local1 = ref player.Player.GetDamage(DamageClass.Generic);
    local1 = StatModifier.op_Addition(local1, 0.1f);
    ref StatModifier local2 = ref player.Player.GetKnockback(DamageClass.Summon);
    local2 = StatModifier.op_Addition(local2, 1f);
    player.Player.moveSpeed += 0.4f;
    player.Player.pickSpeed -= 0.15f;
    player.Player.buffImmune[26] = true;
    player.Player.buffImmune[206] = true;
    player.Player.buffImmune[207] = true;
  }
}
