using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200060B RID: 1547
	public class ItemDropResolver
	{
		// Token: 0x06004437 RID: 17463 RVA: 0x0060A015 File Offset: 0x00608215
		public ItemDropResolver(ItemDropDatabase database)
		{
			this._database = database;
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x0060A024 File Offset: 0x00608224
		public void TryDropping(DropAttemptInfo info)
		{
			List<IItemDropRule> rulesForNPCID = (info.npc != null) ? this._database.GetRulesForNPCID(info.npc.netID, true) : this._database.GetRulesForItemID(info.item);
			for (int i = 0; i < rulesForNPCID.Count; i++)
			{
				this.ResolveRule(rulesForNPCID[i], info);
			}
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x0060A084 File Offset: 0x00608284
		private ItemDropAttemptResult ResolveRule(IItemDropRule rule, DropAttemptInfo info)
		{
			if (!rule.CanDrop(info))
			{
				ItemDropAttemptResult itemDropAttemptResult2 = new ItemDropAttemptResult
				{
					State = ItemDropAttemptResultState.DoesntFillConditions
				};
				this.ResolveRuleChains(rule, info, itemDropAttemptResult2);
				return itemDropAttemptResult2;
			}
			INestedItemDropRule nestedItemDropRule = rule as INestedItemDropRule;
			ItemDropAttemptResult itemDropAttemptResult3 = (nestedItemDropRule == null) ? rule.TryDroppingItem(info) : nestedItemDropRule.TryDroppingItem(info, new ItemDropRuleResolveAction(this.ResolveRule));
			this.ResolveRuleChains(rule, info, itemDropAttemptResult3);
			return itemDropAttemptResult3;
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x0060A0E8 File Offset: 0x006082E8
		private void ResolveRuleChains(IItemDropRule rule, DropAttemptInfo info, ItemDropAttemptResult parentResult)
		{
			this.ResolveRuleChains(ref info, ref parentResult, rule.ChainedRules);
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x0060A0FC File Offset: 0x006082FC
		private void ResolveRuleChains(ref DropAttemptInfo info, ref ItemDropAttemptResult parentResult, List<IItemDropRuleChainAttempt> ruleChains)
		{
			if (ruleChains == null)
			{
				return;
			}
			for (int i = 0; i < ruleChains.Count; i++)
			{
				IItemDropRuleChainAttempt itemDropRuleChainAttempt = ruleChains[i];
				if (itemDropRuleChainAttempt.CanChainIntoRule(parentResult))
				{
					this.ResolveRule(itemDropRuleChainAttempt.RuleToChain, info);
				}
			}
		}

		// Token: 0x04005A75 RID: 23157
		private ItemDropDatabase _database;
	}
}
