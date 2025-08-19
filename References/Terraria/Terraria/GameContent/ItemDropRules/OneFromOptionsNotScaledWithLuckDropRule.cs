using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000298 RID: 664
	public class OneFromOptionsNotScaledWithLuckDropRule : IItemDropRule
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x00518E0A File Offset: 0x0051700A
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x00518E12 File Offset: 0x00517012
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x0600206A RID: 8298 RVA: 0x00518E1B File Offset: 0x0051701B
		public OneFromOptionsNotScaledWithLuckDropRule(int chanceDenominator, int chanceNumerator, params int[] options)
		{
			this.chanceDenominator = chanceDenominator;
			this.dropIds = options;
			this.chanceNumerator = chanceNumerator;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0003266D File Offset: 0x0003086D
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x00518E44 File Offset: 0x00517044
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.rng.Next(this.chanceDenominator) < this.chanceNumerator)
			{
				CommonCode.DropItemFromNPC(info.npc, this.dropIds[info.rng.Next(this.dropIds.Length)], 1, false);
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

		// Token: 0x0600206D RID: 8301 RVA: 0x00518EB4 File Offset: 0x005170B4
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

		// Token: 0x040046E7 RID: 18151
		public int[] dropIds;

		// Token: 0x040046E8 RID: 18152
		public int chanceDenominator;

		// Token: 0x040046E9 RID: 18153
		public int chanceNumerator;
	}
}
