using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	/// <summary>
	/// Used just by Herb Bag. Horribly hardcoded. Do not use if you can.
	/// </summary>
	// Token: 0x02000602 RID: 1538
	public class HerbBagDropsItemDropRule : IItemDropRule
	{
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060043E0 RID: 17376 RVA: 0x006014BC File Offset: 0x005FF6BC
		// (set) Token: 0x060043E1 RID: 17377 RVA: 0x006014C4 File Offset: 0x005FF6C4
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x060043E2 RID: 17378 RVA: 0x006014CD File Offset: 0x005FF6CD
		public HerbBagDropsItemDropRule(params int[] options)
		{
			this.dropIds = options;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x006014E7 File Offset: 0x005FF6E7
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x060043E4 RID: 17380 RVA: 0x006014EC File Offset: 0x005FF6EC
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			int amount = Main.rand.Next(2, 5);
			if (Main.rand.Next(3) == 0)
			{
				amount++;
			}
			for (int i = 0; i < amount; i++)
			{
				int stack = Main.rand.Next(2, 5);
				if (Main.rand.Next(3) == 0)
				{
					stack += Main.rand.Next(1, 5);
				}
				CommonCode.DropItem(info, this.dropIds[info.rng.Next(this.dropIds.Length)], stack, false);
			}
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x00601580 File Offset: 0x005FF780
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float num = 1f;
			float num2 = num * ratesInfo.parentDroprateChance;
			float dropRate = 1f / ((float)this.dropIds.Length + 3.83f) * num2;
			for (int i = 0; i < this.dropIds.Length; i++)
			{
				drops.Add(new DropRateInfo(this.dropIds[i], 1, 1, dropRate, ratesInfo.conditions));
			}
			Chains.ReportDroprates(this.ChainedRules, num, drops, ratesInfo);
		}

		// Token: 0x04005A67 RID: 23143
		public int[] dropIds;
	}
}
