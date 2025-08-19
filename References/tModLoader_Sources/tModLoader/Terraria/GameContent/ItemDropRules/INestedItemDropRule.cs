using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000606 RID: 1542
	public interface INestedItemDropRule
	{
		// Token: 0x060043EF RID: 17391
		ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction);
	}
}
