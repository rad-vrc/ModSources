using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder
{
	// Token: 0x020002B4 RID: 692
	public class MartinsOrderDefenseEffect : IPermanentModdedBuff
	{
		// Token: 0x06001195 RID: 4501 RVA: 0x00087E68 File Offset: 0x00086068
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			new BlackHoleEffect().ApplyEffect(player);
			new ChargingEffect().ApplyEffect(player);
			new GourmetFlavorEffect().ApplyEffect(player);
			new HealingEffect().ApplyEffect(player);
			new RockskinEffect().ApplyEffect(player);
			new SoulEffect().ApplyEffect(player);
			new ZincPillEffect().ApplyEffect(player);
		}
	}
}
