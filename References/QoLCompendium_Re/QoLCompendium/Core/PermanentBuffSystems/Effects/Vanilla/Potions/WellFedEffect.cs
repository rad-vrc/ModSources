using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200038C RID: 908
	public class WellFedEffect : IPermanentBuff
	{
		// Token: 0x06001345 RID: 4933 RVA: 0x0008E4FC File Offset: 0x0008C6FC
		internal unsafe override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[26] && !PermanentBuffPlayer.PermanentBuffsBools[50])
			{
				player.Player.wellFed = true;
				player.Player.statDefense += 2;
				*player.Player.GetCritChance(DamageClass.Generic) += 2f;
				*player.Player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
				*player.Player.GetDamage(DamageClass.Generic) += 0.05f;
				*player.Player.GetKnockback(DamageClass.Summon) += 0.5f;
				player.Player.moveSpeed += 0.2f;
				player.Player.pickSpeed -= 0.05f;
				player.Player.buffImmune[26] = true;
			}
		}
	}
}
