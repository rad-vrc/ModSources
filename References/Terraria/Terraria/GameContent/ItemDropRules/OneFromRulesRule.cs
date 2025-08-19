using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200029B RID: 667
	public class OneFromRulesRule : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x005191BA File Offset: 0x005173BA
		// (set) Token: 0x0600207B RID: 8315 RVA: 0x005191C2 File Offset: 0x005173C2
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x0600207C RID: 8316 RVA: 0x005191CB File Offset: 0x005173CB
		public OneFromRulesRule(int chanceDenominator, params IItemDropRule[] options)
		{
			this.chanceDenominator = chanceDenominator;
			this.options = options;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x0003266D File Offset: 0x0003086D
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x005191EC File Offset: 0x005173EC
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x0051920C File Offset: 0x0051740C
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			if (info.rng.Next(this.chanceDenominator) == 0)
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

		// Token: 0x06002080 RID: 8320 RVA: 0x00519278 File Offset: 0x00517478
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float num = 1f / (float)this.chanceDenominator;
			float num2 = num * ratesInfo.parentDroprateChance;
			float multiplier = 1f / (float)this.options.Length * num2;
			for (int i = 0; i < this.options.Length; i++)
			{
				this.options[i].ReportDroprates(drops, ratesInfo.With(multiplier));
			}
			Chains.ReportDroprates(this.ChainedRules, num, drops, ratesInfo);
		}

		// Token: 0x040046F3 RID: 18163
		public IItemDropRule[] options;

		// Token: 0x040046F4 RID: 18164
		public int chanceDenominator;
	}
}
