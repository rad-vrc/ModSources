using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003B3 RID: 947
	public class StardustPillarBigProgressBar : LunarPillarBigProgessBar
	{
		// Token: 0x060029FF RID: 10751 RVA: 0x0059653C File Offset: 0x0059473C
		internal override float GetCurrentShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerStardust;
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x005964F8 File Offset: 0x005946F8
		internal override float GetMaxShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerMax;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x00596544 File Offset: 0x00594744
		internal override bool IsPlayerInCombatArea()
		{
			return Main.LocalPlayer.ZoneTowerStardust;
		}
	}
}
