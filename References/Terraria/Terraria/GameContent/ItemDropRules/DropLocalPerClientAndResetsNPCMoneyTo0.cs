using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200028C RID: 652
	public class DropLocalPerClientAndResetsNPCMoneyTo0 : CommonDrop
	{
		// Token: 0x06002028 RID: 8232 RVA: 0x005181E0 File Offset: 0x005163E0
		public DropLocalPerClientAndResetsNPCMoneyTo0(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, IItemDropRuleCondition optionalCondition) : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, 1)
		{
			this.condition = optionalCondition;
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x005181F6 File Offset: 0x005163F6
		public override bool CanDrop(DropAttemptInfo info)
		{
			return this.condition == null || this.condition.CanDrop(info);
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00518210 File Offset: 0x00516410
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

		// Token: 0x040046D0 RID: 18128
		public IItemDropRuleCondition condition;
	}
}
