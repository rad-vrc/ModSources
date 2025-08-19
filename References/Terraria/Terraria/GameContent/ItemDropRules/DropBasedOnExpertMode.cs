using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000290 RID: 656
	public class DropBasedOnExpertMode : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x0051849E File Offset: 0x0051669E
		// (set) Token: 0x06002038 RID: 8248 RVA: 0x005184A6 File Offset: 0x005166A6
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06002039 RID: 8249 RVA: 0x005184AF File Offset: 0x005166AF
		public DropBasedOnExpertMode(IItemDropRule ruleForNormalMode, IItemDropRule ruleForExpertMode)
		{
			this.ruleForNormalMode = ruleForNormalMode;
			this.ruleForExpertMode = ruleForExpertMode;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x005184D0 File Offset: 0x005166D0
		public bool CanDrop(DropAttemptInfo info)
		{
			if (info.IsExpertMode)
			{
				return this.ruleForExpertMode.CanDrop(info);
			}
			return this.ruleForNormalMode.CanDrop(info);
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x005184F4 File Offset: 0x005166F4
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x00518512 File Offset: 0x00516712
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			if (info.IsExpertMode)
			{
				return resolveAction(this.ruleForExpertMode, info);
			}
			return resolveAction(this.ruleForNormalMode, info);
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00518538 File Offset: 0x00516738
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

		// Token: 0x040046D4 RID: 18132
		public IItemDropRule ruleForNormalMode;

		// Token: 0x040046D5 RID: 18133
		public IItemDropRule ruleForExpertMode;
	}
}
