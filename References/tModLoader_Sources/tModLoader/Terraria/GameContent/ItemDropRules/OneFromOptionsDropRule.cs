using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000611 RID: 1553
	public class OneFromOptionsDropRule : IItemDropRule
	{
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x0060A5F2 File Offset: 0x006087F2
		// (set) Token: 0x06004470 RID: 17520 RVA: 0x0060A5FA File Offset: 0x006087FA
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06004471 RID: 17521 RVA: 0x0060A603 File Offset: 0x00608803
		public OneFromOptionsDropRule(int chanceDenominator, int chanceNumerator, params int[] options)
		{
			this.chanceDenominator = chanceDenominator;
			this.chanceNumerator = chanceNumerator;
			this.dropIds = options;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06004472 RID: 17522 RVA: 0x0060A62B File Offset: 0x0060882B
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x0060A630 File Offset: 0x00608830
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.player.RollLuck(this.chanceDenominator) < this.chanceNumerator)
			{
				CommonCode.DropItem(info, this.dropIds[info.rng.Next(this.dropIds.Length)], 1, false);
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

		// Token: 0x06004474 RID: 17524 RVA: 0x0060A69C File Offset: 0x0060889C
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float num = (float)this.chanceNumerator / (float)this.chanceDenominator;
			float num2 = num * ratesInfo.parentDroprateChance;
			float dropRate = 1f / (float)this.dropIds.Length * num2;
			for (int i = 0; i < this.dropIds.Length; i++)
			{
				drops.Add(new DropRateInfo(this.dropIds[i], 1, 1, dropRate, ratesInfo.conditions));
			}
			Chains.ReportDroprates(this.ChainedRules, num, drops, ratesInfo);
		}

		// Token: 0x04005A7B RID: 23163
		public int[] dropIds;

		// Token: 0x04005A7C RID: 23164
		public int chanceDenominator;

		// Token: 0x04005A7D RID: 23165
		public int chanceNumerator;
	}
}
