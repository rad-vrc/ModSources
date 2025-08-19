using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Thorium;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.Thorium;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium
{
	// Token: 0x020002AA RID: 682
	public class ThoriumStationsEffect : IPermanentModdedBuff
	{
		// Token: 0x06001181 RID: 4481 RVA: 0x00087B80 File Offset: 0x00085D80
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			new AltarEffect().ApplyEffect(player);
			new ConductorsStandEffect().ApplyEffect(player);
			new MistletoeEffect().ApplyEffect(player);
			new NinjaRackEffect().ApplyEffect(player);
		}
	}
}
