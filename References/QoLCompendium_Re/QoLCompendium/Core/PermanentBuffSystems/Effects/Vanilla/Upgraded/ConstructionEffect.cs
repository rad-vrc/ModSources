using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded
{
	// Token: 0x02000355 RID: 853
	public class ConstructionEffect : IPermanentBuff
	{
		// Token: 0x060012D7 RID: 4823 RVA: 0x0008CFE7 File Offset: 0x0008B1E7
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			new BuilderEffect().ApplyEffect(player);
			new CalmEffect().ApplyEffect(player);
			new MiningEffect().ApplyEffect(player);
		}
	}
}
