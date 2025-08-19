using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded
{
	// Token: 0x0200035A RID: 858
	public class StationEffect : IPermanentBuff
	{
		// Token: 0x060012E1 RID: 4833 RVA: 0x0008D230 File Offset: 0x0008B430
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			new AmmoBoxEffect().ApplyEffect(player);
			new BewitchingTableEffect().ApplyEffect(player);
			new CrystalBallEffect().ApplyEffect(player);
			new SharpeningStationEffect().ApplyEffect(player);
			new SliceOfCakeEffect().ApplyEffect(player);
			new WarTableEffect().ApplyEffect(player);
		}
	}
}
