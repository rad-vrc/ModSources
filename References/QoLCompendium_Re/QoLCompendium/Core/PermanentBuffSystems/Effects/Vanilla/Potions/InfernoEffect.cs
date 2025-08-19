using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000375 RID: 885
	public class InfernoEffect : IPermanentBuff
	{
		// Token: 0x06001317 RID: 4887 RVA: 0x0008DAF4 File Offset: 0x0008BCF4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[116] && !PermanentBuffPlayer.PermanentBuffsBools[27])
			{
				player.Player.inferno = true;
				Lighting.AddLight((int)player.Player.Center.X >> 4, (int)player.Player.Center.Y >> 4, 0.65f, 0.4f, 0.1f);
				bool flag = player.Player.infernoCounter % 60 == 0;
				int damage = 20;
				if (player.Player.whoAmI == Main.myPlayer)
				{
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						NPC nPC = Main.npc[i];
						if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.buffImmune[323] && Vector2.DistanceSquared(player.Player.Center, nPC.Center) <= 40000f)
						{
							if (nPC.FindBuffIndex(323) == -1)
							{
								nPC.AddBuff(323, 120, false);
							}
							if (flag)
							{
								player.Player.ApplyDamageToNPC(nPC, damage, 0f, 0, false, null, false);
							}
						}
					}
					if (Main.netMode != 0 && player.Player.hostile)
					{
						for (int j = 0; j < 255; j++)
						{
							Player target = Main.player[j];
							if (target != player.Player && target.active && !target.dead && target.hostile && !target.buffImmune[323] && (target.team != player.Player.team || target.team == 0) && Vector2.DistanceSquared(player.Player.Center, target.Center) <= 40000f)
							{
								if (target.FindBuffIndex(323) == -1)
								{
									target.AddBuff(323, 120, true, false);
								}
								if (flag)
								{
									Player.HurtInfo info = new Player.HurtInfo();
									target.Hurt(PlayerDeathReason.LegacyEmpty(), damage, 0, true, false, -1, true, 0f, 0f, 4.5f);
									PlayerDeathReason reason = PlayerDeathReason.ByOther(16, -1);
									info.DamageSource = reason;
									info.Damage = damage;
									info.PvP = true;
									info.Knockback = 120f;
									NetMessage.SendPlayerHurt(j, info, -1);
								}
							}
						}
					}
				}
				player.Player.buffImmune[116] = true;
			}
		}
	}
}
