using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005F5 RID: 1525
	public class DropBasedOnExpertMode : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06004399 RID: 17305 RVA: 0x00600683 File Offset: 0x005FE883
		// (set) Token: 0x0600439A RID: 17306 RVA: 0x0060068B File Offset: 0x005FE88B
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x0600439B RID: 17307 RVA: 0x00600694 File Offset: 0x005FE894
		public DropBasedOnExpertMode(IItemDropRule ruleForNormalMode, IItemDropRule ruleForExpertMode)
		{
			this.ruleForNormalMode = ruleForNormalMode;
			this.ruleForExpertMode = ruleForExpertMode;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x006006B5 File Offset: 0x005FE8B5
		public bool CanDrop(DropAttemptInfo info)
		{
			if (info.IsExpertMode)
			{
				return this.ruleForExpertMode.CanDrop(info);
			}
			return this.ruleForNormalMode.CanDrop(info);
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x006006D8 File Offset: 0x005FE8D8
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x006006F6 File Offset: 0x005FE8F6
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			if (info.IsExpertMode)
			{
				return resolveAction(this.ruleForExpertMode, info);
			}
			return resolveAction(this.ruleForNormalMode, info);
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x0060071C File Offset: 0x005FE91C
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			DropRateInfoChainFeed ratesInfo2 = ratesInfo.With(1f);
			ratesInfo2.AddCondition(new Conditions.IsExpert());
			this.ruleForExpertMode.ReportDroprates(drops, ratesInfo2);
			DropRateInfoChainFeed ratesInfo3 = ratesInfo.With(1f);
			ratesInfo3.AddCondition(new Conditions.NotExpert());
			this.ruleForNormalMode.ReportDroprates(drops, ratesInfo3);
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x04005A3E RID: 23102
		public IItemDropRule ruleForNormalMode;

		// Token: 0x04005A3F RID: 23103
		public IItemDropRule ruleForExpertMode;
	}
}
