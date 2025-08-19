using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity
{
	// Token: 0x020002BA RID: 698
	public class CalamityArenaEffect : IPermanentModdedBuff
	{
		// Token: 0x060011A1 RID: 4513 RVA: 0x00087F98 File Offset: 0x00086198
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			new CorruptionEffigyEffect().ApplyEffect(player);
			new CrimsonEffigyEffect().ApplyEffect(player);
			new EffigyOfDecayEffect().ApplyEffect(player);
			new ResilientCandleEffect().ApplyEffect(player);
			new SpitefulCandleEffect().ApplyEffect(player);
			new VigorousCandleEffect().ApplyEffect(player);
			new WeightlessCandleEffect().ApplyEffect(player);
		}
	}
}
