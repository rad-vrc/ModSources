using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.ThoriumBossRework;
using Terraria.ModLoader;
using ThoriumRework;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium
{
	// Token: 0x020002A3 RID: 675
	public class ThoriumBardEffect : IPermanentModdedBuff
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0000A88D File Offset: 0x00008A8D
		[JITWhenModsEnabled(new string[]
		{
			"ThoriumRework"
		})]
		public static bool ThoriumReworkPotionsEnabled
		{
			get
			{
				return ModContent.GetInstance<CompatConfig>().extraPotions;
			}
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00087914 File Offset: 0x00085B14
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			new CreativityEffect().ApplyEffect(player);
			new EarwormEffect().ApplyEffect(player);
			new InspirationalReachEffect().ApplyEffect(player);
			if (!ModConditions.thoriumBossReworkLoaded)
			{
				return;
			}
			if (!ThoriumBardEffect.ThoriumReworkPotionsEnabled)
			{
				return;
			}
			new DeathsingerEffect().ApplyEffect(player);
			new InspirationRegenerationEffect().ApplyEffect(player);
		}
	}
}
