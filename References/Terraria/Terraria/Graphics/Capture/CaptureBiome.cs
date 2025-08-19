using System;

namespace Terraria.Graphics.Capture
{
	// Token: 0x020000F9 RID: 249
	public class CaptureBiome
	{
		// Token: 0x0600160E RID: 5646 RVA: 0x004C57BC File Offset: 0x004C39BC
		public CaptureBiome(int backgroundIndex, int waterStyle, CaptureBiome.TileColorStyle tileColorStyle = CaptureBiome.TileColorStyle.Normal)
		{
			this.BackgroundIndex = backgroundIndex;
			this.WaterStyle = waterStyle;
			this.TileColor = tileColorStyle;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x004C57DC File Offset: 0x004C39DC
		public static CaptureBiome GetCaptureBiome(int biomeChoice)
		{
			switch (biomeChoice)
			{
			case 1:
				return CaptureBiome.GetPurityForPlayer();
			case 2:
				return CaptureBiome.Styles.Corruption;
			case 3:
				return CaptureBiome.Styles.Jungle;
			case 4:
				return CaptureBiome.Styles.Hallow;
			case 5:
				return CaptureBiome.Styles.Snow;
			case 6:
				return CaptureBiome.Styles.Desert;
			case 7:
				return CaptureBiome.Styles.DirtLayer;
			case 8:
				return CaptureBiome.Styles.RockLayer;
			case 9:
				return CaptureBiome.Styles.Crimson;
			case 10:
				return CaptureBiome.Styles.UndergroundDesert;
			case 11:
				return CaptureBiome.Styles.Ocean;
			case 12:
				return CaptureBiome.Styles.Mushroom;
			}
			CaptureBiome biomeByLocation = CaptureBiome.GetBiomeByLocation();
			if (biomeByLocation != null)
			{
				return biomeByLocation;
			}
			CaptureBiome biomeByWater = CaptureBiome.GetBiomeByWater();
			if (biomeByWater != null)
			{
				return biomeByWater;
			}
			return CaptureBiome.GetPurityForPlayer();
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x004C5890 File Offset: 0x004C3A90
		private static CaptureBiome GetBiomeByWater()
		{
			int num = Main.CalculateWaterStyle(true);
			for (int i = 0; i < CaptureBiome.BiomesByWaterStyle.Length; i++)
			{
				CaptureBiome captureBiome = CaptureBiome.BiomesByWaterStyle[i];
				if (captureBiome != null && captureBiome.WaterStyle == num)
				{
					return captureBiome;
				}
			}
			return null;
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x004C58D0 File Offset: 0x004C3AD0
		private static CaptureBiome GetBiomeByLocation()
		{
			switch (Main.GetPreferredBGStyleForPlayer())
			{
			case 0:
				return CaptureBiome.Styles.Purity;
			case 1:
				return CaptureBiome.Styles.Corruption;
			case 2:
				return CaptureBiome.Styles.Desert;
			case 3:
				return CaptureBiome.Styles.Jungle;
			case 4:
				return CaptureBiome.Styles.Ocean;
			case 5:
				return CaptureBiome.Styles.Desert;
			case 6:
				return CaptureBiome.Styles.Hallow;
			case 7:
				return CaptureBiome.Styles.Snow;
			case 8:
				return CaptureBiome.Styles.Crimson;
			case 9:
				return CaptureBiome.Styles.Mushroom;
			case 10:
				return CaptureBiome.Styles.Purity2;
			case 11:
				return CaptureBiome.Styles.Purity3;
			case 12:
				return CaptureBiome.Styles.Purity4;
			default:
				return null;
			}
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x004C5970 File Offset: 0x004C3B70
		private static CaptureBiome GetPurityForPlayer()
		{
			int num = (int)Main.LocalPlayer.Center.X / 16;
			if (num < Main.treeX[0])
			{
				return CaptureBiome.Styles.Purity;
			}
			if (num < Main.treeX[1])
			{
				return CaptureBiome.Styles.Purity2;
			}
			if (num < Main.treeX[2])
			{
				return CaptureBiome.Styles.Purity3;
			}
			return CaptureBiome.Styles.Purity4;
		}

		// Token: 0x04001309 RID: 4873
		public static readonly CaptureBiome DefaultPurity = new CaptureBiome(0, 0, CaptureBiome.TileColorStyle.Normal);

		// Token: 0x0400130A RID: 4874
		public static CaptureBiome[] BiomesByWaterStyle = new CaptureBiome[]
		{
			null,
			null,
			CaptureBiome.Styles.Corruption,
			CaptureBiome.Styles.Jungle,
			CaptureBiome.Styles.Hallow,
			CaptureBiome.Styles.Snow,
			CaptureBiome.Styles.Desert,
			CaptureBiome.Styles.DirtLayer,
			CaptureBiome.Styles.RockLayer,
			CaptureBiome.Styles.BloodMoon,
			CaptureBiome.Styles.Crimson,
			null,
			CaptureBiome.Styles.UndergroundDesert,
			CaptureBiome.Styles.Ocean,
			CaptureBiome.Styles.Mushroom
		};

		// Token: 0x0400130B RID: 4875
		public readonly int WaterStyle;

		// Token: 0x0400130C RID: 4876
		public readonly int BackgroundIndex;

		// Token: 0x0400130D RID: 4877
		public readonly CaptureBiome.TileColorStyle TileColor;

		// Token: 0x0200058E RID: 1422
		public enum TileColorStyle
		{
			// Token: 0x040059DE RID: 23006
			Normal,
			// Token: 0x040059DF RID: 23007
			Jungle,
			// Token: 0x040059E0 RID: 23008
			Crimson,
			// Token: 0x040059E1 RID: 23009
			Corrupt,
			// Token: 0x040059E2 RID: 23010
			Mushroom
		}

		// Token: 0x0200058F RID: 1423
		public class Sets
		{
			// Token: 0x02000813 RID: 2067
			public class WaterStyles
			{
				// Token: 0x040064F0 RID: 25840
				public const int BloodMoon = 9;
			}
		}

		// Token: 0x02000590 RID: 1424
		public class Styles
		{
			// Token: 0x040059E3 RID: 23011
			public static CaptureBiome Purity = new CaptureBiome(0, 0, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x040059E4 RID: 23012
			public static CaptureBiome Purity2 = new CaptureBiome(10, 0, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x040059E5 RID: 23013
			public static CaptureBiome Purity3 = new CaptureBiome(11, 0, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x040059E6 RID: 23014
			public static CaptureBiome Purity4 = new CaptureBiome(12, 0, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x040059E7 RID: 23015
			public static CaptureBiome Corruption = new CaptureBiome(1, 2, CaptureBiome.TileColorStyle.Corrupt);

			// Token: 0x040059E8 RID: 23016
			public static CaptureBiome Jungle = new CaptureBiome(3, 3, CaptureBiome.TileColorStyle.Jungle);

			// Token: 0x040059E9 RID: 23017
			public static CaptureBiome Hallow = new CaptureBiome(6, 4, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x040059EA RID: 23018
			public static CaptureBiome Snow = new CaptureBiome(7, 5, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x040059EB RID: 23019
			public static CaptureBiome Desert = new CaptureBiome(2, 6, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x040059EC RID: 23020
			public static CaptureBiome DirtLayer = new CaptureBiome(0, 7, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x040059ED RID: 23021
			public static CaptureBiome RockLayer = new CaptureBiome(0, 8, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x040059EE RID: 23022
			public static CaptureBiome BloodMoon = new CaptureBiome(0, 9, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x040059EF RID: 23023
			public static CaptureBiome Crimson = new CaptureBiome(8, 10, CaptureBiome.TileColorStyle.Crimson);

			// Token: 0x040059F0 RID: 23024
			public static CaptureBiome UndergroundDesert = new CaptureBiome(2, 12, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x040059F1 RID: 23025
			public static CaptureBiome Ocean = new CaptureBiome(4, 13, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x040059F2 RID: 23026
			public static CaptureBiome Mushroom = new CaptureBiome(9, 7, CaptureBiome.TileColorStyle.Mushroom);
		}

		// Token: 0x02000591 RID: 1425
		private enum BiomeChoiceIndex
		{
			// Token: 0x040059F4 RID: 23028
			AutomatedForPlayer = -1,
			// Token: 0x040059F5 RID: 23029
			Purity = 1,
			// Token: 0x040059F6 RID: 23030
			Corruption,
			// Token: 0x040059F7 RID: 23031
			Jungle,
			// Token: 0x040059F8 RID: 23032
			Hallow,
			// Token: 0x040059F9 RID: 23033
			Snow,
			// Token: 0x040059FA RID: 23034
			Desert,
			// Token: 0x040059FB RID: 23035
			DirtLayer,
			// Token: 0x040059FC RID: 23036
			RockLayer,
			// Token: 0x040059FD RID: 23037
			Crimson,
			// Token: 0x040059FE RID: 23038
			UndergroundDesert,
			// Token: 0x040059FF RID: 23039
			Ocean,
			// Token: 0x04005A00 RID: 23040
			Mushroom
		}
	}
}
