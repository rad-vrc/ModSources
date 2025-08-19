using System;
using Terraria.GameContent.UI.Elements;

namespace Terraria.GameContent.Bestiary
{
	/// <summary>
	/// Allows categorizing Bestiary UI Elements into existing categories. <see cref="T:Terraria.GameContent.Bestiary.IBestiaryInfoElement" /> that are not vanilla Types without this interface will be placed at the bottom in the <see cref="F:Terraria.GameContent.UI.Elements.UIBestiaryEntryInfoPage.BestiaryInfoCategory.Misc" /> category.
	/// </summary>
	// Token: 0x0200068E RID: 1678
	public interface ICategorizedBestiaryInfoElement
	{
		/// <summary>
		/// The category to place this element inside of, which corresponds to the order of the bestiary elements.
		/// <para /> Use <see cref="P:Terraria.GameContent.Bestiary.IBestiaryPrioritizedElement.OrderPriority" /> to dictate a relative ordering within a category.
		/// </summary>
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060047F6 RID: 18422
		UIBestiaryEntryInfoPage.BestiaryInfoCategory ElementCategory { get; }
	}
}
