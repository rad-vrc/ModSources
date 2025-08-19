using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic.Candies;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic
{
	// Token: 0x020002AD RID: 685
	public class SpiritClassicCandyEffect : IPermanentModdedBuff
	{
		// Token: 0x06001187 RID: 4487 RVA: 0x00087C0C File Offset: 0x00085E0C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			new CandyEffect().ApplyEffect(player);
			new ChocolateBarEffect().ApplyEffect(player);
			new HealthCandyEffect().ApplyEffect(player);
			new LollipopEffect().ApplyEffect(player);
			new ManaCandyEffect().ApplyEffect(player);
			new TaffyEffect().ApplyEffect(player);
		}
	}
}
