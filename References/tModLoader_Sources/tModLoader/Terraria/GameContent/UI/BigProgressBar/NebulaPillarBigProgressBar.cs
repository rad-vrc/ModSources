using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000553 RID: 1363
	public class NebulaPillarBigProgressBar : LunarPillarBigProgessBar
	{
		// Token: 0x0600404D RID: 16461 RVA: 0x005DFCC3 File Offset: 0x005DDEC3
		internal override float GetCurrentShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerNebula;
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x005DFCCB File Offset: 0x005DDECB
		internal override float GetMaxShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerMax;
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x005DFCD3 File Offset: 0x005DDED3
		internal override bool IsPlayerInCombatArea()
		{
			return Main.LocalPlayer.ZoneTowerNebula;
		}
	}
}
