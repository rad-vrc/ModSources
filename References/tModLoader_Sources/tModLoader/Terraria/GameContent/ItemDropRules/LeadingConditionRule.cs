using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	/// <summary>
	/// A drop rule that doesn't drop any items by itself, rather it contains just a <see cref="T:Terraria.GameContent.ItemDropRules.IItemDropRuleCondition" />. Use <see cref="M:Terraria.GameContent.ItemDropRules.Chains.OnSuccess(Terraria.GameContent.ItemDropRules.IItemDropRule,Terraria.GameContent.ItemDropRules.IItemDropRule,System.Boolean)" />, OnFailedConditions, or OnFailedRoll to attach other item drop rules to this rule. This can be useful for avoiding code repetition while writing logical item drop code.
	/// <para /> See the <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-NPC-Drops-and-Loot-1.4#chaining-rules">Chaining Rules section of the Basic NPC Drops and Loot Guide</see> for more information and examples.
	/// </summary>
	// Token: 0x0200060F RID: 1551
	public class LeadingConditionRule : IItemDropRule
	{
		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06004463 RID: 17507 RVA: 0x0060A3C6 File Offset: 0x006085C6
		// (set) Token: 0x06004464 RID: 17508 RVA: 0x0060A3CE File Offset: 0x006085CE
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06004465 RID: 17509 RVA: 0x0060A3D7 File Offset: 0x006085D7
		public LeadingConditionRule(IItemDropRuleCondition condition)
		{
			this.condition = condition;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x0060A3F1 File Offset: 0x006085F1
		public bool CanDrop(DropAttemptInfo info)
		{
			return this.condition.CanDrop(info);
		}

		// Token: 0x06004467 RID: 17511 RVA: 0x0060A3FF File Offset: 0x006085FF
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			ratesInfo.AddCondition(this.condition);
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x0060A420 File Offset: 0x00608620
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x04005A77 RID: 23159
		public IItemDropRuleCondition condition;
	}
}
