using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.MartinsOrder;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder
{
	// Token: 0x020002B6 RID: 694
	public class MartinsOrderStationsEffect : IPermanentModdedBuff
	{
		// Token: 0x06001199 RID: 4505 RVA: 0x00087EF5 File Offset: 0x000860F5
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			new ArcheologyEffect().ApplyEffect(player);
			new SporeFarmEffect().ApplyEffect(player);
		}
	}
}
