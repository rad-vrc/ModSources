using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000378 RID: 888
	public class LifeforceEffect : IPermanentBuff
	{
		// Token: 0x0600131D RID: 4893 RVA: 0x0008DDFC File Offset: 0x0008BFFC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[113] && !PermanentBuffPlayer.PermanentBuffsBools[30])
			{
				player.Player.lifeForce = true;
				player.Player.statLifeMax2 += player.Player.statLifeMax / 5;
				player.Player.buffImmune[113] = true;
			}
		}
	}
}
