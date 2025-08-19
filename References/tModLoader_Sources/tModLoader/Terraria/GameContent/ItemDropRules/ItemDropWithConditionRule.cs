using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200060E RID: 1550
	public class ItemDropWithConditionRule : CommonDrop
	{
		// Token: 0x06004460 RID: 17504 RVA: 0x0060A32F File Offset: 0x0060852F
		public ItemDropWithConditionRule(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, IItemDropRuleCondition condition, int chanceNumerator = 1) : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, chanceNumerator)
		{
			this.condition = condition;
		}

		// Token: 0x06004461 RID: 17505 RVA: 0x0060A346 File Offset: 0x00608546
		public override bool CanDrop(DropAttemptInfo info)
		{
			return this.condition.CanDrop(info);
		}

		// Token: 0x06004462 RID: 17506 RVA: 0x0060A354 File Offset: 0x00608554
		public override void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			DropRateInfoChainFeed ratesInfo2 = ratesInfo.With(1f);
			ratesInfo2.AddCondition(this.condition);
			float num = (float)this.chanceNumerator / (float)this.chanceDenominator;
			float dropRate = num * ratesInfo2.parentDroprateChance;
			drops.Add(new DropRateInfo(this.itemId, this.amountDroppedMinimum, this.amountDroppedMaximum, dropRate, ratesInfo2.conditions));
			Chains.ReportDroprates(base.ChainedRules, num, drops, ratesInfo2);
		}

		// Token: 0x04005A76 RID: 23158
		public IItemDropRuleCondition condition;
	}
}
