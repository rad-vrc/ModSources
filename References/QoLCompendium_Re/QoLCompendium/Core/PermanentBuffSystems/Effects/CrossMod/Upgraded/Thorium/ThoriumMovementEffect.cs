using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium
{
	// Token: 0x020002A8 RID: 680
	public class ThoriumMovementEffect : IPermanentModdedBuff
	{
		// Token: 0x0600117D RID: 4477 RVA: 0x00087B09 File Offset: 0x00085D09
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			new AquaAffinityEffect().ApplyEffect(player);
			new BloodRushEffect().ApplyEffect(player);
			new KineticEffect().ApplyEffect(player);
		}
	}
}
