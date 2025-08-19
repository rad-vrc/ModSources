using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000216 RID: 534
	public class SummonDamageClass : VanillaDamageClass
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x0050992A File Offset: 0x00507B2A
		protected override string LangKey
		{
			get
			{
				return "LegacyTooltip.53";
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x00509931 File Offset: 0x00507B31
		public override bool UseStandardCritCalcs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x00509934 File Offset: 0x00507B34
		public override bool ShowStatTooltipLine(Player player, string lineName)
		{
			return lineName != "CritChance" && lineName != "Speed";
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x00509950 File Offset: 0x00507B50
		public override bool GetPrefixInheritance(DamageClass damageClass)
		{
			return damageClass == DamageClass.Magic;
		}
	}
}
