using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000604 RID: 1540
	public interface IItemDropRuleChainAttempt
	{
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060043EA RID: 17386
		IItemDropRule RuleToChain { get; }

		// Token: 0x060043EB RID: 17387
		bool CanChainIntoRule(ItemDropAttemptResult parentResult);

		// Token: 0x060043EC RID: 17388
		void ReportDroprates(float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo);
	}
}
