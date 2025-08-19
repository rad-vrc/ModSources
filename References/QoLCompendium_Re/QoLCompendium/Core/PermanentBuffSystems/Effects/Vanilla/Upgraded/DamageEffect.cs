using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded
{
	// Token: 0x02000356 RID: 854
	public class DamageEffect : IPermanentBuff
	{
		// Token: 0x060012D9 RID: 4825 RVA: 0x0008D00C File Offset: 0x0008B20C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			new AmmoReservationEffect().ApplyEffect(player);
			new ArcheryEffect().ApplyEffect(player);
			new BattleEffect().ApplyEffect(player);
			new LuckyEffect().ApplyEffect(player);
			new MagicPowerEffect().ApplyEffect(player);
			new ManaRegenerationEffect().ApplyEffect(player);
			new SummoningEffect().ApplyEffect(player);
			new TipsyEffect().ApplyEffect(player);
			new TitanEffect().ApplyEffect(player);
			new RageEffect().ApplyEffect(player);
			new WrathEffect().ApplyEffect(player);
		}
	}
}
