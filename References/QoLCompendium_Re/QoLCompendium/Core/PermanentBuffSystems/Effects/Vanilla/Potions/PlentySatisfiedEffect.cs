using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200037F RID: 895
	public class PlentySatisfiedEffect : IPermanentBuff
	{
		// Token: 0x0600132B RID: 4907 RVA: 0x0008E024 File Offset: 0x0008C224
		internal unsafe override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[206] && !PermanentBuffPlayer.PermanentBuffsBools[37])
			{
				player.Player.wellFed = true;
				player.Player.statDefense += 3;
				*player.Player.GetCritChance(DamageClass.Generic) += 3f;
				*player.Player.GetAttackSpeed(DamageClass.Melee) += 0.075f;
				*player.Player.GetDamage(DamageClass.Generic) += 0.075f;
				*player.Player.GetKnockback(DamageClass.Summon) += 0.75f;
				player.Player.moveSpeed += 0.3f;
				player.Player.pickSpeed -= 0.1f;
				player.Player.buffImmune[26] = true;
				player.Player.buffImmune[206] = true;
			}
		}
	}
}
