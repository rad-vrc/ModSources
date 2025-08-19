using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Provides the context of an item being crafted from a recipe. Includes the recipe, consumed items, and destination, facilitating features using data from consumed items to affect the final crafted item.
	/// </summary>
	// Token: 0x0200072C RID: 1836
	public class RecipeItemCreationContext : ItemCreationContext
	{
		// Token: 0x06004AAD RID: 19117 RVA: 0x00668153 File Offset: 0x00666353
		public RecipeItemCreationContext(Recipe recipe, List<Item> consumedItems, Item destinationStack)
		{
			this.Recipe = recipe;
			this.ConsumedItems = consumedItems;
			this.DestinationStack = destinationStack;
		}

		// Token: 0x04005FF6 RID: 24566
		public readonly Recipe Recipe;

		/// <summary>
		/// An item stack that the created item will be combined with (via OnStack). For normal crafting, this is Main.mouseItem
		/// </summary>
		// Token: 0x04005FF7 RID: 24567
		public Item DestinationStack;

		/// <summary>
		/// Cloned list of Items consumed when crafting.
		/// </summary>
		// Token: 0x04005FF8 RID: 24568
		public readonly List<Item> ConsumedItems;
	}
}
