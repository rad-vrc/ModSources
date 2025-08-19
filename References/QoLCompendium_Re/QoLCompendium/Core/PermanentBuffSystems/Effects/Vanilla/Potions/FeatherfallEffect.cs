using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200036E RID: 878
	public class FeatherfallEffect : IPermanentBuff
	{
		// Token: 0x06001309 RID: 4873 RVA: 0x0008D958 File Offset: 0x0008BB58
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[8] && !PermanentBuffPlayer.PermanentBuffsBools[20])
			{
				player.Player.slowFall = true;
				player.Player.buffImmune[8] = true;
			}
		}
	}
}
