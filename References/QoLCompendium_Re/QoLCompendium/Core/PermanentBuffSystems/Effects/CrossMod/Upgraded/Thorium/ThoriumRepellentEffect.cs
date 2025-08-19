using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium
{
	// Token: 0x020002A9 RID: 681
	public class ThoriumRepellentEffect : IPermanentModdedBuff
	{
		// Token: 0x0600117F RID: 4479 RVA: 0x00087B34 File Offset: 0x00085D34
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			new BatRepellentEffect().ApplyEffect(player);
			new FishRepellentEffect().ApplyEffect(player);
			new InsectRepellentEffect().ApplyEffect(player);
			new SkeletonRepellentEffect().ApplyEffect(player);
			new ZombieRepellentEffect().ApplyEffect(player);
		}
	}
}
