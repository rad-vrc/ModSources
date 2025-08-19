using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium
{
	// Token: 0x020002AB RID: 683
	public class ThoriumThrowerEffect : IPermanentModdedBuff
	{
		// Token: 0x06001183 RID: 4483 RVA: 0x00087BB6 File Offset: 0x00085DB6
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			new AssassinEffect().ApplyEffect(player);
			new HydrationEffect().ApplyEffect(player);
		}
	}
}
