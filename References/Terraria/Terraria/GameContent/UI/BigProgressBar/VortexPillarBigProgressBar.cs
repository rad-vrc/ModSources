using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003B1 RID: 945
	public class VortexPillarBigProgressBar : LunarPillarBigProgessBar
	{
		// Token: 0x060029F7 RID: 10743 RVA: 0x00596514 File Offset: 0x00594714
		internal override float GetCurrentShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerVortex;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x005964F8 File Offset: 0x005946F8
		internal override float GetMaxShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerMax;
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x0059651C File Offset: 0x0059471C
		internal override bool IsPlayerInCombatArea()
		{
			return Main.LocalPlayer.ZoneTowerVortex;
		}
	}
}
