using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200028E RID: 654
	public class CommonDropWithRerolls : CommonDrop
	{
		// Token: 0x0600202E RID: 8238 RVA: 0x0051830F File Offset: 0x0051650F
		public CommonDropWithRerolls(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, int rerolls) : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, 1)
		{
			this.timesToRoll = rerolls + 1;
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00518328 File Offset: 0x00516528
		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			bool flag = false;
			for (int i = 0; i < this.timesToRoll; i++)
			{
				flag = (flag || info.player.RollLuck(this.chanceDenominator) < this.chanceNumerator);
			}
			if (flag)
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

		// Token: 0x06002030 RID: 8240 RVA: 0x005183BC File Offset: 0x005165BC
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

		// Token: 0x040046D2 RID: 18130
		public int timesToRoll;
	}
}
