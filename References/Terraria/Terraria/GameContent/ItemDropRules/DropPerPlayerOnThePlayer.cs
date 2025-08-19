using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200028D RID: 653
	public class DropPerPlayerOnThePlayer : CommonDrop
	{
		// Token: 0x0600202B RID: 8235 RVA: 0x00518284 File Offset: 0x00516484
		public DropPerPlayerOnThePlayer(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, IItemDropRuleCondition optionalCondition) : base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, 1)
		{
			this.condition = optionalCondition;
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x0051829A File Offset: 0x0051649A
		public override bool CanDrop(DropAttemptInfo info)
		{
			return this.condition == null || this.condition.CanDrop(info);
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x005182B4 File Offset: 0x005164B4
		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			CommonCode.DropItemForEachInteractingPlayerOnThePlayer(info.npc, this.itemId, info.rng, this.chanceNumerator, this.chanceDenominator, info.rng.Next(this.amountDroppedMinimum, this.amountDroppedMaximum + 1), true);
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x040046D1 RID: 18129
		public IItemDropRuleCondition condition;
	}
}
