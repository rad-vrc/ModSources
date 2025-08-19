using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic
{
	// Token: 0x020002AF RID: 687
	public class SpiritClassicDefenseEffect : IPermanentModdedBuff
	{
		// Token: 0x0600118B RID: 4491 RVA: 0x00087CB0 File Offset: 0x00085EB0
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			new MirrorCoatEffect().ApplyEffect(player);
			new SporecoidEffect().ApplyEffect(player);
			new SteadfastEffect().ApplyEffect(player);
		}
	}
}
