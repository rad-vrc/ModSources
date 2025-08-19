using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005F2 RID: 1522
	public class CommonDropWithRerolls : CommonDrop
	{
		// Token: 0x06004394 RID: 17300 RVA: 0x00600477 File Offset: 0x005FE677
		public CommonDropWithRerolls(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, int rerolls) : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, 1)
		{
			this.timesToRoll = rerolls + 1;
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x00600490 File Offset: 0x005FE690
		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			bool flag = false;
			for (int i = 0; i < this.timesToRoll; i++)
			{
				flag = (flag || info.player.RollLuck(this.chanceDenominator) < this.chanceNumerator);
			}
			if (flag)
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

		// Token: 0x06004396 RID: 17302 RVA: 0x0060051C File Offset: 0x005FE71C
		public override void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float num = (float)this.chanceNumerator / (float)this.chanceDenominator;
			float num2 = 1f - num;
			float num3 = 1f;
			for (int i = 0; i < this.timesToRoll; i++)
			{
				num3 *= num2;
			}
			float num4 = 1f - num3;
			float dropRate = num4 * ratesInfo.parentDroprateChance;
			drops.Add(new DropRateInfo(this.itemId, this.amountDroppedMinimum, this.amountDroppedMaximum, dropRate, ratesInfo.conditions));
			Chains.ReportDroprates(base.ChainedRules, num4, drops, ratesInfo);
		}

		// Token: 0x04005A36 RID: 23094
		public int timesToRoll;
	}
}
