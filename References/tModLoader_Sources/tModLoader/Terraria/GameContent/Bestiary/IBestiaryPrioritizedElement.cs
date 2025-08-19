using System;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200068B RID: 1675
	public interface IBestiaryPrioritizedElement
	{
		/// <summary>
		/// Higher priority values are shown before (higher up) other elements in the same Bestiary info category (<see cref="T:Terraria.GameContent.UI.Elements.UIBestiaryEntryInfoPage.BestiaryInfoCategory" />). Defaults to 0.
		/// <para /> Use <see cref="P:Terraria.GameContent.Bestiary.ICategorizedBestiaryInfoElement.ElementCategory" /> to dictate which category this element belongs to.
		/// </summary>
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060047F3 RID: 18419
		float OrderPriority { get; }
	}
}
