using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000296 RID: 662
	public class LeadingConditionRule : IItemDropRule
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x00518BEA File Offset: 0x00516DEA
		// (set) Token: 0x0600205D RID: 8285 RVA: 0x00518BF2 File Offset: 0x00516DF2
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x0600205E RID: 8286 RVA: 0x00518BFB File Offset: 0x00516DFB
		public LeadingConditionRule(IItemDropRuleCondition condition)
		{
			this.condition = condition;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x00518C15 File Offset: 0x00516E15
		public bool CanDrop(DropAttemptInfo info)
		{
			return this.condition.CanDrop(info);
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x00518C23 File Offset: 0x00516E23
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			ratesInfo.AddCondition(this.condition);
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00518C44 File Offset: 0x00516E44
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x040046E2 RID: 18146
		public IItemDropRuleCondition condition;
	}
}
