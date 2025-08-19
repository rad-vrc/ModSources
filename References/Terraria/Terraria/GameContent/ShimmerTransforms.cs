using System;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x020001CD RID: 461
	public static class ShimmerTransforms
	{
		// Token: 0x06001BE3 RID: 7139 RVA: 0x004F08B4 File Offset: 0x004EEAB4
		public static int GetDecraftingRecipeIndex(int type)
		{
			int num = ItemID.Sets.IsCrafted[type];
			if (num < 0)
			{
				return -1;
			}
			if (WorldGen.crimson && ItemID.Sets.IsCraftedCrimson[type] >= 0)
			{
				return ItemID.Sets.IsCraftedCrimson[type];
			}
			if (!WorldGen.crimson && ItemID.Sets.IsCraftedCorruption[type] >= 0)
			{
				return ItemID.Sets.IsCraftedCorruption[type];
			}
			return num;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x004F0904 File Offset: 0x004EEB04
		public static bool IsItemTransformLocked(int type)
		{
			int decraftingRecipeIndex = ShimmerTransforms.GetDecraftingRecipeIndex(type);
			return decraftingRecipeIndex >= 0 && ((!NPC.downedBoss3 && ShimmerTransforms.RecipeSets.PostSkeletron[decraftingRecipeIndex]) || (!NPC.downedGolemBoss && ShimmerTransforms.RecipeSets.PostGolem[decraftingRecipeIndex]));
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x004F0944 File Offset: 0x004EEB44
		public static void UpdateRecipeSets()
		{
			ShimmerTransforms.RecipeSets.PostSkeletron = Utils.MapArray<Recipe, bool>(Main.recipe, (Recipe r) => r.ContainsIngredient(154));
			ShimmerTransforms.RecipeSets.PostGolem = Utils.MapArray<Recipe, bool>(Main.recipe, (Recipe r) => r.ContainsIngredient(1101));
		}

		// Token: 0x020005F2 RID: 1522
		public static class RecipeSets
		{
			// Token: 0x04006004 RID: 24580
			public static bool[] PostSkeletron;

			// Token: 0x04006005 RID: 24581
			public static bool[] PostGolem;
		}
	}
}
