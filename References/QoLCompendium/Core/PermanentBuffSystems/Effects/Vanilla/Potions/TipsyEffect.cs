// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions.TipsyEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

public class TipsyEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[25] || PermanentBuffPlayer.PermanentBuffsBools[46])
      return;
    if (player.Player.HeldItem.DamageType == DamageClass.Melee)
    {
      player.Player.tipsy = true;
      Player player1 = player.Player;
      player1.statDefense = Player.DefenseStat.op_Subtraction(player1.statDefense, 4);
      player.Player.GetCritChance(DamageClass.Melee) += 2f;
      ref StatModifier local = ref player.Player.GetDamage(DamageClass.Melee);
      local = StatModifier.op_Addition(local, 0.1f);
      player.Player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
    }
    player.Player.buffImmune[25] = true;
  }
}
