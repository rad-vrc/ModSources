using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded
{
	// Token: 0x02000357 RID: 855
	public class DefenseEffect : IPermanentBuff
	{
		// Token: 0x060012DB RID: 4827 RVA: 0x0008D094 File Offset: 0x0008B294
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			new EnduranceEffect().ApplyEffect(player);
			new ExquisitelyStuffedEffect().ApplyEffect(player);
			new HeartreachEffect().ApplyEffect(player);
			new InfernoEffect().ApplyEffect(player);
			new IronskinEffect().ApplyEffect(player);
			new LifeforceEffect().ApplyEffect(player);
			new ObsidianSkinEffect().ApplyEffect(player);
			new RegenerationEffect().ApplyEffect(player);
			new ThornsEffect().ApplyEffect(player);
			new WarmthEffect().ApplyEffect(player);
		}
	}
}
