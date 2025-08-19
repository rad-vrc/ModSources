using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005F0 RID: 1520
	public class CommonDrop : IItemDropRule
	{
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600438B RID: 17291 RVA: 0x006002B0 File Offset: 0x005FE4B0
		// (set) Token: 0x0600438C RID: 17292 RVA: 0x006002B8 File Offset: 0x005FE4B8
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x0600438D RID: 17293 RVA: 0x006002C4 File Offset: 0x005FE4C4
		public CommonDrop(int itemId, int chanceDenominator, int amountDroppedMinimum = 1, int amountDroppedMaximum = 1, int chanceNumerator = 1)
		{
			if (amountDroppedMinimum > amountDroppedMaximum)
			{
				throw new ArgumentOutOfRangeException("amountDroppedMinimum", "amountDroppedMinimum must be lesser or equal to amountDroppedMaximum.");
			}
			this.itemId = itemId;
			this.chanceDenominator = chanceDenominator;
			this.amountDroppedMinimum = amountDroppedMinimum;
			this.amountDroppedMaximum = amountDroppedMaximum;
			this.chanceNumerator = chanceNumerator;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x0060031C File Offset: 0x005FE51C
		public virtual bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x00600320 File Offset: 0x005FE520
		public virtual ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.player.RollLuck(this.chanceDenominator) < this.chanceNumerator)
			{
				CommonCode.DropItem(info, this.itemId, info.rng.Next(this.amountDroppedMinimum, this.amountDroppedMaximum + 1), false);
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

		// Token: 0x06004390 RID: 17296 RVA: 0x00600390 File Offset: 0x005FE590
		public virtual void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float num = (float)this.chanceNumerator / (float)this.chanceDenominator;
			float dropRate = num * ratesInfo.parentDroprateChance;
			drops.Add(new DropRateInfo(this.itemId, this.amountDroppedMinimum, this.amountDroppedMaximum, dropRate, ratesInfo.conditions));
			Chains.ReportDroprates(this.ChainedRules, num, drops, ratesInfo);
		}

		// Token: 0x04005A30 RID: 23088
		public int itemId;

		// Token: 0x04005A31 RID: 23089
		public int chanceDenominator;

		// Token: 0x04005A32 RID: 23090
		public int amountDroppedMinimum;

		// Token: 0x04005A33 RID: 23091
		public int amountDroppedMaximum;

		// Token: 0x04005A34 RID: 23092
		public int chanceNumerator;
	}
}
