using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder
{
	// Token: 0x020002B3 RID: 691
	public class MartinsOrderDamageEffect : IPermanentModdedBuff
	{
		// Token: 0x06001193 RID: 4499 RVA: 0x00087DE4 File Offset: 0x00085FE4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			new DefenderEffect().ApplyEffect(player);
			new EmpowermentEffect().ApplyEffect(player);
			new EvocationEffect().ApplyEffect(player);
			new HasteEffect().ApplyEffect(player);
			new ShooterEffect().ApplyEffect(player);
			new SpellCasterEffect().ApplyEffect(player);
			new StarreachEffect().ApplyEffect(player);
			new SweeperEffect().ApplyEffect(player);
			new ThrowerEffect().ApplyEffect(player);
			new WhipperEffect().ApplyEffect(player);
		}
	}
}
