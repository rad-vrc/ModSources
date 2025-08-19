using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000388 RID: 904
	public class TipsyEffect : IPermanentBuff
	{
		// Token: 0x0600133D RID: 4925 RVA: 0x0008E388 File Offset: 0x0008C588
		internal unsafe override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[25] && !PermanentBuffPlayer.PermanentBuffsBools[46])
			{
				if (player.Player.HeldItem.DamageType == DamageClass.Melee)
				{
					player.Player.tipsy = true;
					player.Player.statDefense -= 4;
					*player.Player.GetCritChance(DamageClass.Melee) += 2f;
					*player.Player.GetDamage(DamageClass.Melee) += 0.1f;
					*player.Player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
				}
				player.Player.buffImmune[25] = true;
			}
		}
	}
}
