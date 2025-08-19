using System;
using Microsoft.Xna.Framework;

namespace Terraria.ID
{
	// Token: 0x020003FD RID: 1021
	public static class Colors
	{
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600350B RID: 13579 RVA: 0x0056BE9C File Offset: 0x0056A09C
		public static Color CurrentLiquidColor
		{
			get
			{
				Color color = Color.Transparent;
				bool flag = true;
				for (int i = 0; i < 11; i++)
				{
					if (Main.liquidAlpha[i] > 0f)
					{
						if (flag)
						{
							flag = false;
							color = Colors._liquidColors[i];
						}
						else
						{
							color = Color.Lerp(color, Colors._liquidColors[i], Main.liquidAlpha[i]);
						}
					}
				}
				return color;
			}
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x0056BEF9 File Offset: 0x0056A0F9
		public static Color AlphaDarken(Color input)
		{
			return input * ((float)Main.mouseTextColor / 255f);
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x0056BF0D File Offset: 0x0056A10D
		public static Color GetSelectionGlowColor(bool isTileSelected, int averageTileLighting)
		{
			if (isTileSelected)
			{
				return new Color(averageTileLighting, averageTileLighting, averageTileLighting / 3, averageTileLighting);
			}
			return new Color(averageTileLighting / 2, averageTileLighting / 2, averageTileLighting / 2, averageTileLighting);
		}

		// Token: 0x040020B6 RID: 8374
		public static readonly Color RarityAmber = new Color(255, 175, 0);

		// Token: 0x040020B7 RID: 8375
		public static readonly Color RarityTrash = new Color(130, 130, 130);

		// Token: 0x040020B8 RID: 8376
		public static readonly Color RarityNormal = Color.White;

		// Token: 0x040020B9 RID: 8377
		public static readonly Color RarityBlue = new Color(150, 150, 255);

		// Token: 0x040020BA RID: 8378
		public static readonly Color RarityGreen = new Color(150, 255, 150);

		// Token: 0x040020BB RID: 8379
		public static readonly Color RarityOrange = new Color(255, 200, 150);

		// Token: 0x040020BC RID: 8380
		public static readonly Color RarityRed = new Color(255, 150, 150);

		// Token: 0x040020BD RID: 8381
		public static readonly Color RarityPink = new Color(255, 150, 255);

		// Token: 0x040020BE RID: 8382
		public static readonly Color RarityPurple = new Color(210, 160, 255);

		// Token: 0x040020BF RID: 8383
		public static readonly Color RarityLime = new Color(150, 255, 10);

		// Token: 0x040020C0 RID: 8384
		public static readonly Color RarityYellow = new Color(255, 255, 10);

		// Token: 0x040020C1 RID: 8385
		public static readonly Color RarityCyan = new Color(5, 200, 255);

		// Token: 0x040020C2 RID: 8386
		public static readonly Color CoinPlatinum = new Color(220, 220, 198);

		// Token: 0x040020C3 RID: 8387
		public static readonly Color CoinGold = new Color(224, 201, 92);

		// Token: 0x040020C4 RID: 8388
		public static readonly Color CoinSilver = new Color(181, 192, 193);

		// Token: 0x040020C5 RID: 8389
		public static readonly Color CoinCopper = new Color(246, 138, 96);

		// Token: 0x040020C6 RID: 8390
		public static readonly Color AmbientNPCGastropodLight = new Color(102, 0, 63);

		// Token: 0x040020C7 RID: 8391
		public static readonly Color JourneyMode = Color.Lerp(Color.HotPink, Color.White, 0.1f);

		// Token: 0x040020C8 RID: 8392
		public static readonly Color Mediumcore = new Color(1f, 0.6f, 0f);

		// Token: 0x040020C9 RID: 8393
		public static readonly Color Hardcore = new Color(1f, 0.15f, 0.1f);

		// Token: 0x040020CA RID: 8394
		public static readonly Color LanternBG = new Color(120, 50, 20);

		// Token: 0x040020CB RID: 8395
		public static readonly Color[] _waterfallColors = new Color[]
		{
			new Color(9, 61, 191),
			new Color(253, 32, 3),
			new Color(143, 143, 143),
			new Color(59, 29, 131),
			new Color(7, 145, 142),
			new Color(171, 11, 209),
			new Color(9, 137, 191),
			new Color(168, 106, 32),
			new Color(36, 60, 148),
			new Color(65, 59, 101),
			new Color(200, 0, 0),
			default(Color),
			default(Color),
			new Color(177, 54, 79),
			new Color(255, 156, 12),
			new Color(91, 34, 104),
			new Color(102, 104, 34),
			new Color(34, 43, 104),
			new Color(34, 104, 38),
			new Color(104, 34, 34),
			new Color(76, 79, 102),
			new Color(104, 61, 34)
		};

		// Token: 0x040020CC RID: 8396
		public static readonly Color[] _liquidColors = new Color[]
		{
			new Color(9, 61, 191),
			new Color(253, 32, 3),
			new Color(59, 29, 131),
			new Color(7, 145, 142),
			new Color(171, 11, 209),
			new Color(9, 137, 191),
			new Color(168, 106, 32),
			new Color(36, 60, 148),
			new Color(65, 59, 101),
			new Color(200, 0, 0),
			new Color(177, 54, 79),
			new Color(255, 156, 12)
		};

		// Token: 0x040020CD RID: 8397
		public static readonly Color FancyUIFatButtonMouseOver = Main.OurFavoriteColor;

		// Token: 0x040020CE RID: 8398
		public static readonly Color InventoryDefaultColor = new Color(63, 65, 151, 255);

		// Token: 0x040020CF RID: 8399
		public static readonly Color InventoryDefaultColorWithOpacity = new Color(63, 65, 151, 255) * 0.785f;

		// Token: 0x040020D0 RID: 8400
		public static readonly Color RarityDarkRed = new Color(255, 40, 100);

		// Token: 0x040020D1 RID: 8401
		public static readonly Color RarityDarkPurple = new Color(180, 40, 255);
	}
}
