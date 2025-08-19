using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000213 RID: 531
	public class MeleeNoSpeedDamageClass : VanillaDamageClass
	{
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x005098BA File Offset: 0x00507ABA
		protected override string LangKey
		{
			get
			{
				return "LegacyTooltip.2";
			}
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x005098C4 File Offset: 0x00507AC4
		public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
		{
			if (damageClass == DamageClass.Generic || damageClass == DamageClass.Melee)
			{
				StatInheritanceData full = StatInheritanceData.Full;
				full.attackSpeedInheritance = 0f;
				return full;
			}
			return StatInheritanceData.None;
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x005098FA File Offset: 0x00507AFA
		public override bool GetEffectInheritance(DamageClass damageClass)
		{
			return damageClass == DamageClass.Melee;
		}
	}
}
