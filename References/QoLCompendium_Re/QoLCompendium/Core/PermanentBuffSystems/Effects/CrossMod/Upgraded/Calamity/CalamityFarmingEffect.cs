using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity
{
	// Token: 0x020002BE RID: 702
	public class CalamityFarmingEffect : IPermanentModdedBuff
	{
		// Token: 0x060011A9 RID: 4521 RVA: 0x000880E0 File Offset: 0x000862E0
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			new ZenEffect().ApplyEffect(player);
			new TranquilityCandleEffect().ApplyEffect(player);
			new ZergEffect().ApplyEffect(player);
			new ChaosCandleEffect().ApplyEffect(player);
			new CeaselessHungerEffect().ApplyEffect(player);
		}
	}
}
