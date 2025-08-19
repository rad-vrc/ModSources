using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000299 RID: 665
	public class OneFromOptionsDropRule : IItemDropRule
	{
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x00518F29 File Offset: 0x00517129
		// (set) Token: 0x0600206F RID: 8303 RVA: 0x00518F31 File Offset: 0x00517131
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06002070 RID: 8304 RVA: 0x00518F3A File Offset: 0x0051713A
		public OneFromOptionsDropRule(int chanceDenominator, int chanceNumerator, params int[] options)
		{
			this.chanceDenominator = chanceDenominator;
			this.chanceNumerator = chanceNumerator;
			this.dropIds = options;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0003266D File Offset: 0x0003086D
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x00518F64 File Offset: 0x00517164
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.player.RollLuck(this.chanceDenominator) < this.chanceNumerator)
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

		// Token: 0x06002073 RID: 8307 RVA: 0x00518FD4 File Offset: 0x005171D4
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

		// Token: 0x040046EB RID: 18155
		public int[] dropIds;

		// Token: 0x040046EC RID: 18156
		public int chanceDenominator;

		// Token: 0x040046ED RID: 18157
		public int chanceNumerator;
	}
}
