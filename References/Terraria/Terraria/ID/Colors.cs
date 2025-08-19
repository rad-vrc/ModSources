using System;
using Microsoft.Xna.Framework;

namespace Terraria.ID
{
	// Token: 0x020001B2 RID: 434
	public static class Colors
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x004EA718 File Offset: 0x004E8918
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

		// Token: 0x06001B99 RID: 7065 RVA: 0x004EA775 File Offset: 0x004E8975
		public static Color AlphaDarken(Color input)
		{
			return input * ((float)Main.mouseTextColor / 255f);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x004EA78C File Offset: 0x004E898C
		public static Color GetSelectionGlowColor(bool isTileSelected, int averageTileLighting)
		{
			Color result;
			if (isTileSelected)
			{
				result = new Color(averageTileLighting, averageTileLighting, averageTileLighting / 3, averageTileLighting);
			}
			else
			{
				result = new Color(averageTileLighting / 2, averageTileLighting / 2, averageTileLighting / 2, averageTileLighting);
			}
			return result;
		}

		// Token: 0x04001A09 RID: 6665
		public static readonly Color RarityAmber = new Color(255, 175, 0);

		// Token: 0x04001A0A RID: 6666
		public static readonly Color RarityTrash = new Color(130, 130, 130);

		// Token: 0x04001A0B RID: 6667
		public static readonly Color RarityNormal = Color.White;

		// Token: 0x04001A0C RID: 6668
		public static readonly Color RarityBlue = new Color(150, 150, 255);

		// Token: 0x04001A0D RID: 6669
		public static readonly Color RarityGreen = new Color(150, 255, 150);

		// Token: 0x04001A0E RID: 6670
		public static readonly Color RarityOrange = new Color(255, 200, 150);

		// Token: 0x04001A0F RID: 6671
		public static readonly Color RarityRed = new Color(255, 150, 150);

		// Token: 0x04001A10 RID: 6672
		public static readonly Color RarityPink = new Color(255, 150, 255);

		// Token: 0x04001A11 RID: 6673
		public static readonly Color RarityPurple = new Color(210, 160, 255);

		// Token: 0x04001A12 RID: 6674
		public static readonly Color RarityLime = new Color(150, 255, 10);

		// Token: 0x04001A13 RID: 6675
		public static readonly Color RarityYellow = new Color(255, 255, 10);

		// Token: 0x04001A14 RID: 6676
		public static readonly Color RarityCyan = new Color(5, 200, 255);

		// Token: 0x04001A15 RID: 6677
		public static readonly Color CoinPlatinum = new Color(220, 220, 198);

		// Token: 0x04001A16 RID: 6678
		public static readonly Color CoinGold = new Color(224, 201, 92);

		// Token: 0x04001A17 RID: 6679
		public static readonly Color CoinSilver = new Color(181, 192, 193);

		// Token: 0x04001A18 RID: 6680
		public static readonly Color CoinCopper = new Color(246, 138, 96);

		// Token: 0x04001A19 RID: 6681
		public static readonly Color AmbientNPCGastropodLight = new Color(102, 0, 63);

		// Token: 0x04001A1A RID: 6682
		public static readonly Color JourneyMode = Color.Lerp(Color.HotPink, Color.White, 0.1f);

		// Token: 0x04001A1B RID: 6683
		public static readonly Color Mediumcore = new Color(1f, 0.6f, 0f);

		// Token: 0x04001A1C RID: 6684
		public static readonly Color Hardcore = new Color(1f, 0.15f, 0.1f);

		// Token: 0x04001A1D RID: 6685
		public static readonly Color LanternBG = new Color(120, 50, 20);

		// Token: 0x04001A1E RID: 6686
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

		// Token: 0x04001A1F RID: 6687
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

		// Token: 0x04001A20 RID: 6688
		public static readonly Color FancyUIFatButtonMouseOver = Main.OurFavoriteColor;

		// Token: 0x04001A21 RID: 6689
		public static readonly Color InventoryDefaultColor = new Color(63, 65, 151, 255);

		// Token: 0x04001A22 RID: 6690
		public static readonly Color InventoryDefaultColorWithOpacity = new Color(63, 65, 151, 255) * 0.785f;
	}
}
