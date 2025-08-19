using System;
using System.Runtime.CompilerServices;
using Terraria.Localization;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000618 RID: 1560
	public static class Extensions
	{
		// Token: 0x060044A9 RID: 17577 RVA: 0x0060AFD8 File Offset: 0x006091D8
		[NullableContext(1)]
		public static SimpleItemDropRuleCondition ToDropCondition(this Condition condition, ShowItemDropInUI showItemDropInUI, bool showConditionInUI = true)
		{
			return new SimpleItemDropRuleCondition(Language.GetText("Bestiary_ItemDropConditions.SimpleCondition").WithFormatArgs(new object[]
			{
				condition.Description
			}), condition.Predicate, showItemDropInUI, showConditionInUI);
		}
	}
}
