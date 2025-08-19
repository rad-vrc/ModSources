using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000210 RID: 528
	public class DefaultDamageClass : VanillaDamageClass
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06002805 RID: 10245 RVA: 0x00509870 File Offset: 0x00507A70
		protected override string LangKey
		{
			get
			{
				return "LegacyTooltip.55";
			}
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x00509877 File Offset: 0x00507A77
		public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
		{
			return StatInheritanceData.None;
		}
	}
}
