using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005F6 RID: 1526
	public class DropBasedOnMasterAndExpertMode : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060043A0 RID: 17312 RVA: 0x00600787 File Offset: 0x005FE987
		// (set) Token: 0x060043A1 RID: 17313 RVA: 0x0060078F File Offset: 0x005FE98F
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x060043A2 RID: 17314 RVA: 0x00600798 File Offset: 0x005FE998
		public DropBasedOnMasterAndExpertMode(IItemDropRule ruleForDefault, IItemDropRule ruleForExpertMode, IItemDropRule ruleForMasterMode)
		{
			this.ruleForDefault = ruleForDefault;
			this.ruleForExpertmode = ruleForExpertMode;
			this.ruleForMasterMode = ruleForMasterMode;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x006007C0 File Offset: 0x005FE9C0
		public bool CanDrop(DropAttemptInfo info)
		{
			if (info.IsMasterMode)
			{
				return this.ruleForMasterMode.CanDrop(info);
			}
			if (info.IsExpertMode)
			{
				return this.ruleForExpertmode.CanDrop(info);
			}
			return this.ruleForDefault.CanDrop(info);
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x006007F8 File Offset: 0x005FE9F8
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x00600816 File Offset: 0x005FEA16
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			if (info.IsMasterMode)
			{
				return resolveAction(this.ruleForMasterMode, info);
			}
			if (info.IsExpertMode)
			{
				return resolveAction(this.ruleForExpertmode, info);
			}
			return resolveAction(this.ruleForDefault, info);
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x00600854 File Offset: 0x005FEA54
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			DropRateInfoChainFeed ratesInfo2 = ratesInfo.With(1f);
			ratesInfo2.AddCondition(new Conditions.IsMasterMode());
			this.ruleForMasterMode.ReportDroprates(drops, ratesInfo2);
			DropRateInfoChainFeed ratesInfo3 = ratesInfo.With(1f);
			ratesInfo3.AddCondition(new Conditions.NotMasterMode());
			ratesInfo3.AddCondition(new Conditions.IsExpert());
			this.ruleForExpertmode.ReportDroprates(drops, ratesInfo3);
			DropRateInfoChainFeed ratesInfo4 = ratesInfo.With(1f);
			ratesInfo4.AddCondition(new Conditions.NotMasterMode());
			ratesInfo4.AddCondition(new Conditions.NotExpert());
			this.ruleForDefault.ReportDroprates(drops, ratesInfo4);
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x04005A41 RID: 23105
		public IItemDropRule ruleForDefault;

		// Token: 0x04005A42 RID: 23106
		public IItemDropRule ruleForExpertmode;

		// Token: 0x04005A43 RID: 23107
		public IItemDropRule ruleForMasterMode;
	}
}
