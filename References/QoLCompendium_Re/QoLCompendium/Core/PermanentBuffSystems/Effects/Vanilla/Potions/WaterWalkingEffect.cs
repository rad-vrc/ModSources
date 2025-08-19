using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200038B RID: 907
	public class WaterWalkingEffect : IPermanentBuff
	{
		// Token: 0x06001343 RID: 4931 RVA: 0x0008E4C3 File Offset: 0x0008C6C3
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[15] && !PermanentBuffPlayer.PermanentBuffsBools[49])
			{
				player.Player.waterWalk = true;
				player.Player.buffImmune[15] = true;
			}
		}
	}
}
