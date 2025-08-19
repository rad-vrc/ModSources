using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity
{
	// Token: 0x020002B9 RID: 697
	public class CalamityAbyssEffect : IPermanentModdedBuff
	{
		// Token: 0x0600119F RID: 4511 RVA: 0x00087F6B File Offset: 0x0008616B
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			new AnechoicCoatingEffect().ApplyEffect(player);
			new OmniscienceEffect().ApplyEffect(player);
			new SulphurskinEffect().ApplyEffect(player);
		}
	}
}
