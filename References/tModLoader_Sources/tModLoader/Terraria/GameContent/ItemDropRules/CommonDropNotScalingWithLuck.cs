using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005F1 RID: 1521
	public class CommonDropNotScalingWithLuck : CommonDrop
	{
		// Token: 0x06004391 RID: 17297 RVA: 0x006003E8 File Offset: 0x005FE5E8
		public CommonDropNotScalingWithLuck(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum) : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, 1)
		{
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x006003F6 File Offset: 0x005FE5F6
		public CommonDropNotScalingWithLuck(int itemId, int chanceDenominator, int chanceNumerator, int amountDroppedMinimum, int amountDroppedMaximum) : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, chanceNumerator)
		{
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x00600408 File Offset: 0x005FE608
		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.rng.Next(this.chanceDenominator) < this.chanceNumerator)
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
	}
}
