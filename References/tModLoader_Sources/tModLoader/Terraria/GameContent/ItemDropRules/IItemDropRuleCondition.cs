using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000605 RID: 1541
	public interface IItemDropRuleCondition : IProvideItemConditionDescription
	{
		/// <summary>
		/// Whether or not the item using this drop rule can drop.
		/// </summary>
		// Token: 0x060043ED RID: 17389
		bool CanDrop(DropAttemptInfo info);

		/// <summary>
		/// Whether or not the item using this drop rule will show in the Bestiary drops listing. This is typically less restrictive than the <see cref="M:Terraria.GameContent.ItemDropRules.IItemDropRuleCondition.CanDrop(Terraria.GameContent.ItemDropRules.DropAttemptInfo)" /> logic and usually only limits drop visibility for conditions that represent world state that can't change, such as the world difficulty or world evil choice. Most other conditions just return true, hinting to the player that the item could potentially drop even if the drop conditions aren't currently fully met.
		/// </summary>
		// Token: 0x060043EE RID: 17390
		bool CanShowItemDropInUI();
	}
}
