using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded
{
	// Token: 0x0200035B RID: 859
	public class TrawlerEffect : IPermanentBuff
	{
		// Token: 0x060012E3 RID: 4835 RVA: 0x0008D27F File Offset: 0x0008B47F
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			new CrateEffect().ApplyEffect(player);
			new FishingEffect().ApplyEffect(player);
			new SonarEffect().ApplyEffect(player);
		}
	}
}
