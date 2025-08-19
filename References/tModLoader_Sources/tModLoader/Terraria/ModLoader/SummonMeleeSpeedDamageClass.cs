using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000217 RID: 535
	public class SummonMeleeSpeedDamageClass : VanillaDamageClass
	{
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x00509962 File Offset: 0x00507B62
		protected override string LangKey
		{
			get
			{
				return "LegacyTooltip.53";
			}
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x0050996C File Offset: 0x00507B6C
		public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
		{
			if (damageClass == DamageClass.Melee)
			{
				return new StatInheritanceData(0f, 0f, 1f, 0f, 0f);
			}
			if (damageClass == DamageClass.Generic || damageClass == DamageClass.Summon)
			{
				return StatInheritanceData.Full;
			}
			return StatInheritanceData.None;
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x005099BB File Offset: 0x00507BBB
		public override bool GetEffectInheritance(DamageClass damageClass)
		{
			return damageClass == DamageClass.Summon;
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600281E RID: 10270 RVA: 0x005099C5 File Offset: 0x00507BC5
		public override bool UseStandardCritCalcs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x005099C8 File Offset: 0x00507BC8
		public override bool ShowStatTooltipLine(Player player, string lineName)
		{
			return lineName != "CritChance";
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x005099D5 File Offset: 0x00507BD5
		public override bool GetPrefixInheritance(DamageClass damageClass)
		{
			return damageClass == DamageClass.Melee;
		}
	}
}
