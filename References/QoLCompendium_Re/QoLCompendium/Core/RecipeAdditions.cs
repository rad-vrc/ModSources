using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using QoLCompendium.Content.Items.Accessories.InformationAccessories;
using QoLCompendium.Content.Items.Placeables.CraftingStations;
using QoLCompendium.Content.Items.Placeables.CraftingStations.CrossMod;
using QoLCompendium.Content.Items.Placeables.Pylons;
using QoLCompendium.Content.Items.Tools.Mirrors;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.All;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Catalyst;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.ThoriumBossRework;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Calamity;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.CalamityEntropy;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Clamity;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.MartinsOrder;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.SOTS;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.SpiritClassic;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Thorium;
using QoLCompendium.Content.Items.Tools.Usables;
using QoLCompendium.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumRework;

namespace QoLCompendium.Core
{
	// Token: 0x0200020C RID: 524
	public class RecipeAdditions : ModSystem
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0000A88D File Offset: 0x00008A8D
		[JITWhenModsEnabled(new string[]
		{
			"ThoriumRework"
		})]
		public static bool ThoriumReworkPotionsEnabled
		{
			get
			{
				return ModContent.GetInstance<CompatConfig>().extraPotions;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0005B180 File Offset: 0x00059380
		public override void PostAddRecipes()
		{
			for (int i = 0; i < Main.recipe.Length; i++)
			{
				if (QoLCompendium.itemConfig.EndlessAmmo)
				{
					if (Main.recipe[i].HasIngredient(97) && Main.recipe[i].HasResult(3104) && Main.recipe[i].HasTile(125))
					{
						Main.recipe[i].RemoveTile(125);
						Main.recipe[i].AddTile(220);
					}
					if (Main.recipe[i].HasIngredient(40) && Main.recipe[i].HasResult(3103) && Main.recipe[i].HasTile(125))
					{
						Main.recipe[i].RemoveTile(125);
						Main.recipe[i].AddTile(220);
					}
				}
				if (Main.recipe[i].HasIngredient(ModContent.ItemType<GoldenLockpick>()) && QoLCompendium.mainConfig.NonConsumableKeys)
				{
					Main.recipe[i].AddConsumeIngredientCallback(delegate(Recipe recipe, int type, ref int amount, bool isDecrafting)
					{
						if (type == ModContent.ItemType<GoldenLockpick>())
						{
							amount = 0;
						}
					});
				}
				if (Main.recipe[i].HasIngredient(329) && QoLCompendium.mainConfig.NonConsumableKeys)
				{
					Main.recipe[i].AddConsumeIngredientCallback(delegate(Recipe recipe, int type, ref int amount, bool isDecrafting)
					{
						if (type == 329)
						{
							amount = 0;
						}
					});
				}
				if (ModConditions.calamityLoaded && ModConditions.catalystLoaded)
				{
					if (Main.recipe[i].HasResult(ModContent.ItemType<PermanentCalamity>()))
					{
						Main.recipe[i].AddIngredient(ModContent.ItemType<PermanentAstracola>(), 1);
					}
					if (Main.recipe[i].HasResult(ModContent.ItemType<CalamityCraftingMonolith>()))
					{
						Main.recipe[i].AddIngredient(Common.GetModItem(ModConditions.catalystMod, "AstralTransmogrifier"), 1);
					}
				}
				if (ModConditions.calamityLoaded && ModConditions.clamityAddonLoaded && Main.recipe[i].HasResult(ModContent.ItemType<PermanentCalamity>()))
				{
					Main.recipe[i].AddIngredient(ModContent.ItemType<PermanentClamity>(), 1);
				}
				if (ModConditions.calamityLoaded && ModConditions.calamityEntropyLoaded)
				{
					if (Main.recipe[i].HasResult(ModContent.ItemType<PermanentCalamity>()))
					{
						Main.recipe[i].AddIngredient(ModContent.ItemType<PermanentCalamityEntropy>(), 1);
					}
					if (Main.recipe[i].HasResult(ModContent.ItemType<CalamityCraftingMonolith>()))
					{
						Main.recipe[i].AddIngredient(Common.GetModItem(ModConditions.calamityEntropyMod, "AbyssalAltar"), 1);
					}
				}
				if (ModConditions.thoriumLoaded && ModConditions.thoriumBossReworkLoaded && RecipeAdditions.ThoriumReworkPotionsEnabled && Main.recipe[i].HasResult(ModContent.ItemType<PermanentThoriumBard>()))
				{
					Main.recipe[i].AddIngredient(ModContent.ItemType<PermanentDeathsinger>(), 1);
					Main.recipe[i].AddIngredient(ModContent.ItemType<PermanentInspirationRegeneration>(), 1);
				}
				if (QoLCompendium.itemConfig.Mirrors && QoLCompendium.itemConfig.InformationAccessories && Main.recipe[i].HasResult(ModContent.ItemType<MosaicMirror>()))
				{
					Main.recipe[i].AddIngredient(ModContent.ItemType<IAH>(), 1);
				}
				if (ModConditions.calamityLoaded && Main.recipe[i].HasResult(ModContent.ItemType<PermanentEverything>()))
				{
					Main.recipe[i].AddIngredient(ModContent.ItemType<PermanentCalamity>(), 1);
				}
				if (ModConditions.martainsOrderLoaded && Main.recipe[i].HasResult(ModContent.ItemType<PermanentEverything>()))
				{
					Main.recipe[i].AddIngredient(ModContent.ItemType<PermanentMartinsOrder>(), 1);
				}
				if (ModConditions.secretsOfTheShadowsLoaded && Main.recipe[i].HasResult(ModContent.ItemType<PermanentEverything>()))
				{
					Main.recipe[i].AddIngredient(ModContent.ItemType<PermanentSecretsOfTheShadows>(), 1);
				}
				if (ModConditions.spiritLoaded && Main.recipe[i].HasResult(ModContent.ItemType<PermanentEverything>()))
				{
					Main.recipe[i].AddIngredient(ModContent.ItemType<PermanentSpiritClassic>(), 1);
				}
				if (ModConditions.thoriumLoaded && Main.recipe[i].HasResult(ModContent.ItemType<PermanentEverything>()))
				{
					Main.recipe[i].AddIngredient(ModContent.ItemType<PermanentThorium>(), 1);
				}
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0005B55C File Offset: 0x0005975C
		public override void AddRecipes()
		{
			if (QoLCompendium.mainConfig.FullyDisableRecipes)
			{
				return;
			}
			for (int i = 0; i < ItemLoader.ItemCount; i++)
			{
				if (ItemID.Sets.ShimmerTransformToItem[i] > 0)
				{
					Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CraftingStations, ItemID.Sets.ShimmerTransformToItem[i], 1, "Mods.QoLCompendium.ItemToggledConditions.CraftingStations");
					itemRecipe.AddIngredient(i, 1);
					itemRecipe.AddTile(ModContent.TileType<AetherAltarTile>());
					itemRecipe.Register();
				}
			}
			if (ModConditions.confectionRebakedLoaded && ModConditions.mechReworkLoaded)
			{
				Recipe recipe = Recipe.Create(Common.GetModItem(ModConditions.confectionRebakedMod, "DeathsRaze"), 1);
				recipe.AddIngredient(795, 1);
				recipe.AddIngredient(155, 1);
				recipe.AddIngredient(190, 1);
				recipe.AddIngredient(121, 1);
				recipe.AddTile(26);
				recipe.Register();
				if (ModConditions.depthsLoaded)
				{
					Recipe recipe2 = Recipe.Create(Common.GetModItem(ModConditions.confectionRebakedMod, "DeathsRaze"), 1);
					recipe2.AddIngredient(795, 1);
					recipe2.AddIngredient(155, 1);
					recipe2.AddIngredient(190, 1);
					recipe2.AddIngredient(Common.GetModItem(ModConditions.depthsMod, "Terminex"), 1);
					recipe2.AddTile(26);
					recipe2.Register();
				}
			}
			if (ModConditions.calamityLoaded)
			{
				Recipe recipe3 = Recipe.Create(Common.GetModItem(ModConditions.calamityMod, "CrimsonEffigy"), 1);
				recipe3.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "BloodSample"), 10);
				recipe3.AddIngredient(1330, 10);
				recipe3.AddIngredient(1257, 6);
				recipe3.AddTile(26);
				recipe3.Register();
				Recipe recipe4 = Recipe.Create(Common.GetModItem(ModConditions.calamityMod, "CorruptionEffigy"), 1);
				recipe4.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "RottenMatter"), 10);
				recipe4.AddIngredient(68, 10);
				recipe4.AddIngredient(57, 6);
				recipe4.AddTile(26);
				recipe4.Register();
			}
			RecipeAdditions.ConversionRecipes();
			RecipeAdditions.CrateRecipes();
			RecipeAdditions.BagRecipes();
			RecipeAdditions.BannerRecipes();
			RecipeAdditions.GrabBagRecipes();
			if (!QoLCompendium.itemConfig.DisableModdedItems)
			{
				int ingredientID = 87;
				int resultID = 3213;
				int tileID = 16;
				int ingredientAmount = 1;
				int resultAmount = 1;
				bool disableDecraft = false;
				bool usesRecipeGroup = false;
				Condition[] array = new Condition[1];
				array[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.MobileStorages", () => QoLCompendium.itemConfig.MobileStorages);
				Common.CreateSimpleRecipe(ingredientID, resultID, tileID, ingredientAmount, resultAmount, disableDecraft, usesRecipeGroup, array);
			}
			int ingredientID2 = 2308;
			int resultID2 = 4022;
			int tileID2 = 96;
			int ingredientAmount2 = 1;
			int resultAmount2 = 1;
			bool disableDecraft2 = false;
			bool usesRecipeGroup2 = false;
			Condition[] array2 = new Condition[1];
			array2[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.RecipeEnabled", () => QoLCompendium.mainConfig.GoldenCarpDelight);
			Common.CreateSimpleRecipe(ingredientID2, resultID2, tileID2, ingredientAmount2, resultAmount2, disableDecraft2, usesRecipeGroup2, array2);
			Recipe universalPylon = ModConditions.GetItemRecipe(() => QoLCompendium.mainConfig.EasierUniversalPylon, 4951, 1, "Mods.QoLCompendium.ItemToggledConditions.Pylons");
			universalPylon.AddIngredient(4917, 1);
			universalPylon.AddIngredient(4919, 1);
			universalPylon.AddIngredient(4876, 1);
			universalPylon.AddIngredient(4916, 1);
			universalPylon.AddIngredient(4875, 1);
			universalPylon.AddIngredient(4921, 1);
			universalPylon.AddIngredient(4918, 1);
			universalPylon.AddIngredient(4920, 1);
			if (!QoLCompendium.itemConfig.DisableModdedItems)
			{
				universalPylon.AddIngredient(ModContent.ItemType<AetherPylon>(), 1);
				universalPylon.AddIngredient(ModContent.ItemType<CorruptionPylon>(), 1);
				universalPylon.AddIngredient(ModContent.ItemType<CrimsonPylon>(), 1);
				universalPylon.AddIngredient(ModContent.ItemType<DungeonPylon>(), 1);
				universalPylon.AddIngredient(ModContent.ItemType<SkyPylon>(), 1);
				universalPylon.AddIngredient(ModContent.ItemType<TemplePylon>(), 1);
				universalPylon.AddIngredient(ModContent.ItemType<HellPylon>(), 1);
			}
			universalPylon.AddTile(16);
			universalPylon.Register();
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0005B920 File Offset: 0x00059B20
		public static void ConversionRecipes()
		{
			Common.ConversionRecipe(12, 699, 16);
			Common.ConversionRecipe(11, 700, 16);
			Common.ConversionRecipe(14, 701, 16);
			Common.ConversionRecipe(13, 702, 16);
			Common.ConversionRecipe(56, 880, 16);
			Common.ConversionRecipe(364, 1104, 16);
			Common.ConversionRecipe(365, 1105, 134);
			Common.ConversionRecipe(366, 1106, 134);
			Common.ConversionRecipe(20, 703, 16);
			Common.ConversionRecipe(22, 704, 16);
			Common.ConversionRecipe(21, 705, 16);
			Common.ConversionRecipe(19, 706, 16);
			Common.ConversionRecipe(57, 1257, 16);
			Common.ConversionRecipe(381, 1184, 16);
			Common.ConversionRecipe(382, 1191, 134);
			Common.ConversionRecipe(391, 1198, 134);
			Common.ConversionRecipe(68, 1330, 16);
			Common.ConversionRecipe(86, 1329, 16);
			Common.ConversionRecipe(522, 1332, 16);
			Common.ConversionRecipe(521, 520, 16);
			Common.ConversionRecipe(60, 2887, 16);
			Common.ConversionRecipe(2318, 2319, 16);
			Common.ConversionRecipe(2318, 2305, 16);
			Common.ConversionRecipe(527, 528, 16);
			int ingredientID = 547;
			int resultID = 548;
			int tileID = 134;
			int ingredientAmount = 1;
			int resultAmount = 1;
			bool disableDecraft = false;
			bool usesRecipeGroup = false;
			Condition[] array = new Condition[2];
			array[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.ItemConversions", () => QoLCompendium.mainConfig.ItemConversions);
			array[1] = Condition.DownedMechBossAll;
			Common.CreateSimpleRecipe(ingredientID, resultID, tileID, ingredientAmount, resultAmount, disableDecraft, usesRecipeGroup, array);
			int ingredientID2 = 548;
			int resultID2 = 549;
			int tileID2 = 134;
			int ingredientAmount2 = 1;
			int resultAmount2 = 1;
			bool disableDecraft2 = false;
			bool usesRecipeGroup2 = false;
			Condition[] array2 = new Condition[2];
			array2[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.ItemConversions", () => QoLCompendium.mainConfig.ItemConversions);
			array2[1] = Condition.DownedMechBossAll;
			Common.CreateSimpleRecipe(ingredientID2, resultID2, tileID2, ingredientAmount2, resultAmount2, disableDecraft2, usesRecipeGroup2, array2);
			int ingredientID3 = 549;
			int resultID3 = 547;
			int tileID3 = 134;
			int ingredientAmount3 = 1;
			int resultAmount3 = 1;
			bool disableDecraft3 = false;
			bool usesRecipeGroup3 = false;
			Condition[] array3 = new Condition[2];
			array3[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.ItemConversions", () => QoLCompendium.mainConfig.ItemConversions);
			array3[1] = Condition.DownedMechBossAll;
			Common.CreateSimpleRecipe(ingredientID3, resultID3, tileID3, ingredientAmount3, resultAmount3, disableDecraft3, usesRecipeGroup3, array3);
			Common.ConversionRecipe(619, 911, 16);
			Common.ConversionRecipe(61, 836, 16);
			Common.ConversionRecipe(370, 1246, 16);
			Common.ConversionRecipe(3276, 3277, 16);
			Common.ConversionRecipe(3274, 3275, 16);
			Common.ConversionRecipe(833, 835, 16);
			Common.ConversionRecipe(59, 2171, 16);
			Common.ConversionRecipe(3203, 3204, 16);
			Common.ConversionRecipe(3982, 3983, 16);
			Common.ConversionRecipe(4144, 3368, 125);
			Common.ConversionRecipe(3008, 3007, 16);
			Common.ConversionRecipe(3014, 3006, 16);
			Common.ConversionRecipe(3012, 3013, 16);
			Common.ConversionRecipe(3015, 3016, 16);
			Common.ConversionRecipe(3023, 3020, 16);
			Common.ConversionRecipe(994, 3060, 16);
			Common.ConversionRecipe(1534, 1535, 134);
			if (ModConditions.calamityLoaded)
			{
				Common.ConversionRecipe(Common.GetModItem(ModConditions.calamityMod, "RottenMatter"), Common.GetModItem(ModConditions.calamityMod, "BloodSample"), 26);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.calamityMod, "FilthyGlove"), Common.GetModItem(ModConditions.calamityMod, "BloodstainedGlove"), 26);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.calamityMod, "RottenBrain"), Common.GetModItem(ModConditions.calamityMod, "BloodyWormTooth"), 26);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.calamityMod, "RottingEyeball"), Common.GetModItem(ModConditions.calamityMod, "BloodyVein"), 26);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.calamityMod, "CorruptionEffigy"), Common.GetModItem(ModConditions.calamityMod, "CrimsonEffigy"), 26);
			}
			bool confectionRebakedLoaded = ModConditions.confectionRebakedLoaded;
			if (ModConditions.depthsLoaded)
			{
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ShadowShrub"), 318, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ShadowShrubSeeds"), 312, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ShadowShrubPlanterBox"), 3222, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "Quartz"), 173, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ArqueriteOre"), 174, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "QuartzCrate"), 4877, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ArqueriteCrate"), 4878, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ShadowFightingFish"), 2312, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "QuartzFeeder"), 2315, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "LodeStone"), 5010, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "StoneRose"), 1323, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "AmalgamAmulet"), 906, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "CrystalSkull"), 193, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "QuicksilverproofFishingHook"), 4881, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "QuicksilverproofTackleBag"), 5064, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "PalladiumShield"), 156, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "CrystalCrown"), Common.GetModItem(ModConditions.depthsMod, "CharredCrown"), 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "WhiteLightning"), 218, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "Skyfall"), 3282, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "BlueSphere"), 3290, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "SilverStar"), 220, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "NightFury"), 3019, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ShadowSphere"), 272, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "Steelocanth"), 2331, 16);
				Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ChasmeBag"), 3324, 16);
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0005C040 File Offset: 0x0005A240
		public static void BagRecipes()
		{
			Common.CreateBagRecipe(Common.kingSlimeDrops, 3318);
			Common.CreateBagRecipe(Common.eyeOfCthulhuDrops, 3319);
			Common.CreateBagRecipe(Common.eaterOfWorldsDrops, 3320);
			Common.CreateBagRecipe(Common.brainOfCthulhuDrops, 3321);
			Common.CreateBagRecipe(Common.queenBeeDrops, 3322);
			Common.CreateBagRecipe(Common.deerclopsDrops, 5111);
			Common.CreateBagRecipe(Common.skeletronDrops, 3323);
			Common.CreateBagRecipe(Common.wallOfFleshDrops, 3324);
			Common.CreateBagRecipe(Common.queenSlimeDrops, 4957);
			Common.CreateBagRecipe(Common.planteraDrops, 3328);
			Common.CreateBagRecipe(Common.golemDrops, 3329);
			Common.CreateBagRecipe(Common.betsyDrops, 3860);
			Common.CreateBagRecipe(Common.dukeFishronDrops, 3330);
			Common.CreateBagRecipe(Common.empressOfLightDrops, 4782);
			Common.CreateBagRecipe(Common.moonLordDrops, 3332);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0005C130 File Offset: 0x0005A330
		public static void CrateRecipes()
		{
			Common.CreateCrateRecipe(3200, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(3201, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(285, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4341, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(953, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(3068, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(3084, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateHardmodeRecipe(2424, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(280, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(281, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(284, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(946, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(3069, 2334, 3979, 1, new Condition[]
			{
				Condition.NotRemixWorld,
				Condition.NotZenithWorld
			});
			Common.CreateCrateRecipe(832, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(933, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4281, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4429, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4427, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2608, 2335, 3980, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2587, 2335, 3980, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2501, 2335, 3980, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2491, 2336, 3981, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(989, 2336, 3981, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(49, 2336, 3981, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(50, 2336, 3981, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(53, 2336, 3981, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(54, 2336, 3981, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(5011, 2336, 3981, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(975, 2336, 3981, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(930, 2336, 3981, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(3017, 3208, 3987, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(212, 3208, 3987, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(964, 3208, 3987, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(211, 3208, 3987, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(213, 3208, 3987, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2292, 3208, 3987, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(753, 3208, 3987, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(3360, 3208, 3987, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(3361, 3208, 3987, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4426, 3208, 3987, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(65, 3206, 3985, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(158, 3206, 3985, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(159, 3206, 3985, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2219, 3206, 3985, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4978, 3206, 3985, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(670, 4405, 4406, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(724, 4405, 4406, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(950, 4405, 4406, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(1319, 4405, 4406, 1, new Condition[]
			{
				Condition.NotRemixWorld,
				Condition.NotZenithWorld
			});
			Common.CreateCrateRecipe(987, 4405, 4406, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(1579, 4405, 4406, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4056, 4407, 4408, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4442, 4407, 4408, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4055, 4407, 4408, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4061, 4407, 4408, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4062, 4407, 4408, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4262, 4407, 4408, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4263, 4407, 4408, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(857, 4407, 4408, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(848, 4407, 4408, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(866, 4407, 4408, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(162, 3203, 3982, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(111, 3203, 3982, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(96, 3203, 3982, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(115, 3203, 3982, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(64, 3203, 3982, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2330, 3203, 3982, 1, Array.Empty<Condition>());
			Common.CreateCrateHardmodeRecipe(3210, 3982, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(800, 3204, 3983, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(802, 3204, 3983, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(1256, 3204, 3983, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(1290, 3204, 3983, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(3062, 3204, 3983, 1, Array.Empty<Condition>());
			Common.CreateCrateHardmodeRecipe(3211, 3983, 1, Array.Empty<Condition>());
			Common.CreateCrateHardmodeRecipe(3209, 3986, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(186, 5002, 5003, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4404, 5002, 5003, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(277, 5002, 5003, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(187, 5002, 5003, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(863, 5002, 5003, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4425, 5002, 5003, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4460, 5002, 5003, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2341, 5002, 5003, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2342, 5002, 5003, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2332, 5002, 5003, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(906, 4877, 4878, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4822, 4877, 4878, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4828, 4877, 4878, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4880, 4877, 4878, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4881, 4877, 4878, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4737, 4877, 4878, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4551, 4877, 4878, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4443, 4877, 4878, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4819, 4877, 4878, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4820, 4877, 4878, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4872, 4877, 4878, 1, Array.Empty<Condition>());
			Common.CreateCrateHardmodeRecipe(2331, 4878, 1, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(274, 4877, 4878, 1, 329, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(220, 4877, 4878, 1, 329, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(112, 4877, 4878, 1, 329, new Condition[]
			{
				Condition.NotRemixWorld,
				Condition.NotZenithWorld
			});
			Common.CreateCrateWithKeyRecipe(218, 4877, 4878, 1, 329, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(3019, 4877, 4878, 1, 329, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(5010, 4877, 4878, 1, 329, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(3317, 3205, 3984, 1, 327, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(155, 3205, 3984, 1, 327, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(156, 3205, 3984, 1, 327, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(157, 3205, 3984, 1, 327, new Condition[]
			{
				Condition.NotRemixWorld,
				Condition.NotZenithWorld
			});
			Common.CreateCrateWithKeyRecipe(163, 3205, 3984, 1, 327, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(113, 3205, 3984, 1, 327, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(164, 3205, 3984, 1, 327, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(329, 3205, 3984, 1, 327, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(3317, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>(), Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(155, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>(), Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(156, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>(), Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(157, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>(), new Condition[]
			{
				Condition.NotRemixWorld,
				Condition.NotZenithWorld
			});
			Common.CreateCrateWithKeyRecipe(163, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>(), Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(113, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>(), Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(164, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>(), Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(329, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>(), Array.Empty<Condition>());
			Common.CreateCrateRecipe(997, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2196, 2334, 3979, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2204, 3208, 3987, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2197, 3206, 3985, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(2198, 4405, 4406, 1, Array.Empty<Condition>());
			Common.CreateCrateRecipe(4276, 4407, 4408, 1, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(3000, 3205, 3984, 1, 327, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(2192, 3205, 3984, 1, 327, Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(3000, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>(), Array.Empty<Condition>());
			Common.CreateCrateWithKeyRecipe(2192, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>(), Array.Empty<Condition>());
			Common.CreateCrateHardmodeRecipe(3064, 3981, 1, Array.Empty<Condition>());
			if (ModConditions.calamityLoaded)
			{
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "AstrophageItem"), Common.GetModItem(ModConditions.calamityMod, "MonolithCrate"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "AstralScythe"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, new Condition[]
				{
					ModConditions.DownedAstrumAureus
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "TitanArm"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, new Condition[]
				{
					ModConditions.DownedAstrumAureus
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "StellarCannon"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, new Condition[]
				{
					ModConditions.DownedAstrumAureus
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "AstralachneaStaff"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, new Condition[]
				{
					ModConditions.DownedAstrumAureus
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "HivePod"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, new Condition[]
				{
					ModConditions.DownedAstrumAureus
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "StellarKnife"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, new Condition[]
				{
					ModConditions.DownedAstrumAureus
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "StarbusterCore"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, new Condition[]
				{
					ModConditions.DownedAstrumAureus
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "AshenStalactite"), Common.GetModItem(ModConditions.calamityMod, "SlagCrate"), Common.GetModItem(ModConditions.calamityMod, "BrimstoneCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "BladecrestOathsword"), Common.GetModItem(ModConditions.calamityMod, "SlagCrate"), Common.GetModItem(ModConditions.calamityMod, "BrimstoneCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "LiliesOfFinality"), Common.GetModItem(ModConditions.calamityMod, "BrimstoneCrate"), 1, new Condition[]
				{
					ModConditions.DownedYharon
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "AbyssalAmulet"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "AlluringBait"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "BrokenWaterFilter"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "EffigyOfDecay"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "RustyBeaconPrototype"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "RustyMedallion"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(2341, Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(2342, Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(2332, Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "BallOFugu"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedSlimeGod
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "Archerfish"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedSlimeGod
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "BlackAnurian"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedSlimeGod
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "HerringStaff"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedSlimeGod
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "Lionfish"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedSlimeGod
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "AnechoicPlating"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedSlimeGod
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "DepthCharm"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedSlimeGod
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "IronBoots"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedSlimeGod
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "StrangeOrb"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedSlimeGod
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "TorrentialTear"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedSlimeGod
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "SulphurousGrabber"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedAcidRain2
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "FlakToxicannon"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedAcidRain2
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "BelchingSaxophone"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedAcidRain2
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "SlitheringEels"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedAcidRain2
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "SkyfinBombers"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedAcidRain2
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "SpentFuelContainer"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedAcidRain2
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "NuclearFuelRod"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, new Condition[]
				{
					ModConditions.DownedAcidRain2
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "RustedJingleBell"), Common.GetModItem(ModConditions.calamityMod, "EutrophicCrate"), Common.GetModItem(ModConditions.calamityMod, "PrismCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "SparklingEmpress"), Common.GetModItem(ModConditions.calamityMod, "EutrophicCrate"), Common.GetModItem(ModConditions.calamityMod, "PrismCrate"), 1, new Condition[]
				{
					ModConditions.DownedDesertScourge
				});
				Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "SerpentsBite"), Common.GetModItem(ModConditions.calamityMod, "PrismCrate"), 1, Array.Empty<Condition>());
			}
			if (ModConditions.spiritLoaded)
			{
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.spiritMod, "ReachBrooch"), Common.GetModItem(ModConditions.spiritMod, "ReachCrate"), Common.GetModItem(ModConditions.spiritMod, "BriarCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.spiritMod, "ReachBoomerang"), Common.GetModItem(ModConditions.spiritMod, "ReachCrate"), Common.GetModItem(ModConditions.spiritMod, "BriarCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.spiritMod, "ThornHook"), Common.GetModItem(ModConditions.spiritMod, "ReachCrate"), Common.GetModItem(ModConditions.spiritMod, "BriarCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.spiritMod, "ReachChestMagic"), Common.GetModItem(ModConditions.spiritMod, "ReachCrate"), Common.GetModItem(ModConditions.spiritMod, "BriarCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.spiritMod, "LivingElderbarkWand"), Common.GetModItem(ModConditions.spiritMod, "ReachCrate"), Common.GetModItem(ModConditions.spiritMod, "BriarCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.spiritMod, "ThornyRod"), Common.GetModItem(ModConditions.spiritMod, "ReachCrate"), Common.GetModItem(ModConditions.spiritMod, "BriarCrate"), 1, Array.Empty<Condition>());
			}
			if (ModConditions.thoriumLoaded)
			{
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "MagicConch"), Common.GetModItem(ModConditions.thoriumMod, "AquaticDepthsCrate"), Common.GetModItem(ModConditions.thoriumMod, "AbyssalCrate"), 1, new Condition[]
				{
					Condition.DownedEowOrBoc
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "AnglerBowl"), Common.GetModItem(ModConditions.thoriumMod, "AquaticDepthsCrate"), Common.GetModItem(ModConditions.thoriumMod, "AbyssalCrate"), 1, new Condition[]
				{
					Condition.DownedEowOrBoc
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "RainStone"), Common.GetModItem(ModConditions.thoriumMod, "AquaticDepthsCrate"), Common.GetModItem(ModConditions.thoriumMod, "AbyssalCrate"), 1, new Condition[]
				{
					Condition.DownedEowOrBoc
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "SteelDrum"), Common.GetModItem(ModConditions.thoriumMod, "AquaticDepthsCrate"), Common.GetModItem(ModConditions.thoriumMod, "AbyssalCrate"), 1, new Condition[]
				{
					Condition.DownedEowOrBoc
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "SeaTurtlesBulwark"), Common.GetModItem(ModConditions.thoriumMod, "AquaticDepthsCrate"), Common.GetModItem(ModConditions.thoriumMod, "AbyssalCrate"), 1, new Condition[]
				{
					Condition.DownedEowOrBoc
				});
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "MixTape"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "LootRang"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "MagmaCharm"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "SpringSteps"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "DeepStaff"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "MagmaLocket"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "SpringHook"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(906, Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "HightechSonarDevice"), Common.GetModItem(ModConditions.thoriumMod, "StrangeCrate"), Common.GetModItem(ModConditions.thoriumMod, "WondrousCrate"), 1, Array.Empty<Condition>());
				Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "DrownedDoubloon"), Common.GetModItem(ModConditions.thoriumMod, "StrangeCrate"), Common.GetModItem(ModConditions.thoriumMod, "WondrousCrate"), 1, Array.Empty<Condition>());
			}
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0005DC7C File Offset: 0x0005BE7C
		public static void BannerRecipes()
		{
			int bannerItemID = 1665;
			int resultID = 1303;
			int bannerAmount = 1;
			int resultAmount = 1;
			Condition[] array = new Condition[1];
			array[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID, resultID, bannerAmount, resultAmount, array);
			int bannerItemID2 = 3448;
			int resultID2 = 1303;
			int bannerAmount2 = 1;
			int resultAmount2 = 1;
			Condition[] array2 = new Condition[2];
			array2[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array2[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID2, resultID2, bannerAmount2, resultAmount2, array2);
			int bannerItemID3 = 2973;
			int resultID3 = 1321;
			int bannerAmount3 = 1;
			int resultAmount3 = 1;
			Condition[] array3 = new Condition[2];
			array3[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array3[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID3, resultID3, bannerAmount3, resultAmount3, array3);
			int bannerItemID4 = 1630;
			int resultID4 = 536;
			int bannerAmount4 = 1;
			int resultAmount4 = 1;
			Condition[] array4 = new Condition[2];
			array4[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array4[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID4, resultID4, bannerAmount4, resultAmount4, array4);
			int bannerItemID5 = 1630;
			int resultID5 = 535;
			int bannerAmount5 = 1;
			int resultAmount5 = 1;
			Condition[] array5 = new Condition[2];
			array5[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array5[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID5, resultID5, bannerAmount5, resultAmount5, array5);
			int bannerItemID6 = 1630;
			int resultID6 = 554;
			int bannerAmount6 = 1;
			int resultAmount6 = 1;
			Condition[] array6 = new Condition[2];
			array6[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array6[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID6, resultID6, bannerAmount6, resultAmount6, array6);
			int bannerItemID7 = 1630;
			int resultID7 = 532;
			int bannerAmount7 = 1;
			int resultAmount7 = 1;
			Condition[] array7 = new Condition[2];
			array7[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array7[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID7, resultID7, bannerAmount7, resultAmount7, array7);
			int bannerItemID8 = 1689;
			int resultID8 = 887;
			int bannerAmount8 = 1;
			int resultAmount8 = 1;
			Condition[] array8 = new Condition[1];
			array8[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID8, resultID8, bannerAmount8, resultAmount8, array8);
			int bannerItemID9 = 1615;
			int resultID9 = 885;
			int bannerAmount9 = 1;
			int resultAmount9 = 1;
			Condition[] array9 = new Condition[2];
			array9[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array9[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID9, resultID9, bannerAmount9, resultAmount9, array9);
			int bannerItemID10 = 1620;
			int resultID10 = 886;
			int bannerAmount10 = 1;
			int resultAmount10 = 1;
			Condition[] array10 = new Condition[2];
			array10[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array10[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID10, resultID10, bannerAmount10, resultAmount10, array10);
			int bannerItemID11 = 2923;
			int resultID11 = 893;
			int bannerAmount11 = 1;
			int resultAmount11 = 1;
			Condition[] array11 = new Condition[2];
			array11[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array11[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID11, resultID11, bannerAmount11, resultAmount11, array11);
			int bannerItemID12 = 3448;
			int resultID12 = 890;
			int bannerAmount12 = 1;
			int resultAmount12 = 1;
			Condition[] array12 = new Condition[2];
			array12[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array12[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID12, resultID12, bannerAmount12, resultAmount12, array12);
			int bannerItemID13 = 3405;
			int resultID13 = 3781;
			int bannerAmount13 = 1;
			int resultAmount13 = 1;
			Condition[] array13 = new Condition[2];
			array13[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array13[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID13, resultID13, bannerAmount13, resultAmount13, array13);
			int bannerItemID14 = 3393;
			int resultID14 = 393;
			int bannerAmount14 = 1;
			int resultAmount14 = 1;
			Condition[] array14 = new Condition[1];
			array14[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID14, resultID14, bannerAmount14, resultAmount14, array14);
			int bannerItemID15 = 3392;
			int resultID15 = 393;
			int bannerAmount15 = 1;
			int resultAmount15 = 1;
			Condition[] array15 = new Condition[1];
			array15[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID15, resultID15, bannerAmount15, resultAmount15, array15);
			int bannerItemID16 = 2955;
			int resultID16 = 393;
			int bannerAmount16 = 1;
			int resultAmount16 = 1;
			Condition[] array16 = new Condition[1];
			array16[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID16, resultID16, bannerAmount16, resultAmount16, array16);
			int bannerItemID17 = 1675;
			int resultID17 = 393;
			int bannerAmount17 = 1;
			int resultAmount17 = 1;
			Condition[] array17 = new Condition[1];
			array17[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID17, resultID17, bannerAmount17, resultAmount17, array17);
			int bannerItemID18 = 3391;
			int resultID18 = 393;
			int bannerAmount18 = 1;
			int resultAmount18 = 1;
			Condition[] array18 = new Condition[1];
			array18[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID18, resultID18, bannerAmount18, resultAmount18, array18);
			int bannerItemID19 = 1621;
			int resultID19 = 18;
			int bannerAmount19 = 1;
			int resultAmount19 = 1;
			Condition[] array19 = new Condition[1];
			array19[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID19, resultID19, bannerAmount19, resultAmount19, array19);
			int bannerItemID20 = 3393;
			int resultID20 = 18;
			int bannerAmount20 = 1;
			int resultAmount20 = 1;
			Condition[] array20 = new Condition[1];
			array20[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID20, resultID20, bannerAmount20, resultAmount20, array20);
			int bannerItemID21 = 2923;
			int resultID21 = 18;
			int bannerAmount21 = 1;
			int resultAmount21 = 1;
			Condition[] array21 = new Condition[1];
			array21[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID21, resultID21, bannerAmount21, resultAmount21, array21);
			int bannerItemID22 = 3392;
			int resultID22 = 18;
			int bannerAmount22 = 1;
			int resultAmount22 = 1;
			Condition[] array22 = new Condition[1];
			array22[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID22, resultID22, bannerAmount22, resultAmount22, array22);
			int bannerItemID23 = 3391;
			int resultID23 = 18;
			int bannerAmount23 = 1;
			int resultAmount23 = 1;
			Condition[] array23 = new Condition[1];
			array23[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID23, resultID23, bannerAmount23, resultAmount23, array23);
			int bannerItemID24 = 1694;
			int resultID24 = 3102;
			int bannerAmount24 = 1;
			int resultAmount24 = 1;
			Condition[] array24 = new Condition[1];
			array24[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID24, resultID24, bannerAmount24, resultAmount24, array24);
			int bannerItemID25 = 3407;
			int resultID25 = 3109;
			int bannerAmount25 = 1;
			int resultAmount25 = 1;
			Condition[] array25 = new Condition[1];
			array25[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID25, resultID25, bannerAmount25, resultAmount25, array25);
			int bannerItemID26 = 3408;
			int resultID26 = 3109;
			int bannerAmount26 = 1;
			int resultAmount26 = 1;
			Condition[] array26 = new Condition[1];
			array26[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID26, resultID26, bannerAmount26, resultAmount26, array26);
			int bannerItemID27 = 3406;
			int resultID27 = 3189;
			int bannerAmount27 = 1;
			int resultAmount27 = 1;
			Condition[] array27 = new Condition[1];
			array27[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID27, resultID27, bannerAmount27, resultAmount27, array27);
			int bannerItemID28 = 3406;
			int resultID28 = 3188;
			int bannerAmount28 = 1;
			int resultAmount28 = 1;
			Condition[] array28 = new Condition[1];
			array28[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID28, resultID28, bannerAmount28, resultAmount28, array28);
			int bannerItemID29 = 3406;
			int resultID29 = 3187;
			int bannerAmount29 = 1;
			int resultAmount29 = 1;
			Condition[] array29 = new Condition[1];
			array29[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID29, resultID29, bannerAmount29, resultAmount29, array29);
			int bannerItemID30 = 1681;
			int resultID30 = 954;
			int bannerAmount30 = 1;
			int resultAmount30 = 1;
			Condition[] array30 = new Condition[1];
			array30[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID30, resultID30, bannerAmount30, resultAmount30, array30);
			int bannerItemID31 = 1681;
			int resultID31 = 955;
			int bannerAmount31 = 1;
			int resultAmount31 = 1;
			Condition[] array31 = new Condition[1];
			array31[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID31, resultID31, bannerAmount31, resultAmount31, array31);
			int bannerItemID32 = 2987;
			int resultID32 = 411;
			int bannerAmount32 = 1;
			int resultAmount32 = 1;
			Condition[] array32 = new Condition[1];
			array32[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID32, resultID32, bannerAmount32, resultAmount32, array32);
			int bannerItemID33 = 2987;
			int resultID33 = 410;
			int bannerAmount33 = 1;
			int resultAmount33 = 1;
			Condition[] array33 = new Condition[1];
			array33[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID33, resultID33, bannerAmount33, resultAmount33, array33);
			int bannerItemID34 = 2987;
			int resultID34 = 88;
			int bannerAmount34 = 1;
			int resultAmount34 = 1;
			Condition[] array34 = new Condition[1];
			array34[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID34, resultID34, bannerAmount34, resultAmount34, array34);
			int bannerItemID35 = 4965;
			int resultID35 = 4761;
			int bannerAmount35 = 1;
			int resultAmount35 = 1;
			Condition[] array35 = new Condition[1];
			array35[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID35, resultID35, bannerAmount35, resultAmount35, array35);
			int bannerItemID36 = 1615;
			int resultID36 = 263;
			int bannerAmount36 = 1;
			int resultAmount36 = 1;
			Condition[] array36 = new Condition[2];
			array36[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array36[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID36, resultID36, bannerAmount36, resultAmount36, array36);
			int bannerItemID37 = 3391;
			int resultID37 = 4030;
			int bannerAmount37 = 1;
			int resultAmount37 = 1;
			Condition[] array37 = new Condition[1];
			array37[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID37, resultID37, bannerAmount37, resultAmount37, array37);
			int bannerItemID38 = 3393;
			int resultID38 = 4030;
			int bannerAmount38 = 1;
			int resultAmount38 = 1;
			Condition[] array38 = new Condition[1];
			array38[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID38, resultID38, bannerAmount38, resultAmount38, array38);
			int bannerItemID39 = 3392;
			int resultID39 = 4030;
			int bannerAmount39 = 1;
			int resultAmount39 = 1;
			Condition[] array39 = new Condition[1];
			array39[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID39, resultID39, bannerAmount39, resultAmount39, array39);
			Common.AddBannerSetToItemRecipe(NPCID.Sets.Skeletons, 5041);
			int bannerItemID40 = 1685;
			int resultID40 = 4020;
			int bannerAmount40 = 1;
			int resultAmount40 = 1;
			Condition[] array40 = new Condition[1];
			array40[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID40, resultID40, bannerAmount40, resultAmount40, array40);
			int bannerItemID41 = 1623;
			int resultID41 = 4020;
			int bannerAmount41 = 1;
			int resultAmount41 = 1;
			Condition[] array41 = new Condition[2];
			array41[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array41[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID41, resultID41, bannerAmount41, resultAmount41, array41);
			int bannerItemID42 = 3406;
			int resultID42 = 4029;
			int bannerAmount42 = 1;
			int resultAmount42 = 1;
			Condition[] array42 = new Condition[1];
			array42[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID42, resultID42, bannerAmount42, resultAmount42, array42);
			int bannerItemID43 = 3405;
			int resultID43 = 4029;
			int bannerAmount43 = 1;
			int resultAmount43 = 1;
			Condition[] array43 = new Condition[1];
			array43[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID43, resultID43, bannerAmount43, resultAmount43, array43);
			int bannerItemID44 = 3408;
			int resultID44 = 4036;
			int bannerAmount44 = 1;
			int resultAmount44 = 1;
			Condition[] array44 = new Condition[1];
			array44[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID44, resultID44, bannerAmount44, resultAmount44, array44);
			int bannerItemID45 = 3407;
			int resultID45 = 4036;
			int bannerAmount45 = 1;
			int resultAmount45 = 1;
			Condition[] array45 = new Condition[1];
			array45[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID45, resultID45, bannerAmount45, resultAmount45, array45);
			int bannerItemID46 = 2987;
			int resultID46 = 4037;
			int bannerAmount46 = 1;
			int resultAmount46 = 1;
			Condition[] array46 = new Condition[1];
			array46[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID46, resultID46, bannerAmount46, resultAmount46, array46);
			int anyBatBanner = Common.AnyBatBanner;
			int resultID47 = 5097;
			int resultAmount47 = 1;
			int groupAmount = 1;
			Condition[] array47 = new Condition[1];
			array47[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerGroupToItemRecipe(anyBatBanner, resultID47, resultAmount47, groupAmount, array47);
			int bannerItemID47 = 1621;
			int resultID48 = 1325;
			int bannerAmount47 = 1;
			int resultAmount48 = 1;
			Condition[] array48 = new Condition[2];
			array48[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array48[1] = Condition.NotRemixWorld;
			Common.AddBannerToItemRecipe(bannerItemID47, resultID48, bannerAmount47, resultAmount48, array48);
			int bannerItemID48 = 3393;
			int resultID49 = 3285;
			int bannerAmount48 = 1;
			int resultAmount49 = 1;
			Condition[] array49 = new Condition[1];
			array49[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID48, resultID49, bannerAmount48, resultAmount49, array49);
			int bannerItemID49 = 3392;
			int resultID50 = 3285;
			int bannerAmount49 = 1;
			int resultAmount50 = 1;
			Condition[] array50 = new Condition[1];
			array50[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID49, resultID50, bannerAmount49, resultAmount50, array50);
			int bannerItemID50 = 3391;
			int resultID51 = 3285;
			int bannerAmount50 = 1;
			int resultAmount51 = 1;
			Condition[] array51 = new Condition[1];
			array51[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID50, resultID51, bannerAmount50, resultAmount51, array51);
			int bannerItemID51 = 1681;
			int resultID52 = 1166;
			int bannerAmount51 = 1;
			int resultAmount52 = 1;
			Condition[] array52 = new Condition[1];
			array52[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID51, resultID52, bannerAmount51, resultAmount52, array52);
			int bannerItemID52 = 2987;
			int resultID53 = 1320;
			int bannerAmount52 = 1;
			int resultAmount53 = 1;
			Condition[] array53 = new Condition[1];
			array53[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID52, resultID53, bannerAmount52, resultAmount53, array53);
			int bannerItemID53 = 1630;
			int resultID54 = 1264;
			int bannerAmount53 = 1;
			int resultAmount54 = 1;
			Condition[] array54 = new Condition[2];
			array54[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array54[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID53, resultID54, bannerAmount53, resultAmount54, array54);
			int bannerItemID54 = 1630;
			int resultID55 = 676;
			int bannerAmount54 = 1;
			int resultAmount55 = 1;
			Condition[] array55 = new Condition[2];
			array55[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array55[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID54, resultID55, bannerAmount54, resultAmount55, array55);
			int bannerItemID55 = 1630;
			int resultID56 = 725;
			int bannerAmount55 = 1;
			int resultAmount56 = 1;
			Condition[] array56 = new Condition[2];
			array56[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array56[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID55, resultID56, bannerAmount55, resultAmount56, array56);
			int bannerItemID56 = 1630;
			int resultID57 = 517;
			int bannerAmount56 = 1;
			int resultAmount57 = 1;
			Condition[] array57 = new Condition[2];
			array57[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array57[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID56, resultID57, bannerAmount56, resultAmount57, array57);
			int bannerItemID57 = 3406;
			int resultID58 = 4463;
			int bannerAmount57 = 1;
			int resultAmount58 = 1;
			Condition[] array58 = new Condition[1];
			array58[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID57, resultID58, bannerAmount57, resultAmount58, array58);
			int bannerItemID58 = 2973;
			int resultID59 = 682;
			int bannerAmount58 = 1;
			int resultAmount59 = 1;
			Condition[] array59 = new Condition[2];
			array59[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array59[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID58, resultID59, bannerAmount58, resultAmount59, array59);
			int bannerItemID59 = 1620;
			int resultID60 = 723;
			int bannerAmount59 = 1;
			int resultAmount60 = 1;
			Condition[] array60 = new Condition[2];
			array60[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array60[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID59, resultID60, bannerAmount59, resultAmount60, array60);
			int bannerItemID60 = 3405;
			int resultID61 = 3269;
			int bannerAmount60 = 1;
			int resultAmount61 = 1;
			Condition[] array61 = new Condition[2];
			array61[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array61[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID60, resultID61, bannerAmount60, resultAmount61, array61);
			int bannerItemID61 = 2923;
			int resultID62 = 1325;
			int bannerAmount61 = 1;
			int resultAmount62 = 1;
			Condition[] array62 = new Condition[3];
			array62[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array62[1] = Condition.Hardmode;
			array62[2] = Condition.NotRemixWorld;
			Common.AddBannerToItemRecipe(bannerItemID61, resultID62, bannerAmount61, resultAmount62, array62);
			int bannerItemID62 = 1623;
			int resultID63 = 1308;
			int bannerAmount62 = 1;
			int resultAmount63 = 1;
			Condition[] array63 = new Condition[2];
			array63[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array63[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID62, resultID63, bannerAmount62, resultAmount63, array63);
			int bannerItemID63 = 1630;
			int resultID64 = 437;
			int bannerAmount63 = 1;
			int resultAmount64 = 1;
			Condition[] array64 = new Condition[2];
			array64[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array64[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID63, resultID64, bannerAmount63, resultAmount64, array64);
			int bannerItemID64 = 1630;
			int resultID65 = 1312;
			int bannerAmount64 = 1;
			int resultAmount65 = 1;
			Condition[] array65 = new Condition[2];
			array65[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array65[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID64, resultID65, bannerAmount64, resultAmount65, array65);
			int bannerItemID65 = 1698;
			int resultID66 = 215;
			int bannerAmount65 = 1;
			int resultAmount66 = 1;
			Condition[] array66 = new Condition[1];
			array66[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID65, resultID66, bannerAmount65, resultAmount66, array66);
			int bannerItemID66 = 1664;
			int resultID67 = 1323;
			int bannerAmount66 = 1;
			int resultAmount67 = 1;
			Condition[] array67 = new Condition[1];
			array67[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID66, resultID67, bannerAmount66, resultAmount67, array67);
			int bannerItemID67 = 1659;
			int resultID68 = 1322;
			int bannerAmount67 = 1;
			int resultAmount68 = 1;
			Condition[] array68 = new Condition[1];
			array68[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID67, resultID68, bannerAmount67, resultAmount68, array68);
			int bannerItemID68 = 2943;
			int resultID69 = 1322;
			int bannerAmount68 = 1;
			int resultAmount69 = 1;
			Condition[] array69 = new Condition[1];
			array69[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID68, resultID69, bannerAmount68, resultAmount69, array69);
			int bannerItemID69 = 1627;
			int resultID70 = 4025;
			int bannerAmount69 = 1;
			int resultAmount70 = 1;
			Condition[] array70 = new Condition[1];
			array70[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID69, resultID70, bannerAmount69, resultAmount70, array70);
			int bannerItemID70 = 3446;
			int resultID71 = 4025;
			int bannerAmount70 = 1;
			int resultAmount71 = 1;
			Condition[] array71 = new Condition[2];
			array71[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array71[1] = Condition.DownedMechBossAny;
			Common.AddBannerToItemRecipe(bannerItemID70, resultID71, bannerAmount70, resultAmount71, array71);
			int bannerItemID71 = 3446;
			int resultID72 = 1518;
			int bannerAmount71 = 1;
			int resultAmount72 = 1;
			Condition[] array72 = new Condition[2];
			array72[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array72[1] = Condition.DownedMechBossAny;
			Common.AddBannerToItemRecipe(bannerItemID71, resultID72, bannerAmount71, resultAmount72, array72);
			int bannerItemID72 = 1638;
			int resultID73 = 272;
			int bannerAmount72 = 1;
			int resultAmount73 = 1;
			Condition[] array73 = new Condition[1];
			array73[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID72, resultID73, bannerAmount72, resultAmount73, array73);
			int anyUnderworldBanner = Common.AnyUnderworldBanner;
			int resultID74 = 3282;
			int resultAmount74 = 1;
			int groupAmount2 = 1;
			Condition[] array74 = new Condition[2];
			array74[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array74[1] = Condition.DownedSkeletron;
			Common.AddBannerGroupToItemRecipe(anyUnderworldBanner, resultID74, resultAmount74, groupAmount2, array74);
			int anyUnderworldBanner2 = Common.AnyUnderworldBanner;
			int resultID75 = 3290;
			int resultAmount75 = 1;
			int groupAmount3 = 1;
			Condition[] array75 = new Condition[2];
			array75[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array75[1] = Condition.Hardmode;
			Common.AddBannerGroupToItemRecipe(anyUnderworldBanner2, resultID75, resultAmount75, groupAmount3, array75);
			int bannerItemID73 = 3446;
			int resultID76 = 683;
			int bannerAmount73 = 1;
			int resultAmount76 = 1;
			Condition[] array76 = new Condition[2];
			array76[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array76[1] = Condition.DownedMechBossAny;
			Common.AddBannerToItemRecipe(bannerItemID73, resultID76, bannerAmount73, resultAmount76, array76);
			int bannerItemID74 = 1658;
			int resultID77 = 4016;
			int bannerAmount74 = 1;
			int resultAmount77 = 1;
			Condition[] array77 = new Condition[1];
			array77[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID74, resultID77, bannerAmount74, resultAmount77, array77);
			int bannerItemID75 = 1658;
			int resultID78 = 1516;
			int bannerAmount75 = 1;
			int resultAmount78 = 1;
			Condition[] array78 = new Condition[2];
			array78[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array78[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID75, resultID78, bannerAmount75, resultAmount78, array78);
			int bannerItemID76 = 3452;
			int resultID79 = 1253;
			int bannerAmount76 = 1;
			int resultAmount79 = 1;
			Condition[] array79 = new Condition[2];
			array79[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array79[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID76, resultID79, bannerAmount76, resultAmount79, array79);
			int bannerItemID77 = 2898;
			int resultID80 = 393;
			int bannerAmount77 = 1;
			int resultAmount80 = 1;
			Condition[] array80 = new Condition[1];
			array80[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID77, resultID80, bannerAmount77, resultAmount80, array80);
			int bannerItemID78 = 1684;
			int resultID81 = 393;
			int bannerAmount78 = 1;
			int resultAmount81 = 1;
			Condition[] array81 = new Condition[1];
			array81[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID78, resultID81, bannerAmount78, resultAmount81, array81);
			int bannerItemID79 = 2988;
			int resultID82 = 393;
			int bannerAmount79 = 1;
			int resultAmount82 = 1;
			Condition[] array82 = new Condition[1];
			array82[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID79, resultID82, bannerAmount79, resultAmount82, array82);
			int bannerItemID80 = 2933;
			int resultID83 = 18;
			int bannerAmount80 = 1;
			int resultAmount83 = 1;
			Condition[] array83 = new Condition[1];
			array83[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID80, resultID83, bannerAmount80, resultAmount83, array83);
			int bannerItemID81 = 2988;
			int resultID84 = 879;
			int bannerAmount81 = 1;
			int resultAmount84 = 1;
			Condition[] array84 = new Condition[1];
			array84[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID81, resultID84, bannerAmount81, resultAmount84, array84);
			int bannerItemID82 = 2898;
			int resultID85 = 879;
			int bannerAmount82 = 1;
			int resultAmount85 = 1;
			Condition[] array85 = new Condition[1];
			array85[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID82, resultID85, bannerAmount82, resultAmount85, array85);
			int bannerItemID83 = 1643;
			int resultID86 = 805;
			int bannerAmount83 = 1;
			int resultAmount86 = 1;
			Condition[] array86 = new Condition[1];
			array86[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID83, resultID86, bannerAmount83, resultAmount86, array86);
			int bannerItemID84 = 1643;
			int resultID87 = 804;
			int bannerAmount84 = 1;
			int resultAmount87 = 1;
			Condition[] array87 = new Condition[1];
			array87[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID84, resultID87, bannerAmount84, resultAmount87, array87);
			int bannerItemID85 = 1643;
			int resultID88 = 803;
			int bannerAmount85 = 1;
			int resultAmount88 = 1;
			Condition[] array88 = new Condition[1];
			array88[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID85, resultID88, bannerAmount85, resultAmount88, array88);
			int bannerItemID86 = 2935;
			int resultID89 = 4026;
			int bannerAmount86 = 1;
			int resultAmount89 = 1;
			Condition[] array89 = new Condition[1];
			array89[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID86, resultID89, bannerAmount86, resultAmount89, array89);
			int bannerItemID87 = 2933;
			int resultID90 = 4026;
			int bannerAmount87 = 1;
			int resultAmount90 = 1;
			Condition[] array90 = new Condition[1];
			array90[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID87, resultID90, bannerAmount87, resultAmount90, array90);
			int bannerItemID88 = 2980;
			int resultID91 = 4026;
			int bannerAmount88 = 1;
			int resultAmount91 = 1;
			Condition[] array91 = new Condition[1];
			array91[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID88, resultID91, bannerAmount88, resultAmount91, array91);
			int bannerItemID89 = 1663;
			int resultID92 = 4027;
			int bannerAmount89 = 1;
			int resultAmount92 = 1;
			Condition[] array92 = new Condition[2];
			array92[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array92[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID89, resultID92, bannerAmount89, resultAmount92, array92);
			int bannerItemID90 = 3452;
			int resultID93 = 4027;
			int bannerAmount90 = 1;
			int resultAmount93 = 1;
			Condition[] array93 = new Condition[2];
			array93[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array93[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID90, resultID93, bannerAmount90, resultAmount93, array93);
			int bannerItemID91 = 1674;
			int resultID94 = 3532;
			int bannerAmount91 = 1;
			int resultAmount94 = 1;
			Condition[] array94 = new Condition[2];
			array94[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array94[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID91, resultID94, bannerAmount91, resultAmount94, array94);
			int bannerItemID92 = 2934;
			int resultID95 = 1519;
			int bannerAmount92 = 1;
			int resultAmount95 = 1;
			Condition[] array95 = new Condition[2];
			array95[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array95[1] = Condition.DownedMechBossAny;
			Common.AddBannerToItemRecipe(bannerItemID92, resultID95, bannerAmount92, resultAmount95, array95);
			int bannerItemID93 = 1684;
			int resultID96 = 951;
			int bannerAmount93 = 1;
			int resultAmount96 = 1;
			Condition[] array96 = new Condition[1];
			array96[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID93, resultID96, bannerAmount93, resultAmount96, array96);
			int anySnowBanner = Common.AnySnowBanner;
			int resultID97 = 3289;
			int resultAmount97 = 1;
			int groupAmount4 = 1;
			Condition[] array97 = new Condition[2];
			array97[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array97[1] = Condition.Hardmode;
			Common.AddBannerGroupToItemRecipe(anySnowBanner, resultID97, resultAmount97, groupAmount4, array97);
			int bannerItemID94 = 2898;
			int resultID98 = 1306;
			int bannerAmount94 = 1;
			int resultAmount98 = 1;
			Condition[] array98 = new Condition[2];
			array98[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array98[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID94, resultID98, bannerAmount94, resultAmount98, array98);
			int bannerItemID95 = 1663;
			int resultID99 = 726;
			int bannerAmount95 = 1;
			int resultAmount99 = 1;
			Condition[] array99 = new Condition[2];
			array99[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array99[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID95, resultID99, bannerAmount95, resultAmount99, array99);
			int bannerItemID96 = 1674;
			int resultID100 = 5096;
			int bannerAmount96 = 1;
			int resultAmount100 = 1;
			Condition[] array100 = new Condition[2];
			array100[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array100[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID96, resultID100, bannerAmount96, resultAmount100, array100);
			int bannerItemID97 = 1674;
			int resultID101 = 4428;
			int bannerAmount97 = 1;
			int resultAmount101 = 1;
			Condition[] array101 = new Condition[2];
			array101[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array101[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID97, resultID101, bannerAmount97, resultAmount101, array101);
			int bannerItemID98 = 1696;
			int resultID102 = 5130;
			int bannerAmount98 = 1;
			int resultAmount102 = 1;
			Condition[] array102 = new Condition[2];
			array102[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array102[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID98, resultID102, bannerAmount98, resultAmount102, array102);
			int anySnowBanner2 = Common.AnySnowBanner;
			int resultID103 = 1537;
			int resultAmount103 = 1;
			int groupAmount5 = 5;
			Condition[] array103 = new Condition[2];
			array103[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array103[1] = Condition.Hardmode;
			Common.AddBannerGroupToItemRecipe(anySnowBanner2, resultID103, resultAmount103, groupAmount5, array103);
			int bannerItemID99 = 1671;
			int resultID104 = 889;
			int bannerAmount99 = 1;
			int resultAmount104 = 1;
			Condition[] array104 = new Condition[2];
			array104[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array104[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID99, resultID104, bannerAmount99, resultAmount104, array104);
			int bannerItemID100 = 3417;
			int resultID105 = 3770;
			int bannerAmount100 = 1;
			int resultAmount105 = 1;
			Condition[] array105 = new Condition[2];
			array105[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array105[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID100, resultID105, bannerAmount100, resultAmount105, array105);
			int bannerItemID101 = 3416;
			int resultID106 = 3784;
			int bannerAmount101 = 1;
			int resultAmount106 = 1;
			Condition[] array106 = new Condition[2];
			array106[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array106[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID101, resultID106, bannerAmount101, resultAmount106, array106);
			int bannerItemID102 = 3416;
			int resultID107 = 3785;
			int bannerAmount102 = 1;
			int resultAmount107 = 1;
			Condition[] array107 = new Condition[2];
			array107[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array107[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID102, resultID107, bannerAmount102, resultAmount107, array107);
			int bannerItemID103 = 3416;
			int resultID108 = 3786;
			int bannerAmount103 = 1;
			int resultAmount108 = 1;
			Condition[] array108 = new Condition[2];
			array108[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array108[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID103, resultID108, bannerAmount103, resultAmount108, array108);
			int bannerItemID104 = 3416;
			int resultID109 = 2801;
			int bannerAmount104 = 1;
			int resultAmount109 = 1;
			Condition[] array109 = new Condition[2];
			array109[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array109[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID104, resultID109, bannerAmount104, resultAmount109, array109);
			int bannerItemID105 = 3416;
			int resultID110 = 2802;
			int bannerAmount105 = 1;
			int resultAmount110 = 1;
			Condition[] array110 = new Condition[2];
			array110[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array110[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID105, resultID110, bannerAmount105, resultAmount110, array110);
			int bannerItemID106 = 1671;
			int resultID111 = 872;
			int bannerAmount106 = 1;
			int resultAmount111 = 1;
			Condition[] array111 = new Condition[2];
			array111[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array111[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID106, resultID111, bannerAmount106, resultAmount111, array111);
			int bannerItemID107 = 1671;
			int resultID112 = 871;
			int bannerAmount107 = 1;
			int resultAmount112 = 1;
			Condition[] array112 = new Condition[2];
			array112[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array112[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID107, resultID112, bannerAmount107, resultAmount112, array112);
			int bannerItemID108 = 1671;
			int resultID113 = 870;
			int bannerAmount108 = 1;
			int resultAmount113 = 1;
			Condition[] array113 = new Condition[2];
			array113[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array113[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID108, resultID113, bannerAmount108, resultAmount113, array113);
			int bannerItemID109 = 1618;
			int resultID114 = 4012;
			int bannerAmount109 = 1;
			int resultAmount114 = 1;
			Condition[] array114 = new Condition[1];
			array114[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID109, resultID114, bannerAmount109, resultAmount114, array114);
			int bannerItemID110 = 3414;
			int resultID115 = 4012;
			int bannerAmount110 = 1;
			int resultAmount115 = 1;
			Condition[] array115 = new Condition[1];
			array115[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID110, resultID115, bannerAmount110, resultAmount115, array115);
			int bannerItemID111 = 3413;
			int resultID116 = 4012;
			int bannerAmount111 = 1;
			int resultAmount116 = 1;
			Condition[] array116 = new Condition[1];
			array116[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID111, resultID116, bannerAmount111, resultAmount116, array116);
			int bannerItemID112 = 3793;
			int resultID117 = 4028;
			int bannerAmount112 = 1;
			int resultAmount117 = 1;
			Condition[] array117 = new Condition[1];
			array117[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID112, resultID117, bannerAmount112, resultAmount117, array117);
			int bannerItemID113 = 3419;
			int resultID118 = 4020;
			int bannerAmount113 = 1;
			int resultAmount118 = 1;
			Condition[] array118 = new Condition[2];
			array118[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array118[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID113, resultID118, bannerAmount113, resultAmount118, array118);
			int bannerItemID114 = 3789;
			int resultID119 = 4028;
			int bannerAmount114 = 1;
			int resultAmount119 = 1;
			Condition[] array119 = new Condition[2];
			array119[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array119[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID114, resultID119, bannerAmount114, resultAmount119, array119);
			int bannerItemID115 = 3414;
			int resultID120 = 3772;
			int bannerAmount115 = 1;
			int resultAmount120 = 1;
			Condition[] array120 = new Condition[1];
			array120[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID115, resultID120, bannerAmount115, resultAmount120, array120);
			int bannerItemID116 = 3418;
			int resultID121 = 3771;
			int bannerAmount116 = 1;
			int resultAmount121 = 1;
			Condition[] array121 = new Condition[2];
			array121[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array121[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID116, resultID121, bannerAmount116, resultAmount121, array121);
			int anyDesertBanner = Common.AnyDesertBanner;
			int resultID122 = 4714;
			int resultAmount122 = 1;
			int groupAmount6 = 5;
			Condition[] array122 = new Condition[2];
			array122[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array122[1] = Condition.Hardmode;
			Common.AddBannerGroupToItemRecipe(anyDesertBanner, resultID122, resultAmount122, groupAmount6, array122);
			int bannerItemID117 = 1680;
			int resultID123 = 268;
			int bannerAmount117 = 1;
			int resultAmount123 = 1;
			Condition[] array123 = new Condition[1];
			array123[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID117, resultID123, bannerAmount117, resultAmount123, array123);
			int bannerItemID118 = 3447;
			int resultID124 = 1303;
			int bannerAmount118 = 1;
			int resultAmount124 = 1;
			Condition[] array124 = new Condition[1];
			array124[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID118, resultID124, bannerAmount118, resultAmount124, array124);
			int bannerItemID119 = 1680;
			int resultID125 = 4035;
			int bannerAmount119 = 1;
			int resultAmount125 = 1;
			Condition[] array125 = new Condition[1];
			array125[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID119, resultID125, bannerAmount119, resultAmount125, array125);
			int bannerItemID120 = 1634;
			int resultID126 = 4035;
			int bannerAmount120 = 1;
			int resultAmount126 = 1;
			Condition[] array126 = new Condition[1];
			array126[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID120, resultID126, bannerAmount120, resultAmount126, array126);
			int bannerItemID121 = 1661;
			int resultID127 = 887;
			int bannerAmount121 = 1;
			int resultAmount127 = 1;
			Condition[] array127 = new Condition[1];
			array127[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID121, resultID127, bannerAmount121, resultAmount127, array127);
			int bannerItemID122 = 4976;
			int resultID128 = 887;
			int bannerAmount122 = 1;
			int resultAmount128 = 1;
			Condition[] array128 = new Condition[1];
			array128[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID122, resultID128, bannerAmount122, resultAmount128, array128);
			int bannerItemID123 = 2939;
			int resultID129 = 18;
			int bannerAmount123 = 1;
			int resultAmount129 = 1;
			Condition[] array129 = new Condition[1];
			array129[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID123, resultID129, bannerAmount123, resultAmount129, array129);
			int bannerItemID124 = 1661;
			int resultID130 = 962;
			int bannerAmount124 = 1;
			int resultAmount130 = 1;
			Condition[] array130 = new Condition[1];
			array130[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID124, resultID130, bannerAmount124, resultAmount130, array130);
			int bannerItemID125 = 1661;
			int resultID131 = 961;
			int bannerAmount125 = 1;
			int resultAmount131 = 1;
			Condition[] array131 = new Condition[1];
			array131[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID125, resultID131, bannerAmount125, resultAmount131, array131);
			int bannerItemID126 = 1661;
			int resultID132 = 960;
			int bannerAmount126 = 1;
			int resultAmount132 = 1;
			Condition[] array132 = new Condition[1];
			array132[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID126, resultID132, bannerAmount126, resultAmount132, array132);
			int bannerItemID127 = 1675;
			int resultID133 = 263;
			int bannerAmount127 = 1;
			int resultAmount133 = 1;
			Condition[] array133 = new Condition[1];
			array133[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID127, resultID133, bannerAmount127, resultAmount133, array133);
			int bannerItemID128 = 1668;
			int resultID134 = 5042;
			int bannerAmount128 = 1;
			int resultAmount134 = 1;
			Condition[] array134 = new Condition[1];
			array134[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID128, resultID134, bannerAmount128, resultAmount134, array134);
			int bannerItemID129 = 2977;
			int resultID135 = 5042;
			int bannerAmount129 = 1;
			int resultAmount135 = 1;
			Condition[] array135 = new Condition[1];
			array135[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID129, resultID135, bannerAmount129, resultAmount135, array135);
			int bannerItemID130 = 2897;
			int resultID136 = 5042;
			int bannerAmount130 = 1;
			int resultAmount136 = 1;
			Condition[] array136 = new Condition[2];
			array136[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array136[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID130, resultID136, bannerAmount130, resultAmount136, array136);
			int bannerItemID131 = 2925;
			int resultID137 = 4023;
			int bannerAmount131 = 1;
			int resultAmount137 = 1;
			Condition[] array137 = new Condition[2];
			array137[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array137[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID131, resultID137, bannerAmount131, resultAmount137, array137);
			int bannerItemID132 = 1640;
			int resultID138 = 4023;
			int bannerAmount132 = 1;
			int resultAmount138 = 1;
			Condition[] array138 = new Condition[2];
			array138[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array138[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID132, resultID138, bannerAmount132, resultAmount138, array138);
			int bannerItemID133 = 4976;
			int resultID139 = 1521;
			int bannerAmount133 = 1;
			int resultAmount139 = 1;
			Condition[] array139 = new Condition[2];
			array139[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array139[1] = Condition.DownedMechBossAny;
			Common.AddBannerToItemRecipe(bannerItemID133, resultID139, bannerAmount133, resultAmount139, array139);
			int bannerItemID134 = 1670;
			int resultID140 = 1611;
			int bannerAmount134 = 1;
			int resultAmount140 = 1;
			Condition[] array140 = new Condition[2];
			array140[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array140[1] = Condition.DownedMechBossAny;
			Common.AddBannerToItemRecipe(bannerItemID134, resultID140, bannerAmount134, resultAmount140, array140);
			int bannerItemID135 = 2897;
			int resultID141 = 1265;
			int bannerAmount135 = 1;
			int resultAmount141 = 1;
			Condition[] array141 = new Condition[2];
			array141[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array141[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID135, resultID141, bannerAmount135, resultAmount141, array141);
			int anyJungleBanner = Common.AnyJungleBanner;
			int resultID142 = 3286;
			int resultAmount142 = 1;
			int groupAmount7 = 1;
			Condition[] array142 = new Condition[2];
			array142[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array142[1] = Condition.DownedMechBossAny;
			Common.AddBannerGroupToItemRecipe(anyJungleBanner, resultID142, resultAmount142, groupAmount7, array142);
			int anyJungleBanner2 = Common.AnyJungleBanner;
			int resultID143 = 1533;
			int resultAmount143 = 1;
			int groupAmount8 = 5;
			Condition[] array143 = new Condition[2];
			array143[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array143[1] = Condition.Hardmode;
			Common.AddBannerGroupToItemRecipe(anyJungleBanner2, resultID143, resultAmount143, groupAmount8, array143);
			int bannerItemID136 = 1667;
			int resultID144 = 1172;
			int bannerAmount136 = 1;
			int resultAmount144 = 1;
			Condition[] array144 = new Condition[2];
			array144[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array144[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID136, resultID144, bannerAmount136, resultAmount144, array144);
			int bannerItemID137 = 4968;
			int resultID145 = 18;
			int bannerAmount137 = 1;
			int resultAmount145 = 1;
			Condition[] array145 = new Condition[1];
			array145[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID137, resultID145, bannerAmount137, resultAmount145, array145);
			int bannerItemID138 = 4968;
			int resultID146 = 4764;
			int bannerAmount138 = 1;
			int resultAmount146 = 1;
			Condition[] array146 = new Condition[1];
			array146[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID138, resultID146, bannerAmount138, resultAmount146, array146);
			int bannerItemID139 = 1637;
			int resultID147 = 891;
			int bannerAmount139 = 1;
			int resultAmount147 = 1;
			Condition[] array147 = new Condition[1];
			array147[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID139, resultID147, bannerAmount139, resultAmount147, array147);
			int bannerItemID140 = 3449;
			int resultID148 = 888;
			int bannerAmount140 = 1;
			int resultAmount148 = 1;
			Condition[] array148 = new Condition[2];
			array148[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array148[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID140, resultID148, bannerAmount140, resultAmount148, array148);
			int bannerItemID141 = 3449;
			int resultID149 = 890;
			int bannerAmount141 = 1;
			int resultAmount149 = 1;
			Condition[] array149 = new Condition[2];
			array149[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array149[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID141, resultID149, bannerAmount141, resultAmount149, array149);
			int bannerItemID142 = 2908;
			int resultID150 = 888;
			int bannerAmount142 = 1;
			int resultAmount150 = 1;
			Condition[] array150 = new Condition[2];
			array150[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array150[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID142, resultID150, bannerAmount142, resultAmount150, array150);
			int bannerItemID143 = 2909;
			int resultID151 = 892;
			int bannerAmount143 = 1;
			int resultAmount151 = 1;
			Condition[] array151 = new Condition[2];
			array151[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array151[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID143, resultID151, bannerAmount143, resultAmount151, array151);
			int bannerItemID144 = 1641;
			int resultID152 = 958;
			int bannerAmount144 = 1;
			int resultAmount152 = 1;
			Condition[] array152 = new Condition[1];
			array152[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID144, resultID152, bannerAmount144, resultAmount152, array152);
			int bannerItemID145 = 1641;
			int resultID153 = 957;
			int bannerAmount145 = 1;
			int resultAmount153 = 1;
			Condition[] array153 = new Condition[1];
			array153[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID145, resultID153, bannerAmount145, resultAmount153, array153);
			int bannerItemID146 = 1641;
			int resultID154 = 956;
			int bannerAmount146 = 1;
			int resultAmount154 = 1;
			Condition[] array154 = new Condition[1];
			array154[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID146, resultID154, bannerAmount146, resultAmount154, array154);
			int bannerItemID147 = 1632;
			int resultID155 = 243;
			int bannerAmount147 = 1;
			int resultAmount155 = 1;
			Condition[] array155 = new Condition[1];
			array155[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID147, resultID155, bannerAmount147, resultAmount155, array155);
			int bannerItemID148 = 2907;
			int resultID156 = 3759;
			int bannerAmount148 = 1;
			int resultAmount156 = 1;
			Condition[] array156 = new Condition[1];
			array156[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID148, resultID156, bannerAmount148, resultAmount156, array156);
			int bannerItemID149 = 2907;
			int resultID157 = 3758;
			int bannerAmount149 = 1;
			int resultAmount157 = 1;
			Condition[] array157 = new Condition[1];
			array157[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID149, resultID157, bannerAmount149, resultAmount157, array157);
			int bannerItemID150 = 2907;
			int resultID158 = 3757;
			int bannerAmount150 = 1;
			int resultAmount158 = 1;
			Condition[] array158 = new Condition[1];
			array158[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID150, resultID158, bannerAmount150, resultAmount158, array158);
			int bannerItemID151 = 3449;
			int resultID159 = 872;
			int bannerAmount151 = 1;
			int resultAmount159 = 1;
			Condition[] array159 = new Condition[2];
			array159[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array159[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID151, resultID159, bannerAmount151, resultAmount159, array159);
			int bannerItemID152 = 3449;
			int resultID160 = 871;
			int bannerAmount152 = 1;
			int resultAmount160 = 1;
			Condition[] array160 = new Condition[2];
			array160[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array160[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID152, resultID160, bannerAmount152, resultAmount160, array160);
			int bannerItemID153 = 3449;
			int resultID161 = 870;
			int bannerAmount153 = 1;
			int resultAmount161 = 1;
			Condition[] array161 = new Condition[2];
			array161[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array161[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID153, resultID161, bannerAmount153, resultAmount161, array161);
			int bannerItemID154 = 1641;
			int resultID162 = 4015;
			int bannerAmount154 = 1;
			int resultAmount162 = 1;
			Condition[] array162 = new Condition[1];
			array162[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID154, resultID162, bannerAmount154, resultAmount162, array162);
			int bannerItemID155 = 3790;
			int resultID163 = 4028;
			int bannerAmount155 = 1;
			int resultAmount163 = 1;
			Condition[] array163 = new Condition[2];
			array163[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array163[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID155, resultID163, bannerAmount155, resultAmount163, array163);
			int anyCorruptionBanner = Common.AnyCorruptionBanner;
			int resultID164 = 996;
			int resultAmount164 = 1;
			int groupAmount9 = 1;
			Condition[] array164 = new Condition[2];
			array164[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array164[1] = Condition.Hardmode;
			Common.AddBannerGroupToItemRecipe(anyCorruptionBanner, resultID164, resultAmount164, groupAmount9, array164);
			int bannerItemID156 = 1641;
			int resultID165 = 5094;
			int bannerAmount156 = 1;
			int resultAmount165 = 1;
			Condition[] array165 = new Condition[1];
			array165[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID156, resultID165, bannerAmount156, resultAmount165, array165);
			int anyCorruptionBanner2 = Common.AnyCorruptionBanner;
			int resultID166 = 1534;
			int resultAmount166 = 1;
			int groupAmount10 = 5;
			Condition[] array166 = new Condition[2];
			array166[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array166[1] = Condition.Hardmode;
			Common.AddBannerGroupToItemRecipe(anyCorruptionBanner2, resultID166, resultAmount166, groupAmount10, array166);
			int bannerItemID157 = 1636;
			int resultID167 = 891;
			int bannerAmount157 = 1;
			int resultAmount167 = 1;
			Condition[] array167 = new Condition[1];
			array167[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID157, resultID167, bannerAmount157, resultAmount167, array167);
			int bannerItemID158 = 4966;
			int resultID168 = 888;
			int bannerAmount158 = 1;
			int resultAmount168 = 1;
			Condition[] array168 = new Condition[2];
			array168[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array168[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID158, resultID168, bannerAmount158, resultAmount168, array168);
			int bannerItemID159 = 4966;
			int resultID169 = 890;
			int bannerAmount159 = 1;
			int resultAmount169 = 1;
			Condition[] array169 = new Condition[2];
			array169[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array169[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID159, resultID169, bannerAmount159, resultAmount169, array169);
			int bannerItemID160 = 2910;
			int resultID170 = 888;
			int bannerAmount160 = 1;
			int resultAmount170 = 1;
			Condition[] array170 = new Condition[2];
			array170[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array170[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID160, resultID170, bannerAmount160, resultAmount170, array170);
			int bannerItemID161 = 1645;
			int resultID171 = 892;
			int bannerAmount161 = 1;
			int resultAmount171 = 1;
			Condition[] array171 = new Condition[2];
			array171[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array171[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID161, resultID171, bannerAmount161, resultAmount171, array171);
			int bannerItemID162 = 4970;
			int resultID172 = 243;
			int bannerAmount162 = 1;
			int resultAmount172 = 1;
			Condition[] array172 = new Condition[1];
			array172[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID162, resultID172, bannerAmount162, resultAmount172, array172);
			int bannerItemID163 = 4972;
			int resultID173 = 3759;
			int bannerAmount163 = 1;
			int resultAmount173 = 1;
			Condition[] array173 = new Condition[1];
			array173[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID163, resultID173, bannerAmount163, resultAmount173, array173);
			int bannerItemID164 = 4972;
			int resultID174 = 3758;
			int bannerAmount164 = 1;
			int resultAmount174 = 1;
			Condition[] array174 = new Condition[1];
			array174[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID164, resultID174, bannerAmount164, resultAmount174, array174);
			int bannerItemID165 = 4972;
			int resultID175 = 3757;
			int bannerAmount165 = 1;
			int resultAmount175 = 1;
			Condition[] array175 = new Condition[1];
			array175[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID165, resultID175, bannerAmount165, resultAmount175, array175);
			int bannerItemID166 = 4966;
			int resultID176 = 872;
			int bannerAmount166 = 1;
			int resultAmount176 = 1;
			Condition[] array176 = new Condition[2];
			array176[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array176[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID166, resultID176, bannerAmount166, resultAmount176, array176);
			int bannerItemID167 = 4966;
			int resultID177 = 871;
			int bannerAmount167 = 1;
			int resultAmount177 = 1;
			Condition[] array177 = new Condition[2];
			array177[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array177[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID167, resultID177, bannerAmount167, resultAmount177, array177);
			int bannerItemID168 = 4966;
			int resultID178 = 870;
			int bannerAmount168 = 1;
			int resultAmount178 = 1;
			Condition[] array178 = new Condition[2];
			array178[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array178[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID168, resultID178, bannerAmount168, resultAmount178, array178);
			int bannerItemID169 = 1635;
			int resultID179 = 4015;
			int bannerAmount169 = 1;
			int resultAmount179 = 1;
			Condition[] array179 = new Condition[1];
			array179[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID169, resultID179, bannerAmount169, resultAmount179, array179);
			int bannerItemID170 = 3791;
			int resultID180 = 4028;
			int bannerAmount170 = 1;
			int resultAmount180 = 1;
			Condition[] array180 = new Condition[2];
			array180[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array180[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID170, resultID180, bannerAmount170, resultAmount180, array180);
			int anyCrimsonBanner = Common.AnyCrimsonBanner;
			int resultID181 = 996;
			int resultAmount181 = 1;
			int groupAmount11 = 1;
			Condition[] array181 = new Condition[2];
			array181[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array181[1] = Condition.Hardmode;
			Common.AddBannerGroupToItemRecipe(anyCrimsonBanner, resultID181, resultAmount181, groupAmount11, array181);
			int bannerItemID171 = 1626;
			int resultID182 = 5094;
			int bannerAmount171 = 1;
			int resultAmount182 = 1;
			Condition[] array182 = new Condition[1];
			array182[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID171, resultID182, bannerAmount171, resultAmount182, array182);
			int bannerItemID172 = 1635;
			int resultID183 = 5094;
			int bannerAmount172 = 1;
			int resultAmount183 = 1;
			Condition[] array183 = new Condition[1];
			array183[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID172, resultID183, bannerAmount172, resultAmount183, array183);
			int bannerItemID173 = 1644;
			int resultID184 = 5094;
			int bannerAmount173 = 1;
			int resultAmount184 = 1;
			Condition[] array184 = new Condition[1];
			array184[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID173, resultID184, bannerAmount173, resultAmount184, array184);
			int anyCrimsonBanner2 = Common.AnyCrimsonBanner;
			int resultID185 = 1535;
			int resultAmount185 = 1;
			int groupAmount12 = 5;
			Condition[] array185 = new Condition[2];
			array185[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array185[1] = Condition.Hardmode;
			Common.AddBannerGroupToItemRecipe(anyCrimsonBanner2, resultID185, resultAmount185, groupAmount12, array185);
			int bannerItemID174 = 1642;
			int resultID186 = 891;
			int bannerAmount174 = 1;
			int resultAmount186 = 1;
			Condition[] array186 = new Condition[1];
			array186[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID174, resultID186, bannerAmount174, resultAmount186, array186);
			int bannerItemID175 = 3450;
			int resultID187 = 893;
			int bannerAmount175 = 1;
			int resultAmount187 = 1;
			Condition[] array187 = new Condition[2];
			array187[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array187[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID175, resultID187, bannerAmount175, resultAmount187, array187);
			int bannerItemID176 = 1677;
			int resultID188 = 890;
			int bannerAmount176 = 1;
			int resultAmount188 = 1;
			Condition[] array188 = new Condition[2];
			array188[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array188[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID176, resultID188, bannerAmount176, resultAmount188, array188);
			int bannerItemID177 = 1677;
			int resultID189 = 889;
			int bannerAmount177 = 1;
			int resultAmount189 = 1;
			Condition[] array189 = new Condition[2];
			array189[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array189[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID177, resultID189, bannerAmount177, resultAmount189, array189);
			int bannerItemID178 = 3450;
			int resultID190 = 872;
			int bannerAmount178 = 1;
			int resultAmount190 = 1;
			Condition[] array190 = new Condition[2];
			array190[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array190[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID178, resultID190, bannerAmount178, resultAmount190, array190);
			int bannerItemID179 = 3450;
			int resultID191 = 871;
			int bannerAmount179 = 1;
			int resultAmount191 = 1;
			Condition[] array191 = new Condition[2];
			array191[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array191[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID179, resultID191, bannerAmount179, resultAmount191, array191);
			int bannerItemID180 = 3450;
			int resultID192 = 870;
			int bannerAmount180 = 1;
			int resultAmount192 = 1;
			Condition[] array192 = new Condition[2];
			array192[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array192[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID180, resultID192, bannerAmount180, resultAmount192, array192);
			int bannerItemID181 = 3792;
			int resultID193 = 4028;
			int bannerAmount181 = 1;
			int resultAmount193 = 1;
			Condition[] array193 = new Condition[2];
			array193[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array193[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID181, resultID193, bannerAmount181, resultAmount193, array193);
			int bannerItemID182 = 1629;
			int resultID194 = 4011;
			int bannerAmount182 = 1;
			int resultAmount194 = 1;
			Condition[] array194 = new Condition[2];
			array194[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array194[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID182, resultID194, bannerAmount182, resultAmount194, array194);
			int bannerItemID183 = 2937;
			int resultID195 = 4011;
			int bannerAmount183 = 1;
			int resultAmount195 = 1;
			Condition[] array195 = new Condition[2];
			array195[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array195[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID183, resultID195, bannerAmount183, resultAmount195, array195);
			int bannerItemID184 = 2938;
			int resultID196 = 4011;
			int bannerAmount184 = 1;
			int resultAmount196 = 1;
			Condition[] array196 = new Condition[2];
			array196[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array196[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID184, resultID196, bannerAmount184, resultAmount196, array196);
			int bannerItemID185 = 1651;
			int resultID197 = 4017;
			int bannerAmount185 = 1;
			int resultAmount197 = 1;
			Condition[] array197 = new Condition[2];
			array197[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array197[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID185, resultID197, bannerAmount185, resultAmount197, array197);
			int bannerItemID186 = 1691;
			int resultID198 = 3260;
			int bannerAmount186 = 1;
			int resultAmount198 = 1;
			Condition[] array198 = new Condition[2];
			array198[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array198[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID186, resultID198, bannerAmount186, resultAmount198, array198);
			int bannerItemID187 = 1629;
			int resultID199 = 1326;
			int bannerAmount187 = 1;
			int resultAmount199 = 1;
			Condition[] array199 = new Condition[2];
			array199[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array199[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID187, resultID199, bannerAmount187, resultAmount199, array199);
			int bannerItemID188 = 1691;
			int resultID200 = 856;
			int bannerAmount188 = 1;
			int resultAmount200 = 1;
			Condition[] array200 = new Condition[2];
			array200[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array200[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID188, resultID200, bannerAmount188, resultAmount200, array200);
			int anyHallowBanner = Common.AnyHallowBanner;
			int resultID201 = 1536;
			int resultAmount201 = 1;
			int groupAmount13 = 5;
			Condition[] array201 = new Condition[2];
			array201[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array201[1] = Condition.Hardmode;
			Common.AddBannerGroupToItemRecipe(anyHallowBanner, resultID201, resultAmount201, groupAmount13, array201);
			int bannerItemID189 = 2904;
			int resultID202 = 963;
			int bannerAmount189 = 1;
			int resultAmount202 = 1;
			Condition[] array202 = new Condition[2];
			array202[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array202[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID189, resultID202, bannerAmount189, resultAmount202, array202);
			int bannerItemID190 = 2904;
			int resultID203 = 977;
			int bannerAmount190 = 1;
			int resultAmount203 = 1;
			Condition[] array203 = new Condition[2];
			array203[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array203[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID190, resultID203, bannerAmount190, resultAmount203, array203);
			int bannerItemID191 = 2975;
			int resultID204 = 1300;
			int bannerAmount191 = 1;
			int resultAmount204 = 1;
			Condition[] array204 = new Condition[2];
			array204[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array204[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID191, resultID204, bannerAmount191, resultAmount204, array204);
			int bannerItemID192 = 2958;
			int resultID205 = 938;
			int bannerAmount192 = 1;
			int resultAmount205 = 1;
			Condition[] array205 = new Condition[2];
			array205[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array205[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID192, resultID205, bannerAmount192, resultAmount205, array205);
			int bannerItemID193 = 2911;
			int resultID206 = 891;
			int bannerAmount193 = 1;
			int resultAmount206 = 1;
			Condition[] array206 = new Condition[1];
			array206[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID193, resultID206, bannerAmount193, resultAmount206, array206);
			int bannerItemID194 = 2924;
			int resultID207 = 891;
			int bannerAmount194 = 1;
			int resultAmount207 = 1;
			Condition[] array207 = new Condition[1];
			array207[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID194, resultID207, bannerAmount194, resultAmount207, array207);
			int bannerItemID195 = 2970;
			int resultID208 = 885;
			int bannerAmount195 = 1;
			int resultAmount208 = 1;
			Condition[] array208 = new Condition[2];
			array208[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array208[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID195, resultID208, bannerAmount195, resultAmount208, array208);
			int bannerItemID196 = 2900;
			int resultID209 = 886;
			int bannerAmount196 = 1;
			int resultAmount209 = 1;
			Condition[] array209 = new Condition[2];
			array209[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array209[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID196, resultID209, bannerAmount196, resultAmount209, array209);
			int bannerItemID197 = 3451;
			int resultID210 = 3095;
			int bannerAmount197 = 1;
			int resultAmount210 = 1;
			Condition[] array210 = new Condition[1];
			array210[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID197, resultID210, bannerAmount197, resultAmount210, array210);
			int bannerItemID198 = 2911;
			int resultID211 = 3095;
			int bannerAmount198 = 1;
			int resultAmount211 = 1;
			Condition[] array211 = new Condition[1];
			array211[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID198, resultID211, bannerAmount198, resultAmount211, array211);
			int bannerItemID199 = 1682;
			int resultID212 = 3095;
			int bannerAmount199 = 1;
			int resultAmount212 = 1;
			Condition[] array212 = new Condition[1];
			array212[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID199, resultID212, bannerAmount199, resultAmount212, array212);
			int bannerItemID200 = 3451;
			int resultID213 = 959;
			int bannerAmount200 = 1;
			int resultAmount213 = 1;
			Condition[] array213 = new Condition[1];
			array213[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID200, resultID213, bannerAmount200, resultAmount213, array213);
			int bannerItemID201 = 1682;
			int resultID214 = 959;
			int bannerAmount201 = 1;
			int resultAmount214 = 1;
			Condition[] array214 = new Condition[1];
			array214[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID201, resultID214, bannerAmount201, resultAmount214, array214);
			int bannerItemID202 = 2984;
			int resultID215 = 1514;
			int bannerAmount202 = 1;
			int resultAmount215 = 1;
			Condition[] array215 = new Condition[2];
			array215[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array215[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID202, resultID215, bannerAmount202, resultAmount215, array215);
			int bannerItemID203 = 2911;
			int resultID216 = 4018;
			int bannerAmount203 = 1;
			int resultAmount216 = 1;
			Condition[] array216 = new Condition[1];
			array216[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID203, resultID216, bannerAmount203, resultAmount216, array216);
			int bannerItemID204 = 2904;
			int resultID217 = 5042;
			int bannerAmount204 = 1;
			int resultAmount217 = 1;
			Condition[] array217 = new Condition[2];
			array217[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array217[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID204, resultID217, bannerAmount204, resultAmount217, array217);
			int bannerItemID205 = 2924;
			int resultID218 = 4018;
			int bannerAmount205 = 1;
			int resultAmount218 = 1;
			Condition[] array218 = new Condition[2];
			array218[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array218[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID205, resultID218, bannerAmount205, resultAmount218, array218);
			int bannerItemID206 = 2974;
			int resultID219 = 4013;
			int bannerAmount206 = 1;
			int resultAmount219 = 1;
			Condition[] array219 = new Condition[2];
			array219[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array219[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID206, resultID219, bannerAmount206, resultAmount219, array219);
			int bannerItemID207 = 2975;
			int resultID220 = 4013;
			int bannerAmount207 = 1;
			int resultAmount220 = 1;
			Condition[] array220 = new Condition[2];
			array220[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array220[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID207, resultID220, bannerAmount207, resultAmount220, array220);
			int bannerItemID208 = 2984;
			int resultID221 = 4013;
			int bannerAmount208 = 1;
			int resultAmount221 = 1;
			Condition[] array221 = new Condition[2];
			array221[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array221[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID208, resultID221, bannerAmount208, resultAmount221, array221);
			int anyArmoredBonesBanner = Common.AnyArmoredBonesBanner;
			int resultID222 = 1517;
			int resultAmount222 = 1;
			int groupAmount14 = 1;
			Condition[] array222 = new Condition[2];
			array222[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array222[1] = Condition.Hardmode;
			Common.AddBannerGroupToItemRecipe(anyArmoredBonesBanner, resultID222, resultAmount222, groupAmount14, array222);
			int anyArmoredBonesBanner2 = Common.AnyArmoredBonesBanner;
			int resultID223 = 671;
			int resultAmount223 = 1;
			int groupAmount15 = 1;
			Condition[] array223 = new Condition[2];
			array223[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array223[1] = Condition.DownedPlantera;
			Common.AddBannerGroupToItemRecipe(anyArmoredBonesBanner2, resultID223, resultAmount223, groupAmount15, array223);
			int anyArmoredBonesBanner3 = Common.AnyArmoredBonesBanner;
			int resultID224 = 3291;
			int resultAmount224 = 1;
			int groupAmount16 = 1;
			Condition[] array224 = new Condition[2];
			array224[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array224[1] = Condition.DownedPlantera;
			Common.AddBannerGroupToItemRecipe(anyArmoredBonesBanner3, resultID224, resultAmount224, groupAmount16, array224);
			int anyArmoredBonesBanner4 = Common.AnyArmoredBonesBanner;
			int resultID225 = 4679;
			int resultAmount225 = 1;
			int groupAmount17 = 1;
			Condition[] array225 = new Condition[2];
			array225[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array225[1] = Condition.DownedPlantera;
			Common.AddBannerGroupToItemRecipe(anyArmoredBonesBanner4, resultID225, resultAmount225, groupAmount17, array225);
			int anyArmoredBonesBanner5 = Common.AnyArmoredBonesBanner;
			int resultID226 = 1266;
			int resultAmount226 = 1;
			int groupAmount18 = 1;
			Condition[] array226 = new Condition[2];
			array226[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array226[1] = Condition.DownedPlantera;
			Common.AddBannerGroupToItemRecipe(anyArmoredBonesBanner5, resultID226, resultAmount226, groupAmount18, array226);
			int bannerItemID209 = 2924;
			int resultID227 = 4789;
			int bannerAmount209 = 1;
			int resultAmount227 = 1;
			Condition[] array227 = new Condition[2];
			array227[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array227[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID209, resultID227, bannerAmount209, resultAmount227, array227);
			int bannerItemID210 = 2914;
			int resultID228 = 1445;
			int bannerAmount210 = 1;
			int resultAmount228 = 1;
			Condition[] array228 = new Condition[2];
			array228[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array228[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID210, resultID228, bannerAmount210, resultAmount228, array228);
			int bannerItemID211 = 2958;
			int resultID229 = 1513;
			int bannerAmount211 = 1;
			int resultAmount229 = 1;
			Condition[] array229 = new Condition[2];
			array229[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array229[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID211, resultID229, bannerAmount211, resultAmount229, array229);
			int bannerItemID212 = 2956;
			int resultID230 = 1444;
			int bannerAmount212 = 1;
			int resultAmount230 = 1;
			Condition[] array230 = new Condition[2];
			array230[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array230[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID212, resultID230, bannerAmount212, resultAmount230, array230);
			int bannerItemID213 = 2965;
			int resultID231 = 1446;
			int bannerAmount213 = 1;
			int resultAmount231 = 1;
			Condition[] array231 = new Condition[2];
			array231[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array231[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID213, resultID231, bannerAmount213, resultAmount231, array231);
			int bannerItemID214 = 2974;
			int resultID232 = 759;
			int bannerAmount214 = 1;
			int resultAmount232 = 1;
			Condition[] array232 = new Condition[2];
			array232[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array232[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID214, resultID232, bannerAmount214, resultAmount232, array232);
			int bannerItemID215 = 2975;
			int resultID233 = 1254;
			int bannerAmount215 = 1;
			int resultAmount233 = 1;
			Condition[] array233 = new Condition[2];
			array233[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array233[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID215, resultID233, bannerAmount215, resultAmount233, array233);
			int bannerItemID216 = 2984;
			int resultID234 = 679;
			int bannerAmount216 = 1;
			int resultAmount234 = 1;
			Condition[] array234 = new Condition[2];
			array234[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array234[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID216, resultID234, bannerAmount216, resultAmount234, array234);
			int anyArmoredBonesBanner6 = Common.AnyArmoredBonesBanner;
			int resultID235 = 1183;
			int resultAmount235 = 1;
			int groupAmount19 = 1;
			Condition[] array235 = new Condition[2];
			array235[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array235[1] = Condition.DownedPlantera;
			Common.AddBannerGroupToItemRecipe(anyArmoredBonesBanner6, resultID235, resultAmount235, groupAmount19, array235);
			int bannerItemID217 = 1682;
			int resultID236 = 932;
			int bannerAmount217 = 1;
			int resultAmount236 = 1;
			Condition[] array236 = new Condition[1];
			array236[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID217, resultID236, bannerAmount217, resultAmount236, array236);
			int bannerItemID218 = 1701;
			int resultID237 = 216;
			int bannerAmount218 = 1;
			int resultAmount237 = 1;
			Condition[] array237 = new Condition[1];
			array237[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID218, resultID237, bannerAmount218, resultAmount237, array237);
			int bannerItemID219 = 1678;
			int resultID238 = 216;
			int bannerAmount219 = 1;
			int resultAmount238 = 1;
			Condition[] array238 = new Condition[1];
			array238[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID219, resultID238, bannerAmount219, resultAmount238, array238);
			int bannerItemID220 = 1643;
			int resultID239 = 216;
			int bannerAmount220 = 1;
			int resultAmount239 = 1;
			Condition[] array239 = new Condition[1];
			array239[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID220, resultID239, bannerAmount220, resultAmount239, array239);
			int bannerItemID221 = 1695;
			int resultID240 = 485;
			int bannerAmount221 = 1;
			int resultAmount240 = 1;
			Condition[] array240 = new Condition[2];
			array240[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array240[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID221, resultID240, bannerAmount221, resultAmount240, array240);
			int bannerItemID222 = 1695;
			int resultID241 = 885;
			int bannerAmount222 = 1;
			int resultAmount241 = 1;
			Condition[] array241 = new Condition[2];
			array241[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array241[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID222, resultID241, bannerAmount222, resultAmount241, array241);
			int bannerItemID223 = 1699;
			int resultID242 = 889;
			int bannerAmount223 = 1;
			int resultAmount242 = 1;
			Condition[] array242 = new Condition[2];
			array242[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array242[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID223, resultID242, bannerAmount223, resultAmount242, array242);
			int bannerItemID224 = 1701;
			int resultID243 = 1304;
			int bannerAmount224 = 1;
			int resultAmount243 = 1;
			Condition[] array243 = new Condition[1];
			array243[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID224, resultID243, bannerAmount224, resultAmount243, array243);
			int bannerItemID225 = 1690;
			int resultID244 = 1243;
			int bannerAmount225 = 1;
			int resultAmount244 = 1;
			Condition[] array244 = new Condition[1];
			array244[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID225, resultID244, bannerAmount225, resultAmount244, array244);
			int bannerItemID226 = 1678;
			int resultID245 = 1136;
			int bannerAmount226 = 1;
			int resultAmount245 = 1;
			Condition[] array245 = new Condition[1];
			array245[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID226, resultID245, bannerAmount226, resultAmount245, array245);
			int bannerItemID227 = 1678;
			int resultID246 = 1135;
			int bannerAmount227 = 1;
			int resultAmount246 = 1;
			Condition[] array246 = new Condition[1];
			array246[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID227, resultID246, bannerAmount227, resultAmount246, array246);
			int bannerItemID228 = 1646;
			int resultID247 = 4021;
			int bannerAmount228 = 1;
			int resultAmount247 = 1;
			Condition[] array247 = new Condition[1];
			array247[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID228, resultID247, bannerAmount228, resultAmount247, array247);
			int bannerItemID229 = 1616;
			int resultID248 = 1244;
			int bannerAmount229 = 1;
			int resultAmount248 = 1;
			Condition[] array248 = new Condition[2];
			array248[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array248[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID229, resultID248, bannerAmount229, resultAmount248, array248);
			int bannerItemID230 = 1646;
			int resultID249 = 4057;
			int bannerAmount230 = 1;
			int resultAmount249 = 1;
			Condition[] array249 = new Condition[1];
			array249[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID230, resultID249, bannerAmount230, resultAmount249, array249);
			int bannerItemID231 = 1627;
			int resultID250 = 4610;
			int bannerAmount231 = 1;
			int resultAmount250 = 1;
			Condition[] array250 = new Condition[1];
			array250[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID231, resultID250, bannerAmount231, resultAmount250, array250);
			int bannerItemID232 = 1628;
			int resultID251 = 4612;
			int bannerAmount232 = 1;
			int resultAmount251 = 1;
			Condition[] array251 = new Condition[1];
			array251[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID232, resultID251, bannerAmount232, resultAmount251, array251);
			int bannerItemID233 = 1632;
			int resultID252 = 4670;
			int bannerAmount233 = 1;
			int resultAmount252 = 1;
			Condition[] array252 = new Condition[1];
			array252[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID233, resultID252, bannerAmount233, resultAmount252, array252);
			int bannerItemID234 = 4970;
			int resultID253 = 4671;
			int bannerAmount234 = 1;
			int resultAmount253 = 1;
			Condition[] array253 = new Condition[1];
			array253[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID234, resultID253, bannerAmount234, resultAmount253, array253);
			int bannerItemID235 = 1657;
			int resultID254 = 4674;
			int bannerAmount235 = 1;
			int resultAmount254 = 1;
			Condition[] array254 = new Condition[1];
			array254[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID235, resultID254, bannerAmount235, resultAmount254, array254);
			int bannerItemID236 = 1665;
			int resultID255 = 4649;
			int bannerAmount236 = 1;
			int resultAmount255 = 1;
			Condition[] array255 = new Condition[1];
			array255[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID236, resultID255, bannerAmount236, resultAmount255, array255);
			int bannerItemID237 = 1668;
			int resultID256 = 4648;
			int bannerAmount237 = 1;
			int resultAmount256 = 1;
			Condition[] array256 = new Condition[1];
			array256[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID237, resultID256, bannerAmount237, resultAmount256, array256);
			int bannerItemID238 = 3447;
			int resultID257 = 4650;
			int bannerAmount238 = 1;
			int resultAmount257 = 1;
			Condition[] array257 = new Condition[1];
			array257[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID238, resultID257, bannerAmount238, resultAmount257, array257);
			int bannerItemID239 = 2968;
			int resultID258 = 4369;
			int bannerAmount239 = 1;
			int resultAmount258 = 1;
			Condition[] array258 = new Condition[1];
			array258[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID239, resultID258, bannerAmount239, resultAmount258, array258);
			int bannerItemID240 = 1680;
			int resultID259 = 4651;
			int bannerAmount240 = 1;
			int resultAmount259 = 1;
			Condition[] array259 = new Condition[1];
			array259[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID240, resultID259, bannerAmount240, resultAmount259, array259);
			int bannerItemID241 = 1683;
			int resultID260 = 4367;
			int bannerAmount241 = 1;
			int resultAmount260 = 1;
			Condition[] array260 = new Condition[1];
			array260[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID241, resultID260, bannerAmount241, resultAmount260, array260);
			int bannerItemID242 = 2992;
			int resultID261 = 4371;
			int bannerAmount242 = 1;
			int resultAmount261 = 1;
			Condition[] array261 = new Condition[1];
			array261[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID242, resultID261, bannerAmount242, resultAmount261, array261);
			int bannerItemID243 = 2897;
			int resultID262 = 4675;
			int bannerAmount243 = 1;
			int resultAmount262 = 1;
			Condition[] array262 = new Condition[2];
			array262[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array262[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID243, resultID262, bannerAmount243, resultAmount262, array262);
			int bannerItemID244 = 1674;
			int resultID263 = 4613;
			int bannerAmount244 = 1;
			int resultAmount263 = 1;
			Condition[] array263 = new Condition[2];
			array263[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array263[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID244, resultID263, bannerAmount244, resultAmount263, array263);
			int bannerItemID245 = 3789;
			int resultID264 = 4669;
			int bannerAmount245 = 1;
			int resultAmount264 = 1;
			Condition[] array264 = new Condition[2];
			array264[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array264[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID245, resultID264, bannerAmount245, resultAmount264, array264);
			int bannerItemID246 = 1691;
			int resultID265 = 4684;
			int bannerAmount246 = 1;
			int resultAmount265 = 1;
			Condition[] array265 = new Condition[2];
			array265[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array265[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID246, resultID265, bannerAmount246, resultAmount265, array265);
			int bannerItemID247 = 4977;
			int resultID266 = 4683;
			int bannerAmount247 = 1;
			int resultAmount266 = 1;
			Condition[] array266 = new Condition[2];
			array266[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array266[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID247, resultID266, bannerAmount247, resultAmount266, array266);
			int bannerItemID248 = 1697;
			int resultID267 = 4611;
			int bannerAmount248 = 1;
			int resultAmount267 = 1;
			Condition[] array267 = new Condition[2];
			array267[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array267[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID248, resultID267, bannerAmount248, resultAmount267, array267);
			int bannerItemID249 = 1700;
			int resultID268 = 4379;
			int bannerAmount249 = 1;
			int resultAmount268 = 1;
			Condition[] array268 = new Condition[2];
			array268[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array268[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID249, resultID268, bannerAmount249, resultAmount268, array268);
			int bannerItemID250 = 3409;
			int resultID269 = 3212;
			int bannerAmount250 = 1;
			int resultAmount269 = 1;
			Condition[] array269 = new Condition[1];
			array269[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID250, resultID269, bannerAmount250, resultAmount269, array269);
			int bannerItemID251 = 3410;
			int resultID270 = 3212;
			int bannerAmount251 = 1;
			int resultAmount270 = 1;
			Condition[] array270 = new Condition[1];
			array270[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID251, resultID270, bannerAmount251, resultAmount270, array270);
			int bannerItemID252 = 1631;
			int resultID271 = 893;
			int bannerAmount252 = 1;
			int resultAmount271 = 1;
			Condition[] array271 = new Condition[2];
			array271[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array271[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID252, resultID271, bannerAmount252, resultAmount271, array271);
			int bannerItemID253 = 4543;
			int resultID272 = 4381;
			int bannerAmount253 = 1;
			int resultAmount272 = 1;
			Condition[] array272 = new Condition[1];
			array272[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID253, resultID272, bannerAmount253, resultAmount272, array272);
			int bannerItemID254 = 4543;
			int resultID273 = 4273;
			int bannerAmount254 = 1;
			int resultAmount273 = 1;
			Condition[] array273 = new Condition[1];
			array273[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID254, resultID273, bannerAmount254, resultAmount273, array273);
			int bannerItemID255 = 4542;
			int resultID274 = 4381;
			int bannerAmount255 = 1;
			int resultAmount274 = 1;
			Condition[] array274 = new Condition[1];
			array274[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID255, resultID274, bannerAmount255, resultAmount274, array274);
			int bannerItemID256 = 4542;
			int resultID275 = 4273;
			int bannerAmount256 = 1;
			int resultAmount275 = 1;
			Condition[] array275 = new Condition[1];
			array275[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID256, resultID275, bannerAmount256, resultAmount275, array275);
			int bannerItemID257 = 3409;
			int resultID276 = 1314;
			int bannerAmount257 = 1;
			int resultAmount276 = 1;
			Condition[] array276 = new Condition[2];
			array276[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array276[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID257, resultID276, bannerAmount257, resultAmount276, array276);
			int bannerItemID258 = 1631;
			int resultID277 = 1314;
			int bannerAmount258 = 1;
			int resultAmount277 = 1;
			Condition[] array277 = new Condition[2];
			array277[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array277[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID258, resultID277, bannerAmount258, resultAmount277, array277);
			int bannerItemID259 = 4546;
			int resultID278 = 4270;
			int bannerAmount259 = 1;
			int resultAmount278 = 1;
			Condition[] array278 = new Condition[2];
			array278[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array278[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID259, resultID278, bannerAmount259, resultAmount278, array278);
			int bannerItemID260 = 4546;
			int resultID279 = 4317;
			int bannerAmount260 = 1;
			int resultAmount279 = 1;
			Condition[] array279 = new Condition[2];
			array279[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array279[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID260, resultID279, bannerAmount260, resultAmount279, array279);
			int bannerItemID261 = 4545;
			int resultID280 = 4272;
			int bannerAmount261 = 1;
			int resultAmount280 = 1;
			Condition[] array280 = new Condition[2];
			array280[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array280[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID261, resultID280, bannerAmount261, resultAmount280, array280);
			int bannerItemID262 = 4545;
			int resultID281 = 4317;
			int bannerAmount262 = 1;
			int resultAmount281 = 1;
			Condition[] array281 = new Condition[2];
			array281[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array281[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID262, resultID281, bannerAmount262, resultAmount281, array281);
			int bannerItemID263 = 4602;
			int resultID282 = 4269;
			int bannerAmount263 = 1;
			int resultAmount282 = 1;
			Condition[] array282 = new Condition[2];
			array282[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array282[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID263, resultID282, bannerAmount263, resultAmount282, array282);
			int bannerItemID264 = 3410;
			int resultID283 = 3213;
			int bannerAmount264 = 1;
			int resultAmount283 = 1;
			Condition[] array283 = new Condition[1];
			array283[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID264, resultID283, bannerAmount264, resultAmount283, array283);
			int bannerItemID265 = 4543;
			int resultID284 = 4325;
			int bannerAmount265 = 1;
			int resultAmount284 = 1;
			Condition[] array284 = new Condition[1];
			array284[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID265, resultID284, bannerAmount265, resultAmount284, array284);
			int bannerItemID266 = 4542;
			int resultID285 = 4325;
			int bannerAmount266 = 1;
			int resultAmount285 = 1;
			Condition[] array285 = new Condition[1];
			array285[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID266, resultID285, bannerAmount266, resultAmount285, array285);
			int bannerItemID267 = 4602;
			int resultID286 = 4054;
			int bannerAmount267 = 1;
			int resultAmount286 = 1;
			Condition[] array286 = new Condition[2];
			array286[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array286[1] = Condition.Hardmode;
			Common.AddBannerToItemRecipe(bannerItemID267, resultID286, bannerAmount267, resultAmount286, array286);
			int bannerItemID268 = 2927;
			int resultID287 = 160;
			int bannerAmount268 = 1;
			int resultAmount287 = 1;
			Condition[] array287 = new Condition[1];
			array287[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			Common.AddBannerToItemRecipe(bannerItemID268, resultID287, bannerAmount268, resultAmount287, array287);
			int anyPirateBanner = Common.AnyPirateBanner;
			int resultID288 = 854;
			int resultAmount288 = 1;
			int groupAmount20 = 1;
			Condition[] array288 = new Condition[2];
			array288[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array288[1] = Condition.DownedPirates;
			Common.AddBannerGroupToItemRecipe(anyPirateBanner, resultID288, resultAmount288, groupAmount20, array288);
			int anyPirateBanner2 = Common.AnyPirateBanner;
			int resultID289 = 3033;
			int resultAmount289 = 1;
			int groupAmount21 = 1;
			Condition[] array289 = new Condition[2];
			array289[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array289[1] = Condition.DownedPirates;
			Common.AddBannerGroupToItemRecipe(anyPirateBanner2, resultID289, resultAmount289, groupAmount21, array289);
			int anyPirateBanner3 = Common.AnyPirateBanner;
			int resultID290 = 855;
			int resultAmount290 = 1;
			int groupAmount22 = 1;
			Condition[] array290 = new Condition[2];
			array290[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array290[1] = Condition.DownedPirates;
			Common.AddBannerGroupToItemRecipe(anyPirateBanner3, resultID290, resultAmount290, groupAmount22, array290);
			int anyPirateBanner4 = Common.AnyPirateBanner;
			int resultID291 = 1280;
			int resultAmount291 = 1;
			int groupAmount23 = 1;
			Condition[] array291 = new Condition[2];
			array291[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array291[1] = Condition.DownedPirates;
			Common.AddBannerGroupToItemRecipe(anyPirateBanner4, resultID291, resultAmount291, groupAmount23, array291);
			int anyPirateBanner5 = Common.AnyPirateBanner;
			int resultID292 = 1279;
			int resultAmount292 = 1;
			int groupAmount24 = 1;
			Condition[] array292 = new Condition[2];
			array292[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array292[1] = Condition.DownedPirates;
			Common.AddBannerGroupToItemRecipe(anyPirateBanner5, resultID292, resultAmount292, groupAmount24, array292);
			int anyPirateBanner6 = Common.AnyPirateBanner;
			int resultID293 = 1277;
			int resultAmount293 = 1;
			int groupAmount25 = 1;
			Condition[] array293 = new Condition[2];
			array293[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array293[1] = Condition.DownedPirates;
			Common.AddBannerGroupToItemRecipe(anyPirateBanner6, resultID293, resultAmount293, groupAmount25, array293);
			int anyPirateBanner7 = Common.AnyPirateBanner;
			int resultID294 = 1278;
			int resultAmount294 = 1;
			int groupAmount26 = 1;
			Condition[] array294 = new Condition[2];
			array294[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array294[1] = Condition.DownedPirates;
			Common.AddBannerGroupToItemRecipe(anyPirateBanner7, resultID294, resultAmount294, groupAmount26, array294);
			int anyPirateBanner8 = Common.AnyPirateBanner;
			int resultID295 = 3265;
			int resultAmount295 = 1;
			int groupAmount27 = 1;
			Condition[] array295 = new Condition[2];
			array295[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array295[1] = Condition.DownedPirates;
			Common.AddBannerGroupToItemRecipe(anyPirateBanner8, resultID295, resultAmount295, groupAmount27, array295);
			int anyPirateBanner9 = Common.AnyPirateBanner;
			int resultID296 = 3264;
			int resultAmount296 = 1;
			int groupAmount28 = 1;
			Condition[] array296 = new Condition[2];
			array296[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array296[1] = Condition.DownedPirates;
			Common.AddBannerGroupToItemRecipe(anyPirateBanner9, resultID296, resultAmount296, groupAmount28, array296);
			int anyPirateBanner10 = Common.AnyPirateBanner;
			int resultID297 = 3263;
			int resultAmount297 = 1;
			int groupAmount29 = 1;
			Condition[] array297 = new Condition[2];
			array297[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array297[1] = Condition.DownedPirates;
			Common.AddBannerGroupToItemRecipe(anyPirateBanner10, resultID297, resultAmount297, groupAmount29, array297);
			int anyPirateBanner11 = Common.AnyPirateBanner;
			int resultID298 = 672;
			int resultAmount298 = 1;
			int groupAmount30 = 1;
			Condition[] array298 = new Condition[2];
			array298[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array298[1] = Condition.DownedPirates;
			Common.AddBannerGroupToItemRecipe(anyPirateBanner11, resultID298, resultAmount298, groupAmount30, array298);
			int anyPirateBanner12 = Common.AnyPirateBanner;
			int resultID299 = 2584;
			int resultAmount299 = 1;
			int groupAmount31 = 1;
			Condition[] array299 = new Condition[2];
			array299[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array299[1] = Condition.DownedPirates;
			Common.AddBannerGroupToItemRecipe(anyPirateBanner12, resultID299, resultAmount299, groupAmount31, array299);
			int bannerItemID269 = 3441;
			int resultID300 = 905;
			int bannerAmount269 = 1;
			int resultAmount300 = 1;
			Condition[] array300 = new Condition[2];
			array300[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array300[1] = Condition.DownedPirates;
			Common.AddBannerToItemRecipe(bannerItemID269, resultID300, bannerAmount269, resultAmount300, array300);
			int bannerItemID270 = 3395;
			int resultID301 = 497;
			int bannerAmount270 = 1;
			int resultAmount301 = 1;
			Condition[] array301 = new Condition[2];
			array301[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array301[1] = Condition.DownedMechBossAny;
			Common.AddBannerToItemRecipe(bannerItemID270, resultID301, bannerAmount270, resultAmount301, array301);
			int bannerItemID271 = 1692;
			int resultID302 = 900;
			int bannerAmount271 = 1;
			int resultAmount302 = 1;
			Condition[] array302 = new Condition[2];
			array302[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array302[1] = Condition.DownedMechBossAny;
			Common.AddBannerToItemRecipe(bannerItemID271, resultID302, bannerAmount271, resultAmount302, array302);
			int bannerItemID272 = 3397;
			int resultID303 = 2770;
			int bannerAmount272 = 1;
			int resultAmount303 = 1;
			Condition[] array303 = new Condition[2];
			array303[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array303[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID272, resultID303, bannerAmount272, resultAmount303, array303);
			int bannerItemID273 = 3400;
			int resultID304 = 4742;
			int bannerAmount273 = 1;
			int resultAmount304 = 1;
			Condition[] array304 = new Condition[2];
			array304[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array304[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID273, resultID304, bannerAmount273, resultAmount304, array304);
			int bannerItemID274 = 3400;
			int resultID305 = 4741;
			int bannerAmount274 = 1;
			int resultAmount305 = 1;
			Condition[] array305 = new Condition[2];
			array305[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array305[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID274, resultID305, bannerAmount274, resultAmount305, array305);
			int bannerItemID275 = 3400;
			int resultID306 = 4742;
			int bannerAmount275 = 1;
			int resultAmount306 = 1;
			Condition[] array306 = new Condition[2];
			array306[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array306[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID275, resultID306, bannerAmount275, resultAmount306, array306);
			int bannerItemID276 = 3396;
			int resultID307 = 4739;
			int bannerAmount276 = 1;
			int resultAmount307 = 1;
			Condition[] array307 = new Condition[2];
			array307[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array307[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID276, resultID307, bannerAmount276, resultAmount307, array307);
			int bannerItemID277 = 3396;
			int resultID308 = 4738;
			int bannerAmount277 = 1;
			int resultAmount308 = 1;
			Condition[] array308 = new Condition[2];
			array308[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array308[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID277, resultID308, bannerAmount277, resultAmount308, array308);
			int bannerItemID278 = 3399;
			int resultID309 = 4037;
			int bannerAmount278 = 1;
			int resultAmount309 = 1;
			Condition[] array309 = new Condition[2];
			array309[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array309[1] = Condition.DownedMechBossAny;
			Common.AddBannerToItemRecipe(bannerItemID278, resultID309, bannerAmount278, resultAmount309, array309);
			int bannerItemID279 = 1692;
			int resultID310 = 1520;
			int bannerAmount279 = 1;
			int resultAmount310 = 1;
			Condition[] array310 = new Condition[2];
			array310[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array310[1] = Condition.DownedMechBossAny;
			Common.AddBannerToItemRecipe(bannerItemID279, resultID310, bannerAmount279, resultAmount310, array310);
			int bannerItemID280 = 3397;
			int resultID311 = 1570;
			int bannerAmount280 = 1;
			int resultAmount311 = 1;
			Condition[] array311 = new Condition[2];
			array311[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array311[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID280, resultID311, bannerAmount280, resultAmount311, array311);
			int bannerItemID281 = 1679;
			int resultID312 = 1327;
			int bannerAmount281 = 1;
			int resultAmount312 = 1;
			Condition[] array312 = new Condition[2];
			array312[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array312[1] = Condition.DownedMechBossAll;
			Common.AddBannerToItemRecipe(bannerItemID281, resultID312, bannerAmount281, resultAmount312, array312);
			int bannerItemID282 = 3400;
			int resultID313 = 3098;
			int bannerAmount282 = 1;
			int resultAmount313 = 1;
			Condition[] array313 = new Condition[2];
			array313[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array313[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID282, resultID313, bannerAmount282, resultAmount313, array313);
			int bannerItemID283 = 3402;
			int resultID314 = 3249;
			int bannerAmount283 = 1;
			int resultAmount314 = 1;
			Condition[] array314 = new Condition[2];
			array314[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array314[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID283, resultID314, bannerAmount283, resultAmount314, array314);
			int bannerItemID284 = 3396;
			int resultID315 = 3105;
			int bannerAmount284 = 1;
			int resultAmount315 = 1;
			Condition[] array315 = new Condition[2];
			array315[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array315[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID284, resultID315, bannerAmount284, resultAmount315, array315);
			int bannerItemID285 = 3397;
			int resultID316 = 3292;
			int bannerAmount285 = 1;
			int resultAmount316 = 1;
			Condition[] array316 = new Condition[2];
			array316[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array316[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID285, resultID316, bannerAmount285, resultAmount316, array316);
			int bannerItemID286 = 3403;
			int resultID317 = 3107;
			int bannerAmount286 = 1;
			int resultAmount317 = 1;
			Condition[] array317 = new Condition[2];
			array317[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array317[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID286, resultID317, bannerAmount286, resultAmount317, array317);
			int bannerItemID287 = 3401;
			int resultID318 = 3106;
			int bannerAmount287 = 1;
			int resultAmount318 = 1;
			Condition[] array318 = new Condition[2];
			array318[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array318[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID287, resultID318, bannerAmount287, resultAmount318, array318);
			int bannerItemID288 = 2920;
			int resultID319 = 1311;
			int bannerAmount288 = 1;
			int resultAmount319 = 1;
			Condition[] array319 = new Condition[2];
			array319[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array319[1] = Condition.DownedMechBossAny;
			Common.AddBannerToItemRecipe(bannerItemID288, resultID319, bannerAmount288, resultAmount319, array319);
			int bannerItemID289 = 2971;
			int resultID320 = 1790;
			int bannerAmount289 = 1;
			int resultAmount320 = 1;
			Condition[] array320 = new Condition[2];
			array320[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array320[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID289, resultID320, bannerAmount289, resultAmount320, array320);
			int bannerItemID290 = 2971;
			int resultID321 = 1789;
			int bannerAmount290 = 1;
			int resultAmount321 = 1;
			Condition[] array321 = new Condition[2];
			array321[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array321[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID290, resultID321, bannerAmount290, resultAmount321, array321);
			int bannerItemID291 = 2971;
			int resultID322 = 1788;
			int bannerAmount291 = 1;
			int resultAmount322 = 1;
			Condition[] array322 = new Condition[2];
			array322[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array322[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID291, resultID322, bannerAmount291, resultAmount322, array322);
			int bannerItemID292 = 2929;
			int resultID323 = 1857;
			int bannerAmount292 = 1;
			int resultAmount323 = 1;
			Condition[] array323 = new Condition[2];
			array323[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array323[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID292, resultID323, bannerAmount292, resultAmount323, array323);
			int bannerItemID293 = 2994;
			int resultID324 = 1945;
			int bannerAmount293 = 1;
			int resultAmount324 = 1;
			Condition[] array324 = new Condition[2];
			array324[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array324[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID293, resultID324, bannerAmount293, resultAmount324, array324);
			int bannerItemID294 = 2994;
			int resultID325 = 1944;
			int bannerAmount294 = 1;
			int resultAmount325 = 1;
			Condition[] array325 = new Condition[2];
			array325[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array325[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID294, resultID325, bannerAmount294, resultAmount325, array325);
			int bannerItemID295 = 2994;
			int resultID326 = 1943;
			int bannerAmount295 = 1;
			int resultAmount326 = 1;
			Condition[] array326 = new Condition[2];
			array326[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array326[1] = Condition.DownedPlantera;
			Common.AddBannerToItemRecipe(bannerItemID295, resultID326, bannerAmount295, resultAmount326, array326);
			int bannerItemID296 = 2949;
			int resultID327 = 2805;
			int bannerAmount296 = 1;
			int resultAmount327 = 1;
			Condition[] array327 = new Condition[2];
			array327[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array327[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID296, resultID327, bannerAmount296, resultAmount327, array327);
			int bannerItemID297 = 2949;
			int resultID328 = 2804;
			int bannerAmount297 = 1;
			int resultAmount328 = 1;
			Condition[] array328 = new Condition[2];
			array328[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array328[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID297, resultID328, bannerAmount297, resultAmount328, array328);
			int bannerItemID298 = 2949;
			int resultID329 = 2803;
			int bannerAmount298 = 1;
			int resultAmount329 = 1;
			Condition[] array329 = new Condition[2];
			array329[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array329[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID298, resultID329, bannerAmount298, resultAmount329, array329);
			int bannerItemID299 = 2951;
			int resultID330 = 2805;
			int bannerAmount299 = 1;
			int resultAmount330 = 1;
			Condition[] array330 = new Condition[2];
			array330[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array330[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID299, resultID330, bannerAmount299, resultAmount330, array330);
			int bannerItemID300 = 2951;
			int resultID331 = 2804;
			int bannerAmount300 = 1;
			int resultAmount331 = 1;
			Condition[] array331 = new Condition[2];
			array331[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array331[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID300, resultID331, bannerAmount300, resultAmount331, array331);
			int bannerItemID301 = 2951;
			int resultID332 = 2803;
			int bannerAmount301 = 1;
			int resultAmount332 = 1;
			Condition[] array332 = new Condition[2];
			array332[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array332[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID301, resultID332, bannerAmount301, resultAmount332, array332);
			int bannerItemID302 = 2945;
			int resultID333 = 2805;
			int bannerAmount302 = 1;
			int resultAmount333 = 1;
			Condition[] array333 = new Condition[2];
			array333[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array333[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID302, resultID333, bannerAmount302, resultAmount333, array333);
			int bannerItemID303 = 2945;
			int resultID334 = 2804;
			int bannerAmount303 = 1;
			int resultAmount334 = 1;
			Condition[] array334 = new Condition[2];
			array334[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array334[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID303, resultID334, bannerAmount303, resultAmount334, array334);
			int bannerItemID304 = 2945;
			int resultID335 = 2803;
			int bannerAmount304 = 1;
			int resultAmount335 = 1;
			Condition[] array335 = new Condition[2];
			array335[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array335[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID304, resultID335, bannerAmount304, resultAmount335, array335);
			int bannerItemID305 = 2950;
			int resultID336 = 2808;
			int bannerAmount305 = 1;
			int resultAmount336 = 1;
			Condition[] array336 = new Condition[2];
			array336[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array336[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID305, resultID336, bannerAmount305, resultAmount336, array336);
			int bannerItemID306 = 2950;
			int resultID337 = 2807;
			int bannerAmount306 = 1;
			int resultAmount337 = 1;
			Condition[] array337 = new Condition[2];
			array337[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array337[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID306, resultID337, bannerAmount306, resultAmount337, array337);
			int bannerItemID307 = 2950;
			int resultID338 = 2806;
			int bannerAmount307 = 1;
			int resultAmount338 = 1;
			Condition[] array338 = new Condition[2];
			array338[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array338[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID307, resultID338, bannerAmount307, resultAmount338, array338);
			int bannerItemID308 = 2947;
			int resultID339 = 2808;
			int bannerAmount308 = 1;
			int resultAmount339 = 1;
			Condition[] array339 = new Condition[2];
			array339[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array339[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID308, resultID339, bannerAmount308, resultAmount339, array339);
			int bannerItemID309 = 2947;
			int resultID340 = 2807;
			int bannerAmount309 = 1;
			int resultAmount340 = 1;
			Condition[] array340 = new Condition[2];
			array340[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array340[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID309, resultID340, bannerAmount309, resultAmount340, array340);
			int bannerItemID310 = 2947;
			int resultID341 = 2806;
			int bannerAmount310 = 1;
			int resultAmount341 = 1;
			Condition[] array341 = new Condition[2];
			array341[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array341[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID310, resultID341, bannerAmount310, resultAmount341, array341);
			int bannerItemID311 = 2948;
			int resultID342 = 2808;
			int bannerAmount311 = 1;
			int resultAmount342 = 1;
			Condition[] array342 = new Condition[2];
			array342[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array342[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID311, resultID342, bannerAmount311, resultAmount342, array342);
			int bannerItemID312 = 2948;
			int resultID343 = 2807;
			int bannerAmount312 = 1;
			int resultAmount343 = 1;
			Condition[] array343 = new Condition[2];
			array343[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array343[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID312, resultID343, bannerAmount312, resultAmount343, array343);
			int bannerItemID313 = 2948;
			int resultID344 = 2806;
			int bannerAmount313 = 1;
			int resultAmount344 = 1;
			Condition[] array344 = new Condition[2];
			array344[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array344[1] = Condition.DownedGolem;
			Common.AddBannerToItemRecipe(bannerItemID313, resultID344, bannerAmount313, resultAmount344, array344);
			int bannerItemID314 = 2952;
			int resultID345 = 2771;
			int bannerAmount314 = 1;
			int resultAmount345 = 1;
			Condition[] array345 = new Condition[2];
			array345[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array345[1] = Condition.DownedMartians;
			Common.AddBannerToItemRecipe(bannerItemID314, resultID345, bannerAmount314, resultAmount345, array345);
			int bannerItemID315 = 2972;
			int resultID346 = 2771;
			int bannerAmount315 = 1;
			int resultAmount346 = 1;
			Condition[] array346 = new Condition[2];
			array346[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
			array346[1] = Condition.DownedMartians;
			Common.AddBannerToItemRecipe(bannerItemID315, resultID346, bannerAmount315, resultAmount346, array346);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00063D84 File Offset: 0x00061F84
		public static void GrabBagRecipes()
		{
			int ingredientID = 1774;
			int resultID = 1810;
			int tileID = 18;
			int ingredientAmount = 5;
			int resultAmount = 1;
			bool disableDecraft = true;
			bool usesRecipeGroup = false;
			Condition[] array = new Condition[1];
			array[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", () => QoLCompendium.mainConfig.CrateRecipes);
			Common.CreateSimpleRecipe(ingredientID, resultID, tileID, ingredientAmount, resultAmount, disableDecraft, usesRecipeGroup, array);
			int ingredientID2 = 1774;
			int resultID2 = 1800;
			int tileID2 = 18;
			int ingredientAmount2 = 5;
			int resultAmount2 = 1;
			bool disableDecraft2 = true;
			bool usesRecipeGroup2 = false;
			Condition[] array2 = new Condition[1];
			array2[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", () => QoLCompendium.mainConfig.CrateRecipes);
			Common.CreateSimpleRecipe(ingredientID2, resultID2, tileID2, ingredientAmount2, resultAmount2, disableDecraft2, usesRecipeGroup2, array2);
			int ingredientID3 = 1774;
			int resultID3 = 1809;
			int tileID3 = 18;
			int ingredientAmount3 = 1;
			int resultAmount3 = 50;
			bool disableDecraft3 = true;
			bool usesRecipeGroup3 = false;
			Condition[] array3 = new Condition[1];
			array3[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", () => QoLCompendium.mainConfig.CrateRecipes);
			Common.CreateSimpleRecipe(ingredientID3, resultID3, tileID3, ingredientAmount3, resultAmount3, disableDecraft3, usesRecipeGroup3, array3);
			int ingredientID4 = 1869;
			int resultID4 = 602;
			int tileID4 = 18;
			int ingredientAmount4 = 5;
			int resultAmount4 = 1;
			bool disableDecraft4 = true;
			bool usesRecipeGroup4 = false;
			Condition[] array4 = new Condition[2];
			array4[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", () => QoLCompendium.mainConfig.CrateRecipes);
			array4[1] = Condition.Hardmode;
			Common.CreateSimpleRecipe(ingredientID4, resultID4, tileID4, ingredientAmount4, resultAmount4, disableDecraft4, usesRecipeGroup4, array4);
			int ingredientID5 = 1869;
			int resultID5 = 1927;
			int tileID5 = 18;
			int ingredientAmount5 = 5;
			int resultAmount5 = 1;
			bool disableDecraft5 = true;
			bool usesRecipeGroup5 = false;
			Condition[] array5 = new Condition[1];
			array5[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", () => QoLCompendium.mainConfig.CrateRecipes);
			Common.CreateSimpleRecipe(ingredientID5, resultID5, tileID5, ingredientAmount5, resultAmount5, disableDecraft5, usesRecipeGroup5, array5);
			int ingredientID6 = 1869;
			int resultID6 = 1870;
			int tileID6 = 18;
			int ingredientAmount6 = 5;
			int resultAmount6 = 1;
			bool disableDecraft6 = true;
			bool usesRecipeGroup6 = false;
			Condition[] array6 = new Condition[1];
			array6[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", () => QoLCompendium.mainConfig.CrateRecipes);
			Common.CreateSimpleRecipe(ingredientID6, resultID6, tileID6, ingredientAmount6, resultAmount6, disableDecraft6, usesRecipeGroup6, array6);
			int ingredientID7 = 1869;
			int resultID7 = 1909;
			int tileID7 = 18;
			int ingredientAmount7 = 5;
			int resultAmount7 = 1;
			bool disableDecraft7 = true;
			bool usesRecipeGroup7 = false;
			Condition[] array7 = new Condition[1];
			array7[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", () => QoLCompendium.mainConfig.CrateRecipes);
			Common.CreateSimpleRecipe(ingredientID7, resultID7, tileID7, ingredientAmount7, resultAmount7, disableDecraft7, usesRecipeGroup7, array7);
			int ingredientID8 = 1869;
			int resultID8 = 1917;
			int tileID8 = 18;
			int ingredientAmount8 = 5;
			int resultAmount8 = 1;
			bool disableDecraft8 = true;
			bool usesRecipeGroup8 = false;
			Condition[] array8 = new Condition[1];
			array8[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", () => QoLCompendium.mainConfig.CrateRecipes);
			Common.CreateSimpleRecipe(ingredientID8, resultID8, tileID8, ingredientAmount8, resultAmount8, disableDecraft8, usesRecipeGroup8, array8);
			int ingredientID9 = 1869;
			int resultID9 = 1915;
			int tileID9 = 18;
			int ingredientAmount9 = 5;
			int resultAmount9 = 1;
			bool disableDecraft9 = true;
			bool usesRecipeGroup9 = false;
			Condition[] array9 = new Condition[1];
			array9[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", () => QoLCompendium.mainConfig.CrateRecipes);
			Common.CreateSimpleRecipe(ingredientID9, resultID9, tileID9, ingredientAmount9, resultAmount9, disableDecraft9, usesRecipeGroup9, array9);
			int ingredientID10 = 1869;
			int resultID10 = 1918;
			int tileID10 = 18;
			int ingredientAmount10 = 5;
			int resultAmount10 = 1;
			bool disableDecraft10 = true;
			bool usesRecipeGroup10 = false;
			Condition[] array10 = new Condition[1];
			array10[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", () => QoLCompendium.mainConfig.CrateRecipes);
			Common.CreateSimpleRecipe(ingredientID10, resultID10, tileID10, ingredientAmount10, resultAmount10, disableDecraft10, usesRecipeGroup10, array10);
			int ingredientID11 = 1869;
			int resultID11 = 1921;
			int tileID11 = 18;
			int ingredientAmount11 = 5;
			int resultAmount11 = 1;
			bool disableDecraft11 = true;
			bool usesRecipeGroup11 = false;
			Condition[] array11 = new Condition[1];
			array11[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", () => QoLCompendium.mainConfig.CrateRecipes);
			Common.CreateSimpleRecipe(ingredientID11, resultID11, tileID11, ingredientAmount11, resultAmount11, disableDecraft11, usesRecipeGroup11, array11);
			int ingredientID12 = 1869;
			int resultID12 = 1923;
			int tileID12 = 18;
			int ingredientAmount12 = 5;
			int resultAmount12 = 1;
			bool disableDecraft12 = true;
			bool usesRecipeGroup12 = false;
			Condition[] array12 = new Condition[1];
			array12[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", () => QoLCompendium.mainConfig.CrateRecipes);
			Common.CreateSimpleRecipe(ingredientID12, resultID12, tileID12, ingredientAmount12, resultAmount12, disableDecraft12, usesRecipeGroup12, array12);
			int ingredientID13 = 1869;
			int resultID13 = 1913;
			int tileID13 = 18;
			int ingredientAmount13 = 1;
			int resultAmount13 = 50;
			bool disableDecraft13 = true;
			bool usesRecipeGroup13 = false;
			Condition[] array13 = new Condition[1];
			array13[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", () => QoLCompendium.mainConfig.CrateRecipes);
			Common.CreateSimpleRecipe(ingredientID13, resultID13, tileID13, ingredientAmount13, resultAmount13, disableDecraft13, usesRecipeGroup13, array13);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00064138 File Offset: 0x00062338
		public override void AddRecipeGroups()
		{
			RecipeAdditions.<>c__DisplayClass16_0 CS$<>8__locals1 = new RecipeAdditions.<>c__DisplayClass16_0();
			CS$<>8__locals1.allItems = Enumerable.Range(0, ItemLoader.ItemCount);
			CS$<>8__locals1.any = Language.GetTextValue("LegacyMisc.37");
			List<int> chairItems = new List<int>();
			List<int> doorItems = new List<int>();
			List<int> torchItems = new List<int>();
			List<int> wellFedItems = new List<int>();
			List<int> plentySatisfiedItems = new List<int>();
			List<int> exquisitelyStuffedItems = new List<int>();
			List<int> gourmetFlavorItems = new List<int>();
			for (int i = 0; i < TextureAssets.Item.Length; i++)
			{
				Item item = new Item();
				item.SetDefaults(i);
				if (item.buffType == 26)
				{
					wellFedItems.Add(item.type);
				}
				else if (item.buffType == 206)
				{
					plentySatisfiedItems.Add(item.type);
				}
				else if (item.buffType == 207)
				{
					exquisitelyStuffedItems.Add(item.type);
				}
				else if (ModConditions.martainsOrderLoaded && item.buffType == Common.GetModBuff(ModConditions.martainsOrderMod, "Gourmet"))
				{
					gourmetFlavorItems.Add(item.type);
				}
				if (item.consumable && item.createTile >= 0 && (item.ModItem == null || item.ModItem.Mod != base.Mod))
				{
					if (TileID.Sets.RoomNeeds.CountsAsChair.Contains(item.createTile))
					{
						chairItems.Add(item.type);
					}
					if (TileID.Sets.RoomNeeds.CountsAsDoor.Contains(item.createTile))
					{
						doorItems.Add(item.type);
					}
					if (TileID.Sets.RoomNeeds.CountsAsTorch.Contains(item.createTile))
					{
						torchItems.Add(item.type);
					}
				}
			}
			int[] anvilItems = new int[]
			{
				35,
				716
			};
			if (ModConditions.exxoAvalonOriginsLoaded)
			{
				Array.Resize<int>(ref anvilItems, anvilItems.Length + 1);
				int[] array = anvilItems;
				array[array.Length - 1] = Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "NickelAnvil");
			}
			int[] hardmodeAnvilItems = new int[]
			{
				525,
				1220
			};
			if (ModConditions.exxoAvalonOriginsLoaded)
			{
				Array.Resize<int>(ref hardmodeAnvilItems, hardmodeAnvilItems.Length + 1);
				int[] array2 = hardmodeAnvilItems;
				array2[array2.Length - 1] = Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "NaquadahAnvil");
			}
			int[] hardmodeForgeItems = new int[]
			{
				524,
				1221
			};
			if (ModConditions.exxoAvalonOriginsLoaded)
			{
				Array.Resize<int>(ref hardmodeForgeItems, hardmodeForgeItems.Length + 1);
				int[] array3 = hardmodeForgeItems;
				array3[array3.Length - 1] = Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "TroxiniumForge");
			}
			int[] altarItems = new int[]
			{
				ModContent.ItemType<DemonAltar>(),
				ModContent.ItemType<CrimsonAltar>()
			};
			if (ModConditions.exxoAvalonOriginsLoaded)
			{
				Array.Resize<int>(ref altarItems, altarItems.Length + 3);
				int[] array4 = altarItems;
				array4[array4.Length - 3] = Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "DemonAltar");
				int[] array5 = altarItems;
				array5[array5.Length - 2] = Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "CrimsonAltar");
				int[] array6 = altarItems;
				array6[array6.Length - 1] = Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "IckyAltar");
			}
			if (ModConditions.fargosMutantLoaded)
			{
				Array.Resize<int>(ref altarItems, altarItems.Length + 2);
				int[] array7 = altarItems;
				array7[array7.Length - 2] = Common.GetModItem(ModConditions.fargosMutantMod, "DemonAltar");
				int[] array8 = altarItems;
				array8[array8.Length - 1] = Common.GetModItem(ModConditions.fargosMutantMod, "CrimsonAltar");
			}
			if (ModConditions.luiAFKLoaded)
			{
				Array.Resize<int>(ref altarItems, altarItems.Length + 2);
				int[] array9 = altarItems;
				array9[array9.Length - 2] = Common.GetModItem(ModConditions.luiAFKMod, "CorruptionAltar");
				int[] array10 = altarItems;
				array10[array10.Length - 1] = Common.GetModItem(ModConditions.luiAFKMod, "CrimsonAltar");
			}
			if (ModConditions.magicStorageLoaded)
			{
				Array.Resize<int>(ref altarItems, altarItems.Length + 2);
				int[] array11 = altarItems;
				array11[array11.Length - 2] = Common.GetModItem(ModConditions.magicStorageMod, "DemonAltar");
				int[] array12 = altarItems;
				array12[array12.Length - 1] = Common.GetModItem(ModConditions.magicStorageMod, "CrimsonAltar");
			}
			if (ModConditions.martainsOrderLoaded)
			{
				Array.Resize<int>(ref altarItems, altarItems.Length + 1);
				int[] array13 = altarItems;
				array13[array13.Length - 1] = Common.GetModItem(ModConditions.martainsOrderMod, "DemonAltar");
			}
			if (ModConditions.thoriumLoaded)
			{
				Array.Resize<int>(ref altarItems, altarItems.Length + 2);
				int[] array14 = altarItems;
				array14[array14.Length - 2] = Common.GetModItem(ModConditions.thoriumMod, "GrimPedestal");
				int[] array15 = altarItems;
				array15[array15.Length - 1] = Common.GetModItem(ModConditions.thoriumMod, "GrimPedestalCrimson");
			}
			RecipeGroup anvils = new RecipeGroup(() => CS$<>8__locals1.any + " " + Lang.GetItemNameValue(35), anvilItems);
			RecipeGroup.RegisterGroup("QoLCompendium:Anvils", anvils);
			RecipeGroup hmAnvils = new RecipeGroup(() => CS$<>8__locals1.any + " " + Lang.GetItemNameValue(525), hardmodeAnvilItems);
			RecipeGroup.RegisterGroup("QoLCompendium:HardmodeAnvils", hmAnvils);
			RecipeGroup hmForges = new RecipeGroup(() => CS$<>8__locals1.any + " " + Lang.GetItemNameValue(524), hardmodeForgeItems);
			RecipeGroup.RegisterGroup("QoLCompendium:HardmodeForges", hmForges);
			RecipeGroup altars = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("MapObject.DemonAltar"), altarItems);
			RecipeGroup.RegisterGroup("QoLCompendium:Altars", altars);
			int[] items = CS$<>8__locals1.<AddRecipeGroups>g__GetItems|0(48, RecipeAdditions.chestItemRegex, Array.Empty<int>());
			RecipeGroup chests = new RecipeGroup(() => CS$<>8__locals1.any + " " + Lang.GetItemNameValue(48), items);
			RecipeGroup.RegisterGroup("QoLCompendium:AnyChest", chests);
			RecipeAdditions.RegisterGroupClone(chests, "Chest");
			items = CS$<>8__locals1.<AddRecipeGroups>g__GetItems|0(36, RecipeAdditions.workBenchItemRegex, new int[]
			{
				2172
			});
			RecipeGroup workBenches = new RecipeGroup(() => CS$<>8__locals1.any + " " + Lang.GetItemNameValue(36), items);
			RecipeGroup.RegisterGroup("QoLCompendium:AnyWorkBench", workBenches);
			RecipeAdditions.RegisterGroupClone(workBenches, "WorkBench");
			items = new int[]
			{
				31,
				350,
				351,
				2234,
				2244,
				2257,
				2258
			};
			RecipeGroup bottles = new RecipeGroup(() => CS$<>8__locals1.any + " " + Lang.GetItemNameValue(31), items);
			RecipeGroup.RegisterGroup("QoLCompendium:AnyBottle", bottles);
			RecipeAdditions.RegisterGroupClone(bottles, "Bottle");
			items = CS$<>8__locals1.<AddRecipeGroups>g__GetItems|0(2841, RecipeAdditions.sinkItemRegex, Array.Empty<int>());
			RecipeGroup sinks = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.Sinks"), items);
			RecipeGroup.RegisterGroup("QoLCompendium:AnySink", sinks);
			RecipeAdditions.RegisterGroupClone(sinks, "MetalSink");
			items = CS$<>8__locals1.<AddRecipeGroups>g__GetItems|0(32, RecipeAdditions.tableItemRegex, new int[]
			{
				2999,
				3000,
				3814
			});
			RecipeGroup tables = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.Tables"), items);
			RecipeGroup.RegisterGroup("QoLCompendium:AnyTable", tables);
			RecipeAdditions.RegisterGroupClone(tables, "WoodenTable");
			items = CS$<>8__locals1.<AddRecipeGroups>g__GetItems|0(34, RecipeAdditions.chairItemRegex, Array.Empty<int>());
			RecipeGroup chairs = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.Chairs"), items);
			RecipeGroup.RegisterGroup("QoLCompendium:AnyChair", chairs);
			RecipeAdditions.RegisterGroupClone(chairs, "WoodenChair");
			RecipeGroup doors = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.Doors"), doorItems.ToArray());
			RecipeGroup.RegisterGroup("QoLCompendium:AnyDoor", doors);
			RecipeAdditions.RegisterGroupClone(doors, "WoodenDoor");
			RecipeGroup torches = new RecipeGroup(() => CS$<>8__locals1.any + " " + Lang.GetItemNameValue(8), torchItems.ToArray());
			RecipeGroup.RegisterGroup("QoLCompendium:AnyTorch", torches);
			RecipeAdditions.RegisterGroupClone(torches, "Torch");
			items = new int[]
			{
				345,
				1791
			};
			RecipeGroup cookingPots = new RecipeGroup(() => CS$<>8__locals1.any + " " + Lang.GetItemNameValue(345), items);
			RecipeGroup.RegisterGroup("QoLCompendium:AnyCookingPot", cookingPots);
			RecipeAdditions.RegisterGroupClone(cookingPots, "CookingPot");
			items = CS$<>8__locals1.<AddRecipeGroups>g__GetItems|0(354, RecipeAdditions.bookcaseItemRegex, Array.Empty<int>());
			RecipeGroup bookcases = new RecipeGroup(() => CS$<>8__locals1.any + " " + Lang.GetItemNameValue(354), items);
			RecipeGroup.RegisterGroup("QoLCompendium:AnyBookcase", bookcases);
			RecipeAdditions.RegisterGroupClone(bookcases, "Bookcase");
			RecipeGroup tombstones = new RecipeGroup(() => CS$<>8__locals1.any + " " + Lang.GetItemNameValue(321), new int[]
			{
				321,
				1173,
				1174,
				1175,
				1176,
				1177,
				3229,
				3230,
				3231,
				3232,
				3233
			});
			RecipeGroup.RegisterGroup("QoLCompendium:AnyTombstone", tombstones);
			RecipeGroup goldBars = new RecipeGroup(() => CS$<>8__locals1.any + " " + Lang.GetItemNameValue(19), new int[]
			{
				19,
				706
			});
			RecipeGroup.RegisterGroup("QoLCompendium:GoldBars", goldBars);
			RecipeGroup pirateBanners = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyPirateBanner"), new int[]
			{
				3442,
				3443,
				3444,
				1676
			});
			Common.AnyPirateBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyPirateBanner", pirateBanners);
			RecipeGroup armoredBonesBanners = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyArmoredBonesBanner"), new int[]
			{
				2900,
				2930,
				2970
			});
			Common.AnyArmoredBonesBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyArmoredBonesBanner", armoredBonesBanners);
			RecipeGroup slimeBanners = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnySlimeBanner"), new int[]
			{
				1683,
				2928,
				2968,
				2964,
				2992,
				2899,
				2935,
				3593,
				2940,
				2980,
				2981,
				2955,
				1690,
				1689,
				2908,
				2976,
				2910,
				1651,
				2938,
				2966
			});
			Common.AnySlimeBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnySlimeBanner", slimeBanners);
			RecipeGroup hallowBanners = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyHallowBanner"), new int[]
			{
				1677,
				1691,
				2966,
				1651,
				3450,
				2937,
				2938,
				1629,
				1642,
				4975
			});
			Common.AnyHallowBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyHallowBanner", hallowBanners);
			RecipeGroup corruptionBanners = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyCorruptionBanner"), new int[]
			{
				1641,
				2909,
				2908,
				2976,
				2913,
				1697,
				3449,
				1637,
				2905,
				4973
			});
			Common.AnyCorruptionBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyCorruptionBanner", corruptionBanners);
			RecipeGroup crimsonBanners = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyCrimsonBanner"), new int[]
			{
				1626,
				1644,
				1635,
				1660,
				2910,
				1625,
				1624,
				4966,
				1636,
				2936,
				1645,
				4974
			});
			Common.AnyCrimsonBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyCrimsonBanner", crimsonBanners);
			RecipeGroup jungleBanners = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyJungleBanner"), new int[]
			{
				1675,
				2977,
				2939,
				2940,
				2915,
				1615,
				1619,
				1688,
				2897,
				1640,
				2925,
				1661,
				2981,
				1666,
				1670,
				1668,
				4976
			});
			Common.AnyJungleBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyJungleBanner", jungleBanners);
			RecipeGroup snowBanners = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnySnowBanner"), new int[]
			{
				2935,
				1643,
				1662,
				1696,
				2934,
				2933,
				1684,
				2980,
				2988,
				2898,
				3452,
				1663,
				1674
			});
			Common.AnySnowBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnySnowBanner", snowBanners);
			RecipeGroup desertBanners = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyDesertBanner"), new int[]
			{
				1693,
				1671,
				4966,
				3449,
				3450,
				3413,
				3414,
				4969,
				1618,
				3593,
				3411,
				3418,
				3419,
				3416,
				3415,
				3417,
				3412,
				3780,
				3789,
				3790,
				3791,
				3792,
				3793
			});
			Common.AnyDesertBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyDesertBanner", desertBanners);
			RecipeGroup underworldBanners = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyUnderworldBanner"), new int[]
			{
				1659,
				2944,
				1664,
				1638,
				1627,
				2943,
				3446
			});
			Common.AnyUnderworldBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyUnderworldBanner", underworldBanners);
			RecipeGroup batBanners = new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyBatBanner"), new int[]
			{
				1621,
				2923,
				2925,
				2933,
				2937,
				2939,
				1659,
				2943,
				4968
			});
			Common.AnyBatBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyBatBanner", batBanners);
			RecipeGroup fishingBobbers = new RecipeGroup(() => CS$<>8__locals1.any + " " + Lang.GetItemNameValue(5139), new int[]
			{
				5139,
				5140,
				5144,
				5142,
				5141,
				5146,
				5145,
				5143
			});
			RecipeGroup.RegisterGroup("QoLCompendium:FishingBobbers", fishingBobbers);
			RecipeGroup.RegisterGroup("QoLCompendium:WellFed", new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.WellFed"), wellFedItems.ToArray()));
			RecipeGroup.RegisterGroup("QoLCompendium:PlentySatisfied", new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.PlentySatisfied"), plentySatisfiedItems.ToArray()));
			RecipeGroup.RegisterGroup("QoLCompendium:ExquisitelyStuffed", new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.ExquisitelyStuffed"), exquisitelyStuffedItems.ToArray()));
			if (ModConditions.martainsOrderLoaded && gourmetFlavorItems.Count > 0)
			{
				RecipeGroup.RegisterGroup("QoLCompendium:GourmetFlavor", new RecipeGroup(() => CS$<>8__locals1.any + " " + Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.GourmetFlavor"), gourmetFlavorItems.ToArray()));
			}
			if (ModConditions.thoriumLoaded)
			{
				RecipeGroup.RegisterGroup("QoLCompendium:GrimPedestals", new RecipeGroup(() => CS$<>8__locals1.any + " " + Lang.GetItemNameValue(Common.GetModItem(ModConditions.thoriumMod, "GrimPedestal")), new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "GrimPedestal"),
					Common.GetModItem(ModConditions.thoriumMod, "GrimPedestalCrimson")
				}));
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00064C30 File Offset: 0x00062E30
		private static void RegisterGroupClone(RecipeGroup original, string groupName)
		{
			int groupID;
			if (RecipeGroup.recipeGroupIDs.TryGetValue(groupName, ref groupID))
			{
				RecipeGroup group = RecipeGroup.recipeGroups[groupID];
				original.ValidItems.UnionWith(group.ValidItems);
				group.ValidItems = original.ValidItems;
				return;
			}
			RecipeGroup.RegisterGroup(groupName, new RecipeGroup(original.GetText, new int[1])
			{
				ValidItems = original.ValidItems
			});
		}

		// Token: 0x04000579 RID: 1401
		public static readonly Regex chestItemRegex = new Regex("\\b(?!fake_)(.*chest)\\b", 8);

		// Token: 0x0400057A RID: 1402
		public static readonly Regex workBenchItemRegex = new Regex("\\b(.*workbench)\\b", 8);

		// Token: 0x0400057B RID: 1403
		public static readonly Regex sinkItemRegex = new Regex("\\b(.*sink)(?:does)?\\b", 8);

		// Token: 0x0400057C RID: 1404
		public static readonly Regex tableItemRegex = new Regex("\\b(.*table)(?:withcloth)?\\b", 8);

		// Token: 0x0400057D RID: 1405
		public static readonly Regex chairItemRegex = new Regex("\\b(.*chair)\\b", 8);

		// Token: 0x0400057E RID: 1406
		public static readonly Regex bookcaseItemRegex = new Regex("\\b(.*bookcase)\\b", 8);

		// Token: 0x0400057F RID: 1407
		public static readonly Regex campfireItemRegex = new Regex("\\b(.*campfire)\\b", 8);
	}
}
