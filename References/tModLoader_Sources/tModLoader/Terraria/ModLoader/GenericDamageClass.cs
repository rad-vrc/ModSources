using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000211 RID: 529
	public class GenericDamageClass : VanillaDamageClass
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x00509886 File Offset: 0x00507A86
		protected override string LangKey
		{
			get
			{
				return "LegacyTooltip.55";
			}
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x0050988D File Offset: 0x00507A8D
		public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
		{
			return StatInheritanceData.None;
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x00509894 File Offset: 0x00507A94
		public unsafe override void SetDefaultStats(Player player)
		{
			*player.GetCritChance(this) = 4f;
		}
	}
}
