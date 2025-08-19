using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200038D RID: 909
	public class WrathEffect : IPermanentBuff
	{
		// Token: 0x06001347 RID: 4935 RVA: 0x0008E608 File Offset: 0x0008C808
		internal unsafe override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[117] && !PermanentBuffPlayer.PermanentBuffsBools[51])
			{
				*player.Player.GetDamage(DamageClass.Generic) += 0.1f;
				player.Player.buffImmune[117] = true;
			}
		}
	}
}
