using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000281 RID: 641
	public struct DropRateInfoChainFeed
	{
		// Token: 0x0600200E RID: 8206 RVA: 0x00517BE9 File Offset: 0x00515DE9
		public void AddCondition(IItemDropRuleCondition condition)
		{
			if (this.conditions == null)
			{
				this.conditions = new List<IItemDropRuleCondition>();
			}
			this.conditions.Add(condition);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00517C0A File Offset: 0x00515E0A
		public DropRateInfoChainFeed(float droprate)
		{
			this.parentDroprateChance = droprate;
			this.conditions = null;
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x00517C1C File Offset: 0x00515E1C
		public DropRateInfoChainFeed With(float multiplier)
		{
			DropRateInfoChainFeed result = new DropRateInfoChainFeed(this.parentDroprateChance * multiplier);
			if (this.conditions != null)
			{
				result.conditions = new List<IItemDropRuleCondition>(this.conditions);
			}
			return result;
		}

		// Token: 0x040046C2 RID: 18114
		public float parentDroprateChance;

		// Token: 0x040046C3 RID: 18115
		public List<IItemDropRuleCondition> conditions;
	}
}
