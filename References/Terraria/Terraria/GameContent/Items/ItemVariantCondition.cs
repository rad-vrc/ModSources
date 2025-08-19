using System;
using Terraria.Localization;

namespace Terraria.GameContent.Items
{
	// Token: 0x020003D9 RID: 985
	public class ItemVariantCondition
	{
		// Token: 0x06002AA0 RID: 10912 RVA: 0x0059A561 File Offset: 0x00598761
		public ItemVariantCondition(NetworkText description, ItemVariantCondition.Condition condition)
		{
			this.Description = description;
			this.IsMet = condition;
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x0059A577 File Offset: 0x00598777
		public override string ToString()
		{
			return this.Description.ToString();
		}

		// Token: 0x04004D46 RID: 19782
		public readonly NetworkText Description;

		// Token: 0x04004D47 RID: 19783
		public readonly ItemVariantCondition.Condition IsMet;

		// Token: 0x02000762 RID: 1890
		// (Invoke) Token: 0x060038D8 RID: 14552
		public delegate bool Condition();
	}
}
