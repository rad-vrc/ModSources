using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium
{
	// Token: 0x020002A6 RID: 678
	public class ThoriumEffect : IPermanentModdedBuff
	{
		// Token: 0x06001179 RID: 4473 RVA: 0x00087A7C File Offset: 0x00085C7C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			new ThoriumBardEffect().ApplyEffect(player);
			new ThoriumDamageEffect().ApplyEffect(player);
			new ThoriumHealerEffect().ApplyEffect(player);
			new ThoriumMovementEffect().ApplyEffect(player);
			new ThoriumRepellentEffect().ApplyEffect(player);
			new ThoriumStationsEffect().ApplyEffect(player);
			new ThoriumThrowerEffect().ApplyEffect(player);
		}
	}
}
