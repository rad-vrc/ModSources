using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000218 RID: 536
	public class MagicSummonHybridDamageClass : VanillaDamageClass
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x005099E7 File Offset: 0x00507BE7
		protected override string LangKey
		{
			get
			{
				return "tModLoader.MagicSummonHybridDamageClass";
			}
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x005099EE File Offset: 0x00507BEE
		public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
		{
			if (damageClass == DamageClass.Generic || damageClass == DamageClass.Magic || damageClass == DamageClass.Summon)
			{
				return StatInheritanceData.Full;
			}
			return StatInheritanceData.None;
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x00509A13 File Offset: 0x00507C13
		public override bool GetEffectInheritance(DamageClass damageClass)
		{
			return damageClass == DamageClass.Magic || damageClass == DamageClass.Summon;
		}
	}
}
