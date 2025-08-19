// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions.ArcheryEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

public class ArcheryEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[16 /*0x10*/] || PermanentBuffPlayer.PermanentBuffsBools[11])
      return;
    player.Player.archery = true;
    Player player1 = player.Player;
    player1.arrowDamage = StatModifier.op_Multiply(player1.arrowDamage, 1.1f);
    player.Player.buffImmune[16 /*0x10*/] = true;
  }
}
