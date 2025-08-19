using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity
{
	// Token: 0x020002BF RID: 703
	public class CalamityMovementEffect : IPermanentModdedBuff
	{
		// Token: 0x060011AB RID: 4523 RVA: 0x0008812C File Offset: 0x0008632C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			new BoundingEffect().ApplyEffect(player);
			new CalciumEffect().ApplyEffect(player);
			new GravityNormalizerEffect().ApplyEffect(player);
			new SoaringEffect().ApplyEffect(player);
		}
	}
}
