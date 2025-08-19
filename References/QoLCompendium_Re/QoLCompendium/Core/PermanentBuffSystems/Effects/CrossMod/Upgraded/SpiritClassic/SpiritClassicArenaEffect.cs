using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.SpiritClassic;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic
{
	// Token: 0x020002AC RID: 684
	public class SpiritClassicArenaEffect : IPermanentModdedBuff
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x00087BD6 File Offset: 0x00085DD6
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			new CoiledEnergizerEffect().ApplyEffect(player);
			new KoiTotemEffect().ApplyEffect(player);
			new SunPotEffect().ApplyEffect(player);
			new TheCouchEffect().ApplyEffect(player);
		}
	}
}
