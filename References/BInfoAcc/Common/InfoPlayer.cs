using BInfoAcc.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BInfoAcc.Common
{
	public class InfoPlayer : ModPlayer
	{
		public bool minionDisplay;
		public bool luckDisplay;
		public bool sentryDisplay;
		public bool comboDisplay;
		public bool spawnRateDisplay;
		public bool regenDisplay;
        public bool biomeDisplay;
        public bool aggroDisplay;
        public bool manaDisplay;

        // Used by some data trackers to reduce frequency of checks, to prevent flckering data and help reduce potential lag
        public int bufferedInfoUpdateTimer;
        // Used by some data trackers to cycle between different display states, similar to the weather radio displaying sandstorms
        public int displayTimer;

        // For spawn rate/cap
        public int spawnRate; // The actual spawn rate recorded
        public int spawnCap; // The actual spawn limit recorded
        static FieldInfo spawnRateFieldInfo; // Used in reflection for obtaining spawn rate
        static FieldInfo spawnCapFieldInfo; // Used in reflection for obtaining spawn limit

        // For combo
        public int hitCounter;
        public int comboTime;
        public int comboDmg;
        int expirationCounter = -1;

        // For biomes
        public List<string> biomeNames;

        // For mana; adapted from DPS meter
        public DateTime mpsStart;
        public DateTime mpsEnd;
        public DateTime mpsLastUse;
        public int mpsAmount;
        public bool mpsStarted;

        public override void ResetInfoAccessories()
		{
			minionDisplay = false;
			luckDisplay = false;
			sentryDisplay = false;
			comboDisplay = false;
			spawnRateDisplay = false;
			regenDisplay = false;
            biomeDisplay = false;
            aggroDisplay = false;
            manaDisplay = false;
        }

		public override void RefreshInfoAccessoriesFromTeamPlayers(Player otherPlayer)
		{
			if (otherPlayer.GetModPlayer<InfoPlayer>().minionDisplay)
			{
				minionDisplay = true;
			}
			if (otherPlayer.GetModPlayer<InfoPlayer>().luckDisplay)
			{
				luckDisplay = true;
			}
			if (otherPlayer.GetModPlayer<InfoPlayer>().sentryDisplay)
			{
				sentryDisplay = true;
			}
			if (otherPlayer.GetModPlayer<InfoPlayer>().comboDisplay)
			{
				comboDisplay = true;
			}
			if (otherPlayer.GetModPlayer<InfoPlayer>().spawnRateDisplay)
			{
				spawnRateDisplay = true;
			}
			if (otherPlayer.GetModPlayer<InfoPlayer>().regenDisplay)
			{
				regenDisplay = true;
			}
            if (otherPlayer.GetModPlayer<InfoPlayer>().manaDisplay)
            {
                manaDisplay = true;
            }
            if (otherPlayer.GetModPlayer<InfoPlayer>().aggroDisplay)
            {
                aggroDisplay = true;
            }
            if (otherPlayer.GetModPlayer<InfoPlayer>().biomeDisplay)
            {
                biomeDisplay = true;
            }
        }

        public override void PreUpdate()
        {
            if(bufferedInfoUpdateTimer > 0)
            {
                bufferedInfoUpdateTimer--;
			}

            displayTimer += (int)(ModContent.GetInstance<ConfigServer>().cycleRate * 4f);
            if (displayTimer > 4000)
            {
                displayTimer = 1;
            }

            // Handles combo display info
            if (hitCounter > 0 && expirationCounter > 0 && !Player.dead && Player.active)
            {
                expirationCounter--;
                comboTime++;
            }
            else
            {
                hitCounter = 0;
                comboTime = 0;
                comboDmg = 0;
                expirationCounter = -1;
            }
        }

        public override void PostUpdateEquips()
        {
            if (!manaDisplay && mpsStarted) // Adapted from DPS meter
            {
                mpsStarted = false;
                mpsEnd = DateTime.Now;
            }
        }

        public override void PostUpdate()
        {
            if(bufferedInfoUpdateTimer <= 0)
            {
                bufferedInfoUpdateTimer = ModContent.GetInstance<ConfigServer>().refreshRate;

                if(spawnRateDisplay)
                {
                    // !Reflection!
                    spawnRateFieldInfo = typeof(NPC).GetField("spawnRate", BindingFlags.Static | BindingFlags.NonPublic);
                    spawnCapFieldInfo = typeof(NPC).GetField("maxSpawns", BindingFlags.Static | BindingFlags.NonPublic);

                    spawnRate = (int)spawnRateFieldInfo.GetValue(null);
                    spawnCap = (int)spawnCapFieldInfo.GetValue(null);
                }

                if(biomeDisplay)
                {
                    Point tilePos = Player.Center.ToTileCoordinates();
                    biomeNames = GetBiomes(Main.tile[tilePos.X, tilePos.Y]);
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            IncrementHitCounter(damageDone);
        }

        private void IncrementHitCounter(int dmg)
        {
            if (Player.dead || !Player.active)
            {
                return;
            }
            hitCounter++;
            comboDmg += dmg;

            // Hard cap values to avoid integer limit
            if (hitCounter >= int.MaxValue - 1 || hitCounter < 0)
            {
                hitCounter = int.MaxValue - 1;
            }
            if (comboDmg >= int.MaxValue - 1 || comboDmg < 0)
            {
                comboDmg = int.MaxValue - 1;
            }
            expirationCounter = 180;
        }

        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            AddMPS(manaConsumed);
        }

        // Mana Display functions; adapted from DPS meter
        public void AddMPS(int mana) 
        {
            if (mpsStarted)
            {
                mpsLastUse = DateTime.Now;
                mpsAmount += mana;
                mpsEnd = DateTime.Now;
            }
            else
            {
                mpsStarted = true;
                mpsStart = DateTime.Now;
                mpsEnd = DateTime.Now;
                mpsLastUse = DateTime.Now;
                mpsAmount = mana;
            }
        }

        public void CheckMPSTime()
        {
            if (mpsStarted)
            {
                TimeSpan val = DateTime.Now - mpsLastUse;
                if (val.Seconds > 1 || (Player.manaRegenDelay <= 0 && Player.manaRegen > 0))
                {
                    mpsStarted = false;
                }
            }
        }

        public int GetMPS()
        {
            TimeSpan timeSpan = mpsEnd - mpsStart;
            float num = (float)(timeSpan.Milliseconds / 1000f);
            num += (float)(timeSpan.Seconds);
            num += (float)(timeSpan.Minutes / 60f);
            if (num >= 3f)
            {
                mpsStart = DateTime.Now;
                mpsStart = mpsStart.AddSeconds(-1.0);
                mpsAmount = (int)((float)mpsAmount / num);
                timeSpan = mpsEnd - mpsStart;

                num = (float)(timeSpan.Milliseconds / 1000f);
                num += (float)(timeSpan.Seconds);
                num += (float)(timeSpan.Minutes / 60f);
            }
            if (num < 1f)
            {
                num = 1f;
            }
            return (int)((float)mpsAmount / num);
        }

        // Helper methods for biome display
        private List<string> GetBiomes(Tile plrCenterTile)
        {
            List<string> biomeNames = new List<string>();

            // Towns have highest prio
            if (Player.townNPCs > 2f && !Player.ZoneShadowCandle)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Town");
            }
            // Space and Underworld have high prio, but depend on the depth meter
            if (Player.accDepthMeter != 0)
            {
                if (Player.ZoneUnderworldHeight)
                {
                    if(TryCheckInBiome("TheDepths", "DepthsBiome")) // The Depths mod compat.
                    {
                        AddBiomeName(ref biomeNames, "Depths.Depths");
                    }
                    else
                    {
                        AddBiomeName(ref biomeNames, "Vanilla.Underworld");
                    }
                }
                if (Player.ZoneSkyHeight)
                {
                    AddBiomeName(ref biomeNames, "Vanilla.Space");
                }
            }
            if (ModContent.GetInstance<ConfigServer>().prioModBiomes)
            {
                AddModdedBiomeNames(ref biomeNames);
                AddVanillaBiomeNames(ref biomeNames, plrCenterTile);
            }
            else
            {
                AddVanillaBiomeNames(ref biomeNames, plrCenterTile);
                AddModdedBiomeNames(ref biomeNames);
            }
            // Underground/Cavern has lowest prio. Also dependent on depth meter like space and underworld
            if ((Player.ZoneRockLayerHeight || Player.ZoneDirtLayerHeight || Player.ZoneUndergroundDesert)
                && Player.accDepthMeter != 0)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Underground");
            }

            return biomeNames;
        }

        private void AddVanillaBiomeNames(ref List<string> biomeNames, Tile plrCenterTile)
        {
            // High prio
            if (Player.ZoneTowerSolar)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Solar");
            }
            if (Player.ZoneTowerVortex)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Vortex");
            }
            if (Player.ZoneTowerNebula)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Nebula");
            }
            if (Player.ZoneTowerStardust)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Stardust");
            }
            if (Player.ZoneMeteor)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Meteor");
            }
            if (Player.ZoneDungeon)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Dungeon");
            }
            if (Player.ZoneLihzhardTemple || plrCenterTile.WallType == WallID.LihzahrdBrickUnsafe)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Temple", ModLoader.HasMod("Remnants"));
            }
            if (Player.ZoneShimmer)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Aether");
            }
            if (plrCenterTile.WallType == WallID.SpiderUnsafe) // Spider
            {
                AddBiomeName(ref biomeNames, "Vanilla.Spider");
            }

            // Standard prio
            if (Player.ZoneCorrupt)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Corruption", true);
            }
            if (Player.ZoneCrimson)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Crimson", true);
            }
            if (Player.ZoneGlowshroom)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Shroom");
            }
            if (Player.ZoneJungle)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Jungle");
            }
            if (Player.ZoneHallow)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Hallow", true);
            }
            if (Player.ZoneBeach)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Ocean", ModLoader.HasMod("Remnants"));
            }
            if (Player.ZoneSnow)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Snow", true);
            }
            if (Player.ZoneDesert || Player.ZoneUndergroundDesert)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Desert", true);
            }

            // Low prio
            if (Player.ZoneHive)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Beehive", ModLoader.HasMod("Remnants"));
            }
            if (Player.ZoneGranite)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Granite");
            }
            if (Player.ZoneMarble)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Marble", ModLoader.HasMod("Remnants"));
            }
            if (Player.ZoneGraveyard &&
                !(Player.ZoneUnderworldHeight || Player.ZoneRockLayerHeight || Player.ZoneDirtLayerHeight))
            {
                AddBiomeName(ref biomeNames, "Vanilla.Graveyard");
            }
            else if (Player.ZoneForest)
            {
                AddBiomeName(ref biomeNames, "Vanilla.Forest");
            }
        }

        // Mod support
        private void AddModdedBiomeNames(ref List<string> biomeNames)
        {      
            AddStarsAboveBiomeNames(ref biomeNames);
            AddCalamityBiomeNames(ref biomeNames);
            AddThoriumBiomeNames(ref biomeNames);
            AddRedemptionBiomeNames(ref biomeNames);
            AddAvalonBiomeNames(ref biomeNames);
            AddSpiritBiomeNames(ref biomeNames); 
            AddConfectionBiomeNames(ref biomeNames);
            AddSpookyBiomeNames(ref biomeNames);
            AddAequusBiomeNames(ref biomeNames);
            AddVerdantBiomeNames(ref biomeNames);
            AddValhallaBiomeNames(ref biomeNames);
            AddRiseOfAgesBiomeNames(ref biomeNames);
            AddRemnantsBiomeNames(ref biomeNames);
            AddFFFBiomeNames(ref biomeNames);
        }

        private void AddThoriumBiomeNames(ref List<string> biomeNames)
        {
            string modName = "ThoriumMod";
            if (TryCheckInBiome(modName, "DepthsBiome")
                && Player.position.Y > Main.worldSurface * 16)
            {
                AddBiomeName(ref biomeNames, "Thorium.Depths");
            }
        }

        private void AddCalamityBiomeNames(ref List<string> biomeNames)
        {
            string modName = "CalamityMod";

            // Astral Infection
            if(TryCheckInBiome(modName, "AstralInfectionBiome"))
            {
                AddBiomeName(ref biomeNames, "Calamity.Astral", true);
            }
            if (TryCheckInBiome(modName, "AbovegroundAstralSnowBiome"))
            {
                AddBiomeName(ref biomeNames, "Calamity.Astral", true);
                AddBiomeName(ref biomeNames, "Vanilla.Snow", true);
            }
            if (TryCheckInBiome(modName, "AbovegroundAstralDesertBiome"))
            {
                AddBiomeName(ref biomeNames, "Calamity.Astral", true);
                AddBiomeName(ref biomeNames, "Vanilla.Desert", true);
            }
            if (TryCheckInBiome(modName, "UndergroundAstralBiome"))
            {
                AddBiomeName(ref biomeNames, "Calamity.Astral", true);
                AddBiomeName(ref biomeNames, "Vanilla.Underground", true);
            }

            if (TryCheckInBiome(modName, "BrimstoneCragsBiome"))
            {
                AddBiomeName(ref biomeNames, "Calamity.Crags");
            }
            if (TryCheckInBiome(modName, "SulphurousSeaBiome"))
            {
                AddBiomeName(ref biomeNames, "Calamity.SulphurousSea");
                RemoveBiomeName(ref biomeNames, "Vanilla.Ocean");
            }
            if (TryCheckInBiome(modName, "SunkenSeaBiome"))
            {
                AddBiomeName(ref biomeNames, "Calamity.SunkenSea");
            }

            // The Abyss   
            if (TryCheckInBiome(modName, "AbyssLayer4Biome"))
            {
                AddBiomeName(ref biomeNames, "Calamity.AbyssMain");
                AddBiomeName(ref biomeNames, "Calamity.Abyss4");
            }
            else if (TryCheckInBiome(modName, "AbyssLayer3Biome"))
            {
                AddBiomeName(ref biomeNames, "Calamity.AbyssMain");
                AddBiomeName(ref biomeNames, "Calamity.Abyss3");
            }
            else if (TryCheckInBiome(modName, "AbyssLayer2Biome"))
            {
                AddBiomeName(ref biomeNames, "Calamity.AbyssMain");
                AddBiomeName(ref biomeNames, "Calamity.Abyss2");
            }
            else if (TryCheckInBiome(modName, "AbyssLayer1Biome"))
            {
                AddBiomeName(ref biomeNames, "Calamity.AbyssMain");
                AddBiomeName(ref biomeNames, "Calamity.Abyss1");
            }
        }

        private void AddRedemptionBiomeNames(ref List<string> biomeNames)
        {
            string modName = "Redemption";
            if (TryCheckInBiome(modName, "LabBiome"))
            {
                AddBiomeName(ref biomeNames, "Redemption.Lab");
            }
            if (TryCheckInBiome(modName, "SoullessBiome"))
            {
                AddBiomeName(ref biomeNames, "Redemption.Soulless");
            }


            if (TryCheckInBiome(modName, "WastelandCorruptionBiome"))
            {
                AddBiomeName(ref biomeNames, "Redemption.Wasteland", true);
                AddBiomeName(ref biomeNames, "Vanilla.Corruption", true);
            }
            if (TryCheckInBiome(modName, "WastelandCrimsonBiome"))
            {
                AddBiomeName(ref biomeNames, "Redemption.Wasteland", true);
                AddBiomeName(ref biomeNames, "Vanilla.Crimson", true);
            }
            if (TryCheckInBiome(modName, "WastelandSnowBiome"))
            {
                AddBiomeName(ref biomeNames, "Redemption.Wasteland", true);
                AddBiomeName(ref biomeNames, "Vanilla.Snow", true);
            }
            if (TryCheckInBiome(modName, "WastelandDesertBiome"))
            {
                AddBiomeName(ref biomeNames, "Redemption.Wasteland", true);
                AddBiomeName(ref biomeNames, "Vanilla.Desert", true);
            }
            if (TryCheckInBiome(modName, "WastelandPurityBiome"))
            {
                AddBiomeName(ref biomeNames, "Redemption.Wasteland", true);
            }
        }

        private void AddAequusBiomeNames(ref List<string> biomeNames)
        {
            string modName = "Aequus";
            if (TryCheckInBiome(modName, "CrabCreviceBiome"))
            {
                AddBiomeName(ref biomeNames, "Aequus.Crevice");
            }
        }

        private void AddConfectionBiomeNames(ref List<string> biomeNames)
        {
            string modName = "TheConfectionRebirth";
            if (TryCheckInBiome(modName, "SandConfectionUndergroundBiome"))
            {
                AddBiomeName(ref biomeNames, "Confection.Confection", true);
                AddBiomeName(ref biomeNames, "Vanilla.Desert", true);
            }
            else if (TryCheckInBiome(modName, "SandConfectionSurfaceBiome"))
            {
                AddBiomeName(ref biomeNames, "Confection.Confection", true);
                AddBiomeName(ref biomeNames, "Vanilla.Desert", true);
            }
            if (TryCheckInBiome(modName, "IceConfectionUndergroundBiome"))
            {
                AddBiomeName(ref biomeNames, "Confection.Confection", true);
                AddBiomeName(ref biomeNames, "Vanilla.Snow", true);
            }
            else if (TryCheckInBiome(modName, "IceConfectionSurfaceBiome"))
            {
                AddBiomeName(ref biomeNames, "Confection.Confection", true);
                AddBiomeName(ref biomeNames, "Vanilla.Snow", true);
            }
            if (TryCheckInBiome(modName, "ConfectionUndergroundBiome"))
            {
                AddBiomeName(ref biomeNames, "Confection.Confection", true);
            }
            else if (TryCheckInBiome(modName, "ConfectionBiome"))
            {
                AddBiomeName(ref biomeNames, "Confection.Confection", true);
            }
        }

        private void AddFFFBiomeNames(ref List<string> biomeNames)
        {
            string modName = "CosmeticVariety";
            if (TryCheckInBiome(modName, "CelestialSurfaceBiome"))
            {
                AddBiomeName(ref biomeNames, "FFF.Celestial");
            }
            if (TryCheckInBiome(modName, "GardenBiome"))
            {
                AddBiomeName(ref biomeNames, "FFF.Garden");
            }
            else if (TryCheckInBiome(modName, "GardenSurfaceBiome"))
            {
                AddBiomeName(ref biomeNames, "FFF.Garden");
            }
        }

        private void AddRemnantsBiomeNames(ref List<string> biomeNames)
        {
            string modName = "Remnants";
            // Vanilla biome overhauls
            if (TryCheckInBiome(modName, "OceanCave"))
            {
                AddBiomeName(ref biomeNames, "Vanilla.Ocean", true);
            }
            if (TryCheckInBiome(modName, "Hive"))
            {
                AddBiomeName(ref biomeNames, "Vanilla.Beehive", true);
            }
            if (TryCheckInBiome(modName, "JungleTemple"))
            {
                AddBiomeName(ref biomeNames, "Vanilla.Temple", true);
            }
            if (TryCheckInBiome(modName, "MarbleCave"))
            {
                AddBiomeName(ref biomeNames, "Vanilla.Marble", true);
            }
            // Remnants-exclusive
            if (TryCheckInBiome(modName, "AerialGarden"))
            {
                AddBiomeName(ref biomeNames, "Remnants.AerialGarden");
            }
            if (TryCheckInBiome(modName, "EchoingHalls"))
            {
                AddBiomeName(ref biomeNames, "Remnants.EchoingHalls");
            }
            if (TryCheckInBiome(modName, "ForgottenTomb"))
            {
                AddBiomeName(ref biomeNames, "Remnants.Tomb");
            }
            if (TryCheckInBiome(modName, "Undergrowth"))
            {
                AddBiomeName(ref biomeNames, "Remnants.Undergrowth");
            }
            if (TryCheckInBiome(modName, "MagicalLab"))
            {
                AddBiomeName(ref biomeNames, "Remnants.MagicLab");
            }
        }

        private void AddSpiritBiomeNames(ref List<string> biomeNames)
        {
            string modName = "SpiritMod"; // Spirit Classic
            if (TryCheckInBiome(modName, "AsteroidBiome"))
            {
                AddBiomeName(ref biomeNames, "Spirit.Asteroid", true);
            }
            if (TryCheckInBiome(modName, "SpiritUndergroundBiome"))
            {
                AddBiomeName(ref biomeNames, "Spirit.Spirit", true);
            }
            else if (TryCheckInBiome(modName, "SpiritSurfaceBiome"))
            {
                AddBiomeName(ref biomeNames, "Spirit.Spirit", true);
            }
            if (TryCheckInBiome(modName, "BriarUndergroundBiome"))
            {
                AddBiomeName(ref biomeNames, "Spirit.Reach", true);
            }
            else if(TryCheckInBiome(modName, "BriarSurfaceBiome"))
            {
                AddBiomeName(ref biomeNames, "Spirit.Reach", true);
            }

            modName = "SpiritReforged"; // Spirit Reforged
            if (TryCheckInBiome(modName, "SavannaBiome"))
            {
                AddBiomeName(ref biomeNames, "Spirit.Savanna", true);
            }
        }

        private void AddStarsAboveBiomeNames(ref List<string> biomeNames)
        {
            string modName = "StarsAbove";
            if (TryCheckInBiome(modName, "ObservatoryBiome"))
            {
                AddBiomeName(ref biomeNames, "StarsAbove.Observatory", true);
            }
            if (TryCheckInBiome(modName, "AstarteDriverBiome"))
            {
                AddBiomeName(ref biomeNames, "StarsAbove.Astarte", true);
            }
            if (TryCheckInBiome(modName, "BleachedWorldBiome"))
            {
                AddBiomeName(ref biomeNames, "StarsAbove.Bleached", true);
            }
            if (TryCheckInBiome(modName, "CorvusBiome"))
            {
                AddBiomeName(ref biomeNames, "StarsAbove.Corvus", true);
            }
            if (TryCheckInBiome(modName, "LyraBiome"))
            {
                AddBiomeName(ref biomeNames, "StarsAbove.Lyra", true);
            }
            if (TryCheckInBiome(modName, "FriendlySpaceBiome"))
            {
                AddBiomeName(ref biomeNames, "StarsAbove.Pyxis", true);
            }
            if (TryCheckInBiome(modName, "MoonBiome"))
            {
                AddBiomeName(ref biomeNames, "StarsAbove.Moon", true);
            }
            if (TryCheckInBiome(modName, "SeaOfStarsBiome"))
            {
                AddBiomeName(ref biomeNames, "StarsAbove.StarSea", true);
            }
        }

        private void AddSpookyBiomeNames(ref List<string> biomeNames)
        {
            string modName = "Spooky";
            if (TryCheckInBiome(modName, "CatacombBiome"))
            {
                AddBiomeName(ref biomeNames, "Spooky.Catacomb", true);
            }
            if (TryCheckInBiome(modName, "SpookyBiomeUg"))
            {
                AddBiomeName(ref biomeNames, "Spooky.Spooky", true);
            }
            else if (TryCheckInBiome(modName, "SpookyHellBiome"))
            {
                AddBiomeName(ref biomeNames, "Spooky.Spooky", true);
            }
            else if (TryCheckInBiome(modName, "SpookyBiome"))
            {
                AddBiomeName(ref biomeNames, "Spooky.Spooky", true);
            }
        }

        private void AddVerdantBiomeNames(ref List<string> biomeNames)
        {
            string modName = "Verdant";
            if (TryCheckInBiome(modName, "VerdantUndergroundBiome"))
            {
                AddBiomeName(ref biomeNames, "Verdant.Verdant", true);
            }
            else if (TryCheckInBiome(modName, "VerdantBiome"))
            {
                AddBiomeName(ref biomeNames, "Verdant.Verdant", true);
            }
        }

        private void AddValhallaBiomeNames(ref List<string> biomeNames)
        {
            string modName = "ValhallaMod";
            if (TryCheckInBiome(modName, "TarBiome"))
            {
                AddBiomeName(ref biomeNames, "Valhalla.Tar");
            }
        }

        private void AddAvalonBiomeNames(ref List<string> biomeNames)
        {
            string modName = "Avalon";

            // The Dungeon
            if (TryCheckInBiome(modName, "DungeonAltColors"))
            {
                AddBiomeName(ref biomeNames, "Vanilla.Dungeon", true);
            }

            // Dungeons
            if (TryCheckInBiome(modName, "Hellcastle"))
            {
                AddBiomeName(ref biomeNames, "Avalon.Hellcastle");
            }
            else if (TryCheckInBiome(modName, "NearHellcastle"))
            {
                AddBiomeName(ref biomeNames, "Avalon.PhantomGardens");
            }
            if (TryCheckInBiome(modName, "SkyFortress"))
            {
                AddBiomeName(ref biomeNames, "Avalon.SkyFortress");
            }

            // Contagion
            if (TryCheckInBiome(modName, "ContagionDesert") || 
                TryCheckInBiome(modName, "ContagionCaveDesert"))
            {
                AddBiomeName(ref biomeNames, "Avalon.Contagion", true);
                AddBiomeName(ref biomeNames, "Vanilla.Desert", true);
            }
            if (TryCheckInBiome(modName, "UndergroundContagionIce"))
            {
                AddBiomeName(ref biomeNames, "Avalon.Contagion", true);
                AddBiomeName(ref biomeNames, "Vanilla.Snow", true);
            }
            if (TryCheckInBiome(modName, "UndergroundContagion") ||
                TryCheckInBiome(modName, "Contagion"))
            {
                AddBiomeName(ref biomeNames, "Avalon.Contagion", true);
            }

            // Savanna/Tropics
            if (TryCheckInBiome(modName, "TuhrtlOutpost"))
            {
                AddBiomeName(ref biomeNames, "Avalon.TuhrtlOutpost");
            }
            if (TryCheckInBiome(modName, "UndergroundTropics") 
                || TryCheckInBiome(modName, "Tropics"))
            {
                AddBiomeName(ref biomeNames, "Avalon.Tropics", true);
            }

            // Misc
            if (TryCheckInBiome(modName, "DarkMatter"))
            {
                AddBiomeName(ref biomeNames, "Avalon.DarkMatter");
            }
            if (TryCheckInBiome(modName, "CaesiumBlastplains"))
            {
                AddBiomeName(ref biomeNames, "Avalon.Blastplains");
            }
            if (TryCheckInBiome(modName, "BasaltObeliskBiome"))
            {
                AddBiomeName(ref biomeNames, "Avalon.BasaltObelisk");
            }
        }

        private void AddRiseOfAgesBiomeNames(ref List<string> biomeNames)
        {
            string modName = "RoA";
            if (TryCheckInBiome(modName, "BackwoodsBiome"))
            {
                AddBiomeName(ref biomeNames, "RiseOfAges.Backwoods");
            }
        }
        //

        private bool TryCheckInBiome(string modName, string biomeName)
        {
            if (ModLoader.TryGetMod(modName, out Mod mod))
            {
                if (mod.TryFind(biomeName, out ModBiome biome))
                {
                    return Player.InModBiome(biome);
                }
                
            }
            return false;
        }

        private static void AddBiomeName(ref List<string> biomeNames, string keySuffix, bool noDuplicates = false)
        {
            string localizationKey = "Mods.BInfoAcc.Biomes." + keySuffix;

            if(noDuplicates)
            {
                if(biomeNames.Contains(Language.GetTextValue(localizationKey)))
                {
                    return;
                }
            }

            biomeNames.Add(Language.GetTextValue(localizationKey));
        }

        private static void RemoveBiomeName(ref List<string> biomeNames, string keySuffix)
        {
            string localizationKey = "Mods.BInfoAcc.Biomes." + keySuffix;

            biomeNames.RemoveAll(name => name == Language.GetTextValue(localizationKey));
        }

        // Q.O.L. 3000 cross-mod
        public override void UpdateEquips()
        {
            bool crossModActive = false;
            if(ModLoader.TryGetMod("BlockQoL", out Mod BlockQoL) && BlockQoL.Call("BankPassivesActive") is bool bankPassives && bankPassives)
            {
                crossModActive = true;
            }
            if (ModLoader.HasMod("GlobalTrinkets")) // Also adds compatibility with Global Trinkets, which does the same thing
            {
                crossModActive = true;
            }
            if (ModLoader.HasMod("Fargowiltas")) // Also adds compatibility with Fargo's Mutant, which does the same thing
            {
                crossModActive = true;
            }

            if (!crossModActive)
            {
                return;
            }

            bool hasPDA = Player.HasItemInAnyInventory(ItemID.PDA) || Player.HasItemInAnyInventory(ItemID.CellPhone) ||
                Player.HasItemInAnyInventory(ItemID.ShellphoneDummy) || Player.HasItemInAnyInventory(ItemID.Shellphone) ||
                Player.HasItemInAnyInventory(ItemID.ShellphoneHell) || Player.HasItemInAnyInventory(ItemID.ShellphoneSpawn) || Player.HasItemInAnyInventory(ItemID.ShellphoneOcean);

            // Thorium Cross-Mod
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium) && thorium.TryFind("WishingGlass", out ModItem wishingGlass)
                && Player.HasItemInAnyInventory(wishingGlass.Type))
            {
                hasPDA = true;
            }

            if(!ModContent.GetInstance<ConfigServer>().updatedPhones)
            {
                hasPDA = false;
            }

            if (hasPDA || Player.HasItemInAnyInventory(ModContent.ItemType<AttendanceLog>()) || Player.HasItemInAnyInventory(ModContent.ItemType<ScryingMirror>()))
            {
                minionDisplay = true;
            }
            if (hasPDA || Player.HasItemInAnyInventory(ModContent.ItemType<EngiRegistry>()) || Player.HasItemInAnyInventory(ModContent.ItemType<ScryingMirror>()))
            {
                sentryDisplay = true;
            }
            if (hasPDA || Player.HasItemInAnyInventory(ModContent.ItemType<FortuneMirror>()) || Player.HasItemInAnyInventory(ModContent.ItemType<ScryingMirror>()))
            {
                luckDisplay = true;
            }
            if (hasPDA || Player.HasItemInAnyInventory(ModContent.ItemType<SmartHeart>()) || Player.HasItemInAnyInventory(ModContent.ItemType<RSH>()))
            {
                regenDisplay = true;
            }
            if (hasPDA || Player.HasItemInAnyInventory(ModContent.ItemType<Magimeter>()) || Player.HasItemInAnyInventory(ModContent.ItemType<RSH>()))
            {
                manaDisplay = true;
            }
            if (hasPDA || Player.HasItemInAnyInventory(ModContent.ItemType<HitMarker>()) || Player.HasItemInAnyInventory(ModContent.ItemType<RSH>()))
            {
                comboDisplay = true;
            }
            if (hasPDA || Player.HasItemInAnyInventory(ModContent.ItemType<SafteyScanner>()) || Player.HasItemInAnyInventory(ModContent.ItemType<ThreatAnalyzer>()))
            {
                spawnRateDisplay = true;
            }
            if (hasPDA || Player.HasItemInAnyInventory(ModContent.ItemType<BiomeCrystal>()) || Player.HasItemInAnyInventory(ModContent.ItemType<ThreatAnalyzer>()))
            {
                biomeDisplay = true;
            }
            if (hasPDA || Player.HasItemInAnyInventory(ModContent.ItemType<WantedPoster>()) || Player.HasItemInAnyInventory(ModContent.ItemType<ThreatAnalyzer>()))
            {
                aggroDisplay = true;
            }
        }
    }
}