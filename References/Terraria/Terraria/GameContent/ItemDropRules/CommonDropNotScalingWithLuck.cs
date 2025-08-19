using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200028B RID: 651
	public class CommonDropNotScalingWithLuck : CommonDrop
	{
		// Token: 0x06002026 RID: 8230 RVA: 0x0051815C File Offset: 0x0051635C
		public CommonDropNotScalingWithLuck(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum) : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, 1)
		{
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x0051816C File Offset: 0x0051636C
		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.rng.Next(this.chanceDenominator) < this.chanceNumerator)
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
	}
}
