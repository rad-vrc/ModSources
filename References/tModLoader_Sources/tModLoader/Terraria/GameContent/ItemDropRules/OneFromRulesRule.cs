using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000613 RID: 1555
	public class OneFromRulesRule : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x0600447B RID: 17531 RVA: 0x0060A831 File Offset: 0x00608A31
		// (set) Token: 0x0600447C RID: 17532 RVA: 0x0060A839 File Offset: 0x00608A39
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x0600447D RID: 17533 RVA: 0x0060A842 File Offset: 0x00608A42
		public OneFromRulesRule(int chanceDenominator, params IItemDropRule[] options) : this(chanceDenominator, 1, options)
		{
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x0060A84D File Offset: 0x00608A4D
		public OneFromRulesRule(int chanceDenominator, int chanceNumerator, params IItemDropRule[] options)
		{
			this.chanceDenominator = chanceDenominator;
			this.chanceNumerator = chanceNumerator;
			this.options = options;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
			if (chanceNumerator > chanceDenominator)
			{
				throw new ArgumentOutOfRangeException("chanceNumerator", "chanceNumerator must be lesser or equal to chanceDenominator.");
			}
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x0060A889 File Offset: 0x00608A89
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x0060A88C File Offset: 0x00608A8C
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x0060A8AC File Offset: 0x00608AAC
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			if (info.rng.Next(this.chanceDenominator) < this.chanceNumerator)
			{
				int num = info.rng.Next(this.options.Length);
				resolveAction(this.options[num], info);
				return new ItemDropAttemptResult
				{
					State = ItemDropAttemptResultState.Success
				};
			}
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.FailedRandomRoll
			};
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x0060A91C File Offset: 0x00608B1C
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float num = (float)this.chanceNumerator / (float)this.chanceDenominator;
			float multiplier = 1f / (float)this.options.Length * num;
			for (int i = 0; i < this.options.Length; i++)
			{
				this.options[i].ReportDroprates(drops, ratesInfo.With(multiplier));
			}
			Chains.ReportDroprates(this.ChainedRules, num, drops, ratesInfo);
		}

		// Token: 0x04005A83 RID: 23171
		public IItemDropRule[] options;

		// Token: 0x04005A84 RID: 23172
		public int chanceDenominator;

		// Token: 0x04005A85 RID: 23173
		public int chanceNumerator;
	}
}
