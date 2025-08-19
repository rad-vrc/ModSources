using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000282 RID: 642
	public interface IItemDropRule
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06002011 RID: 8209
		List<IItemDropRuleChainAttempt> ChainedRules { get; }

		// Token: 0x06002012 RID: 8210
		bool CanDrop(DropAttemptInfo info);

		// Token: 0x06002013 RID: 8211
		void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo);

		// Token: 0x06002014 RID: 8212
		ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info);
	}
}
