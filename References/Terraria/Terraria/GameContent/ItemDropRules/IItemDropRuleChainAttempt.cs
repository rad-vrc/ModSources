using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000286 RID: 646
	public interface IItemDropRuleChainAttempt
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06002016 RID: 8214
		IItemDropRule RuleToChain { get; }

		// Token: 0x06002017 RID: 8215
		bool CanChainIntoRule(ItemDropAttemptResult parentResult);

		// Token: 0x06002018 RID: 8216
		void ReportDroprates(float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo);
	}
}
