using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000385 RID: 901
	public class SummoningEffect : IPermanentBuff
	{
		// Token: 0x06001337 RID: 4919 RVA: 0x0008E2CE File Offset: 0x0008C4CE
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[110] && !PermanentBuffPlayer.PermanentBuffsBools[43])
			{
				player.Player.maxMinions++;
				player.Player.buffImmune[110] = true;
			}
		}
	}
}
