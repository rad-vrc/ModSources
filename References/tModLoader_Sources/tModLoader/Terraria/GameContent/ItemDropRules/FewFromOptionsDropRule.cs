using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.GameContent.ItemDropRules
{
	/// <summary>
	/// Runs multiple rules if successes.
	/// </summary>
	// Token: 0x020005FE RID: 1534
	public class FewFromOptionsDropRule : IItemDropRule
	{
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060043C7 RID: 17351 RVA: 0x00600E87 File Offset: 0x005FF087
		// (set) Token: 0x060043C8 RID: 17352 RVA: 0x00600E8F File Offset: 0x005FF08F
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x060043C9 RID: 17353 RVA: 0x00600E98 File Offset: 0x005FF098
		public FewFromOptionsDropRule(int amount, int chanceDenominator, int chanceNumerator, params int[] options)
		{
			if (amount > options.Length)
			{
				throw new ArgumentOutOfRangeException("amount", "amount must be less than the number of options");
			}
			this.amount = amount;
			this.chanceDenominator = chanceDenominator;
			this.chanceNumerator = chanceNumerator;
			this.dropIds = options;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x00600EEA File Offset: 0x005FF0EA
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x00600EF0 File Offset: 0x005FF0F0
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.player.RollLuck(this.chanceDenominator) < this.chanceNumerator)
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

		// Token: 0x060043CC RID: 17356 RVA: 0x00600FAC File Offset: 0x005FF1AC
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float num = (float)this.chanceNumerator / (float)this.chanceDenominator;
			float num2 = num * ratesInfo.parentDroprateChance;
			float dropRate = 1f / (float)(this.dropIds.Length - this.amount + 1) * num2;
			for (int i = 0; i < this.dropIds.Length; i++)
			{
				drops.Add(new DropRateInfo(this.dropIds[i], 1, 1, dropRate, ratesInfo.conditions));
			}
			Chains.ReportDroprates(this.ChainedRules, num, drops, ratesInfo);
		}

		// Token: 0x04005A55 RID: 23125
		public int[] dropIds;

		// Token: 0x04005A56 RID: 23126
		public int chanceDenominator;

		// Token: 0x04005A57 RID: 23127
		public int chanceNumerator;

		// Token: 0x04005A58 RID: 23128
		public int amount;
	}
}
