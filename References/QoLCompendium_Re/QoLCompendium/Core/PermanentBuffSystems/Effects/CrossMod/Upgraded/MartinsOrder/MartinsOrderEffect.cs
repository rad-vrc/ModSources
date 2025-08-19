using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder
{
	// Token: 0x020002B5 RID: 693
	public class MartinsOrderEffect : IPermanentModdedBuff
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x00087ECA File Offset: 0x000860CA
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			new MartinsOrderDamageEffect().ApplyEffect(player);
			new MartinsOrderDefenseEffect().ApplyEffect(player);
			new MartinsOrderStationsEffect().ApplyEffect(player);
		}
	}
}
