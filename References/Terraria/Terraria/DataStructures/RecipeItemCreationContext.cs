using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200040D RID: 1037
	public class RecipeItemCreationContext : ItemCreationContext
	{
		// Token: 0x06002B31 RID: 11057 RVA: 0x0059DBB7 File Offset: 0x0059BDB7
		public RecipeItemCreationContext(Recipe recipe)
		{
			this.Recipe = recipe;
		}

		// Token: 0x04004F5E RID: 20318
		public readonly Recipe Recipe;
	}
}
