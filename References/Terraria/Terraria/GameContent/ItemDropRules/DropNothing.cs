using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200028F RID: 655
	public class DropNothing : IItemDropRule
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x00518445 File Offset: 0x00516645
		// (set) Token: 0x06002032 RID: 8242 RVA: 0x0051844D File Offset: 0x0051664D
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06002033 RID: 8243 RVA: 0x00518456 File Offset: 0x00516656
		public DropNothing()
		{
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public bool CanDrop(DropAttemptInfo info)
		{
			return false;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0051846C File Offset: 0x0051666C
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DoesntFillConditions
			};
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x0051848A File Offset: 0x0051668A
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}
	}
}
