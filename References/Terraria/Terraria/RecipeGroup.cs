using System;
using System.Collections.Generic;

namespace Terraria
{
	// Token: 0x02000037 RID: 55
	public class RecipeGroup
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0010D46E File Offset: 0x0010B66E
		public RecipeGroup(Func<string> getName, params int[] validItems)
		{
			this.GetText = getName;
			this.ValidItems = new HashSet<int>(validItems);
			this.IconicItemId = validItems[0];
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0010D494 File Offset: 0x0010B694
		public static int RegisterGroup(string name, RecipeGroup rec)
		{
			int num = RecipeGroup.nextRecipeGroupIndex++;
			rec.RegisteredId = num;
			RecipeGroup.recipeGroups.Add(num, rec);
			RecipeGroup.recipeGroupIDs.Add(name, num);
			return num;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0010D4D0 File Offset: 0x0010B6D0
		public int CountUsableItems(Dictionary<int, int> itemStacksAvailable)
		{
			int num = 0;
			foreach (int key in this.ValidItems)
			{
				int num2;
				if (itemStacksAvailable.TryGetValue(key, out num2))
				{
					num += num2;
				}
			}
			return num;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0010D530 File Offset: 0x0010B730
		public int GetGroupFakeItemId()
		{
			return this.RegisteredId + 1000000;
		}

		// Token: 0x0400029B RID: 667
		public Func<string> GetText;

		// Token: 0x0400029C RID: 668
		public HashSet<int> ValidItems;

		// Token: 0x0400029D RID: 669
		public int IconicItemId;

		// Token: 0x0400029E RID: 670
		public int RegisteredId;

		// Token: 0x0400029F RID: 671
		public static Dictionary<int, RecipeGroup> recipeGroups = new Dictionary<int, RecipeGroup>();

		// Token: 0x040002A0 RID: 672
		public static Dictionary<string, int> recipeGroupIDs = new Dictionary<string, int>();

		// Token: 0x040002A1 RID: 673
		public static int nextRecipeGroupIndex;
	}
}
