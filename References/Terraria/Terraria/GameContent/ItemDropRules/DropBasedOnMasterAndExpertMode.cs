using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000292 RID: 658
	public class DropBasedOnMasterAndExpertMode : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x005186A7 File Offset: 0x005168A7
		// (set) Token: 0x06002046 RID: 8262 RVA: 0x005186AF File Offset: 0x005168AF
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06002047 RID: 8263 RVA: 0x005186B8 File Offset: 0x005168B8
		public DropBasedOnMasterAndExpertMode(IItemDropRule ruleForDefault, IItemDropRule ruleForExpertMode, IItemDropRule ruleForMasterMode)
		{
			this.ruleForDefault = ruleForDefault;
			this.ruleForExpertmode = ruleForExpertMode;
			this.ruleForMasterMode = ruleForMasterMode;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x005186E0 File Offset: 0x005168E0
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

		// Token: 0x06002049 RID: 8265 RVA: 0x00518718 File Offset: 0x00516918
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00518736 File Offset: 0x00516936
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

		// Token: 0x0600204B RID: 8267 RVA: 0x00518774 File Offset: 0x00516974
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

		// Token: 0x040046DA RID: 18138
		public IItemDropRule ruleForDefault;

		// Token: 0x040046DB RID: 18139
		public IItemDropRule ruleForExpertmode;

		// Token: 0x040046DC RID: 18140
		public IItemDropRule ruleForMasterMode;
	}
}
