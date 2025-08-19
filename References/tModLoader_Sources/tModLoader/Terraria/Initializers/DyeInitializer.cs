using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Dyes;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.Initializers
{
	// Token: 0x020003EC RID: 1004
	public static class DyeInitializer
	{
		// Token: 0x060034CE RID: 13518 RVA: 0x005646D4 File Offset: 0x005628D4
		private static void LoadBasicColorDye(int baseDyeItem, int blackDyeItem, int brightDyeItem, int silverDyeItem, float r, float g, float b, float saturation = 1f, int oldShader = 1)
		{
			Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
			GameShaders.Armor.BindShader<ArmorShaderData>(baseDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColored")).UseColor(r, g, b).UseSaturation(saturation);
			GameShaders.Armor.BindShader<ArmorShaderData>(blackDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlack")).UseColor(r, g, b).UseSaturation(saturation);
			GameShaders.Armor.BindShader<ArmorShaderData>(brightDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColored")).UseColor(r * 0.5f + 0.5f, g * 0.5f + 0.5f, b * 0.5f + 0.5f).UseSaturation(saturation);
			GameShaders.Armor.BindShader<ArmorShaderData>(silverDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrim")).UseColor(r, g, b).UseSaturation(saturation);
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x005647B0 File Offset: 0x005629B0
		private static void LoadBasicColorDye(int baseDyeItem, float r, float g, float b, float saturation = 1f, int oldShader = 1)
		{
			DyeInitializer.LoadBasicColorDye(baseDyeItem, baseDyeItem + 12, baseDyeItem + 31, baseDyeItem + 44, r, g, b, saturation, oldShader);
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x005647D8 File Offset: 0x005629D8
		private static void LoadBasicColorDyes()
		{
			DyeInitializer.LoadBasicColorDye(1007, 1f, 0f, 0f, 1.2f, 1);
			DyeInitializer.LoadBasicColorDye(1008, 1f, 0.5f, 0f, 1.2f, 2);
			DyeInitializer.LoadBasicColorDye(1009, 1f, 1f, 0f, 1.2f, 3);
			DyeInitializer.LoadBasicColorDye(1010, 0.5f, 1f, 0f, 1.2f, 4);
			DyeInitializer.LoadBasicColorDye(1011, 0f, 1f, 0f, 1.2f, 5);
			DyeInitializer.LoadBasicColorDye(1012, 0f, 1f, 0.5f, 1.2f, 6);
			DyeInitializer.LoadBasicColorDye(1013, 0f, 1f, 1f, 1.2f, 7);
			DyeInitializer.LoadBasicColorDye(1014, 0.2f, 0.5f, 1f, 1.2f, 8);
			DyeInitializer.LoadBasicColorDye(1015, 0f, 0f, 1f, 1.2f, 9);
			DyeInitializer.LoadBasicColorDye(1016, 0.5f, 0f, 1f, 1.2f, 10);
			DyeInitializer.LoadBasicColorDye(1017, 1f, 0f, 1f, 1.2f, 11);
			DyeInitializer.LoadBasicColorDye(1018, 1f, 0.1f, 0.5f, 1.3f, 12);
			DyeInitializer.LoadBasicColorDye(2874, 2875, 2876, 2877, 0.4f, 0.2f, 0f, 1f, 1);
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x0056498C File Offset: 0x00562B8C
		private static void LoadArmorDyes()
		{
			Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
			DyeInitializer.LoadBasicColorDyes();
			GameShaders.Armor.BindShader<ArmorShaderData>(1050, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(0.6f, 0.6f, 0.6f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1037, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(1f, 1f, 1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3558, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(1.5f, 1.5f, 1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2871, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(0.05f, 0.05f, 0.05f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3559, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlack")).UseColor(1f, 1f, 1f).UseSaturation(1.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1031, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(1f, 0f, 0f).UseSecondaryColor(1f, 1f, 0f).UseSaturation(1.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1032, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlackGradient")).UseColor(1f, 0f, 0f).UseSecondaryColor(1f, 1f, 0f).UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3550, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrimGradient")).UseColor(1f, 0f, 0f).UseSecondaryColor(1f, 1f, 0f).UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1063, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessGradient")).UseColor(1f, 0f, 0f).UseSecondaryColor(1f, 1f, 0f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1035, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0f, 0f, 1f).UseSecondaryColor(0f, 1f, 1f).UseSaturation(1.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1036, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlackGradient")).UseColor(0f, 0f, 1f).UseSecondaryColor(0f, 1f, 1f).UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3552, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrimGradient")).UseColor(0f, 0f, 1f).UseSecondaryColor(0f, 1f, 1f).UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1065, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessGradient")).UseColor(0f, 0f, 1f).UseSecondaryColor(0f, 1f, 1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1033, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0f, 1f, 0f).UseSecondaryColor(1f, 1f, 0f).UseSaturation(1.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1034, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlackGradient")).UseColor(0f, 1f, 0f).UseSecondaryColor(1f, 1f, 0f).UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3551, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrimGradient")).UseColor(0f, 1f, 0f).UseSecondaryColor(1f, 1f, 0f).UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1064, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessGradient")).UseColor(0f, 1f, 0f).UseSecondaryColor(1f, 1f, 0f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1068, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0.5f, 1f, 0f).UseSecondaryColor(1f, 0.5f, 0f).UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1069, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0f, 1f, 0.5f).UseSecondaryColor(0f, 0.5f, 1f).UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1070, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(1f, 0f, 0.5f).UseSecondaryColor(0.5f, 0f, 1f).UseSaturation(1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(1066, new ArmorShaderData(pixelShaderRef, "ArmorColoredRainbow"));
			GameShaders.Armor.BindShader<ArmorShaderData>(1067, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessRainbow"));
			GameShaders.Armor.BindShader<ArmorShaderData>(3556, new ArmorShaderData(pixelShaderRef, "ArmorMidnightRainbow"));
			GameShaders.Armor.BindShader<ArmorShaderData>(2869, new ArmorShaderData(pixelShaderRef, "ArmorLivingFlame")).UseColor(1f, 0.9f, 0f).UseSecondaryColor(1f, 0.2f, 0f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2870, new ArmorShaderData(pixelShaderRef, "ArmorLivingRainbow"));
			GameShaders.Armor.BindShader<ArmorShaderData>(2873, new ArmorShaderData(pixelShaderRef, "ArmorLivingOcean"));
			GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3026, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(1f, 1f, 1f);
			GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3027, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(1.5f, 1.2f, 0.5f);
			GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3553, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(1.35f, 0.7f, 0.4f);
			GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3554, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(0.25f, 0f, 0.7f);
			GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3555, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(0.4f, 0.4f, 0.4f);
			GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3190, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflective"));
			GameShaders.Armor.BindShader<TeamArmorShaderData>(1969, new TeamArmorShaderData(pixelShaderRef, "ArmorColored"));
			GameShaders.Armor.BindShader<ArmorShaderData>(2864, new ArmorShaderData(pixelShaderRef, "ArmorMartian")).UseColor(0f, 2f, 3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2872, new ArmorShaderData(pixelShaderRef, "ArmorInvert"));
			GameShaders.Armor.BindShader<ArmorShaderData>(2878, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(0.7f, 1f, 0.9f).UseSecondaryColor(0.35f, 0.85f, 0.8f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2879, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(1f, 1.2f, 0f).UseSecondaryColor(1f, 0.6f, 0.3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2885, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(1.2f, 0.8f, 0f).UseSecondaryColor(0.8f, 0.2f, 0f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2884, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(1f, 0f, 1f).UseSecondaryColor(1f, 0.3f, 0.6f);
			GameShaders.Armor.BindShader<ArmorShaderData>(2883, new ArmorShaderData(pixelShaderRef, "ArmorHighContrastGlow")).UseColor(0f, 1f, 0f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3025, new ArmorShaderData(pixelShaderRef, "ArmorFlow")).UseColor(1f, 0.5f, 1f).UseSecondaryColor(0.6f, 0.1f, 1f);
			GameShaders.Armor.BindShader<TwilightDyeShaderData>(3039, new TwilightDyeShaderData(pixelShaderRef, "ArmorTwilight")).UseImage("Images/Misc/noise").UseColor(0.5f, 0.1f, 1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3040, new ArmorShaderData(pixelShaderRef, "ArmorAcid")).UseColor(0.5f, 1f, 0.3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3041, new ArmorShaderData(pixelShaderRef, "ArmorMushroom")).UseColor(0.05f, 0.2f, 1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3042, new ArmorShaderData(pixelShaderRef, "ArmorPhase")).UseImage("Images/Misc/noise").UseColor(0.4f, 0.2f, 1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3560, new ArmorShaderData(pixelShaderRef, "ArmorAcid")).UseColor(0.9f, 0.2f, 0.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3561, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(0.4f, 0.7f, 1.4f).UseSecondaryColor(0f, 0f, 0.1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3562, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(1.4f, 0.75f, 1f).UseSecondaryColor(0.45f, 0.1f, 0.3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3024, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(-0.5f, -1f, 0f).UseSecondaryColor(1.5f, 1f, 2.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(4663, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(2.6f, 0.6f, 0.6f).UseSecondaryColor(0.2f, -0.2f, -0.2f);
			GameShaders.Armor.BindShader<ArmorShaderData>(4662, new ArmorShaderData(pixelShaderRef, "ArmorFog")).UseImage("Images/Misc/noise").UseColor(0.95f, 0.95f, 0.95f).UseSecondaryColor(0.3f, 0.3f, 0.3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(4778, new ArmorShaderData(pixelShaderRef, "ArmorHallowBoss")).UseImage("Images/Extra_" + 156.ToString());
			GameShaders.Armor.BindShader<ArmorShaderData>(3534, new ArmorShaderData(pixelShaderRef, "ArmorMirage"));
			GameShaders.Armor.BindShader<ArmorShaderData>(3028, new ArmorShaderData(pixelShaderRef, "ArmorAcid")).UseColor(0.5f, 0.7f, 1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3557, new ArmorShaderData(pixelShaderRef, "ArmorPolarized"));
			GameShaders.Armor.BindShader<ArmorShaderData>(3978, new ArmorShaderData(pixelShaderRef, "ColorOnly"));
			GameShaders.Armor.BindShader<ArmorShaderData>(3038, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(0.5f, 0.7f, 1.3f).UseSecondaryColor(0.5f, 0.7f, 1.3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3600, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(0.7f, 0.4f, 1.5f).UseSecondaryColor(0.7f, 0.4f, 1.5f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3597, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(1.5f, 0.6f, 0.4f).UseSecondaryColor(1.5f, 0.6f, 0.4f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3598, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(0.1f, 0.1f, 0.1f).UseSecondaryColor(0.4f, 0.05f, 0.025f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3599, new ArmorShaderData(pixelShaderRef, "ArmorLoki")).UseColor(0.1f, 0.1f, 0.1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3533, new ArmorShaderData(pixelShaderRef, "ArmorShiftingSands")).UseImage("Images/Misc/noise").UseColor(1.1f, 1f, 0.5f).UseSecondaryColor(0.7f, 0.5f, 0.3f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3535, new ArmorShaderData(pixelShaderRef, "ArmorShiftingPearlsands")).UseImage("Images/Misc/noise").UseColor(1.1f, 0.8f, 0.9f).UseSecondaryColor(0.35f, 0.25f, 0.44f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3526, new ArmorShaderData(pixelShaderRef, "ArmorSolar")).UseColor(1f, 0f, 0f).UseSecondaryColor(1f, 1f, 0f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3527, new ArmorShaderData(pixelShaderRef, "ArmorNebula")).UseImage("Images/Misc/noise").UseColor(1f, 0f, 1f).UseSecondaryColor(1f, 1f, 1f).UseSaturation(1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3528, new ArmorShaderData(pixelShaderRef, "ArmorVortex")).UseImage("Images/Misc/noise").UseColor(0.1f, 0.5f, 0.35f).UseSecondaryColor(1f, 1f, 1f).UseSaturation(1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3529, new ArmorShaderData(pixelShaderRef, "ArmorStardust")).UseImage("Images/Misc/noise").UseColor(0.4f, 0.6f, 1f).UseSecondaryColor(1f, 1f, 1f).UseSaturation(1f);
			GameShaders.Armor.BindShader<ArmorShaderData>(3530, new ArmorShaderData(pixelShaderRef, "ArmorVoid"));
			DyeInitializer.FixRecipes();
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x00565928 File Offset: 0x00563B28
		private static void LoadHairDyes()
		{
			Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
			DyeInitializer.LoadLegacyHairdyes();
			GameShaders.Hair.BindShader<TwilightHairDyeShaderData>(3259, new TwilightHairDyeShaderData(pixelShaderRef, "ArmorTwilight")).UseImage("Images/Misc/noise").UseColor(0.5f, 0.1f, 1f);
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x0056597C File Offset: 0x00563B7C
		private static void LoadLegacyHairdyes()
		{
			Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1977, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				newColor.R = (byte)((float)player.statLife / (float)player.statLifeMax2 * 235f + 20f);
				newColor.B = 20;
				newColor.G = 20;
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1978, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				newColor.R = (byte)((1f - (float)player.statMana / (float)player.statManaMax2) * 200f + 50f);
				newColor.B = byte.MaxValue;
				newColor.G = (byte)((1f - (float)player.statMana / (float)player.statManaMax2) * 180f + 75f);
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1979, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				float num27 = (float)(Main.worldSurface * 0.45) * 16f;
				float num28 = (float)(Main.worldSurface + Main.rockLayer) * 8f;
				float num29 = (float)(Main.rockLayer + (double)Main.maxTilesY) * 8f;
				float num30 = (float)(Main.maxTilesY - 150) * 16f;
				Vector2 center = player.Center;
				if (center.Y < num27)
				{
					float num31 = center.Y / num27;
					float num32 = 1f - num31;
					newColor.R = (byte)(116f * num32 + 28f * num31);
					newColor.G = (byte)(160f * num32 + 216f * num31);
					newColor.B = (byte)(249f * num32 + 94f * num31);
				}
				else if (center.Y < num28)
				{
					float num33 = num27;
					float num34 = (center.Y - num33) / (num28 - num33);
					float num35 = 1f - num34;
					newColor.R = (byte)(28f * num35 + 151f * num34);
					newColor.G = (byte)(216f * num35 + 107f * num34);
					newColor.B = (byte)(94f * num35 + 75f * num34);
				}
				else if (center.Y < num29)
				{
					float num36 = num28;
					float num37 = (center.Y - num36) / (num29 - num36);
					float num38 = 1f - num37;
					newColor.R = (byte)(151f * num38 + 128f * num37);
					newColor.G = (byte)(107f * num38 + 128f * num37);
					newColor.B = (byte)(75f * num38 + 128f * num37);
				}
				else if (center.Y < num30)
				{
					float num39 = num29;
					float num40 = (center.Y - num39) / (num30 - num39);
					float num41 = 1f - num40;
					newColor.R = (byte)(128f * num41 + 255f * num40);
					newColor.G = (byte)(128f * num41 + 50f * num40);
					newColor.B = (byte)(128f * num41 + 15f * num40);
				}
				else
				{
					newColor.R = byte.MaxValue;
					newColor.G = 50;
					newColor.B = 10;
				}
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1980, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				long num15 = 0L;
				for (int i = 0; i < 54; i++)
				{
					if (player.inventory[i].type == 71)
					{
						num15 += (long)player.inventory[i].stack;
					}
					if (player.inventory[i].type == 72)
					{
						num15 += (long)player.inventory[i].stack * 100L;
					}
					if (player.inventory[i].type == 73)
					{
						num15 += (long)player.inventory[i].stack * 10000L;
					}
					if (player.inventory[i].type == 74)
					{
						num15 += (long)player.inventory[i].stack * 1000000L;
					}
				}
				if (num15 < 0L || num15 > 999999999L)
				{
					num15 = 999999999L;
				}
				float num16 = (float)Item.buyPrice(0, 5, 0, 0);
				float num17 = (float)Item.buyPrice(0, 50, 0, 0);
				float num18 = (float)Item.buyPrice(2, 0, 0, 0);
				Color color8;
				color8..ctor(226, 118, 76);
				Color color9;
				color9..ctor(174, 194, 196);
				Color color10;
				color10..ctor(204, 181, 72);
				Color color11;
				color11..ctor(161, 172, 173);
				if ((float)num15 < num16)
				{
					float num19 = (float)num15 / num16;
					float num20 = 1f - num19;
					newColor.R = (byte)((float)color8.R * num20 + (float)color9.R * num19);
					newColor.G = (byte)((float)color8.G * num20 + (float)color9.G * num19);
					newColor.B = (byte)((float)color8.B * num20 + (float)color9.B * num19);
				}
				else if ((float)num15 < num17)
				{
					float num21 = num16;
					float num22 = ((float)num15 - num21) / (num17 - num21);
					float num23 = 1f - num22;
					newColor.R = (byte)((float)color9.R * num23 + (float)color10.R * num22);
					newColor.G = (byte)((float)color9.G * num23 + (float)color10.G * num22);
					newColor.B = (byte)((float)color9.B * num23 + (float)color10.B * num22);
				}
				else if ((float)num15 < num18)
				{
					float num24 = num17;
					float num25 = ((float)num15 - num24) / (num18 - num24);
					float num26 = 1f - num25;
					newColor.R = (byte)((float)color10.R * num26 + (float)color11.R * num25);
					newColor.G = (byte)((float)color10.G * num26 + (float)color11.G * num25);
					newColor.B = (byte)((float)color10.B * num26 + (float)color11.B * num25);
				}
				else
				{
					newColor = color11;
				}
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1981, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				Color color4;
				color4..ctor(1, 142, 255);
				Color color5;
				color5..ctor(255, 255, 0);
				Color color6;
				color6..ctor(211, 45, 127);
				Color color7;
				color7..ctor(67, 44, 118);
				if (Main.dayTime)
				{
					if (Main.time < 27000.0)
					{
						float num5 = (float)(Main.time / 27000.0);
						float num6 = 1f - num5;
						newColor.R = (byte)((float)color4.R * num6 + (float)color5.R * num5);
						newColor.G = (byte)((float)color4.G * num6 + (float)color5.G * num5);
						newColor.B = (byte)((float)color4.B * num6 + (float)color5.B * num5);
					}
					else
					{
						float num7 = 27000f;
						float num8 = (float)((Main.time - (double)num7) / (54000.0 - (double)num7));
						float num9 = 1f - num8;
						newColor.R = (byte)((float)color5.R * num9 + (float)color6.R * num8);
						newColor.G = (byte)((float)color5.G * num9 + (float)color6.G * num8);
						newColor.B = (byte)((float)color5.B * num9 + (float)color6.B * num8);
					}
				}
				else if (Main.time < 16200.0)
				{
					float num10 = (float)(Main.time / 16200.0);
					float num11 = 1f - num10;
					newColor.R = (byte)((float)color6.R * num11 + (float)color7.R * num10);
					newColor.G = (byte)((float)color6.G * num11 + (float)color7.G * num10);
					newColor.B = (byte)((float)color6.B * num11 + (float)color7.B * num10);
				}
				else
				{
					float num12 = 16200f;
					float num13 = (float)((Main.time - (double)num12) / (32400.0 - (double)num12));
					float num14 = 1f - num13;
					newColor.R = (byte)((float)color7.R * num14 + (float)color4.R * num13);
					newColor.G = (byte)((float)color7.G * num14 + (float)color4.G * num13);
					newColor.B = (byte)((float)color7.B * num14 + (float)color4.B * num13);
				}
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1982, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				if (player.team >= 0 && player.team < Main.teamColor.Length)
				{
					newColor = Main.teamColor[player.team];
				}
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1983, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				Color color2 = default(Color);
				if (!player.ZoneShimmer)
				{
					color2 = ((Main.waterStyle == 2) ? new Color(124, 118, 242) : ((Main.waterStyle == 3) ? new Color(143, 215, 29) : ((Main.waterStyle == 4) ? new Color(78, 193, 227) : ((Main.waterStyle == 5) ? new Color(189, 231, 255) : ((Main.waterStyle == 6) ? new Color(230, 219, 100) : ((Main.waterStyle == 7) ? new Color(151, 107, 75) : ((Main.waterStyle == 8) ? new Color(128, 128, 128) : ((Main.waterStyle == 9) ? new Color(200, 0, 0) : ((Main.waterStyle == 10) ? new Color(208, 80, 80) : ((Main.waterStyle == 12) ? new Color(230, 219, 100) : ((Main.waterStyle != 13) ? new Color(28, 216, 94) : new Color(28, 216, 94))))))))))));
					if (Main.waterStyle >= 15)
					{
						color2 = LoaderManager.Get<WaterStylesLoader>().Get(Main.waterStyle).BiomeHairColor();
					}
				}
				else
				{
					float R;
					float G;
					float B;
					TorchID.TorchColor(23, out R, out G, out B);
					color2..ctor(R, G, B);
				}
				Color color3 = player.hairDyeColor;
				if (color3.A == 0)
				{
					color3 = color2;
				}
				if (color3.R > color2.R)
				{
					byte b = color3.R;
					color3.R = b - 1;
				}
				if (color3.R < color2.R)
				{
					byte b = color3.R;
					color3.R = b + 1;
				}
				if (color3.G > color2.G)
				{
					byte b = color3.G;
					color3.G = b - 1;
				}
				if (color3.G < color2.G)
				{
					byte b = color3.G;
					color3.G = b + 1;
				}
				if (color3.B > color2.B)
				{
					byte b = color3.B;
					color3.B = b - 1;
				}
				if (color3.B < color2.B)
				{
					byte b = color3.B;
					color3.B = b + 1;
				}
				return color3;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1984, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				newColor..ctor(244, 22, 175);
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1985, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				newColor..ctor(Main.DiscoR, Main.DiscoG, Main.DiscoB);
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(1986, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				float num = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
				float num2 = 10f;
				if (num > num2)
				{
					num = num2;
				}
				float num3 = num / num2;
				float num4 = 1f - num3;
				newColor.R = (byte)(75f * num3 + (float)player.hairColor.R * num4);
				newColor.G = (byte)(255f * num3 + (float)player.hairColor.G * num4);
				newColor.B = (byte)(200f * num3 + (float)player.hairColor.B * num4);
				return newColor;
			}));
			GameShaders.Hair.BindShader<LegacyHairShaderData>(2863, new LegacyHairShaderData().UseLegacyMethod(delegate(Player player, Color newColor, ref bool lighting)
			{
				lighting = false;
				int x = (int)((double)player.position.X + (double)player.width * 0.5) / 16;
				int y = (int)(((double)player.position.Y + (double)player.height * 0.25) / 16.0);
				Color color = Lighting.GetColor(x, y);
				newColor.R = (byte)(color.R + newColor.R >> 1);
				newColor.G = (byte)(color.G + newColor.G >> 1);
				newColor.B = (byte)(color.B + newColor.B >> 1);
				return newColor;
			}));
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x00565C04 File Offset: 0x00563E04
		private static void LoadMisc()
		{
			Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
			GameShaders.Misc["ForceField"] = new MiscShaderData(pixelShaderRef, "ForceField");
			GameShaders.Misc["WaterProcessor"] = new MiscShaderData(pixelShaderRef, "WaterProcessor");
			GameShaders.Misc["WaterDistortionObject"] = new MiscShaderData(pixelShaderRef, "WaterDistortionObject");
			GameShaders.Misc["WaterDebugDraw"] = new MiscShaderData(Main.ScreenShaderRef, "WaterDebugDraw");
			GameShaders.Misc["HallowBoss"] = new MiscShaderData(pixelShaderRef, "HallowBoss");
			GameShaders.Misc["HallowBoss"].UseImage1("Images/Extra_" + 156.ToString());
			GameShaders.Misc["MaskedFade"] = new MiscShaderData(pixelShaderRef, "MaskedFade");
			GameShaders.Misc["MaskedFade"].UseImage1("Images/Extra_" + 216.ToString());
			GameShaders.Misc["QueenSlime"] = new MiscShaderData(pixelShaderRef, "QueenSlime");
			GameShaders.Misc["QueenSlime"].UseImage1("Images/Extra_" + 180.ToString());
			GameShaders.Misc["QueenSlime"].UseImage2("Images/Extra_" + 179.ToString());
			GameShaders.Misc["StardewValleyFade"] = new MiscShaderData(pixelShaderRef, "MaskedFade").UseSamplerState(SamplerState.LinearClamp);
			GameShaders.Misc["StardewValleyFade"].UseImage1("Images/Extra_" + 248.ToString());
			GameShaders.Misc["RainbowTownSlime"] = new MiscShaderData(pixelShaderRef, "RainbowTownSlime");
			int type = 3530;
			bool[] array = new bool[GameShaders.Armor.GetShaderIdFromItemId(type) + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = true;
			}
			foreach (int nonColorfulDyeItem in ItemID.Sets.NonColorfulDyeItems)
			{
				array[GameShaders.Armor.GetShaderIdFromItemId(nonColorfulDyeItem)] = false;
			}
			ItemID.Sets.ColorfulDyeValues = array;
			DyeInitializer.LoadMiscVertexShaders();
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x00565E78 File Offset: 0x00564078
		private static void LoadMiscVertexShaders()
		{
			Ref<Effect> vertexPixelShaderRef = Main.VertexPixelShaderRef;
			GameShaders.Misc["MagicMissile"] = new MiscShaderData(vertexPixelShaderRef, "MagicMissile").UseProjectionMatrix(true);
			GameShaders.Misc["MagicMissile"].UseImage0("Images/Extra_" + 192.ToString());
			GameShaders.Misc["MagicMissile"].UseImage1("Images/Extra_" + 194.ToString());
			GameShaders.Misc["MagicMissile"].UseImage2("Images/Extra_" + 193.ToString());
			GameShaders.Misc["FlameLash"] = new MiscShaderData(vertexPixelShaderRef, "MagicMissile").UseProjectionMatrix(true);
			GameShaders.Misc["FlameLash"].UseImage0("Images/Extra_" + 191.ToString());
			GameShaders.Misc["FlameLash"].UseImage1("Images/Extra_" + 189.ToString());
			GameShaders.Misc["FlameLash"].UseImage2("Images/Extra_" + 190.ToString());
			GameShaders.Misc["RainbowRod"] = new MiscShaderData(vertexPixelShaderRef, "MagicMissile").UseProjectionMatrix(true);
			GameShaders.Misc["RainbowRod"].UseImage0("Images/Extra_" + 195.ToString());
			GameShaders.Misc["RainbowRod"].UseImage1("Images/Extra_" + 197.ToString());
			GameShaders.Misc["RainbowRod"].UseImage2("Images/Extra_" + 196.ToString());
			GameShaders.Misc["FinalFractal"] = new MiscShaderData(vertexPixelShaderRef, "FinalFractalVertex").UseProjectionMatrix(true);
			GameShaders.Misc["FinalFractal"].UseImage0("Images/Extra_" + 195.ToString());
			GameShaders.Misc["FinalFractal"].UseImage1("Images/Extra_" + 197.ToString());
			GameShaders.Misc["EmpressBlade"] = new MiscShaderData(vertexPixelShaderRef, "FinalFractalVertex").UseProjectionMatrix(true);
			GameShaders.Misc["EmpressBlade"].UseImage0("Images/Extra_" + 209.ToString());
			GameShaders.Misc["EmpressBlade"].UseImage1("Images/Extra_" + 210.ToString());
			GameShaders.Misc["LightDisc"] = new MiscShaderData(vertexPixelShaderRef, "MagicMissile").UseProjectionMatrix(true);
			GameShaders.Misc["LightDisc"].UseImage0("Images/Extra_" + 195.ToString());
			GameShaders.Misc["LightDisc"].UseImage1("Images/Extra_" + 195.ToString());
			GameShaders.Misc["LightDisc"].UseImage2("Images/Extra_" + 252.ToString());
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x0056620B File Offset: 0x0056440B
		public static void Load()
		{
			DyeInitializer.LoadArmorDyes();
			DyeInitializer.LoadHairDyes();
			DyeInitializer.LoadMisc();
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x0056621C File Offset: 0x0056441C
		private static void FixRecipes()
		{
			for (int i = 0; i < Recipe.maxRecipes; i++)
			{
				Main.recipe[i].createItem.dye = GameShaders.Armor.GetShaderIdFromItemId(Main.recipe[i].createItem.type);
				Main.recipe[i].createItem.hairDye = GameShaders.Hair.GetShaderIdFromItemId(Main.recipe[i].createItem.type);
			}
		}
	}
}
