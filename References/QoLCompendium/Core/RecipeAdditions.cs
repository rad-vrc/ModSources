// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.RecipeAdditions
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumRework;

#nullable disable
namespace QoLCompendium.Core;

public class RecipeAdditions : ModSystem
{
  public static readonly Regex chestItemRegex = new Regex("\\b(?!fake_)(.*chest)\\b", RegexOptions.Compiled);
  public static readonly Regex workBenchItemRegex = new Regex("\\b(.*workbench)\\b", RegexOptions.Compiled);
  public static readonly Regex sinkItemRegex = new Regex("\\b(.*sink)(?:does)?\\b", RegexOptions.Compiled);
  public static readonly Regex tableItemRegex = new Regex("\\b(.*table)(?:withcloth)?\\b", RegexOptions.Compiled);
  public static readonly Regex chairItemRegex = new Regex("\\b(.*chair)\\b", RegexOptions.Compiled);
  public static readonly Regex bookcaseItemRegex = new Regex("\\b(.*bookcase)\\b", RegexOptions.Compiled);
  public static readonly Regex campfireItemRegex = new Regex("\\b(.*campfire)\\b", RegexOptions.Compiled);

  [JITWhenModsEnabled(new string[] {"ThoriumRework"})]
  public static bool ThoriumReworkPotionsEnabled
  {
    get => ModContent.GetInstance<CompatConfig>().extraPotions;
  }

  public virtual void PostAddRecipes()
  {
    for (int index = 0; index < Main.recipe.Length; ++index)
    {
      if (QoLCompendium.QoLCompendium.itemConfig.EndlessAmmo)
      {
        if (Main.recipe[index].HasIngredient(97) && Main.recipe[index].HasResult(3104) && Main.recipe[index].HasTile(125))
        {
          Main.recipe[index].RemoveTile(125);
          Main.recipe[index].AddTile(220);
        }
        if (Main.recipe[index].HasIngredient(40) && Main.recipe[index].HasResult(3103) && Main.recipe[index].HasTile(125))
        {
          Main.recipe[index].RemoveTile(125);
          Main.recipe[index].AddTile(220);
        }
      }
      if (Main.recipe[index].HasIngredient(ModContent.ItemType<GoldenLockpick>()) && QoLCompendium.QoLCompendium.mainConfig.NonConsumableKeys)
        Main.recipe[index].AddConsumeIngredientCallback((Recipe.IngredientQuantityCallback) ((Recipe recipe, int type, ref int amount, bool isDecrafting) =>
        {
          if (type != ModContent.ItemType<GoldenLockpick>())
            return;
          amount = 0;
        }));
      if (Main.recipe[index].HasIngredient(329) && QoLCompendium.QoLCompendium.mainConfig.NonConsumableKeys)
        Main.recipe[index].AddConsumeIngredientCallback((Recipe.IngredientQuantityCallback) ((Recipe recipe, int type, ref int amount, bool isDecrafting) =>
        {
          if (type != 329)
            return;
          amount = 0;
        }));
      if (ModConditions.calamityLoaded && ModConditions.catalystLoaded)
      {
        if (Main.recipe[index].HasResult(ModContent.ItemType<PermanentCalamity>()))
          Main.recipe[index].AddIngredient(ModContent.ItemType<PermanentAstracola>(), 1);
        if (Main.recipe[index].HasResult(ModContent.ItemType<CalamityCraftingMonolith>()))
          Main.recipe[index].AddIngredient(Common.GetModItem(ModConditions.catalystMod, "AstralTransmogrifier"), 1);
      }
      if (ModConditions.calamityLoaded && ModConditions.clamityAddonLoaded && Main.recipe[index].HasResult(ModContent.ItemType<PermanentCalamity>()))
        Main.recipe[index].AddIngredient(ModContent.ItemType<PermanentClamity>(), 1);
      if (ModConditions.calamityLoaded && ModConditions.calamityEntropyLoaded)
      {
        if (Main.recipe[index].HasResult(ModContent.ItemType<PermanentCalamity>()))
          Main.recipe[index].AddIngredient(ModContent.ItemType<PermanentCalamityEntropy>(), 1);
        if (Main.recipe[index].HasResult(ModContent.ItemType<CalamityCraftingMonolith>()))
          Main.recipe[index].AddIngredient(Common.GetModItem(ModConditions.calamityEntropyMod, "AbyssalAltar"), 1);
      }
      if (ModConditions.thoriumLoaded && ModConditions.thoriumBossReworkLoaded && RecipeAdditions.ThoriumReworkPotionsEnabled && Main.recipe[index].HasResult(ModContent.ItemType<PermanentThoriumBard>()))
      {
        Main.recipe[index].AddIngredient(ModContent.ItemType<PermanentDeathsinger>(), 1);
        Main.recipe[index].AddIngredient(ModContent.ItemType<PermanentInspirationRegeneration>(), 1);
      }
      if (QoLCompendium.QoLCompendium.itemConfig.Mirrors && QoLCompendium.QoLCompendium.itemConfig.InformationAccessories && Main.recipe[index].HasResult(ModContent.ItemType<MosaicMirror>()))
        Main.recipe[index].AddIngredient(ModContent.ItemType<IAH>(), 1);
      if (ModConditions.calamityLoaded && Main.recipe[index].HasResult(ModContent.ItemType<PermanentEverything>()))
        Main.recipe[index].AddIngredient(ModContent.ItemType<PermanentCalamity>(), 1);
      if (ModConditions.martainsOrderLoaded && Main.recipe[index].HasResult(ModContent.ItemType<PermanentEverything>()))
        Main.recipe[index].AddIngredient(ModContent.ItemType<PermanentMartinsOrder>(), 1);
      if (ModConditions.secretsOfTheShadowsLoaded && Main.recipe[index].HasResult(ModContent.ItemType<PermanentEverything>()))
        Main.recipe[index].AddIngredient(ModContent.ItemType<PermanentSecretsOfTheShadows>(), 1);
      if (ModConditions.spiritLoaded && Main.recipe[index].HasResult(ModContent.ItemType<PermanentEverything>()))
        Main.recipe[index].AddIngredient(ModContent.ItemType<PermanentSpiritClassic>(), 1);
      if (ModConditions.thoriumLoaded && Main.recipe[index].HasResult(ModContent.ItemType<PermanentEverything>()))
        Main.recipe[index].AddIngredient(ModContent.ItemType<PermanentThorium>(), 1);
    }
  }

  public virtual void AddRecipes()
  {
    if (QoLCompendium.QoLCompendium.mainConfig.FullyDisableRecipes)
      return;
    for (int index = 0; index < ItemLoader.ItemCount; ++index)
    {
      if (ItemID.Sets.ShimmerTransformToItem[index] > 0)
      {
        Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.CraftingStations), ItemID.Sets.ShimmerTransformToItem[index], displayText: "Mods.QoLCompendium.ItemToggledConditions.CraftingStations");
        itemRecipe.AddIngredient(index, 1);
        itemRecipe.AddTile(ModContent.TileType<AetherAltarTile>());
        itemRecipe.Register();
      }
    }
    if (ModConditions.confectionRebakedLoaded && ModConditions.mechReworkLoaded)
    {
      Recipe recipe1 = Recipe.Create(Common.GetModItem(ModConditions.confectionRebakedMod, "DeathsRaze"), 1);
      recipe1.AddIngredient(795, 1);
      recipe1.AddIngredient(155, 1);
      recipe1.AddIngredient(190, 1);
      recipe1.AddIngredient(121, 1);
      recipe1.AddTile(26);
      recipe1.Register();
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
    if (!QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems)
      Common.CreateSimpleRecipe(87, 3213, 16 /*0x10*/, 1, 1, false, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.MobileStorages", (Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.MobileStorages)));
    Common.CreateSimpleRecipe(2308, 4022, 96 /*0x60*/, 1, 1, false, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.RecipeEnabled", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.GoldenCarpDelight)));
    Recipe itemRecipe1 = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.EasierUniversalPylon), 4951, displayText: "Mods.QoLCompendium.ItemToggledConditions.Pylons");
    itemRecipe1.AddIngredient(4917, 1);
    itemRecipe1.AddIngredient(4919, 1);
    itemRecipe1.AddIngredient(4876, 1);
    itemRecipe1.AddIngredient(4916, 1);
    itemRecipe1.AddIngredient(4875, 1);
    itemRecipe1.AddIngredient(4921, 1);
    itemRecipe1.AddIngredient(4918, 1);
    itemRecipe1.AddIngredient(4920, 1);
    if (!QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems)
    {
      itemRecipe1.AddIngredient(ModContent.ItemType<AetherPylon>(), 1);
      itemRecipe1.AddIngredient(ModContent.ItemType<CorruptionPylon>(), 1);
      itemRecipe1.AddIngredient(ModContent.ItemType<CrimsonPylon>(), 1);
      itemRecipe1.AddIngredient(ModContent.ItemType<DungeonPylon>(), 1);
      itemRecipe1.AddIngredient(ModContent.ItemType<SkyPylon>(), 1);
      itemRecipe1.AddIngredient(ModContent.ItemType<TemplePylon>(), 1);
      itemRecipe1.AddIngredient(ModContent.ItemType<HellPylon>(), 1);
    }
    itemRecipe1.AddTile(16 /*0x10*/);
    itemRecipe1.Register();
  }

  public static void ConversionRecipes()
  {
    Common.ConversionRecipe(12, 699, 16 /*0x10*/);
    Common.ConversionRecipe(11, 700, 16 /*0x10*/);
    Common.ConversionRecipe(14, 701, 16 /*0x10*/);
    Common.ConversionRecipe(13, 702, 16 /*0x10*/);
    Common.ConversionRecipe(56, 880, 16 /*0x10*/);
    Common.ConversionRecipe(364, 1104, 16 /*0x10*/);
    Common.ConversionRecipe(365, 1105, 134);
    Common.ConversionRecipe(366, 1106, 134);
    Common.ConversionRecipe(20, 703, 16 /*0x10*/);
    Common.ConversionRecipe(22, 704, 16 /*0x10*/);
    Common.ConversionRecipe(21, 705, 16 /*0x10*/);
    Common.ConversionRecipe(19, 706, 16 /*0x10*/);
    Common.ConversionRecipe(57, 1257, 16 /*0x10*/);
    Common.ConversionRecipe(381, 1184, 16 /*0x10*/);
    Common.ConversionRecipe(382, 1191, 134);
    Common.ConversionRecipe(391, 1198, 134);
    Common.ConversionRecipe(68, 1330, 16 /*0x10*/);
    Common.ConversionRecipe(86, 1329, 16 /*0x10*/);
    Common.ConversionRecipe(522, 1332, 16 /*0x10*/);
    Common.ConversionRecipe(521, 520, 16 /*0x10*/);
    Common.ConversionRecipe(60, 2887, 16 /*0x10*/);
    Common.ConversionRecipe(2318, 2319, 16 /*0x10*/);
    Common.ConversionRecipe(2318, 2305, 16 /*0x10*/);
    Common.ConversionRecipe(527, 528, 16 /*0x10*/);
    Common.CreateSimpleRecipe(547, 548, 134, 1, 1, false, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.ItemConversions", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.ItemConversions)), Condition.DownedMechBossAll);
    Common.CreateSimpleRecipe(548, 549, 134, 1, 1, false, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.ItemConversions", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.ItemConversions)), Condition.DownedMechBossAll);
    Common.CreateSimpleRecipe(549, 547, 134, 1, 1, false, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.ItemConversions", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.ItemConversions)), Condition.DownedMechBossAll);
    Common.ConversionRecipe(619, 911, 16 /*0x10*/);
    Common.ConversionRecipe(61, 836, 16 /*0x10*/);
    Common.ConversionRecipe(370, 1246, 16 /*0x10*/);
    Common.ConversionRecipe(3276, 3277, 16 /*0x10*/);
    Common.ConversionRecipe(3274, 3275, 16 /*0x10*/);
    Common.ConversionRecipe(833, 835, 16 /*0x10*/);
    Common.ConversionRecipe(59, 2171, 16 /*0x10*/);
    Common.ConversionRecipe(3203, 3204, 16 /*0x10*/);
    Common.ConversionRecipe(3982, 3983, 16 /*0x10*/);
    Common.ConversionRecipe(4144, 3368, 125);
    Common.ConversionRecipe(3008, 3007, 16 /*0x10*/);
    Common.ConversionRecipe(3014, 3006, 16 /*0x10*/);
    Common.ConversionRecipe(3012, 3013, 16 /*0x10*/);
    Common.ConversionRecipe(3015, 3016, 16 /*0x10*/);
    Common.ConversionRecipe(3023, 3020, 16 /*0x10*/);
    Common.ConversionRecipe(994, 3060, 16 /*0x10*/);
    Common.ConversionRecipe(1534, 1535 /*0x05FF*/, 134);
    if (ModConditions.calamityLoaded)
    {
      Common.ConversionRecipe(Common.GetModItem(ModConditions.calamityMod, "RottenMatter"), Common.GetModItem(ModConditions.calamityMod, "BloodSample"), 26);
      Common.ConversionRecipe(Common.GetModItem(ModConditions.calamityMod, "FilthyGlove"), Common.GetModItem(ModConditions.calamityMod, "BloodstainedGlove"), 26);
      Common.ConversionRecipe(Common.GetModItem(ModConditions.calamityMod, "RottenBrain"), Common.GetModItem(ModConditions.calamityMod, "BloodyWormTooth"), 26);
      Common.ConversionRecipe(Common.GetModItem(ModConditions.calamityMod, "RottingEyeball"), Common.GetModItem(ModConditions.calamityMod, "BloodyVein"), 26);
      Common.ConversionRecipe(Common.GetModItem(ModConditions.calamityMod, "CorruptionEffigy"), Common.GetModItem(ModConditions.calamityMod, "CrimsonEffigy"), 26);
    }
    int num = ModConditions.confectionRebakedLoaded ? 1 : 0;
    if (!ModConditions.depthsLoaded)
      return;
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ShadowShrub"), 318, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ShadowShrubSeeds"), 312, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ShadowShrubPlanterBox"), 3222, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "Quartz"), 173, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ArqueriteOre"), 174, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "QuartzCrate"), 4877, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ArqueriteCrate"), 4878, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ShadowFightingFish"), 2312, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "QuartzFeeder"), 2315, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "LodeStone"), 5010, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "StoneRose"), 1323, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "AmalgamAmulet"), 906, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "CrystalSkull"), 193, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "QuicksilverproofFishingHook"), 4881, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "QuicksilverproofTackleBag"), 5064, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "PalladiumShield"), 156, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "CrystalCrown"), Common.GetModItem(ModConditions.depthsMod, "CharredCrown"), 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "WhiteLightning"), 218, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "Skyfall"), 3282, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "BlueSphere"), 3290, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "SilverStar"), 220, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "NightFury"), 3019, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ShadowSphere"), 272, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "Steelocanth"), 2331, 16 /*0x10*/);
    Common.ConversionRecipe(Common.GetModItem(ModConditions.depthsMod, "ChasmeBag"), 3324, 16 /*0x10*/);
  }

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
    Common.CreateBagRecipe(Common.planteraDrops, 3328 /*0x0D00*/);
    Common.CreateBagRecipe(Common.golemDrops, 3329);
    Common.CreateBagRecipe(Common.betsyDrops, 3860);
    Common.CreateBagRecipe(Common.dukeFishronDrops, 3330);
    Common.CreateBagRecipe(Common.empressOfLightDrops, 4782);
    Common.CreateBagRecipe(Common.moonLordDrops, 3332);
  }

  public static void CrateRecipes()
  {
    Common.CreateCrateRecipe(3200, 2334, 3979, 1);
    Common.CreateCrateRecipe(3201, 2334, 3979, 1);
    Common.CreateCrateRecipe(285, 2334, 3979, 1);
    Common.CreateCrateRecipe(4341, 2334, 3979, 1);
    Common.CreateCrateRecipe(953, 2334, 3979, 1);
    Common.CreateCrateRecipe(3068, 2334, 3979, 1);
    Common.CreateCrateRecipe(3084, 2334, 3979, 1);
    Common.CreateCrateHardmodeRecipe(2424, 3979, 1);
    Common.CreateCrateRecipe(280, 2334, 3979, 1);
    Common.CreateCrateRecipe(281, 2334, 3979, 1);
    Common.CreateCrateRecipe(284, 2334, 3979, 1);
    Common.CreateCrateRecipe(946, 2334, 3979, 1);
    Common.CreateCrateRecipe(3069, 2334, 3979, 1, Condition.NotRemixWorld, Condition.NotZenithWorld);
    Common.CreateCrateRecipe(832, 2334, 3979, 1);
    Common.CreateCrateRecipe(933, 2334, 3979, 1);
    Common.CreateCrateRecipe(4281, 2334, 3979, 1);
    Common.CreateCrateRecipe(4429, 2334, 3979, 1);
    Common.CreateCrateRecipe(4427, 2334, 3979, 1);
    Common.CreateCrateRecipe(2608, 2335, 3980, 1);
    Common.CreateCrateRecipe(2587, 2335, 3980, 1);
    Common.CreateCrateRecipe(2501, 2335, 3980, 1);
    Common.CreateCrateRecipe(2491, 2336, 3981, 1);
    Common.CreateCrateRecipe(989, 2336, 3981, 1);
    Common.CreateCrateRecipe(49, 2336, 3981, 1);
    Common.CreateCrateRecipe(50, 2336, 3981, 1);
    Common.CreateCrateRecipe(53, 2336, 3981, 1);
    Common.CreateCrateRecipe(54, 2336, 3981, 1);
    Common.CreateCrateRecipe(5011, 2336, 3981, 1);
    Common.CreateCrateRecipe(975, 2336, 3981, 1);
    Common.CreateCrateRecipe(930, 2336, 3981, 1);
    Common.CreateCrateRecipe(3017, 3208, 3987, 1);
    Common.CreateCrateRecipe(212, 3208, 3987, 1);
    Common.CreateCrateRecipe(964, 3208, 3987, 1);
    Common.CreateCrateRecipe(211, 3208, 3987, 1);
    Common.CreateCrateRecipe(213, 3208, 3987, 1);
    Common.CreateCrateRecipe(2292, 3208, 3987, 1);
    Common.CreateCrateRecipe(753, 3208, 3987, 1);
    Common.CreateCrateRecipe(3360, 3208, 3987, 1);
    Common.CreateCrateRecipe(3361, 3208, 3987, 1);
    Common.CreateCrateRecipe(4426, 3208, 3987, 1);
    Common.CreateCrateRecipe(65, 3206, 3985, 1);
    Common.CreateCrateRecipe(158, 3206, 3985, 1);
    Common.CreateCrateRecipe(159, 3206, 3985, 1);
    Common.CreateCrateRecipe(2219, 3206, 3985, 1);
    Common.CreateCrateRecipe(4978, 3206, 3985, 1);
    Common.CreateCrateRecipe(670, 4405, 4406, 1);
    Common.CreateCrateRecipe(724, 4405, 4406, 1);
    Common.CreateCrateRecipe(950, 4405, 4406, 1);
    Common.CreateCrateRecipe(1319, 4405, 4406, 1, Condition.NotRemixWorld, Condition.NotZenithWorld);
    Common.CreateCrateRecipe(987, 4405, 4406, 1);
    Common.CreateCrateRecipe(1579, 4405, 4406, 1);
    Common.CreateCrateRecipe(4056, 4407, 4408, 1);
    Common.CreateCrateRecipe(4442, 4407, 4408, 1);
    Common.CreateCrateRecipe(4055, 4407, 4408, 1);
    Common.CreateCrateRecipe(4061, 4407, 4408, 1);
    Common.CreateCrateRecipe(4062, 4407, 4408, 1);
    Common.CreateCrateRecipe(4262, 4407, 4408, 1);
    Common.CreateCrateRecipe(4263, 4407, 4408, 1);
    Common.CreateCrateRecipe(857, 4407, 4408, 1);
    Common.CreateCrateRecipe(848, 4407, 4408, 1);
    Common.CreateCrateRecipe(866, 4407, 4408, 1);
    Common.CreateCrateRecipe(162, 3203, 3982, 1);
    Common.CreateCrateRecipe(111, 3203, 3982, 1);
    Common.CreateCrateRecipe(96 /*0x60*/, 3203, 3982, 1);
    Common.CreateCrateRecipe(115, 3203, 3982, 1);
    Common.CreateCrateRecipe(64 /*0x40*/, 3203, 3982, 1);
    Common.CreateCrateRecipe(2330, 3203, 3982, 1);
    Common.CreateCrateHardmodeRecipe(3210, 3982, 1);
    Common.CreateCrateRecipe(800, 3204, 3983, 1);
    Common.CreateCrateRecipe(802, 3204, 3983, 1);
    Common.CreateCrateRecipe(1256, 3204, 3983, 1);
    Common.CreateCrateRecipe(1290, 3204, 3983, 1);
    Common.CreateCrateRecipe(3062, 3204, 3983, 1);
    Common.CreateCrateHardmodeRecipe(3211, 3983, 1);
    Common.CreateCrateHardmodeRecipe(3209, 3986, 1);
    Common.CreateCrateRecipe(186, 5002, 5003, 1);
    Common.CreateCrateRecipe(4404, 5002, 5003, 1);
    Common.CreateCrateRecipe(277, 5002, 5003, 1);
    Common.CreateCrateRecipe(187, 5002, 5003, 1);
    Common.CreateCrateRecipe(863, 5002, 5003, 1);
    Common.CreateCrateRecipe(4425, 5002, 5003, 1);
    Common.CreateCrateRecipe(4460, 5002, 5003, 1);
    Common.CreateCrateRecipe(2341, 5002, 5003, 1);
    Common.CreateCrateRecipe(2342, 5002, 5003, 1);
    Common.CreateCrateRecipe(2332, 5002, 5003, 1);
    Common.CreateCrateRecipe(906, 4877, 4878, 1);
    Common.CreateCrateRecipe(4822, 4877, 4878, 1);
    Common.CreateCrateRecipe(4828, 4877, 4878, 1);
    Common.CreateCrateRecipe(4880, 4877, 4878, 1);
    Common.CreateCrateRecipe(4881, 4877, 4878, 1);
    Common.CreateCrateRecipe(4737, 4877, 4878, 1);
    Common.CreateCrateRecipe(4551, 4877, 4878, 1);
    Common.CreateCrateRecipe(4443, 4877, 4878, 1);
    Common.CreateCrateRecipe(4819, 4877, 4878, 1);
    Common.CreateCrateRecipe(4820, 4877, 4878, 1);
    Common.CreateCrateRecipe(4872, 4877, 4878, 1);
    Common.CreateCrateHardmodeRecipe(2331, 4878, 1);
    Common.CreateCrateWithKeyRecipe(274, 4877, 4878, 1, 329);
    Common.CreateCrateWithKeyRecipe(220, 4877, 4878, 1, 329);
    Common.CreateCrateWithKeyRecipe(112 /*0x70*/, 4877, 4878, 1, 329, Condition.NotRemixWorld, Condition.NotZenithWorld);
    Common.CreateCrateWithKeyRecipe(218, 4877, 4878, 1, 329);
    Common.CreateCrateWithKeyRecipe(3019, 4877, 4878, 1, 329);
    Common.CreateCrateWithKeyRecipe(5010, 4877, 4878, 1, 329);
    Common.CreateCrateWithKeyRecipe(3317, 3205, 3984, 1, 327);
    Common.CreateCrateWithKeyRecipe(155, 3205, 3984, 1, 327);
    Common.CreateCrateWithKeyRecipe(156, 3205, 3984, 1, 327);
    Common.CreateCrateWithKeyRecipe(157, 3205, 3984, 1, 327, Condition.NotRemixWorld, Condition.NotZenithWorld);
    Common.CreateCrateWithKeyRecipe(163, 3205, 3984, 1, 327);
    Common.CreateCrateWithKeyRecipe(113, 3205, 3984, 1, 327);
    Common.CreateCrateWithKeyRecipe(164, 3205, 3984, 1, 327);
    Common.CreateCrateWithKeyRecipe(329, 3205, 3984, 1, 327);
    Common.CreateCrateWithKeyRecipe(3317, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>());
    Common.CreateCrateWithKeyRecipe(155, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>());
    Common.CreateCrateWithKeyRecipe(156, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>());
    Common.CreateCrateWithKeyRecipe(157, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>(), Condition.NotRemixWorld, Condition.NotZenithWorld);
    Common.CreateCrateWithKeyRecipe(163, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>());
    Common.CreateCrateWithKeyRecipe(113, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>());
    Common.CreateCrateWithKeyRecipe(164, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>());
    Common.CreateCrateWithKeyRecipe(329, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>());
    Common.CreateCrateRecipe(997, 2334, 3979, 1);
    Common.CreateCrateRecipe(2196, 2334, 3979, 1);
    Common.CreateCrateRecipe(2204, 3208, 3987, 1);
    Common.CreateCrateRecipe(2197, 3206, 3985, 1);
    Common.CreateCrateRecipe(2198, 4405, 4406, 1);
    Common.CreateCrateRecipe(4276, 4407, 4408, 1);
    Common.CreateCrateWithKeyRecipe(3000, 3205, 3984, 1, 327);
    Common.CreateCrateWithKeyRecipe(2192, 3205, 3984, 1, 327);
    Common.CreateCrateWithKeyRecipe(3000, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>());
    Common.CreateCrateWithKeyRecipe(2192, 3205, 3984, 1, ModContent.ItemType<GoldenLockpick>());
    Common.CreateCrateHardmodeRecipe(3064, 3981, 1);
    if (ModConditions.calamityLoaded)
    {
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "AstrophageItem"), Common.GetModItem(ModConditions.calamityMod, "MonolithCrate"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "AstralScythe"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, ModConditions.DownedAstrumAureus);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "TitanArm"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, ModConditions.DownedAstrumAureus);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "StellarCannon"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, ModConditions.DownedAstrumAureus);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "AstralachneaStaff"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, ModConditions.DownedAstrumAureus);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "HivePod"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, ModConditions.DownedAstrumAureus);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "StellarKnife"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, ModConditions.DownedAstrumAureus);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "StarbusterCore"), Common.GetModItem(ModConditions.calamityMod, "AstralCrate"), 1, ModConditions.DownedAstrumAureus);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "AshenStalactite"), Common.GetModItem(ModConditions.calamityMod, "SlagCrate"), Common.GetModItem(ModConditions.calamityMod, "BrimstoneCrate"), 1);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "BladecrestOathsword"), Common.GetModItem(ModConditions.calamityMod, "SlagCrate"), Common.GetModItem(ModConditions.calamityMod, "BrimstoneCrate"), 1);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "LiliesOfFinality"), Common.GetModItem(ModConditions.calamityMod, "BrimstoneCrate"), 1, ModConditions.DownedYharon);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "AbyssalAmulet"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "AlluringBait"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "BrokenWaterFilter"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "EffigyOfDecay"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "RustyBeaconPrototype"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "RustyMedallion"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1);
      Common.CreateCrateRecipe(2341, Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1);
      Common.CreateCrateRecipe(2342, Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1);
      Common.CreateCrateRecipe(2332, Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "BallOFugu"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedSlimeGod);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "Archerfish"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedSlimeGod);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "BlackAnurian"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedSlimeGod);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "HerringStaff"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedSlimeGod);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "Lionfish"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedSlimeGod);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "AnechoicPlating"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedSlimeGod);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "DepthCharm"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedSlimeGod);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "IronBoots"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedSlimeGod);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "StrangeOrb"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedSlimeGod);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "TorrentialTear"), Common.GetModItem(ModConditions.calamityMod, "SulphurousCrate"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedSlimeGod);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "SulphurousGrabber"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedAcidRain2);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "FlakToxicannon"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedAcidRain2);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "BelchingSaxophone"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedAcidRain2);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "SlitheringEels"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedAcidRain2);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "SkyfinBombers"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedAcidRain2);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "SpentFuelContainer"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedAcidRain2);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "NuclearFuelRod"), Common.GetModItem(ModConditions.calamityMod, "HydrothermalCrate"), 1, ModConditions.DownedAcidRain2);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "RustedJingleBell"), Common.GetModItem(ModConditions.calamityMod, "EutrophicCrate"), Common.GetModItem(ModConditions.calamityMod, "PrismCrate"), 1);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.calamityMod, "SparklingEmpress"), Common.GetModItem(ModConditions.calamityMod, "EutrophicCrate"), Common.GetModItem(ModConditions.calamityMod, "PrismCrate"), 1, ModConditions.DownedDesertScourge);
      Common.CreateCrateHardmodeRecipe(Common.GetModItem(ModConditions.calamityMod, "SerpentsBite"), Common.GetModItem(ModConditions.calamityMod, "PrismCrate"), 1);
    }
    if (ModConditions.spiritLoaded)
    {
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.spiritMod, "ReachBrooch"), Common.GetModItem(ModConditions.spiritMod, "ReachCrate"), Common.GetModItem(ModConditions.spiritMod, "BriarCrate"), 1);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.spiritMod, "ReachBoomerang"), Common.GetModItem(ModConditions.spiritMod, "ReachCrate"), Common.GetModItem(ModConditions.spiritMod, "BriarCrate"), 1);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.spiritMod, "ThornHook"), Common.GetModItem(ModConditions.spiritMod, "ReachCrate"), Common.GetModItem(ModConditions.spiritMod, "BriarCrate"), 1);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.spiritMod, "ReachChestMagic"), Common.GetModItem(ModConditions.spiritMod, "ReachCrate"), Common.GetModItem(ModConditions.spiritMod, "BriarCrate"), 1);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.spiritMod, "LivingElderbarkWand"), Common.GetModItem(ModConditions.spiritMod, "ReachCrate"), Common.GetModItem(ModConditions.spiritMod, "BriarCrate"), 1);
      Common.CreateCrateRecipe(Common.GetModItem(ModConditions.spiritMod, "ThornyRod"), Common.GetModItem(ModConditions.spiritMod, "ReachCrate"), Common.GetModItem(ModConditions.spiritMod, "BriarCrate"), 1);
    }
    if (!ModConditions.thoriumLoaded)
      return;
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "MagicConch"), Common.GetModItem(ModConditions.thoriumMod, "AquaticDepthsCrate"), Common.GetModItem(ModConditions.thoriumMod, "AbyssalCrate"), 1, Condition.DownedEowOrBoc);
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "AnglerBowl"), Common.GetModItem(ModConditions.thoriumMod, "AquaticDepthsCrate"), Common.GetModItem(ModConditions.thoriumMod, "AbyssalCrate"), 1, Condition.DownedEowOrBoc);
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "RainStone"), Common.GetModItem(ModConditions.thoriumMod, "AquaticDepthsCrate"), Common.GetModItem(ModConditions.thoriumMod, "AbyssalCrate"), 1, Condition.DownedEowOrBoc);
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "SteelDrum"), Common.GetModItem(ModConditions.thoriumMod, "AquaticDepthsCrate"), Common.GetModItem(ModConditions.thoriumMod, "AbyssalCrate"), 1, Condition.DownedEowOrBoc);
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "SeaTurtlesBulwark"), Common.GetModItem(ModConditions.thoriumMod, "AquaticDepthsCrate"), Common.GetModItem(ModConditions.thoriumMod, "AbyssalCrate"), 1, Condition.DownedEowOrBoc);
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "MixTape"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1);
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "LootRang"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1);
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "MagmaCharm"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1);
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "SpringSteps"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1);
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "DeepStaff"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1);
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "MagmaLocket"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1);
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "SpringHook"), Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1);
    Common.CreateCrateRecipe(906, Common.GetModItem(ModConditions.thoriumMod, "ScarletCrate"), Common.GetModItem(ModConditions.thoriumMod, "SinisterCrate"), 1);
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "HightechSonarDevice"), Common.GetModItem(ModConditions.thoriumMod, "StrangeCrate"), Common.GetModItem(ModConditions.thoriumMod, "WondrousCrate"), 1);
    Common.CreateCrateRecipe(Common.GetModItem(ModConditions.thoriumMod, "DrownedDoubloon"), Common.GetModItem(ModConditions.thoriumMod, "StrangeCrate"), Common.GetModItem(ModConditions.thoriumMod, "WondrousCrate"), 1);
  }

  public static void BannerRecipes()
  {
    Common.AddBannerToItemRecipe(1665, 1303, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3448, 1303, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2973, 1321, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1630, 536, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1630, 535, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1630, 554, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1630, 532, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1689, 887, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1615, 885, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1620, 886, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2923, 893, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3448, 890, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3405, 3781, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3393, 393, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3392, 393, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2955, 393, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1675, 393, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3391, 393, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1621, 18, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3393, 18, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2923, 18, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3392, 18, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3391, 18, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1694, 3102, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3407, 3109, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3408, 3109, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3406, 3189, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3406, 3188, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3406, 3187, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1681, 954, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1681, 955, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2987, 411, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2987, 410, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2987, 88, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4965, 4761, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1615, 263, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3391, 4030, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3393, 4030, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3392, 4030, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerSetToItemRecipe(NPCID.Sets.Skeletons, 5041);
    Common.AddBannerToItemRecipe(1685, 4020, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1623, 4020, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3406, 4029, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3405, 4029, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3408, 4036, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3407, 4036, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2987, 4037, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerGroupToItemRecipe(Common.AnyBatBanner, 5097, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1621, 1325, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.NotRemixWorld);
    Common.AddBannerToItemRecipe(3393, 3285, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3392, 3285, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3391, 3285, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1681, 1166, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2987, 1320, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1630, 1264, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1630, 676, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1630, 725, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1630, 517, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3406, 4463, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2973, 682, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1620, 723, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3405, 3269, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2923, 1325, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode, Condition.NotRemixWorld);
    Common.AddBannerToItemRecipe(1623, 1308, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1630, 437, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1630, 1312, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1698, 215, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1664, 1323, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1659, 1322, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2943, 1322, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1627, 4025, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3446, 4025, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMechBossAny);
    Common.AddBannerToItemRecipe(3446, 1518, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMechBossAny);
    Common.AddBannerToItemRecipe(1638, 272, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerGroupToItemRecipe(Common.AnyUnderworldBanner, 3282, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedSkeletron);
    Common.AddBannerGroupToItemRecipe(Common.AnyUnderworldBanner, 3290, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3446, 683, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMechBossAny);
    Common.AddBannerToItemRecipe(1658, 4016, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1658, 1516, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3452, 1253, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2898, 393, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1684, 393, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2988, 393, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2933, 18, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2988, 879, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2898, 879, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1643, 805, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1643, 804, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1643, 803, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2935, 4026, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2933, 4026, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2980, 4026, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1663, 4027, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3452, 4027, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1674, 3532, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2934, 1519, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMechBossAny);
    Common.AddBannerToItemRecipe(1684, 951, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerGroupToItemRecipe(Common.AnySnowBanner, 3289, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2898, 1306, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1663, 726, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1674, 5096, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1674, 4428, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1696, 5130, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerGroupToItemRecipe(Common.AnySnowBanner, 1537, 1, 5, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1671, 889, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3417, 3770, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3416, 3784, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3416, 3785, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3416, 3786, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3416, 2801, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3416, 2802, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1671, 872, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1671, 871, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1671, 870, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1618, 4012, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3414, 4012, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3413, 4012, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3793, 4028, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3419, 4020, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3789, 4028, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3414, 3772, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3418, 3771, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerGroupToItemRecipe(Common.AnyDesertBanner, 4714, 1, 5, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1680, 268, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3447, 1303, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1680, 4035, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1634, 4035, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1661, 887, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4976, 887, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2939, 18, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1661, 962, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1661, 961, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1661, 960, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1675, 263, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1668, 5042, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2977, 5042, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2897, 5042, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2925, 4023, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1640, 4023, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(4976, 1521, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMechBossAny);
    Common.AddBannerToItemRecipe(1670, 1611, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMechBossAny);
    Common.AddBannerToItemRecipe(2897, 1265, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerGroupToItemRecipe(Common.AnyJungleBanner, 3286, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMechBossAny);
    Common.AddBannerGroupToItemRecipe(Common.AnyJungleBanner, 1533, 1, 5, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1667, 1172, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(4968, 18, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4968, 4764, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1637, 891, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3449, 888, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3449, 890, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2908, 888, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2909, 892, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1641, 958, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1641, 957, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1641, 956, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1632, 243, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2907, 3759, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2907, 3758, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2907, 3757, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3449, 872, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3449, 871, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3449, 870, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1641, 4015, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3790, 4028, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerGroupToItemRecipe(Common.AnyCorruptionBanner, 996, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1641, 5094, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerGroupToItemRecipe(Common.AnyCorruptionBanner, 1534, 1, 5, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1636, 891, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4966, 888, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(4966, 890, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2910, 888, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1645, 892, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(4970, 243, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4972, 3759, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4972, 3758, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4972, 3757, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4966, 872, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(4966, 871, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(4966, 870, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1635, 4015, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3791, 4028, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerGroupToItemRecipe(Common.AnyCrimsonBanner, 996, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1626, 5094, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1635, 5094, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1644, 5094, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerGroupToItemRecipe(Common.AnyCrimsonBanner, 1535 /*0x05FF*/, 1, 5, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1642, 891, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3450, 893, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1677, 890, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1677, 889, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3450, 872, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3450, 871, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3450, 870, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3792, 4028, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1629, 4011, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2937, 4011, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2938, 4011, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1651, 4017, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1691, 3260, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1629, 1326, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1691, 856, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerGroupToItemRecipe(Common.AnyHallowBanner, 1536 /*0x0600*/, 1, 5, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2904, 963, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2904, 977, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2975, 1300, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2958, 938, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2911, 891, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2924, 891, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2970, 885, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2900, 886, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(3451, 3095, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2911, 3095, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1682, 3095, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3451, 959, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1682, 959, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2984, 1514, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2911, 4018, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2904, 5042, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2924, 4018, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2974, 4013, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2975, 4013, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2984, 4013, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerGroupToItemRecipe(Common.AnyArmoredBonesBanner, 1517, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerGroupToItemRecipe(Common.AnyArmoredBonesBanner, 671, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerGroupToItemRecipe(Common.AnyArmoredBonesBanner, 3291, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerGroupToItemRecipe(Common.AnyArmoredBonesBanner, 4679, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerGroupToItemRecipe(Common.AnyArmoredBonesBanner, 1266, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2924, 4789, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2914, 1445, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2958, 1513, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2956, 1444, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2965, 1446, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2974, 759, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2975, 1254, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2984, 679, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerGroupToItemRecipe(Common.AnyArmoredBonesBanner, 1183, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(1682, 932, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1701, 216, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1678, 216, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1643, 216, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1695, 485, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1695, 885, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1699, 889, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1701, 1304, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1690, 1243, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1678, 1136, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1678, 1135, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1646, 4021, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1616, 1244, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1646, 4057, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1627, 4610, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1628, 4612, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1632, 4670, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4970, 4671, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1657, 4674, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1665, 4649, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1668, 4648, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3447, 4650, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2968, 4369 /*0x1111*/, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1680, 4651, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1683, 4367, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2992, 4371, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(2897, 4675, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1674, 4613, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3789, 4669, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1691, 4684, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(4977, 4683, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1697, 4611, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1700, 4379, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3409, 3212, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3410, 3212, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(1631, 893, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(4543, 4381, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4543, 4273, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4542, 4381, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4542, 4273, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(3409, 1314, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(1631, 1314, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(4546, 4270, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(4546, 4317, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(4545, 4272, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(4545, 4317, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(4602, 4269, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(3410, 3213, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4543, 4325, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4542, 4325, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerToItemRecipe(4602, 4054, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.Hardmode);
    Common.AddBannerToItemRecipe(2927, 160 /*0xA0*/, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
    Common.AddBannerGroupToItemRecipe(Common.AnyPirateBanner, 854, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPirates);
    Common.AddBannerGroupToItemRecipe(Common.AnyPirateBanner, 3033, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPirates);
    Common.AddBannerGroupToItemRecipe(Common.AnyPirateBanner, 855, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPirates);
    Common.AddBannerGroupToItemRecipe(Common.AnyPirateBanner, 1280 /*0x0500*/, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPirates);
    Common.AddBannerGroupToItemRecipe(Common.AnyPirateBanner, 1279 /*0x04FF*/, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPirates);
    Common.AddBannerGroupToItemRecipe(Common.AnyPirateBanner, 1277, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPirates);
    Common.AddBannerGroupToItemRecipe(Common.AnyPirateBanner, 1278, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPirates);
    Common.AddBannerGroupToItemRecipe(Common.AnyPirateBanner, 3265, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPirates);
    Common.AddBannerGroupToItemRecipe(Common.AnyPirateBanner, 3264, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPirates);
    Common.AddBannerGroupToItemRecipe(Common.AnyPirateBanner, 3263, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPirates);
    Common.AddBannerGroupToItemRecipe(Common.AnyPirateBanner, 672, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPirates);
    Common.AddBannerGroupToItemRecipe(Common.AnyPirateBanner, 2584, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPirates);
    Common.AddBannerToItemRecipe(3441, 905, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPirates);
    Common.AddBannerToItemRecipe(3395, 497, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMechBossAny);
    Common.AddBannerToItemRecipe(1692, 900, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMechBossAny);
    Common.AddBannerToItemRecipe(3397, 2770, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(3400, 4742, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(3400, 4741, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(3400, 4742, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(3396, 4739, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(3396, 4738, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(3399, 4037, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMechBossAny);
    Common.AddBannerToItemRecipe(1692, 1520, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMechBossAny);
    Common.AddBannerToItemRecipe(3397, 1570, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(1679, 1327, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMechBossAll);
    Common.AddBannerToItemRecipe(3400, 3098, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(3402, 3249, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(3396, 3105, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(3397, 3292, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(3403, 3107, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(3401, 3106, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2920, 1311, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMechBossAny);
    Common.AddBannerToItemRecipe(2971, 1790, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2971, 1789, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2971, 1788, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2929, 1857, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2994, 1945, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2994, 1944, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2994, 1943, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedPlantera);
    Common.AddBannerToItemRecipe(2949, 2805, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2949, 2804, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2949, 2803, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2951, 2805, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2951, 2804, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2951, 2803, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2945, 2805, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2945, 2804, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2945, 2803, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2950, 2808, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2950, 2807, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2950, 2806, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2947, 2808, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2947, 2807, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2947, 2806, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2948, 2808, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2948, 2807, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2948, 2806, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedGolem);
    Common.AddBannerToItemRecipe(2952, 2771, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMartians);
    Common.AddBannerToItemRecipe(2972, 2771, 1, 1, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)), Condition.DownedMartians);
  }

  public static void GrabBagRecipes()
  {
    Common.CreateSimpleRecipe(1774, 1810, 18, 5, 1, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes)));
    Common.CreateSimpleRecipe(1774, 1800, 18, 5, 1, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes)));
    Common.CreateSimpleRecipe(1774, 1809, 18, 1, 50, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes)));
    Common.CreateSimpleRecipe(1869, 602, 18, 5, 1, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes)), Condition.Hardmode);
    Common.CreateSimpleRecipe(1869, 1927, 18, 5, 1, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes)));
    Common.CreateSimpleRecipe(1869, 1870, 18, 5, 1, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes)));
    Common.CreateSimpleRecipe(1869, 1909, 18, 5, 1, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes)));
    Common.CreateSimpleRecipe(1869, 1917, 18, 5, 1, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes)));
    Common.CreateSimpleRecipe(1869, 1915, 18, 5, 1, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes)));
    Common.CreateSimpleRecipe(1869, 1918, 18, 5, 1, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes)));
    Common.CreateSimpleRecipe(1869, 1921, 18, 5, 1, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes)));
    Common.CreateSimpleRecipe(1869, 1923, 18, 5, 1, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes)));
    Common.CreateSimpleRecipe(1869, 1913, 18, 1, 50, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Crates", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes)));
  }

  public virtual void AddRecipeGroups()
  {
    IEnumerable<int> allItems = Enumerable.Range(0, ItemLoader.ItemCount);
    string any = Language.GetTextValue("LegacyMisc.37");
    List<int> intList1 = new List<int>();
    List<int> intList2 = new List<int>();
    List<int> intList3 = new List<int>();
    List<int> intList4 = new List<int>();
    List<int> intList5 = new List<int>();
    List<int> intList6 = new List<int>();
    List<int> intList7 = new List<int>();
    for (int index = 0; index < TextureAssets.Item.Length; ++index)
    {
      Item obj = new Item();
      obj.SetDefaults(index);
      if (obj.buffType == 26)
        intList4.Add(obj.type);
      else if (obj.buffType == 206)
        intList5.Add(obj.type);
      else if (obj.buffType == 207)
        intList6.Add(obj.type);
      else if (ModConditions.martainsOrderLoaded && obj.buffType == Common.GetModBuff(ModConditions.martainsOrderMod, "Gourmet"))
        intList7.Add(obj.type);
      if (obj.consumable && obj.createTile >= 0 && (obj.ModItem == null || ((ModType) obj.ModItem).Mod != ((ModType) this).Mod))
      {
        if (((IEnumerable<int>) TileID.Sets.RoomNeeds.CountsAsChair).Contains<int>(obj.createTile))
          intList1.Add(obj.type);
        if (((IEnumerable<int>) TileID.Sets.RoomNeeds.CountsAsDoor).Contains<int>(obj.createTile))
          intList2.Add(obj.type);
        if (((IEnumerable<int>) TileID.Sets.RoomNeeds.CountsAsTorch).Contains<int>(obj.createTile))
          intList3.Add(obj.type);
      }
    }
    int[] array1 = new int[2]{ 35, 716 };
    if (ModConditions.exxoAvalonOriginsLoaded)
    {
      Array.Resize<int>(ref array1, array1.Length + 1);
      int[] numArray = array1;
      numArray[numArray.Length - 1] = Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "NickelAnvil");
    }
    int[] array2 = new int[2]{ 525, 1220 };
    if (ModConditions.exxoAvalonOriginsLoaded)
    {
      Array.Resize<int>(ref array2, array2.Length + 1);
      int[] numArray = array2;
      numArray[numArray.Length - 1] = Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "NaquadahAnvil");
    }
    int[] array3 = new int[2]{ 524, 1221 };
    if (ModConditions.exxoAvalonOriginsLoaded)
    {
      Array.Resize<int>(ref array3, array3.Length + 1);
      int[] numArray = array3;
      numArray[numArray.Length - 1] = Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "TroxiniumForge");
    }
    int[] array4 = new int[2]
    {
      ModContent.ItemType<DemonAltar>(),
      ModContent.ItemType<CrimsonAltar>()
    };
    if (ModConditions.exxoAvalonOriginsLoaded)
    {
      Array.Resize<int>(ref array4, array4.Length + 3);
      int[] numArray1 = array4;
      numArray1[numArray1.Length - 3] = Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "DemonAltar");
      int[] numArray2 = array4;
      numArray2[numArray2.Length - 2] = Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "CrimsonAltar");
      int[] numArray3 = array4;
      numArray3[numArray3.Length - 1] = Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "IckyAltar");
    }
    if (ModConditions.fargosMutantLoaded)
    {
      Array.Resize<int>(ref array4, array4.Length + 2);
      int[] numArray4 = array4;
      numArray4[numArray4.Length - 2] = Common.GetModItem(ModConditions.fargosMutantMod, "DemonAltar");
      int[] numArray5 = array4;
      numArray5[numArray5.Length - 1] = Common.GetModItem(ModConditions.fargosMutantMod, "CrimsonAltar");
    }
    if (ModConditions.luiAFKLoaded)
    {
      Array.Resize<int>(ref array4, array4.Length + 2);
      int[] numArray6 = array4;
      numArray6[numArray6.Length - 2] = Common.GetModItem(ModConditions.luiAFKMod, "CorruptionAltar");
      int[] numArray7 = array4;
      numArray7[numArray7.Length - 1] = Common.GetModItem(ModConditions.luiAFKMod, "CrimsonAltar");
    }
    if (ModConditions.magicStorageLoaded)
    {
      Array.Resize<int>(ref array4, array4.Length + 2);
      int[] numArray8 = array4;
      numArray8[numArray8.Length - 2] = Common.GetModItem(ModConditions.magicStorageMod, "DemonAltar");
      int[] numArray9 = array4;
      numArray9[numArray9.Length - 1] = Common.GetModItem(ModConditions.magicStorageMod, "CrimsonAltar");
    }
    if (ModConditions.martainsOrderLoaded)
    {
      Array.Resize<int>(ref array4, array4.Length + 1);
      int[] numArray = array4;
      numArray[numArray.Length - 1] = Common.GetModItem(ModConditions.martainsOrderMod, "DemonAltar");
    }
    if (ModConditions.thoriumLoaded)
    {
      Array.Resize<int>(ref array4, array4.Length + 2);
      int[] numArray10 = array4;
      numArray10[numArray10.Length - 2] = Common.GetModItem(ModConditions.thoriumMod, "GrimPedestal");
      int[] numArray11 = array4;
      numArray11[numArray11.Length - 1] = Common.GetModItem(ModConditions.thoriumMod, "GrimPedestalCrimson");
    }
    RecipeGroup.RegisterGroup("QoLCompendium:Anvils", new RecipeGroup((Func<string>) (() => $"{any} {Lang.GetItemNameValue(35)}"), array1));
    RecipeGroup.RegisterGroup("QoLCompendium:HardmodeAnvils", new RecipeGroup((Func<string>) (() => $"{any} {Lang.GetItemNameValue(525)}"), array2));
    RecipeGroup.RegisterGroup("QoLCompendium:HardmodeForges", new RecipeGroup((Func<string>) (() => $"{any} {Lang.GetItemNameValue(524)}"), array3));
    RecipeGroup.RegisterGroup("QoLCompendium:Altars", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("MapObject.DemonAltar")}"), array4));
    RecipeGroup original1 = new RecipeGroup((Func<string>) (() => $"{any} {Lang.GetItemNameValue(48 /*0x30*/)}"), GetItems(48 /*0x30*/, RecipeAdditions.chestItemRegex, Array.Empty<int>()));
    RecipeGroup.RegisterGroup("QoLCompendium:AnyChest", original1);
    RecipeAdditions.RegisterGroupClone(original1, "Chest");
    RecipeGroup original2 = new RecipeGroup((Func<string>) (() => $"{any} {Lang.GetItemNameValue(36)}"), GetItems(36, RecipeAdditions.workBenchItemRegex, new int[1]
    {
      2172
    }));
    RecipeGroup.RegisterGroup("QoLCompendium:AnyWorkBench", original2);
    RecipeAdditions.RegisterGroupClone(original2, "WorkBench");
    RecipeGroup original3 = new RecipeGroup((Func<string>) (() => $"{any} {Lang.GetItemNameValue(31 /*0x1F*/)}"), new int[7]
    {
      31 /*0x1F*/,
      350,
      351,
      2234,
      2244,
      2257,
      2258
    });
    RecipeGroup.RegisterGroup("QoLCompendium:AnyBottle", original3);
    RecipeAdditions.RegisterGroupClone(original3, "Bottle");
    RecipeGroup original4 = new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.Sinks")}"), GetItems(2841, RecipeAdditions.sinkItemRegex, Array.Empty<int>()));
    RecipeGroup.RegisterGroup("QoLCompendium:AnySink", original4);
    RecipeAdditions.RegisterGroupClone(original4, "MetalSink");
    RecipeGroup original5 = new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.Tables")}"), GetItems(32 /*0x20*/, RecipeAdditions.tableItemRegex, new int[3]
    {
      2999,
      3000,
      3814
    }));
    RecipeGroup.RegisterGroup("QoLCompendium:AnyTable", original5);
    RecipeAdditions.RegisterGroupClone(original5, "WoodenTable");
    RecipeGroup original6 = new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.Chairs")}"), GetItems(34, RecipeAdditions.chairItemRegex, Array.Empty<int>()));
    RecipeGroup.RegisterGroup("QoLCompendium:AnyChair", original6);
    RecipeAdditions.RegisterGroupClone(original6, "WoodenChair");
    RecipeGroup original7 = new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.Doors")}"), intList2.ToArray());
    RecipeGroup.RegisterGroup("QoLCompendium:AnyDoor", original7);
    RecipeAdditions.RegisterGroupClone(original7, "WoodenDoor");
    RecipeGroup original8 = new RecipeGroup((Func<string>) (() => $"{any} {Lang.GetItemNameValue(8)}"), intList3.ToArray());
    RecipeGroup.RegisterGroup("QoLCompendium:AnyTorch", original8);
    RecipeAdditions.RegisterGroupClone(original8, "Torch");
    RecipeGroup original9 = new RecipeGroup((Func<string>) (() => $"{any} {Lang.GetItemNameValue(345)}"), new int[2]
    {
      345,
      1791 /*0x06FF*/
    });
    RecipeGroup.RegisterGroup("QoLCompendium:AnyCookingPot", original9);
    RecipeAdditions.RegisterGroupClone(original9, "CookingPot");
    RecipeGroup original10 = new RecipeGroup((Func<string>) (() => $"{any} {Lang.GetItemNameValue(354)}"), GetItems(354, RecipeAdditions.bookcaseItemRegex, Array.Empty<int>()));
    RecipeGroup.RegisterGroup("QoLCompendium:AnyBookcase", original10);
    RecipeAdditions.RegisterGroupClone(original10, "Bookcase");
    RecipeGroup.RegisterGroup("QoLCompendium:AnyTombstone", new RecipeGroup((Func<string>) (() => $"{any} {Lang.GetItemNameValue(321)}"), new int[11]
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
    }));
    RecipeGroup.RegisterGroup("QoLCompendium:GoldBars", new RecipeGroup((Func<string>) (() => $"{any} {Lang.GetItemNameValue(19)}"), new int[2]
    {
      19,
      706
    }));
    Common.AnyPirateBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyPirateBanner", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyPirateBanner")}"), new int[4]
    {
      3442,
      3443,
      3444,
      1676
    }));
    Common.AnyArmoredBonesBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyArmoredBonesBanner", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyArmoredBonesBanner")}"), new int[3]
    {
      2900,
      2930,
      2970
    }));
    Common.AnySlimeBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnySlimeBanner", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnySlimeBanner")}"), new int[20]
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
    }));
    Common.AnyHallowBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyHallowBanner", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyHallowBanner")}"), new int[10]
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
    }));
    Common.AnyCorruptionBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyCorruptionBanner", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyCorruptionBanner")}"), new int[10]
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
    }));
    Common.AnyCrimsonBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyCrimsonBanner", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyCrimsonBanner")}"), new int[12]
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
    }));
    Common.AnyJungleBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyJungleBanner", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyJungleBanner")}"), new int[17]
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
    }));
    Common.AnySnowBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnySnowBanner", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnySnowBanner")}"), new int[13]
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
    }));
    Common.AnyDesertBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyDesertBanner", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyDesertBanner")}"), new int[23]
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
    }));
    Common.AnyUnderworldBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyUnderworldBanner", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyUnderworldBanner")}"), new int[7]
    {
      1659,
      2944,
      1664,
      1638,
      1627,
      2943,
      3446
    }));
    Common.AnyBatBanner = RecipeGroup.RegisterGroup("QoLCompendium:AnyBatBanner", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.AnyBatBanner")}"), new int[9]
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
    }));
    RecipeGroup.RegisterGroup("QoLCompendium:FishingBobbers", new RecipeGroup((Func<string>) (() => $"{any} {Lang.GetItemNameValue(5139)}"), new int[8]
    {
      5139,
      5140,
      5144,
      5142,
      5141,
      5146,
      5145,
      5143
    }));
    RecipeGroup.RegisterGroup("QoLCompendium:WellFed", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.WellFed")}"), intList4.ToArray()));
    RecipeGroup.RegisterGroup("QoLCompendium:PlentySatisfied", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.PlentySatisfied")}"), intList5.ToArray()));
    RecipeGroup.RegisterGroup("QoLCompendium:ExquisitelyStuffed", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.ExquisitelyStuffed")}"), intList6.ToArray()));
    if (ModConditions.martainsOrderLoaded && intList7.Count > 0)
      RecipeGroup.RegisterGroup("QoLCompendium:GourmetFlavor", new RecipeGroup((Func<string>) (() => $"{any} {Language.GetTextValue("Mods.QoLCompendium.RecipeGroupNames.GourmetFlavor")}"), intList7.ToArray()));
    if (!ModConditions.thoriumLoaded)
      return;
    RecipeGroup.RegisterGroup("QoLCompendium:GrimPedestals", new RecipeGroup((Func<string>) (() => $"{any} {Lang.GetItemNameValue(Common.GetModItem(ModConditions.thoriumMod, "GrimPedestal"))}"), new int[2]
    {
      Common.GetModItem(ModConditions.thoriumMod, "GrimPedestal"),
      Common.GetModItem(ModConditions.thoriumMod, "GrimPedestalCrimson")
    }));

    int[] GetItems(int iconicItem, Regex regex, int[] ignore)
    {
      List<int> list = allItems.Where<int>((Func<int, bool>) (id => regex.IsMatch(ItemID.Search.GetName(id).ToLower()))).ToList<int>();
      list.Remove(iconicItem);
      list.Insert(0, iconicItem);
      foreach (int num in ignore)
        list.Remove(num);
      return list.ToArray();
    }
  }

  private static void RegisterGroupClone(RecipeGroup original, string groupName)
  {
    int key;
    if (RecipeGroup.recipeGroupIDs.TryGetValue(groupName, out key))
    {
      RecipeGroup recipeGroup = RecipeGroup.recipeGroups[key];
      original.ValidItems.UnionWith((IEnumerable<int>) recipeGroup.ValidItems);
      recipeGroup.ValidItems = original.ValidItems;
    }
    else
      RecipeGroup.RegisterGroup(groupName, new RecipeGroup(original.GetText, new int[1])
      {
        ValidItems = original.ValidItems
      });
  }
}
