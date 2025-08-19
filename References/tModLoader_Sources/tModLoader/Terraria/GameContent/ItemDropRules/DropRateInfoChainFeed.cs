using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005FD RID: 1533
	public struct DropRateInfoChainFeed
	{
		// Token: 0x060043C4 RID: 17348 RVA: 0x00600E1D File Offset: 0x005FF01D
		public void AddCondition(IItemDropRuleCondition condition)
		{
			if (this.conditions == null)
			{
				this.conditions = new List<IItemDropRuleCondition>();
			}
			this.conditions.Add(condition);
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x00600E3E File Offset: 0x005FF03E
		public DropRateInfoChainFeed(float droprate)
		{
			this.parentDroprateChance = droprate;
			this.conditions = null;
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x00600E50 File Offset: 0x005FF050
		public DropRateInfoChainFeed With(float multiplier)
		{
			DropRateInfoChainFeed result = new DropRateInfoChainFeed(this.parentDroprateChance * multiplier);
			if (this.conditions != null)
			{
				result.conditions = new List<IItemDropRuleCondition>(this.conditions);
			}
			return result;
		}

		// Token: 0x04005A53 RID: 23123
		public float parentDroprateChance;

		// Token: 0x04005A54 RID: 23124
		public List<IItemDropRuleCondition> conditions;
	}
}
