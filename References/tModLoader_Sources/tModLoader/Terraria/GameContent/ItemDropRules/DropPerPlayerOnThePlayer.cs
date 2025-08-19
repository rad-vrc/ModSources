using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005FB RID: 1531
	public class DropPerPlayerOnThePlayer : CommonDrop
	{
		// Token: 0x060043BF RID: 17343 RVA: 0x00600D24 File Offset: 0x005FEF24
		public DropPerPlayerOnThePlayer(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, IItemDropRuleCondition optionalCondition) : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, 1)
		{
			this.condition = optionalCondition;
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x00600D3A File Offset: 0x005FEF3A
		public override bool CanDrop(DropAttemptInfo info)
		{
			return this.condition == null || this.condition.CanDrop(info);
		}

		// Token: 0x060043C1 RID: 17345 RVA: 0x00600D54 File Offset: 0x005FEF54
		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			CommonCode.DropItemForEachInteractingPlayerOnThePlayer(info.npc, this.itemId, info.rng, this.chanceNumerator, this.chanceDenominator, info.rng.Next(this.amountDroppedMinimum, this.amountDroppedMaximum + 1), true);
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x04005A4D RID: 23117
		public IItemDropRuleCondition condition;
	}
}
