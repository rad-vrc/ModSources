using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic
{
	// Token: 0x020002B0 RID: 688
	public class SpiritClassicEffect : IPermanentModdedBuff
	{
		// Token: 0x0600118D RID: 4493 RVA: 0x00087CDC File Offset: 0x00085EDC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			new SpiritClassicArenaEffect().ApplyEffect(player);
			new SpiritClassicCandyEffect().ApplyEffect(player);
			new SpiritClassicDamageEffect().ApplyEffect(player);
			new SpiritClassicDefenseEffect().ApplyEffect(player);
			new SpiritClassicMovementEffect().ApplyEffect(player);
		}
	}
}
