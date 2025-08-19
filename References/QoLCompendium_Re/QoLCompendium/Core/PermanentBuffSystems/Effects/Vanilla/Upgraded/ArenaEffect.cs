using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded
{
	// Token: 0x02000354 RID: 852
	public class ArenaEffect : IPermanentBuff
	{
		// Token: 0x060012D5 RID: 4821 RVA: 0x0008CF6C File Offset: 0x0008B16C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			new BastStatueEffect().ApplyEffect(player);
			new CampfireEffect().ApplyEffect(player);
			new GardenGnomeEffect().ApplyEffect(player);
			new HeartLanternEffect().ApplyEffect(player);
			new HoneyEffect().ApplyEffect(player);
			new PeaceCandleEffect().ApplyEffect(player);
			new ShadowCandleEffect().ApplyEffect(player);
			new StarInABottleEffect().ApplyEffect(player);
			new SunflowerEffect().ApplyEffect(player);
			new WaterCandleEffect().ApplyEffect(player);
		}
	}
}
