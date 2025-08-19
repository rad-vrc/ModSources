using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena
{
	// Token: 0x02000396 RID: 918
	public class BastStatueEffect : IPermanentBuff
	{
		// Token: 0x06001359 RID: 4953 RVA: 0x0008E824 File Offset: 0x0008CA24
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[215] && !PermanentBuffPlayer.PermanentBuffsBools[0])
			{
				player.Player.statDefense += 5;
				player.Player.buffImmune[215] = true;
			}
		}
	}
}
