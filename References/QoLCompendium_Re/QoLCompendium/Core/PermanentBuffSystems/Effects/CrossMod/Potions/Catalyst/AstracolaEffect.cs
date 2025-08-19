using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Catalyst
{
	// Token: 0x02000312 RID: 786
	public class AstracolaEffect : IPermanentModdedBuff
	{
		// Token: 0x06001251 RID: 4689 RVA: 0x0008ACBD File Offset: 0x00088EBD
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.catalystLoaded)
			{
				return;
			}
			if (!PermanentBuffPlayer.PermanentCalamityBuffsBools[26])
			{
				new AstraJellyEffect().ApplyEffect(player);
				new MagicPowerEffect().ApplyEffect(player);
				new ManaRegenerationEffect().ApplyEffect(player);
			}
		}
	}
}
