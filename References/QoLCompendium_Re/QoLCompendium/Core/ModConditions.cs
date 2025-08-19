using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Humanizer;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace QoLCompendium.Core
{
	// Token: 0x02000208 RID: 520
	public class ModConditions : ModSystem
	{
		// Token: 0x06000BEA RID: 3050 RVA: 0x0004FAB2 File Offset: 0x0004DCB2
		public static Condition ItemToggled(string displayText, Func<bool> toggle)
		{
			return new Condition(Language.GetTextValue(displayText), toggle);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0004FAC0 File Offset: 0x0004DCC0
		public static Recipe GetItemRecipe(Func<bool> toggle, int itemType, int amount = 1, string displayText = "")
		{
			Recipe recipe = Recipe.Create(itemType, amount);
			recipe.AddCondition(ModConditions.ItemToggled(displayText, toggle));
			return recipe;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0004FAD8 File Offset: 0x0004DCD8
		public static Condition ItemObtained(Item item)
		{
			return new Condition(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.ModConditions.ItemObtained"), new object[]
			{
				item.ResearchUnlockCount
			}), () => ModConditions.ItemHasBeenOwned[item.type]);
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0004FB2B File Offset: 0x0004DD2B
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x0004FB32 File Offset: 0x0004DD32
		public static bool[] DownedBoss
		{
			get
			{
				return ModConditions.downedBoss;
			}
			set
			{
				ModConditions.downedBoss = value;
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0004FB3A File Offset: 0x0004DD3A
		public override void Unload()
		{
			ModConditions.DownedBoss = null;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0004FB42 File Offset: 0x0004DD42
		public override void OnWorldLoad()
		{
			ModConditions.ResetDowned();
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0004FB42 File Offset: 0x0004DD42
		public override void OnWorldUnload()
		{
			ModConditions.ResetDowned();
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0004FB4C File Offset: 0x0004DD4C
		public override void PostUpdateEverything()
		{
			if (Main.netMode == 1 || Main.netMode == 2 || QoLCompendium.mainConfig.RemoveBiomeShopRequirements)
			{
				ModConditions.beenThroughNight = true;
				ModConditions.beenToPurity = true;
				ModConditions.beenToCavernsOrUnderground = true;
				ModConditions.beenToUnderworld = true;
				ModConditions.beenToSky = true;
				ModConditions.beenToSnow = true;
				ModConditions.beenToDesert = true;
				ModConditions.beenToOcean = true;
				ModConditions.beenToJungle = true;
				ModConditions.beenToMushroom = true;
				ModConditions.beenToCorruption = true;
				ModConditions.beenToCrimson = true;
				ModConditions.beenToHallow = true;
				ModConditions.beenToTemple = true;
				ModConditions.beenToDungeon = true;
				ModConditions.beenToAether = true;
				ModConditions.beenToCrabCrevice = true;
				ModConditions.beenToCrags = true;
				ModConditions.beenToAstral = true;
				ModConditions.beenToSunkenSea = true;
				ModConditions.beenToSulphurSea = true;
				ModConditions.beenToAbyss = true;
				ModConditions.beenToAbyssLayer1 = true;
				ModConditions.beenToAbyssLayer2 = true;
				ModConditions.beenToAbyssLayer3 = true;
				ModConditions.beenToAbyssLayer4 = true;
				ModConditions.beenToAstralBlight = true;
				ModConditions.beenToConfection = true;
				ModConditions.beenToDepths = true;
				ModConditions.beenToJadeLake = true;
				ModConditions.beenToContagion = true;
				ModConditions.beenToDread = true;
				ModConditions.beenToHomewardAbyss = true;
				ModConditions.beenToProfanedGardens = true;
				ModConditions.beenToLunarVeilAbyss = true;
				ModConditions.beenToAcid = true;
				ModConditions.beenToAurelus = true;
				ModConditions.beenToFable = true;
				ModConditions.beenToGovheilCastle = true;
				ModConditions.beenToCathedral = true;
				ModConditions.beenToMarrowSurface = true;
				ModConditions.beenToMorrowUnderground = true;
				ModConditions.beenToSkyFortress = true;
				ModConditions.beenToLab = true;
				ModConditions.beenToWasteland = true;
				ModConditions.beenToPyramid = true;
				ModConditions.beenToPlanetarium = true;
				ModConditions.beenToCinderForest = true;
				ModConditions.beenToBriar = true;
				ModConditions.beenToSpirit = true;
				ModConditions.beenToSpookyForest = true;
				ModConditions.beenToSpookyUnderground = true;
				ModConditions.beenToEyeValley = true;
				ModConditions.beenToSpiderCave = true;
				ModConditions.beenToCatacombs = true;
				ModConditions.beenToCemetery = true;
				ModConditions.beenToAuroracleTemple = true;
				ModConditions.beenToVitricDesert = true;
				ModConditions.beenToVitricTemple = true;
				ModConditions.beenToAquaticDepths = true;
				ModConditions.beenToVerdant = true;
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0004FCE8 File Offset: 0x0004DEE8
		public override void PreUpdatePlayers()
		{
			if (Main.bloodMoon && !this.waitForBloodMoon)
			{
				this.waitForBloodMoon = true;
			}
			if (this.waitForBloodMoon && !Main.bloodMoon && Main.dayTime)
			{
				ModConditions.downedBloodMoon = true;
			}
			if (this.waitForBloodMoon && !Main.bloodMoon && !Main.dayTime)
			{
				this.waitForBloodMoon = false;
			}
			if (Main.eclipse && !this.waitForEclipse)
			{
				this.waitForEclipse = true;
			}
			if (this.waitForEclipse && !Main.eclipse && !Main.dayTime)
			{
				ModConditions.downedEclipse = true;
			}
			if (this.waitForEclipse && !Main.eclipse && Main.dayTime)
			{
				this.waitForEclipse = false;
			}
			if (NPC.downedTowerNebula && NPC.downedTowerSolar && NPC.downedTowerStardust && NPC.downedTowerVortex)
			{
				ModConditions.downedLunarEvent = true;
			}
			if (!Main.dayTime)
			{
				ModConditions.beenThroughNight = true;
			}
			if (Main.LocalPlayer.ZoneForest)
			{
				ModConditions.beenToPurity = true;
			}
			if (Main.LocalPlayer.ZoneNormalCaverns || Main.LocalPlayer.ZoneNormalUnderground)
			{
				ModConditions.beenToCavernsOrUnderground = true;
			}
			if (Main.LocalPlayer.ZoneUnderworldHeight)
			{
				ModConditions.beenToUnderworld = true;
			}
			if (Main.LocalPlayer.ZoneSkyHeight)
			{
				ModConditions.beenToSky = true;
			}
			if (Main.LocalPlayer.ZoneSnow)
			{
				ModConditions.beenToSnow = true;
			}
			if (Main.LocalPlayer.ZoneDesert || Main.LocalPlayer.ZoneUndergroundDesert)
			{
				ModConditions.beenToDesert = true;
			}
			if (Main.LocalPlayer.ZoneBeach)
			{
				ModConditions.beenToOcean = true;
			}
			if (Main.LocalPlayer.ZoneJungle)
			{
				ModConditions.beenToJungle = true;
			}
			if (Main.LocalPlayer.ZoneGlowshroom)
			{
				ModConditions.beenToMushroom = true;
			}
			if (Main.LocalPlayer.ZoneCorrupt)
			{
				ModConditions.beenToCorruption = true;
			}
			if (Main.LocalPlayer.ZoneCrimson)
			{
				ModConditions.beenToCrimson = true;
			}
			if (Main.LocalPlayer.ZoneHallow)
			{
				ModConditions.beenToHallow = true;
			}
			if (Main.LocalPlayer.ZoneLihzhardTemple)
			{
				ModConditions.beenToTemple = true;
			}
			if (Main.LocalPlayer.ZoneDungeon)
			{
				ModConditions.beenToDungeon = true;
			}
			if (Main.LocalPlayer.ZoneShimmer)
			{
				ModConditions.beenToAether = true;
			}
			if (ModConditions.aequusLoaded)
			{
				ModConditions.downedCrabson = (bool)ModConditions.aequusMod.Call(new object[]
				{
					"downedCrabson"
				});
				ModConditions.downedOmegaStarite = (bool)ModConditions.aequusMod.Call(new object[]
				{
					"downedOmegaStarite"
				});
				ModConditions.downedDustDevil = (bool)ModConditions.aequusMod.Call(new object[]
				{
					"downedDustDevil"
				});
				ModConditions.downedRedSprite = (bool)ModConditions.aequusMod.Call(new object[]
				{
					"downedRedSprite"
				});
				ModConditions.downedSpaceSquid = (bool)ModConditions.aequusMod.Call(new object[]
				{
					"downedSpaceSquid"
				});
				ModConditions.downedHyperStarite = (bool)ModConditions.aequusMod.Call(new object[]
				{
					"downedHyperStarite"
				});
				ModConditions.downedUltraStarite = (bool)ModConditions.aequusMod.Call(new object[]
				{
					"downedUltraStarite"
				});
				ModConditions.downedDemonSiege = (bool)ModConditions.aequusMod.Call(new object[]
				{
					"downedEventDemon"
				});
				ModConditions.downedGlimmer = (bool)ModConditions.aequusMod.Call(new object[]
				{
					"downedEventCosmic"
				});
				ModConditions.downedGaleStreams = (bool)ModConditions.aequusMod.Call(new object[]
				{
					"downedEventAtmosphere"
				});
				ModBiome CrabCreviceBiome;
				if (ModConditions.aequusMod.TryFind<ModBiome>("CrabCreviceBiome", out CrabCreviceBiome) && Main.LocalPlayer.InModBiome(CrabCreviceBiome))
				{
					ModConditions.beenToCrabCrevice = true;
				}
			}
			if (ModConditions.calamityLoaded)
			{
				ModConditions.downedDesertScourge = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"DesertScourge"
				});
				ModConditions.downedCrabulon = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"Crabulon"
				});
				ModConditions.downedHiveMind = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"HiveMind"
				});
				ModConditions.downedPerforators = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"Perforator"
				});
				ModConditions.downedSlimeGod = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"SlimeGod"
				});
				ModConditions.downedCryogen = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"Cryogen"
				});
				ModConditions.downedAquaticScourge = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"AquaticScourge"
				});
				ModConditions.downedBrimstoneElemental = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"BrimstoneElemental"
				});
				ModConditions.downedCalamitasClone = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"CalamitasClone"
				});
				ModConditions.downedLeviathanAndAnahita = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"AnahitaLeviathan"
				});
				ModConditions.downedAstrumAureus = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"AstrumAureus"
				});
				ModConditions.downedPlaguebringerGoliath = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"PlaguebringerGoliath"
				});
				ModConditions.downedRavager = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"Ravager"
				});
				ModConditions.downedAstrumDeus = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"AstrumDeus"
				});
				ModConditions.downedProfanedGuardians = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"Guardians"
				});
				ModConditions.downedDragonfolly = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"Dragonfolly"
				});
				ModConditions.downedProvidence = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"Providence"
				});
				ModConditions.downedStormWeaver = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"StormWeaver"
				});
				ModConditions.downedCeaselessVoid = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"CeaselessVoid"
				});
				ModConditions.downedSignus = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"Signus"
				});
				ModConditions.downedPolterghast = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"Polterghast"
				});
				ModConditions.downedOldDuke = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"OldDuke"
				});
				ModConditions.downedDevourerOfGods = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"DevourerOfGods"
				});
				ModConditions.downedYharon = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"Yharon"
				});
				ModConditions.downedExoMechs = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"ExoMechs"
				});
				ModConditions.downedSupremeCalamitas = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"SupremeCalamitas"
				});
				ModConditions.downedGiantClam = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"GiantClam"
				});
				ModConditions.downedCragmawMire = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"cragmawmire"
				});
				ModConditions.downedGreatSandShark = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"GreatSandShark"
				});
				ModConditions.downedMauler = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"mauler"
				});
				ModConditions.downedNuclearTerror = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"nuclearterror"
				});
				ModConditions.downedEidolonWyrm = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"primordialwyrm"
				});
				ModConditions.downedAcidRain1 = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"acidraineoc"
				});
				ModConditions.downedAcidRain2 = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"acidrainscourge"
				});
				ModConditions.downedBossRush = (bool)ModConditions.calamityMod.Call(new object[]
				{
					"GetBossDowned",
					"bossrush"
				});
				ModBiome AstralInfectionBiome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("AstralInfectionBiome", out AstralInfectionBiome) && Main.LocalPlayer.InModBiome(AstralInfectionBiome))
				{
					ModConditions.beenToAstral = true;
				}
				ModBiome AbyssLayer1Biome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer1Biome", out AbyssLayer1Biome) && Main.LocalPlayer.InModBiome(AbyssLayer1Biome))
				{
					ModConditions.beenToAbyss = true;
					ModConditions.beenToAbyssLayer1 = true;
				}
				ModBiome AbyssLayer2Biome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer2Biome", out AbyssLayer2Biome) && Main.LocalPlayer.InModBiome(AbyssLayer2Biome))
				{
					ModConditions.beenToAbyss = true;
					ModConditions.beenToAbyssLayer2 = true;
				}
				ModBiome AbyssLayer3Biome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer3Biome", out AbyssLayer3Biome) && Main.LocalPlayer.InModBiome(AbyssLayer3Biome))
				{
					ModConditions.beenToAbyss = true;
					ModConditions.beenToAbyssLayer3 = true;
				}
				ModBiome AbyssLayer4Biome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer4Biome", out AbyssLayer4Biome) && Main.LocalPlayer.InModBiome(AbyssLayer4Biome))
				{
					ModConditions.beenToAbyss = true;
					ModConditions.beenToAbyssLayer4 = true;
				}
				ModBiome BrimstoneCragsBiome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("BrimstoneCragsBiome", out BrimstoneCragsBiome) && Main.LocalPlayer.InModBiome(BrimstoneCragsBiome))
				{
					ModConditions.beenToCrags = true;
				}
				ModBiome SulphurousSeaBiome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("SulphurousSeaBiome", out SulphurousSeaBiome) && Main.LocalPlayer.InModBiome(SulphurousSeaBiome))
				{
					ModConditions.beenToSulphurSea = true;
				}
				ModBiome SunkenSeaBiome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("SunkenSeaBiome", out SunkenSeaBiome) && Main.LocalPlayer.InModBiome(SunkenSeaBiome))
				{
					ModConditions.beenToSunkenSea = true;
				}
			}
			ModBiome AstralBlight;
			if (ModConditions.calamityVanitiesLoaded && ModConditions.calamityVanitiesMod.TryFind<ModBiome>("AstralBlight", out AstralBlight) && Main.LocalPlayer.InModBiome(AstralBlight))
			{
				ModConditions.beenToAstralBlight = true;
			}
			ModBiome ConfectionBiome;
			ModBiome ConfectionUndergroundBiome;
			ModBiome IceConfectionSurfaceBiome;
			ModBiome IceConfectionUndergroundBiome;
			ModBiome SandConfectionSurfaceBiome;
			ModBiome SandConfectionUndergroundBiome;
			if (ModConditions.confectionRebakedLoaded && ((ModConditions.confectionRebakedMod.TryFind<ModBiome>("ConfectionBiome", out ConfectionBiome) && Main.LocalPlayer.InModBiome(ConfectionBiome)) || (ModConditions.confectionRebakedMod.TryFind<ModBiome>("ConfectionUndergroundBiome", out ConfectionUndergroundBiome) && Main.LocalPlayer.InModBiome(ConfectionUndergroundBiome)) || (ModConditions.confectionRebakedMod.TryFind<ModBiome>("IceConfectionSurfaceBiome", out IceConfectionSurfaceBiome) && Main.LocalPlayer.InModBiome(IceConfectionSurfaceBiome)) || (ModConditions.confectionRebakedMod.TryFind<ModBiome>("IceConfectionUndergroundBiome", out IceConfectionUndergroundBiome) && Main.LocalPlayer.InModBiome(IceConfectionUndergroundBiome)) || (ModConditions.confectionRebakedMod.TryFind<ModBiome>("SandConfectionSurfaceBiome", out SandConfectionSurfaceBiome) && Main.LocalPlayer.InModBiome(SandConfectionSurfaceBiome)) || (ModConditions.confectionRebakedMod.TryFind<ModBiome>("SandConfectionUndergroundBiome", out SandConfectionUndergroundBiome) && Main.LocalPlayer.InModBiome(SandConfectionUndergroundBiome))))
			{
				ModConditions.beenToConfection = true;
				ModConditions.beenToHallow = true;
			}
			ModBiome DepthsBiome;
			if (ModConditions.depthsLoaded && ModConditions.depthsMod.TryFind<ModBiome>("DepthsBiome", out DepthsBiome) && Main.LocalPlayer.InModBiome(DepthsBiome))
			{
				ModConditions.beenToDepths = true;
				ModConditions.beenToUnderworld = true;
			}
			ModBiome JadeLakeBiome;
			if (ModConditions.everjadeLoaded && ModConditions.everjadeMod.TryFind<ModBiome>("JadeLakeBiome", out JadeLakeBiome) && Main.LocalPlayer.InModBiome(JadeLakeBiome))
			{
				ModConditions.beenToJadeLake = true;
			}
			ModBiome Contagion;
			ModBiome UndergroundContagion;
			ModBiome ContagionDesert;
			ModBiome ContagionCaveDesert;
			if (ModConditions.exxoAvalonOriginsLoaded && ((ModConditions.exxoAvalonOriginsMod.TryFind<ModBiome>("Contagion", out Contagion) && Main.LocalPlayer.InModBiome(Contagion)) || (ModConditions.exxoAvalonOriginsMod.TryFind<ModBiome>("UndergroundContagion", out UndergroundContagion) && Main.LocalPlayer.InModBiome(UndergroundContagion)) || (ModConditions.exxoAvalonOriginsMod.TryFind<ModBiome>("ContagionDesert", out ContagionDesert) && Main.LocalPlayer.InModBiome(ContagionDesert)) || (ModConditions.exxoAvalonOriginsMod.TryFind<ModBiome>("ContagionCaveDesert", out ContagionCaveDesert) && Main.LocalPlayer.InModBiome(ContagionCaveDesert))))
			{
				ModConditions.beenToContagion = true;
			}
			ModBiome DreadSurfaceBiome;
			ModBiome DreadUndergroundBiome;
			if (ModConditions.fracturesOfPenumbraLoaded && ((ModConditions.fracturesOfPenumbraMod.TryFind<ModBiome>("DreadSurfaceBiome", out DreadSurfaceBiome) && Main.LocalPlayer.InModBiome(DreadSurfaceBiome)) || (ModConditions.fracturesOfPenumbraMod.TryFind<ModBiome>("DreadUndergroundBiome", out DreadUndergroundBiome) && Main.LocalPlayer.InModBiome(DreadUndergroundBiome))))
			{
				ModConditions.beenToDread = true;
			}
			ModBiome AbyssUndergroundBiome;
			if (ModConditions.homewardJourneyLoaded && ModConditions.homewardJourneyMod.TryFind<ModBiome>("AbyssUndergroundBiome", out AbyssUndergroundBiome) && Main.LocalPlayer.InModBiome(AbyssUndergroundBiome))
			{
				ModConditions.beenToHomewardAbyss = true;
			}
			ModBiome ProfanedTempleBiome;
			if (ModConditions.infernumLoaded && ModConditions.infernumMod.TryFind<ModBiome>("ProfanedTempleBiome", out ProfanedTempleBiome) && Main.LocalPlayer.InModBiome(ProfanedTempleBiome))
			{
				ModConditions.beenToProfanedGardens = true;
			}
			if (ModConditions.lunarVeilLoaded)
			{
				ModBiome AbyssBiome;
				if (ModConditions.lunarVeilMod.TryFind<ModBiome>("AbyssBiome", out AbyssBiome) && Main.LocalPlayer.InModBiome(AbyssBiome))
				{
					ModConditions.beenToLunarVeilAbyss = true;
				}
				ModBiome AcidBiome;
				if (ModConditions.lunarVeilMod.TryFind<ModBiome>("AcidBiome", out AcidBiome) && Main.LocalPlayer.InModBiome(AcidBiome))
				{
					ModConditions.beenToAcid = true;
				}
				ModBiome AurelusBiome;
				if (ModConditions.lunarVeilMod.TryFind<ModBiome>("AurelusBiome", out AurelusBiome) && Main.LocalPlayer.InModBiome(AurelusBiome))
				{
					ModConditions.beenToAurelus = true;
				}
				ModBiome FableBiome;
				if (ModConditions.lunarVeilMod.TryFind<ModBiome>("FableBiome", out FableBiome) && Main.LocalPlayer.InModBiome(FableBiome))
				{
					ModConditions.beenToFable = true;
				}
				ModBiome GovheilCastle;
				if (ModConditions.lunarVeilMod.TryFind<ModBiome>("GovheilCastle", out GovheilCastle) && Main.LocalPlayer.InModBiome(GovheilCastle))
				{
					ModConditions.beenToGovheilCastle = true;
				}
				ModBiome CathedralBiome;
				if (ModConditions.lunarVeilMod.TryFind<ModBiome>("CathedralBiome", out CathedralBiome) && Main.LocalPlayer.InModBiome(CathedralBiome))
				{
					ModConditions.beenToCathedral = true;
				}
				ModBiome MarrowSurfaceBiome;
				if (ModConditions.lunarVeilMod.TryFind<ModBiome>("MarrowSurfaceBiome", out MarrowSurfaceBiome) && Main.LocalPlayer.InModBiome(MarrowSurfaceBiome))
				{
					ModConditions.beenToMarrowSurface = true;
				}
				ModBiome MorrowUndergroundBiome;
				if (ModConditions.lunarVeilMod.TryFind<ModBiome>("MorrowUndergroundBiome", out MorrowUndergroundBiome) && Main.LocalPlayer.InModBiome(MorrowUndergroundBiome))
				{
					ModConditions.beenToMorrowUnderground = true;
				}
			}
			ModBiome FortressBiome;
			if (ModConditions.qwertyLoaded && ModConditions.qwertyMod.TryFind<ModBiome>("FortressBiome", out FortressBiome) && Main.LocalPlayer.InModBiome(FortressBiome))
			{
				ModConditions.beenToSkyFortress = true;
			}
			if (ModConditions.redemptionLoaded)
			{
				ModBiome LabBiome;
				if (ModConditions.redemptionMod.TryFind<ModBiome>("LabBiome", out LabBiome) && Main.LocalPlayer.InModBiome(LabBiome))
				{
					ModConditions.beenToLab = true;
				}
				ModBiome WastelandPurityBiome;
				if (ModConditions.redemptionMod.TryFind<ModBiome>("WastelandPurityBiome", out WastelandPurityBiome) && Main.LocalPlayer.InModBiome(WastelandPurityBiome))
				{
					ModConditions.beenToWasteland = true;
				}
			}
			if (ModConditions.secretsOfTheShadowsLoaded)
			{
				ModBiome PyramidBiome;
				if (ModConditions.secretsOfTheShadowsMod.TryFind<ModBiome>("PyramidBiome", out PyramidBiome) && Main.LocalPlayer.InModBiome(PyramidBiome))
				{
					ModConditions.beenToPyramid = true;
				}
				ModBiome PlanetariumBiome;
				if (ModConditions.secretsOfTheShadowsMod.TryFind<ModBiome>("PlanetariumBiome", out PlanetariumBiome) && Main.LocalPlayer.InModBiome(PlanetariumBiome))
				{
					ModConditions.beenToPlanetarium = true;
				}
			}
			ModBiome CinderDesertBiome;
			ModBiome CinderForestBiome;
			ModBiome CinderForestUndergroundBiome;
			if (ModConditions.shadowsOfAbaddonLoaded && ((ModConditions.shadowsOfAbaddonMod.TryFind<ModBiome>("CinderDesertBiome", out CinderDesertBiome) && Main.LocalPlayer.InModBiome(CinderDesertBiome)) || (ModConditions.shadowsOfAbaddonMod.TryFind<ModBiome>("CinderForestBiome", out CinderForestBiome) && Main.LocalPlayer.InModBiome(CinderForestBiome)) || (ModConditions.shadowsOfAbaddonMod.TryFind<ModBiome>("CinderForestUndergroundBiome", out CinderForestUndergroundBiome) && Main.LocalPlayer.InModBiome(CinderForestUndergroundBiome))))
			{
				ModConditions.beenToCinderForest = true;
			}
			if (ModConditions.spiritLoaded)
			{
				ModBiome BriarSurfaceBiome;
				ModBiome BriarUndergroundBiome;
				if ((ModConditions.spiritMod.TryFind<ModBiome>("BriarSurfaceBiome", out BriarSurfaceBiome) && Main.LocalPlayer.InModBiome(BriarSurfaceBiome)) || (ModConditions.spiritMod.TryFind<ModBiome>("BriarUndergroundBiome", out BriarUndergroundBiome) && Main.LocalPlayer.InModBiome(BriarUndergroundBiome)))
				{
					ModConditions.beenToBriar = true;
				}
				ModBiome SpiritSurfaceBiome;
				ModBiome SpiritUndergroundBiome;
				if ((ModConditions.spiritMod.TryFind<ModBiome>("SpiritSurfaceBiome", out SpiritSurfaceBiome) && Main.LocalPlayer.InModBiome(SpiritSurfaceBiome)) || (ModConditions.spiritMod.TryFind<ModBiome>("SpiritUndergroundBiome", out SpiritUndergroundBiome) && Main.LocalPlayer.InModBiome(SpiritUndergroundBiome)))
				{
					ModConditions.beenToSpirit = true;
				}
			}
			if (ModConditions.spookyLoaded)
			{
				ModBiome SpookyBiome;
				if (ModConditions.spookyMod.TryFind<ModBiome>("SpookyBiome", out SpookyBiome) && Main.LocalPlayer.InModBiome(SpookyBiome))
				{
					ModConditions.beenToSpookyForest = true;
				}
				ModBiome SpookyBiomeUg;
				if (ModConditions.spookyMod.TryFind<ModBiome>("SpookyBiomeUg", out SpookyBiomeUg) && Main.LocalPlayer.InModBiome(SpookyBiomeUg))
				{
					ModConditions.beenToSpookyUnderground = true;
				}
				ModBiome SpookyHellBiome;
				if (ModConditions.spookyMod.TryFind<ModBiome>("SpookyHellBiome", out SpookyHellBiome) && Main.LocalPlayer.InModBiome(SpookyHellBiome))
				{
					ModConditions.beenToEyeValley = true;
				}
				ModBiome SpiderCaveBiome;
				if (ModConditions.spookyMod.TryFind<ModBiome>("SpiderCaveBiome", out SpiderCaveBiome) && Main.LocalPlayer.InModBiome(SpiderCaveBiome))
				{
					ModConditions.beenToSpiderCave = true;
				}
				ModBiome CatacombBiome;
				if (ModConditions.spookyMod.TryFind<ModBiome>("CatacombBiome", out CatacombBiome) && Main.LocalPlayer.InModBiome(CatacombBiome))
				{
					ModConditions.beenToCatacombs = true;
				}
				ModBiome CemeteryBiome;
				if (ModConditions.spookyMod.TryFind<ModBiome>("CemeteryBiome", out CemeteryBiome) && Main.LocalPlayer.InModBiome(CemeteryBiome))
				{
					ModConditions.beenToCemetery = true;
				}
			}
			if (ModConditions.starlightRiverLoaded)
			{
				ModBiome PermafrostTempleBiome;
				if (ModConditions.starlightRiverMod.TryFind<ModBiome>("PermafrostTempleBiome", out PermafrostTempleBiome) && Main.LocalPlayer.InModBiome(PermafrostTempleBiome))
				{
					ModConditions.beenToAuroracleTemple = true;
				}
				ModBiome VitricDesertBiome;
				if (ModConditions.starlightRiverMod.TryFind<ModBiome>("VitricDesertBiome", out VitricDesertBiome) && Main.LocalPlayer.InModBiome(VitricDesertBiome))
				{
					ModConditions.beenToVitricDesert = true;
				}
				ModBiome VitricTempleBiome;
				if (ModConditions.starlightRiverMod.TryFind<ModBiome>("VitricTempleBiome", out VitricTempleBiome) && Main.LocalPlayer.InModBiome(VitricTempleBiome))
				{
					ModConditions.beenToVitricTemple = true;
				}
			}
			if (ModConditions.starsAboveLoaded)
			{
				ModConditions.downedVagrantofSpace = (bool)ModConditions.starsAboveMod.Call(new object[]
				{
					"downedVagrant",
					base.Mod
				});
				ModConditions.downedThespian = (bool)ModConditions.starsAboveMod.Call(new object[]
				{
					"downedThespian",
					base.Mod
				});
				ModConditions.downedDioskouroi = (bool)ModConditions.starsAboveMod.Call(new object[]
				{
					"downedDioskouroi",
					base.Mod
				});
				ModConditions.downedNalhaun = (bool)ModConditions.starsAboveMod.Call(new object[]
				{
					"downedNalhaun",
					base.Mod
				});
				ModConditions.downedStarfarers = (bool)ModConditions.starsAboveMod.Call(new object[]
				{
					"downedStarfarers",
					base.Mod
				});
				ModConditions.downedPenthesilea = (bool)ModConditions.starsAboveMod.Call(new object[]
				{
					"downedPenthesilea",
					base.Mod
				});
				ModConditions.downedArbitration = (bool)ModConditions.starsAboveMod.Call(new object[]
				{
					"downedArbitration",
					base.Mod
				});
				ModConditions.downedWarriorOfLight = (bool)ModConditions.starsAboveMod.Call(new object[]
				{
					"downedWarriorOfLight",
					base.Mod
				});
				ModConditions.downedTsukiyomi = (bool)ModConditions.starsAboveMod.Call(new object[]
				{
					"downedTsukiyomi",
					base.Mod
				});
				if (ModConditions.downedCastor && ModConditions.downedPollux)
				{
					ModConditions.downedDioskouroi = true;
				}
			}
			if (ModConditions.thoriumLoaded)
			{
				ModConditions.downedGrandThunderBird = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"TheGrandThunderBird"
				});
				ModConditions.downedQueenJellyfish = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"QueenJellyfish"
				});
				ModConditions.downedViscount = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"Viscount"
				});
				ModConditions.downedGraniteEnergyStorm = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"GraniteEnergyStorm"
				});
				ModConditions.downedBuriedChampion = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"BuriedChampion"
				});
				ModConditions.downedStarScouter = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"StarScouter"
				});
				ModConditions.downedBoreanStrider = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"BoreanStrider"
				});
				ModConditions.downedFallenBeholder = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"FallenBeholder"
				});
				ModConditions.downedLich = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"Lich"
				});
				ModConditions.downedForgottenOne = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"ForgottenOne"
				});
				ModConditions.downedPrimordials = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"ThePrimordials"
				});
				ModConditions.downedPatchWerk = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"PatchWerk"
				});
				ModConditions.downedCorpseBloom = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"CorpseBloom"
				});
				ModConditions.downedIllusionist = (bool)ModConditions.thoriumMod.Call(new object[]
				{
					"GetDownedBoss",
					"Illusionist"
				});
				ModBiome DepthsBiome2;
				if (ModConditions.thoriumMod.TryFind<ModBiome>("DepthsBiome", out DepthsBiome2) && Main.LocalPlayer.InModBiome(DepthsBiome2))
				{
					ModConditions.beenToAquaticDepths = true;
				}
			}
			ModBiome VerdantBiome;
			if (ModConditions.verdantLoaded && ModConditions.verdantMod.TryFind<ModBiome>("VerdantBiome", out VerdantBiome) && Main.LocalPlayer.InModBiome(VerdantBiome))
			{
				ModConditions.beenToVerdant = true;
			}
			if (ModConditions.wrathOfTheGodsLoaded)
			{
				ModConditions.downedNoxus = (bool)ModConditions.wrathOfTheGodsMod.Call(new object[]
				{
					"GetBossDefeated",
					"noxus"
				});
				ModConditions.downedNamelessDeityOfLight = (bool)ModConditions.wrathOfTheGodsMod.Call(new object[]
				{
					"GetBossDefeated",
					"namelessdeity"
				});
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00051370 File Offset: 0x0004F570
		public override void SaveWorldData(TagCompound tag)
		{
			tag.Add("downedBloodMoon", ModConditions.downedBloodMoon);
			tag.Add("downedEclipse", ModConditions.downedEclipse);
			tag.Add("downedLunarEvent", ModConditions.downedLunarEvent);
			tag.Add("beenThroughNight", ModConditions.beenThroughNight);
			tag.Add("beenToPurity", ModConditions.beenToPurity);
			tag.Add("beenToCavernsOrUnderground", ModConditions.beenToCavernsOrUnderground);
			tag.Add("beenToUnderworld", ModConditions.beenToUnderworld);
			tag.Add("beenToSky", ModConditions.beenToSky);
			tag.Add("beenToSnow", ModConditions.beenToSnow);
			tag.Add("beenToDesert", ModConditions.beenToDesert);
			tag.Add("beenToOcean", ModConditions.beenToOcean);
			tag.Add("beenToJungle", ModConditions.beenToJungle);
			tag.Add("beenToMushroom", ModConditions.beenToMushroom);
			tag.Add("beenToCorruption", ModConditions.beenToCorruption);
			tag.Add("beenToCrimson", ModConditions.beenToCrimson);
			tag.Add("beenToHallow", ModConditions.beenToHallow);
			tag.Add("beenToTemple", ModConditions.beenToTemple);
			tag.Add("beenToDungeon", ModConditions.beenToDungeon);
			tag.Add("beenToAether", ModConditions.beenToAether);
			tag.Add("talkedToSkeletonMerchant", ModConditions.talkedToSkeletonMerchant);
			tag.Add("talkedToTravelingMerchant", ModConditions.talkedToTravelingMerchant);
			tag.Add("beenToCrabCrevice", ModConditions.beenToCrabCrevice);
			tag.Add("beenToCrags", ModConditions.beenToCrags);
			tag.Add("beenToAstral", ModConditions.beenToAstral);
			tag.Add("beenToSunkenSea", ModConditions.beenToSunkenSea);
			tag.Add("beenToSulphurSea", ModConditions.beenToSulphurSea);
			tag.Add("beenToAbyss", ModConditions.beenToAbyss);
			tag.Add("beenToAbyssLayer1", ModConditions.beenToAbyssLayer1);
			tag.Add("beenToAbyssLayer2", ModConditions.beenToAbyssLayer2);
			tag.Add("beenToAbyssLayer3", ModConditions.beenToAbyssLayer3);
			tag.Add("beenToAbyssLayer4", ModConditions.beenToAbyssLayer4);
			tag.Add("beenToAstralBlight", ModConditions.beenToAstralBlight);
			tag.Add("beenToDepths", ModConditions.beenToDepths);
			tag.Add("beenToJadeLake", ModConditions.beenToJadeLake);
			tag.Add("beenToContagion", ModConditions.beenToContagion);
			tag.Add("beenToDread", ModConditions.beenToDread);
			tag.Add("beenToHomewardAbyss", ModConditions.beenToHomewardAbyss);
			tag.Add("beenToProfanedGardens", ModConditions.beenToProfanedGardens);
			tag.Add("beenToLunarVeilAbyss", ModConditions.beenToLunarVeilAbyss);
			tag.Add("beenToAcid", ModConditions.beenToAcid);
			tag.Add("beenToAurelus", ModConditions.beenToAurelus);
			tag.Add("beenToFable", ModConditions.beenToFable);
			tag.Add("beenToGovheilCastle", ModConditions.beenToGovheilCastle);
			tag.Add("beenToCathedral", ModConditions.beenToCathedral);
			tag.Add("beenToMarrowSurface", ModConditions.beenToMarrowSurface);
			tag.Add("beenToMorrowUnderground", ModConditions.beenToMorrowUnderground);
			tag.Add("beenToSkyFortress", ModConditions.beenToSkyFortress);
			tag.Add("beenToLab", ModConditions.beenToLab);
			tag.Add("beenToWasteland", ModConditions.beenToWasteland);
			tag.Add("beenToPyramid", ModConditions.beenToPyramid);
			tag.Add("beenToPlanetarium", ModConditions.beenToPlanetarium);
			tag.Add("beenToCinderForest", ModConditions.beenToCinderForest);
			tag.Add("beenToBriar", ModConditions.beenToBriar);
			tag.Add("beenToSpirit", ModConditions.beenToSpirit);
			tag.Add("beenToSpookyForest", ModConditions.beenToSpookyForest);
			tag.Add("beenToSpookyUnderground", ModConditions.beenToSpookyUnderground);
			tag.Add("beenToEyeValley", ModConditions.beenToEyeValley);
			tag.Add("beenToSpiderCave", ModConditions.beenToSpiderCave);
			tag.Add("beenToCatacombs", ModConditions.beenToCatacombs);
			tag.Add("beenToCemetery", ModConditions.beenToCemetery);
			tag.Add("beenToAuroracleTemple", ModConditions.beenToAuroracleTemple);
			tag.Add("beenToVitricDesert", ModConditions.beenToVitricDesert);
			tag.Add("beenToVitricTemple", ModConditions.beenToVitricTemple);
			tag.Add("beenToAquaticDepths", ModConditions.beenToAquaticDepths);
			tag.Add("beenToVerdant", ModConditions.beenToVerdant);
			List<string> downed = new List<string>();
			for (int i = 0; i < ModConditions.DownedBoss.Length; i++)
			{
				if (ModConditions.DownedBoss[i])
				{
					downed.Add("QoLCdownedBoss" + i.ToString());
				}
			}
			tag.Add("QoLCdowned", downed);
			List<string> ownedItemsData = new List<string>();
			for (int j = 0; j < ModConditions.ItemHasBeenOwned.Length; j++)
			{
				if (ModConditions.ItemHasBeenOwned[j])
				{
					if (j > (int)ItemID.Count)
					{
						ModItem modItem = ItemLoader.GetItem(j);
						if (modItem != null && modItem != null)
						{
							ownedItemsData.Add(modItem.FullName ?? "");
						}
					}
					else
					{
						List<string> list = ownedItemsData;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
						defaultInterpolatedStringHandler..ctor(0, 1);
						defaultInterpolatedStringHandler.AppendFormatted<int>(j);
						list.Add(defaultInterpolatedStringHandler.ToStringAndClear());
					}
				}
			}
			tag.Add("OwnedItemsList", ownedItemsData);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00051994 File Offset: 0x0004FB94
		public override void LoadWorldData(TagCompound tag)
		{
			ModConditions.downedBloodMoon = tag.Get<bool>("downedBloodMoon");
			ModConditions.downedEclipse = tag.Get<bool>("downedEclipse");
			ModConditions.downedLunarEvent = tag.Get<bool>("downedLunarEvent");
			ModConditions.beenThroughNight = tag.Get<bool>("beenThroughNight");
			ModConditions.beenToPurity = tag.Get<bool>("beenToPurity");
			ModConditions.beenToCavernsOrUnderground = tag.Get<bool>("beenToCavernsOrUnderground");
			ModConditions.beenToUnderworld = tag.Get<bool>("beenToUnderworld");
			ModConditions.beenToSky = tag.Get<bool>("beenToSky");
			ModConditions.beenToSnow = tag.Get<bool>("beenToSnow");
			ModConditions.beenToDesert = tag.Get<bool>("beenToDesert");
			ModConditions.beenToOcean = tag.Get<bool>("beenToOcean");
			ModConditions.beenToJungle = tag.Get<bool>("beenToJungle");
			ModConditions.beenToMushroom = tag.Get<bool>("beenToMushroom");
			ModConditions.beenToCorruption = tag.Get<bool>("beenToCorruption");
			ModConditions.beenToCrimson = tag.Get<bool>("beenToCrimson");
			ModConditions.beenToHallow = tag.Get<bool>("beenToHallow");
			ModConditions.beenToTemple = tag.Get<bool>("beenToTemple");
			ModConditions.beenToDungeon = tag.Get<bool>("beenToDungeon");
			ModConditions.beenToAether = tag.Get<bool>("beenToAether");
			ModConditions.talkedToSkeletonMerchant = tag.Get<bool>("talkedToSkeletonMerchant");
			ModConditions.talkedToTravelingMerchant = tag.Get<bool>("talkedToTravelingMerchant");
			ModConditions.beenToCrabCrevice = tag.Get<bool>("beenToCrabCrevice");
			ModConditions.beenToCrags = tag.Get<bool>("beenToCrags");
			ModConditions.beenToAstral = tag.Get<bool>("beenToAstral");
			ModConditions.beenToSunkenSea = tag.Get<bool>("beenToSunkenSea");
			ModConditions.beenToSulphurSea = tag.Get<bool>("beenToSulphurSea");
			ModConditions.beenToAbyss = tag.Get<bool>("beenToAbyss");
			ModConditions.beenToAbyssLayer1 = tag.Get<bool>("beenToAbyssLayer1");
			ModConditions.beenToAbyssLayer2 = tag.Get<bool>("beenToAbyssLayer2");
			ModConditions.beenToAbyssLayer3 = tag.Get<bool>("beenToAbyssLayer3");
			ModConditions.beenToAbyssLayer4 = tag.Get<bool>("beenToAbyssLayer4");
			ModConditions.beenToAstralBlight = tag.Get<bool>("beenToAstralBlight");
			ModConditions.beenToDepths = tag.Get<bool>("beenToDepths");
			ModConditions.beenToJadeLake = tag.Get<bool>("beenToJadeLake");
			ModConditions.beenToContagion = tag.Get<bool>("beenToContagion");
			ModConditions.beenToDread = tag.Get<bool>("beenToDread");
			ModConditions.beenToHomewardAbyss = tag.Get<bool>("beenToHomewardAbyss");
			ModConditions.beenToProfanedGardens = tag.Get<bool>("beenToProfanedGardens");
			ModConditions.beenToLunarVeilAbyss = tag.Get<bool>("beenToLunarVeilAbyss");
			ModConditions.beenToAcid = tag.Get<bool>("beenToAcid");
			ModConditions.beenToAurelus = tag.Get<bool>("beenToAurelus");
			ModConditions.beenToFable = tag.Get<bool>("beenToFable");
			ModConditions.beenToGovheilCastle = tag.Get<bool>("beenToGovheilCastle");
			ModConditions.beenToCathedral = tag.Get<bool>("beenToCathedral");
			ModConditions.beenToMarrowSurface = tag.Get<bool>("beenToMarrowSurface");
			ModConditions.beenToMorrowUnderground = tag.Get<bool>("beenToMorrowUnderground");
			ModConditions.beenToSkyFortress = tag.Get<bool>("beenToSkyFortress");
			ModConditions.beenToLab = tag.Get<bool>("beenToLab");
			ModConditions.beenToWasteland = tag.Get<bool>("beenToWasteland");
			ModConditions.beenToPyramid = tag.Get<bool>("beenToPyramid");
			ModConditions.beenToPlanetarium = tag.Get<bool>("beenToPlanetarium");
			ModConditions.beenToCinderForest = tag.Get<bool>("beenToCinderForest");
			ModConditions.beenToBriar = tag.Get<bool>("beenToBriar");
			ModConditions.beenToSpirit = tag.Get<bool>("beenToSpirit");
			ModConditions.beenToSpookyForest = tag.Get<bool>("beenToSpookyForest");
			ModConditions.beenToSpookyUnderground = tag.Get<bool>("beenToSpookyUnderground");
			ModConditions.beenToEyeValley = tag.Get<bool>("beenToEyeValley");
			ModConditions.beenToSpiderCave = tag.Get<bool>("beenToSpiderCave");
			ModConditions.beenToCatacombs = tag.Get<bool>("beenToCatacombs");
			ModConditions.beenToCemetery = tag.Get<bool>("beenToCemetery");
			ModConditions.beenToAuroracleTemple = tag.Get<bool>("beenToAuroracleTemple");
			ModConditions.beenToVitricDesert = tag.Get<bool>("beenToVitricDesert");
			ModConditions.beenToVitricTemple = tag.Get<bool>("beenToVitricTemple");
			ModConditions.beenToAquaticDepths = tag.Get<bool>("beenToAquaticDepths");
			ModConditions.beenToVerdant = tag.Get<bool>("beenToVerdant");
			IList<string> downed = tag.GetList<string>("QoLCdowned");
			for (int i = 0; i < ModConditions.DownedBoss.Length; i++)
			{
				bool[] array = ModConditions.DownedBoss;
				int num = i;
				ICollection<string> collection = downed;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(14, 1);
				defaultInterpolatedStringHandler.AppendLiteral("QoLCdownedBoss");
				defaultInterpolatedStringHandler.AppendFormatted<int>(i);
				array[num] = collection.Contains(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			ModConditions.ItemHasBeenOwned = ItemID.Sets.Factory.CreateBoolSet(false, Array.Empty<int>());
			foreach (string entry in tag.GetList<string>("OwnedItemsList"))
			{
				int type;
				ModItem item;
				if (int.TryParse(entry, out type) && type < (int)ItemID.Count)
				{
					ModConditions.ItemHasBeenOwned[type] = true;
				}
				else if (ModContent.TryFind<ModItem>(entry, out item))
				{
					ModConditions.ItemHasBeenOwned[item.Type] = true;
				}
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00051E90 File Offset: 0x00050090
		public override void NetSend(BinaryWriter writer)
		{
			BitsByte bb = default(BitsByte);
			bb[0] = ModConditions.downedDreadnautilus;
			bb[1] = ModConditions.downedMartianSaucer;
			bb[2] = ModConditions.downedBloodMoon;
			bb[3] = ModConditions.downedEclipse;
			bb[4] = ModConditions.downedLunarEvent;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedSlayerOfEvil;
			bb[1] = ModConditions.downedSATLA;
			bb[2] = ModConditions.downedDrFetus;
			bb[3] = ModConditions.downedSlimesHope;
			bb[4] = ModConditions.downedPoliticianSlime;
			bb[5] = ModConditions.downedAncientTrio;
			bb[6] = ModConditions.downedLavalGolem;
			bb[7] = ModConditions.downedAntony;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedBunnyZeppelin;
			bb[1] = ModConditions.downedOkiku;
			bb[2] = ModConditions.downedHarpyAirforce;
			bb[3] = ModConditions.downedIsaac;
			bb[4] = ModConditions.downedAncientGuardian;
			bb[5] = ModConditions.downedHeroicSlime;
			bb[6] = ModConditions.downedHoloSlime;
			bb[7] = ModConditions.downedSecurityBot;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedUndeadChef;
			bb[1] = ModConditions.downedGuardianOfFrost;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedSoulHarvester;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedTreeToad;
			bb[1] = ModConditions.downedSeseKitsugai;
			bb[2] = ModConditions.downedEyeOfTheStorm;
			bb[3] = ModConditions.downedFrigidius;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedCoreBoss;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedCragmawMire;
			bb[1] = ModConditions.downedNuclearTerror;
			bb[2] = ModConditions.downedMauler;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedWulfrumExcavator;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedCruiser;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedAstrageldon;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedClamitas;
			bb[1] = ModConditions.downedPyrogen;
			bb[2] = ModConditions.downedWallOfBronze;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedLepus;
			bb[1] = ModConditions.downedTurkor;
			bb[2] = ModConditions.downedOcram;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedRediancie;
			bb[1] = ModConditions.downedBabyIceDragon;
			bb[2] = ModConditions.downedSlimeEmperor;
			bb[3] = ModConditions.downedBloodiancie;
			bb[4] = ModConditions.downedThunderveinDragon;
			bb[5] = ModConditions.downedNightmarePlantera;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedChasme;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedLifeGuardian;
			bb[1] = ModConditions.downedManaGuardian;
			bb[2] = ModConditions.downedMeteorExcavator;
			bb[3] = ModConditions.downedMeteorAnnihilator;
			bb[4] = ModConditions.downedHellfireSerpent;
			bb[5] = ModConditions.downedWitheredAcornSpirit;
			bb[6] = ModConditions.downedGoblinSorcererChieftain;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedGalahis;
			bb[1] = ModConditions.downedCreation;
			bb[2] = ModConditions.downedDestruction;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedBlightKing;
			bb[1] = ModConditions.downedGardener;
			bb[2] = ModConditions.downedGlaciation;
			bb[3] = ModConditions.downedHandOfCthulhu;
			bb[4] = ModConditions.downedCursePreacher;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedEffulgence;
			bb[1] = ModConditions.downedIceLich;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedNiflheim;
			bb[1] = ModConditions.downedStellarStarship;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedBacteriumPrime;
			bb[1] = ModConditions.downedDesertBeak;
			bb[2] = ModConditions.downedKingSting;
			bb[3] = ModConditions.downedMechasting;
			bb[4] = ModConditions.downedPhantasm;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedTrojanSquirrel;
			bb[1] = ModConditions.downedCursedCoffin;
			bb[2] = ModConditions.downedDeviantt;
			bb[3] = ModConditions.downedLifelight;
			bb[4] = ModConditions.downedBanishedBaron;
			bb[5] = ModConditions.downedEridanus;
			bb[6] = ModConditions.downedAbominationn;
			bb[7] = ModConditions.downedMutant;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedAlphaFrostjaw;
			bb[1] = ModConditions.downedSanguineElemental;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedLad;
			bb[1] = ModConditions.downedHornlitz;
			bb[2] = ModConditions.downedSnowDon;
			bb[3] = ModConditions.downedStoffie;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedLilyWhite;
			bb[1] = ModConditions.downedRumia;
			bb[2] = ModConditions.downedEternityLarva;
			bb[3] = ModConditions.downedNazrin;
			bb[4] = ModConditions.downedHinaKagiyama;
			bb[5] = ModConditions.downedSekibanki;
			bb[6] = ModConditions.downedSeiran;
			bb[7] = ModConditions.downedNitoriKawashiro;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedMedicineMelancholy;
			bb[1] = ModConditions.downedCirno;
			bb[2] = ModConditions.downedMinamitsuMurasa;
			bb[3] = ModConditions.downedAliceMargatroid;
			bb[4] = ModConditions.downedSakuyaIzayoi;
			bb[5] = ModConditions.downedSeijaKijin;
			bb[6] = ModConditions.downedMayumiJoutouguu;
			bb[7] = ModConditions.downedToyosatomimiNoMiko;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedKaguyaHouraisan;
			bb[1] = ModConditions.downedUtsuhoReiuji;
			bb[2] = ModConditions.downedTenshiHinanawi;
			bb[3] = ModConditions.downedKisume;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedTrerios;
			bb[1] = ModConditions.downedMagmaEye;
			bb[2] = ModConditions.downedJack;
			bb[3] = ModConditions.downedAcheron;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedMarquisMoonsquid;
			bb[1] = ModConditions.downedPriestessRod;
			bb[2] = ModConditions.downedDiver;
			bb[3] = ModConditions.downedMotherbrain;
			bb[4] = ModConditions.downedWallOfShadow;
			bb[5] = ModConditions.downedSunSlimeGod;
			bb[6] = ModConditions.downedOverwatcher;
			bb[7] = ModConditions.downedLifebringer;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedMaterealizer;
			bb[1] = ModConditions.downedScarabBelief;
			bb[2] = ModConditions.downedWorldsEndWhale;
			bb[3] = ModConditions.downedSon;
			bb[4] = ModConditions.downedCaveOrdeal;
			bb[5] = ModConditions.downedCorruptOrdeal;
			bb[6] = ModConditions.downedCrimsonOrdeal;
			bb[7] = ModConditions.downedDesertOrdeal;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedForestOrdeal;
			bb[1] = ModConditions.downedHallowOrdeal;
			bb[2] = ModConditions.downedJungleOrdeal;
			bb[3] = ModConditions.downedSkyOrdeal;
			bb[4] = ModConditions.downedSnowOrdeal;
			bb[5] = ModConditions.downedUnderworldOrdeal;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedGoozma;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedBereftVassal;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedStoneGuardian;
			bb[1] = ModConditions.downedCommanderGintzia;
			bb[2] = ModConditions.downedSunStalker;
			bb[3] = ModConditions.downedPumpkinJack;
			bb[4] = ModConditions.downedForgottenPuppetDaedus;
			bb[5] = ModConditions.downedDreadMire;
			bb[6] = ModConditions.downedSingularityFragment;
			bb[7] = ModConditions.downedVerlia;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedIrradia;
			bb[1] = ModConditions.downedSylia;
			bb[2] = ModConditions.downedFenix;
			bb[3] = ModConditions.downedBlazingSerpent;
			bb[4] = ModConditions.downedCogwork;
			bb[5] = ModConditions.downedWaterCogwork;
			bb[6] = ModConditions.downedWaterJellyfish;
			bb[7] = ModConditions.downedSparn;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedPandorasFlamebox;
			bb[1] = ModConditions.downedSTARBOMBER;
			bb[2] = ModConditions.downedGintzeArmy;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedBritzz;
			bb[1] = ModConditions.downedTheAlchemist;
			bb[2] = ModConditions.downedCarnagePillar;
			bb[3] = ModConditions.downedVoidDigger;
			bb[4] = ModConditions.downedPrinceSlime;
			bb[5] = ModConditions.downedTriplets;
			bb[6] = ModConditions.downedJungleDefenders;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedSt4sys;
			bb[1] = ModConditions.downedTerminator;
			bb[2] = ModConditions.downedCaretaker;
			bb[3] = ModConditions.downedSiegeEngine;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedSuperVoltaicMotherSlime;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedTorizo;
			bb[1] = ModConditions.downedSerris;
			bb[2] = ModConditions.downedKraid;
			bb[3] = ModConditions.downedPhantoon;
			bb[4] = ModConditions.downedOmegaPirate;
			bb[5] = ModConditions.downedNightmare;
			bb[6] = ModConditions.downedGoldenTorizo;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedOphiopede;
			bb[1] = ModConditions.downedOphiocoon;
			bb[2] = ModConditions.downedOphiofly;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedStormCloudfish;
			bb[1] = ModConditions.downedStarConstruct;
			bb[2] = ModConditions.downedGigabat;
			bb[3] = ModConditions.downedSunPixie;
			bb[4] = ModConditions.downedEsophage;
			bb[5] = ModConditions.downedConvectiveWanderer;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedForestGuardian;
			bb[1] = ModConditions.downedCryoGuardian;
			bb[2] = ModConditions.downedPrimordialWorm;
			bb[3] = ModConditions.downedTheGuardianOfHell;
			bb[4] = ModConditions.downedVoid;
			bb[5] = ModConditions.downedArmagem;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedPolarExterminator;
			bb[1] = ModConditions.downedDivineLight;
			bb[2] = ModConditions.downedAncientMachine;
			bb[3] = ModConditions.downedNoehtnap;
			bb[4] = ModConditions.downedHydra;
			bb[5] = ModConditions.downedImperious;
			bb[6] = ModConditions.downedRuneGhost;
			bb[7] = ModConditions.downedInvaderBattleship;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedInvaderNoehtnap;
			bb[1] = ModConditions.downedOLORD;
			bb[2] = ModConditions.downedGreatTyrannosaurus;
			bb[3] = ModConditions.downedDinoMilitia;
			bb[4] = ModConditions.downedInvaders;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedThorn;
			bb[1] = ModConditions.downedErhan;
			bb[2] = ModConditions.downedKeeper;
			bb[3] = ModConditions.downedSeedOfInfection;
			bb[4] = ModConditions.downedKingSlayerIII;
			bb[5] = ModConditions.downedOmegaCleaver;
			bb[6] = ModConditions.downedOmegaGigapora;
			bb[7] = ModConditions.downedOmegaObliterator;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedPatientZero;
			bb[1] = ModConditions.downedAkka;
			bb[2] = ModConditions.downedUkko;
			bb[3] = ModConditions.downedAncientDeityDuo;
			bb[4] = ModConditions.downedNebuleus;
			bb[5] = ModConditions.downedFowlEmperor;
			bb[6] = ModConditions.downedCockatrice;
			bb[7] = ModConditions.downedBasan;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedSkullDigger;
			bb[1] = ModConditions.downedEaglecrestGolem;
			bb[2] = ModConditions.downedCalavia;
			bb[3] = ModConditions.downedTheJanitor;
			bb[4] = ModConditions.downedIrradiatedBehemoth;
			bb[5] = ModConditions.downedBlisterface;
			bb[6] = ModConditions.downedProtectorVolt;
			bb[7] = ModConditions.downedMACEProject;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedFowlMorning;
			bb[1] = ModConditions.downedRaveyard;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedPutridPinky;
			bb[1] = ModConditions.downedGlowmoth;
			bb[2] = ModConditions.downedPharaohsCurse;
			bb[3] = ModConditions.downedAdvisor;
			bb[4] = ModConditions.downedPolaris;
			bb[5] = ModConditions.downedLux;
			bb[6] = ModConditions.downedSubspaceSerpent;
			bb[7] = ModConditions.downedNatureConstruct;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedEarthenConstruct;
			bb[1] = ModConditions.downedPermafrostConstruct;
			bb[2] = ModConditions.downedTidalConstruct;
			bb[3] = ModConditions.downedOtherworldlyConstruct;
			bb[4] = ModConditions.downedEvilConstruct;
			bb[5] = ModConditions.downedInfernoConstruct;
			bb[6] = ModConditions.downedChaosConstruct;
			bb[7] = ModConditions.downedNatureSpirit;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedEarthenSpirit;
			bb[1] = ModConditions.downedPermafrostSpirit;
			bb[2] = ModConditions.downedTidalSpirit;
			bb[3] = ModConditions.downedOtherworldlySpirit;
			bb[4] = ModConditions.downedEvilSpirit;
			bb[5] = ModConditions.downedInfernoSpirit;
			bb[6] = ModConditions.downedChaosSpirit;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedDecree;
			bb[1] = ModConditions.downedFlamingPumpkin;
			bb[2] = ModConditions.downedZombiePiglinBrute;
			bb[3] = ModConditions.downedJensenTheGrandHarpy;
			bb[4] = ModConditions.downedAraneas;
			bb[5] = ModConditions.downedHarpyQueenRaynare;
			bb[6] = ModConditions.downedPrimordia;
			bb[7] = ModConditions.downedAbaddon;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedAraghur;
			bb[1] = ModConditions.downedLostSiblings;
			bb[2] = ModConditions.downedErazor;
			bb[3] = ModConditions.downedNihilus;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedExodygen;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedScarabeus;
			bb[1] = ModConditions.downedMoonJellyWizard;
			bb[2] = ModConditions.downedVinewrathBane;
			bb[3] = ModConditions.downedAncientAvian;
			bb[4] = ModConditions.downedStarplateVoyager;
			bb[5] = ModConditions.downedInfernon;
			bb[6] = ModConditions.downedDusking;
			bb[7] = ModConditions.downedAtlas;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedJellyDeluge;
			bb[1] = ModConditions.downedTide;
			bb[2] = ModConditions.downedMysticMoon;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedSpookySpirit;
			bb[1] = ModConditions.downedRotGourd;
			bb[2] = ModConditions.downedMoco;
			bb[3] = ModConditions.downedDaffodil;
			bb[4] = ModConditions.downedOrroBoro;
			bb[5] = ModConditions.downedBigBone;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedAuroracle;
			bb[1] = ModConditions.downedCeiros;
			bb[2] = ModConditions.downedGlassweaver;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedVagrantofSpace;
			bb[1] = ModConditions.downedThespian;
			bb[2] = ModConditions.downedDioskouroi;
			bb[3] = ModConditions.downedNalhaun;
			bb[4] = ModConditions.downedStarfarers;
			bb[5] = ModConditions.downedPenthesilea;
			bb[6] = ModConditions.downedArbitration;
			bb[7] = ModConditions.downedWarriorOfLight;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedTsukiyomi;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedAncientHusk;
			bb[1] = ModConditions.downedOverloadedScandrone;
			bb[2] = ModConditions.downedPainbringer;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedHarbingerOfAnnihilation;
			bb[1] = ModConditions.downedFlyingTerror;
			bb[2] = ModConditions.downedStoneMantaRay;
			bb[3] = ModConditions.downedBloodweaver;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedInfectedIncarnate;
			bb[1] = ModConditions.downedTidalTitan;
			bb[2] = ModConditions.downedDunestock;
			bb[3] = ModConditions.downedHexedConstructor;
			bb[4] = ModConditions.downedShadowcrawler;
			bb[5] = ModConditions.downedPrototypeI;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedGraniteOvergrowth;
			bb[1] = ModConditions.downedBeholder;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedDredger;
			bb[1] = ModConditions.downedCharcoolSnowman;
			bb[2] = ModConditions.downedCosmicMenace;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedEvilFlyingBlade;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedColossalCarnage;
			bb[1] = ModConditions.downedYurnero;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedStormCloud;
			bb[1] = ModConditions.downedGrandAntlion;
			bb[2] = ModConditions.downedGemstoneElemental;
			bb[3] = ModConditions.downedMoonlightDragonfly;
			bb[4] = ModConditions.downedDreadnaught;
			bb[5] = ModConditions.downedMosquitoMonarch;
			bb[6] = ModConditions.downedAnarchulesBeetle;
			bb[7] = ModConditions.downedChaosbringer;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedPaladinSpirit;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedManaflora;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedNoxus;
			bb[1] = ModConditions.downedNamelessDeityOfLight;
			writer.Write(bb);
			bb = default(BitsByte);
			bb[0] = ModConditions.downedDirtball;
			bb[1] = ModConditions.downedMetelord;
			bb[2] = ModConditions.downedAdeneb;
			bb[3] = ModConditions.downedEldritchJellyfish;
			bb[4] = ModConditions.downedSaburRex;
			writer.Write(bb);
			BitsByte bitsByte = default(BitsByte);
			for (int i = 0; i < ModConditions.DownedBoss.Length; i++)
			{
				int bit = i % 8;
				if (bit == 0 && i != 0)
				{
					writer.Write(bitsByte);
					bitsByte = default(BitsByte);
				}
				bitsByte[bit] = ModConditions.DownedBoss[i];
			}
			writer.Write(bitsByte);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0005355C File Offset: 0x0005175C
		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			ModConditions.downedDreadnautilus = flags[0];
			ModConditions.downedMartianSaucer = flags[1];
			ModConditions.downedBloodMoon = flags[2];
			ModConditions.downedEclipse = flags[3];
			ModConditions.downedLunarEvent = flags[4];
			flags = reader.ReadByte();
			ModConditions.downedSlayerOfEvil = flags[0];
			ModConditions.downedSATLA = flags[1];
			ModConditions.downedDrFetus = flags[2];
			ModConditions.downedSlimesHope = flags[3];
			ModConditions.downedPoliticianSlime = flags[4];
			ModConditions.downedAncientTrio = flags[5];
			ModConditions.downedLavalGolem = flags[6];
			ModConditions.downedAntony = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedBunnyZeppelin = flags[0];
			ModConditions.downedOkiku = flags[1];
			ModConditions.downedHarpyAirforce = flags[2];
			ModConditions.downedIsaac = flags[3];
			ModConditions.downedAncientGuardian = flags[4];
			ModConditions.downedHeroicSlime = flags[5];
			ModConditions.downedHoloSlime = flags[6];
			ModConditions.downedSecurityBot = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedUndeadChef = flags[0];
			ModConditions.downedGuardianOfFrost = flags[1];
			ModConditions.downedGuardianOfFrost = flags[1];
			ModConditions.downedSoulHarvester = reader.ReadByte()[0];
			flags = reader.ReadByte();
			ModConditions.downedTreeToad = flags[0];
			ModConditions.downedSeseKitsugai = flags[1];
			ModConditions.downedEyeOfTheStorm = flags[2];
			ModConditions.downedFrigidius = flags[3];
			ModConditions.downedCoreBoss = reader.ReadByte()[0];
			flags = reader.ReadByte();
			ModConditions.downedCragmawMire = flags[0];
			ModConditions.downedNuclearTerror = flags[1];
			ModConditions.downedMauler = flags[2];
			ModConditions.downedWulfrumExcavator = reader.ReadByte()[0];
			ModConditions.downedCruiser = reader.ReadByte()[0];
			ModConditions.downedAstrageldon = reader.ReadByte()[0];
			flags = reader.ReadByte();
			ModConditions.downedClamitas = flags[0];
			ModConditions.downedPyrogen = flags[1];
			ModConditions.downedWallOfBronze = flags[2];
			flags = reader.ReadByte();
			ModConditions.downedLepus = flags[0];
			ModConditions.downedTurkor = flags[1];
			ModConditions.downedOcram = flags[2];
			flags = reader.ReadByte();
			ModConditions.downedRediancie = flags[0];
			ModConditions.downedBabyIceDragon = flags[1];
			ModConditions.downedSlimeEmperor = flags[2];
			ModConditions.downedBloodiancie = flags[3];
			ModConditions.downedThunderveinDragon = flags[4];
			ModConditions.downedNightmarePlantera = flags[5];
			ModConditions.downedChasme = reader.ReadByte()[0];
			flags = reader.ReadByte();
			ModConditions.downedLifeGuardian = flags[0];
			ModConditions.downedManaGuardian = flags[1];
			ModConditions.downedMeteorExcavator = flags[2];
			ModConditions.downedMeteorAnnihilator = flags[3];
			ModConditions.downedHellfireSerpent = flags[4];
			ModConditions.downedWitheredAcornSpirit = flags[5];
			ModConditions.downedGoblinSorcererChieftain = flags[6];
			flags = reader.ReadByte();
			ModConditions.downedGalahis = flags[0];
			ModConditions.downedCreation = flags[1];
			ModConditions.downedDestruction = flags[2];
			flags = reader.ReadByte();
			ModConditions.downedBlightKing = flags[0];
			ModConditions.downedGardener = flags[1];
			ModConditions.downedGlaciation = flags[2];
			ModConditions.downedHandOfCthulhu = flags[3];
			ModConditions.downedCursePreacher = flags[4];
			flags = reader.ReadByte();
			ModConditions.downedEffulgence = flags[0];
			ModConditions.downedIceLich = flags[1];
			flags = reader.ReadByte();
			ModConditions.downedNiflheim = flags[0];
			ModConditions.downedStellarStarship = flags[1];
			flags = reader.ReadByte();
			ModConditions.downedBacteriumPrime = flags[0];
			ModConditions.downedDesertBeak = flags[1];
			ModConditions.downedKingSting = flags[2];
			ModConditions.downedMechasting = flags[3];
			ModConditions.downedPhantasm = flags[4];
			flags = reader.ReadByte();
			ModConditions.downedTrojanSquirrel = flags[0];
			ModConditions.downedCursedCoffin = flags[1];
			ModConditions.downedDeviantt = flags[2];
			ModConditions.downedLifelight = flags[3];
			ModConditions.downedBanishedBaron = flags[4];
			ModConditions.downedEridanus = flags[5];
			ModConditions.downedAbominationn = flags[6];
			ModConditions.downedMutant = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedAlphaFrostjaw = flags[0];
			ModConditions.downedSanguineElemental = flags[1];
			flags = reader.ReadByte();
			ModConditions.downedLad = flags[0];
			ModConditions.downedHornlitz = flags[1];
			ModConditions.downedSnowDon = flags[2];
			ModConditions.downedStoffie = flags[3];
			flags = reader.ReadByte();
			ModConditions.downedLilyWhite = flags[0];
			ModConditions.downedRumia = flags[1];
			ModConditions.downedEternityLarva = flags[2];
			ModConditions.downedNazrin = flags[3];
			ModConditions.downedHinaKagiyama = flags[4];
			ModConditions.downedSekibanki = flags[5];
			ModConditions.downedSeiran = flags[6];
			ModConditions.downedNitoriKawashiro = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedMedicineMelancholy = flags[0];
			ModConditions.downedCirno = flags[1];
			ModConditions.downedMinamitsuMurasa = flags[2];
			ModConditions.downedAliceMargatroid = flags[3];
			ModConditions.downedSakuyaIzayoi = flags[4];
			ModConditions.downedSeijaKijin = flags[5];
			ModConditions.downedMayumiJoutouguu = flags[6];
			ModConditions.downedToyosatomimiNoMiko = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedKaguyaHouraisan = flags[0];
			ModConditions.downedUtsuhoReiuji = flags[1];
			ModConditions.downedTenshiHinanawi = flags[2];
			ModConditions.downedKisume = flags[3];
			flags = reader.ReadByte();
			ModConditions.downedTrerios = flags[0];
			ModConditions.downedMagmaEye = flags[1];
			ModConditions.downedJack = flags[2];
			ModConditions.downedAcheron = flags[3];
			flags = reader.ReadByte();
			ModConditions.downedMarquisMoonsquid = flags[0];
			ModConditions.downedPriestessRod = flags[1];
			ModConditions.downedDiver = flags[2];
			ModConditions.downedMotherbrain = flags[3];
			ModConditions.downedWallOfShadow = flags[4];
			ModConditions.downedSunSlimeGod = flags[5];
			ModConditions.downedOverwatcher = flags[6];
			ModConditions.downedLifebringer = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedMaterealizer = flags[0];
			ModConditions.downedScarabBelief = flags[1];
			ModConditions.downedWorldsEndWhale = flags[2];
			ModConditions.downedSon = flags[3];
			ModConditions.downedCaveOrdeal = flags[4];
			ModConditions.downedCorruptOrdeal = flags[5];
			ModConditions.downedCrimsonOrdeal = flags[6];
			ModConditions.downedDesertOrdeal = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedForestOrdeal = flags[0];
			ModConditions.downedHallowOrdeal = flags[1];
			ModConditions.downedJungleOrdeal = flags[2];
			ModConditions.downedSkyOrdeal = flags[3];
			ModConditions.downedSnowOrdeal = flags[4];
			ModConditions.downedUnderworldOrdeal = flags[5];
			ModConditions.downedGoozma = reader.ReadByte()[0];
			ModConditions.downedBereftVassal = reader.ReadByte()[0];
			flags = reader.ReadByte();
			ModConditions.downedStoneGuardian = flags[0];
			ModConditions.downedCommanderGintzia = flags[1];
			ModConditions.downedSunStalker = flags[2];
			ModConditions.downedPumpkinJack = flags[3];
			ModConditions.downedForgottenPuppetDaedus = flags[4];
			ModConditions.downedDreadMire = flags[5];
			ModConditions.downedSingularityFragment = flags[6];
			ModConditions.downedVerlia = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedIrradia = flags[0];
			ModConditions.downedSylia = flags[1];
			ModConditions.downedFenix = flags[2];
			ModConditions.downedBlazingSerpent = flags[3];
			ModConditions.downedCogwork = flags[4];
			ModConditions.downedWaterCogwork = flags[5];
			ModConditions.downedWaterJellyfish = flags[6];
			ModConditions.downedSparn = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedPandorasFlamebox = flags[0];
			ModConditions.downedSTARBOMBER = flags[1];
			ModConditions.downedGintzeArmy = flags[2];
			flags = reader.ReadByte();
			ModConditions.downedBritzz = flags[0];
			ModConditions.downedTheAlchemist = flags[1];
			ModConditions.downedCarnagePillar = flags[2];
			ModConditions.downedVoidDigger = flags[3];
			ModConditions.downedPrinceSlime = flags[4];
			ModConditions.downedTriplets = flags[5];
			ModConditions.downedJungleDefenders = flags[6];
			flags = reader.ReadByte();
			ModConditions.downedSt4sys = flags[0];
			ModConditions.downedTerminator = flags[1];
			ModConditions.downedCaretaker = flags[2];
			ModConditions.downedSiegeEngine = flags[3];
			ModConditions.downedSuperVoltaicMotherSlime = reader.ReadByte()[0];
			flags = reader.ReadByte();
			ModConditions.downedTorizo = flags[0];
			ModConditions.downedSerris = flags[1];
			ModConditions.downedKraid = flags[2];
			ModConditions.downedPhantoon = flags[3];
			ModConditions.downedOmegaPirate = flags[4];
			ModConditions.downedNightmare = flags[5];
			ModConditions.downedGoldenTorizo = flags[6];
			flags = reader.ReadByte();
			ModConditions.downedOphiopede = flags[0];
			ModConditions.downedOphiocoon = flags[1];
			ModConditions.downedOphiofly = flags[2];
			flags = reader.ReadByte();
			ModConditions.downedStormCloudfish = flags[0];
			ModConditions.downedStarConstruct = flags[1];
			ModConditions.downedGigabat = flags[2];
			ModConditions.downedSunPixie = flags[3];
			ModConditions.downedEsophage = flags[4];
			ModConditions.downedConvectiveWanderer = flags[5];
			flags = reader.ReadByte();
			ModConditions.downedForestGuardian = flags[0];
			ModConditions.downedCryoGuardian = flags[1];
			ModConditions.downedPrimordialWorm = flags[2];
			ModConditions.downedTheGuardianOfHell = flags[3];
			ModConditions.downedVoid = flags[4];
			ModConditions.downedArmagem = flags[5];
			flags = reader.ReadByte();
			ModConditions.downedPolarExterminator = flags[0];
			ModConditions.downedDivineLight = flags[1];
			ModConditions.downedAncientMachine = flags[2];
			ModConditions.downedNoehtnap = flags[3];
			ModConditions.downedHydra = flags[4];
			ModConditions.downedImperious = flags[5];
			ModConditions.downedRuneGhost = flags[6];
			ModConditions.downedInvaderBattleship = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedInvaderNoehtnap = flags[0];
			ModConditions.downedOLORD = flags[1];
			ModConditions.downedGreatTyrannosaurus = flags[2];
			ModConditions.downedDinoMilitia = flags[3];
			ModConditions.downedInvaders = flags[4];
			flags = reader.ReadByte();
			ModConditions.downedThorn = flags[0];
			ModConditions.downedErhan = flags[1];
			ModConditions.downedKeeper = flags[2];
			ModConditions.downedSeedOfInfection = flags[3];
			ModConditions.downedKingSlayerIII = flags[4];
			ModConditions.downedOmegaCleaver = flags[5];
			ModConditions.downedOmegaGigapora = flags[6];
			ModConditions.downedOmegaObliterator = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedPatientZero = flags[0];
			ModConditions.downedAkka = flags[1];
			ModConditions.downedUkko = flags[2];
			ModConditions.downedAncientDeityDuo = flags[3];
			ModConditions.downedNebuleus = flags[4];
			ModConditions.downedFowlEmperor = flags[5];
			ModConditions.downedCockatrice = flags[6];
			ModConditions.downedBasan = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedSkullDigger = flags[0];
			ModConditions.downedEaglecrestGolem = flags[1];
			ModConditions.downedCalavia = flags[2];
			ModConditions.downedTheJanitor = flags[3];
			ModConditions.downedIrradiatedBehemoth = flags[4];
			ModConditions.downedBlisterface = flags[5];
			ModConditions.downedProtectorVolt = flags[6];
			ModConditions.downedMACEProject = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedFowlMorning = flags[0];
			ModConditions.downedRaveyard = flags[1];
			flags = reader.ReadByte();
			ModConditions.downedPutridPinky = flags[0];
			ModConditions.downedGlowmoth = flags[1];
			ModConditions.downedPharaohsCurse = flags[2];
			ModConditions.downedAdvisor = flags[3];
			ModConditions.downedPolaris = flags[4];
			ModConditions.downedLux = flags[5];
			ModConditions.downedSubspaceSerpent = flags[6];
			ModConditions.downedNatureConstruct = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedEarthenConstruct = flags[0];
			ModConditions.downedPermafrostConstruct = flags[1];
			ModConditions.downedTidalConstruct = flags[2];
			ModConditions.downedOtherworldlyConstruct = flags[3];
			ModConditions.downedEvilConstruct = flags[4];
			ModConditions.downedInfernoConstruct = flags[5];
			ModConditions.downedChaosConstruct = flags[6];
			ModConditions.downedNatureSpirit = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedEarthenSpirit = flags[0];
			ModConditions.downedPermafrostSpirit = flags[1];
			ModConditions.downedTidalSpirit = flags[2];
			ModConditions.downedOtherworldlySpirit = flags[3];
			ModConditions.downedEvilSpirit = flags[4];
			ModConditions.downedInfernoSpirit = flags[5];
			ModConditions.downedChaosSpirit = flags[6];
			flags = reader.ReadByte();
			ModConditions.downedDecree = flags[0];
			ModConditions.downedFlamingPumpkin = flags[1];
			ModConditions.downedZombiePiglinBrute = flags[2];
			ModConditions.downedJensenTheGrandHarpy = flags[3];
			ModConditions.downedAraneas = flags[4];
			ModConditions.downedHarpyQueenRaynare = flags[5];
			ModConditions.downedPrimordia = flags[6];
			ModConditions.downedAbaddon = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedAraghur = flags[0];
			ModConditions.downedLostSiblings = flags[1];
			ModConditions.downedErazor = flags[2];
			ModConditions.downedNihilus = flags[3];
			ModConditions.downedExodygen = reader.ReadByte()[0];
			flags = reader.ReadByte();
			ModConditions.downedScarabeus = flags[0];
			ModConditions.downedMoonJellyWizard = flags[1];
			ModConditions.downedVinewrathBane = flags[2];
			ModConditions.downedAncientAvian = flags[3];
			ModConditions.downedStarplateVoyager = flags[4];
			ModConditions.downedInfernon = flags[5];
			ModConditions.downedDusking = flags[6];
			ModConditions.downedAtlas = flags[7];
			flags = reader.ReadByte();
			ModConditions.downedJellyDeluge = flags[0];
			ModConditions.downedTide = flags[1];
			ModConditions.downedMysticMoon = flags[2];
			flags = reader.ReadByte();
			ModConditions.downedSpookySpirit = flags[0];
			ModConditions.downedRotGourd = flags[1];
			ModConditions.downedMoco = flags[2];
			ModConditions.downedDaffodil = flags[3];
			ModConditions.downedOrroBoro = flags[4];
			ModConditions.downedBigBone = flags[5];
			flags = reader.ReadByte();
			ModConditions.downedAuroracle = flags[0];
			ModConditions.downedCeiros = flags[1];
			ModConditions.downedGlassweaver = flags[2];
			flags = reader.ReadByte();
			ModConditions.downedVagrantofSpace = flags[0];
			ModConditions.downedThespian = flags[1];
			ModConditions.downedDioskouroi = flags[2];
			ModConditions.downedNalhaun = flags[3];
			ModConditions.downedStarfarers = flags[4];
			ModConditions.downedPenthesilea = flags[5];
			ModConditions.downedArbitration = flags[6];
			ModConditions.downedWarriorOfLight = flags[7];
			ModConditions.downedTsukiyomi = reader.ReadByte()[0];
			flags = reader.ReadByte();
			ModConditions.downedAncientHusk = flags[0];
			ModConditions.downedOverloadedScandrone = flags[1];
			ModConditions.downedPainbringer = flags[2];
			flags = reader.ReadByte();
			ModConditions.downedHarbingerOfAnnihilation = flags[0];
			ModConditions.downedFlyingTerror = flags[1];
			ModConditions.downedStoneMantaRay = flags[2];
			ModConditions.downedBloodweaver = flags[3];
			flags = reader.ReadByte();
			ModConditions.downedInfectedIncarnate = flags[0];
			ModConditions.downedTidalTitan = flags[1];
			ModConditions.downedDunestock = flags[2];
			ModConditions.downedHexedConstructor = flags[3];
			ModConditions.downedShadowcrawler = flags[4];
			ModConditions.downedPrototypeI = flags[5];
			flags = reader.ReadByte();
			ModConditions.downedGraniteOvergrowth = flags[0];
			ModConditions.downedBeholder = flags[1];
			flags = reader.ReadByte();
			ModConditions.downedDredger = flags[0];
			ModConditions.downedCharcoolSnowman = flags[1];
			ModConditions.downedCosmicMenace = flags[2];
			ModConditions.downedEvilFlyingBlade = reader.ReadByte()[0];
			flags = reader.ReadByte();
			ModConditions.downedColossalCarnage = flags[0];
			ModConditions.downedYurnero = flags[1];
			flags = reader.ReadByte();
			ModConditions.downedStormCloud = flags[0];
			ModConditions.downedGrandAntlion = flags[1];
			ModConditions.downedGemstoneElemental = flags[2];
			ModConditions.downedMoonlightDragonfly = flags[3];
			ModConditions.downedDreadnaught = flags[4];
			ModConditions.downedMosquitoMonarch = flags[5];
			ModConditions.downedAnarchulesBeetle = flags[6];
			ModConditions.downedChaosbringer = flags[7];
			ModConditions.downedPaladinSpirit = reader.ReadByte()[0];
			ModConditions.downedManaflora = reader.ReadByte()[0];
			flags = reader.ReadByte();
			ModConditions.downedNoxus = flags[0];
			ModConditions.downedNamelessDeityOfLight = flags[1];
			flags = reader.ReadByte();
			ModConditions.downedDirtball = flags[0];
			ModConditions.downedMetelord = flags[1];
			ModConditions.downedAdeneb = flags[2];
			ModConditions.downedEldritchJellyfish = flags[3];
			ModConditions.downedSaburRex = flags[4];
			for (int i = 0; i < ModConditions.DownedBoss.Length; i++)
			{
				int bits = i % 8;
				if (bits == 0)
				{
					flags = reader.ReadByte();
				}
				ModConditions.DownedBoss[i] = flags[bits];
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000549CC File Offset: 0x00052BCC
		public static void ResetDowned()
		{
			ModConditions.ItemHasBeenOwned = ItemID.Sets.Factory.CreateBoolSet(false, Array.Empty<int>());
			ModConditions.downedDreadnautilus = false;
			ModConditions.downedMartianSaucer = false;
			ModConditions.downedBloodMoon = false;
			ModConditions.downedEclipse = false;
			ModConditions.downedLunarEvent = false;
			ModConditions.beenThroughNight = false;
			ModConditions.beenToPurity = false;
			ModConditions.beenToCavernsOrUnderground = false;
			ModConditions.beenToUnderworld = false;
			ModConditions.beenToSky = false;
			ModConditions.beenToSnow = false;
			ModConditions.beenToDesert = false;
			ModConditions.beenToOcean = false;
			ModConditions.beenToJungle = false;
			ModConditions.beenToMushroom = false;
			ModConditions.beenToCorruption = false;
			ModConditions.beenToCrimson = false;
			ModConditions.beenToHallow = false;
			ModConditions.beenToTemple = false;
			ModConditions.beenToDungeon = false;
			ModConditions.beenToAether = false;
			ModConditions.talkedToSkeletonMerchant = false;
			ModConditions.talkedToTravelingMerchant = false;
			ModConditions.downedCrabson = false;
			ModConditions.downedOmegaStarite = false;
			ModConditions.downedDustDevil = false;
			ModConditions.downedRedSprite = false;
			ModConditions.downedSpaceSquid = false;
			ModConditions.downedHyperStarite = false;
			ModConditions.downedUltraStarite = false;
			ModConditions.downedDemonSiege = false;
			ModConditions.downedGlimmer = false;
			ModConditions.downedGaleStreams = false;
			ModConditions.beenToCrabCrevice = false;
			ModConditions.downedSlayerOfEvil = false;
			ModConditions.downedSATLA = false;
			ModConditions.downedDrFetus = false;
			ModConditions.downedSlimesHope = false;
			ModConditions.downedPoliticianSlime = false;
			ModConditions.downedAncientTrio = false;
			ModConditions.downedLavalGolem = false;
			ModConditions.downedAntony = false;
			ModConditions.downedBunnyZeppelin = false;
			ModConditions.downedOkiku = false;
			ModConditions.downedHarpyAirforce = false;
			ModConditions.downedIsaac = false;
			ModConditions.downedAncientGuardian = false;
			ModConditions.downedHeroicSlime = false;
			ModConditions.downedHoloSlime = false;
			ModConditions.downedSecurityBot = false;
			ModConditions.downedUndeadChef = false;
			ModConditions.downedGuardianOfFrost = false;
			ModConditions.downedSoulHarvester = false;
			ModConditions.downedTreeToad = false;
			ModConditions.downedSeseKitsugai = false;
			ModConditions.downedEyeOfTheStorm = false;
			ModConditions.downedFrigidius = false;
			ModConditions.downedCoreBoss = false;
			ModConditions.downedDesertScourge = false;
			ModConditions.downedCrabulon = false;
			ModConditions.downedHiveMind = false;
			ModConditions.downedPerforators = false;
			ModConditions.downedSlimeGod = false;
			ModConditions.downedCryogen = false;
			ModConditions.downedAquaticScourge = false;
			ModConditions.downedBrimstoneElemental = false;
			ModConditions.downedCalamitasClone = false;
			ModConditions.downedLeviathanAndAnahita = false;
			ModConditions.downedAstrumAureus = false;
			ModConditions.downedPlaguebringerGoliath = false;
			ModConditions.downedRavager = false;
			ModConditions.downedAstrumDeus = false;
			ModConditions.downedProfanedGuardians = false;
			ModConditions.downedDragonfolly = false;
			ModConditions.downedProvidence = false;
			ModConditions.downedStormWeaver = false;
			ModConditions.downedCeaselessVoid = false;
			ModConditions.downedSignus = false;
			ModConditions.downedPolterghast = false;
			ModConditions.downedOldDuke = false;
			ModConditions.downedDevourerOfGods = false;
			ModConditions.downedYharon = false;
			ModConditions.downedExoMechs = false;
			ModConditions.downedSupremeCalamitas = false;
			ModConditions.downedGiantClam = false;
			ModConditions.downedGreatSandShark = false;
			ModConditions.downedCragmawMire = false;
			ModConditions.downedNuclearTerror = false;
			ModConditions.downedMauler = false;
			ModConditions.downedEidolonWyrm = false;
			ModConditions.downedAcidRain1 = false;
			ModConditions.downedAcidRain2 = false;
			ModConditions.downedBossRush = false;
			ModConditions.beenToCrags = false;
			ModConditions.beenToAstral = false;
			ModConditions.beenToSunkenSea = false;
			ModConditions.beenToSulphurSea = false;
			ModConditions.beenToAbyss = false;
			ModConditions.beenToAbyssLayer1 = false;
			ModConditions.beenToAbyssLayer2 = false;
			ModConditions.beenToAbyssLayer3 = false;
			ModConditions.beenToAbyssLayer4 = false;
			ModConditions.downedWulfrumExcavator = false;
			ModConditions.downedCruiser = false;
			ModConditions.beenToAstralBlight = false;
			ModConditions.downedAstrageldon = false;
			ModConditions.downedClamitas = false;
			ModConditions.downedPyrogen = false;
			ModConditions.downedWallOfBronze = false;
			ModConditions.beenToConfection = false;
			ModConditions.downedLepus = false;
			ModConditions.downedTurkor = false;
			ModConditions.downedOcram = false;
			ModConditions.downedRediancie = false;
			ModConditions.downedBabyIceDragon = false;
			ModConditions.downedSlimeEmperor = false;
			ModConditions.downedBloodiancie = false;
			ModConditions.downedThunderveinDragon = false;
			ModConditions.downedNightmarePlantera = false;
			ModConditions.beenToDepths = false;
			ModConditions.downedLifeGuardian = false;
			ModConditions.downedManaGuardian = false;
			ModConditions.downedMeteorExcavator = false;
			ModConditions.downedMeteorAnnihilator = false;
			ModConditions.downedHellfireSerpent = false;
			ModConditions.downedWitheredAcornSpirit = false;
			ModConditions.downedGoblinSorcererChieftain = false;
			ModConditions.downedGalahis = false;
			ModConditions.downedCreation = false;
			ModConditions.downedDestruction = false;
			ModConditions.downedBlightKing = false;
			ModConditions.downedGardener = false;
			ModConditions.downedGlaciation = false;
			ModConditions.downedHandOfCthulhu = false;
			ModConditions.downedCursePreacher = false;
			ModConditions.beenToJadeLake = false;
			ModConditions.downedEffulgence = false;
			ModConditions.downedIceLich = false;
			ModConditions.downedBacteriumPrime = false;
			ModConditions.downedDesertBeak = false;
			ModConditions.downedKingSting = false;
			ModConditions.downedMechasting = false;
			ModConditions.downedPhantasm = false;
			ModConditions.beenToContagion = false;
			ModConditions.downedTrojanSquirrel = false;
			ModConditions.downedCursedCoffin = false;
			ModConditions.downedDeviantt = false;
			ModConditions.downedBanishedBaron = false;
			ModConditions.downedLifelight = false;
			ModConditions.downedEridanus = false;
			ModConditions.downedAbominationn = false;
			ModConditions.downedMutant = false;
			ModConditions.downedAlphaFrostjaw = false;
			ModConditions.downedSanguineElemental = false;
			ModConditions.beenToDread = false;
			ModConditions.downedLad = false;
			ModConditions.downedHornlitz = false;
			ModConditions.downedSnowDon = false;
			ModConditions.downedStoffie = false;
			ModConditions.downedLilyWhite = false;
			ModConditions.downedRumia = false;
			ModConditions.downedEternityLarva = false;
			ModConditions.downedNazrin = false;
			ModConditions.downedHinaKagiyama = false;
			ModConditions.downedSekibanki = false;
			ModConditions.downedSeiran = false;
			ModConditions.downedNitoriKawashiro = false;
			ModConditions.downedMedicineMelancholy = false;
			ModConditions.downedCirno = false;
			ModConditions.downedMinamitsuMurasa = false;
			ModConditions.downedAliceMargatroid = false;
			ModConditions.downedSakuyaIzayoi = false;
			ModConditions.downedSeijaKijin = false;
			ModConditions.downedMayumiJoutouguu = false;
			ModConditions.downedToyosatomimiNoMiko = false;
			ModConditions.downedKaguyaHouraisan = false;
			ModConditions.downedUtsuhoReiuji = false;
			ModConditions.downedTenshiHinanawi = false;
			ModConditions.downedKisume = false;
			ModConditions.downedTrerios = false;
			ModConditions.downedMagmaEye = false;
			ModConditions.downedJack = false;
			ModConditions.downedAcheron = false;
			ModConditions.downedMarquisMoonsquid = false;
			ModConditions.downedPriestessRod = false;
			ModConditions.downedDiver = false;
			ModConditions.downedMotherbrain = false;
			ModConditions.downedWallOfShadow = false;
			ModConditions.downedSunSlimeGod = false;
			ModConditions.downedOverwatcher = false;
			ModConditions.downedLifebringer = false;
			ModConditions.downedMaterealizer = false;
			ModConditions.downedScarabBelief = false;
			ModConditions.downedWorldsEndWhale = false;
			ModConditions.downedSon = false;
			ModConditions.downedCaveOrdeal = false;
			ModConditions.downedCorruptOrdeal = false;
			ModConditions.downedCrimsonOrdeal = false;
			ModConditions.downedDesertOrdeal = false;
			ModConditions.downedForestOrdeal = false;
			ModConditions.downedHallowOrdeal = false;
			ModConditions.downedJungleOrdeal = false;
			ModConditions.downedSkyOrdeal = false;
			ModConditions.downedSnowOrdeal = false;
			ModConditions.downedUnderworldOrdeal = false;
			ModConditions.beenToHomewardAbyss = false;
			ModConditions.downedGoozma = false;
			ModConditions.downedBereftVassal = false;
			ModConditions.beenToProfanedGardens = false;
			ModConditions.downedStoneGuardian = false;
			ModConditions.downedCommanderGintzia = false;
			ModConditions.downedSunStalker = false;
			ModConditions.downedPumpkinJack = false;
			ModConditions.downedForgottenPuppetDaedus = false;
			ModConditions.downedDreadMire = false;
			ModConditions.downedSingularityFragment = false;
			ModConditions.downedVerlia = false;
			ModConditions.downedIrradia = false;
			ModConditions.downedSylia = false;
			ModConditions.downedFenix = false;
			ModConditions.downedBlazingSerpent = false;
			ModConditions.downedCogwork = false;
			ModConditions.downedWaterCogwork = false;
			ModConditions.downedWaterJellyfish = false;
			ModConditions.downedSparn = false;
			ModConditions.downedPandorasFlamebox = false;
			ModConditions.downedSTARBOMBER = false;
			ModConditions.downedGintzeArmy = false;
			ModConditions.beenToLunarVeilAbyss = false;
			ModConditions.beenToAcid = false;
			ModConditions.beenToAurelus = false;
			ModConditions.beenToFable = false;
			ModConditions.beenToGovheilCastle = false;
			ModConditions.beenToCathedral = false;
			ModConditions.beenToMarrowSurface = false;
			ModConditions.beenToMorrowUnderground = false;
			ModConditions.downedBritzz = false;
			ModConditions.downedTheAlchemist = false;
			ModConditions.downedCarnagePillar = false;
			ModConditions.downedVoidDigger = false;
			ModConditions.downedPrinceSlime = false;
			ModConditions.downedTriplets = false;
			ModConditions.downedJungleDefenders = false;
			ModConditions.downedSt4sys = false;
			ModConditions.downedTerminator = false;
			ModConditions.downedCaretaker = false;
			ModConditions.downedSiegeEngine = false;
			ModConditions.downedSuperVoltaicMotherSlime = false;
			ModConditions.downedTorizo = false;
			ModConditions.downedSerris = false;
			ModConditions.downedKraid = false;
			ModConditions.downedPhantoon = false;
			ModConditions.downedOmegaPirate = false;
			ModConditions.downedNightmare = false;
			ModConditions.downedGoldenTorizo = false;
			ModConditions.downedOphiopede = false;
			ModConditions.downedOphiocoon = false;
			ModConditions.downedOphiofly = false;
			ModConditions.downedStormCloudfish = false;
			ModConditions.downedStarConstruct = false;
			ModConditions.downedGigabat = false;
			ModConditions.downedSunPixie = false;
			ModConditions.downedEsophage = false;
			ModConditions.downedConvectiveWanderer = false;
			ModConditions.downedForestGuardian = false;
			ModConditions.downedCryoGuardian = false;
			ModConditions.downedPrimordialWorm = false;
			ModConditions.downedTheGuardianOfHell = false;
			ModConditions.downedVoid = false;
			ModConditions.downedArmagem = false;
			ModConditions.downedPolarExterminator = false;
			ModConditions.downedDivineLight = false;
			ModConditions.downedAncientMachine = false;
			ModConditions.downedNoehtnap = false;
			ModConditions.downedHydra = false;
			ModConditions.downedImperious = false;
			ModConditions.downedRuneGhost = false;
			ModConditions.downedInvaderBattleship = false;
			ModConditions.downedInvaderNoehtnap = false;
			ModConditions.downedOLORD = false;
			ModConditions.downedGreatTyrannosaurus = false;
			ModConditions.downedDinoMilitia = false;
			ModConditions.downedInvaders = false;
			ModConditions.beenToSkyFortress = false;
			ModConditions.downedThorn = false;
			ModConditions.downedErhan = false;
			ModConditions.downedKeeper = false;
			ModConditions.downedSeedOfInfection = false;
			ModConditions.downedKingSlayerIII = false;
			ModConditions.downedOmegaCleaver = false;
			ModConditions.downedOmegaGigapora = false;
			ModConditions.downedOmegaObliterator = false;
			ModConditions.downedPatientZero = false;
			ModConditions.downedAkka = false;
			ModConditions.downedUkko = false;
			ModConditions.downedAncientDeityDuo = false;
			ModConditions.downedNebuleus = false;
			ModConditions.downedFowlEmperor = false;
			ModConditions.downedCockatrice = false;
			ModConditions.downedBasan = false;
			ModConditions.downedSkullDigger = false;
			ModConditions.downedEaglecrestGolem = false;
			ModConditions.downedCalavia = false;
			ModConditions.downedTheJanitor = false;
			ModConditions.downedIrradiatedBehemoth = false;
			ModConditions.downedBlisterface = false;
			ModConditions.downedProtectorVolt = false;
			ModConditions.downedMACEProject = false;
			ModConditions.downedFowlMorning = false;
			ModConditions.downedRaveyard = false;
			ModConditions.beenToLab = false;
			ModConditions.beenToWasteland = false;
			ModConditions.downedGlowmoth = false;
			ModConditions.downedPutridPinky = false;
			ModConditions.downedPharaohsCurse = false;
			ModConditions.downedAdvisor = false;
			ModConditions.downedPolaris = false;
			ModConditions.downedLux = false;
			ModConditions.downedSubspaceSerpent = false;
			ModConditions.downedNatureConstruct = false;
			ModConditions.downedEarthenConstruct = false;
			ModConditions.downedPermafrostConstruct = false;
			ModConditions.downedTidalConstruct = false;
			ModConditions.downedOtherworldlyConstruct = false;
			ModConditions.downedEvilConstruct = false;
			ModConditions.downedInfernoConstruct = false;
			ModConditions.downedChaosConstruct = false;
			ModConditions.downedNatureSpirit = false;
			ModConditions.downedEarthenSpirit = false;
			ModConditions.downedPermafrostSpirit = false;
			ModConditions.downedTidalSpirit = false;
			ModConditions.downedOtherworldlySpirit = false;
			ModConditions.downedEvilSpirit = false;
			ModConditions.downedInfernoSpirit = false;
			ModConditions.downedChaosSpirit = false;
			ModConditions.beenToPyramid = false;
			ModConditions.beenToPlanetarium = false;
			ModConditions.downedDecree = false;
			ModConditions.downedFlamingPumpkin = false;
			ModConditions.downedZombiePiglinBrute = false;
			ModConditions.downedJensenTheGrandHarpy = false;
			ModConditions.downedAraneas = false;
			ModConditions.downedHarpyQueenRaynare = false;
			ModConditions.downedPrimordia = false;
			ModConditions.downedAbaddon = false;
			ModConditions.downedAraghur = false;
			ModConditions.downedLostSiblings = false;
			ModConditions.downedErazor = false;
			ModConditions.downedNihilus = false;
			ModConditions.beenToCinderForest = false;
			ModConditions.downedExodygen = false;
			ModConditions.downedScarabeus = false;
			ModConditions.downedMoonJellyWizard = false;
			ModConditions.downedVinewrathBane = false;
			ModConditions.downedAncientAvian = false;
			ModConditions.downedStarplateVoyager = false;
			ModConditions.downedInfernon = false;
			ModConditions.downedDusking = false;
			ModConditions.downedAtlas = false;
			ModConditions.downedJellyDeluge = false;
			ModConditions.downedTide = false;
			ModConditions.downedMysticMoon = false;
			ModConditions.beenToBriar = false;
			ModConditions.beenToSpirit = false;
			ModConditions.downedSpookySpirit = false;
			ModConditions.downedRotGourd = false;
			ModConditions.downedMoco = false;
			ModConditions.downedDaffodil = false;
			ModConditions.downedOrroBoro = false;
			ModConditions.downedOrro = false;
			ModConditions.downedBoro = false;
			ModConditions.downedBigBone = false;
			ModConditions.beenToSpookyForest = false;
			ModConditions.beenToSpookyUnderground = false;
			ModConditions.beenToEyeValley = false;
			ModConditions.beenToSpiderCave = false;
			ModConditions.beenToCatacombs = false;
			ModConditions.beenToCemetery = false;
			ModConditions.downedAuroracle = false;
			ModConditions.downedCeiros = false;
			ModConditions.downedGlassweaver = false;
			ModConditions.beenToAuroracleTemple = false;
			ModConditions.beenToVitricDesert = false;
			ModConditions.beenToVitricTemple = false;
			ModConditions.downedVagrantofSpace = false;
			ModConditions.downedThespian = false;
			ModConditions.downedDioskouroi = false;
			ModConditions.downedNalhaun = false;
			ModConditions.downedStarfarers = false;
			ModConditions.downedPenthesilea = false;
			ModConditions.downedArbitration = false;
			ModConditions.downedWarriorOfLight = false;
			ModConditions.downedTsukiyomi = false;
			ModConditions.downedAncientHusk = false;
			ModConditions.downedOverloadedScandrone = false;
			ModConditions.downedPainbringer = false;
			ModConditions.downedHarbingerOfAnnihilation = false;
			ModConditions.downedFlyingTerror = false;
			ModConditions.downedStoneMantaRay = false;
			ModConditions.downedBloodweaver = false;
			ModConditions.downedInfectedIncarnate = false;
			ModConditions.downedTidalTitan = false;
			ModConditions.downedDunestock = false;
			ModConditions.downedShadowcrawler = false;
			ModConditions.downedHexedConstructor = false;
			ModConditions.downedPrototypeI = false;
			ModConditions.downedGrandThunderBird = false;
			ModConditions.downedQueenJellyfish = false;
			ModConditions.downedViscount = false;
			ModConditions.downedGraniteEnergyStorm = false;
			ModConditions.downedBuriedChampion = false;
			ModConditions.downedBoreanStrider = false;
			ModConditions.downedFallenBeholder = false;
			ModConditions.downedLich = false;
			ModConditions.downedForgottenOne = false;
			ModConditions.downedPrimordials = false;
			ModConditions.downedPatchWerk = false;
			ModConditions.downedCorpseBloom = false;
			ModConditions.downedIllusionist = false;
			ModConditions.beenToAquaticDepths = false;
			ModConditions.downedGraniteOvergrowth = false;
			ModConditions.downedBeholder = false;
			ModConditions.downedDredger = false;
			ModConditions.downedCharcoolSnowman = false;
			ModConditions.downedCosmicMenace = false;
			ModConditions.downedEvilFlyingBlade = false;
			ModConditions.downedColossalCarnage = false;
			ModConditions.downedYurnero = false;
			ModConditions.beenToVerdant = false;
			ModConditions.downedStormCloud = false;
			ModConditions.downedGrandAntlion = false;
			ModConditions.downedGemstoneElemental = false;
			ModConditions.downedMoonlightDragonfly = false;
			ModConditions.downedDreadnaught = false;
			ModConditions.downedMosquitoMonarch = false;
			ModConditions.downedAnarchulesBeetle = false;
			ModConditions.downedChaosbringer = false;
			ModConditions.downedPaladinSpirit = false;
			ModConditions.downedManaflora = false;
			ModConditions.downedNoxus = false;
			ModConditions.downedNamelessDeityOfLight = false;
			ModConditions.downedDirtball = false;
			ModConditions.downedMetelord = false;
			ModConditions.downedAdeneb = false;
			ModConditions.downedEldritchJellyfish = false;
			ModConditions.downedSaburRex = false;
			for (int i = 0; i < ModConditions.DownedBoss.Length; i++)
			{
				ModConditions.DownedBoss[i] = false;
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0005547C File Offset: 0x0005367C
		public static void LoadSupportedMods()
		{
			Mod Aequus;
			ModConditions.aequusLoaded = ModLoader.TryGetMod("Aequus", out Aequus);
			ModConditions.aequusMod = Aequus;
			Mod AFKPETS;
			ModConditions.afkpetsLoaded = ModLoader.TryGetMod("AFKPETS", out AFKPETS);
			ModConditions.afkpetsMod = AFKPETS;
			Mod AmuletOfManyMinions;
			ModConditions.amuletOfManyMinionsLoaded = ModLoader.TryGetMod("AmuletOfManyMinions", out AmuletOfManyMinions);
			ModConditions.amuletOfManyMinionsMod = AmuletOfManyMinions;
			Mod Arbour;
			ModConditions.arbourLoaded = ModLoader.TryGetMod("Arbour", out Arbour);
			ModConditions.arbourMod = Arbour;
			Mod AssortedCrazyThings;
			ModConditions.assortedCrazyThingsLoaded = ModLoader.TryGetMod("AssortedCrazyThings", out AssortedCrazyThings);
			ModConditions.assortedCrazyThingsMod = AssortedCrazyThings;
			Mod AwfulGarbageMod;
			ModConditions.awfulGarbageLoaded = ModLoader.TryGetMod("AwfulGarbageMod", out AwfulGarbageMod);
			ModConditions.awfulGarbageMod = AwfulGarbageMod;
			Mod Arsenal_Mod;
			ModConditions.blocksArsenalLoaded = ModLoader.TryGetMod("Arsenal_Mod", out Arsenal_Mod);
			ModConditions.blocksArsenalMod = Arsenal_Mod;
			Mod ArtificerMod;
			ModConditions.blocksArtificerLoaded = ModLoader.TryGetMod("ArtificerMod", out ArtificerMod);
			ModConditions.blocksArtificerMod = ArtificerMod;
			Mod CorruptionBoss;
			ModConditions.blocksCoreBossLoaded = ModLoader.TryGetMod("CorruptionBoss", out CorruptionBoss);
			ModConditions.blocksCoreBossMod = CorruptionBoss;
			Mod BInfoAcc;
			ModConditions.blocksInfoAccessoriesLoaded = ModLoader.TryGetMod("BInfoAcc", out BInfoAcc);
			ModConditions.blocksInfoAccessoriesMod = BInfoAcc;
			Mod BCThrower;
			ModConditions.blocksThrowerLoaded = ModLoader.TryGetMod("BCThrower", out BCThrower);
			ModConditions.blocksThrowerMod = BCThrower;
			Mod BombusApisBee;
			ModConditions.bombusApisLoaded = ModLoader.TryGetMod("BombusApisBee", out BombusApisBee);
			ModConditions.bombusApisMod = BombusApisBee;
			Mod Buffaria;
			ModConditions.buffariaLoaded = ModLoader.TryGetMod("Buffaria", out Buffaria);
			ModConditions.buffariaMod = Buffaria;
			Mod CalamityMod;
			ModConditions.calamityLoaded = ModLoader.TryGetMod("CalamityMod", out CalamityMod);
			ModConditions.calamityMod = CalamityMod;
			Mod CalRemix;
			ModConditions.calamityCommunityRemixLoaded = ModLoader.TryGetMod("CalRemix", out CalRemix);
			ModConditions.calamityCommunityRemixMod = CalRemix;
			Mod CalamityEntropy;
			ModConditions.calamityEntropyLoaded = ModLoader.TryGetMod("CalamityEntropy", out CalamityEntropy);
			ModConditions.calamityEntropyMod = CalamityEntropy;
			Mod CalamityOverhaul;
			ModConditions.calamityOverhaulLoaded = ModLoader.TryGetMod("CalamityOverhaul", out CalamityOverhaul);
			ModConditions.calamityOverhaulMod = CalamityOverhaul;
			Mod CalValEX;
			ModConditions.calamityVanitiesLoaded = ModLoader.TryGetMod("CalValEX", out CalValEX);
			ModConditions.calamityVanitiesMod = CalValEX;
			Mod CaptureDiscClass;
			ModConditions.captureDiscsClassLoaded = ModLoader.TryGetMod("CaptureDiscClass", out CaptureDiscClass);
			ModConditions.captureDiscsClassMod = CaptureDiscClass;
			Mod CatalystMod;
			ModConditions.catalystLoaded = ModLoader.TryGetMod("CatalystMod", out CatalystMod);
			ModConditions.catalystMod = CatalystMod;
			Mod CerebralMod;
			ModConditions.cerebralLoaded = ModLoader.TryGetMod("CerebralMod", out CerebralMod);
			ModConditions.cerebralMod = CerebralMod;
			Mod Clamity;
			ModConditions.clamityAddonLoaded = ModLoader.TryGetMod("Clamity", out Clamity);
			ModConditions.clamityAddonMod = Clamity;
			Mod ClickerClass;
			ModConditions.clickerClassLoaded = ModLoader.TryGetMod("ClickerClass", out ClickerClass);
			ModConditions.clickerClassMod = ClickerClass;
			Mod TheConfectionRebirth;
			ModConditions.confectionRebakedLoaded = ModLoader.TryGetMod("TheConfectionRebirth", out TheConfectionRebirth);
			ModConditions.confectionRebakedMod = TheConfectionRebirth;
			Mod Consolaria;
			ModConditions.consolariaLoaded = ModLoader.TryGetMod("Consolaria", out Consolaria);
			ModConditions.consolariaMod = Consolaria;
			Mod Coralite;
			ModConditions.coraliteLoaded = ModLoader.TryGetMod("Coralite", out Coralite);
			ModConditions.coraliteMod = Coralite;
			Mod CrystalDragons;
			ModConditions.crystalDragonsLoaded = ModLoader.TryGetMod("CrystalDragons", out CrystalDragons);
			ModConditions.crystalDragonsMod = CrystalDragons;
			Mod TheDepths;
			ModConditions.depthsLoaded = ModLoader.TryGetMod("TheDepths", out TheDepths);
			ModConditions.depthsMod = TheDepths;
			Mod DDmod;
			ModConditions.dormantDawnLoaded = ModLoader.TryGetMod("DDmod", out DDmod);
			ModConditions.dormantDawnMod = DDmod;
			Mod DraedonExpansion;
			ModConditions.draedonExpansionLoaded = ModLoader.TryGetMod("DraedonExpansion", out DraedonExpansion);
			ModConditions.draedonExpansionMod = DraedonExpansion;
			Mod DBZMODPORT;
			ModConditions.dragonBallTerrariaLoaded = ModLoader.TryGetMod("DBZMODPORT", out DBZMODPORT);
			ModConditions.dragonBallTerrariaMod = DBZMODPORT;
			Mod EchoesoftheAncients;
			ModConditions.echoesOfTheAncientsLoaded = ModLoader.TryGetMod("EchoesoftheAncients", out EchoesoftheAncients);
			ModConditions.echoesOfTheAncientsMod = EchoesoftheAncients;
			Mod Edorbis;
			ModConditions.edorbisLoaded = ModLoader.TryGetMod("Edorbis", out Edorbis);
			ModConditions.edorbisMod = Edorbis;
			Mod BlueMoon;
			ModConditions.enchantedMoonsLoaded = ModLoader.TryGetMod("BlueMoon", out BlueMoon);
			ModConditions.enchantedMoonsMod = BlueMoon;
			Mod JadeFables;
			ModConditions.everjadeLoaded = ModLoader.TryGetMod("JadeFables", out JadeFables);
			ModConditions.everjadeMod = JadeFables;
			Mod ExaltMod;
			ModConditions.exaltLoaded = ModLoader.TryGetMod("ExaltMod", out ExaltMod);
			ModConditions.exaltMod = ExaltMod;
			Mod excels;
			ModConditions.excelsiorLoaded = ModLoader.TryGetMod("excels", out excels);
			ModConditions.excelsiorMod = excels;
			Mod Avalon;
			ModConditions.exxoAvalonOriginsLoaded = ModLoader.TryGetMod("Avalon", out Avalon);
			ModConditions.exxoAvalonOriginsMod = Avalon;
			Mod Fargowiltas;
			ModConditions.fargosMutantLoaded = ModLoader.TryGetMod("Fargowiltas", out Fargowiltas);
			ModConditions.fargosMutantMod = Fargowiltas;
			Mod FargowiltasSouls;
			ModConditions.fargosSoulsLoaded = ModLoader.TryGetMod("FargowiltasSouls", out FargowiltasSouls);
			ModConditions.fargosSoulsMod = FargowiltasSouls;
			Mod FargowiltasCrossmod;
			ModConditions.fargosSoulsDLCLoaded = ModLoader.TryGetMod("FargowiltasCrossmod", out FargowiltasCrossmod);
			ModConditions.fargosSoulsDLCMod = FargowiltasCrossmod;
			Mod FargowiltasSoulsDLC;
			ModConditions.fargosSoulsExtrasLoaded = ModLoader.TryGetMod("FargowiltasSoulsDLC", out FargowiltasSoulsDLC);
			ModConditions.fargosSoulsExtrasMod = FargowiltasSoulsDLC;
			Mod FPenumbra;
			ModConditions.fracturesOfPenumbraLoaded = ModLoader.TryGetMod("FPenumbra", out FPenumbra);
			ModConditions.fracturesOfPenumbraMod = FPenumbra;
			Mod CosmeticVariety;
			ModConditions.furnitureFoodAndFunLoaded = ModLoader.TryGetMod("CosmeticVariety", out CosmeticVariety);
			ModConditions.furnitureFoodAndFunMod = CosmeticVariety;
			Mod GMT;
			ModConditions.gameTerrariaLoaded = ModLoader.TryGetMod("GMT", out GMT);
			ModConditions.gameTerrariaMod = GMT;
			Mod Gensokyo;
			ModConditions.gensokyoLoaded = ModLoader.TryGetMod("Gensokyo", out Gensokyo);
			ModConditions.gensokyoMod = Gensokyo;
			Mod GMR;
			ModConditions.gerdsLabLoaded = ModLoader.TryGetMod("GMR", out GMR);
			ModConditions.gerdsLabMod = GMR;
			Mod XDContentMod;
			ModConditions.heartbeatariaLoaded = ModLoader.TryGetMod("XDContentMod", out XDContentMod);
			ModConditions.heartbeatariaMod = XDContentMod;
			Mod ContinentOfJourney;
			ModConditions.homewardJourneyLoaded = ModLoader.TryGetMod("ContinentOfJourney", out ContinentOfJourney);
			ModConditions.homewardJourneyMod = ContinentOfJourney;
			Mod CalamityHunt;
			ModConditions.huntOfTheOldGodLoaded = ModLoader.TryGetMod("CalamityHunt", out CalamityHunt);
			ModConditions.huntOfTheOldGodMod = CalamityHunt;
			Mod InfectedQualities;
			ModConditions.infectedQualitiesLoaded = ModLoader.TryGetMod("InfectedQualities", out InfectedQualities);
			ModConditions.infectedQualitiesMod = InfectedQualities;
			Mod InfernumMode;
			ModConditions.infernumLoaded = ModLoader.TryGetMod("InfernumMode", out InfernumMode);
			ModConditions.infernumMod = InfernumMode;
			Mod miningcracks_take_on_luiafk;
			ModConditions.luiAFKLoaded = ModLoader.TryGetMod("miningcracks_take_on_luiafk", out miningcracks_take_on_luiafk);
			ModConditions.luiAFKMod = miningcracks_take_on_luiafk;
			Mod UnofficialLuiAFKDLC;
			ModConditions.luiAFKDLCLoaded = ModLoader.TryGetMod("UnofficialLuiAFKDLC", out UnofficialLuiAFKDLC);
			ModConditions.luiAFKDLCMod = UnofficialLuiAFKDLC;
			Mod Stellamod;
			ModConditions.lunarVeilLoaded = ModLoader.TryGetMod("Stellamod", out Stellamod);
			ModConditions.lunarVeilMod = Stellamod;
			Mod MagicStorage;
			ModConditions.magicStorageLoaded = ModLoader.TryGetMod("MagicStorage", out MagicStorage);
			ModConditions.magicStorageMod = MagicStorage;
			Mod MartainsOrder;
			ModConditions.martainsOrderLoaded = ModLoader.TryGetMod("MartainsOrder", out MartainsOrder);
			ModConditions.martainsOrderMod = MartainsOrder;
			Mod PrimeRework;
			ModConditions.mechReworkLoaded = ModLoader.TryGetMod("PrimeRework", out PrimeRework);
			ModConditions.mechReworkMod = PrimeRework;
			Mod MedRift;
			ModConditions.medialRiftLoaded = ModLoader.TryGetMod("MedRift", out MedRift);
			ModConditions.medialRiftMod = MedRift;
			Mod MetroidMod;
			ModConditions.metroidLoaded = ModLoader.TryGetMod("MetroidMod", out MetroidMod);
			ModConditions.metroidMod = MetroidMod;
			Mod CombinationsMod;
			ModConditions.moomoosUltimateYoyoRevampLoaded = ModLoader.TryGetMod("CombinationsMod", out CombinationsMod);
			ModConditions.moomoosUltimateYoyoRevampMod = CombinationsMod;
			Mod MrPlagueRaces;
			ModConditions.mrPlagueRacesLoaded = ModLoader.TryGetMod("MrPlagueRaces", out MrPlagueRaces);
			ModConditions.mrPlagueRacesMod = MrPlagueRaces;
			Mod OrchidMod;
			ModConditions.orchidLoaded = ModLoader.TryGetMod("OrchidMod", out OrchidMod);
			ModConditions.orchidMod = OrchidMod;
			Mod OphioidMod;
			ModConditions.ophioidLoaded = ModLoader.TryGetMod("OphioidMod", out OphioidMod);
			ModConditions.ophioidMod = OphioidMod;
			Mod Polarities;
			ModConditions.polaritiesLoaded = ModLoader.TryGetMod("Polarities", out Polarities);
			ModConditions.polaritiesMod = Polarities;
			Mod FM;
			ModConditions.projectZeroLoaded = ModLoader.TryGetMod("FM", out FM);
			ModConditions.projectZeroMod = FM;
			Mod QwertyMod;
			ModConditions.qwertyLoaded = ModLoader.TryGetMod("QwertyMod", out QwertyMod);
			ModConditions.qwertyMod = QwertyMod;
			Mod RagnarokMod;
			ModConditions.ragnarokLoaded = ModLoader.TryGetMod("RagnarokMod", out RagnarokMod);
			ModConditions.ragnarokMod = RagnarokMod;
			Mod Redemption;
			ModConditions.redemptionLoaded = ModLoader.TryGetMod("Redemption", out Redemption);
			ModConditions.redemptionMod = Redemption;
			Mod ReforgeOverhaul;
			ModConditions.reforgedLoaded = ModLoader.TryGetMod("ReforgeOverhaul", out ReforgeOverhaul);
			ModConditions.reforgedMod = ReforgeOverhaul;
			Mod Remnants;
			ModConditions.remnantsLoaded = ModLoader.TryGetMod("Remnants", out Remnants);
			ModConditions.remnantsMod = Remnants;
			Mod Rupture;
			ModConditions.ruptureLoaded = ModLoader.TryGetMod("Rupture", out Rupture);
			ModConditions.ruptureMod = Rupture;
			Mod SOTS;
			ModConditions.secretsOfTheShadowsLoaded = ModLoader.TryGetMod("SOTS", out SOTS);
			ModConditions.secretsOfTheShadowsMod = SOTS;
			Mod SacredTools;
			ModConditions.shadowsOfAbaddonLoaded = ModLoader.TryGetMod("SacredTools", out SacredTools);
			ModConditions.shadowsOfAbaddonMod = SacredTools;
			Mod Bloopsitems;
			ModConditions.sloomeLoaded = ModLoader.TryGetMod("Bloopsitems", out Bloopsitems);
			ModConditions.sloomeMod = Bloopsitems;
			Mod SpiritMod;
			ModConditions.spiritLoaded = ModLoader.TryGetMod("SpiritMod", out SpiritMod);
			ModConditions.spiritMod = SpiritMod;
			Mod Spooky;
			ModConditions.spookyLoaded = ModLoader.TryGetMod("Spooky", out Spooky);
			ModConditions.spookyMod = Spooky;
			Mod StarlightRiver;
			ModConditions.starlightRiverLoaded = ModLoader.TryGetMod("StarlightRiver", out StarlightRiver);
			ModConditions.starlightRiverMod = StarlightRiver;
			Mod StarsAbove;
			ModConditions.starsAboveLoaded = ModLoader.TryGetMod("StarsAbove", out StarsAbove);
			ModConditions.starsAboveMod = StarsAbove;
			Mod StormDiversMod;
			ModConditions.stormsAdditionsLoaded = ModLoader.TryGetMod("StormDiversMod", out StormDiversMod);
			ModConditions.stormsAdditionsMod = StormDiversMod;
			Mod StramClasses;
			ModConditions.stramsClassesLoaded = ModLoader.TryGetMod("StramClasses", out StramClasses);
			ModConditions.stramsClassesMod = StramClasses;
			Mod SupernovaMod;
			ModConditions.supernovaLoaded = ModLoader.TryGetMod("SupernovaMod", out SupernovaMod);
			ModConditions.supernovaMod = SupernovaMod;
			Mod TerrorbornMod;
			ModConditions.terrorbornLoaded = ModLoader.TryGetMod("TerrorbornMod", out TerrorbornMod);
			ModConditions.terrorbornMod = TerrorbornMod;
			Mod ThoriumMod;
			ModConditions.thoriumLoaded = ModLoader.TryGetMod("ThoriumMod", out ThoriumMod);
			ModConditions.thoriumMod = ThoriumMod;
			Mod ThoriumRework;
			ModConditions.thoriumBossReworkLoaded = ModLoader.TryGetMod("ThoriumRework", out ThoriumRework);
			ModConditions.thoriumBossReworkMod = ThoriumRework;
			Mod ExhaustionDisabler;
			ModConditions.exhaustionDisablerLoaded = ModLoader.TryGetMod("ExhaustionDisabler", out ExhaustionDisabler);
			ModConditions.exhaustionDisablerMod = ExhaustionDisabler;
			Mod TRAEProject;
			ModConditions.traeLoaded = ModLoader.TryGetMod("TRAEProject", out TRAEProject);
			ModConditions.traeMod = TRAEProject;
			Mod Uhtric;
			ModConditions.uhtricLoaded = ModLoader.TryGetMod("Uhtric", out Uhtric);
			ModConditions.uhtricMod = Uhtric;
			Mod UniverseOfSwordsMod;
			ModConditions.universeOfSwordsLoaded = ModLoader.TryGetMod("UniverseOfSwordsMod", out UniverseOfSwordsMod);
			ModConditions.universeOfSwordsMod = UniverseOfSwordsMod;
			Mod ValhallaMod;
			ModConditions.valhallaLoaded = ModLoader.TryGetMod("ValhallaMod", out ValhallaMod);
			ModConditions.valhallaMod = ValhallaMod;
			Mod Verdant;
			ModConditions.verdantLoaded = ModLoader.TryGetMod("Verdant", out Verdant);
			ModConditions.verdantMod = Verdant;
			Mod VitalityMod;
			ModConditions.vitalityLoaded = ModLoader.TryGetMod("VitalityMod", out VitalityMod);
			ModConditions.vitalityMod = VitalityMod;
			Mod WAYFAIRContent;
			ModConditions.wayfairContentLoaded = ModLoader.TryGetMod("WAYFAIRContent", out WAYFAIRContent);
			ModConditions.wayfairContentMod = WAYFAIRContent;
			Mod NoxusBoss;
			ModConditions.wrathOfTheGodsLoaded = ModLoader.TryGetMod("NoxusBoss", out NoxusBoss);
			ModConditions.wrathOfTheGodsMod = NoxusBoss;
			Mod Zylon;
			ModConditions.zylonLoaded = ModLoader.TryGetMod("Zylon", out Zylon);
			ModConditions.zylonMod = Zylon;
		}

		// Token: 0x0400009A RID: 154
		public static bool[] ItemHasBeenOwned;

		// Token: 0x0400009B RID: 155
		public static Condition expertOrMaster = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.inExpertOrMaster"), () => Main.expertMode || Main.masterMode);

		// Token: 0x0400009C RID: 156
		public static bool downedDreadnautilus;

		// Token: 0x0400009D RID: 157
		public static Condition DownedDreadnautilus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDreadnautilus"), () => ModConditions.DownedBoss[0]);

		// Token: 0x0400009E RID: 158
		public static bool downedMartianSaucer;

		// Token: 0x0400009F RID: 159
		public static Condition DownedMartianSaucer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMartianSaucer"), () => ModConditions.DownedBoss[1]);

		// Token: 0x040000A0 RID: 160
		public static Condition NotDownedMechBossAll = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.notDownedMechBossAll"), () => !NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3);

		// Token: 0x040000A1 RID: 161
		public bool waitForBloodMoon;

		// Token: 0x040000A2 RID: 162
		public static bool downedBloodMoon;

		// Token: 0x040000A3 RID: 163
		public static Condition DownedBloodMoon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBloodMoon"), () => ModConditions.downedBloodMoon);

		// Token: 0x040000A4 RID: 164
		public bool waitForEclipse;

		// Token: 0x040000A5 RID: 165
		public static bool downedEclipse;

		// Token: 0x040000A6 RID: 166
		public static Condition DownedEclipse = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEclipse"), () => ModConditions.downedEclipse);

		// Token: 0x040000A7 RID: 167
		public static bool downedLunarEvent;

		// Token: 0x040000A8 RID: 168
		public static Condition DownedLunarEvent = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLunarEvent"), () => ModConditions.downedLunarEvent);

		// Token: 0x040000A9 RID: 169
		public static Condition DownedLunarPillarAny = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLunarPillarAny"), () => NPC.downedTowerNebula || NPC.downedTowerSolar || NPC.downedTowerStardust || NPC.downedTowerVortex);

		// Token: 0x040000AA RID: 170
		public bool waitForNight;

		// Token: 0x040000AB RID: 171
		public static bool beenThroughNight;

		// Token: 0x040000AC RID: 172
		public static Condition HasBeenThroughNight = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenThroughNight"), () => ModConditions.beenThroughNight);

		// Token: 0x040000AD RID: 173
		public static bool beenToPurity;

		// Token: 0x040000AE RID: 174
		public static Condition HasBeenToPurity = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToPurity"), () => ModConditions.beenToPurity);

		// Token: 0x040000AF RID: 175
		public static bool beenToCavernsOrUnderground;

		// Token: 0x040000B0 RID: 176
		public static Condition HasBeenToCavernsOrUnderground = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCavernsOrUnderground"), () => ModConditions.beenToCavernsOrUnderground);

		// Token: 0x040000B1 RID: 177
		public static bool beenToUnderworld;

		// Token: 0x040000B2 RID: 178
		public static Condition HasBeenToUnderworld = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToUnderworld"), () => ModConditions.beenToUnderworld);

		// Token: 0x040000B3 RID: 179
		public static bool beenToSky;

		// Token: 0x040000B4 RID: 180
		public static Condition HasBeenToSky = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSky"), () => ModConditions.beenToSky);

		// Token: 0x040000B5 RID: 181
		public static bool beenToSnow;

		// Token: 0x040000B6 RID: 182
		public static Condition HasBeenToSnow = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSnow"), () => ModConditions.beenToSnow);

		// Token: 0x040000B7 RID: 183
		public static bool beenToDesert;

		// Token: 0x040000B8 RID: 184
		public static Condition HasBeenToDesert = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToDesert"), () => ModConditions.beenToDesert);

		// Token: 0x040000B9 RID: 185
		public static bool beenToOcean;

		// Token: 0x040000BA RID: 186
		public static Condition HasBeenToOcean = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToOcean"), () => ModConditions.beenToOcean);

		// Token: 0x040000BB RID: 187
		public static bool beenToJungle;

		// Token: 0x040000BC RID: 188
		public static Condition HasBeenToJungle = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToJungle"), () => ModConditions.beenToJungle);

		// Token: 0x040000BD RID: 189
		public static bool beenToMushroom;

		// Token: 0x040000BE RID: 190
		public static Condition HasBeenToMushroom = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToMushroom"), () => ModConditions.beenToMushroom);

		// Token: 0x040000BF RID: 191
		public static bool beenToCorruption;

		// Token: 0x040000C0 RID: 192
		public static Condition HasBeenToCorruption = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCorruption"), () => ModConditions.beenToCorruption);

		// Token: 0x040000C1 RID: 193
		public static bool beenToCrimson;

		// Token: 0x040000C2 RID: 194
		public static Condition HasBeenToCrimson = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCrimson"), () => ModConditions.beenToCrimson);

		// Token: 0x040000C3 RID: 195
		public static Condition HasBeenToEvil = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToEvil"), () => ModConditions.beenToCorruption || ModConditions.beenToCrimson);

		// Token: 0x040000C4 RID: 196
		public static bool beenToHallow;

		// Token: 0x040000C5 RID: 197
		public static Condition HasBeenToHallow = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToHallow"), () => ModConditions.beenToHallow);

		// Token: 0x040000C6 RID: 198
		public static bool beenToTemple;

		// Token: 0x040000C7 RID: 199
		public static Condition HasBeenToTemple = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToTemple"), () => ModConditions.beenToTemple);

		// Token: 0x040000C8 RID: 200
		public static bool beenToDungeon;

		// Token: 0x040000C9 RID: 201
		public static Condition HasBeenToDungeon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToDungeon"), () => ModConditions.beenToDungeon);

		// Token: 0x040000CA RID: 202
		public static bool beenToAether;

		// Token: 0x040000CB RID: 203
		public static Condition HasBeenToAether = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAether"), () => ModConditions.beenToAether);

		// Token: 0x040000CC RID: 204
		public static bool talkedToSkeletonMerchant;

		// Token: 0x040000CD RID: 205
		public static Condition HasTalkedToSkeletonMerchant = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.talkedToSkeletonMerchant"), () => ModConditions.talkedToSkeletonMerchant);

		// Token: 0x040000CE RID: 206
		public static bool talkedToTravelingMerchant;

		// Token: 0x040000CF RID: 207
		public static Condition HasTalkedToTravelingMerchant = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.talkedToTravelingMerchant"), () => ModConditions.talkedToTravelingMerchant);

		// Token: 0x040000D0 RID: 208
		public static bool aequusLoaded;

		// Token: 0x040000D1 RID: 209
		public static Mod aequusMod;

		// Token: 0x040000D2 RID: 210
		public static bool downedCrabson;

		// Token: 0x040000D3 RID: 211
		public static Condition DownedCrabson = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCrabson"), () => ModConditions.downedCrabson);

		// Token: 0x040000D4 RID: 212
		public static bool downedOmegaStarite;

		// Token: 0x040000D5 RID: 213
		public static Condition DownedOmegaStarite = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOmegaStarite"), () => ModConditions.downedOmegaStarite);

		// Token: 0x040000D6 RID: 214
		public static bool downedDustDevil;

		// Token: 0x040000D7 RID: 215
		public static Condition DownedDustDevil = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDustDevil"), () => ModConditions.downedDustDevil);

		// Token: 0x040000D8 RID: 216
		public static bool downedHyperStarite;

		// Token: 0x040000D9 RID: 217
		public static Condition DownedHyperStarite = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHyperStarite"), () => ModConditions.downedHyperStarite);

		// Token: 0x040000DA RID: 218
		public static bool downedUltraStarite;

		// Token: 0x040000DB RID: 219
		public static Condition DownedUltraStarite = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedUltraStarite"), () => ModConditions.downedUltraStarite);

		// Token: 0x040000DC RID: 220
		public static bool downedRedSprite;

		// Token: 0x040000DD RID: 221
		public static Condition DownedRedSprite = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRedSprite"), () => ModConditions.downedRedSprite);

		// Token: 0x040000DE RID: 222
		public static bool downedSpaceSquid;

		// Token: 0x040000DF RID: 223
		public static Condition DownedSpaceSquid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSpaceSquid"), () => ModConditions.downedSpaceSquid);

		// Token: 0x040000E0 RID: 224
		public static bool downedDemonSiege;

		// Token: 0x040000E1 RID: 225
		public static Condition DownedDemonSiege = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDemonSiege"), () => ModConditions.downedDemonSiege);

		// Token: 0x040000E2 RID: 226
		public static bool downedGlimmer;

		// Token: 0x040000E3 RID: 227
		public static Condition DownedGlimmer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGlimmer"), () => ModConditions.downedGlimmer);

		// Token: 0x040000E4 RID: 228
		public static bool downedGaleStreams;

		// Token: 0x040000E5 RID: 229
		public static Condition DownedGaleStreams = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGaleStreams"), () => ModConditions.downedGaleStreams);

		// Token: 0x040000E6 RID: 230
		public static bool beenToCrabCrevice;

		// Token: 0x040000E7 RID: 231
		public static Condition HasBeenToCrabCrevice = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCrabCrevice"), () => ModConditions.beenToCrabCrevice);

		// Token: 0x040000E8 RID: 232
		public static bool afkpetsLoaded;

		// Token: 0x040000E9 RID: 233
		public static Mod afkpetsMod;

		// Token: 0x040000EA RID: 234
		public static bool downedSlayerOfEvil;

		// Token: 0x040000EB RID: 235
		public static Condition DownedSlayerOfEvil = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSlayerOfEvil"), () => ModConditions.DownedBoss[5]);

		// Token: 0x040000EC RID: 236
		public static bool downedSATLA;

		// Token: 0x040000ED RID: 237
		public static Condition DownedSATLA = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSATLA"), () => ModConditions.DownedBoss[6]);

		// Token: 0x040000EE RID: 238
		public static bool downedDrFetus;

		// Token: 0x040000EF RID: 239
		public static Condition DownedDrFetus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDrFetus"), () => ModConditions.DownedBoss[7]);

		// Token: 0x040000F0 RID: 240
		public static bool downedSlimesHope;

		// Token: 0x040000F1 RID: 241
		public static Condition DownedSlimesHope = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSlimesHope"), () => ModConditions.DownedBoss[8]);

		// Token: 0x040000F2 RID: 242
		public static bool downedPoliticianSlime;

		// Token: 0x040000F3 RID: 243
		public static Condition DownedPoliticianSlime = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPoliticianSlime"), () => ModConditions.DownedBoss[9]);

		// Token: 0x040000F4 RID: 244
		public static bool downedAncientTrio;

		// Token: 0x040000F5 RID: 245
		public static Condition DownedAncientTrio = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAncientTrio"), () => ModConditions.DownedBoss[10]);

		// Token: 0x040000F6 RID: 246
		public static bool downedLavalGolem;

		// Token: 0x040000F7 RID: 247
		public static Condition DownedLavalGolem = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLavalGolem"), () => ModConditions.DownedBoss[11]);

		// Token: 0x040000F8 RID: 248
		public static bool downedAntony;

		// Token: 0x040000F9 RID: 249
		public static Condition DownedAntony = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAntony"), () => ModConditions.DownedBoss[12]);

		// Token: 0x040000FA RID: 250
		public static bool downedBunnyZeppelin;

		// Token: 0x040000FB RID: 251
		public static Condition DownedBunnyZeppelin = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBunnyZeppelin"), () => ModConditions.DownedBoss[13]);

		// Token: 0x040000FC RID: 252
		public static bool downedOkiku;

		// Token: 0x040000FD RID: 253
		public static Condition DownedOkiku = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOkiku"), () => ModConditions.DownedBoss[14]);

		// Token: 0x040000FE RID: 254
		public static bool downedHarpyAirforce;

		// Token: 0x040000FF RID: 255
		public static Condition DownedHarpyAirforce = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHarpyAirforce"), () => ModConditions.DownedBoss[15]);

		// Token: 0x04000100 RID: 256
		public static bool downedIsaac;

		// Token: 0x04000101 RID: 257
		public static Condition DownedIsaac = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedIsaac"), () => ModConditions.DownedBoss[16]);

		// Token: 0x04000102 RID: 258
		public static bool downedAncientGuardian;

		// Token: 0x04000103 RID: 259
		public static Condition DownedAncientGuardian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAncientGuardian"), () => ModConditions.DownedBoss[17]);

		// Token: 0x04000104 RID: 260
		public static bool downedHeroicSlime;

		// Token: 0x04000105 RID: 261
		public static Condition DownedHeroicSlime = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHeroicSlime"), () => ModConditions.DownedBoss[18]);

		// Token: 0x04000106 RID: 262
		public static bool downedHoloSlime;

		// Token: 0x04000107 RID: 263
		public static Condition DownedHoloSlime = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHoloSlime"), () => ModConditions.DownedBoss[19]);

		// Token: 0x04000108 RID: 264
		public static bool downedSecurityBot;

		// Token: 0x04000109 RID: 265
		public static Condition DownedSecurityBot = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSecurityBot"), () => ModConditions.DownedBoss[20]);

		// Token: 0x0400010A RID: 266
		public static bool downedUndeadChef;

		// Token: 0x0400010B RID: 267
		public static Condition DownedUndeadChef = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedUndeadChef"), () => ModConditions.DownedBoss[21]);

		// Token: 0x0400010C RID: 268
		public static bool downedGuardianOfFrost;

		// Token: 0x0400010D RID: 269
		public static Condition DownedGuardianOfFrost = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGuardianOfFrost"), () => ModConditions.DownedBoss[22]);

		// Token: 0x0400010E RID: 270
		public static bool amuletOfManyMinionsLoaded;

		// Token: 0x0400010F RID: 271
		public static Mod amuletOfManyMinionsMod;

		// Token: 0x04000110 RID: 272
		public static bool arbourLoaded;

		// Token: 0x04000111 RID: 273
		public static Mod arbourMod;

		// Token: 0x04000112 RID: 274
		public static bool assortedCrazyThingsLoaded;

		// Token: 0x04000113 RID: 275
		public static Mod assortedCrazyThingsMod;

		// Token: 0x04000114 RID: 276
		public static bool downedSoulHarvester;

		// Token: 0x04000115 RID: 277
		public static Condition DownedSoulHarvester = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSoulHarvester"), () => ModConditions.DownedBoss[23]);

		// Token: 0x04000116 RID: 278
		public static bool awfulGarbageLoaded;

		// Token: 0x04000117 RID: 279
		public static Mod awfulGarbageMod;

		// Token: 0x04000118 RID: 280
		public static bool downedTreeToad;

		// Token: 0x04000119 RID: 281
		public static Condition DownedTreeToad = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTreeToad"), () => ModConditions.DownedBoss[24]);

		// Token: 0x0400011A RID: 282
		public static bool downedSeseKitsugai;

		// Token: 0x0400011B RID: 283
		public static Condition DownedSeseKitsugai = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSeseKitsugai"), () => ModConditions.DownedBoss[25]);

		// Token: 0x0400011C RID: 284
		public static bool downedEyeOfTheStorm;

		// Token: 0x0400011D RID: 285
		public static Condition DownedEyeOfTheStorm = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEyeOfTheStorm"), () => ModConditions.DownedBoss[26]);

		// Token: 0x0400011E RID: 286
		public static bool downedFrigidius;

		// Token: 0x0400011F RID: 287
		public static Condition DownedFrigidius = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFrigidius"), () => ModConditions.DownedBoss[27]);

		// Token: 0x04000120 RID: 288
		public static bool blocksArsenalLoaded;

		// Token: 0x04000121 RID: 289
		public static Mod blocksArsenalMod;

		// Token: 0x04000122 RID: 290
		public static bool blocksArtificerLoaded;

		// Token: 0x04000123 RID: 291
		public static Mod blocksArtificerMod;

		// Token: 0x04000124 RID: 292
		public static bool blocksCoreBossLoaded;

		// Token: 0x04000125 RID: 293
		public static Mod blocksCoreBossMod;

		// Token: 0x04000126 RID: 294
		public static bool downedCoreBoss;

		// Token: 0x04000127 RID: 295
		public static Condition DownedCoreBoss = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCoreBoss"), () => ModConditions.DownedBoss[28]);

		// Token: 0x04000128 RID: 296
		public static bool blocksInfoAccessoriesLoaded;

		// Token: 0x04000129 RID: 297
		public static Mod blocksInfoAccessoriesMod;

		// Token: 0x0400012A RID: 298
		public static bool blocksThrowerLoaded;

		// Token: 0x0400012B RID: 299
		public static Mod blocksThrowerMod;

		// Token: 0x0400012C RID: 300
		public static bool bombusApisLoaded;

		// Token: 0x0400012D RID: 301
		public static Mod bombusApisMod;

		// Token: 0x0400012E RID: 302
		public static bool buffariaLoaded;

		// Token: 0x0400012F RID: 303
		public static Mod buffariaMod;

		// Token: 0x04000130 RID: 304
		public static bool calamityLoaded;

		// Token: 0x04000131 RID: 305
		public static Mod calamityMod;

		// Token: 0x04000132 RID: 306
		public static bool downedDesertScourge;

		// Token: 0x04000133 RID: 307
		public static Condition DownedDesertScourge = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDesertScourge"), () => ModConditions.downedDesertScourge);

		// Token: 0x04000134 RID: 308
		public static bool downedCrabulon;

		// Token: 0x04000135 RID: 309
		public static Condition DownedCrabulon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCrabulon"), () => ModConditions.downedCrabulon);

		// Token: 0x04000136 RID: 310
		public static bool downedHiveMind;

		// Token: 0x04000137 RID: 311
		public static Condition DownedHiveMind = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHiveMind"), () => ModConditions.downedHiveMind);

		// Token: 0x04000138 RID: 312
		public static bool downedPerforators;

		// Token: 0x04000139 RID: 313
		public static Condition DownedPerforators = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPerforators"), () => ModConditions.downedPerforators);

		// Token: 0x0400013A RID: 314
		public static Condition DownedPerforatorsOrHiveMind = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPerfOrHive"), () => ModConditions.downedPerforators || ModConditions.downedHiveMind);

		// Token: 0x0400013B RID: 315
		public static bool downedSlimeGod;

		// Token: 0x0400013C RID: 316
		public static Condition DownedSlimeGod = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSlimeGod"), () => ModConditions.downedSlimeGod);

		// Token: 0x0400013D RID: 317
		public static bool downedCryogen;

		// Token: 0x0400013E RID: 318
		public static Condition DownedCryogen = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCryogen"), () => ModConditions.downedCryogen);

		// Token: 0x0400013F RID: 319
		public static bool downedAquaticScourge;

		// Token: 0x04000140 RID: 320
		public static Condition DownedAquaticScourge = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAquaticScourge"), () => ModConditions.downedAquaticScourge);

		// Token: 0x04000141 RID: 321
		public static bool downedBrimstoneElemental;

		// Token: 0x04000142 RID: 322
		public static Condition DownedBrimstoneElemental = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBrimstoneElemental"), () => ModConditions.downedBrimstoneElemental);

		// Token: 0x04000143 RID: 323
		public static bool downedCalamitasClone;

		// Token: 0x04000144 RID: 324
		public static Condition DownedCalamitasClone = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCalamitasClone"), () => ModConditions.downedCalamitasClone);

		// Token: 0x04000145 RID: 325
		public static bool downedLeviathanAndAnahita;

		// Token: 0x04000146 RID: 326
		public static Condition DownedLeviathanAndAnahita = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLeviathanAndAnahita"), () => ModConditions.downedLeviathanAndAnahita);

		// Token: 0x04000147 RID: 327
		public static bool downedAstrumAureus;

		// Token: 0x04000148 RID: 328
		public static Condition DownedAstrumAureus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAstrumAureus"), () => ModConditions.downedAstrumAureus);

		// Token: 0x04000149 RID: 329
		public static bool downedPlaguebringerGoliath;

		// Token: 0x0400014A RID: 330
		public static Condition DownedPlaguebringerGoliath = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPlaguebringerGoliath"), () => ModConditions.downedPlaguebringerGoliath);

		// Token: 0x0400014B RID: 331
		public static bool downedRavager;

		// Token: 0x0400014C RID: 332
		public static Condition DownedRavager = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRavager"), () => ModConditions.downedRavager);

		// Token: 0x0400014D RID: 333
		public static bool downedAstrumDeus;

		// Token: 0x0400014E RID: 334
		public static Condition DownedAstrumDeus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAstrumDeus"), () => ModConditions.downedAstrumDeus);

		// Token: 0x0400014F RID: 335
		public static bool downedProfanedGuardians;

		// Token: 0x04000150 RID: 336
		public static Condition DownedProfanedGuardians = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedProfanedGuardians"), () => ModConditions.downedProfanedGuardians);

		// Token: 0x04000151 RID: 337
		public static bool downedDragonfolly;

		// Token: 0x04000152 RID: 338
		public static Condition DownedDragonfolly = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDragonfolly"), () => ModConditions.downedDragonfolly);

		// Token: 0x04000153 RID: 339
		public static bool downedProvidence;

		// Token: 0x04000154 RID: 340
		public static Condition DownedProvidence = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedProvidence"), () => ModConditions.downedProvidence);

		// Token: 0x04000155 RID: 341
		public static bool downedStormWeaver;

		// Token: 0x04000156 RID: 342
		public static Condition DownedStormWeaver = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStormWeaver"), () => ModConditions.downedStormWeaver);

		// Token: 0x04000157 RID: 343
		public static bool downedCeaselessVoid;

		// Token: 0x04000158 RID: 344
		public static Condition DownedCeaselessVoid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCeaselessVoid"), () => ModConditions.downedCeaselessVoid);

		// Token: 0x04000159 RID: 345
		public static bool downedSignus;

		// Token: 0x0400015A RID: 346
		public static Condition DownedSignus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSignus"), () => ModConditions.downedSignus);

		// Token: 0x0400015B RID: 347
		public static bool downedPolterghast;

		// Token: 0x0400015C RID: 348
		public static Condition DownedPolterghast = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPolterghast"), () => ModConditions.downedPolterghast);

		// Token: 0x0400015D RID: 349
		public static bool downedOldDuke;

		// Token: 0x0400015E RID: 350
		public static Condition DownedOldDuke = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOldDuke"), () => ModConditions.downedOldDuke);

		// Token: 0x0400015F RID: 351
		public static bool downedDevourerOfGods;

		// Token: 0x04000160 RID: 352
		public static Condition DownedDevourerOfGods = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDevourerOfGods"), () => ModConditions.downedDevourerOfGods);

		// Token: 0x04000161 RID: 353
		public static bool downedYharon;

		// Token: 0x04000162 RID: 354
		public static Condition DownedYharon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedYharon"), () => ModConditions.downedYharon);

		// Token: 0x04000163 RID: 355
		public static bool downedExoMechs;

		// Token: 0x04000164 RID: 356
		public static Condition DownedExoMechs = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedExoMechs"), () => ModConditions.downedExoMechs);

		// Token: 0x04000165 RID: 357
		public static bool downedSupremeCalamitas;

		// Token: 0x04000166 RID: 358
		public static Condition DownedSupremeCalamitas = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSupremeCalamitas"), () => ModConditions.downedSupremeCalamitas);

		// Token: 0x04000167 RID: 359
		public static bool downedGiantClam;

		// Token: 0x04000168 RID: 360
		public static Condition DownedGiantClam = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGiantClam"), () => ModConditions.downedGiantClam);

		// Token: 0x04000169 RID: 361
		public static bool downedCragmawMire;

		// Token: 0x0400016A RID: 362
		public static Condition DownedCragmawMire = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCragmawMire"), () => ModConditions.downedCragmawMire);

		// Token: 0x0400016B RID: 363
		public static bool downedGreatSandShark;

		// Token: 0x0400016C RID: 364
		public static Condition DownedGreatSandShark = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGreatSandShark"), () => ModConditions.downedGreatSandShark);

		// Token: 0x0400016D RID: 365
		public static bool downedNuclearTerror;

		// Token: 0x0400016E RID: 366
		public static Condition DownedNuclearTerror = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNuclearTerror"), () => ModConditions.downedNuclearTerror);

		// Token: 0x0400016F RID: 367
		public static bool downedMauler;

		// Token: 0x04000170 RID: 368
		public static Condition DownedMauler = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMauler"), () => ModConditions.downedMauler);

		// Token: 0x04000171 RID: 369
		public static bool downedEidolonWyrm;

		// Token: 0x04000172 RID: 370
		public static Condition DownedEidolonWyrm = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEidolonWyrm"), () => ModConditions.downedEidolonWyrm);

		// Token: 0x04000173 RID: 371
		public static bool downedAcidRain1;

		// Token: 0x04000174 RID: 372
		public static Condition DownedAcidRain1 = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAcidRain1"), () => ModConditions.downedAcidRain1);

		// Token: 0x04000175 RID: 373
		public static bool downedAcidRain2;

		// Token: 0x04000176 RID: 374
		public static Condition DownedAcidRain2 = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAcidRain2"), () => ModConditions.downedAcidRain2);

		// Token: 0x04000177 RID: 375
		public static bool downedBossRush;

		// Token: 0x04000178 RID: 376
		public static Condition DownedBossRush = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBossRush"), () => ModConditions.downedBossRush);

		// Token: 0x04000179 RID: 377
		public static bool beenToCrags;

		// Token: 0x0400017A RID: 378
		public static Condition HasBeenToCrags = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCrags"), () => ModConditions.beenToCrags);

		// Token: 0x0400017B RID: 379
		public static bool beenToAstral;

		// Token: 0x0400017C RID: 380
		public static Condition HasBeenToAstral = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAstral"), () => ModConditions.beenToAstral);

		// Token: 0x0400017D RID: 381
		public static bool beenToSunkenSea;

		// Token: 0x0400017E RID: 382
		public static Condition HasBeenToSunkenSea = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSunkenSea"), () => ModConditions.beenToSunkenSea);

		// Token: 0x0400017F RID: 383
		public static bool beenToSulphurSea;

		// Token: 0x04000180 RID: 384
		public static Condition HasBeenToSulphurSea = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSulphurSea"), () => ModConditions.beenToSulphurSea);

		// Token: 0x04000181 RID: 385
		public static bool beenToAbyss;

		// Token: 0x04000182 RID: 386
		public static Condition HasBeenToAbyss = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAbyss"), () => ModConditions.beenToAbyss);

		// Token: 0x04000183 RID: 387
		public static bool beenToAbyssLayer1;

		// Token: 0x04000184 RID: 388
		public static Condition HasBeenToAbyssLayer1 = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAbyssLayer1"), () => ModConditions.beenToAbyssLayer1);

		// Token: 0x04000185 RID: 389
		public static bool beenToAbyssLayer2;

		// Token: 0x04000186 RID: 390
		public static Condition HasBeenToAbyssLayer2 = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAbyssLayer2"), () => ModConditions.beenToAbyssLayer2);

		// Token: 0x04000187 RID: 391
		public static bool beenToAbyssLayer3;

		// Token: 0x04000188 RID: 392
		public static Condition HasBeenToAbyssLayer3 = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAbyssLayer3"), () => ModConditions.beenToAbyssLayer3);

		// Token: 0x04000189 RID: 393
		public static bool beenToAbyssLayer4;

		// Token: 0x0400018A RID: 394
		public static Condition HasBeenToAbyssLayer4 = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAbyssLayer4"), () => ModConditions.beenToAbyssLayer4);

		// Token: 0x0400018B RID: 395
		public static Condition ShimmerableAfterMoonLordOrSupremeCalamitas = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.shimmerableAfterMoonLordOrSupremeCalamitas"), () => (ModConditions.calamityLoaded && ModConditions.downedSupremeCalamitas && NPC.downedMoonlord) || (!ModConditions.calamityLoaded && NPC.downedMoonlord));

		// Token: 0x0400018C RID: 396
		public static bool calamityCommunityRemixLoaded;

		// Token: 0x0400018D RID: 397
		public static Mod calamityCommunityRemixMod;

		// Token: 0x0400018E RID: 398
		public static bool downedWulfrumExcavator;

		// Token: 0x0400018F RID: 399
		public static Condition DownedWulfrumExcavator = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWulfrumExcavator"), () => ModConditions.DownedBoss[32]);

		// Token: 0x04000190 RID: 400
		public static bool calamityEntropyLoaded;

		// Token: 0x04000191 RID: 401
		public static Mod calamityEntropyMod;

		// Token: 0x04000192 RID: 402
		public static bool downedLuminaris;

		// Token: 0x04000193 RID: 403
		public static Condition DownedLuminaris = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLuminaris"), () => ModConditions.DownedBoss[33]);

		// Token: 0x04000194 RID: 404
		public static bool downedProphet;

		// Token: 0x04000195 RID: 405
		public static Condition DownedProphet = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedProphet"), () => ModConditions.DownedBoss[34]);

		// Token: 0x04000196 RID: 406
		public static bool downedNihilityTwin;

		// Token: 0x04000197 RID: 407
		public static Condition DownedNihilityTwin = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNihilityTwin"), () => ModConditions.DownedBoss[35]);

		// Token: 0x04000198 RID: 408
		public static bool downedCruiser;

		// Token: 0x04000199 RID: 409
		public static Condition DownedCruiser = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCruiser"), () => ModConditions.DownedBoss[36]);

		// Token: 0x0400019A RID: 410
		public static bool calamityOverhaulLoaded;

		// Token: 0x0400019B RID: 411
		public static Mod calamityOverhaulMod;

		// Token: 0x0400019C RID: 412
		public static bool calamityVanitiesLoaded;

		// Token: 0x0400019D RID: 413
		public static Mod calamityVanitiesMod;

		// Token: 0x0400019E RID: 414
		public static bool beenToAstralBlight;

		// Token: 0x0400019F RID: 415
		public static Condition HasBeenToAstralBlight = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAstralBlight"), () => ModConditions.beenToAstralBlight);

		// Token: 0x040001A0 RID: 416
		public static bool captureDiscsClassLoaded;

		// Token: 0x040001A1 RID: 417
		public static Mod captureDiscsClassMod;

		// Token: 0x040001A2 RID: 418
		public static bool catalystLoaded;

		// Token: 0x040001A3 RID: 419
		public static Mod catalystMod;

		// Token: 0x040001A4 RID: 420
		public static bool downedAstrageldon;

		// Token: 0x040001A5 RID: 421
		public static Condition DownedAstrageldon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAstrageldon"), () => ModConditions.DownedBoss[37]);

		// Token: 0x040001A6 RID: 422
		public static bool cerebralLoaded;

		// Token: 0x040001A7 RID: 423
		public static Mod cerebralMod;

		// Token: 0x040001A8 RID: 424
		public static bool clamityAddonLoaded;

		// Token: 0x040001A9 RID: 425
		public static Mod clamityAddonMod;

		// Token: 0x040001AA RID: 426
		public static bool downedClamitas;

		// Token: 0x040001AB RID: 427
		public static Condition DownedClamitas = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedClamitas"), () => ModConditions.DownedBoss[38]);

		// Token: 0x040001AC RID: 428
		public static bool downedPyrogen;

		// Token: 0x040001AD RID: 429
		public static Condition DownedPyrogen = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPyrogen"), () => ModConditions.DownedBoss[39]);

		// Token: 0x040001AE RID: 430
		public static bool downedWallOfBronze;

		// Token: 0x040001AF RID: 431
		public static Condition DownedWallOfBronze = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWallOfBronze"), () => ModConditions.DownedBoss[40]);

		// Token: 0x040001B0 RID: 432
		public static bool clickerClassLoaded;

		// Token: 0x040001B1 RID: 433
		public static Mod clickerClassMod;

		// Token: 0x040001B2 RID: 434
		public static bool confectionRebakedLoaded;

		// Token: 0x040001B3 RID: 435
		public static Mod confectionRebakedMod;

		// Token: 0x040001B4 RID: 436
		public static bool beenToConfection;

		// Token: 0x040001B5 RID: 437
		public static Condition HasBeenToConfection = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToConfection"), () => ModConditions.beenToConfection);

		// Token: 0x040001B6 RID: 438
		public static Condition HasBeenToConfectionOrHallow = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToConfectionOrHallow"), () => ModConditions.beenToConfection || ModConditions.beenToHallow);

		// Token: 0x040001B7 RID: 439
		public static bool consolariaLoaded;

		// Token: 0x040001B8 RID: 440
		public static Mod consolariaMod;

		// Token: 0x040001B9 RID: 441
		public static bool downedLepus;

		// Token: 0x040001BA RID: 442
		public static Condition DownedLepus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLepus"), () => ModConditions.DownedBoss[41]);

		// Token: 0x040001BB RID: 443
		public static bool downedTurkor;

		// Token: 0x040001BC RID: 444
		public static Condition DownedTurkor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTurkor"), () => ModConditions.DownedBoss[42]);

		// Token: 0x040001BD RID: 445
		public static bool downedOcram;

		// Token: 0x040001BE RID: 446
		public static Condition DownedOcram = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOcram"), () => ModConditions.DownedBoss[43]);

		// Token: 0x040001BF RID: 447
		public static bool coraliteLoaded;

		// Token: 0x040001C0 RID: 448
		public static Mod coraliteMod;

		// Token: 0x040001C1 RID: 449
		public static bool downedRediancie;

		// Token: 0x040001C2 RID: 450
		public static Condition DownedRediancie = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRediancie"), () => ModConditions.DownedBoss[44]);

		// Token: 0x040001C3 RID: 451
		public static bool downedBabyIceDragon;

		// Token: 0x040001C4 RID: 452
		public static Condition DownedBabyIceDragon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBabyIceDragon"), () => ModConditions.DownedBoss[45]);

		// Token: 0x040001C5 RID: 453
		public static bool downedSlimeEmperor;

		// Token: 0x040001C6 RID: 454
		public static Condition DownedSlimeEmperor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSlimeEmperor"), () => ModConditions.DownedBoss[46]);

		// Token: 0x040001C7 RID: 455
		public static bool downedBloodiancie;

		// Token: 0x040001C8 RID: 456
		public static Condition DownedBloodiancie = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBloodiancie"), () => ModConditions.DownedBoss[47]);

		// Token: 0x040001C9 RID: 457
		public static bool downedThunderveinDragon;

		// Token: 0x040001CA RID: 458
		public static Condition DownedThunderveinDragon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedThunderveinDragon"), () => ModConditions.DownedBoss[48]);

		// Token: 0x040001CB RID: 459
		public static bool downedNightmarePlantera;

		// Token: 0x040001CC RID: 460
		public static Condition DownedNightmarePlantera = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNightmarePlantera"), () => ModConditions.DownedBoss[49]);

		// Token: 0x040001CD RID: 461
		public static bool crystalDragonsLoaded;

		// Token: 0x040001CE RID: 462
		public static Mod crystalDragonsMod;

		// Token: 0x040001CF RID: 463
		public static bool depthsLoaded;

		// Token: 0x040001D0 RID: 464
		public static Mod depthsMod;

		// Token: 0x040001D1 RID: 465
		public static bool downedChasme;

		// Token: 0x040001D2 RID: 466
		public static Condition DownedChasme = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedChasme"), () => ModConditions.DownedBoss[50]);

		// Token: 0x040001D3 RID: 467
		public static bool beenToDepths;

		// Token: 0x040001D4 RID: 468
		public static Condition HasBeenToDepths = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToDepths"), () => ModConditions.beenToDepths);

		// Token: 0x040001D5 RID: 469
		public static Condition HasBeenToDepthsOrUnderworld = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToDepthsOrUnderworld"), () => ModConditions.beenToDepths || ModConditions.beenToUnderworld);

		// Token: 0x040001D6 RID: 470
		public static bool dormantDawnLoaded;

		// Token: 0x040001D7 RID: 471
		public static Mod dormantDawnMod;

		// Token: 0x040001D8 RID: 472
		public static bool downedLifeGuardian;

		// Token: 0x040001D9 RID: 473
		public static Condition DownedLifeGuardian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLifeGuardian"), () => ModConditions.DownedBoss[51]);

		// Token: 0x040001DA RID: 474
		public static bool downedManaGuardian;

		// Token: 0x040001DB RID: 475
		public static Condition DownedManaGuardian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedManaGuardian"), () => ModConditions.DownedBoss[52]);

		// Token: 0x040001DC RID: 476
		public static bool downedMeteorExcavator;

		// Token: 0x040001DD RID: 477
		public static Condition DownedMeteorExcavator = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMeteorExcavator"), () => ModConditions.DownedBoss[53]);

		// Token: 0x040001DE RID: 478
		public static bool downedMeteorAnnihilator;

		// Token: 0x040001DF RID: 479
		public static Condition DownedMeteorAnnihilator = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMeteorAnnihilator"), () => ModConditions.DownedBoss[54]);

		// Token: 0x040001E0 RID: 480
		public static bool downedHellfireSerpent;

		// Token: 0x040001E1 RID: 481
		public static Condition DownedHellfireSerpent = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHellfireSerpent"), () => ModConditions.DownedBoss[55]);

		// Token: 0x040001E2 RID: 482
		public static bool downedWitheredAcornSpirit;

		// Token: 0x040001E3 RID: 483
		public static Condition DownedWitheredAcornSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWitheredAcornSpirit"), () => ModConditions.DownedBoss[56]);

		// Token: 0x040001E4 RID: 484
		public static bool downedGoblinSorcererChieftain;

		// Token: 0x040001E5 RID: 485
		public static Condition DownedGoblinSorcererChieftain = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGoblinSorcererChieftain"), () => ModConditions.DownedBoss[57]);

		// Token: 0x040001E6 RID: 486
		public static bool draedonExpansionLoaded;

		// Token: 0x040001E7 RID: 487
		public static Mod draedonExpansionMod;

		// Token: 0x040001E8 RID: 488
		public static bool dragonBallTerrariaLoaded;

		// Token: 0x040001E9 RID: 489
		public static Mod dragonBallTerrariaMod;

		// Token: 0x040001EA RID: 490
		public static bool echoesOfTheAncientsLoaded;

		// Token: 0x040001EB RID: 491
		public static Mod echoesOfTheAncientsMod;

		// Token: 0x040001EC RID: 492
		public static bool downedGalahis;

		// Token: 0x040001ED RID: 493
		public static Condition DownedGalahis = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGalahis"), () => ModConditions.DownedBoss[58]);

		// Token: 0x040001EE RID: 494
		public static bool downedCreation;

		// Token: 0x040001EF RID: 495
		public static Condition DownedCreation = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCreation"), () => ModConditions.DownedBoss[59]);

		// Token: 0x040001F0 RID: 496
		public static bool downedDestruction;

		// Token: 0x040001F1 RID: 497
		public static Condition DownedDestruction = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDestruction"), () => ModConditions.DownedBoss[60]);

		// Token: 0x040001F2 RID: 498
		public static bool edorbisLoaded;

		// Token: 0x040001F3 RID: 499
		public static Mod edorbisMod;

		// Token: 0x040001F4 RID: 500
		public static bool downedBlightKing;

		// Token: 0x040001F5 RID: 501
		public static Condition DownedBlightKing = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBlightKing"), () => ModConditions.DownedBoss[61]);

		// Token: 0x040001F6 RID: 502
		public static bool downedGardener;

		// Token: 0x040001F7 RID: 503
		public static Condition DownedGardener = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGardener"), () => ModConditions.DownedBoss[62]);

		// Token: 0x040001F8 RID: 504
		public static bool downedGlaciation;

		// Token: 0x040001F9 RID: 505
		public static Condition DownedGlaciation = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGlaciation"), () => ModConditions.DownedBoss[63]);

		// Token: 0x040001FA RID: 506
		public static bool downedHandOfCthulhu;

		// Token: 0x040001FB RID: 507
		public static Condition DownedHandOfCthulhu = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHandOfCthulhu"), () => ModConditions.DownedBoss[64]);

		// Token: 0x040001FC RID: 508
		public static bool downedCursePreacher;

		// Token: 0x040001FD RID: 509
		public static Condition DownedCursePreacher = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCursePreacher"), () => ModConditions.DownedBoss[65]);

		// Token: 0x040001FE RID: 510
		public static bool enchantedMoonsLoaded;

		// Token: 0x040001FF RID: 511
		public static Mod enchantedMoonsMod;

		// Token: 0x04000200 RID: 512
		public static bool everjadeLoaded;

		// Token: 0x04000201 RID: 513
		public static Mod everjadeMod;

		// Token: 0x04000202 RID: 514
		public static bool beenToJadeLake;

		// Token: 0x04000203 RID: 515
		public static Condition HasBeenToJadeLake = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToJadeLake"), () => ModConditions.beenToJadeLake);

		// Token: 0x04000204 RID: 516
		public static bool exaltLoaded;

		// Token: 0x04000205 RID: 517
		public static Mod exaltMod;

		// Token: 0x04000206 RID: 518
		public static bool downedEffulgence;

		// Token: 0x04000207 RID: 519
		public static Condition DownedEffulgence = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEffulgence"), () => ModConditions.DownedBoss[66]);

		// Token: 0x04000208 RID: 520
		public static bool downedIceLich;

		// Token: 0x04000209 RID: 521
		public static Condition DownedIceLich = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedIceLich"), () => ModConditions.DownedBoss[67]);

		// Token: 0x0400020A RID: 522
		public static bool excelsiorLoaded;

		// Token: 0x0400020B RID: 523
		public static Mod excelsiorMod;

		// Token: 0x0400020C RID: 524
		public static bool downedNiflheim;

		// Token: 0x0400020D RID: 525
		public static Condition DownedNiflheim = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNiflheim"), () => ModConditions.DownedBoss[68]);

		// Token: 0x0400020E RID: 526
		public static bool downedStellarStarship;

		// Token: 0x0400020F RID: 527
		public static Condition DownedStellarStarship = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStellarStarship"), () => ModConditions.DownedBoss[69]);

		// Token: 0x04000210 RID: 528
		public static bool exxoAvalonOriginsLoaded;

		// Token: 0x04000211 RID: 529
		public static Mod exxoAvalonOriginsMod;

		// Token: 0x04000212 RID: 530
		public static bool downedBacteriumPrime;

		// Token: 0x04000213 RID: 531
		public static Condition DownedBacteriumPrime = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBacteriumPrime"), () => ModConditions.DownedBoss[70]);

		// Token: 0x04000214 RID: 532
		public static Condition DownedAvalonEvilBosses = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAvalonEvilBosses"), () => ModConditions.DownedBoss[70] || NPC.downedBoss2);

		// Token: 0x04000215 RID: 533
		public static bool downedDesertBeak;

		// Token: 0x04000216 RID: 534
		public static Condition DownedDesertBeak = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDesertBeak"), () => ModConditions.DownedBoss[71]);

		// Token: 0x04000217 RID: 535
		public static bool downedKingSting;

		// Token: 0x04000218 RID: 536
		public static Condition DownedKingSting = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedKingSting"), () => ModConditions.DownedBoss[72]);

		// Token: 0x04000219 RID: 537
		public static bool downedMechasting;

		// Token: 0x0400021A RID: 538
		public static Condition DownedMechasting = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMechasting"), () => ModConditions.DownedBoss[73]);

		// Token: 0x0400021B RID: 539
		public static bool downedPhantasm;

		// Token: 0x0400021C RID: 540
		public static Condition DownedPhantasm = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPhantasm"), () => ModConditions.DownedBoss[74]);

		// Token: 0x0400021D RID: 541
		public static bool beenToContagion;

		// Token: 0x0400021E RID: 542
		public static Condition HasBeenToContagion = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToContagion"), () => ModConditions.beenToContagion);

		// Token: 0x0400021F RID: 543
		public static Condition HasBeenToAvalonEvilBiomes = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAvalonEvilBiomes"), () => ModConditions.beenToContagion || ModConditions.beenToCorruption || ModConditions.beenToCrimson);

		// Token: 0x04000220 RID: 544
		public static bool fargosMutantLoaded;

		// Token: 0x04000221 RID: 545
		public static Mod fargosMutantMod;

		// Token: 0x04000222 RID: 546
		public static bool fargosSoulsLoaded;

		// Token: 0x04000223 RID: 547
		public static Mod fargosSoulsMod;

		// Token: 0x04000224 RID: 548
		public static bool downedTrojanSquirrel;

		// Token: 0x04000225 RID: 549
		public static Condition DownedTrojanSquirrel = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTrojanSquirrel"), () => ModConditions.DownedBoss[75]);

		// Token: 0x04000226 RID: 550
		public static bool downedCursedCoffin;

		// Token: 0x04000227 RID: 551
		public static Condition DownedCursedCoffin = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCursedCoffin"), () => ModConditions.DownedBoss[76]);

		// Token: 0x04000228 RID: 552
		public static bool downedDeviantt;

		// Token: 0x04000229 RID: 553
		public static Condition DownedDeviantt = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDeviantt"), () => ModConditions.DownedBoss[77]);

		// Token: 0x0400022A RID: 554
		public static bool downedLifelight;

		// Token: 0x0400022B RID: 555
		public static Condition DownedLifelight = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLifelight"), () => ModConditions.DownedBoss[78]);

		// Token: 0x0400022C RID: 556
		public static bool downedBanishedBaron;

		// Token: 0x0400022D RID: 557
		public static Condition DownedBanishedBaron = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBanishedBaron"), () => ModConditions.DownedBoss[79]);

		// Token: 0x0400022E RID: 558
		public static bool downedEridanus;

		// Token: 0x0400022F RID: 559
		public static Condition DownedEridanus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEridanus"), () => ModConditions.DownedBoss[80]);

		// Token: 0x04000230 RID: 560
		public static bool downedAbominationn;

		// Token: 0x04000231 RID: 561
		public static Condition DownedAbominationn = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAbominationn"), () => ModConditions.DownedBoss[81]);

		// Token: 0x04000232 RID: 562
		public static bool downedMutant;

		// Token: 0x04000233 RID: 563
		public static Condition DownedMutant = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMutant"), () => ModConditions.DownedBoss[82]);

		// Token: 0x04000234 RID: 564
		public static bool fargosSoulsDLCLoaded;

		// Token: 0x04000235 RID: 565
		public static Mod fargosSoulsDLCMod;

		// Token: 0x04000236 RID: 566
		public static bool fargosSoulsExtrasLoaded;

		// Token: 0x04000237 RID: 567
		public static Mod fargosSoulsExtrasMod;

		// Token: 0x04000238 RID: 568
		public static bool fracturesOfPenumbraLoaded;

		// Token: 0x04000239 RID: 569
		public static Mod fracturesOfPenumbraMod;

		// Token: 0x0400023A RID: 570
		public static bool downedAlphaFrostjaw;

		// Token: 0x0400023B RID: 571
		public static Condition DownedAlphaFrostjaw = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAlphaFrostjaw"), () => ModConditions.DownedBoss[83]);

		// Token: 0x0400023C RID: 572
		public static bool downedSanguineElemental;

		// Token: 0x0400023D RID: 573
		public static Condition DownedSanguineElemental = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSanguineElemental"), () => ModConditions.DownedBoss[84]);

		// Token: 0x0400023E RID: 574
		public static bool beenToDread;

		// Token: 0x0400023F RID: 575
		public static Condition HasBeenToDread = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToDread"), () => ModConditions.beenToDread);

		// Token: 0x04000240 RID: 576
		public static bool furnitureFoodAndFunLoaded;

		// Token: 0x04000241 RID: 577
		public static Mod furnitureFoodAndFunMod;

		// Token: 0x04000242 RID: 578
		public static bool gameTerrariaLoaded;

		// Token: 0x04000243 RID: 579
		public static Mod gameTerrariaMod;

		// Token: 0x04000244 RID: 580
		public static bool downedLad;

		// Token: 0x04000245 RID: 581
		public static Condition DownedLad = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLad"), () => ModConditions.DownedBoss[85]);

		// Token: 0x04000246 RID: 582
		public static bool downedHornlitz;

		// Token: 0x04000247 RID: 583
		public static Condition DownedHornlitz = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHornlitz"), () => ModConditions.DownedBoss[86]);

		// Token: 0x04000248 RID: 584
		public static bool downedSnowDon;

		// Token: 0x04000249 RID: 585
		public static Condition DownedSnowDon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSnowDon"), () => ModConditions.DownedBoss[87]);

		// Token: 0x0400024A RID: 586
		public static bool downedStoffie;

		// Token: 0x0400024B RID: 587
		public static Condition DownedStoffie = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStoffie"), () => ModConditions.DownedBoss[88]);

		// Token: 0x0400024C RID: 588
		public static bool gensokyoLoaded;

		// Token: 0x0400024D RID: 589
		public static Mod gensokyoMod;

		// Token: 0x0400024E RID: 590
		public static bool downedLilyWhite;

		// Token: 0x0400024F RID: 591
		public static Condition DownedLilyWhite = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLilyWhite"), () => ModConditions.DownedBoss[89]);

		// Token: 0x04000250 RID: 592
		public static bool downedRumia;

		// Token: 0x04000251 RID: 593
		public static Condition DownedRumia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRumia"), () => ModConditions.DownedBoss[90]);

		// Token: 0x04000252 RID: 594
		public static bool downedEternityLarva;

		// Token: 0x04000253 RID: 595
		public static Condition DownedEternityLarva = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEternityLarva"), () => ModConditions.DownedBoss[91]);

		// Token: 0x04000254 RID: 596
		public static bool downedNazrin;

		// Token: 0x04000255 RID: 597
		public static Condition DownedNazrin = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNazrin"), () => ModConditions.DownedBoss[92]);

		// Token: 0x04000256 RID: 598
		public static bool downedHinaKagiyama;

		// Token: 0x04000257 RID: 599
		public static Condition DownedHinaKagiyama = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHinaKagiyama"), () => ModConditions.DownedBoss[93]);

		// Token: 0x04000258 RID: 600
		public static bool downedSekibanki;

		// Token: 0x04000259 RID: 601
		public static Condition DownedSekibanki = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSekibanki"), () => ModConditions.DownedBoss[94]);

		// Token: 0x0400025A RID: 602
		public static bool downedSeiran;

		// Token: 0x0400025B RID: 603
		public static Condition DownedSeiran = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSeiran"), () => ModConditions.DownedBoss[95]);

		// Token: 0x0400025C RID: 604
		public static bool downedNitoriKawashiro;

		// Token: 0x0400025D RID: 605
		public static Condition DownedNitoriKawashiro = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNitoriKawashiro"), () => ModConditions.DownedBoss[96]);

		// Token: 0x0400025E RID: 606
		public static bool downedMedicineMelancholy;

		// Token: 0x0400025F RID: 607
		public static Condition DownedMedicineMelancholy = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMedicineMelancholy"), () => ModConditions.DownedBoss[97]);

		// Token: 0x04000260 RID: 608
		public static bool downedCirno;

		// Token: 0x04000261 RID: 609
		public static Condition DownedCirno = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCirno"), () => ModConditions.DownedBoss[98]);

		// Token: 0x04000262 RID: 610
		public static bool downedMinamitsuMurasa;

		// Token: 0x04000263 RID: 611
		public static Condition DownedMinamitsuMurasa = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMinamitsuMurasa"), () => ModConditions.DownedBoss[99]);

		// Token: 0x04000264 RID: 612
		public static bool downedAliceMargatroid;

		// Token: 0x04000265 RID: 613
		public static Condition DownedAliceMargatroid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAliceMargatroid"), () => ModConditions.DownedBoss[100]);

		// Token: 0x04000266 RID: 614
		public static bool downedSakuyaIzayoi;

		// Token: 0x04000267 RID: 615
		public static Condition DownedSakuyaIzayoi = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSakuyaIzayoi"), () => ModConditions.DownedBoss[101]);

		// Token: 0x04000268 RID: 616
		public static bool downedSeijaKijin;

		// Token: 0x04000269 RID: 617
		public static Condition DownedSeijaKijin = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSeijaKijin"), () => ModConditions.DownedBoss[102]);

		// Token: 0x0400026A RID: 618
		public static bool downedMayumiJoutouguu;

		// Token: 0x0400026B RID: 619
		public static Condition DownedMayumiJoutouguu = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMayumiJoutouguu"), () => ModConditions.DownedBoss[103]);

		// Token: 0x0400026C RID: 620
		public static bool downedToyosatomimiNoMiko;

		// Token: 0x0400026D RID: 621
		public static Condition DownedToyosatomimiNoMiko = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedToyosatomimiNoMiko"), () => ModConditions.DownedBoss[104]);

		// Token: 0x0400026E RID: 622
		public static bool downedKaguyaHouraisan;

		// Token: 0x0400026F RID: 623
		public static Condition DownedKaguyaHouraisan = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedKaguyaHouraisan"), () => ModConditions.DownedBoss[105]);

		// Token: 0x04000270 RID: 624
		public static bool downedUtsuhoReiuji;

		// Token: 0x04000271 RID: 625
		public static Condition DownedUtsuhoReiuji = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedUtsuhoReiuji"), () => ModConditions.DownedBoss[106]);

		// Token: 0x04000272 RID: 626
		public static bool downedTenshiHinanawi;

		// Token: 0x04000273 RID: 627
		public static Condition DownedTenshiHinanawi = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTenshiHinanawi"), () => ModConditions.DownedBoss[107]);

		// Token: 0x04000274 RID: 628
		public static bool downedKisume;

		// Token: 0x04000275 RID: 629
		public static Condition DownedKisume = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedKisume"), () => ModConditions.DownedBoss[108]);

		// Token: 0x04000276 RID: 630
		public static bool gerdsLabLoaded;

		// Token: 0x04000277 RID: 631
		public static Mod gerdsLabMod;

		// Token: 0x04000278 RID: 632
		public static bool downedTrerios;

		// Token: 0x04000279 RID: 633
		public static Condition DownedTrerios = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTrerios"), () => ModConditions.DownedBoss[109]);

		// Token: 0x0400027A RID: 634
		public static bool downedMagmaEye;

		// Token: 0x0400027B RID: 635
		public static Condition DownedMagmaEye = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMagmaEye"), () => ModConditions.DownedBoss[110]);

		// Token: 0x0400027C RID: 636
		public static bool downedJack;

		// Token: 0x0400027D RID: 637
		public static Condition DownedJack = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedJack"), () => ModConditions.DownedBoss[111]);

		// Token: 0x0400027E RID: 638
		public static bool downedAcheron;

		// Token: 0x0400027F RID: 639
		public static Condition DownedAcheron = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAcheron"), () => ModConditions.DownedBoss[112]);

		// Token: 0x04000280 RID: 640
		public static bool heartbeatariaLoaded;

		// Token: 0x04000281 RID: 641
		public static Mod heartbeatariaMod;

		// Token: 0x04000282 RID: 642
		public static bool homewardJourneyLoaded;

		// Token: 0x04000283 RID: 643
		public static Mod homewardJourneyMod;

		// Token: 0x04000284 RID: 644
		public static bool downedMarquisMoonsquid;

		// Token: 0x04000285 RID: 645
		public static Condition DownedMarquisMoonsquid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMarquisMoonsquid"), () => ModConditions.DownedBoss[113]);

		// Token: 0x04000286 RID: 646
		public static bool downedPriestessRod;

		// Token: 0x04000287 RID: 647
		public static Condition DownedPriestessRod = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPriestessRod"), () => ModConditions.DownedBoss[114]);

		// Token: 0x04000288 RID: 648
		public static bool downedDiver;

		// Token: 0x04000289 RID: 649
		public static Condition DownedDiver = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDiver"), () => ModConditions.DownedBoss[115]);

		// Token: 0x0400028A RID: 650
		public static bool downedMotherbrain;

		// Token: 0x0400028B RID: 651
		public static Condition DownedMotherbrain = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMotherbrain"), () => ModConditions.DownedBoss[116]);

		// Token: 0x0400028C RID: 652
		public static bool downedWallOfShadow;

		// Token: 0x0400028D RID: 653
		public static Condition DownedWallOfShadow = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWallOfShadow"), () => ModConditions.DownedBoss[117]);

		// Token: 0x0400028E RID: 654
		public static bool downedSunSlimeGod;

		// Token: 0x0400028F RID: 655
		public static Condition DownedSunSlimeGod = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSunSlimeGod"), () => ModConditions.DownedBoss[118]);

		// Token: 0x04000290 RID: 656
		public static bool downedOverwatcher;

		// Token: 0x04000291 RID: 657
		public static Condition DownedOverwatcher = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOverwatcher"), () => ModConditions.DownedBoss[119]);

		// Token: 0x04000292 RID: 658
		public static bool downedLifebringer;

		// Token: 0x04000293 RID: 659
		public static Condition DownedLifebringer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLifebringer"), () => ModConditions.DownedBoss[120]);

		// Token: 0x04000294 RID: 660
		public static bool downedMaterealizer;

		// Token: 0x04000295 RID: 661
		public static Condition DownedMaterealizer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMaterealizer"), () => ModConditions.DownedBoss[121]);

		// Token: 0x04000296 RID: 662
		public static bool downedScarabBelief;

		// Token: 0x04000297 RID: 663
		public static Condition DownedScarabBelief = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedScarabBelief"), () => ModConditions.DownedBoss[122]);

		// Token: 0x04000298 RID: 664
		public static bool downedWorldsEndWhale;

		// Token: 0x04000299 RID: 665
		public static Condition DownedWorldsEndWhale = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWorldsEndWhale"), () => ModConditions.DownedBoss[123]);

		// Token: 0x0400029A RID: 666
		public static bool downedSon;

		// Token: 0x0400029B RID: 667
		public static Condition DownedSon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSon"), () => ModConditions.DownedBoss[124]);

		// Token: 0x0400029C RID: 668
		public static bool downedCaveOrdeal;

		// Token: 0x0400029D RID: 669
		public static Condition DownedCaveOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCaveOrdeal"), () => ModConditions.DownedBoss[125]);

		// Token: 0x0400029E RID: 670
		public static bool downedCorruptOrdeal;

		// Token: 0x0400029F RID: 671
		public static Condition DownedCorruptOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCorruptOrdeal"), () => ModConditions.DownedBoss[126]);

		// Token: 0x040002A0 RID: 672
		public static bool downedCrimsonOrdeal;

		// Token: 0x040002A1 RID: 673
		public static Condition DownedCrimsonOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCrimsonOrdeal"), () => ModConditions.DownedBoss[127]);

		// Token: 0x040002A2 RID: 674
		public static bool downedDesertOrdeal;

		// Token: 0x040002A3 RID: 675
		public static Condition DownedDesertOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDesertOrdeal"), () => ModConditions.DownedBoss[128]);

		// Token: 0x040002A4 RID: 676
		public static bool downedForestOrdeal;

		// Token: 0x040002A5 RID: 677
		public static Condition DownedForestOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedForestOrdeal"), () => ModConditions.DownedBoss[129]);

		// Token: 0x040002A6 RID: 678
		public static bool downedHallowOrdeal;

		// Token: 0x040002A7 RID: 679
		public static Condition DownedHallowOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHallowOrdeal"), () => ModConditions.DownedBoss[130]);

		// Token: 0x040002A8 RID: 680
		public static bool downedJungleOrdeal;

		// Token: 0x040002A9 RID: 681
		public static Condition DownedJungleOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedJungleOrdeal"), () => ModConditions.DownedBoss[131]);

		// Token: 0x040002AA RID: 682
		public static bool downedSkyOrdeal;

		// Token: 0x040002AB RID: 683
		public static Condition DownedSkyOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSkyOrdeal"), () => ModConditions.DownedBoss[132]);

		// Token: 0x040002AC RID: 684
		public static bool downedSnowOrdeal;

		// Token: 0x040002AD RID: 685
		public static Condition DownedSnowOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSnowOrdeal"), () => ModConditions.DownedBoss[133]);

		// Token: 0x040002AE RID: 686
		public static bool downedUnderworldOrdeal;

		// Token: 0x040002AF RID: 687
		public static Condition DownedUnderworldOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedUnderworldOrdeal"), () => ModConditions.DownedBoss[134]);

		// Token: 0x040002B0 RID: 688
		public static Condition DownedOrdealAny = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOrdealAny"), () => ModConditions.DownedBoss[125] || ModConditions.DownedBoss[126] || ModConditions.DownedBoss[127] || ModConditions.DownedBoss[128] || ModConditions.DownedBoss[129] || ModConditions.DownedBoss[130] || ModConditions.DownedBoss[131] || ModConditions.DownedBoss[132] || ModConditions.DownedBoss[133] || ModConditions.DownedBoss[134]);

		// Token: 0x040002B1 RID: 689
		public static bool beenToHomewardAbyss;

		// Token: 0x040002B2 RID: 690
		public static Condition HasBeenToHomewardAbyss = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToHomewardAbyss"), () => ModConditions.beenToHomewardAbyss);

		// Token: 0x040002B3 RID: 691
		public static bool huntOfTheOldGodLoaded;

		// Token: 0x040002B4 RID: 692
		public static Mod huntOfTheOldGodMod;

		// Token: 0x040002B5 RID: 693
		public static bool downedGoozma;

		// Token: 0x040002B6 RID: 694
		public static Condition DownedGoozma = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGoozma"), () => ModConditions.DownedBoss[135]);

		// Token: 0x040002B7 RID: 695
		public static bool infectedQualitiesLoaded;

		// Token: 0x040002B8 RID: 696
		public static Mod infectedQualitiesMod;

		// Token: 0x040002B9 RID: 697
		public static bool infernumLoaded;

		// Token: 0x040002BA RID: 698
		public static Mod infernumMod;

		// Token: 0x040002BB RID: 699
		public static bool downedBereftVassal;

		// Token: 0x040002BC RID: 700
		public static Condition DownedBereftVassal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBereftVassal"), () => ModConditions.DownedBoss[136]);

		// Token: 0x040002BD RID: 701
		public static bool beenToProfanedGardens;

		// Token: 0x040002BE RID: 702
		public static Condition HasBeenToProfanedGardens = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToProfanedGardens"), () => ModConditions.beenToProfanedGardens);

		// Token: 0x040002BF RID: 703
		public static bool luiAFKLoaded;

		// Token: 0x040002C0 RID: 704
		public static Mod luiAFKMod;

		// Token: 0x040002C1 RID: 705
		public static bool luiAFKDLCLoaded;

		// Token: 0x040002C2 RID: 706
		public static Mod luiAFKDLCMod;

		// Token: 0x040002C3 RID: 707
		public static bool lunarVeilLoaded;

		// Token: 0x040002C4 RID: 708
		public static Mod lunarVeilMod;

		// Token: 0x040002C5 RID: 709
		public static bool downedStoneGuardian;

		// Token: 0x040002C6 RID: 710
		public static Condition DownedStoneGuardian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStoneGuardian"), () => ModConditions.DownedBoss[137]);

		// Token: 0x040002C7 RID: 711
		public static bool downedCommanderGintzia;

		// Token: 0x040002C8 RID: 712
		public static Condition DownedCommanderGintzia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCommanderGintzia"), () => ModConditions.DownedBoss[138]);

		// Token: 0x040002C9 RID: 713
		public static bool downedSunStalker;

		// Token: 0x040002CA RID: 714
		public static Condition DownedSunStalker = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSunStalker"), () => ModConditions.DownedBoss[139]);

		// Token: 0x040002CB RID: 715
		public static bool downedPumpkinJack;

		// Token: 0x040002CC RID: 716
		public static Condition DownedPumpkinJack = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPumpkinJack"), () => ModConditions.DownedBoss[140]);

		// Token: 0x040002CD RID: 717
		public static bool downedForgottenPuppetDaedus;

		// Token: 0x040002CE RID: 718
		public static Condition DownedForgottenPuppetDaedus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedForgottenPuppetDaedus"), () => ModConditions.DownedBoss[141]);

		// Token: 0x040002CF RID: 719
		public static bool downedDreadMire;

		// Token: 0x040002D0 RID: 720
		public static Condition DownedDreadMire = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDreadMire"), () => ModConditions.DownedBoss[142]);

		// Token: 0x040002D1 RID: 721
		public static bool downedSingularityFragment;

		// Token: 0x040002D2 RID: 722
		public static Condition DownedSingularityFragment = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSingularityFragment"), () => ModConditions.DownedBoss[143]);

		// Token: 0x040002D3 RID: 723
		public static bool downedVerlia;

		// Token: 0x040002D4 RID: 724
		public static Condition DownedVerlia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedVerlia"), () => ModConditions.DownedBoss[144]);

		// Token: 0x040002D5 RID: 725
		public static bool downedIrradia;

		// Token: 0x040002D6 RID: 726
		public static Condition DownedIrradia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedIrradia"), () => ModConditions.DownedBoss[145]);

		// Token: 0x040002D7 RID: 727
		public static bool downedSylia;

		// Token: 0x040002D8 RID: 728
		public static Condition DownedSylia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSylia"), () => ModConditions.DownedBoss[146]);

		// Token: 0x040002D9 RID: 729
		public static bool downedFenix;

		// Token: 0x040002DA RID: 730
		public static Condition DownedFenix = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFenix"), () => ModConditions.DownedBoss[147]);

		// Token: 0x040002DB RID: 731
		public static bool downedBlazingSerpent;

		// Token: 0x040002DC RID: 732
		public static Condition DownedBlazingSerpent = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBlazingSerpent"), () => ModConditions.DownedBoss[148]);

		// Token: 0x040002DD RID: 733
		public static bool downedCogwork;

		// Token: 0x040002DE RID: 734
		public static Condition DownedCogwork = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCogwork"), () => ModConditions.DownedBoss[149]);

		// Token: 0x040002DF RID: 735
		public static bool downedWaterCogwork;

		// Token: 0x040002E0 RID: 736
		public static Condition DownedWaterCogwork = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWaterCogwork"), () => ModConditions.DownedBoss[150]);

		// Token: 0x040002E1 RID: 737
		public static bool downedWaterJellyfish;

		// Token: 0x040002E2 RID: 738
		public static Condition DownedWaterJellyfish = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWaterJellyfish"), () => ModConditions.DownedBoss[151]);

		// Token: 0x040002E3 RID: 739
		public static bool downedSparn;

		// Token: 0x040002E4 RID: 740
		public static Condition DownedSparn = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSparn"), () => ModConditions.DownedBoss[152]);

		// Token: 0x040002E5 RID: 741
		public static bool downedPandorasFlamebox;

		// Token: 0x040002E6 RID: 742
		public static Condition DownedPandorasFlamebox = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPandorasFlamebox"), () => ModConditions.DownedBoss[153]);

		// Token: 0x040002E7 RID: 743
		public static bool downedSTARBOMBER;

		// Token: 0x040002E8 RID: 744
		public static Condition DownedSTARBOMBER = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSTARBOMBER"), () => ModConditions.DownedBoss[154]);

		// Token: 0x040002E9 RID: 745
		public static Condition DownedWaterJellyfishOrWaterCogwork = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWaterJellyfishOrWaterCogwork"), () => ModConditions.DownedBoss[150] || ModConditions.DownedBoss[151]);

		// Token: 0x040002EA RID: 746
		public static Condition DownedCogworkOrSparn = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCogworkOrSparn"), () => ModConditions.DownedBoss[149] || ModConditions.DownedBoss[152]);

		// Token: 0x040002EB RID: 747
		public static Condition DownedBlazingSerpentOrPandorasFlamebox = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBlazingSerpentOrPandorasFlamebox"), () => ModConditions.DownedBoss[148] || ModConditions.DownedBoss[153]);

		// Token: 0x040002EC RID: 748
		public static bool downedGintzeArmy;

		// Token: 0x040002ED RID: 749
		public static Condition DownedGintzeArmy = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGintzeArmy"), () => ModConditions.DownedBoss[155]);

		// Token: 0x040002EE RID: 750
		public static bool beenToLunarVeilAbyss;

		// Token: 0x040002EF RID: 751
		public static Condition HasBeenToLunarVeilAbyss = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToLunarVeilAbyss"), () => ModConditions.beenToLunarVeilAbyss);

		// Token: 0x040002F0 RID: 752
		public static bool beenToAcid;

		// Token: 0x040002F1 RID: 753
		public static Condition HasBeenToAcid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAcid"), () => ModConditions.beenToAcid);

		// Token: 0x040002F2 RID: 754
		public static bool beenToAurelus;

		// Token: 0x040002F3 RID: 755
		public static Condition HasBeenToAurelus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAurelus"), () => ModConditions.beenToAurelus);

		// Token: 0x040002F4 RID: 756
		public static bool beenToFable;

		// Token: 0x040002F5 RID: 757
		public static Condition HasBeenToFable = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToFable"), () => ModConditions.beenToFable);

		// Token: 0x040002F6 RID: 758
		public static bool beenToGovheilCastle;

		// Token: 0x040002F7 RID: 759
		public static Condition HasBeenToGovheilCastle = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToGovheilCastle"), () => ModConditions.beenToGovheilCastle);

		// Token: 0x040002F8 RID: 760
		public static bool beenToCathedral;

		// Token: 0x040002F9 RID: 761
		public static Condition HasBeenToCathedral = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCathedral"), () => ModConditions.beenToCathedral);

		// Token: 0x040002FA RID: 762
		public static bool beenToMarrowSurface;

		// Token: 0x040002FB RID: 763
		public static Condition HasBeenToMarrowSurface = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToMarrowSurface"), () => ModConditions.beenToMarrowSurface);

		// Token: 0x040002FC RID: 764
		public static bool beenToMorrowUnderground;

		// Token: 0x040002FD RID: 765
		public static Condition HasBeenToMorrowUnderground = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToMorrowUnderground"), () => ModConditions.beenToMorrowUnderground);

		// Token: 0x040002FE RID: 766
		public static bool magicStorageLoaded;

		// Token: 0x040002FF RID: 767
		public static Mod magicStorageMod;

		// Token: 0x04000300 RID: 768
		public static bool martainsOrderLoaded;

		// Token: 0x04000301 RID: 769
		public static Mod martainsOrderMod;

		// Token: 0x04000302 RID: 770
		public static bool downedBritzz;

		// Token: 0x04000303 RID: 771
		public static Condition DownedBritzz = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBritzz"), () => ModConditions.DownedBoss[156]);

		// Token: 0x04000304 RID: 772
		public static bool downedTheAlchemist;

		// Token: 0x04000305 RID: 773
		public static Condition DownedTheAlchemist = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTheAlchemist"), () => ModConditions.DownedBoss[157]);

		// Token: 0x04000306 RID: 774
		public static bool downedCarnagePillar;

		// Token: 0x04000307 RID: 775
		public static Condition DownedCarnagePillar = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCarnagePillar"), () => ModConditions.DownedBoss[158]);

		// Token: 0x04000308 RID: 776
		public static bool downedVoidDigger;

		// Token: 0x04000309 RID: 777
		public static Condition DownedVoidDigger = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedVoidDigger"), () => ModConditions.DownedBoss[159]);

		// Token: 0x0400030A RID: 778
		public static bool downedPrinceSlime;

		// Token: 0x0400030B RID: 779
		public static Condition DownedPrinceSlime = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPrinceSlime"), () => ModConditions.DownedBoss[160]);

		// Token: 0x0400030C RID: 780
		public static bool downedTriplets;

		// Token: 0x0400030D RID: 781
		public static Condition DownedTriplets = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTriplets"), () => ModConditions.DownedBoss[161]);

		// Token: 0x0400030E RID: 782
		public static bool downedJungleDefenders;

		// Token: 0x0400030F RID: 783
		public static Condition DownedJungleDefenders = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedJungleDefenders"), () => ModConditions.DownedBoss[162]);

		// Token: 0x04000310 RID: 784
		public static bool mechReworkLoaded;

		// Token: 0x04000311 RID: 785
		public static Mod mechReworkMod;

		// Token: 0x04000312 RID: 786
		public static bool downedSt4sys;

		// Token: 0x04000313 RID: 787
		public static Condition DownedSt4sys = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSt4sys"), () => ModConditions.DownedBoss[163]);

		// Token: 0x04000314 RID: 788
		public static bool downedTerminator;

		// Token: 0x04000315 RID: 789
		public static Condition DownedTerminator = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTerminator"), () => ModConditions.DownedBoss[164]);

		// Token: 0x04000316 RID: 790
		public static bool downedCaretaker;

		// Token: 0x04000317 RID: 791
		public static Condition DownedCaretaker = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCaretaker"), () => ModConditions.DownedBoss[165]);

		// Token: 0x04000318 RID: 792
		public static bool downedSiegeEngine;

		// Token: 0x04000319 RID: 793
		public static Condition DownedSiegeEngine = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSiegeEngine"), () => ModConditions.DownedBoss[166]);

		// Token: 0x0400031A RID: 794
		public static bool medialRiftLoaded;

		// Token: 0x0400031B RID: 795
		public static Mod medialRiftMod;

		// Token: 0x0400031C RID: 796
		public static bool downedSuperVoltaicMotherSlime;

		// Token: 0x0400031D RID: 797
		public static Condition DownedSuperVoltaicMotherSlime = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSuperVoltaicMotherSlime"), () => ModConditions.DownedBoss[167]);

		// Token: 0x0400031E RID: 798
		public static bool metroidLoaded;

		// Token: 0x0400031F RID: 799
		public static Mod metroidMod;

		// Token: 0x04000320 RID: 800
		public static bool downedTorizo;

		// Token: 0x04000321 RID: 801
		public static Condition DownedTorizo = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTorizo"), () => ModConditions.DownedBoss[168]);

		// Token: 0x04000322 RID: 802
		public static bool downedSerris;

		// Token: 0x04000323 RID: 803
		public static Condition DownedSerris = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSerris"), () => ModConditions.DownedBoss[169]);

		// Token: 0x04000324 RID: 804
		public static bool downedKraid;

		// Token: 0x04000325 RID: 805
		public static Condition DownedKraid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedKraid"), () => ModConditions.DownedBoss[170]);

		// Token: 0x04000326 RID: 806
		public static bool downedPhantoon;

		// Token: 0x04000327 RID: 807
		public static Condition DownedPhantoon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPhantoon"), () => ModConditions.DownedBoss[171]);

		// Token: 0x04000328 RID: 808
		public static bool downedOmegaPirate;

		// Token: 0x04000329 RID: 809
		public static Condition DownedOmegaPirate = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOmegaPirate"), () => ModConditions.DownedBoss[172]);

		// Token: 0x0400032A RID: 810
		public static bool downedNightmare;

		// Token: 0x0400032B RID: 811
		public static Condition DownedNightmare = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNightmare"), () => ModConditions.DownedBoss[173]);

		// Token: 0x0400032C RID: 812
		public static bool downedGoldenTorizo;

		// Token: 0x0400032D RID: 813
		public static Condition DownedGoldenTorizo = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGoldenTorizo"), () => ModConditions.DownedBoss[174]);

		// Token: 0x0400032E RID: 814
		public static bool moomoosUltimateYoyoRevampLoaded;

		// Token: 0x0400032F RID: 815
		public static Mod moomoosUltimateYoyoRevampMod;

		// Token: 0x04000330 RID: 816
		public static bool mrPlagueRacesLoaded;

		// Token: 0x04000331 RID: 817
		public static Mod mrPlagueRacesMod;

		// Token: 0x04000332 RID: 818
		public static bool orchidLoaded;

		// Token: 0x04000333 RID: 819
		public static Mod orchidMod;

		// Token: 0x04000334 RID: 820
		public static bool ophioidLoaded;

		// Token: 0x04000335 RID: 821
		public static Mod ophioidMod;

		// Token: 0x04000336 RID: 822
		public static bool downedOphiopede;

		// Token: 0x04000337 RID: 823
		public static Condition DownedOphiopede = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOphiopede"), () => ModConditions.DownedBoss[175]);

		// Token: 0x04000338 RID: 824
		public static bool downedOphiocoon;

		// Token: 0x04000339 RID: 825
		public static Condition DownedOphiocoon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOphiocoon"), () => ModConditions.DownedBoss[176]);

		// Token: 0x0400033A RID: 826
		public static bool downedOphiofly;

		// Token: 0x0400033B RID: 827
		public static Condition DownedOphiofly = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOphiofly"), () => ModConditions.DownedBoss[177]);

		// Token: 0x0400033C RID: 828
		public static bool polaritiesLoaded;

		// Token: 0x0400033D RID: 829
		public static Mod polaritiesMod;

		// Token: 0x0400033E RID: 830
		public static bool downedStormCloudfish;

		// Token: 0x0400033F RID: 831
		public static Condition DownedStormCloudfish = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStormCloudfish"), () => ModConditions.DownedBoss[178]);

		// Token: 0x04000340 RID: 832
		public static bool downedStarConstruct;

		// Token: 0x04000341 RID: 833
		public static Condition DownedStarConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStarConstruct"), () => ModConditions.DownedBoss[179]);

		// Token: 0x04000342 RID: 834
		public static bool downedGigabat;

		// Token: 0x04000343 RID: 835
		public static Condition DownedGigabat = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGigabat"), () => ModConditions.DownedBoss[180]);

		// Token: 0x04000344 RID: 836
		public static bool downedSunPixie;

		// Token: 0x04000345 RID: 837
		public static Condition DownedSunPixie = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSunPixie"), () => ModConditions.DownedBoss[181]);

		// Token: 0x04000346 RID: 838
		public static bool downedEsophage;

		// Token: 0x04000347 RID: 839
		public static Condition DownedEsophage = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEsophage"), () => ModConditions.DownedBoss[182]);

		// Token: 0x04000348 RID: 840
		public static bool downedConvectiveWanderer;

		// Token: 0x04000349 RID: 841
		public static Condition DownedConvectiveWanderer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedConvectiveWanderer"), () => ModConditions.DownedBoss[183]);

		// Token: 0x0400034A RID: 842
		public static bool projectZeroLoaded;

		// Token: 0x0400034B RID: 843
		public static Mod projectZeroMod;

		// Token: 0x0400034C RID: 844
		public static bool downedForestGuardian;

		// Token: 0x0400034D RID: 845
		public static Condition DownedForestGuardian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedForestGuardian"), () => ModConditions.DownedBoss[184]);

		// Token: 0x0400034E RID: 846
		public static bool downedCryoGuardian;

		// Token: 0x0400034F RID: 847
		public static Condition DownedCryoGuardian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCryoGuardian"), () => ModConditions.DownedBoss[185]);

		// Token: 0x04000350 RID: 848
		public static bool downedPrimordialWorm;

		// Token: 0x04000351 RID: 849
		public static Condition DownedPrimordialWorm = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPrimordialWorm"), () => ModConditions.DownedBoss[186]);

		// Token: 0x04000352 RID: 850
		public static bool downedTheGuardianOfHell;

		// Token: 0x04000353 RID: 851
		public static Condition DownedTheGuardianOfHell = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTheGuardianOfHell"), () => ModConditions.DownedBoss[187]);

		// Token: 0x04000354 RID: 852
		public static bool downedVoid;

		// Token: 0x04000355 RID: 853
		public static Condition DownedVoid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedVoid"), () => ModConditions.DownedBoss[188]);

		// Token: 0x04000356 RID: 854
		public static bool downedArmagem;

		// Token: 0x04000357 RID: 855
		public static Condition DownedArmagem = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedArmagem"), () => ModConditions.DownedBoss[189]);

		// Token: 0x04000358 RID: 856
		public static bool qwertyLoaded;

		// Token: 0x04000359 RID: 857
		public static Mod qwertyMod;

		// Token: 0x0400035A RID: 858
		public static bool downedPolarExterminator;

		// Token: 0x0400035B RID: 859
		public static Condition DownedPolarExterminator = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPolarExterminator"), () => ModConditions.DownedBoss[190]);

		// Token: 0x0400035C RID: 860
		public static bool downedDivineLight;

		// Token: 0x0400035D RID: 861
		public static Condition DownedDivineLight = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDivineLight"), () => ModConditions.DownedBoss[191]);

		// Token: 0x0400035E RID: 862
		public static bool downedAncientMachine;

		// Token: 0x0400035F RID: 863
		public static Condition DownedAncientMachine = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAncientMachine"), () => ModConditions.DownedBoss[192]);

		// Token: 0x04000360 RID: 864
		public static bool downedNoehtnap;

		// Token: 0x04000361 RID: 865
		public static Condition DownedNoehtnap = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNoehtnap"), () => ModConditions.DownedBoss[193]);

		// Token: 0x04000362 RID: 866
		public static bool downedHydra;

		// Token: 0x04000363 RID: 867
		public static Condition DownedHydra = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHydra"), () => ModConditions.DownedBoss[194]);

		// Token: 0x04000364 RID: 868
		public static bool downedImperious;

		// Token: 0x04000365 RID: 869
		public static Condition DownedImperious = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedImperious"), () => ModConditions.DownedBoss[195]);

		// Token: 0x04000366 RID: 870
		public static bool downedRuneGhost;

		// Token: 0x04000367 RID: 871
		public static Condition DownedRuneGhost = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRuneGhost"), () => ModConditions.DownedBoss[196]);

		// Token: 0x04000368 RID: 872
		public static bool downedInvaderBattleship;

		// Token: 0x04000369 RID: 873
		public static Condition DownedInvaderBattleship = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInvaderBattleship"), () => ModConditions.DownedBoss[197]);

		// Token: 0x0400036A RID: 874
		public static bool downedInvaderNoehtnap;

		// Token: 0x0400036B RID: 875
		public static Condition DownedInvaderNoehtnap = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInvaderNoehtnap"), () => ModConditions.DownedBoss[198]);

		// Token: 0x0400036C RID: 876
		public static bool downedOLORD;

		// Token: 0x0400036D RID: 877
		public static Condition DownedOLORD = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOLORD"), () => ModConditions.DownedBoss[199]);

		// Token: 0x0400036E RID: 878
		public static bool downedGreatTyrannosaurus;

		// Token: 0x0400036F RID: 879
		public static Condition DownedGreatTyrannosaurus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGreatTyrannosaurus"), () => ModConditions.DownedBoss[200]);

		// Token: 0x04000370 RID: 880
		public static bool downedDinoMilitia;

		// Token: 0x04000371 RID: 881
		public static Condition DownedDinoMilitia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDinoMilitia"), () => ModConditions.DownedBoss[201]);

		// Token: 0x04000372 RID: 882
		public static bool downedInvaders;

		// Token: 0x04000373 RID: 883
		public static Condition DownedInvaders = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInvaders"), () => ModConditions.DownedBoss[202]);

		// Token: 0x04000374 RID: 884
		public static bool beenToSkyFortress;

		// Token: 0x04000375 RID: 885
		public static Condition HasBeenToSkyFortress = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSkyFortress"), () => ModConditions.beenToSkyFortress);

		// Token: 0x04000376 RID: 886
		public static bool ragnarokLoaded;

		// Token: 0x04000377 RID: 887
		public static Mod ragnarokMod;

		// Token: 0x04000378 RID: 888
		public static bool redemptionLoaded;

		// Token: 0x04000379 RID: 889
		public static Mod redemptionMod;

		// Token: 0x0400037A RID: 890
		public static bool downedThorn;

		// Token: 0x0400037B RID: 891
		public static Condition DownedThorn = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedThorn"), () => ModConditions.DownedBoss[203]);

		// Token: 0x0400037C RID: 892
		public static bool downedErhan;

		// Token: 0x0400037D RID: 893
		public static Condition DownedErhan = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedErhan"), () => ModConditions.DownedBoss[204]);

		// Token: 0x0400037E RID: 894
		public static bool downedKeeper;

		// Token: 0x0400037F RID: 895
		public static Condition DownedKeeper = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedKeeper"), () => ModConditions.DownedBoss[205]);

		// Token: 0x04000380 RID: 896
		public static bool downedSeedOfInfection;

		// Token: 0x04000381 RID: 897
		public static Condition DownedSeedOfInfection = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSeedOfInfection"), () => ModConditions.DownedBoss[206]);

		// Token: 0x04000382 RID: 898
		public static bool downedKingSlayerIII;

		// Token: 0x04000383 RID: 899
		public static Condition DownedKingSlayerIII = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedKingSlayerIII"), () => ModConditions.DownedBoss[207]);

		// Token: 0x04000384 RID: 900
		public static bool downedOmegaCleaver;

		// Token: 0x04000385 RID: 901
		public static Condition DownedOmegaCleaver = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOmegaCleaver"), () => ModConditions.DownedBoss[208]);

		// Token: 0x04000386 RID: 902
		public static bool downedOmegaGigapora;

		// Token: 0x04000387 RID: 903
		public static Condition DownedOmegaGigapora = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOmegaGigapora"), () => ModConditions.DownedBoss[209]);

		// Token: 0x04000388 RID: 904
		public static bool downedOmegaObliterator;

		// Token: 0x04000389 RID: 905
		public static Condition DownedOmegaObliterator = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOmegaObliterator"), () => ModConditions.DownedBoss[210]);

		// Token: 0x0400038A RID: 906
		public static bool downedPatientZero;

		// Token: 0x0400038B RID: 907
		public static Condition DownedPatientZero = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPatientZero"), () => ModConditions.DownedBoss[211]);

		// Token: 0x0400038C RID: 908
		public static bool downedAkka;

		// Token: 0x0400038D RID: 909
		public static Condition DownedAkka = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAkka"), () => ModConditions.DownedBoss[212]);

		// Token: 0x0400038E RID: 910
		public static bool downedUkko;

		// Token: 0x0400038F RID: 911
		public static Condition DownedUkko = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedUkko"), () => ModConditions.DownedBoss[213]);

		// Token: 0x04000390 RID: 912
		public static bool downedAncientDeityDuo;

		// Token: 0x04000391 RID: 913
		public static Condition DownedAncientDeityDuo = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAncientDeityDuo"), () => ModConditions.DownedBoss[214]);

		// Token: 0x04000392 RID: 914
		public static bool downedNebuleus;

		// Token: 0x04000393 RID: 915
		public static Condition DownedNebuleus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNebuleus"), () => ModConditions.DownedBoss[215]);

		// Token: 0x04000394 RID: 916
		public static bool downedFowlEmperor;

		// Token: 0x04000395 RID: 917
		public static Condition DownedFowlEmperor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFowlEmperor"), () => ModConditions.DownedBoss[216]);

		// Token: 0x04000396 RID: 918
		public static bool downedCockatrice;

		// Token: 0x04000397 RID: 919
		public static Condition DownedCockatrice = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCockatrice"), () => ModConditions.DownedBoss[217]);

		// Token: 0x04000398 RID: 920
		public static bool downedBasan;

		// Token: 0x04000399 RID: 921
		public static Condition DownedBasan = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBasan"), () => ModConditions.DownedBoss[218]);

		// Token: 0x0400039A RID: 922
		public static bool downedSkullDigger;

		// Token: 0x0400039B RID: 923
		public static Condition DownedSkullDigger = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSkullDigger"), () => ModConditions.DownedBoss[219]);

		// Token: 0x0400039C RID: 924
		public static bool downedEaglecrestGolem;

		// Token: 0x0400039D RID: 925
		public static Condition DownedEaglecrestGolem = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEaglecrestGolem"), () => ModConditions.DownedBoss[220]);

		// Token: 0x0400039E RID: 926
		public static bool downedCalavia;

		// Token: 0x0400039F RID: 927
		public static Condition DownedCalavia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCalavia"), () => ModConditions.DownedBoss[221]);

		// Token: 0x040003A0 RID: 928
		public static bool downedTheJanitor;

		// Token: 0x040003A1 RID: 929
		public static Condition DownedTheJanitor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTheJanitor"), () => ModConditions.DownedBoss[222]);

		// Token: 0x040003A2 RID: 930
		public static bool downedIrradiatedBehemoth;

		// Token: 0x040003A3 RID: 931
		public static Condition DownedIrradiatedBehemoth = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedIrradiatedBehemoth"), () => ModConditions.DownedBoss[223]);

		// Token: 0x040003A4 RID: 932
		public static bool downedBlisterface;

		// Token: 0x040003A5 RID: 933
		public static Condition DownedBlisterface = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBlisterface"), () => ModConditions.DownedBoss[224]);

		// Token: 0x040003A6 RID: 934
		public static bool downedProtectorVolt;

		// Token: 0x040003A7 RID: 935
		public static Condition DownedProtectorVolt = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedProtectorVolt"), () => ModConditions.DownedBoss[225]);

		// Token: 0x040003A8 RID: 936
		public static bool downedMACEProject;

		// Token: 0x040003A9 RID: 937
		public static Condition DownedMACEProject = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMACEProject"), () => ModConditions.DownedBoss[226]);

		// Token: 0x040003AA RID: 938
		public static bool downedFowlMorning;

		// Token: 0x040003AB RID: 939
		public static Condition DownedFowlMorning = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFowlMorning"), () => ModConditions.DownedBoss[227]);

		// Token: 0x040003AC RID: 940
		public static bool downedRaveyard;

		// Token: 0x040003AD RID: 941
		public static Condition DownedRaveyard = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRaveyard"), () => ModConditions.DownedBoss[228]);

		// Token: 0x040003AE RID: 942
		public static bool beenToLab;

		// Token: 0x040003AF RID: 943
		public static Condition HasBeenToLab = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToLab"), () => ModConditions.beenToLab);

		// Token: 0x040003B0 RID: 944
		public static bool beenToWasteland;

		// Token: 0x040003B1 RID: 945
		public static Condition HasBeenToWasteland = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToWasteland"), () => ModConditions.beenToWasteland);

		// Token: 0x040003B2 RID: 946
		public static bool reforgedLoaded;

		// Token: 0x040003B3 RID: 947
		public static Mod reforgedMod;

		// Token: 0x040003B4 RID: 948
		public static bool remnantsLoaded;

		// Token: 0x040003B5 RID: 949
		public static Mod remnantsMod;

		// Token: 0x040003B6 RID: 950
		public static bool ruptureLoaded;

		// Token: 0x040003B7 RID: 951
		public static Mod ruptureMod;

		// Token: 0x040003B8 RID: 952
		public static bool secretsOfTheShadowsLoaded;

		// Token: 0x040003B9 RID: 953
		public static Mod secretsOfTheShadowsMod;

		// Token: 0x040003BA RID: 954
		public static bool downedPutridPinky;

		// Token: 0x040003BB RID: 955
		public static Condition DownedPutridPinky = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPutridPinky"), () => ModConditions.DownedBoss[229]);

		// Token: 0x040003BC RID: 956
		public static bool downedGlowmoth;

		// Token: 0x040003BD RID: 957
		public static Condition DownedGlowmoth = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGlowmoth"), () => ModConditions.DownedBoss[230]);

		// Token: 0x040003BE RID: 958
		public static bool downedPharaohsCurse;

		// Token: 0x040003BF RID: 959
		public static Condition DownedPharaohsCurse = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPharaohsCurse"), () => ModConditions.DownedBoss[231]);

		// Token: 0x040003C0 RID: 960
		public static bool downedAdvisor;

		// Token: 0x040003C1 RID: 961
		public static Condition DownedAdvisor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAdvisor"), () => ModConditions.DownedBoss[232]);

		// Token: 0x040003C2 RID: 962
		public static bool downedPolaris;

		// Token: 0x040003C3 RID: 963
		public static Condition DownedPolaris = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPolaris"), () => ModConditions.DownedBoss[233]);

		// Token: 0x040003C4 RID: 964
		public static bool downedLux;

		// Token: 0x040003C5 RID: 965
		public static Condition DownedLux = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLux"), () => ModConditions.DownedBoss[234]);

		// Token: 0x040003C6 RID: 966
		public static bool downedSubspaceSerpent;

		// Token: 0x040003C7 RID: 967
		public static Condition DownedSubspaceSerpent = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSubspaceSerpent"), () => ModConditions.DownedBoss[235]);

		// Token: 0x040003C8 RID: 968
		public static bool downedNatureConstruct;

		// Token: 0x040003C9 RID: 969
		public static Condition DownedNatureConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNatureConstruct"), () => ModConditions.DownedBoss[236]);

		// Token: 0x040003CA RID: 970
		public static bool downedEarthenConstruct;

		// Token: 0x040003CB RID: 971
		public static Condition DownedEarthenConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEarthenConstruct"), () => ModConditions.DownedBoss[237]);

		// Token: 0x040003CC RID: 972
		public static bool downedPermafrostConstruct;

		// Token: 0x040003CD RID: 973
		public static Condition DownedPermafrostConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPermafrostConstruct"), () => ModConditions.DownedBoss[238]);

		// Token: 0x040003CE RID: 974
		public static bool downedTidalConstruct;

		// Token: 0x040003CF RID: 975
		public static Condition DownedTidalConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTidalConstruct"), () => ModConditions.DownedBoss[239]);

		// Token: 0x040003D0 RID: 976
		public static bool downedOtherworldlyConstruct;

		// Token: 0x040003D1 RID: 977
		public static Condition DownedOtherworldlyConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOtherworldlyConstruct"), () => ModConditions.DownedBoss[240]);

		// Token: 0x040003D2 RID: 978
		public static bool downedEvilConstruct;

		// Token: 0x040003D3 RID: 979
		public static Condition DownedEvilConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEvilConstruct"), () => ModConditions.DownedBoss[241]);

		// Token: 0x040003D4 RID: 980
		public static bool downedInfernoConstruct;

		// Token: 0x040003D5 RID: 981
		public static Condition DownedInfernoConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInfernoConstruct"), () => ModConditions.DownedBoss[242]);

		// Token: 0x040003D6 RID: 982
		public static bool downedChaosConstruct;

		// Token: 0x040003D7 RID: 983
		public static Condition DownedChaosConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedChaosConstruct"), () => ModConditions.DownedBoss[243]);

		// Token: 0x040003D8 RID: 984
		public static bool downedNatureSpirit;

		// Token: 0x040003D9 RID: 985
		public static Condition DownedNatureSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNatureSpirit"), () => ModConditions.DownedBoss[244]);

		// Token: 0x040003DA RID: 986
		public static bool downedEarthenSpirit;

		// Token: 0x040003DB RID: 987
		public static Condition DownedEarthenSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEarthenSpirit"), () => ModConditions.DownedBoss[245]);

		// Token: 0x040003DC RID: 988
		public static bool downedPermafrostSpirit;

		// Token: 0x040003DD RID: 989
		public static Condition DownedPermafrostSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPermafrostSpirit"), () => ModConditions.DownedBoss[246]);

		// Token: 0x040003DE RID: 990
		public static bool downedTidalSpirit;

		// Token: 0x040003DF RID: 991
		public static Condition DownedTidalSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTidalSpirit"), () => ModConditions.DownedBoss[247]);

		// Token: 0x040003E0 RID: 992
		public static bool downedOtherworldlySpirit;

		// Token: 0x040003E1 RID: 993
		public static Condition DownedOtherworldlySpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOtherworldlySpirit"), () => ModConditions.DownedBoss[248]);

		// Token: 0x040003E2 RID: 994
		public static bool downedEvilSpirit;

		// Token: 0x040003E3 RID: 995
		public static Condition DownedEvilSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEvilSpirit"), () => ModConditions.DownedBoss[249]);

		// Token: 0x040003E4 RID: 996
		public static bool downedInfernoSpirit;

		// Token: 0x040003E5 RID: 997
		public static Condition DownedInfernoSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInfernoSpirit"), () => ModConditions.DownedBoss[250]);

		// Token: 0x040003E6 RID: 998
		public static bool downedChaosSpirit;

		// Token: 0x040003E7 RID: 999
		public static Condition DownedChaosSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedChaosSpirit"), () => ModConditions.DownedBoss[251]);

		// Token: 0x040003E8 RID: 1000
		public static bool beenToPyramid;

		// Token: 0x040003E9 RID: 1001
		public static Condition HasBeenToPyramid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToPyramid"), () => ModConditions.beenToPyramid);

		// Token: 0x040003EA RID: 1002
		public static bool beenToPlanetarium;

		// Token: 0x040003EB RID: 1003
		public static Condition HasBeenToPlanetarium = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToPlanetarium"), () => ModConditions.beenToPlanetarium);

		// Token: 0x040003EC RID: 1004
		public static bool shadowsOfAbaddonLoaded;

		// Token: 0x040003ED RID: 1005
		public static Mod shadowsOfAbaddonMod;

		// Token: 0x040003EE RID: 1006
		public static bool downedDecree;

		// Token: 0x040003EF RID: 1007
		public static Condition DownedDecree = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDecree"), () => ModConditions.DownedBoss[252]);

		// Token: 0x040003F0 RID: 1008
		public static bool downedFlamingPumpkin;

		// Token: 0x040003F1 RID: 1009
		public static Condition DownedFlamingPumpkin = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFlamingPumpkin"), () => ModConditions.DownedBoss[253]);

		// Token: 0x040003F2 RID: 1010
		public static bool downedZombiePiglinBrute;

		// Token: 0x040003F3 RID: 1011
		public static Condition DownedZombiePiglinBrute = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedZombiePiglinBrute"), () => ModConditions.DownedBoss[254]);

		// Token: 0x040003F4 RID: 1012
		public static bool downedJensenTheGrandHarpy;

		// Token: 0x040003F5 RID: 1013
		public static Condition DownedJensenTheGrandHarpy = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedJensenTheGrandHarpy"), () => ModConditions.DownedBoss[255]);

		// Token: 0x040003F6 RID: 1014
		public static bool downedAraneas;

		// Token: 0x040003F7 RID: 1015
		public static Condition DownedAraneas = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAraneas"), () => ModConditions.DownedBoss[256]);

		// Token: 0x040003F8 RID: 1016
		public static bool downedHarpyQueenRaynare;

		// Token: 0x040003F9 RID: 1017
		public static Condition DownedHarpyQueenRaynare = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHarpyQueenRaynare"), () => ModConditions.DownedBoss[257]);

		// Token: 0x040003FA RID: 1018
		public static bool downedPrimordia;

		// Token: 0x040003FB RID: 1019
		public static Condition DownedPrimordia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPrimordia"), () => ModConditions.DownedBoss[258]);

		// Token: 0x040003FC RID: 1020
		public static bool downedAbaddon;

		// Token: 0x040003FD RID: 1021
		public static Condition DownedAbaddon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAbaddon"), () => ModConditions.DownedBoss[259]);

		// Token: 0x040003FE RID: 1022
		public static bool downedAraghur;

		// Token: 0x040003FF RID: 1023
		public static Condition DownedAraghur = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAraghur"), () => ModConditions.DownedBoss[260]);

		// Token: 0x04000400 RID: 1024
		public static bool downedLostSiblings;

		// Token: 0x04000401 RID: 1025
		public static Condition DownedLostSiblings = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLostSiblings"), () => ModConditions.DownedBoss[261]);

		// Token: 0x04000402 RID: 1026
		public static bool downedErazor;

		// Token: 0x04000403 RID: 1027
		public static Condition DownedErazor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedErazor"), () => ModConditions.DownedBoss[262]);

		// Token: 0x04000404 RID: 1028
		public static bool downedNihilus;

		// Token: 0x04000405 RID: 1029
		public static Condition DownedNihilus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNihilus"), () => ModConditions.DownedBoss[263]);

		// Token: 0x04000406 RID: 1030
		public static bool beenToCinderForest;

		// Token: 0x04000407 RID: 1031
		public static Condition HasBeenToCinderForest = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCinderForest"), () => ModConditions.beenToCinderForest);

		// Token: 0x04000408 RID: 1032
		public static bool sloomeLoaded;

		// Token: 0x04000409 RID: 1033
		public static Mod sloomeMod;

		// Token: 0x0400040A RID: 1034
		public static bool downedExodygen;

		// Token: 0x0400040B RID: 1035
		public static Condition DownedExodygen = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedExodygen"), () => ModConditions.DownedBoss[264]);

		// Token: 0x0400040C RID: 1036
		public static bool spiritLoaded;

		// Token: 0x0400040D RID: 1037
		public static Mod spiritMod;

		// Token: 0x0400040E RID: 1038
		public static bool downedScarabeus;

		// Token: 0x0400040F RID: 1039
		public static Condition DownedScarabeus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedScarabeus"), () => ModConditions.DownedBoss[265]);

		// Token: 0x04000410 RID: 1040
		public static bool downedMoonJellyWizard;

		// Token: 0x04000411 RID: 1041
		public static Condition DownedMoonJellyWizard = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMoonJellyWizard"), () => ModConditions.DownedBoss[266]);

		// Token: 0x04000412 RID: 1042
		public static bool downedVinewrathBane;

		// Token: 0x04000413 RID: 1043
		public static Condition DownedVinewrathBane = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedVinewrathBane"), () => ModConditions.DownedBoss[267]);

		// Token: 0x04000414 RID: 1044
		public static bool downedAncientAvian;

		// Token: 0x04000415 RID: 1045
		public static Condition DownedAncientAvian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAncientAvian"), () => ModConditions.DownedBoss[268]);

		// Token: 0x04000416 RID: 1046
		public static bool downedStarplateVoyager;

		// Token: 0x04000417 RID: 1047
		public static Condition DownedStarplateVoyager = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStarplateVoyager"), () => ModConditions.DownedBoss[269]);

		// Token: 0x04000418 RID: 1048
		public static bool downedInfernon;

		// Token: 0x04000419 RID: 1049
		public static Condition DownedInfernon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInfernon"), () => ModConditions.DownedBoss[270]);

		// Token: 0x0400041A RID: 1050
		public static bool downedDusking;

		// Token: 0x0400041B RID: 1051
		public static Condition DownedDusking = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDusking"), () => ModConditions.DownedBoss[271]);

		// Token: 0x0400041C RID: 1052
		public static bool downedAtlas;

		// Token: 0x0400041D RID: 1053
		public static Condition DownedAtlas = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAtlas"), () => ModConditions.DownedBoss[272]);

		// Token: 0x0400041E RID: 1054
		public bool waitForJellyDeluge;

		// Token: 0x0400041F RID: 1055
		public static bool downedJellyDeluge;

		// Token: 0x04000420 RID: 1056
		public static Condition DownedJellyDeluge = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedJellyDeluge"), () => ModConditions.DownedBoss[273]);

		// Token: 0x04000421 RID: 1057
		public static bool downedTide;

		// Token: 0x04000422 RID: 1058
		public static Condition DownedTide = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTide"), () => ModConditions.DownedBoss[274]);

		// Token: 0x04000423 RID: 1059
		public bool waitForMysticMoon;

		// Token: 0x04000424 RID: 1060
		public static bool downedMysticMoon;

		// Token: 0x04000425 RID: 1061
		public static Condition DownedMysticMoon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMysticMoon"), () => ModConditions.DownedBoss[275]);

		// Token: 0x04000426 RID: 1062
		public static bool beenToBriar;

		// Token: 0x04000427 RID: 1063
		public static Condition HasBeenToBriar = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToBriar"), () => ModConditions.beenToBriar);

		// Token: 0x04000428 RID: 1064
		public static bool beenToSpirit;

		// Token: 0x04000429 RID: 1065
		public static Condition HasBeenToSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSpirit"), () => ModConditions.beenToSpirit);

		// Token: 0x0400042A RID: 1066
		public static bool spookyLoaded;

		// Token: 0x0400042B RID: 1067
		public static Mod spookyMod;

		// Token: 0x0400042C RID: 1068
		public static bool downedSpookySpirit;

		// Token: 0x0400042D RID: 1069
		public static Condition DownedSpookySpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSpookySpirit"), () => ModConditions.DownedBoss[276]);

		// Token: 0x0400042E RID: 1070
		public static bool downedRotGourd;

		// Token: 0x0400042F RID: 1071
		public static Condition DownedRotGourd = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRotGourd"), () => ModConditions.DownedBoss[277]);

		// Token: 0x04000430 RID: 1072
		public static bool downedMoco;

		// Token: 0x04000431 RID: 1073
		public static Condition DownedMoco = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMoco"), () => ModConditions.DownedBoss[278]);

		// Token: 0x04000432 RID: 1074
		public static bool downedDaffodil;

		// Token: 0x04000433 RID: 1075
		public static Condition DownedDaffodil = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDaffodil"), () => ModConditions.DownedBoss[279]);

		// Token: 0x04000434 RID: 1076
		public static bool downedOrroBoro;

		// Token: 0x04000435 RID: 1077
		public static bool downedOrro;

		// Token: 0x04000436 RID: 1078
		public static bool downedBoro;

		// Token: 0x04000437 RID: 1079
		public static Condition DownedOrroBoro = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOrroBoro"), () => ModConditions.DownedBoss[280]);

		// Token: 0x04000438 RID: 1080
		public static bool downedBigBone;

		// Token: 0x04000439 RID: 1081
		public static Condition DownedBigBone = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBigBone"), () => ModConditions.DownedBoss[281]);

		// Token: 0x0400043A RID: 1082
		public static bool beenToSpookyForest;

		// Token: 0x0400043B RID: 1083
		public static Condition HasBeenToSpookyForest = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSpookyForest"), () => ModConditions.beenToSpookyForest);

		// Token: 0x0400043C RID: 1084
		public static bool beenToSpookyUnderground;

		// Token: 0x0400043D RID: 1085
		public static Condition HasBeenToSpookyUnderground = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSpookyUnderground"), () => ModConditions.beenToSpookyUnderground);

		// Token: 0x0400043E RID: 1086
		public static bool beenToEyeValley;

		// Token: 0x0400043F RID: 1087
		public static Condition HasBeenToEyeValley = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToEyeValley"), () => ModConditions.beenToEyeValley);

		// Token: 0x04000440 RID: 1088
		public static bool beenToSpiderCave;

		// Token: 0x04000441 RID: 1089
		public static Condition HasBeenToSpiderCave = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSpiderCave"), () => ModConditions.beenToSpiderCave);

		// Token: 0x04000442 RID: 1090
		public static bool beenToCatacombs;

		// Token: 0x04000443 RID: 1091
		public static Condition HasBeenToCatacombs = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCatacombs"), () => ModConditions.beenToCatacombs);

		// Token: 0x04000444 RID: 1092
		public static bool beenToCemetery;

		// Token: 0x04000445 RID: 1093
		public static Condition HasBeenToCemetery = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCemetery"), () => ModConditions.beenToCemetery);

		// Token: 0x04000446 RID: 1094
		public static bool starlightRiverLoaded;

		// Token: 0x04000447 RID: 1095
		public static Mod starlightRiverMod;

		// Token: 0x04000448 RID: 1096
		public static bool downedAuroracle;

		// Token: 0x04000449 RID: 1097
		public static Condition DownedAuroracle = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAuroracle"), () => ModConditions.DownedBoss[282]);

		// Token: 0x0400044A RID: 1098
		public static bool downedCeiros;

		// Token: 0x0400044B RID: 1099
		public static Condition DownedCeiros = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCeiros"), () => ModConditions.DownedBoss[283]);

		// Token: 0x0400044C RID: 1100
		public static bool downedGlassweaver;

		// Token: 0x0400044D RID: 1101
		public static Condition DownedGlassweaver = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGlassweaver"), () => ModConditions.DownedBoss[284]);

		// Token: 0x0400044E RID: 1102
		public static bool beenToAuroracleTemple;

		// Token: 0x0400044F RID: 1103
		public static Condition HasBeenToAuroracleTemple = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAuroracleTemple"), () => ModConditions.beenToAuroracleTemple);

		// Token: 0x04000450 RID: 1104
		public static bool beenToVitricDesert;

		// Token: 0x04000451 RID: 1105
		public static Condition HasBeenToVitricDesert = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToVitricDesert"), () => ModConditions.beenToVitricDesert);

		// Token: 0x04000452 RID: 1106
		public static bool beenToVitricTemple;

		// Token: 0x04000453 RID: 1107
		public static Condition HasBeenToVitricTemple = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToVitricTemple"), () => ModConditions.beenToVitricTemple);

		// Token: 0x04000454 RID: 1108
		public static bool starsAboveLoaded;

		// Token: 0x04000455 RID: 1109
		public static Mod starsAboveMod;

		// Token: 0x04000456 RID: 1110
		public static bool downedVagrantofSpace;

		// Token: 0x04000457 RID: 1111
		public static Condition DownedVagrantofSpace = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedVagrantofSpace"), () => ModConditions.downedVagrantofSpace);

		// Token: 0x04000458 RID: 1112
		public static bool downedThespian;

		// Token: 0x04000459 RID: 1113
		public static Condition DownedThespian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedThespian"), () => ModConditions.downedThespian);

		// Token: 0x0400045A RID: 1114
		public static bool downedCastor;

		// Token: 0x0400045B RID: 1115
		public static bool downedPollux;

		// Token: 0x0400045C RID: 1116
		public static bool downedDioskouroi;

		// Token: 0x0400045D RID: 1117
		public static Condition DownedDioskouroi = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDioskouroi"), () => ModConditions.downedDioskouroi);

		// Token: 0x0400045E RID: 1118
		public static bool downedNalhaun;

		// Token: 0x0400045F RID: 1119
		public static Condition DownedNalhaun = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNalhaun"), () => ModConditions.downedNalhaun);

		// Token: 0x04000460 RID: 1120
		public static bool downedStarfarers;

		// Token: 0x04000461 RID: 1121
		public static Condition DownedStarfarers = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStarfarers"), () => ModConditions.downedStarfarers);

		// Token: 0x04000462 RID: 1122
		public static bool downedPenthesilea;

		// Token: 0x04000463 RID: 1123
		public static Condition DownedPenthesilea = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPenthesilea"), () => ModConditions.downedPenthesilea);

		// Token: 0x04000464 RID: 1124
		public static bool downedArbitration;

		// Token: 0x04000465 RID: 1125
		public static Condition DownedArbitration = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedArbitration"), () => ModConditions.downedArbitration);

		// Token: 0x04000466 RID: 1126
		public static bool downedWarriorOfLight;

		// Token: 0x04000467 RID: 1127
		public static Condition DownedWarriorOfLight = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWarriorOfLight"), () => ModConditions.downedWarriorOfLight);

		// Token: 0x04000468 RID: 1128
		public static bool downedTsukiyomi;

		// Token: 0x04000469 RID: 1129
		public static Condition DownedTsukiyomi = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTsukiyomi"), () => ModConditions.downedTsukiyomi);

		// Token: 0x0400046A RID: 1130
		public static bool stormsAdditionsLoaded;

		// Token: 0x0400046B RID: 1131
		public static Mod stormsAdditionsMod;

		// Token: 0x0400046C RID: 1132
		public static bool downedAncientHusk;

		// Token: 0x0400046D RID: 1133
		public static Condition DownedAncientHusk = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAncientHusk"), () => ModConditions.DownedBoss[294]);

		// Token: 0x0400046E RID: 1134
		public static bool downedOverloadedScandrone;

		// Token: 0x0400046F RID: 1135
		public static Condition DownedOverloadedScandrone = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOverloadedScandrone"), () => ModConditions.DownedBoss[295]);

		// Token: 0x04000470 RID: 1136
		public static bool downedPainbringer;

		// Token: 0x04000471 RID: 1137
		public static Condition DownedPainbringer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPainbringer"), () => ModConditions.DownedBoss[296]);

		// Token: 0x04000472 RID: 1138
		public static bool stramsClassesLoaded;

		// Token: 0x04000473 RID: 1139
		public static Mod stramsClassesMod;

		// Token: 0x04000474 RID: 1140
		public static bool supernovaLoaded;

		// Token: 0x04000475 RID: 1141
		public static Mod supernovaMod;

		// Token: 0x04000476 RID: 1142
		public static bool downedHarbingerOfAnnihilation;

		// Token: 0x04000477 RID: 1143
		public static Condition DownedHarbingerOfAnnihilation = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHarbingerOfAnnihilation"), () => ModConditions.DownedBoss[297]);

		// Token: 0x04000478 RID: 1144
		public static bool downedFlyingTerror;

		// Token: 0x04000479 RID: 1145
		public static Condition DownedFlyingTerror = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFlyingTerror"), () => ModConditions.DownedBoss[298]);

		// Token: 0x0400047A RID: 1146
		public static bool downedStoneMantaRay;

		// Token: 0x0400047B RID: 1147
		public static Condition DownedStoneMantaRay = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStoneMantaRay"), () => ModConditions.DownedBoss[299]);

		// Token: 0x0400047C RID: 1148
		public static bool downedBloodweaver;

		// Token: 0x0400047D RID: 1149
		public static Condition DownedBloodweaver = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBloodweaver"), () => ModConditions.DownedBoss[300]);

		// Token: 0x0400047E RID: 1150
		public static bool terrorbornLoaded;

		// Token: 0x0400047F RID: 1151
		public static Mod terrorbornMod;

		// Token: 0x04000480 RID: 1152
		public static bool downedInfectedIncarnate;

		// Token: 0x04000481 RID: 1153
		public static Condition DownedInfectedIncarnate = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInfectedIncarnate"), () => ModConditions.DownedBoss[301]);

		// Token: 0x04000482 RID: 1154
		public static bool downedTidalTitan;

		// Token: 0x04000483 RID: 1155
		public static Condition DownedTidalTitan = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTidalTitan"), () => ModConditions.DownedBoss[302]);

		// Token: 0x04000484 RID: 1156
		public static bool downedDunestock;

		// Token: 0x04000485 RID: 1157
		public static Condition DownedDunestock = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDunestock"), () => ModConditions.DownedBoss[303]);

		// Token: 0x04000486 RID: 1158
		public static bool downedHexedConstructor;

		// Token: 0x04000487 RID: 1159
		public static Condition DownedHexedConstructor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHexedConstructor"), () => ModConditions.DownedBoss[304]);

		// Token: 0x04000488 RID: 1160
		public static bool downedShadowcrawler;

		// Token: 0x04000489 RID: 1161
		public static Condition DownedShadowcrawler = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedShadowcrawler"), () => ModConditions.DownedBoss[305]);

		// Token: 0x0400048A RID: 1162
		public static bool downedPrototypeI;

		// Token: 0x0400048B RID: 1163
		public static Condition DownedPrototypeI = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPrototypeI"), () => ModConditions.DownedBoss[306]);

		// Token: 0x0400048C RID: 1164
		public static bool thoriumLoaded;

		// Token: 0x0400048D RID: 1165
		public static Mod thoriumMod;

		// Token: 0x0400048E RID: 1166
		public static bool downedGrandThunderBird;

		// Token: 0x0400048F RID: 1167
		public static Condition DownedGrandThunderBird = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGrandThunderBird"), () => ModConditions.downedGrandThunderBird);

		// Token: 0x04000490 RID: 1168
		public static bool downedQueenJellyfish;

		// Token: 0x04000491 RID: 1169
		public static Condition DownedQueenJellyfish = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedQueenJellyfish"), () => ModConditions.downedQueenJellyfish);

		// Token: 0x04000492 RID: 1170
		public static bool downedViscount;

		// Token: 0x04000493 RID: 1171
		public static Condition DownedViscount = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedViscount"), () => ModConditions.downedViscount);

		// Token: 0x04000494 RID: 1172
		public static bool downedGraniteEnergyStorm;

		// Token: 0x04000495 RID: 1173
		public static Condition DownedGraniteEnergyStorm = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGraniteEnergyStorm"), () => ModConditions.downedGraniteEnergyStorm);

		// Token: 0x04000496 RID: 1174
		public static bool downedBuriedChampion;

		// Token: 0x04000497 RID: 1175
		public static Condition DownedBuriedChampion = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBuriedChampion"), () => ModConditions.downedBuriedChampion);

		// Token: 0x04000498 RID: 1176
		public static bool downedStarScouter;

		// Token: 0x04000499 RID: 1177
		public static Condition DownedStarScouter = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStarScouter"), () => ModConditions.downedStarScouter);

		// Token: 0x0400049A RID: 1178
		public static bool downedBoreanStrider;

		// Token: 0x0400049B RID: 1179
		public static Condition DownedBoreanStrider = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBoreanStrider"), () => ModConditions.downedBoreanStrider);

		// Token: 0x0400049C RID: 1180
		public static bool downedFallenBeholder;

		// Token: 0x0400049D RID: 1181
		public static Condition DownedFallenBeholder = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFallenBeholder"), () => ModConditions.downedFallenBeholder);

		// Token: 0x0400049E RID: 1182
		public static bool downedLich;

		// Token: 0x0400049F RID: 1183
		public static Condition DownedLich = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLich"), () => ModConditions.downedLich);

		// Token: 0x040004A0 RID: 1184
		public static bool downedForgottenOne;

		// Token: 0x040004A1 RID: 1185
		public static Condition DownedForgottenOne = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedForgottenOne"), () => ModConditions.downedForgottenOne);

		// Token: 0x040004A2 RID: 1186
		public static bool downedPrimordials;

		// Token: 0x040004A3 RID: 1187
		public static Condition DownedPrimordials = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPrimordials"), () => ModConditions.downedPrimordials);

		// Token: 0x040004A4 RID: 1188
		public static bool downedPatchWerk;

		// Token: 0x040004A5 RID: 1189
		public static Condition DownedPatchWerk = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPatchWerk"), () => ModConditions.downedPatchWerk);

		// Token: 0x040004A6 RID: 1190
		public static bool downedCorpseBloom;

		// Token: 0x040004A7 RID: 1191
		public static Condition DownedCorpseBloom = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCorpseBloom"), () => ModConditions.downedCorpseBloom);

		// Token: 0x040004A8 RID: 1192
		public static bool downedIllusionist;

		// Token: 0x040004A9 RID: 1193
		public static Condition DownedIllusionist = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedIllusionist"), () => ModConditions.downedIllusionist);

		// Token: 0x040004AA RID: 1194
		public static bool beenToAquaticDepths;

		// Token: 0x040004AB RID: 1195
		public static Condition HasBeenToAquaticDepths = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAquaticDepths"), () => ModConditions.beenToAquaticDepths);

		// Token: 0x040004AC RID: 1196
		public static bool thoriumBossReworkLoaded;

		// Token: 0x040004AD RID: 1197
		public static Mod thoriumBossReworkMod;

		// Token: 0x040004AE RID: 1198
		public static bool exhaustionDisablerLoaded;

		// Token: 0x040004AF RID: 1199
		public static Mod exhaustionDisablerMod;

		// Token: 0x040004B0 RID: 1200
		public static bool traeLoaded;

		// Token: 0x040004B1 RID: 1201
		public static Mod traeMod;

		// Token: 0x040004B2 RID: 1202
		public static bool downedGraniteOvergrowth;

		// Token: 0x040004B3 RID: 1203
		public static Condition DownedGraniteOvergrowth = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGraniteOvergrowth"), () => ModConditions.DownedBoss[307]);

		// Token: 0x040004B4 RID: 1204
		public static bool downedBeholder;

		// Token: 0x040004B5 RID: 1205
		public static Condition DownedBeholder = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBeholder"), () => ModConditions.DownedBoss[308]);

		// Token: 0x040004B6 RID: 1206
		public static bool uhtricLoaded;

		// Token: 0x040004B7 RID: 1207
		public static Mod uhtricMod;

		// Token: 0x040004B8 RID: 1208
		public static bool downedDredger;

		// Token: 0x040004B9 RID: 1209
		public static Condition DownedDredger = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDredger"), () => ModConditions.DownedBoss[309]);

		// Token: 0x040004BA RID: 1210
		public static bool downedCharcoolSnowman;

		// Token: 0x040004BB RID: 1211
		public static Condition DownedCharcoolSnowman = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCharcoolSnowman"), () => ModConditions.DownedBoss[310]);

		// Token: 0x040004BC RID: 1212
		public static bool downedCosmicMenace;

		// Token: 0x040004BD RID: 1213
		public static Condition DownedCosmicMenace = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCosmicMenace"), () => ModConditions.DownedBoss[311]);

		// Token: 0x040004BE RID: 1214
		public static bool universeOfSwordsLoaded;

		// Token: 0x040004BF RID: 1215
		public static Mod universeOfSwordsMod;

		// Token: 0x040004C0 RID: 1216
		public static bool downedEvilFlyingBlade;

		// Token: 0x040004C1 RID: 1217
		public static Condition DownedEvilFlyingBlade = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEvilFlyingBlade"), () => ModConditions.DownedBoss[312]);

		// Token: 0x040004C2 RID: 1218
		public static bool valhallaLoaded;

		// Token: 0x040004C3 RID: 1219
		public static Mod valhallaMod;

		// Token: 0x040004C4 RID: 1220
		public static bool downedColossalCarnage;

		// Token: 0x040004C5 RID: 1221
		public static Condition DownedColossalCarnage = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedColossalCarnage"), () => ModConditions.DownedBoss[313]);

		// Token: 0x040004C6 RID: 1222
		public static bool downedYurnero;

		// Token: 0x040004C7 RID: 1223
		public static Condition DownedYurnero = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedYurnero"), () => ModConditions.DownedBoss[314]);

		// Token: 0x040004C8 RID: 1224
		public static bool verdantLoaded;

		// Token: 0x040004C9 RID: 1225
		public static Mod verdantMod;

		// Token: 0x040004CA RID: 1226
		public static bool beenToVerdant;

		// Token: 0x040004CB RID: 1227
		public static Condition HasBeenToVerdant = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToVerdant"), () => ModConditions.beenToVerdant);

		// Token: 0x040004CC RID: 1228
		public static bool vitalityLoaded;

		// Token: 0x040004CD RID: 1229
		public static Mod vitalityMod;

		// Token: 0x040004CE RID: 1230
		public static bool downedStormCloud;

		// Token: 0x040004CF RID: 1231
		public static Condition DownedStormCloud = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStormCloud"), () => ModConditions.DownedBoss[315]);

		// Token: 0x040004D0 RID: 1232
		public static bool downedGrandAntlion;

		// Token: 0x040004D1 RID: 1233
		public static Condition DownedGrandAntlion = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGrandAntlion"), () => ModConditions.DownedBoss[316]);

		// Token: 0x040004D2 RID: 1234
		public static bool downedGemstoneElemental;

		// Token: 0x040004D3 RID: 1235
		public static Condition DownedGemstoneElemental = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGemstoneElemental"), () => ModConditions.DownedBoss[317]);

		// Token: 0x040004D4 RID: 1236
		public static bool downedMoonlightDragonfly;

		// Token: 0x040004D5 RID: 1237
		public static Condition DownedMoonlightDragonfly = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMoonlightDragonfly"), () => ModConditions.DownedBoss[318]);

		// Token: 0x040004D6 RID: 1238
		public static bool downedDreadnaught;

		// Token: 0x040004D7 RID: 1239
		public static Condition DownedDreadnaught = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDreadnaught"), () => ModConditions.DownedBoss[319]);

		// Token: 0x040004D8 RID: 1240
		public static bool downedMosquitoMonarch;

		// Token: 0x040004D9 RID: 1241
		public static Condition DownedMosquitoMonarch = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMosquitoMonarch"), () => ModConditions.DownedBoss[320]);

		// Token: 0x040004DA RID: 1242
		public static bool downedAnarchulesBeetle;

		// Token: 0x040004DB RID: 1243
		public static Condition DownedAnarchulesBeetle = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAnarchulesBeetle"), () => ModConditions.DownedBoss[321]);

		// Token: 0x040004DC RID: 1244
		public static bool downedChaosbringer;

		// Token: 0x040004DD RID: 1245
		public static Condition DownedChaosbringer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedChaosbringer"), () => ModConditions.DownedBoss[322]);

		// Token: 0x040004DE RID: 1246
		public static bool downedPaladinSpirit;

		// Token: 0x040004DF RID: 1247
		public static Condition DownedPaladinSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPaladinSpirit"), () => ModConditions.DownedBoss[323]);

		// Token: 0x040004E0 RID: 1248
		public static bool wayfairContentLoaded;

		// Token: 0x040004E1 RID: 1249
		public static Mod wayfairContentMod;

		// Token: 0x040004E2 RID: 1250
		public static bool downedManaflora;

		// Token: 0x040004E3 RID: 1251
		public static Condition DownedManaflora = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedManaflora"), () => ModConditions.DownedBoss[324]);

		// Token: 0x040004E4 RID: 1252
		public static bool wrathOfTheGodsLoaded;

		// Token: 0x040004E5 RID: 1253
		public static Mod wrathOfTheGodsMod;

		// Token: 0x040004E6 RID: 1254
		public static bool downedNoxus;

		// Token: 0x040004E7 RID: 1255
		public static Condition DownedNoxus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNoxus"), () => ModConditions.DownedBoss[325]);

		// Token: 0x040004E8 RID: 1256
		public static bool downedNamelessDeityOfLight;

		// Token: 0x040004E9 RID: 1257
		public static Condition DownedNamelessDeityOfLight = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNamelessDeityOfLight"), () => ModConditions.DownedBoss[326]);

		// Token: 0x040004EA RID: 1258
		public static bool zylonLoaded;

		// Token: 0x040004EB RID: 1259
		public static Mod zylonMod;

		// Token: 0x040004EC RID: 1260
		public static bool downedDirtball;

		// Token: 0x040004ED RID: 1261
		public static Condition DownedDirtball = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDirtball"), () => ModConditions.DownedBoss[327]);

		// Token: 0x040004EE RID: 1262
		public static bool downedMetelord;

		// Token: 0x040004EF RID: 1263
		public static Condition DownedMetelord = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMetelord"), () => ModConditions.DownedBoss[328]);

		// Token: 0x040004F0 RID: 1264
		public static bool downedAdeneb;

		// Token: 0x040004F1 RID: 1265
		public static Condition DownedAdeneb = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAdeneb"), () => ModConditions.DownedBoss[329]);

		// Token: 0x040004F2 RID: 1266
		public static bool downedEldritchJellyfish;

		// Token: 0x040004F3 RID: 1267
		public static Condition DownedEldritchJellyfish = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEldritchJellyfish"), () => ModConditions.DownedBoss[330]);

		// Token: 0x040004F4 RID: 1268
		public static bool downedSaburRex;

		// Token: 0x040004F5 RID: 1269
		public static Condition DownedSaburRex = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSaburRex"), () => ModConditions.DownedBoss[331]);

		// Token: 0x040004F6 RID: 1270
		public static bool[] downedBoss = new bool[Enum.GetValues(typeof(ModConditions.Downed)).Length];

		// Token: 0x02000530 RID: 1328
		public enum Downed
		{
			// Token: 0x04000B9F RID: 2975
			Dreadnautilus,
			// Token: 0x04000BA0 RID: 2976
			MartianSaucer,
			// Token: 0x04000BA1 RID: 2977
			BloodMoon,
			// Token: 0x04000BA2 RID: 2978
			Eclipse,
			// Token: 0x04000BA3 RID: 2979
			LunarEvent,
			// Token: 0x04000BA4 RID: 2980
			SlayerOfEvil,
			// Token: 0x04000BA5 RID: 2981
			SATLA,
			// Token: 0x04000BA6 RID: 2982
			DrFetus,
			// Token: 0x04000BA7 RID: 2983
			SlimesHope,
			// Token: 0x04000BA8 RID: 2984
			PoliticianSlime,
			// Token: 0x04000BA9 RID: 2985
			AncientTrio,
			// Token: 0x04000BAA RID: 2986
			LavalGolem,
			// Token: 0x04000BAB RID: 2987
			Antony,
			// Token: 0x04000BAC RID: 2988
			BunnyZeppelin,
			// Token: 0x04000BAD RID: 2989
			Okiku,
			// Token: 0x04000BAE RID: 2990
			HarpyAirforce,
			// Token: 0x04000BAF RID: 2991
			Isaac,
			// Token: 0x04000BB0 RID: 2992
			AncientGuardian,
			// Token: 0x04000BB1 RID: 2993
			HeroicSlime,
			// Token: 0x04000BB2 RID: 2994
			HoloSlime,
			// Token: 0x04000BB3 RID: 2995
			SecurityBot,
			// Token: 0x04000BB4 RID: 2996
			UndeadChef,
			// Token: 0x04000BB5 RID: 2997
			GuardianOfFrost,
			// Token: 0x04000BB6 RID: 2998
			SoulHarvester,
			// Token: 0x04000BB7 RID: 2999
			TreeToad,
			// Token: 0x04000BB8 RID: 3000
			SeseKitsugai,
			// Token: 0x04000BB9 RID: 3001
			EyeOfTheStorm,
			// Token: 0x04000BBA RID: 3002
			Frigidius,
			// Token: 0x04000BBB RID: 3003
			CoreBoss,
			// Token: 0x04000BBC RID: 3004
			CragmawMire,
			// Token: 0x04000BBD RID: 3005
			NuclearTerror,
			// Token: 0x04000BBE RID: 3006
			Mauler,
			// Token: 0x04000BBF RID: 3007
			WulfrumExcavator,
			// Token: 0x04000BC0 RID: 3008
			Luminaris,
			// Token: 0x04000BC1 RID: 3009
			Prophet,
			// Token: 0x04000BC2 RID: 3010
			NihilityTwin,
			// Token: 0x04000BC3 RID: 3011
			Cruiser,
			// Token: 0x04000BC4 RID: 3012
			Astrageldon,
			// Token: 0x04000BC5 RID: 3013
			Clamitas,
			// Token: 0x04000BC6 RID: 3014
			Pyrogen,
			// Token: 0x04000BC7 RID: 3015
			WallOfBronze,
			// Token: 0x04000BC8 RID: 3016
			Lepus,
			// Token: 0x04000BC9 RID: 3017
			Turkor,
			// Token: 0x04000BCA RID: 3018
			Ocram,
			// Token: 0x04000BCB RID: 3019
			Rediancie,
			// Token: 0x04000BCC RID: 3020
			BabyIceDragon,
			// Token: 0x04000BCD RID: 3021
			SlimeEmperor,
			// Token: 0x04000BCE RID: 3022
			Bloodiancie,
			// Token: 0x04000BCF RID: 3023
			ThunderveinDragon,
			// Token: 0x04000BD0 RID: 3024
			NightmarePlantera,
			// Token: 0x04000BD1 RID: 3025
			Chasme,
			// Token: 0x04000BD2 RID: 3026
			LifeGuardian,
			// Token: 0x04000BD3 RID: 3027
			ManaGuardian,
			// Token: 0x04000BD4 RID: 3028
			MeteorExcavator,
			// Token: 0x04000BD5 RID: 3029
			MeteorAnnihilator,
			// Token: 0x04000BD6 RID: 3030
			HellfireSerpent,
			// Token: 0x04000BD7 RID: 3031
			WitheredAcornSpirit,
			// Token: 0x04000BD8 RID: 3032
			GoblinSorcererChieftain,
			// Token: 0x04000BD9 RID: 3033
			Galahis,
			// Token: 0x04000BDA RID: 3034
			Creation,
			// Token: 0x04000BDB RID: 3035
			Destruction,
			// Token: 0x04000BDC RID: 3036
			BlightKing,
			// Token: 0x04000BDD RID: 3037
			Gardener,
			// Token: 0x04000BDE RID: 3038
			Glaciation,
			// Token: 0x04000BDF RID: 3039
			HandOfCthulhu,
			// Token: 0x04000BE0 RID: 3040
			CursePreacher,
			// Token: 0x04000BE1 RID: 3041
			Effulgence,
			// Token: 0x04000BE2 RID: 3042
			IceLich,
			// Token: 0x04000BE3 RID: 3043
			Niflheim,
			// Token: 0x04000BE4 RID: 3044
			StellarStarship,
			// Token: 0x04000BE5 RID: 3045
			BacteriumPrime,
			// Token: 0x04000BE6 RID: 3046
			DesertBeak,
			// Token: 0x04000BE7 RID: 3047
			KingSting,
			// Token: 0x04000BE8 RID: 3048
			Mechasting,
			// Token: 0x04000BE9 RID: 3049
			Phantasm,
			// Token: 0x04000BEA RID: 3050
			TrojanSquirrel,
			// Token: 0x04000BEB RID: 3051
			CursedCoffin,
			// Token: 0x04000BEC RID: 3052
			Deviantt,
			// Token: 0x04000BED RID: 3053
			Lifelight,
			// Token: 0x04000BEE RID: 3054
			BanishedBaron,
			// Token: 0x04000BEF RID: 3055
			Eridanus,
			// Token: 0x04000BF0 RID: 3056
			Abominationn,
			// Token: 0x04000BF1 RID: 3057
			Mutant,
			// Token: 0x04000BF2 RID: 3058
			AlphaFrostjaw,
			// Token: 0x04000BF3 RID: 3059
			SanguineElemental,
			// Token: 0x04000BF4 RID: 3060
			Lad,
			// Token: 0x04000BF5 RID: 3061
			Hornlitz,
			// Token: 0x04000BF6 RID: 3062
			SnowDon,
			// Token: 0x04000BF7 RID: 3063
			Stoffie,
			// Token: 0x04000BF8 RID: 3064
			LilyWhite,
			// Token: 0x04000BF9 RID: 3065
			Rumia,
			// Token: 0x04000BFA RID: 3066
			EternityLarva,
			// Token: 0x04000BFB RID: 3067
			Nazrin,
			// Token: 0x04000BFC RID: 3068
			HinaKagiyama,
			// Token: 0x04000BFD RID: 3069
			Sekibanki,
			// Token: 0x04000BFE RID: 3070
			Seiran,
			// Token: 0x04000BFF RID: 3071
			NitoriKawashiro,
			// Token: 0x04000C00 RID: 3072
			MedicineMelancholy,
			// Token: 0x04000C01 RID: 3073
			Cirno,
			// Token: 0x04000C02 RID: 3074
			MinamitsuMurasa,
			// Token: 0x04000C03 RID: 3075
			AliceMargatroid,
			// Token: 0x04000C04 RID: 3076
			SakuyaIzayoi,
			// Token: 0x04000C05 RID: 3077
			SeijaKijin,
			// Token: 0x04000C06 RID: 3078
			MayumiJoutouguu,
			// Token: 0x04000C07 RID: 3079
			ToyosatomimiNoMiko,
			// Token: 0x04000C08 RID: 3080
			KaguyaHouraisan,
			// Token: 0x04000C09 RID: 3081
			UtsuhoReiuji,
			// Token: 0x04000C0A RID: 3082
			TenshiHinanawi,
			// Token: 0x04000C0B RID: 3083
			Kisume,
			// Token: 0x04000C0C RID: 3084
			Trerios,
			// Token: 0x04000C0D RID: 3085
			MagmaEye,
			// Token: 0x04000C0E RID: 3086
			Jack,
			// Token: 0x04000C0F RID: 3087
			Acheron,
			// Token: 0x04000C10 RID: 3088
			MarquisMoonsquid,
			// Token: 0x04000C11 RID: 3089
			PriestessRod,
			// Token: 0x04000C12 RID: 3090
			Diver,
			// Token: 0x04000C13 RID: 3091
			Motherbrain,
			// Token: 0x04000C14 RID: 3092
			WallOfShadow,
			// Token: 0x04000C15 RID: 3093
			SunSlimeGod,
			// Token: 0x04000C16 RID: 3094
			Overwatcher,
			// Token: 0x04000C17 RID: 3095
			Lifebringer,
			// Token: 0x04000C18 RID: 3096
			Materealizer,
			// Token: 0x04000C19 RID: 3097
			ScarabBelief,
			// Token: 0x04000C1A RID: 3098
			WorldsEndWhale,
			// Token: 0x04000C1B RID: 3099
			Son,
			// Token: 0x04000C1C RID: 3100
			CaveOrdeal,
			// Token: 0x04000C1D RID: 3101
			CorruptOrdeal,
			// Token: 0x04000C1E RID: 3102
			CrimsonOrdeal,
			// Token: 0x04000C1F RID: 3103
			DesertOrdeal,
			// Token: 0x04000C20 RID: 3104
			ForestOrdeal,
			// Token: 0x04000C21 RID: 3105
			HallowOrdeal,
			// Token: 0x04000C22 RID: 3106
			JungleOrdeal,
			// Token: 0x04000C23 RID: 3107
			SkyOrdeal,
			// Token: 0x04000C24 RID: 3108
			SnowOrdeal,
			// Token: 0x04000C25 RID: 3109
			UnderworldOrdeal,
			// Token: 0x04000C26 RID: 3110
			Goozma,
			// Token: 0x04000C27 RID: 3111
			BereftVassal,
			// Token: 0x04000C28 RID: 3112
			StoneGuardian,
			// Token: 0x04000C29 RID: 3113
			CommanderGintzia,
			// Token: 0x04000C2A RID: 3114
			SunStalker,
			// Token: 0x04000C2B RID: 3115
			PumpkinJack,
			// Token: 0x04000C2C RID: 3116
			ForgottenPuppetDaedus,
			// Token: 0x04000C2D RID: 3117
			DreadMire,
			// Token: 0x04000C2E RID: 3118
			SingularityFragment,
			// Token: 0x04000C2F RID: 3119
			Verlia,
			// Token: 0x04000C30 RID: 3120
			Irradia,
			// Token: 0x04000C31 RID: 3121
			Sylia,
			// Token: 0x04000C32 RID: 3122
			Fenix,
			// Token: 0x04000C33 RID: 3123
			BlazingSerpent,
			// Token: 0x04000C34 RID: 3124
			Cogwork,
			// Token: 0x04000C35 RID: 3125
			WaterCogwork,
			// Token: 0x04000C36 RID: 3126
			WaterJellyfish,
			// Token: 0x04000C37 RID: 3127
			Sparn,
			// Token: 0x04000C38 RID: 3128
			PandorasFlamebox,
			// Token: 0x04000C39 RID: 3129
			STARBOMBER,
			// Token: 0x04000C3A RID: 3130
			GintzeArmy,
			// Token: 0x04000C3B RID: 3131
			Britzz,
			// Token: 0x04000C3C RID: 3132
			TheAlchemist,
			// Token: 0x04000C3D RID: 3133
			CarnagePillar,
			// Token: 0x04000C3E RID: 3134
			VoidDigger,
			// Token: 0x04000C3F RID: 3135
			PrinceSlime,
			// Token: 0x04000C40 RID: 3136
			Triplets,
			// Token: 0x04000C41 RID: 3137
			JungleDefenders,
			// Token: 0x04000C42 RID: 3138
			St4sys,
			// Token: 0x04000C43 RID: 3139
			Terminator,
			// Token: 0x04000C44 RID: 3140
			Caretaker,
			// Token: 0x04000C45 RID: 3141
			SiegeEngine,
			// Token: 0x04000C46 RID: 3142
			SuperVMS,
			// Token: 0x04000C47 RID: 3143
			Torizo,
			// Token: 0x04000C48 RID: 3144
			Serris,
			// Token: 0x04000C49 RID: 3145
			Kraid,
			// Token: 0x04000C4A RID: 3146
			Phantoon,
			// Token: 0x04000C4B RID: 3147
			OmegaPirate,
			// Token: 0x04000C4C RID: 3148
			Nightmare,
			// Token: 0x04000C4D RID: 3149
			GoldenTorizo,
			// Token: 0x04000C4E RID: 3150
			Ophiopede,
			// Token: 0x04000C4F RID: 3151
			Ophiocoon,
			// Token: 0x04000C50 RID: 3152
			Ophiofly,
			// Token: 0x04000C51 RID: 3153
			StormCloudfish,
			// Token: 0x04000C52 RID: 3154
			StarConstruct,
			// Token: 0x04000C53 RID: 3155
			Gigabat,
			// Token: 0x04000C54 RID: 3156
			SunPixie,
			// Token: 0x04000C55 RID: 3157
			Esophage,
			// Token: 0x04000C56 RID: 3158
			ConvectiveWanderer,
			// Token: 0x04000C57 RID: 3159
			ForestGuardian,
			// Token: 0x04000C58 RID: 3160
			CryoGuardian,
			// Token: 0x04000C59 RID: 3161
			PrimordialWorm,
			// Token: 0x04000C5A RID: 3162
			TheGuardianOfHell,
			// Token: 0x04000C5B RID: 3163
			Void,
			// Token: 0x04000C5C RID: 3164
			Armagem,
			// Token: 0x04000C5D RID: 3165
			PolarExterminator,
			// Token: 0x04000C5E RID: 3166
			DivineLight,
			// Token: 0x04000C5F RID: 3167
			AncientMachine,
			// Token: 0x04000C60 RID: 3168
			Noehtnap,
			// Token: 0x04000C61 RID: 3169
			Hydra,
			// Token: 0x04000C62 RID: 3170
			Imperious,
			// Token: 0x04000C63 RID: 3171
			RuneGhost,
			// Token: 0x04000C64 RID: 3172
			InvaderBattleship,
			// Token: 0x04000C65 RID: 3173
			InvaderNoehtnap,
			// Token: 0x04000C66 RID: 3174
			OLORD,
			// Token: 0x04000C67 RID: 3175
			GreatTyrannosaurus,
			// Token: 0x04000C68 RID: 3176
			DinoMilitia,
			// Token: 0x04000C69 RID: 3177
			Invaders,
			// Token: 0x04000C6A RID: 3178
			Thorn,
			// Token: 0x04000C6B RID: 3179
			Erhan,
			// Token: 0x04000C6C RID: 3180
			Keeper,
			// Token: 0x04000C6D RID: 3181
			SeedOfInfection,
			// Token: 0x04000C6E RID: 3182
			KingSlayerIII,
			// Token: 0x04000C6F RID: 3183
			OmegaCleaver,
			// Token: 0x04000C70 RID: 3184
			OmegaGigapora,
			// Token: 0x04000C71 RID: 3185
			OmegaObliterator,
			// Token: 0x04000C72 RID: 3186
			PatientZero,
			// Token: 0x04000C73 RID: 3187
			Akka,
			// Token: 0x04000C74 RID: 3188
			Ukko,
			// Token: 0x04000C75 RID: 3189
			AncientDeityDuo,
			// Token: 0x04000C76 RID: 3190
			Nebuleus,
			// Token: 0x04000C77 RID: 3191
			FowlEmperor,
			// Token: 0x04000C78 RID: 3192
			Cockatrice,
			// Token: 0x04000C79 RID: 3193
			Basan,
			// Token: 0x04000C7A RID: 3194
			SkullDigger,
			// Token: 0x04000C7B RID: 3195
			EaglecrestGolem,
			// Token: 0x04000C7C RID: 3196
			Calavia,
			// Token: 0x04000C7D RID: 3197
			TheJanitor,
			// Token: 0x04000C7E RID: 3198
			IrradiatedBehemoth,
			// Token: 0x04000C7F RID: 3199
			Blisterface,
			// Token: 0x04000C80 RID: 3200
			ProtectorVolt,
			// Token: 0x04000C81 RID: 3201
			MACEProject,
			// Token: 0x04000C82 RID: 3202
			FowlMorning,
			// Token: 0x04000C83 RID: 3203
			Raveyard,
			// Token: 0x04000C84 RID: 3204
			PutridPinky,
			// Token: 0x04000C85 RID: 3205
			Glowmoth,
			// Token: 0x04000C86 RID: 3206
			PharaohsCurse,
			// Token: 0x04000C87 RID: 3207
			Advisor,
			// Token: 0x04000C88 RID: 3208
			Polaris,
			// Token: 0x04000C89 RID: 3209
			Lux,
			// Token: 0x04000C8A RID: 3210
			SubspaceSerpent,
			// Token: 0x04000C8B RID: 3211
			NatureConstruct,
			// Token: 0x04000C8C RID: 3212
			EarthenConstruct,
			// Token: 0x04000C8D RID: 3213
			PermafrostConstruct,
			// Token: 0x04000C8E RID: 3214
			TidalConstruct,
			// Token: 0x04000C8F RID: 3215
			OtherworldlyConstruct,
			// Token: 0x04000C90 RID: 3216
			EvilConstruct,
			// Token: 0x04000C91 RID: 3217
			InfernoConstruct,
			// Token: 0x04000C92 RID: 3218
			ChaosConstruct,
			// Token: 0x04000C93 RID: 3219
			NatureSpirit,
			// Token: 0x04000C94 RID: 3220
			EarthenSpirit,
			// Token: 0x04000C95 RID: 3221
			PermafrostSpirit,
			// Token: 0x04000C96 RID: 3222
			TidalSpirit,
			// Token: 0x04000C97 RID: 3223
			OtherworldlySpirit,
			// Token: 0x04000C98 RID: 3224
			EvilSpirit,
			// Token: 0x04000C99 RID: 3225
			InfernoSpirit,
			// Token: 0x04000C9A RID: 3226
			ChaosSpirit,
			// Token: 0x04000C9B RID: 3227
			Decree,
			// Token: 0x04000C9C RID: 3228
			FlamingPumpkin,
			// Token: 0x04000C9D RID: 3229
			ZombiePiglinBrute,
			// Token: 0x04000C9E RID: 3230
			JensenTheGrandHarpy,
			// Token: 0x04000C9F RID: 3231
			Araneas,
			// Token: 0x04000CA0 RID: 3232
			HarpyQueenRaynare,
			// Token: 0x04000CA1 RID: 3233
			Primordia,
			// Token: 0x04000CA2 RID: 3234
			Abaddon,
			// Token: 0x04000CA3 RID: 3235
			Araghur,
			// Token: 0x04000CA4 RID: 3236
			LostSiblings,
			// Token: 0x04000CA5 RID: 3237
			Erazor,
			// Token: 0x04000CA6 RID: 3238
			Nihilus,
			// Token: 0x04000CA7 RID: 3239
			Exodygen,
			// Token: 0x04000CA8 RID: 3240
			Scarabeus,
			// Token: 0x04000CA9 RID: 3241
			MoonJellyWizard,
			// Token: 0x04000CAA RID: 3242
			VinewrathBane,
			// Token: 0x04000CAB RID: 3243
			AncientAvian,
			// Token: 0x04000CAC RID: 3244
			StarplateVoyager,
			// Token: 0x04000CAD RID: 3245
			Infernon,
			// Token: 0x04000CAE RID: 3246
			Dusking,
			// Token: 0x04000CAF RID: 3247
			Atlas,
			// Token: 0x04000CB0 RID: 3248
			JellyDeluge,
			// Token: 0x04000CB1 RID: 3249
			Tide,
			// Token: 0x04000CB2 RID: 3250
			MysticMoon,
			// Token: 0x04000CB3 RID: 3251
			SpookySpirit,
			// Token: 0x04000CB4 RID: 3252
			RotGourd,
			// Token: 0x04000CB5 RID: 3253
			Moco,
			// Token: 0x04000CB6 RID: 3254
			Daffodil,
			// Token: 0x04000CB7 RID: 3255
			OrroBoro,
			// Token: 0x04000CB8 RID: 3256
			BigBone,
			// Token: 0x04000CB9 RID: 3257
			Auroracle,
			// Token: 0x04000CBA RID: 3258
			Ceiros,
			// Token: 0x04000CBB RID: 3259
			Glassweaver,
			// Token: 0x04000CBC RID: 3260
			VagrantofSpace,
			// Token: 0x04000CBD RID: 3261
			Thespian,
			// Token: 0x04000CBE RID: 3262
			Dioskouroi,
			// Token: 0x04000CBF RID: 3263
			Nalhaun,
			// Token: 0x04000CC0 RID: 3264
			Starfarers,
			// Token: 0x04000CC1 RID: 3265
			Penthesilea,
			// Token: 0x04000CC2 RID: 3266
			Arbitration,
			// Token: 0x04000CC3 RID: 3267
			WarriorOfLight,
			// Token: 0x04000CC4 RID: 3268
			Tsukiyomi,
			// Token: 0x04000CC5 RID: 3269
			AncientHusk,
			// Token: 0x04000CC6 RID: 3270
			OverloadedScandrone,
			// Token: 0x04000CC7 RID: 3271
			Painbringer,
			// Token: 0x04000CC8 RID: 3272
			HarbingerOfAnnihilation,
			// Token: 0x04000CC9 RID: 3273
			FlyingTerror,
			// Token: 0x04000CCA RID: 3274
			StoneMantaRay,
			// Token: 0x04000CCB RID: 3275
			Bloodweaver,
			// Token: 0x04000CCC RID: 3276
			InfectedIncarnate,
			// Token: 0x04000CCD RID: 3277
			TidalTitan,
			// Token: 0x04000CCE RID: 3278
			Dunestock,
			// Token: 0x04000CCF RID: 3279
			HexedConstructor,
			// Token: 0x04000CD0 RID: 3280
			Shadowcrawler,
			// Token: 0x04000CD1 RID: 3281
			PrototypeI,
			// Token: 0x04000CD2 RID: 3282
			GraniteOvergrowth,
			// Token: 0x04000CD3 RID: 3283
			Beholder,
			// Token: 0x04000CD4 RID: 3284
			Dredger,
			// Token: 0x04000CD5 RID: 3285
			CharcoolSnowman,
			// Token: 0x04000CD6 RID: 3286
			CosmicMenace,
			// Token: 0x04000CD7 RID: 3287
			EvilFlyingBlade,
			// Token: 0x04000CD8 RID: 3288
			ColossalCarnage,
			// Token: 0x04000CD9 RID: 3289
			Yurnero,
			// Token: 0x04000CDA RID: 3290
			StormCloud,
			// Token: 0x04000CDB RID: 3291
			GrandAntlion,
			// Token: 0x04000CDC RID: 3292
			GemstoneElemental,
			// Token: 0x04000CDD RID: 3293
			MoonlightDragonfly,
			// Token: 0x04000CDE RID: 3294
			Dreadnaught,
			// Token: 0x04000CDF RID: 3295
			MosquitoMonarch,
			// Token: 0x04000CE0 RID: 3296
			AnarchulesBeetle,
			// Token: 0x04000CE1 RID: 3297
			Chaosbringer,
			// Token: 0x04000CE2 RID: 3298
			PaladinSpirit,
			// Token: 0x04000CE3 RID: 3299
			Manaflora,
			// Token: 0x04000CE4 RID: 3300
			Noxus,
			// Token: 0x04000CE5 RID: 3301
			NamelessDeityOfLight,
			// Token: 0x04000CE6 RID: 3302
			Dirtball,
			// Token: 0x04000CE7 RID: 3303
			Metelord,
			// Token: 0x04000CE8 RID: 3304
			Adeneb,
			// Token: 0x04000CE9 RID: 3305
			EldritchJellyfish,
			// Token: 0x04000CEA RID: 3306
			SaburRex
		}
	}
}
