using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded
{
	// Token: 0x02000359 RID: 857
	public class MovementEffect : IPermanentBuff
	{
		// Token: 0x060012DF RID: 4831 RVA: 0x0008D20C File Offset: 0x0008B40C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			new FeatherfallEffect().ApplyEffect(player);
			new GravitationEffect().ApplyEffect(player);
			new SwiftnessEffect().ApplyEffect(player);
		}
	}
}
