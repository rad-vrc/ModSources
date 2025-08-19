using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005F8 RID: 1528
	public class DropLocalPerClientAndResetsNPCMoneyTo0 : CommonDrop
	{
		// Token: 0x060043AE RID: 17326 RVA: 0x00600A03 File Offset: 0x005FEC03
		public DropLocalPerClientAndResetsNPCMoneyTo0(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, IItemDropRuleCondition optionalCondition) : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, 1)
		{
			this.condition = optionalCondition;
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x00600A19 File Offset: 0x005FEC19
		public override bool CanDrop(DropAttemptInfo info)
		{
			return this.condition == null || this.condition.CanDrop(info);
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x00600A34 File Offset: 0x005FEC34
		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.rng.Next(this.chanceDenominator) < this.chanceNumerator)
			{
				CommonCode.DropItemLocalPerClientAndSetNPCMoneyTo0(info.npc, this.itemId, info.rng.Next(this.amountDroppedMinimum, this.amountDroppedMaximum + 1), true);
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

		// Token: 0x04005A48 RID: 23112
		public IItemDropRuleCondition condition;
	}
}
