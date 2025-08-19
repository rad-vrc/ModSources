using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic
{
	// Token: 0x020002AE RID: 686
	public class SpiritClassicDamageEffect : IPermanentModdedBuff
	{
		// Token: 0x06001189 RID: 4489 RVA: 0x00087C64 File Offset: 0x00085E64
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			new RunescribeEffect().ApplyEffect(player);
			new SoulguardEffect().ApplyEffect(player);
			new SpiritEffect().ApplyEffect(player);
			new StarburnEffect().ApplyEffect(player);
			new ToxinEffect().ApplyEffect(player);
		}
	}
}
