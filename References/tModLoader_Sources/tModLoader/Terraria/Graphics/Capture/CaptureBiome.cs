using System;
using Terraria.ModLoader;

namespace Terraria.Graphics.Capture
{
	// Token: 0x02000476 RID: 1142
	public class CaptureBiome
	{
		// Token: 0x06003761 RID: 14177 RVA: 0x005877A7 File Offset: 0x005859A7
		public CaptureBiome(int backgroundIndex, int waterStyle, CaptureBiome.TileColorStyle tileColorStyle = CaptureBiome.TileColorStyle.Normal)
		{
			this.BackgroundIndex = backgroundIndex;
			this.WaterStyle = waterStyle;
			this.TileColor = tileColorStyle;
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x005877C4 File Offset: 0x005859C4
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
			default:
			{
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
			}
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x00587870 File Offset: 0x00585A70
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
			SceneEffectLoader.SceneEffectInstance sceneEffect = Main.LocalPlayer.CurrentSceneEffect;
			if (sceneEffect.waterStyle.value >= 15)
			{
				return new CaptureBiome(sceneEffect.surfaceBackground.value, sceneEffect.waterStyle.value, sceneEffect.tileColorStyle);
			}
			return null;
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x005878EC File Offset: 0x00585AEC
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
			{
				SceneEffectLoader.SceneEffectInstance sceneEffect = Main.LocalPlayer.CurrentSceneEffect;
				if (sceneEffect.surfaceBackground.value >= 14)
				{
					return new CaptureBiome(sceneEffect.surfaceBackground.value, sceneEffect.waterStyle.value, sceneEffect.tileColorStyle);
				}
				return null;
			}
			}
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x005879C8 File Offset: 0x00585BC8
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

		// Token: 0x04005111 RID: 20753
		public static readonly CaptureBiome DefaultPurity = new CaptureBiome(0, 0, CaptureBiome.TileColorStyle.Normal);

		// Token: 0x04005112 RID: 20754
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

		// Token: 0x04005113 RID: 20755
		public readonly int WaterStyle;

		// Token: 0x04005114 RID: 20756
		public readonly int BackgroundIndex;

		// Token: 0x04005115 RID: 20757
		public readonly CaptureBiome.TileColorStyle TileColor;

		// Token: 0x02000B82 RID: 2946
		public enum TileColorStyle
		{
			// Token: 0x04007622 RID: 30242
			Normal,
			// Token: 0x04007623 RID: 30243
			Jungle,
			// Token: 0x04007624 RID: 30244
			Crimson,
			// Token: 0x04007625 RID: 30245
			Corrupt,
			// Token: 0x04007626 RID: 30246
			Mushroom
		}

		// Token: 0x02000B83 RID: 2947
		public class Sets
		{
			// Token: 0x02000E4F RID: 3663
			public class WaterStyles
			{
				// Token: 0x04007D41 RID: 32065
				public const int BloodMoon = 9;
			}
		}

		// Token: 0x02000B84 RID: 2948
		public class Styles
		{
			// Token: 0x04007627 RID: 30247
			public static CaptureBiome Purity = new CaptureBiome(0, 0, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007628 RID: 30248
			public static CaptureBiome Purity2 = new CaptureBiome(10, 0, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007629 RID: 30249
			public static CaptureBiome Purity3 = new CaptureBiome(11, 0, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x0400762A RID: 30250
			public static CaptureBiome Purity4 = new CaptureBiome(12, 0, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x0400762B RID: 30251
			public static CaptureBiome Corruption = new CaptureBiome(1, 2, CaptureBiome.TileColorStyle.Corrupt);

			// Token: 0x0400762C RID: 30252
			public static CaptureBiome Jungle = new CaptureBiome(3, 3, CaptureBiome.TileColorStyle.Jungle);

			// Token: 0x0400762D RID: 30253
			public static CaptureBiome Hallow = new CaptureBiome(6, 4, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x0400762E RID: 30254
			public static CaptureBiome Snow = new CaptureBiome(7, 5, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x0400762F RID: 30255
			public static CaptureBiome Desert = new CaptureBiome(2, 6, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007630 RID: 30256
			public static CaptureBiome DirtLayer = new CaptureBiome(0, 7, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007631 RID: 30257
			public static CaptureBiome RockLayer = new CaptureBiome(0, 8, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007632 RID: 30258
			public static CaptureBiome BloodMoon = new CaptureBiome(0, 9, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007633 RID: 30259
			public static CaptureBiome Crimson = new CaptureBiome(8, 10, CaptureBiome.TileColorStyle.Crimson);

			// Token: 0x04007634 RID: 30260
			public static CaptureBiome UndergroundDesert = new CaptureBiome(2, 12, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007635 RID: 30261
			public static CaptureBiome Ocean = new CaptureBiome(4, 13, CaptureBiome.TileColorStyle.Normal);

			// Token: 0x04007636 RID: 30262
			public static CaptureBiome Mushroom = new CaptureBiome(9, 7, CaptureBiome.TileColorStyle.Mushroom);
		}

		// Token: 0x02000B85 RID: 2949
		private enum BiomeChoiceIndex
		{
			// Token: 0x04007638 RID: 30264
			AutomatedForPlayer = -1,
			// Token: 0x04007639 RID: 30265
			Purity = 1,
			// Token: 0x0400763A RID: 30266
			Corruption,
			// Token: 0x0400763B RID: 30267
			Jungle,
			// Token: 0x0400763C RID: 30268
			Hallow,
			// Token: 0x0400763D RID: 30269
			Snow,
			// Token: 0x0400763E RID: 30270
			Desert,
			// Token: 0x0400763F RID: 30271
			DirtLayer,
			// Token: 0x04007640 RID: 30272
			RockLayer,
			// Token: 0x04007641 RID: 30273
			Crimson,
			// Token: 0x04007642 RID: 30274
			UndergroundDesert,
			// Token: 0x04007643 RID: 30275
			Ocean,
			// Token: 0x04007644 RID: 30276
			Mushroom
		}
	}
}
