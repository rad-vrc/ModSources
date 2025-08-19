using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000557 RID: 1367
	public class StardustPillarBigProgressBar : LunarPillarBigProgessBar
	{
		// Token: 0x0600405C RID: 16476 RVA: 0x005DFED2 File Offset: 0x005DE0D2
		internal override float GetCurrentShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerStardust;
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x005DFEDA File Offset: 0x005DE0DA
		internal override float GetMaxShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerMax;
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x005DFEE2 File Offset: 0x005DE0E2
		internal override bool IsPlayerInCombatArea()
		{
			return Main.LocalPlayer.ZoneTowerStardust;
		}
	}
}
