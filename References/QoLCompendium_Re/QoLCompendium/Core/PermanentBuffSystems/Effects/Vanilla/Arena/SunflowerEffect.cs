using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena
{
	// Token: 0x0200039E RID: 926
	public class SunflowerEffect : IPermanentBuff
	{
		// Token: 0x06001369 RID: 4969 RVA: 0x0008EAEC File Offset: 0x0008CCEC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[146] && !PermanentBuffPlayer.PermanentBuffsBools[8])
			{
				player.Player.moveSpeed += 0.1f;
				player.Player.moveSpeed *= 1.1f;
				player.Player.sunflower = true;
				player.Player.buffImmune[146] = true;
			}
		}
	}
}
