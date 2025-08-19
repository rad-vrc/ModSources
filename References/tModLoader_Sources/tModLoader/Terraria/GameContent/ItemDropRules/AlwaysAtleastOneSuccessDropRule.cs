using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	/// <summary>
	/// Re-runs all drop rules if none succeeded.
	/// </summary>
	// Token: 0x020005EC RID: 1516
	public class AlwaysAtleastOneSuccessDropRule : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06004370 RID: 17264 RVA: 0x005FF9C8 File Offset: 0x005FDBC8
		// (set) Token: 0x06004371 RID: 17265 RVA: 0x005FF9D0 File Offset: 0x005FDBD0
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06004372 RID: 17266 RVA: 0x005FF9D9 File Offset: 0x005FDBD9
		public AlwaysAtleastOneSuccessDropRule(params IItemDropRule[] rules)
		{
			this.rules = rules;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x005FF9F3 File Offset: 0x005FDBF3
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x005FF9F8 File Offset: 0x005FDBF8
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x005FFA18 File Offset: 0x005FDC18
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			bool anyDropped;
			do
			{
				anyDropped = false;
				foreach (IItemDropRule rule in this.rules)
				{
					if (resolveAction(rule, info).State == ItemDropAttemptResultState.Success)
					{
						anyDropped = true;
					}
				}
			}
			while (!anyDropped);
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x005FFA68 File Offset: 0x005FDC68
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float reroll = 1f;
			foreach (IItemDropRule rule in this.rules)
			{
				reroll *= 1f - AlwaysAtleastOneSuccessDropRule.GetPersonalDropRate(rule);
			}
			float scale = (reroll == 1f) ? 1f : (1f / (1f - reroll));
			IItemDropRule[] array = this.rules;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ReportDroprates(drops, ratesInfo.With(scale));
			}
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x005FFAFC File Offset: 0x005FDCFC
		public static float GetPersonalDropRate(IItemDropRule rule)
		{
			IItemDropRuleChainAttempt[] chained = rule.ChainedRules.ToArray();
			rule.ChainedRules.Clear();
			float dropRate = 0f;
			rule.ChainedRules.Add(new AlwaysAtleastOneSuccessDropRule.PersonalDropRateReportingRule(delegate(float f)
			{
				dropRate = f;
			}));
			rule.ReportDroprates(new List<DropRateInfo>(), new DropRateInfoChainFeed(1f));
			rule.ChainedRules.Clear();
			rule.ChainedRules.AddRange(chained);
			return dropRate;
		}

		// Token: 0x04005A2B RID: 23083
		public IItemDropRule[] rules;

		// Token: 0x02000C78 RID: 3192
		private class PersonalDropRateReportingRule : IItemDropRuleChainAttempt
		{
			// Token: 0x0600602D RID: 24621 RVA: 0x006D1EB8 File Offset: 0x006D00B8
			public PersonalDropRateReportingRule(Action<float> report)
			{
				this.report = report;
			}

			// Token: 0x1700096B RID: 2411
			// (get) Token: 0x0600602E RID: 24622 RVA: 0x006D1EC7 File Offset: 0x006D00C7
			public IItemDropRule RuleToChain
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x0600602F RID: 24623 RVA: 0x006D1ECE File Offset: 0x006D00CE
			public bool CanChainIntoRule(ItemDropAttemptResult parentResult)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06006030 RID: 24624 RVA: 0x006D1ED5 File Offset: 0x006D00D5
			public void ReportDroprates(float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
			{
				this.report(personalDropRate);
			}

			// Token: 0x040079DB RID: 31195
			private readonly Action<float> report;
		}
	}
}
