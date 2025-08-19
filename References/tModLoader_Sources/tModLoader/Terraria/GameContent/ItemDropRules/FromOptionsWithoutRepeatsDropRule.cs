using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000601 RID: 1537
	public class FromOptionsWithoutRepeatsDropRule : IItemDropRule
	{
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060043DA RID: 17370 RVA: 0x00601352 File Offset: 0x005FF552
		// (set) Token: 0x060043DB RID: 17371 RVA: 0x0060135A File Offset: 0x005FF55A
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x060043DC RID: 17372 RVA: 0x00601363 File Offset: 0x005FF563
		public FromOptionsWithoutRepeatsDropRule(int dropCount, params int[] options)
		{
			this.dropCount = dropCount;
			this.dropIds = options;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x0060138F File Offset: 0x005FF58F
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x00601394 File Offset: 0x005FF594
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			this._temporaryAvailableItems.Clear();
			this._temporaryAvailableItems.AddRange(this.dropIds);
			int i = 0;
			while (i < this.dropCount && this._temporaryAvailableItems.Count > 0)
			{
				int index = info.rng.Next(this._temporaryAvailableItems.Count);
				CommonCode.DropItem(info, this._temporaryAvailableItems[index], 1, false);
				this._temporaryAvailableItems.RemoveAt(index);
				i++;
			}
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x00601424 File Offset: 0x005FF624
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

		// Token: 0x04005A63 RID: 23139
		public int[] dropIds;

		// Token: 0x04005A64 RID: 23140
		public int dropCount;

		// Token: 0x04005A65 RID: 23141
		private List<int> _temporaryAvailableItems = new List<int>();
	}
}
