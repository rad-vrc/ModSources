using System;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x020004C1 RID: 1217
	public static class TreePaintSystemData
	{
		// Token: 0x06003A4C RID: 14924 RVA: 0x005A6F5C File Offset: 0x005A515C
		public static TreePaintingSettings GetTileSettings(int tileType, int tileStyle)
		{
			if (tileType <= 109)
			{
				if (tileType > 5)
				{
					if (tileType <= 60)
					{
						if (tileType == 23)
						{
							goto IL_104;
						}
						if (tileType - 59 > 1)
						{
							goto IL_FE;
						}
					}
					else if (tileType != 70)
					{
						if (tileType != 109)
						{
							goto IL_FE;
						}
						goto IL_104;
					}
					return TreePaintSystemData.CullMud;
				}
				if (tileType == 0 || tileType == 2)
				{
					goto IL_104;
				}
				if (tileType == 5)
				{
					switch (tileStyle)
					{
					case 0:
						return TreePaintSystemData.WoodCorruption;
					case 1:
						return TreePaintSystemData.WoodJungle;
					case 2:
						return TreePaintSystemData.WoodHallow;
					case 3:
						return TreePaintSystemData.WoodSnow;
					case 4:
						return TreePaintSystemData.WoodCrimson;
					case 5:
						return TreePaintSystemData.WoodJungleUnderground;
					case 6:
						return TreePaintSystemData.WoodGlowingMushroom;
					default:
					{
						int lookup = Math.Abs(tileStyle) - 7;
						ModTree tree = PlantLoader.Get<ModTree>(tileType, lookup);
						if (tree != null)
						{
							return tree.TreeShaderSettings;
						}
						return TreePaintSystemData.WoodPurity;
					}
					}
				}
			}
			else if (tileType <= 492)
			{
				if (tileType <= 323)
				{
					if (tileType == 199)
					{
						goto IL_104;
					}
					if (tileType == 323)
					{
						switch (tileStyle)
						{
						case 0:
						case 4:
							return TreePaintSystemData.PalmTreePurity;
						case 1:
						case 5:
							return TreePaintSystemData.PalmTreeCrimson;
						case 2:
						case 6:
							return TreePaintSystemData.PalmTreeHallow;
						case 3:
						case 7:
							return TreePaintSystemData.PalmTreeCorruption;
						default:
						{
							int lookup2 = Math.Abs(tileStyle) - 8;
							ModPalmTree palm = PlantLoader.Get<ModPalmTree>(tileType, lookup2);
							if (palm != null)
							{
								return palm.TreeShaderSettings;
							}
							return TreePaintSystemData.WoodPurity;
						}
						}
					}
				}
				else if (tileType == 477 || tileType == 492)
				{
					goto IL_104;
				}
			}
			else if (tileType <= 616)
			{
				switch (tileType)
				{
				case 583:
					return TreePaintSystemData.GemTreeTopaz;
				case 584:
					return TreePaintSystemData.GemTreeAmethyst;
				case 585:
					return TreePaintSystemData.GemTreeSapphire;
				case 586:
					return TreePaintSystemData.GemTreeEmerald;
				case 587:
					return TreePaintSystemData.GemTreeRuby;
				case 588:
					return TreePaintSystemData.GemTreeDiamond;
				case 589:
					return TreePaintSystemData.GemTreeAmber;
				case 590:
				case 591:
				case 592:
				case 593:
				case 594:
					break;
				case 595:
				case 596:
					return TreePaintSystemData.VanityCherry;
				default:
					if (tileType - 615 <= 1)
					{
						return TreePaintSystemData.VanityYellowWillow;
					}
					break;
				}
			}
			else
			{
				if (tileType == 633)
				{
					goto IL_104;
				}
				if (tileType == 634)
				{
					return TreePaintSystemData.TreeAsh;
				}
			}
			IL_FE:
			return TreePaintSystemData.DefaultNoSpecialGroups;
			IL_104:
			return TreePaintSystemData.DefaultDirt;
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x005A7180 File Offset: 0x005A5380
		public static TreePaintingSettings GetTreeFoliageSettings(int foliageIndex, int foliageStyle)
		{
			switch (foliageIndex)
			{
			case 0:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
				return TreePaintSystemData.WoodPurity;
			case 1:
				return TreePaintSystemData.WoodCorruption;
			case 2:
			case 11:
			case 13:
				return TreePaintSystemData.WoodJungle;
			case 3:
			case 19:
			case 20:
				return TreePaintSystemData.WoodHallow;
			case 4:
			case 12:
			case 16:
			case 17:
			case 18:
				return TreePaintSystemData.WoodSnow;
			case 5:
				return TreePaintSystemData.WoodCrimson;
			case 14:
				return TreePaintSystemData.WoodGlowingMushroom;
			case 15:
			case 21:
				switch (foliageStyle)
				{
				case 0:
				case 4:
					return TreePaintSystemData.PalmTreePurity;
				case 1:
				case 5:
					return TreePaintSystemData.PalmTreeCrimson;
				case 2:
				case 6:
					return TreePaintSystemData.PalmTreeHallow;
				case 3:
				case 7:
					return TreePaintSystemData.PalmTreeCorruption;
				default:
					return TreePaintSystemData.WoodPurity;
				}
				break;
			case 22:
				return TreePaintSystemData.GemTreeTopaz;
			case 23:
				return TreePaintSystemData.GemTreeAmethyst;
			case 24:
				return TreePaintSystemData.GemTreeSapphire;
			case 25:
				return TreePaintSystemData.GemTreeEmerald;
			case 26:
				return TreePaintSystemData.GemTreeRuby;
			case 27:
				return TreePaintSystemData.GemTreeDiamond;
			case 28:
				return TreePaintSystemData.GemTreeAmber;
			case 29:
				return TreePaintSystemData.VanityCherry;
			case 30:
				return TreePaintSystemData.VanityYellowWillow;
			default:
				if (foliageIndex >= 100)
				{
					int lookup = foliageIndex - 100;
					ModTree tree = PlantLoader.Get<ModTree>(5, lookup);
					if (tree != null)
					{
						return tree.TreeShaderSettings;
					}
				}
				else if (foliageIndex < 0)
				{
					int lookup2 = -1 * foliageIndex;
					if (lookup2 % 2 == 0)
					{
						lookup2 /= 2;
					}
					else
					{
						lookup2 = (lookup2 - 1) / 2;
					}
					ModPalmTree tree2 = PlantLoader.Get<ModPalmTree>(323, lookup2);
					if (tree2 != null)
					{
						return tree2.TreeShaderSettings;
					}
				}
				return TreePaintSystemData.DefaultDirt;
			}
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x005A7306 File Offset: 0x005A5506
		public static TreePaintingSettings GetWallSettings(int wallType)
		{
			return TreePaintSystemData.DefaultNoSpecialGroups_ForWalls;
		}

		// Token: 0x040053DF RID: 21471
		private static TreePaintingSettings DefaultNoSpecialGroups = new TreePaintingSettings
		{
			UseSpecialGroups = false
		};

		// Token: 0x040053E0 RID: 21472
		private static TreePaintingSettings DefaultNoSpecialGroups_ForWalls = new TreePaintingSettings
		{
			UseSpecialGroups = false,
			UseWallShaderHacks = true
		};

		// Token: 0x040053E1 RID: 21473
		private static TreePaintingSettings DefaultDirt = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.03f,
			SpecialGroupMaximumHueValue = 0.08f,
			SpecialGroupMinimumSaturationValue = 0.38f,
			SpecialGroupMaximumSaturationValue = 0.53f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x040053E2 RID: 21474
		private static TreePaintingSettings CullMud = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			HueTestOffset = 0.5f,
			SpecialGroupMinimalHueValue = 0.42f,
			SpecialGroupMaximumHueValue = 0.55f,
			SpecialGroupMinimumSaturationValue = 0.2f,
			SpecialGroupMaximumSaturationValue = 0.27f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x040053E3 RID: 21475
		private static TreePaintingSettings WoodPurity = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.16666667f,
			SpecialGroupMaximumHueValue = 0.8333333f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x040053E4 RID: 21476
		private static TreePaintingSettings WoodCorruption = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.5f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0.27f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x040053E5 RID: 21477
		private static TreePaintingSettings WoodJungle = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.16666667f,
			SpecialGroupMaximumHueValue = 0.8333333f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x040053E6 RID: 21478
		private static TreePaintingSettings WoodHallow = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.34f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x040053E7 RID: 21479
		private static TreePaintingSettings WoodSnow = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 0.06944445f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x040053E8 RID: 21480
		private static TreePaintingSettings WoodCrimson = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.33333334f,
			SpecialGroupMaximumHueValue = 0.6666667f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x040053E9 RID: 21481
		private static TreePaintingSettings WoodJungleUnderground = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.16666667f,
			SpecialGroupMaximumHueValue = 0.8333333f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x040053EA RID: 21482
		private static TreePaintingSettings WoodGlowingMushroom = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.5f,
			SpecialGroupMaximumHueValue = 0.8333333f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x040053EB RID: 21483
		private static TreePaintingSettings VanityCherry = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.8333333f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x040053EC RID: 21484
		private static TreePaintingSettings VanityYellowWillow = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 0.025f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x040053ED RID: 21485
		private static TreePaintingSettings TreeAsh = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 0.025f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x040053EE RID: 21486
		private static TreePaintingSettings GemTreeRuby = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x040053EF RID: 21487
		private static TreePaintingSettings GemTreeAmber = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x040053F0 RID: 21488
		private static TreePaintingSettings GemTreeSapphire = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x040053F1 RID: 21489
		private static TreePaintingSettings GemTreeEmerald = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x040053F2 RID: 21490
		private static TreePaintingSettings GemTreeAmethyst = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x040053F3 RID: 21491
		private static TreePaintingSettings GemTreeTopaz = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x040053F4 RID: 21492
		private static TreePaintingSettings GemTreeDiamond = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 0.0027777778f,
			InvertSpecialGroupResult = true
		};

		// Token: 0x040053F5 RID: 21493
		private static TreePaintingSettings PalmTreePurity = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.15277778f,
			SpecialGroupMaximumHueValue = 0.25f,
			SpecialGroupMinimumSaturationValue = 0.88f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x040053F6 RID: 21494
		private static TreePaintingSettings PalmTreeCorruption = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0f,
			SpecialGroupMaximumHueValue = 1f,
			SpecialGroupMinimumSaturationValue = 0.4f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x040053F7 RID: 21495
		private static TreePaintingSettings PalmTreeCrimson = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			HueTestOffset = 0.5f,
			SpecialGroupMinimalHueValue = 0.33333334f,
			SpecialGroupMaximumHueValue = 0.5277778f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		// Token: 0x040053F8 RID: 21496
		private static TreePaintingSettings PalmTreeHallow = new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 0.5f,
			SpecialGroupMaximumHueValue = 0.6111111f,
			SpecialGroupMinimumSaturationValue = 0f,
			SpecialGroupMaximumSaturationValue = 1f
		};
	}
}
