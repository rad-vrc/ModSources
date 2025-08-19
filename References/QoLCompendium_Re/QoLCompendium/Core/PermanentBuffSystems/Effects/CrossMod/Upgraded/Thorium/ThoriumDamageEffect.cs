using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium
{
	// Token: 0x020002A5 RID: 677
	public class ThoriumDamageEffect : IPermanentModdedBuff
	{
		// Token: 0x06001177 RID: 4471 RVA: 0x00087A24 File Offset: 0x00085C24
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			new ArtilleryEffect().ApplyEffect(player);
			new BouncingFlameEffect().ApplyEffect(player);
			new CactusFruitEffect().ApplyEffect(player);
			new ConflagrationEffect().ApplyEffect(player);
			new FrenzyEffect().ApplyEffect(player);
			new WarmongerEffect().ApplyEffect(player);
		}
	}
}
