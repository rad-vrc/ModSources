using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations
{
	// Token: 0x0200035F RID: 863
	public class BewitchingTableEffect : IPermanentBuff
	{
		// Token: 0x060012EB RID: 4843 RVA: 0x0008D3A8 File Offset: 0x0008B5A8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[150] && !PermanentBuffPlayer.PermanentBuffsBools[53])
			{
				player.Player.maxMinions++;
				player.Player.buffImmune[150] = true;
			}
		}
	}
}
