using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	/// <summary>
	/// Runs the provided rules in order, stopping after a rule succeeds.<br />
	/// </summary>
	// Token: 0x02000615 RID: 1557
	public class SequentialRulesRule : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x0600448B RID: 17547 RVA: 0x0060AB1A File Offset: 0x00608D1A
		// (set) Token: 0x0600448C RID: 17548 RVA: 0x0060AB22 File Offset: 0x00608D22
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x0600448D RID: 17549 RVA: 0x0060AB2B File Offset: 0x00608D2B
		public SequentialRulesRule(int chanceDenominator, params IItemDropRule[] rules)
		{
			this.chanceDenominator = chanceDenominator;
			this.rules = rules;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x0060AB4C File Offset: 0x00608D4C
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x0060AB50 File Offset: 0x00608D50
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x0060AB70 File Offset: 0x00608D70
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			ItemDropAttemptResult result = default(ItemDropAttemptResult);
			if (info.player.RollLuck(this.chanceDenominator) == 0)
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

		// Token: 0x06004491 RID: 17553 RVA: 0x0060ABD4 File Offset: 0x00608DD4
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			for (int i = this.rules.Length - 1; i >= 1; i--)
			{
				this.rules[i - 1].OnFailedRoll(this.rules[i], false);
			}
			float selfChance = 1f / (float)this.chanceDenominator;
			float baseChance = ratesInfo.parentDroprateChance * selfChance;
			this.rules[0].ReportDroprates(drops, ratesInfo.With(baseChance));
			Chains.ReportDroprates(this.ChainedRules, selfChance, drops, ratesInfo);
			for (int j = 0; j < this.rules.Length - 1; j++)
			{
				this.rules[j].ChainedRules.RemoveAt(this.rules[j].ChainedRules.Count - 1);
			}
		}

		// Token: 0x04005A8B RID: 23179
		public IItemDropRule[] rules;

		// Token: 0x04005A8C RID: 23180
		public int chanceDenominator;
	}
}
