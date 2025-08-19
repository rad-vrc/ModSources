using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005F7 RID: 1527
	public class DropBasedOnMasterMode : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060043A7 RID: 17319 RVA: 0x006008FD File Offset: 0x005FEAFD
		// (set) Token: 0x060043A8 RID: 17320 RVA: 0x00600905 File Offset: 0x005FEB05
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x060043A9 RID: 17321 RVA: 0x0060090E File Offset: 0x005FEB0E
		public DropBasedOnMasterMode(IItemDropRule ruleForDefault, IItemDropRule ruleForMasterMode)
		{
			this.ruleForDefault = ruleForDefault;
			this.ruleForMasterMode = ruleForMasterMode;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x0060092F File Offset: 0x005FEB2F
		public bool CanDrop(DropAttemptInfo info)
		{
			if (info.IsMasterMode)
			{
				return this.ruleForMasterMode.CanDrop(info);
			}
			return this.ruleForDefault.CanDrop(info);
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x00600954 File Offset: 0x005FEB54
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x00600972 File Offset: 0x005FEB72
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			if (info.IsMasterMode)
			{
				return resolveAction(this.ruleForMasterMode, info);
			}
			return resolveAction(this.ruleForDefault, info);
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x00600998 File Offset: 0x005FEB98
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

		// Token: 0x04005A45 RID: 23109
		public IItemDropRule ruleForDefault;

		// Token: 0x04005A46 RID: 23110
		public IItemDropRule ruleForMasterMode;
	}
}
