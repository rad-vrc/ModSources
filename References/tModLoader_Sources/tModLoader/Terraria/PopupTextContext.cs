using System;

namespace Terraria
{
	/// <summary>
	/// A context for a generated <see cref="T:Terraria.PopupText" />.
	/// </summary>
	// Token: 0x02000046 RID: 70
	public enum PopupTextContext
	{
		/// <summary>
		/// Used when a player picks up an <see cref="T:Terraria.Item" /> and it goes into their inventory.
		/// </summary>
		// Token: 0x04000DA0 RID: 3488
		RegularItemPickup,
		/// <summary>
		/// Used when a player picks up an <see cref="T:Terraria.Item" /> and it goes into their Void Bag.
		/// </summary>
		// Token: 0x04000DA1 RID: 3489
		ItemPickupToVoidContainer,
		/// <summary>
		/// Used for fishing alerts for Sonar Potions.
		/// </summary>
		// Token: 0x04000DA2 RID: 3490
		SonarAlert,
		/// <summary>
		/// Used when a player reforges an <see cref="T:Terraria.Item" />.
		/// </summary>
		// Token: 0x04000DA3 RID: 3491
		ItemReforge,
		/// <summary>
		/// Used when a player crafts an <see cref="T:Terraria.Item" />.
		/// </summary>
		// Token: 0x04000DA4 RID: 3492
		ItemCraft,
		/// <summary>
		/// Used for all other <see cref="T:Terraria.PopupText" />s.
		/// </summary>
		// Token: 0x04000DA5 RID: 3493
		Advanced
	}
}
