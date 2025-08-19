using System;
using System.Reflection;

namespace Terraria.ModLoader.Utilities
{
	// Token: 0x02000239 RID: 569
	internal static class RecipeGroupHelper
	{
		// Token: 0x060028D0 RID: 10448 RVA: 0x0050E614 File Offset: 0x0050C814
		internal static void ResetRecipeGroups()
		{
			RecipeGroup.recipeGroups.Clear();
			RecipeGroup.recipeGroupIDs.Clear();
			RecipeGroup.nextRecipeGroupIndex = 0;
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x0050E630 File Offset: 0x0050C830
		internal static void AddRecipeGroups()
		{
			MethodInfo addRecipeGroupsMethod = typeof(Mod).GetMethod("AddRecipeGroups", BindingFlags.Instance | BindingFlags.Public);
			foreach (Mod mod in ModLoader.Mods)
			{
				try
				{
					addRecipeGroupsMethod.Invoke(mod, Array.Empty<object>());
					SystemLoader.AddRecipeGroups(mod);
				}
				catch (Exception ex)
				{
					ex.Data["mod"] = mod.Name;
					throw;
				}
			}
			RecipeGroupHelper.CreateRecipeGroupLookups();
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x0050E6B0 File Offset: 0x0050C8B0
		internal static void CreateRecipeGroupLookups()
		{
			for (int i = 0; i < RecipeGroup.nextRecipeGroupIndex; i++)
			{
				RecipeGroup rec = RecipeGroup.recipeGroups[i];
				rec.ValidItemsLookup = new bool[ItemLoader.ItemCount];
				foreach (int type in rec.ValidItems)
				{
					rec.ValidItemsLookup[type] = true;
				}
			}
		}
	}
}
