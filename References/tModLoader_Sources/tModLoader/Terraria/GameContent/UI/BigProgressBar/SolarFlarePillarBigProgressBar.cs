using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000556 RID: 1366
	public class SolarFlarePillarBigProgressBar : LunarPillarBigProgessBar
	{
		// Token: 0x06004058 RID: 16472 RVA: 0x005DFEAE File Offset: 0x005DE0AE
		internal override float GetCurrentShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerSolar;
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x005DFEB6 File Offset: 0x005DE0B6
		internal override float GetMaxShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerMax;
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x005DFEBE File Offset: 0x005DE0BE
		internal override bool IsPlayerInCombatArea()
		{
			return Main.LocalPlayer.ZoneTowerSolar;
		}
	}
}
