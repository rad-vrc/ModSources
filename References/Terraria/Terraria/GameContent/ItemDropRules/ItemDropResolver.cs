using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200029F RID: 671
	public class ItemDropResolver
	{
		// Token: 0x060020BD RID: 8381 RVA: 0x0051ECF3 File Offset: 0x0051CEF3
		public ItemDropResolver(ItemDropDatabase database)
		{
			this._database = database;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0051ED04 File Offset: 0x0051CF04
		public void TryDropping(DropAttemptInfo info)
		{
			List<IItemDropRule> rulesForNPCID = this._database.GetRulesForNPCID(info.npc.netID, true);
			for (int i = 0; i < rulesForNPCID.Count; i++)
			{
				this.ResolveRule(rulesForNPCID[i], info);
			}
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x0051ED4C File Offset: 0x0051CF4C
		private ItemDropAttemptResult ResolveRule(IItemDropRule rule, DropAttemptInfo info)
		{
			if (!rule.CanDrop(info))
			{
				ItemDropAttemptResult itemDropAttemptResult = new ItemDropAttemptResult
				{
					State = ItemDropAttemptResultState.DoesntFillConditions
				};
				this.ResolveRuleChains(rule, info, itemDropAttemptResult);
				return itemDropAttemptResult;
			}
			INestedItemDropRule nestedItemDropRule = rule as INestedItemDropRule;
			ItemDropAttemptResult itemDropAttemptResult2;
			if (nestedItemDropRule != null)
			{
				itemDropAttemptResult2 = nestedItemDropRule.TryDroppingItem(info, new ItemDropRuleResolveAction(this.ResolveRule));
			}
			else
			{
				itemDropAttemptResult2 = rule.TryDroppingItem(info);
			}
			this.ResolveRuleChains(rule, info, itemDropAttemptResult2);
			return itemDropAttemptResult2;
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x0051EDB1 File Offset: 0x0051CFB1
		private void ResolveRuleChains(IItemDropRule rule, DropAttemptInfo info, ItemDropAttemptResult parentResult)
		{
			this.ResolveRuleChains(ref info, ref parentResult, rule.ChainedRules);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0051EDC4 File Offset: 0x0051CFC4
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

		// Token: 0x040046FA RID: 18170
		private ItemDropDatabase _database;
	}
}
