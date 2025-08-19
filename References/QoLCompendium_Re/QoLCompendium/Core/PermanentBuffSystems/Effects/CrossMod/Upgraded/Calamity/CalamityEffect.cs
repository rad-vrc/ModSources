using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Catalyst;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.CalamityEntropy;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Clamity;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity
{
	// Token: 0x020002BD RID: 701
	public class CalamityEffect : IPermanentModdedBuff
	{
		// Token: 0x060011A7 RID: 4519 RVA: 0x00088050 File Offset: 0x00086250
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			new CalamityAbyssEffect().ApplyEffect(player);
			new CalamityArenaEffect().ApplyEffect(player);
			new CalamityDamageEffect().ApplyEffect(player);
			new CalamityDefenseEffect().ApplyEffect(player);
			new CalamityFarmingEffect().ApplyEffect(player);
			new CalamityMovementEffect().ApplyEffect(player);
			if (ModConditions.catalystLoaded)
			{
				new AstracolaEffect().ApplyEffect(player);
			}
			if (ModConditions.clamityAddonLoaded)
			{
				new ClamityEffect().ApplyEffect(player);
			}
			if (ModConditions.calamityEntropyLoaded)
			{
				new CalamityEntropyEffect().ApplyEffect(player);
			}
		}
	}
}
