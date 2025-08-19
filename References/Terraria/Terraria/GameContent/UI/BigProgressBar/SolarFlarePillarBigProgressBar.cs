using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003B0 RID: 944
	public class SolarFlarePillarBigProgressBar : LunarPillarBigProgessBar
	{
		// Token: 0x060029F3 RID: 10739 RVA: 0x005964F0 File Offset: 0x005946F0
		internal override float GetCurrentShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerSolar;
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x005964F8 File Offset: 0x005946F8
		internal override float GetMaxShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerMax;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x00596500 File Offset: 0x00594700
		internal override bool IsPlayerInCombatArea()
		{
			return Main.LocalPlayer.ZoneTowerSolar;
		}
	}
}
