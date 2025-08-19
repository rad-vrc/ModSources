using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000219 RID: 537
	public class ThrowingDamageClass : VanillaDamageClass
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x00509A2F File Offset: 0x00507C2F
		protected override string LangKey
		{
			get
			{
				return "LegacyTooltip.58";
			}
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x00509A36 File Offset: 0x00507C36
		public override bool GetPrefixInheritance(DamageClass damageClass)
		{
			return damageClass == DamageClass.Ranged;
		}
	}
}
