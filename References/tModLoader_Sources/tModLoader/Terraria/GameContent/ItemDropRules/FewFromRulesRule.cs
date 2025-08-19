using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.GameContent.ItemDropRules
{
	/// <summary>
	/// Runs multiple drop rules if successes.
	/// </summary>
	// Token: 0x02000600 RID: 1536
	public class FewFromRulesRule : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060043D3 RID: 17363 RVA: 0x006011C7 File Offset: 0x005FF3C7
		// (set) Token: 0x060043D4 RID: 17364 RVA: 0x006011CF File Offset: 0x005FF3CF
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x060043D5 RID: 17365 RVA: 0x006011D8 File Offset: 0x005FF3D8
		public FewFromRulesRule(int amount, int chanceDenominator, params IItemDropRule[] options)
		{
			if (amount > options.Length)
			{
				throw new ArgumentOutOfRangeException("amount", "amount must be less than the number of options");
			}
			this.amount = amount;
			this.chanceDenominator = chanceDenominator;
			this.options = options;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x00601216 File Offset: 0x005FF416
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x0060121C File Offset: 0x005FF41C
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x0060123C File Offset: 0x005FF43C
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			if (info.rng.Next(this.chanceDenominator) == 0)
			{
				List<IItemDropRule> savedDropIds = this.options.ToList<IItemDropRule>();
				int count = 0;
				int num = info.rng.Next(savedDropIds.Count);
				resolveAction(savedDropIds[num], info);
				savedDropIds.RemoveAt(num);
				while (++count < this.amount)
				{
					num = info.rng.Next(savedDropIds.Count);
					resolveAction(savedDropIds[num], info);
					savedDropIds.RemoveAt(num);
				}
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

		// Token: 0x060043D9 RID: 17369 RVA: 0x006012EC File Offset: 0x005FF4EC
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float personalDroprate = 1f / (float)this.chanceDenominator;
			float multiplier = (float)this.amount / (float)this.options.Length * personalDroprate;
			for (int i = 0; i < this.options.Length; i++)
			{
				this.options[i].ReportDroprates(drops, ratesInfo.With(multiplier));
			}
			Chains.ReportDroprates(this.ChainedRules, personalDroprate, drops, ratesInfo);
		}

		// Token: 0x04005A5F RID: 23135
		public int amount;

		// Token: 0x04005A60 RID: 23136
		public IItemDropRule[] options;

		// Token: 0x04005A61 RID: 23137
		public int chanceDenominator;
	}
}
