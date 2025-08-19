using System;

namespace Terraria.ModLoader
{
	// Token: 0x020001FB RID: 507
	public struct StatInheritanceData
	{
		// Token: 0x0600271A RID: 10010 RVA: 0x00502208 File Offset: 0x00500408
		public StatInheritanceData(float damageInheritance = 0f, float critChanceInheritance = 0f, float attackSpeedInheritance = 0f, float armorPenInheritance = 0f, float knockbackInheritance = 0f)
		{
			this.damageInheritance = damageInheritance;
			this.critChanceInheritance = critChanceInheritance;
			this.attackSpeedInheritance = attackSpeedInheritance;
			this.armorPenInheritance = armorPenInheritance;
			this.knockbackInheritance = knockbackInheritance;
		}

		// Token: 0x040018B7 RID: 6327
		public static readonly StatInheritanceData Full = new StatInheritanceData(1f, 1f, 1f, 1f, 1f);

		// Token: 0x040018B8 RID: 6328
		public static readonly StatInheritanceData None = new StatInheritanceData(0f, 0f, 0f, 0f, 0f);

		// Token: 0x040018B9 RID: 6329
		public float damageInheritance;

		// Token: 0x040018BA RID: 6330
		public float critChanceInheritance;

		// Token: 0x040018BB RID: 6331
		public float attackSpeedInheritance;

		// Token: 0x040018BC RID: 6332
		public float armorPenInheritance;

		// Token: 0x040018BD RID: 6333
		public float knockbackInheritance;
	}
}
