// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions.InfernoEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

public class InfernoEffect : IPermanentBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (player.Player.buffImmune[116] || PermanentBuffPlayer.PermanentBuffsBools[27])
      return;
    player.Player.inferno = true;
    Lighting.AddLight((int) ((Entity) player.Player).Center.X >> 4, (int) ((Entity) player.Player).Center.Y >> 4, 0.65f, 0.4f, 0.1f);
    bool flag = player.Player.infernoCounter % 60 == 0;
    int num = 20;
    if (((Entity) player.Player).whoAmI == Main.myPlayer)
    {
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        NPC npc = Main.npc[index];
        if (((Entity) npc).active && !npc.friendly && npc.damage > 0 && !npc.dontTakeDamage && !npc.buffImmune[323] && (double) Vector2.DistanceSquared(((Entity) player.Player).Center, ((Entity) npc).Center) <= 40000.0)
        {
          if (npc.FindBuffIndex(323) == -1)
            npc.AddBuff(323, 120, false);
          if (flag)
            player.Player.ApplyDamageToNPC(npc, num, 0.0f, 0, false, (DamageClass) null, false);
        }
      }
      if (Main.netMode != 0 && player.Player.hostile)
      {
        for (int index = 0; index < (int) byte.MaxValue; ++index)
        {
          Player player1 = Main.player[index];
          if (player1 != player.Player && ((Entity) player1).active && !player1.dead && player1.hostile && !player1.buffImmune[323] && (player1.team != player.Player.team || player1.team == 0) && (double) Vector2.DistanceSquared(((Entity) player.Player).Center, ((Entity) player1).Center) <= 40000.0)
          {
            if (player1.FindBuffIndex(323) == -1)
              player1.AddBuff(323, 120, true, false);
            if (flag)
            {
              Player.HurtInfo hurtInfo;
              // ISSUE: explicit constructor call
              ((Player.HurtInfo) ref hurtInfo).\u002Ector();
              player1.Hurt(PlayerDeathReason.LegacyEmpty(), num, 0, true, false, -1, true, 0.0f, 0.0f, 4.5f);
              PlayerDeathReason playerDeathReason = PlayerDeathReason.ByOther(16 /*0x10*/, -1);
              hurtInfo.DamageSource = playerDeathReason;
              ((Player.HurtInfo) ref hurtInfo).Damage = num;
              hurtInfo.PvP = true;
              hurtInfo.Knockback = 120f;
              NetMessage.SendPlayerHurt(index, hurtInfo, -1);
            }
          }
        }
      }
    }
    player.Player.buffImmune[116] = true;
  }
}
