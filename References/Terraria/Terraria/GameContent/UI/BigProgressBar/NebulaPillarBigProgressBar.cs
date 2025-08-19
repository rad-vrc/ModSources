using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003B2 RID: 946
	public class NebulaPillarBigProgressBar : LunarPillarBigProgessBar
	{
		// Token: 0x060029FB RID: 10747 RVA: 0x00596528 File Offset: 0x00594728
		internal override float GetCurrentShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerNebula;
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x005964F8 File Offset: 0x005946F8
		internal override float GetMaxShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerMax;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x00596530 File Offset: 0x00594730
		internal override bool IsPlayerInCombatArea()
		{
			return Main.LocalPlayer.ZoneTowerNebula;
		}
	}
}
