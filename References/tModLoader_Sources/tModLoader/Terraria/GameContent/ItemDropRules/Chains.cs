using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005ED RID: 1517
	public static class Chains
	{
		// Token: 0x06004378 RID: 17272 RVA: 0x005FFB80 File Offset: 0x005FDD80
		public static void ReportDroprates(List<IItemDropRuleChainAttempt> ChainedRules, float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			foreach (IItemDropRuleChainAttempt itemDropRuleChainAttempt in ChainedRules)
			{
				itemDropRuleChainAttempt.ReportDroprates(personalDropRate, drops, ratesInfo);
			}
		}

		/// <summary>
		/// Chains a rule to this rule. The chained rule will be evaluated if this rule meets all conditions but fails a random chance.
		/// <para /> See the <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-NPC-Drops-and-Loot-1.4#chaining-rules">Chaining Rules section of the Basic NPC Drops and Loot Guide</see> for more information and examples.
		/// </summary>
		/// <param name="rule"></param>
		/// <param name="ruleToChain"></param>
		/// <param name="hideLootReport"></param>
		/// <returns></returns>
		// Token: 0x06004379 RID: 17273 RVA: 0x005FFBD0 File Offset: 0x005FDDD0
		public static IItemDropRule OnFailedRoll(this IItemDropRule rule, IItemDropRule ruleToChain, bool hideLootReport = false)
		{
			rule.ChainedRules.Add(new Chains.TryIfFailedRandomRoll(ruleToChain, hideLootReport));
			return ruleToChain;
		}

		/// <summary>
		/// Chains a rule to this rule. The chained rule will be evaluated if this rule succeeds, meaning it met all conditions and passed a random chance, if any.
		/// <para /> See the <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-NPC-Drops-and-Loot-1.4#chaining-rules">Chaining Rules section of the Basic NPC Drops and Loot Guide</see> for more information and examples.
		/// </summary>
		/// <param name="rule"></param>
		/// <param name="ruleToChain"></param>
		/// <param name="hideLootReport"></param>
		/// <returns></returns>
		// Token: 0x0600437A RID: 17274 RVA: 0x005FFBE5 File Offset: 0x005FDDE5
		public static IItemDropRule OnSuccess(this IItemDropRule rule, IItemDropRule ruleToChain, bool hideLootReport = false)
		{
			rule.ChainedRules.Add(new Chains.TryIfSucceeded(ruleToChain, hideLootReport));
			return ruleToChain;
		}

		/// <summary>
		/// Chains a rule to this rule. The chained rule will be evaluated if this rule doesn't meets all conditions.
		/// <para /> See the <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-NPC-Drops-and-Loot-1.4#chaining-rules">Chaining Rules section of the Basic NPC Drops and Loot Guide</see> for more information and examples.
		/// <para /> Note that this rule will appear in the bestiary according to the display conditions, if any, of the rule it is chained to. Either set <paramref name="hideLootReport" /> to hide this rule or and use a different drop rule that handles both cases of a condition directly like <see cref="T:Terraria.GameContent.ItemDropRules.DropBasedOnExpertMode" /> instead if it is important to hide this rule when conditions are not met.  
		/// </summary>
		/// <param name="rule"></param>
		/// <param name="ruleToChain"></param>
		/// <param name="hideLootReport"></param>
		/// <returns></returns>
		// Token: 0x0600437B RID: 17275 RVA: 0x005FFBFA File Offset: 0x005FDDFA
		public static IItemDropRule OnFailedConditions(this IItemDropRule rule, IItemDropRule ruleToChain, bool hideLootReport = false)
		{
			rule.ChainedRules.Add(new Chains.TryIfDoesntFillConditions(ruleToChain, hideLootReport));
			return ruleToChain;
		}

		// Token: 0x02000C7A RID: 3194
		public class TryIfFailedRandomRoll : IItemDropRuleChainAttempt
		{
			// Token: 0x1700096C RID: 2412
			// (get) Token: 0x06006033 RID: 24627 RVA: 0x006D1EF4 File Offset: 0x006D00F4
			// (set) Token: 0x06006034 RID: 24628 RVA: 0x006D1EFC File Offset: 0x006D00FC
			public IItemDropRule RuleToChain { get; private set; }

			// Token: 0x06006035 RID: 24629 RVA: 0x006D1F05 File Offset: 0x006D0105
			public TryIfFailedRandomRoll(IItemDropRule rule, bool hideLootReport = false)
			{
				this.RuleToChain = rule;
				this.hideLootReport = hideLootReport;
			}

			// Token: 0x06006036 RID: 24630 RVA: 0x006D1F1B File Offset: 0x006D011B
			public bool CanChainIntoRule(ItemDropAttemptResult parentResult)
			{
				return parentResult.State == ItemDropAttemptResultState.FailedRandomRoll;
			}

			// Token: 0x06006037 RID: 24631 RVA: 0x006D1F26 File Offset: 0x006D0126
			public void ReportDroprates(float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
			{
				if (!this.hideLootReport)
				{
					this.RuleToChain.ReportDroprates(drops, ratesInfo.With(1f - personalDropRate));
				}
			}

			// Token: 0x040079DD RID: 31197
			public bool hideLootReport;
		}

		// Token: 0x02000C7B RID: 3195
		public class TryIfSucceeded : IItemDropRuleChainAttempt
		{
			// Token: 0x1700096D RID: 2413
			// (get) Token: 0x06006038 RID: 24632 RVA: 0x006D1F4A File Offset: 0x006D014A
			// (set) Token: 0x06006039 RID: 24633 RVA: 0x006D1F52 File Offset: 0x006D0152
			public IItemDropRule RuleToChain { get; private set; }

			// Token: 0x0600603A RID: 24634 RVA: 0x006D1F5B File Offset: 0x006D015B
			public TryIfSucceeded(IItemDropRule rule, bool hideLootReport = false)
			{
				this.RuleToChain = rule;
				this.hideLootReport = hideLootReport;
			}

			// Token: 0x0600603B RID: 24635 RVA: 0x006D1F71 File Offset: 0x006D0171
			public bool CanChainIntoRule(ItemDropAttemptResult parentResult)
			{
				return parentResult.State == ItemDropAttemptResultState.Success;
			}

			// Token: 0x0600603C RID: 24636 RVA: 0x006D1F7C File Offset: 0x006D017C
			public void ReportDroprates(float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
			{
				if (!this.hideLootReport)
				{
					this.RuleToChain.ReportDroprates(drops, ratesInfo.With(personalDropRate));
				}
			}

			// Token: 0x040079DF RID: 31199
			public bool hideLootReport;
		}

		// Token: 0x02000C7C RID: 3196
		public class TryIfDoesntFillConditions : IItemDropRuleChainAttempt
		{
			// Token: 0x1700096E RID: 2414
			// (get) Token: 0x0600603D RID: 24637 RVA: 0x006D1F9A File Offset: 0x006D019A
			// (set) Token: 0x0600603E RID: 24638 RVA: 0x006D1FA2 File Offset: 0x006D01A2
			public IItemDropRule RuleToChain { get; private set; }

			// Token: 0x0600603F RID: 24639 RVA: 0x006D1FAB File Offset: 0x006D01AB
			public TryIfDoesntFillConditions(IItemDropRule rule, bool hideLootReport = false)
			{
				this.RuleToChain = rule;
				this.hideLootReport = hideLootReport;
			}

			// Token: 0x06006040 RID: 24640 RVA: 0x006D1FC1 File Offset: 0x006D01C1
			public bool CanChainIntoRule(ItemDropAttemptResult parentResult)
			{
				return parentResult.State == ItemDropAttemptResultState.DoesntFillConditions;
			}

			// Token: 0x06006041 RID: 24641 RVA: 0x006D1FCC File Offset: 0x006D01CC
			public void ReportDroprates(float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
			{
				if (!this.hideLootReport)
				{
					this.RuleToChain.ReportDroprates(drops, ratesInfo.With(personalDropRate));
				}
			}

			// Token: 0x040079E1 RID: 31201
			public bool hideLootReport;
		}
	}
}
