using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity
{
	// Token: 0x020002BB RID: 699
	public class CalamityDamageEffect : IPermanentModdedBuff
	{
		// Token: 0x060011A3 RID: 4515 RVA: 0x00087FFA File Offset: 0x000861FA
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			new AstralInjectionEffect().ApplyEffect(player);
			new ShadowEffect().ApplyEffect(player);
		}
	}
}
