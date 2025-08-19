using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200029A RID: 666
	public class FromOptionsWithoutRepeatsDropRule : IItemDropRule
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x00519049 File Offset: 0x00517249
		// (set) Token: 0x06002075 RID: 8309 RVA: 0x00519051 File Offset: 0x00517251
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06002076 RID: 8310 RVA: 0x0051905A File Offset: 0x0051725A
		public FromOptionsWithoutRepeatsDropRule(int dropCount, params int[] options)
		{
			this.dropCount = dropCount;
			this.dropIds = options;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x0003266D File Offset: 0x0003086D
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x00519088 File Offset: 0x00517288
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			this._temporaryAvailableItems.Clear();
			this._temporaryAvailableItems.AddRange(this.dropIds);
			int num = 0;
			while (num < this.dropCount && this._temporaryAvailableItems.Count > 0)
			{
				int index = info.rng.Next(this._temporaryAvailableItems.Count);
				CommonCode.DropItemFromNPC(info.npc, this._temporaryAvailableItems[index], 1, false);
				this._temporaryAvailableItems.RemoveAt(index);
				num++;
			}
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x00519120 File Offset: 0x00517320
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float parentDroprateChance = ratesInfo.parentDroprateChance;
			int num = this.dropIds.Length;
			float num2 = 1f;
			int num3 = 0;
			while (num3 < this.dropCount && num > 0)
			{
				num2 *= (float)(num - 1) / (float)num;
				num3++;
				num--;
			}
			float dropRate = (1f - num2) * parentDroprateChance;
			for (int i = 0; i < this.dropIds.Length; i++)
			{
				drops.Add(new DropRateInfo(this.dropIds[i], 1, 1, dropRate, ratesInfo.conditions));
			}
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x040046EF RID: 18159
		public int[] dropIds;

		// Token: 0x040046F0 RID: 18160
		public int dropCount;

		// Token: 0x040046F2 RID: 18162
		private List<int> _temporaryAvailableItems = new List<int>();
	}
}
