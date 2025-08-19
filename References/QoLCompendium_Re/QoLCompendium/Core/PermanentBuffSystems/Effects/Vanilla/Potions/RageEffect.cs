using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000380 RID: 896
	public class RageEffect : IPermanentBuff
	{
		// Token: 0x0600132D RID: 4909 RVA: 0x0008E144 File Offset: 0x0008C344
		internal unsafe override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[115] && !PermanentBuffPlayer.PermanentBuffsBools[38])
			{
				*player.Player.GetCritChance(DamageClass.Generic) += 10f;
				player.Player.buffImmune[115] = true;
			}
		}
	}
}
