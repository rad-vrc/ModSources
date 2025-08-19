using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000387 RID: 903
	public class ThornsEffect : IPermanentBuff
	{
		// Token: 0x0600133B RID: 4923 RVA: 0x0008E34C File Offset: 0x0008C54C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[14] && !PermanentBuffPlayer.PermanentBuffsBools[45])
			{
				player.Player.thorns = 1f;
				player.Player.buffImmune[14] = true;
			}
		}
	}
}
