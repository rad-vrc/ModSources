using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x020004B3 RID: 1203
	public static class ShimmerTransforms
	{
		// Token: 0x060039CA RID: 14794 RVA: 0x0059BDBC File Offset: 0x00599FBC
		public static int GetDecraftingRecipeIndex(int type)
		{
			foreach (int recipeIndex in ItemID.Sets.CraftingRecipeIndices[type])
			{
				if (RecipeLoader.DecraftAvailable(Main.recipe[recipeIndex]))
				{
					return recipeIndex;
				}
			}
			return -1;
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x0059BDF4 File Offset: 0x00599FF4
		public static bool IsItemTransformLocked(int type)
		{
			int decraftingRecipeIndex = ShimmerTransforms.GetDecraftingRecipeIndex(type);
			return decraftingRecipeIndex >= 0 && ((!NPC.downedBoss3 && ShimmerTransforms.RecipeSets.PostSkeletron[decraftingRecipeIndex]) || (!NPC.downedGolemBoss && ShimmerTransforms.RecipeSets.PostGolem[decraftingRecipeIndex]) || !RecipeLoader.DecraftAvailable(Main.recipe[decraftingRecipeIndex]));
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x0059BE44 File Offset: 0x0059A044
		public static void UpdateRecipeSets()
		{
			ShimmerTransforms.RecipeSets.PostSkeletron = Utils.MapArray<Recipe, bool>(Main.recipe, (Recipe r) => r.ContainsIngredient(154));
			ShimmerTransforms.RecipeSets.PostGolem = Utils.MapArray<Recipe, bool>(Main.recipe, (Recipe r) => r.ContainsIngredient(1101));
		}

		// Token: 0x02000BB9 RID: 3001
		public static class RecipeSets
		{
			// Token: 0x040076F2 RID: 30450
			public static bool[] PostSkeletron;

			// Token: 0x040076F3 RID: 30451
			public static bool[] PostGolem;
		}
	}
}
