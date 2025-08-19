using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent.UI
{
	// Token: 0x020004CE RID: 1230
	public class ItemRarity
	{
		// Token: 0x06003ABD RID: 15037 RVA: 0x005AC004 File Offset: 0x005AA204
		public static void Initialize()
		{
			ItemRarity._rarities.Clear();
			ItemRarity._rarities.Add(-11, Colors.RarityAmber);
			ItemRarity._rarities.Add(-1, Colors.RarityTrash);
			ItemRarity._rarities.Add(1, Colors.RarityBlue);
			ItemRarity._rarities.Add(2, Colors.RarityGreen);
			ItemRarity._rarities.Add(3, Colors.RarityOrange);
			ItemRarity._rarities.Add(4, Colors.RarityRed);
			ItemRarity._rarities.Add(5, Colors.RarityPink);
			ItemRarity._rarities.Add(6, Colors.RarityPurple);
			ItemRarity._rarities.Add(7, Colors.RarityLime);
			ItemRarity._rarities.Add(8, Colors.RarityYellow);
			ItemRarity._rarities.Add(9, Colors.RarityCyan);
			ItemRarity._rarities.Add(10, Colors.RarityDarkRed);
			ItemRarity._rarities.Add(11, Colors.RarityDarkPurple);
			for (int i = 12; i < RarityLoader.RarityCount; i++)
			{
				ItemRarity._rarities.Add(i, RarityLoader.GetRarity(i).RarityColor);
			}
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x005AC118 File Offset: 0x005AA318
		public static Color GetColor(int rarity)
		{
			Color result;
			result..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			if (ItemRarity._rarities.ContainsKey(rarity))
			{
				return ItemRarity._rarities[rarity];
			}
			return result;
		}

		// Token: 0x040054C0 RID: 21696
		private static Dictionary<int, Color> _rarities = new Dictionary<int, Color>();
	}
}
