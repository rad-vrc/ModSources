using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded
{
	// Token: 0x02000353 RID: 851
	public class AquaticEffect : IPermanentBuff
	{
		// Token: 0x060012D3 RID: 4819 RVA: 0x0008CF40 File Offset: 0x0008B140
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			new GillsEffect().ApplyEffect(player);
			new FlipperEffect().ApplyEffect(player);
			new WaterWalkingEffect().ApplyEffect(player);
		}
	}
}
