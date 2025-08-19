using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000370 RID: 880
	public class FlipperEffect : IPermanentBuff
	{
		// Token: 0x0600130D RID: 4877 RVA: 0x0008D9CC File Offset: 0x0008BBCC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[109] && !PermanentBuffPlayer.PermanentBuffsBools[22])
			{
				player.Player.ignoreWater = true;
				player.Player.accFlipper = true;
				player.Player.buffImmune[109] = true;
			}
		}
	}
}
