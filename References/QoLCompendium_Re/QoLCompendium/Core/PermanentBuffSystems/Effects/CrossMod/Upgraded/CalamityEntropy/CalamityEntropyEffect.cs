using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.CalamityEntropy;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.CalamityEntropy;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.CalamityEntropy
{
	// Token: 0x020002B8 RID: 696
	public class CalamityEntropyEffect : IPermanentModdedBuff
	{
		// Token: 0x0600119D RID: 4509 RVA: 0x00087F40 File Offset: 0x00086140
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityEntropyLoaded)
			{
				return;
			}
			new VoidCandleEffect().ApplyEffect(player);
			new YharimsStimulantsEffect().ApplyEffect(player);
			new SoyMilkEffect().ApplyEffect(player);
		}
	}
}
