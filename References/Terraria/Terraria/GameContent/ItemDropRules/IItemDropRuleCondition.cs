using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000287 RID: 647
	public interface IItemDropRuleCondition : IProvideItemConditionDescription
	{
		// Token: 0x06002019 RID: 8217
		bool CanDrop(DropAttemptInfo info);

		// Token: 0x0600201A RID: 8218
		bool CanShowItemDropInUI();
	}
}
