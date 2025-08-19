using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000559 RID: 1369
	public class VortexPillarBigProgressBar : LunarPillarBigProgessBar
	{
		// Token: 0x06004063 RID: 16483 RVA: 0x005E000A File Offset: 0x005DE20A
		internal override float GetCurrentShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerVortex;
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x005E0012 File Offset: 0x005DE212
		internal override float GetMaxShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerMax;
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x005E001A File Offset: 0x005DE21A
		internal override bool IsPlayerInCombatArea()
		{
			return Main.LocalPlayer.ZoneTowerVortex;
		}
	}
}
