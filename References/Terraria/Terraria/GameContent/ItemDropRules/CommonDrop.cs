using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200028A RID: 650
	public class CommonDrop : IItemDropRule
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x00518044 File Offset: 0x00516244
		// (set) Token: 0x06002021 RID: 8225 RVA: 0x0051804C File Offset: 0x0051624C
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06002022 RID: 8226 RVA: 0x00518055 File Offset: 0x00516255
		public CommonDrop(int itemId, int chanceDenominator, int amountDroppedMinimum = 1, int amountDroppedMaximum = 1, int chanceNumerator = 1)
		{
			this.itemId = itemId;
			this.chanceDenominator = chanceDenominator;
			this.amountDroppedMinimum = amountDroppedMinimum;
			this.amountDroppedMaximum = amountDroppedMaximum;
			this.chanceNumerator = chanceNumerator;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x0003266D File Offset: 0x0003086D
		public virtual bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00518090 File Offset: 0x00516290
		public virtual ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.player.RollLuck(this.chanceDenominator) < this.chanceNumerator)
			{
				CommonCode.DropItemFromNPC(info.npc, this.itemId, info.rng.Next(this.amountDroppedMinimum, this.amountDroppedMaximum + 1), false);
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

		// Token: 0x06002025 RID: 8229 RVA: 0x00518104 File Offset: 0x00516304
		public virtual void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float num = (float)this.chanceNumerator / (float)this.chanceDenominator;
			float dropRate = num * ratesInfo.parentDroprateChance;
			drops.Add(new DropRateInfo(this.itemId, this.amountDroppedMinimum, this.amountDroppedMaximum, dropRate, ratesInfo.conditions));
			Chains.ReportDroprates(this.ChainedRules, num, drops, ratesInfo);
		}

		// Token: 0x040046CA RID: 18122
		public int itemId;

		// Token: 0x040046CB RID: 18123
		public int chanceDenominator;

		// Token: 0x040046CC RID: 18124
		public int amountDroppedMinimum;

		// Token: 0x040046CD RID: 18125
		public int amountDroppedMaximum;

		// Token: 0x040046CE RID: 18126
		public int chanceNumerator;
	}
}
