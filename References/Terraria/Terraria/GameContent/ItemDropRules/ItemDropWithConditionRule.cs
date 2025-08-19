using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000295 RID: 661
	public class ItemDropWithConditionRule : CommonDrop
	{
		// Token: 0x06002059 RID: 8281 RVA: 0x00518B52 File Offset: 0x00516D52
		public ItemDropWithConditionRule(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, IItemDropRuleCondition condition, int chanceNumerator = 1) : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, chanceNumerator)
		{
			this.condition = condition;
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x00518B69 File Offset: 0x00516D69
		public override bool CanDrop(DropAttemptInfo info)
		{
			return this.condition.CanDrop(info);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x00518B78 File Offset: 0x00516D78
		public override void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			DropRateInfoChainFeed dropRateInfoChainFeed = ratesInfo.With(1f);
			dropRateInfoChainFeed.AddCondition(this.condition);
			float num = (float)this.chanceNumerator / (float)this.chanceDenominator;
			float dropRate = num * dropRateInfoChainFeed.parentDroprateChance;
			drops.Add(new DropRateInfo(this.itemId, this.amountDroppedMinimum, this.amountDroppedMaximum, dropRate, dropRateInfoChainFeed.conditions));
			Chains.ReportDroprates(base.ChainedRules, num, drops, dropRateInfoChainFeed);
		}

		// Token: 0x040046E1 RID: 18145
		public IItemDropRuleCondition condition;
	}
}
