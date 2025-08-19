using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200029C RID: 668
	public static class Chains
	{
		// Token: 0x06002081 RID: 8321 RVA: 0x005192E8 File Offset: 0x005174E8
		public static void ReportDroprates(List<IItemDropRuleChainAttempt> ChainedRules, float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			foreach (IItemDropRuleChainAttempt itemDropRuleChainAttempt in ChainedRules)
			{
				itemDropRuleChainAttempt.ReportDroprates(personalDropRate, drops, ratesInfo);
			}
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x00519338 File Offset: 0x00517538
		public static IItemDropRule OnFailedRoll(this IItemDropRule rule, IItemDropRule ruleToChain, bool hideLootReport = false)
		{
			rule.ChainedRules.Add(new Chains.TryIfFailedRandomRoll(ruleToChain, hideLootReport));
			return ruleToChain;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0051934D File Offset: 0x0051754D
		public static IItemDropRule OnSuccess(this IItemDropRule rule, IItemDropRule ruleToChain, bool hideLootReport = false)
		{
			rule.ChainedRules.Add(new Chains.TryIfSucceeded(ruleToChain, hideLootReport));
			return ruleToChain;
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x00519362 File Offset: 0x00517562
		public static IItemDropRule OnFailedConditions(this IItemDropRule rule, IItemDropRule ruleToChain, bool hideLootReport = false)
		{
			rule.ChainedRules.Add(new Chains.TryIfDoesntFillConditions(ruleToChain, hideLootReport));
			return ruleToChain;
		}

		// Token: 0x0200064B RID: 1611
		public class TryIfFailedRandomRoll : IItemDropRuleChainAttempt
		{
			// Token: 0x170003D1 RID: 977
			// (get) Token: 0x0600340B RID: 13323 RVA: 0x006077D4 File Offset: 0x006059D4
			// (set) Token: 0x0600340C RID: 13324 RVA: 0x006077DC File Offset: 0x006059DC
			public IItemDropRule RuleToChain { get; private set; }

			// Token: 0x0600340D RID: 13325 RVA: 0x006077E5 File Offset: 0x006059E5
			public TryIfFailedRandomRoll(IItemDropRule rule, bool hideLootReport = false)
			{
				this.RuleToChain = rule;
				this.hideLootReport = hideLootReport;
			}

			// Token: 0x0600340E RID: 13326 RVA: 0x006077FB File Offset: 0x006059FB
			public bool CanChainIntoRule(ItemDropAttemptResult parentResult)
			{
				return parentResult.State == ItemDropAttemptResultState.FailedRandomRoll;
			}

			// Token: 0x0600340F RID: 13327 RVA: 0x00607806 File Offset: 0x00605A06
			public void ReportDroprates(float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
			{
				if (this.hideLootReport)
				{
					return;
				}
				this.RuleToChain.ReportDroprates(drops, ratesInfo.With(1f - personalDropRate));
			}

			// Token: 0x04006178 RID: 24952
			public bool hideLootReport;
		}

		// Token: 0x0200064C RID: 1612
		public class TryIfSucceeded : IItemDropRuleChainAttempt
		{
			// Token: 0x170003D2 RID: 978
			// (get) Token: 0x06003410 RID: 13328 RVA: 0x0060782B File Offset: 0x00605A2B
			// (set) Token: 0x06003411 RID: 13329 RVA: 0x00607833 File Offset: 0x00605A33
			public IItemDropRule RuleToChain { get; private set; }

			// Token: 0x06003412 RID: 13330 RVA: 0x0060783C File Offset: 0x00605A3C
			public TryIfSucceeded(IItemDropRule rule, bool hideLootReport = false)
			{
				this.RuleToChain = rule;
				this.hideLootReport = hideLootReport;
			}

			// Token: 0x06003413 RID: 13331 RVA: 0x00607852 File Offset: 0x00605A52
			public bool CanChainIntoRule(ItemDropAttemptResult parentResult)
			{
				return parentResult.State == ItemDropAttemptResultState.Success;
			}

			// Token: 0x06003414 RID: 13332 RVA: 0x0060785D File Offset: 0x00605A5D
			public void ReportDroprates(float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
			{
				if (this.hideLootReport)
				{
					return;
				}
				this.RuleToChain.ReportDroprates(drops, ratesInfo.With(personalDropRate));
			}

			// Token: 0x0400617A RID: 24954
			public bool hideLootReport;
		}

		// Token: 0x0200064D RID: 1613
		public class TryIfDoesntFillConditions : IItemDropRuleChainAttempt
		{
			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x06003415 RID: 13333 RVA: 0x0060787C File Offset: 0x00605A7C
			// (set) Token: 0x06003416 RID: 13334 RVA: 0x00607884 File Offset: 0x00605A84
			public IItemDropRule RuleToChain { get; private set; }

			// Token: 0x06003417 RID: 13335 RVA: 0x0060788D File Offset: 0x00605A8D
			public TryIfDoesntFillConditions(IItemDropRule rule, bool hideLootReport = false)
			{
				this.RuleToChain = rule;
				this.hideLootReport = hideLootReport;
			}

			// Token: 0x06003418 RID: 13336 RVA: 0x006078A3 File Offset: 0x00605AA3
			public bool CanChainIntoRule(ItemDropAttemptResult parentResult)
			{
				return parentResult.State == ItemDropAttemptResultState.DoesntFillConditions;
			}

			// Token: 0x06003419 RID: 13337 RVA: 0x006078AE File Offset: 0x00605AAE
			public void ReportDroprates(float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
			{
				if (this.hideLootReport)
				{
					return;
				}
				this.RuleToChain.ReportDroprates(drops, ratesInfo.With(personalDropRate));
			}

			// Token: 0x0400617C RID: 24956
			public bool hideLootReport;
		}
	}
}
