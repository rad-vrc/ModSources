using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200036D RID: 877
	public class ExquisitelyStuffedEffect : IPermanentBuff
	{
		// Token: 0x06001307 RID: 4871 RVA: 0x0008D828 File Offset: 0x0008BA28
		internal unsafe override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[207] && !PermanentBuffPlayer.PermanentBuffsBools[19])
			{
				player.Player.wellFed = true;
				player.Player.statDefense += 4;
				*player.Player.GetCritChance(DamageClass.Generic) += 4f;
				*player.Player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
				*player.Player.GetDamage(DamageClass.Generic) += 0.1f;
				*player.Player.GetKnockback(DamageClass.Summon) += 1f;
				player.Player.moveSpeed += 0.4f;
				player.Player.pickSpeed -= 0.15f;
				player.Player.buffImmune[26] = true;
				player.Player.buffImmune[206] = true;
				player.Player.buffImmune[207] = true;
			}
		}
	}
}
