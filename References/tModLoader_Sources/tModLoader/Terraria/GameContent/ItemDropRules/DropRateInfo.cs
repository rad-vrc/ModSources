using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005FC RID: 1532
	public struct DropRateInfo
	{
		// Token: 0x060043C2 RID: 17346 RVA: 0x00600DB0 File Offset: 0x005FEFB0
		public DropRateInfo(int itemId, int stackMin, int stackMax, float dropRate, List<IItemDropRuleCondition> conditions = null)
		{
			this.itemId = itemId;
			this.stackMin = stackMin;
			this.stackMax = stackMax;
			this.dropRate = dropRate;
			this.conditions = null;
			if (conditions != null && conditions.Count > 0)
			{
				this.conditions = new List<IItemDropRuleCondition>(conditions);
			}
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x00600DFC File Offset: 0x005FEFFC
		public void AddCondition(IItemDropRuleCondition condition)
		{
			if (this.conditions == null)
			{
				this.conditions = new List<IItemDropRuleCondition>();
			}
			this.conditions.Add(condition);
		}

		// Token: 0x04005A4E RID: 23118
		public int itemId;

		// Token: 0x04005A4F RID: 23119
		public int stackMin;

		// Token: 0x04005A50 RID: 23120
		public int stackMax;

		// Token: 0x04005A51 RID: 23121
		public float dropRate;

		// Token: 0x04005A52 RID: 23122
		public List<IItemDropRuleCondition> conditions;
	}
}
