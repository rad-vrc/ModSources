using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	/// <summary>
	/// Runs the provided rules in order, stopping after a rule succeeds.<br />
	/// Does not use player luck.<br />
	/// </summary>
	// Token: 0x02000614 RID: 1556
	public class SequentialRulesNotScalingWithLuckRule : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06004483 RID: 17539 RVA: 0x0060A982 File Offset: 0x00608B82
		// (set) Token: 0x06004484 RID: 17540 RVA: 0x0060A98A File Offset: 0x00608B8A
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06004485 RID: 17541 RVA: 0x0060A993 File Offset: 0x00608B93
		public SequentialRulesNotScalingWithLuckRule(int chanceDenominator, params IItemDropRule[] rules)
		{
			this.chanceDenominator = chanceDenominator;
			this.chanceNumerator = 1;
			this.rules = rules;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x0060A9BB File Offset: 0x00608BBB
		public SequentialRulesNotScalingWithLuckRule(int chanceDenominator, int chanceNumerator, params IItemDropRule[] rules)
		{
			this.chanceDenominator = chanceDenominator;
			this.chanceNumerator = chanceNumerator;
			this.rules = rules;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x0060A9E3 File Offset: 0x00608BE3
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x0060A9E8 File Offset: 0x00608BE8
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x0060AA08 File Offset: 0x00608C08
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			ItemDropAttemptResult result = default(ItemDropAttemptResult);
			if (info.rng.Next(this.chanceDenominator) < this.chanceNumerator)
			{
				for (int i = 0; i < this.rules.Length; i++)
				{
					IItemDropRule rule = this.rules[i];
					result = resolveAction(rule, info);
					if (result.State == ItemDropAttemptResultState.Success)
					{
						return result;
					}
				}
			}
			result.State = ItemDropAttemptResultState.FailedRandomRoll;
			return result;
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x0060AA70 File Offset: 0x00608C70
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			for (int i = this.rules.Length - 1; i >= 1; i--)
			{
				this.rules[i - 1].OnFailedRoll(this.rules[i], false);
			}
			float selfChance = (float)this.chanceNumerator / (float)this.chanceDenominator;
			this.rules[0].ReportDroprates(drops, ratesInfo.With(selfChance));
			Chains.ReportDroprates(this.ChainedRules, selfChance, drops, ratesInfo);
			for (int j = 0; j < this.rules.Length - 1; j++)
			{
				this.rules[j].ChainedRules.RemoveAt(this.rules[j].ChainedRules.Count - 1);
			}
		}

		// Token: 0x04005A87 RID: 23175
		public IItemDropRule[] rules;

		// Token: 0x04005A88 RID: 23176
		public int chanceDenominator;

		// Token: 0x04005A89 RID: 23177
		public int chanceNumerator;
	}
}
