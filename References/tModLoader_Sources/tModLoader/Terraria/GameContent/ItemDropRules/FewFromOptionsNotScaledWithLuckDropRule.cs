using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.GameContent.ItemDropRules
{
	/// <summary>
	/// Runs multiple rules if successes.
	/// Does not use player luck.
	/// </summary>
	// Token: 0x020005FF RID: 1535
	public class FewFromOptionsNotScaledWithLuckDropRule : IItemDropRule
	{
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060043CD RID: 17357 RVA: 0x0060102A File Offset: 0x005FF22A
		// (set) Token: 0x060043CE RID: 17358 RVA: 0x00601032 File Offset: 0x005FF232
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x060043CF RID: 17359 RVA: 0x0060103C File Offset: 0x005FF23C
		public FewFromOptionsNotScaledWithLuckDropRule(int amount, int chanceDenominator, int chanceNumerator, params int[] options)
		{
			if (amount > options.Length)
			{
				throw new ArgumentOutOfRangeException("amount", "amount must be less than the number of options");
			}
			this.amount = amount;
			this.chanceDenominator = chanceDenominator;
			this.dropIds = options;
			this.chanceNumerator = chanceNumerator;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x0060108E File Offset: 0x005FF28E
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x00601094 File Offset: 0x005FF294
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.rng.Next(this.chanceDenominator) < this.chanceNumerator)
			{
				List<int> savedDropIds = this.dropIds.ToList<int>();
				int count = 0;
				int index = info.rng.Next(savedDropIds.Count);
				CommonCode.DropItem(info, savedDropIds[index], 1, false);
				savedDropIds.RemoveAt(index);
				while (++count < this.amount)
				{
					int index2 = info.rng.Next(savedDropIds.Count);
					CommonCode.DropItem(info, savedDropIds[index2], 1, false);
					savedDropIds.RemoveAt(index2);
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

		// Token: 0x060043D2 RID: 17362 RVA: 0x00601150 File Offset: 0x005FF350
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float pesonalDroprate = (float)this.chanceNumerator / (float)this.chanceDenominator;
			float num2 = pesonalDroprate * ratesInfo.parentDroprateChance;
			float dropRate = (float)this.amount / (float)this.dropIds.Length * num2;
			for (int i = 0; i < this.dropIds.Length; i++)
			{
				drops.Add(new DropRateInfo(this.dropIds[i], 1, 1, dropRate, ratesInfo.conditions));
			}
			Chains.ReportDroprates(this.ChainedRules, pesonalDroprate, drops, ratesInfo);
		}

		// Token: 0x04005A5A RID: 23130
		public int amount;

		// Token: 0x04005A5B RID: 23131
		public int[] dropIds;

		// Token: 0x04005A5C RID: 23132
		public int chanceDenominator;

		// Token: 0x04005A5D RID: 23133
		public int chanceNumerator;
	}
}
