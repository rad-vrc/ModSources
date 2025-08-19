using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Clamity;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Clamity
{
	// Token: 0x020002B7 RID: 695
	public class ClamityEffect : IPermanentModdedBuff
	{
		// Token: 0x0600119B RID: 4507 RVA: 0x00087F15 File Offset: 0x00086115
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.clamityAddonLoaded)
			{
				return;
			}
			new ExoBaguetteEffect().ApplyEffect(player);
			new SupremeLuckEffect().ApplyEffect(player);
			new TitanScaleEffect().ApplyEffect(player);
		}
	}
}
