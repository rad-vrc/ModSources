using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000384 RID: 900
	public class SpelunkerEffect : IPermanentBuff
	{
		// Token: 0x06001335 RID: 4917 RVA: 0x0008E297 File Offset: 0x0008C497
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[9] && !PermanentBuffPlayer.PermanentBuffsBools[42])
			{
				player.Player.findTreasure = true;
				player.Player.buffImmune[9] = true;
			}
		}
	}
}
