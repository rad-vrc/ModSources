using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x0200023B RID: 571
	public class TowerShieldHealth : ModSystem
	{
		// Token: 0x06000DA8 RID: 3496 RVA: 0x0006D0F8 File Offset: 0x0006B2F8
		public override void PreUpdateWorld()
		{
			if (NPC.ShieldStrengthTowerVortex > QoLCompendium.mainConfig.LunarPillarShieldHeath)
			{
				NPC.ShieldStrengthTowerVortex = QoLCompendium.mainConfig.LunarPillarShieldHeath;
			}
			if (NPC.ShieldStrengthTowerSolar > QoLCompendium.mainConfig.LunarPillarShieldHeath)
			{
				NPC.ShieldStrengthTowerSolar = QoLCompendium.mainConfig.LunarPillarShieldHeath;
			}
			if (NPC.ShieldStrengthTowerNebula > QoLCompendium.mainConfig.LunarPillarShieldHeath)
			{
				NPC.ShieldStrengthTowerNebula = QoLCompendium.mainConfig.LunarPillarShieldHeath;
			}
			if (NPC.ShieldStrengthTowerStardust > QoLCompendium.mainConfig.LunarPillarShieldHeath)
			{
				NPC.ShieldStrengthTowerStardust = QoLCompendium.mainConfig.LunarPillarShieldHeath;
			}
		}
	}
}
