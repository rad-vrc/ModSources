using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SOTS;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.All
{
	// Token: 0x020003A0 RID: 928
	public class EverythingEffect : IPermanentModdedBuff
	{
		// Token: 0x0600136D RID: 4973 RVA: 0x0008EBC4 File Offset: 0x0008CDC4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			new VanillaEffect().ApplyEffect(player);
			if (ModConditions.calamityLoaded)
			{
				new CalamityEffect().ApplyEffect(player);
			}
			if (ModConditions.martainsOrderLoaded)
			{
				new MartinsOrderEffect().ApplyEffect(player);
			}
			if (ModConditions.secretsOfTheShadowsLoaded)
			{
				new SecretsOfTheShadowsEffect().ApplyEffect(player);
			}
			if (ModConditions.spiritLoaded)
			{
				new SpiritClassicEffect().ApplyEffect(player);
			}
			if (ModConditions.thoriumLoaded)
			{
				new ThoriumEffect().ApplyEffect(player);
			}
		}
	}
}
