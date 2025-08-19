using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000283 RID: 643
	public interface INestedItemDropRule
	{
		// Token: 0x06002015 RID: 8213
		ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction);
	}
}
