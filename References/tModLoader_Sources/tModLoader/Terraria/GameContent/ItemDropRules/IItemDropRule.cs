using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000603 RID: 1539
	public interface IItemDropRule
	{
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060043E6 RID: 17382
		List<IItemDropRuleChainAttempt> ChainedRules { get; }

		// Token: 0x060043E7 RID: 17383
		bool CanDrop(DropAttemptInfo info);

		// Token: 0x060043E8 RID: 17384
		void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo);

		// Token: 0x060043E9 RID: 17385
		ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info);
	}
}
