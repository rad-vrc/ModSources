using System;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Created in the context of a player buying an item from a shop. Very similar to <seealso cref="T:Terraria.DataStructures.RecipeItemCreationContext" /> in
	/// functionality.
	/// </summary>
	// Token: 0x02000718 RID: 1816
	public class BuyItemCreationContext : ItemCreationContext
	{
		// Token: 0x060049DA RID: 18906 RVA: 0x0064E54C File Offset: 0x0064C74C
		public BuyItemCreationContext(Item destinationStack, NPC vendorNPC)
		{
			this.DestinationStack = destinationStack;
			this.VendorNPC = vendorNPC;
		}

		/// <summary>
		/// An item stack that the bought item will be combined with (via OnStack).
		/// </summary>
		// Token: 0x04005F07 RID: 24327
		public Item DestinationStack;

		/// <summary>
		/// The NPC that this item was bought from.
		/// </summary>
		// Token: 0x04005F08 RID: 24328
		public NPC VendorNPC;
	}
}
