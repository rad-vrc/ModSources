using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000280 RID: 640
	public struct DropRateInfo
	{
		// Token: 0x0600200C RID: 8204 RVA: 0x00517B7C File Offset: 0x00515D7C
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

		// Token: 0x0600200D RID: 8205 RVA: 0x00517BC8 File Offset: 0x00515DC8
		public void AddCondition(IItemDropRuleCondition condition)
		{
			if (this.conditions == null)
			{
				this.conditions = new List<IItemDropRuleCondition>();
			}
			this.conditions.Add(condition);
		}

		// Token: 0x040046BD RID: 18109
		public int itemId;

		// Token: 0x040046BE RID: 18110
		public int stackMin;

		// Token: 0x040046BF RID: 18111
		public int stackMax;

		// Token: 0x040046C0 RID: 18112
		public float dropRate;

		// Token: 0x040046C1 RID: 18113
		public List<IItemDropRuleCondition> conditions;
	}
}
