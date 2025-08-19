using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity
{
	// Token: 0x020002BC RID: 700
	public class CalamityDefenseEffect : IPermanentModdedBuff
	{
		// Token: 0x060011A5 RID: 4517 RVA: 0x0008801A File Offset: 0x0008621A
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			new BaguetteEffect().ApplyEffect(player);
			new BloodfinEffect().ApplyEffect(player);
			new PhotosynthesisEffect().ApplyEffect(player);
			new TeslaEffect().ApplyEffect(player);
		}
	}
}
