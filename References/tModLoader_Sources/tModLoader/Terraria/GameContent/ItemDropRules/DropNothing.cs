using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005F9 RID: 1529
	public class DropNothing : IItemDropRule
	{
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060043B1 RID: 17329 RVA: 0x00600AA8 File Offset: 0x005FECA8
		// (set) Token: 0x060043B2 RID: 17330 RVA: 0x00600AB0 File Offset: 0x005FECB0
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x060043B3 RID: 17331 RVA: 0x00600AB9 File Offset: 0x005FECB9
		public DropNothing()
		{
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x00600ACC File Offset: 0x005FECCC
		public bool CanDrop(DropAttemptInfo info)
		{
			return false;
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x00600AD0 File Offset: 0x005FECD0
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DoesntFillConditions
			};
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x00600AEE File Offset: 0x005FECEE
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}
	}
}
