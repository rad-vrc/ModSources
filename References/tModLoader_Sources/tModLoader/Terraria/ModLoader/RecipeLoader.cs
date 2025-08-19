using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This is where all Recipe hooks are gathered and called.
	/// </summary>
	// Token: 0x020001F0 RID: 496
	public static class RecipeLoader
	{
		/// <summary>
		/// The mod currently adding recipes.
		/// </summary>
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x005015E9 File Offset: 0x004FF7E9
		// (set) Token: 0x060026E8 RID: 9960 RVA: 0x005015F0 File Offset: 0x004FF7F0
		internal static Mod CurrentMod { get; private set; }

		// Token: 0x060026E9 RID: 9961 RVA: 0x005015F8 File Offset: 0x004FF7F8
		internal static void Unload()
		{
			RecipeLoader.setupRecipes = false;
			RecipeLoader.FirstRecipeForItem = new Recipe[Recipe.maxRecipes];
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x00501610 File Offset: 0x004FF810
		internal static void AddRecipes()
		{
			MethodInfo addRecipesMethod = typeof(Mod).GetMethod("AddRecipes", BindingFlags.Instance | BindingFlags.Public);
			foreach (Mod mod in ModLoader.Mods)
			{
				RecipeLoader.CurrentMod = mod;
				try
				{
					addRecipesMethod.Invoke(mod, Array.Empty<object>());
					SystemLoader.AddRecipes(mod);
					LoaderUtils.ForEachAndAggregateExceptions<ModItem>(mod.GetContent<ModItem>(), delegate(ModItem item)
					{
						item.AddRecipes();
					});
					LoaderUtils.ForEachAndAggregateExceptions<GlobalItem>(mod.GetContent<GlobalItem>(), delegate(GlobalItem global)
					{
						global.AddRecipes();
					});
				}
				catch (Exception ex)
				{
					ex.Data["mod"] = mod.Name;
					throw;
				}
				finally
				{
					RecipeLoader.CurrentMod = null;
				}
			}
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x005016FC File Offset: 0x004FF8FC
		internal static void PostAddRecipes()
		{
			MethodInfo postAddRecipesMethod = typeof(Mod).GetMethod("PostAddRecipes", BindingFlags.Instance | BindingFlags.Public);
			foreach (Mod mod in ModLoader.Mods)
			{
				RecipeLoader.CurrentMod = mod;
				try
				{
					postAddRecipesMethod.Invoke(mod, Array.Empty<object>());
					SystemLoader.PostAddRecipes(mod);
				}
				catch (Exception ex)
				{
					ex.Data["mod"] = mod.Name;
					throw;
				}
				finally
				{
					RecipeLoader.CurrentMod = null;
				}
			}
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x00501790 File Offset: 0x004FF990
		internal static void PostSetupRecipes()
		{
			foreach (Mod mod in ModLoader.Mods)
			{
				RecipeLoader.CurrentMod = mod;
				try
				{
					SystemLoader.PostSetupRecipes(mod);
				}
				catch (Exception ex)
				{
					ex.Data["mod"] = mod.Name;
					throw;
				}
				finally
				{
					RecipeLoader.CurrentMod = null;
				}
			}
		}

		/// <summary>
		/// Orders everything in the recipe according to their Ordering.
		/// </summary>
		// Token: 0x060026ED RID: 9965 RVA: 0x00501800 File Offset: 0x004FFA00
		internal static void OrderRecipes()
		{
			RecipeLoader.<>c__DisplayClass11_0 CS$<>8__locals1;
			CS$<>8__locals1.sortBefore = new Dictionary<Recipe, List<Recipe>>();
			CS$<>8__locals1.sortAfter = new Dictionary<Recipe, List<Recipe>>();
			List<Recipe> baseOrder = new List<Recipe>(Main.recipe.Length);
			foreach (Recipe r in Main.recipe)
			{
				ValueTuple<Recipe, bool> ordering = r.Ordering;
				Recipe target = ordering.Item1;
				if (target != null)
				{
					if (ordering.Item2)
					{
						Recipe target2 = target;
						List<Recipe> after;
						if (!CS$<>8__locals1.sortAfter.TryGetValue(target2, out after))
						{
							after = (CS$<>8__locals1.sortAfter[target2] = new List<Recipe>());
						}
						after.Add(r);
					}
					else
					{
						List<Recipe> before;
						if (!CS$<>8__locals1.sortBefore.TryGetValue(target, out before))
						{
							before = (CS$<>8__locals1.sortBefore[target] = new List<Recipe>());
						}
						before.Add(r);
					}
				}
				else
				{
					baseOrder.Add(r);
				}
			}
			if (!CS$<>8__locals1.sortBefore.Any<KeyValuePair<Recipe, List<Recipe>>>() && !CS$<>8__locals1.sortAfter.Any<KeyValuePair<Recipe, List<Recipe>>>())
			{
				return;
			}
			CS$<>8__locals1.i = 0;
			foreach (Recipe r2 in baseOrder)
			{
				RecipeLoader.<OrderRecipes>g__Sort|11_0(r2, ref CS$<>8__locals1);
			}
			if (CS$<>8__locals1.i != Main.recipe.Length)
			{
				throw new Exception("Sorting code is broken?");
			}
		}

		/// <summary>
		/// Returns whether or not the conditions are met for this recipe to be available for the player to use.
		/// </summary>
		/// <param name="recipe">The recipe to check.</param>
		/// <returns>Whether or not the conditions are met for this recipe.</returns>
		// Token: 0x060026EE RID: 9966 RVA: 0x00501968 File Offset: 0x004FFB68
		public static bool RecipeAvailable(Recipe recipe)
		{
			return recipe.Conditions.All((Condition c) => c.IsMet());
		}

		/// <summary>
		/// Returns whether or not the conditions are met for this recipe to be shimmered/decrafted.
		/// </summary>
		/// <param name="recipe">The recipe to check.</param>
		/// <returns>Whether or not the conditions are met for this recipe.</returns>
		// Token: 0x060026EF RID: 9967 RVA: 0x00501994 File Offset: 0x004FFB94
		public static bool DecraftAvailable(Recipe recipe)
		{
			if (!recipe.notDecraftable)
			{
				return recipe.DecraftConditions.All((Condition c) => c.IsMet());
			}
			return false;
		}

		/// <summary>
		/// recipe.OnCraftHooks followed by Calls ItemLoader.OnCreate with a RecipeItemCreationContext
		/// </summary>
		/// <param name="item">The item crafted.</param>
		/// <param name="recipe">The recipe used to craft the item.</param>
		/// <param name="consumedItems">Materials used to craft the item.</param>
		/// <param name="destinationStack">The stack that the crafted item will be combined with</param>
		// Token: 0x060026F0 RID: 9968 RVA: 0x005019CA File Offset: 0x004FFBCA
		public static void OnCraft(Item item, Recipe recipe, List<Item> consumedItems, Item destinationStack)
		{
			Recipe.OnCraftCallback onCraftHooks = recipe.OnCraftHooks;
			if (onCraftHooks != null)
			{
				onCraftHooks(recipe, item, consumedItems, destinationStack);
			}
			item.OnCreated(new RecipeItemCreationContext(recipe, consumedItems, destinationStack));
		}

		/// <summary>
		/// Helper version of OnCraft, used in combination with Recipe.Create and the internal ConsumedItems list
		/// </summary>
		/// <param name="item"></param>
		/// <param name="recipe"></param>
		/// <param name="destinationStack">The stack that the crafted item will be combined with</param>
		// Token: 0x060026F1 RID: 9969 RVA: 0x005019EF File Offset: 0x004FFBEF
		public static void OnCraft(Item item, Recipe recipe, Item destinationStack)
		{
			RecipeLoader.OnCraft(item, recipe, RecipeLoader.ConsumedItems, destinationStack);
			RecipeLoader.ConsumedItems.Clear();
		}

		/// <summary>
		/// Allows to edit the amount of item the player uses in a recipe. Also used to decide the amount a shimmer transformation returns
		/// </summary>
		/// <param name="recipe">The recipe used for the craft.</param>
		/// <param name="type">Type of the ingredient.</param>
		/// <param name="amount">Modifiable amount of the item consumed.</param>
		// Token: 0x060026F2 RID: 9970 RVA: 0x00501A08 File Offset: 0x004FFC08
		[Obsolete("Replaced by ConsumeIngredient due to not accounting for shimmer decrafting")]
		public static void ConsumeItem(Recipe recipe, int type, ref int amount)
		{
			RecipeLoader.ConsumeIngredient(recipe, type, ref amount, false);
		}

		/// <summary>
		/// Allows to edit the amount of item the player uses in a recipe. Also used to decide the amount a shimmer transformation returns
		/// </summary>
		/// <param name="recipe">The recipe used for the craft.</param>
		/// <param name="type">Type of the ingredient.</param>
		/// <param name="amount">Modifiable amount of the item consumed.</param>
		/// <param name="isDecrafting">If the operation takes place during shimmer decrafting.</param>
		// Token: 0x060026F3 RID: 9971 RVA: 0x00501A13 File Offset: 0x004FFC13
		public static void ConsumeIngredient(Recipe recipe, int type, ref int amount, bool isDecrafting)
		{
			Recipe.IngredientQuantityCallback consumeIngredientHooks = recipe.ConsumeIngredientHooks;
			if (consumeIngredientHooks == null)
			{
				return;
			}
			consumeIngredientHooks(recipe, type, ref amount, isDecrafting);
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x00501A4C File Offset: 0x004FFC4C
		[CompilerGenerated]
		internal static void <OrderRecipes>g__Sort|11_0(Recipe r, ref RecipeLoader.<>c__DisplayClass11_0 A_1)
		{
			List<Recipe> before;
			if (A_1.sortBefore.TryGetValue(r, out before))
			{
				foreach (Recipe r2 in before)
				{
					RecipeLoader.<OrderRecipes>g__Sort|11_0(r2, ref A_1);
				}
			}
			r.RecipeIndex = A_1.i;
			Recipe[] recipe = Main.recipe;
			int i = A_1.i;
			A_1.i = i + 1;
			recipe[i] = r;
			List<Recipe> after;
			if (A_1.sortAfter.TryGetValue(r, out after))
			{
				foreach (Recipe r3 in after)
				{
					RecipeLoader.<OrderRecipes>g__Sort|11_0(r3, ref A_1);
				}
			}
		}

		// Token: 0x04001899 RID: 6297
		internal static Recipe[] FirstRecipeForItem = new Recipe[(int)ItemID.Count];

		/// <summary>
		/// Cloned list of Items consumed when crafting.  Cleared after the OnCreate and OnCraft hooks.
		/// </summary>
		// Token: 0x0400189A RID: 6298
		internal static List<Item> ConsumedItems = new List<Item>();

		/// <summary>
		/// Set when tML sets up modded recipes. Used to detect misuse of CreateRecipe
		/// </summary>
		// Token: 0x0400189B RID: 6299
		internal static bool setupRecipes = false;
	}
}
