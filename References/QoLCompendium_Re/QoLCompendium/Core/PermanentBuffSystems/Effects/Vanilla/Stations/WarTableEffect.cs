using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations
{
	// Token: 0x02000363 RID: 867
	public class WarTableEffect : IPermanentBuff
	{
		// Token: 0x060012F3 RID: 4851 RVA: 0x0008D560 File Offset: 0x0008B760
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[348] && !PermanentBuffPlayer.PermanentBuffsBools[57])
			{
				player.Player.maxTurrets++;
				player.Player.buffImmune[348] = true;
			}
		}
	}
}
