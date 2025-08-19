using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000291 RID: 657
	public class DropBasedOnMasterMode : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600203E RID: 8254 RVA: 0x005185A3 File Offset: 0x005167A3
		// (set) Token: 0x0600203F RID: 8255 RVA: 0x005185AB File Offset: 0x005167AB
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06002040 RID: 8256 RVA: 0x005185B4 File Offset: 0x005167B4
		public DropBasedOnMasterMode(IItemDropRule ruleForDefault, IItemDropRule ruleForMasterMode)
		{
			this.ruleForDefault = ruleForDefault;
			this.ruleForMasterMode = ruleForMasterMode;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x005185D5 File Offset: 0x005167D5
		public bool CanDrop(DropAttemptInfo info)
		{
			if (info.IsMasterMode)
			{
				return this.ruleForMasterMode.CanDrop(info);
			}
			return this.ruleForDefault.CanDrop(info);
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x005185F8 File Offset: 0x005167F8
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x00518616 File Offset: 0x00516816
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			if (info.IsMasterMode)
			{
				return resolveAction(this.ruleForMasterMode, info);
			}
			return resolveAction(this.ruleForDefault, info);
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x0051863C File Offset: 0x0051683C
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			DropRateInfoChainFeed ratesInfo2 = ratesInfo.With(1f);
			ratesInfo2.AddCondition(new Conditions.IsMasterMode());
			this.ruleForMasterMode.ReportDroprates(drops, ratesInfo2);
			DropRateInfoChainFeed ratesInfo3 = ratesInfo.With(1f);
			ratesInfo3.AddCondition(new Conditions.NotMasterMode());
			this.ruleForDefault.ReportDroprates(drops, ratesInfo3);
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x040046D7 RID: 18135
		public IItemDropRule ruleForDefault;

		// Token: 0x040046D8 RID: 18136
		public IItemDropRule ruleForMasterMode;
	}
}
