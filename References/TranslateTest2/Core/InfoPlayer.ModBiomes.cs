using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace TranslateTest2.Core
{
    public partial class InfoPlayer
    {
        /// <summary>
        /// Get all current biomes (Vanilla + MOD) for display.
        /// Exact reproduction of BInfoAcc logic.
        /// </summary>
        public List<string> GetBiomes(Player player, bool prioModBiomes = true)
        {
            List<string> biomes = new List<string>();

            // Add vanilla biomes first
            AddVanillaBiomeNames(player, biomes);

            // Add modded biomes
            AddModdedBiomeNames(player, biomes);

            // Priority handling: if prioModBiomes is true and we have both vanilla and mod biomes,
            // move mod biomes to the front
            if (prioModBiomes && biomes.Count > 1)
            {
                List<string> modBiomes = new List<string>();
                List<string> vanillaBiomes = new List<string>();

                foreach (string biome in biomes)
                {
                    // Check if it's a vanilla biome by trying to find it in Vanilla keys
                    if (IsVanillaBiome(biome))
                    {
                        vanillaBiomes.Add(biome);
                    }
                    else
                    {
                        modBiomes.Add(biome);
                    }
                }

                // Reconstruct list with mod biomes first
                biomes.Clear();
                biomes.AddRange(modBiomes);
                biomes.AddRange(vanillaBiomes);
            }

            return biomes;
        }

        /// <summary>
        /// Check if a biome name corresponds to a vanilla biome
        /// </summary>
        private bool IsVanillaBiome(string biomeName)
        {
            // All vanilla biome translations are under Mods.TranslateTest2.Biomes.Vanilla.*
            return biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Space") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Underworld") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Underground") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Solar") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Vortex") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Nebula") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Stardust") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Meteor") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Dungeon") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Temple") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Aether") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Spider") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Corruption") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Crimson") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Shroom") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Jungle") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Hallow") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Ocean") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Snow") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Desert") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Beehive") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Granite") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Marble") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Graveyard") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Forest") ||
                   biomeName == Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Town");
        }

        /// <summary>
        /// Add vanilla biome names to the list (exact reproduction of BInfoAcc logic)
        /// </summary>
        public void AddVanillaBiomeNames(Player player, List<string> biomes)
        {
            // Priority order based on BInfoAcc source code
            if (player.ZoneUnderworldHeight)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Underworld"));
            else if (player.ZoneSkyHeight)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Space"));
            else if (player.ZoneDirtLayerHeight)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Underground"));

            if (player.ZoneTowerSolar)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Solar"));
            if (player.ZoneTowerVortex)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Vortex"));
            if (player.ZoneTowerNebula)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Nebula"));
            if (player.ZoneTowerStardust)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Stardust"));

            if (player.ZoneMeteor)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Meteor"));
            if (player.ZoneDungeon)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Dungeon"));
            if (player.ZoneShimmer)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Aether"));
            
            // Exact tile-based detection for Spider and Temple walls (BInfoAcc parity)
            try
            {
                int tileX = (int)(player.Center.X / 16f);
                int tileY = (int)(player.Center.Y / 16f);
                tileX = Utils.Clamp(tileX, 0, Main.maxTilesX - 1);
                tileY = Utils.Clamp(tileY, 0, Main.maxTilesY - 1);
                var tile = Main.tile[tileX, tileY];
                if (tile != null)
                {
                    ushort wall = tile.WallType;
                    if (wall == WallID.SpiderUnsafe)
                        AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Spider"));
                    if (wall == WallID.LihzahrdBrickUnsafe || player.ZoneLihzhardTemple)
                        AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Temple"));
                }
            }
            catch { /* ignore tile read issues */ }

            if (player.ZoneCorrupt)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Corruption"));
            if (player.ZoneCrimson)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Crimson"));
            if (player.ZoneGlowshroom)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Shroom"));
            if (player.ZoneJungle)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Jungle"));
            if (player.ZoneHallow)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Hallow"));

            if (player.ZoneBeach)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Ocean"));
            if (player.ZoneSnow)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Snow"));
            if (player.ZoneDesert)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Desert"));
            if (player.ZoneHive)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Beehive"));
            if (player.ZoneGranite)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Granite"));
            if (player.ZoneMarble)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Marble"));
            if (player.ZoneGraveyard)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Graveyard"));

            if (player.ZoneForest)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Forest"));
            if (player.townNPCs > 2f && !player.ZoneShadowCandle)
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Town"));
        }

        /// <summary>
        /// Add modded biome names to the list (exact reproduction of BInfoAcc logic)
        /// </summary>
        public void AddModdedBiomeNames(Player player, List<string> biomes)
        {
            // Call individual MOD handlers in the same order as BInfoAcc
            AddCalamityBiomeNames(player, biomes);
            AddThoriumBiomeNames(player, biomes);
            AddRedemptionBiomeNames(player, biomes);
            AddAvalonBiomeNames(player, biomes);
            AddSpiritBiomeNames(player, biomes);
            AddStarsAboveBiomeNames(player, biomes);
            AddDepthsBiomeNames(player, biomes);
            AddSpookyBiomeNames(player, biomes);
            AddAequusBiomeNames(player, biomes);
            AddVerdantBiomeNames(player, biomes);
            AddValhallaBiomeNames(player, biomes);
            AddRiseOfAgesBiomeNames(player, biomes);
            AddRemnantsBiomeNames(player, biomes);
            AddConfectionBiomeNames(player, biomes);
            AddFFFBiomeNames(player, biomes);
        }

        #region Individual MOD Handlers (Exact reproduction from BInfoAcc)

        /// <summary>
        /// Add Calamity biome names - exact reproduction of BInfoAcc logic
        /// </summary>
    public void AddCalamityBiomeNames(Player player, List<string> biomes)
    {
            // Priority order from BInfoAcc source
            if (TryCheckInBiome("CalamityMod", "AbyssLayer4"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Calamity.AbyssLayer4"));
            else if (TryCheckInBiome("CalamityMod", "AbyssLayer3"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Calamity.AbyssLayer3"));
            else if (TryCheckInBiome("CalamityMod", "AbyssLayer2"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Calamity.AbyssLayer2"));
            else if (TryCheckInBiome("CalamityMod", "AbyssLayer1"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Calamity.AbyssLayer1"));

            if (TryCheckInBiome("CalamityMod", "AshenStalagmiteSubworld"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Calamity.AshenStalagmiteSubworld"));
            if (TryCheckInBiome("CalamityMod", "AstralInfection"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Calamity.AstralInfection"));
            if (TryCheckInBiome("CalamityMod", "Brimstone"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Calamity.Brimstone"));

            // Handle Ocean vs SulphurousSea conflict (exact BInfoAcc logic)
            if (TryCheckInBiome("CalamityMod", "SulphurousSea"))
            {
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Calamity.SulphurousSea"));
                RemoveBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Vanilla.Ocean"));
            }

            if (TryCheckInBiome("CalamityMod", "SunkenSea"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Calamity.SunkenSea"));
        }

        /// <summary>
        /// Add Thorium biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddThoriumBiomeNames(Player player, List<string> biomes)
        {
            if (TryCheckInBiome("ThoriumMod", "Depths"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Thorium.Depths"));
        }

        /// <summary>
        /// Add Redemption biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddRedemptionBiomeNames(Player player, List<string> biomes)
        {
            if (TryCheckInBiome("Redemption", "LabBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Redemption.LabBiome"));
            if (TryCheckInBiome("Redemption", "SlayerShipBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Redemption.SlayerShipBiome"));
            if (TryCheckInBiome("Redemption", "SoullessBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Redemption.SoullessBiome"));

            // Wasteland variants (exact BInfoAcc priority)
            if (TryCheckInBiome("Redemption", "WastelandCorruptBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Redemption.WastelandCorruptBiome"));
            else if (TryCheckInBiome("Redemption", "WastelandCrimsonBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Redemption.WastelandCrimsonBiome"));
            else if (TryCheckInBiome("Redemption", "WastelandPurityBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Redemption.WastelandPurityBiome"));
        }

        /// <summary>
        /// Add Avalon biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddAvalonBiomeNames(Player player, List<string> biomes)
        {
            if (TryCheckInBiome("Avalon", "Contagion"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Avalon.Contagion"));
            if (TryCheckInBiome("Avalon", "Desert"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Avalon.Desert"));
            if (TryCheckInBiome("Avalon", "Tropics"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Avalon.Tropics"));
        }

        /// <summary>
        /// Add Spirit biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddSpiritBiomeNames(Player player, List<string> biomes)
        {
            if (TryCheckInBiome("SpiritMod", "SpiritBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Spirit.SpiritBiome"));
        }

        /// <summary>
        /// Add StarsAbove biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddStarsAboveBiomeNames(Player player, List<string> biomes)
        {
            if (TryCheckInBiome("StarsAbove", "SeaOfStars"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.StarsAbove.SeaOfStars"));
        }

        /// <summary>
        /// Add Depths biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddDepthsBiomeNames(Player player, List<string> biomes)
        {
            if (TryCheckInBiome("TheDepths", "DepthsBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Depths.DepthsBiome"));
        }

        /// <summary>
        /// Add Spooky biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddSpookyBiomeNames(Player player, List<string> biomes)
        {
            if (TryCheckInBiome("Spooky", "SpiderCave"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Spooky.SpiderCave"));
            if (TryCheckInBiome("Spooky", "SpookyBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Spooky.SpookyBiome"));
            if (TryCheckInBiome("Spooky", "SpookyHell"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Spooky.SpookyHell"));
        }

        /// <summary>
        /// Add Aequus biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddAequusBiomeNames(Player player, List<string> biomes)
        {
            if (TryCheckInBiome("Aequus", "Crevice"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Aequus.Crevice"));
        }

        /// <summary>
        /// Add Verdant biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddVerdantBiomeNames(Player player, List<string> biomes)
        {
            if (TryCheckInBiome("Verdant", "VerdantBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Verdant.VerdantBiome"));
        }

        /// <summary>
        /// Add Valhalla biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddValhallaBiomeNames(Player player, List<string> biomes)
        {
            if (TryCheckInBiome("Valhalla", "GlacialDepths"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Valhalla.GlacialDepths"));
        }

        /// <summary>
        /// Add RiseOfAges biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddRiseOfAgesBiomeNames(Player player, List<string> biomes)
        {
            // Priority from BInfoAcc: Flooded variants take precedence over basic caves
            if (TryCheckInBiome("RiseofAges", "BiomeCavesFloodedWater"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.RiseOfAges.BiomeCavesFloodedWater"));
            else if (TryCheckInBiome("RiseofAges", "BiomeCavesFloodedLava"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.RiseOfAges.BiomeCavesFloodedLava"));
            else if (TryCheckInBiome("RiseofAges", "BiomeCaves"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.RiseOfAges.BiomeCaves"));

            if (TryCheckInBiome("RiseofAges", "BiomeDarkRealm"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.RiseOfAges.BiomeDarkRealm"));
            if (TryCheckInBiome("RiseofAges", "BiomeIndustrialEra"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.RiseOfAges.BiomeIndustrialEra"));
        }

        /// <summary>
        /// Add Remnants biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddRemnantsBiomeNames(Player player, List<string> biomes)
        {
            if (TryCheckInBiome("Remnants", "RemnantsBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Remnants.RemnantsBiome"));
        }

        /// <summary>
        /// Add Confection biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddConfectionBiomeNames(Player player, List<string> biomes)
        {
            if (TryCheckInBiome("TheConfectionRebirth", "SugarBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.Confection.SugarBiome"));
        }

        /// <summary>
        /// Add FFF biome names - exact reproduction of BInfoAcc logic
        /// </summary>
        public void AddFFFBiomeNames(Player player, List<string> biomes)
        {
            if (TryCheckInBiome("FargowiltasSouls", "BloomingGarden"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.FFF.BloomingGarden"));
            if (TryCheckInBiome("FargowiltasSouls", "BlueMushroomBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.FFF.BlueMushroomBiome"));
            if (TryCheckInBiome("FargowiltasSouls", "CandyCane"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.FFF.CandyCane"));
            if (TryCheckInBiome("FargowiltasSouls", "CanopyBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.FFF.CanopyBiome"));
            if (TryCheckInBiome("FargowiltasSouls", "DeepFarmBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.FFF.DeepFarmBiome"));
            if (TryCheckInBiome("FargowiltasSouls", "DustBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.FFF.DustBiome"));
            if (TryCheckInBiome("FargowiltasSouls", "FadeBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.FFF.FadeBiome"));
            if (TryCheckInBiome("FargowiltasSouls", "FlowerFieldBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.FFF.FlowerFieldBiome"));
            if (TryCheckInBiome("FargowiltasSouls", "FrozenBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.FFF.FrozenBiome"));
            if (TryCheckInBiome("FargowiltasSouls", "LushCaveBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.FFF.LushCaveBiome"));
            if (TryCheckInBiome("FargowiltasSouls", "PurpleBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.FFF.PurpleBiome"));
            if (TryCheckInBiome("FargowiltasSouls", "ShallowCaveBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.FFF.ShallowCaveBiome"));
            if (TryCheckInBiome("FargowiltasSouls", "TechBiome"))
                AddBiomeName(biomes, Language.GetTextValue("Mods.TranslateTest2.Biomes.FFF.TechBiome"));
        }

        #endregion

        #region Helper Methods (Exact reproduction from BInfoAcc)

        /// <summary>
        /// Check if player is in a specific MOD biome using runtime detection
        /// Exact reproduction of BInfoAcc TryCheckInBiome logic
        /// </summary>
    public bool TryCheckInBiome(string modName, string biomeName)
        {
            try
            {
                if (!ModLoader.TryGetMod(modName, out Mod mod))
                    return false;

                // Try to get the ModBiome instance
                if (!mod.TryFind<ModBiome>(biomeName, out ModBiome modBiome))
                    return false;

                // Check if current player is in this biome
                return Player.InModBiome(modBiome);
            }
            catch (Exception)
            {
                // If any error occurs, return false (MOD not available or biome doesn't exist)
                return false;
            }
        }

        /// <summary>
        /// Add a biome name to the list if it's not already present
        /// Exact reproduction of BInfoAcc AddBiomeName logic
        /// </summary>
        public void AddBiomeName(List<string> biomes, string biomeName)
        {
            if (!string.IsNullOrEmpty(biomeName) && !biomes.Contains(biomeName))
            {
                biomes.Add(biomeName);
            }
        }

        /// <summary>
        /// Remove a biome name from the list if present
        /// Exact reproduction of BInfoAcc RemoveBiomeName logic
        /// </summary>
        public void RemoveBiomeName(List<string> biomes, string biomeName)
        {
            if (!string.IsNullOrEmpty(biomeName))
            {
                biomes.Remove(biomeName);
            }
        }

        #endregion
    }
}
