using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations
{
	// Token: 0x0200035E RID: 862
	public class AmmoBoxEffect : IPermanentBuff
	{
		// Token: 0x060012E9 RID: 4841 RVA: 0x0008D36E File Offset: 0x0008B56E
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[93] && !PermanentBuffPlayer.PermanentBuffsBools[52])
			{
				player.Player.ammoBox = true;
				player.Player.buffImmune[93] = true;
			}
		}
	}
}
