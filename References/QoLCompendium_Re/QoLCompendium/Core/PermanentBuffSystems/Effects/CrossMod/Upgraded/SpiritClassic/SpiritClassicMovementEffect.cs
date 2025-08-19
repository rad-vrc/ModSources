using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic
{
	// Token: 0x020002B1 RID: 689
	public class SpiritClassicMovementEffect : IPermanentModdedBuff
	{
		// Token: 0x0600118F RID: 4495 RVA: 0x00087D28 File Offset: 0x00085F28
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			new JumpEffect().ApplyEffect(player);
			new SoaringEffect().ApplyEffect(player);
			new ZephyrEffect().ApplyEffect(player);
		}
	}
}
