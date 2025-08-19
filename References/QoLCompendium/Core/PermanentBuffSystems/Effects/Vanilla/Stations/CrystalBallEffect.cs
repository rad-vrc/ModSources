// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations.CrystalBallEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations;

public class CrystalBallEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[29] || PermanentBuffPlayer.PermanentBuffsBools[54])
      return;
    ref StatModifier local = ref player.Player.GetDamage(DamageClass.Magic);
    local = StatModifier.op_Addition(local, 0.05f);
    player.Player.GetCritChance(DamageClass.Magic) += 2f;
    player.Player.statManaMax2 += 20;
    player.Player.manaCost -= 0.02f;
    player.Player.buffImmune[29] = true;
  }
}
