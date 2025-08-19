using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium
{
	// Token: 0x020002A7 RID: 679
	public class ThoriumHealerEffect : IPermanentModdedBuff
	{
		// Token: 0x0600117B RID: 4475 RVA: 0x00087ADE File Offset: 0x00085CDE
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			new ArcaneEffect().ApplyEffect(player);
			new GlowingEffect().ApplyEffect(player);
			new HolyEffect().ApplyEffect(player);
		}
	}
}
