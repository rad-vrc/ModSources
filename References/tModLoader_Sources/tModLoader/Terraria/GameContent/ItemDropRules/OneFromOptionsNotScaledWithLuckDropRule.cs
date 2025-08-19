using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000612 RID: 1554
	public class OneFromOptionsNotScaledWithLuckDropRule : IItemDropRule
	{
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06004475 RID: 17525 RVA: 0x0060A711 File Offset: 0x00608911
		// (set) Token: 0x06004476 RID: 17526 RVA: 0x0060A719 File Offset: 0x00608919
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06004477 RID: 17527 RVA: 0x0060A722 File Offset: 0x00608922
		public OneFromOptionsNotScaledWithLuckDropRule(int chanceDenominator, int chanceNumerator, params int[] options)
		{
			this.chanceDenominator = chanceDenominator;
			this.dropIds = options;
			this.chanceNumerator = chanceNumerator;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x0060A74A File Offset: 0x0060894A
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x0060A750 File Offset: 0x00608950
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.rng.Next(this.chanceDenominator) < this.chanceNumerator)
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

		// Token: 0x0600447A RID: 17530 RVA: 0x0060A7BC File Offset: 0x006089BC
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

		// Token: 0x04005A7F RID: 23167
		public int[] dropIds;

		// Token: 0x04005A80 RID: 23168
		public int chanceDenominator;

		// Token: 0x04005A81 RID: 23169
		public int chanceNumerator;
	}
}
