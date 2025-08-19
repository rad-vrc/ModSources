using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour;
using QoLCompendium.Content.Items.Accessories.Construction;
using QoLCompendium.Content.Items.Accessories.Fishing;
using QoLCompendium.Content.Items.Accessories.InformationAccessories;
using QoLCompendium.Content.Items.Tools.Mirrors;
using QoLCompendium.Content.Items.Tools.Summons;
using QoLCompendium.Content.Items.Tools.Summons.CrossMod;
using QoLCompendium.Content.Projectiles.MobileStorages;
using QoLCompendium.Content.Tiles.Other;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace QoLCompendium.Core
{
	// Token: 0x020001FF RID: 511
	public static class Common
	{
		// Token: 0x06000B6B RID: 2923 RVA: 0x00049F48 File Offset: 0x00048148
		public static void UnloadTasks()
		{
			foreach (Hook hook in Common.detours)
			{
				hook.Undo();
			}
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00049F98 File Offset: 0x00048198
		public static void PostSetupTasks()
		{
			HashSet<int> hashSet = new HashSet<int>();
			hashSet.Add(Common.GetModItem(ModConditions.orchidMod, "Chip"));
			hashSet.Add(Common.GetModItem(ModConditions.orchidMod, "Guard"));
			hashSet.Add(Common.GetModItem(ModConditions.orchidMod, "Potency"));
			hashSet.Add(Common.GetModItem(ModConditions.thoriumMod, "InspirationNote"));
			hashSet.Add(Common.GetModItem(ModConditions.thoriumMod, "InspirationNoteStatue"));
			hashSet.Add(Common.GetModItem(ModConditions.thoriumMod, "InspirationNoteNoble"));
			hashSet.Add(Common.GetModItem(ModConditions.thoriumMod, "InspirationNoteRhapsodist"));
			hashSet.Add(Common.GetModItem(ModConditions.thoriumMod, "MeatSlab"));
			hashSet.Add(Common.GetModItem(ModConditions.thoriumMod, "GreatFlesh"));
			hashSet.Add(Common.GetModItem(ModConditions.vitalityMod, "BloodClot"));
			HashSet<int> ModPowerUpItems = hashSet;
			Common.PowerUpItems.UnionWith(ModPowerUpItems);
			HashSet<int> hashSet2 = new HashSet<int>();
			hashSet2.Add(ModContent.ItemType<BattalionLog>());
			hashSet2.Add(ModContent.ItemType<HarmInducer>());
			hashSet2.Add(ModContent.ItemType<HeadCounter>());
			hashSet2.Add(ModContent.ItemType<Kettlebell>());
			hashSet2.Add(ModContent.ItemType<LuckyDie>());
			hashSet2.Add(ModContent.ItemType<MetallicClover>());
			hashSet2.Add(ModContent.ItemType<PlateCracker>());
			hashSet2.Add(ModContent.ItemType<Regenerator>());
			hashSet2.Add(ModContent.ItemType<ReinforcedPanel>());
			hashSet2.Add(ModContent.ItemType<Replenisher>());
			hashSet2.Add(ModContent.ItemType<TrackingDevice>());
			hashSet2.Add(ModContent.ItemType<WingTimer>());
			hashSet2.Add(ModContent.ItemType<Fitbit>());
			hashSet2.Add(ModContent.ItemType<HeartbeatSensor>());
			hashSet2.Add(ModContent.ItemType<ToleranceDetector>());
			hashSet2.Add(ModContent.ItemType<VitalDisplay>());
			hashSet2.Add(ModContent.ItemType<IAH>());
			hashSet2.Add(ModContent.ItemType<MosaicMirror>());
			hashSet2.Add(ModContent.ItemType<SkullWatch>());
			hashSet2.Add(ModContent.ItemType<DeteriorationDisplay>());
			hashSet2.Add(ModContent.ItemType<MiningEmblem>());
			hashSet2.Add(ModContent.ItemType<ConstructionEmblem>());
			hashSet2.Add(ModContent.ItemType<CreationClubMembersPass>());
			hashSet2.Add(ModContent.ItemType<SonarDevice>());
			hashSet2.Add(ModContent.ItemType<AnglerRadar>());
			hashSet2.Add(ModContent.ItemType<AnglersDream>());
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "AnglerBroadcaster"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "Calendar"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "GeigerCounter"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "HoloLens"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "RichMansMonocle"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "DevilsTongue"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "NeonGenesis"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "RadonFishingBobber"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "Ramishroom"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "RegrowingBait"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "LavaproofMitten"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "BusinessCard"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "HaltingMachine"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "HaltingMagnet"));
			hashSet2.Add(Common.GetModItem(ModConditions.aequusMod, "HyperJet"));
			hashSet2.Add(Common.GetModItem(ModConditions.afkpetsMod, "FishermansPride"));
			hashSet2.Add(Common.GetModItem(ModConditions.afkpetsMod, "LampyridaeHairpin"));
			hashSet2.Add(Common.GetModItem(ModConditions.afkpetsMod, "Piracy"));
			hashSet2.Add(Common.GetModItem(ModConditions.afkpetsMod, "PortableSonar"));
			hashSet2.Add(Common.GetModItem(ModConditions.afkpetsMod, "TheHandyman"));
			hashSet2.Add(Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "AttendanceLog"));
			hashSet2.Add(Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "BiomeCrystal"));
			hashSet2.Add(Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "EngiRegistry"));
			hashSet2.Add(Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "FortuneMirror"));
			hashSet2.Add(Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "HitMarker"));
			hashSet2.Add(Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "Magimeter"));
			hashSet2.Add(Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "RSH"));
			hashSet2.Add(Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "SafteyScanner"));
			hashSet2.Add(Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "ScryingMirror"));
			hashSet2.Add(Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "ThreatAnalyzer"));
			hashSet2.Add(Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "WantedPoster"));
			hashSet2.Add(Common.GetModItem(ModConditions.calamityMod, "AlluringBait"));
			hashSet2.Add(Common.GetModItem(ModConditions.calamityMod, "EnchantedPearl"));
			hashSet2.Add(Common.GetModItem(ModConditions.calamityMod, "SupremeBaitTackleBoxFishingStation"));
			hashSet2.Add(Common.GetModItem(ModConditions.calamityMod, "AncientFossil"));
			hashSet2.Add(Common.GetModItem(ModConditions.calamityMod, "OceanCrest"));
			hashSet2.Add(Common.GetModItem(ModConditions.calamityMod, "SpelunkersAmulet"));
			hashSet2.Add(Common.GetModItem(ModConditions.clickerClassMod, "ButtonMasher"));
			hashSet2.Add(Common.GetModItem(ModConditions.depthsMod, "LodeStone"));
			hashSet2.Add(Common.GetModItem(ModConditions.depthsMod, "MercuryMossFishingBobber"));
			hashSet2.Add(Common.GetModItem(ModConditions.depthsMod, "QuicksilverproofFishingHook"));
			hashSet2.Add(Common.GetModItem(ModConditions.depthsMod, "QuicksilverproofTackleBag"));
			hashSet2.Add(Common.GetModItem(ModConditions.luiAFKMod, "FasterMining"));
			hashSet2.Add(Common.GetModItem(ModConditions.luiAFKMod, "SuperToolTime"));
			hashSet2.Add(Common.GetModItem(ModConditions.luiAFKMod, "ToolTime"));
			hashSet2.Add(Common.GetModItem(ModConditions.luiAFKDLCMod, "ArchitectHeavyEquipment"));
			hashSet2.Add(Common.GetModItem(ModConditions.luiAFKDLCMod, "EnchantedSupremeFishingBundle"));
			hashSet2.Add(Common.GetModItem(ModConditions.martainsOrderMod, "ArmorDisplayer"));
			hashSet2.Add(Common.GetModItem(ModConditions.martainsOrderMod, "FlightTimer"));
			hashSet2.Add(Common.GetModItem(ModConditions.martainsOrderMod, "Journal"));
			hashSet2.Add(Common.GetModItem(ModConditions.martainsOrderMod, "IronWatch"));
			hashSet2.Add(Common.GetModItem(ModConditions.martainsOrderMod, "LeadWatch"));
			hashSet2.Add(Common.GetModItem(ModConditions.martainsOrderMod, "LeprechaunSensor"));
			hashSet2.Add(Common.GetModItem(ModConditions.martainsOrderMod, "MinionCounter"));
			hashSet2.Add(Common.GetModItem(ModConditions.martainsOrderMod, "SentryCounter"));
			hashSet2.Add(Common.GetModItem(ModConditions.martainsOrderMod, "SummonersTracker"));
			hashSet2.Add(Common.GetModItem(ModConditions.martainsOrderMod, "SurvivalTracker"));
			hashSet2.Add(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "AnomalyLocator"));
			hashSet2.Add(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ArchaeologistToolbelt"));
			hashSet2.Add(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ElectromagneticDeterrent"));
			hashSet2.Add(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "GoldenTrowel"));
			hashSet2.Add(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "InfiniteVoid"));
			hashSet2.Add(Common.GetModItem(ModConditions.spiritMod, "FisheyeGem"));
			hashSet2.Add(Common.GetModItem(ModConditions.spiritMod, "MetalBand"));
			hashSet2.Add(Common.GetModItem(ModConditions.spiritMod, "MimicRepellent"));
			hashSet2.Add(Common.GetModItem(ModConditions.thoriumMod, "HeartRateMonitor"));
			hashSet2.Add(Common.GetModItem(ModConditions.thoriumMod, "HightechSonarDevice"));
			hashSet2.Add(Common.GetModItem(ModConditions.thoriumMod, "GlitteringChalice"));
			hashSet2.Add(Common.GetModItem(ModConditions.thoriumMod, "GreedyGoblet"));
			hashSet2.Add(Common.GetModItem(ModConditions.thoriumMod, "LuckyRabbitsFoot"));
			HashSet<int> ModBankItems = hashSet2;
			Common.BankItems.UnionWith(ModBankItems);
			HashSet<int> hashSet3 = new HashSet<int>();
			hashSet3.Add(ModContent.ItemType<CultistSummon>());
			hashSet3.Add(ModContent.ItemType<DukeFishronSummon>());
			hashSet3.Add(ModContent.ItemType<EmpressOfLightSummon>());
			hashSet3.Add(ModContent.ItemType<PlanteraSummon>());
			hashSet3.Add(ModContent.ItemType<SkeletronSummon>());
			hashSet3.Add(ModContent.ItemType<WallOfFleshSummon>());
			hashSet3.Add(ModContent.ItemType<ForgottenOneSummon>());
			hashSet3.Add(ModContent.ItemType<GiantClamSummon>());
			hashSet3.Add(ModContent.ItemType<LeviathanAnahitaSummon>());
			hashSet3.Add(ModContent.ItemType<OldDukeSummon>());
			hashSet3.Add(Common.GetModItem(ModConditions.aequusMod, "GalacticStarfruit"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "AncientSand"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "BlackenedHeart"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "BrokenDelftPlate"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "CookingBook"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "CorruptedServer"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "DemonicAnalysis"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "DesertMirror"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "DuckWhistle"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "FallingSlimeReplica"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "FrozenSkull"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "GoldenKingSlimeIdol"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "GoldenSkull"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "HaniwaIdol"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "HolographicSlimeReplica"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "IceBossCrystal"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "MagicWand"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "NightmareFuel"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "PinkDiamond"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "PlantAshContainer"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "PreyTrackingChip"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "RoastChickenPlate"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "SeveredClothierHead"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "SeveredDryadHead"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "SeveredHarpyHead"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "ShogunSlimesHelmet"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "SlimeinaGlassCube"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "SlimyWarBanner"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "SoulofAgonyinaBottle"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "SpineWormFood"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "SpiritofFunPot"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "SpiritualHeart"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "StoryBook"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "SuspiciousLookingChest"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "SwissChocolate"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "TiedBunny"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "TinyMeatIdol"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "TradeDeal"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "UnstableRainbowCookie"));
			hashSet3.Add(Common.GetModItem(ModConditions.afkpetsMod, "UntoldBurial"));
			hashSet3.Add(Common.GetModItem(ModConditions.awfulGarbageMod, "InsectOnAStick"));
			hashSet3.Add(Common.GetModItem(ModConditions.awfulGarbageMod, "PileOfFakeBones"));
			hashSet3.Add(Common.GetModItem(ModConditions.blocksCoreBossMod, "ChargedOrb"));
			hashSet3.Add(Common.GetModItem(ModConditions.blocksCoreBossMod, "ChargedOrbCrim"));
			hashSet3.Add(Common.GetModItem(ModConditions.consolariaMod, "SuspiciousLookingEgg"));
			hashSet3.Add(Common.GetModItem(ModConditions.consolariaMod, "CursedStuffing"));
			hashSet3.Add(Common.GetModItem(ModConditions.consolariaMod, "SuspiciousLookingSkull"));
			hashSet3.Add(Common.GetModItem(ModConditions.consolariaMod, "Wishbone"));
			hashSet3.Add(Common.GetModItem(ModConditions.coraliteMod, "RedBerry"));
			hashSet3.Add(Common.GetModItem(ModConditions.edorbisMod, "BiomechanicalMatter"));
			hashSet3.Add(Common.GetModItem(ModConditions.edorbisMod, "CursedSoul"));
			hashSet3.Add(Common.GetModItem(ModConditions.edorbisMod, "KelviniteRadar"));
			hashSet3.Add(Common.GetModItem(ModConditions.edorbisMod, "SlayerTrophy"));
			hashSet3.Add(Common.GetModItem(ModConditions.edorbisMod, "ThePrettiestFlower"));
			hashSet3.Add(Common.GetModItem(ModConditions.enchantedMoonsMod, "BlueMedallion"));
			hashSet3.Add(Common.GetModItem(ModConditions.enchantedMoonsMod, "CherryAmulet"));
			hashSet3.Add(Common.GetModItem(ModConditions.enchantedMoonsMod, "HarvestLantern"));
			hashSet3.Add(Common.GetModItem(ModConditions.enchantedMoonsMod, "MintRing"));
			hashSet3.Add(Common.GetModItem(ModConditions.everjadeMod, "FestivalLantern"));
			hashSet3.Add(Common.GetModItem(ModConditions.excelsiorMod, "ReflectiveIceShard"));
			hashSet3.Add(Common.GetModItem(ModConditions.excelsiorMod, "PlanetaryTrackingDevice"));
			hashSet3.Add(Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "BloodyAmulet"));
			hashSet3.Add(Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "InfestedCarcass"));
			hashSet3.Add(Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "DesertHorn"));
			hashSet3.Add(Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "GoblinRetreatOrder"));
			hashSet3.Add(Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "FalseTreasureMap"));
			hashSet3.Add(Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "OddFertilizer"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "AliceMargatroidSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "CirnoSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "EternityLarvaSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "HinaKagiyamaSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "KaguyaHouraisanSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "LilyWhiteSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "MayumiJoutouguuSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "MedicineMelancholySpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "MinamitsuMurasaSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "NazrinSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "NitoriKawashiroSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "RumiaSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "SakuyaIzayoiSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "SeijaKijinSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "SeiranSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "SekibankiSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "TenshiHinanawiSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "ToyosatomimiNoMikoSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.gensokyoMod, "UtsuhoReiujiSpawner"));
			hashSet3.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "CannedSoulofFlight"));
			hashSet3.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "MetalSpine"));
			hashSet3.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "SouthernPotting"));
			hashSet3.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "SunlightCrown"));
			hashSet3.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "UltimateTorch"));
			hashSet3.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "UnstableGlobe"));
			hashSet3.Add(Common.GetModItem(ModConditions.martainsOrderMod, "FrigidEgg"));
			hashSet3.Add(Common.GetModItem(ModConditions.martainsOrderMod, "SuspiciousLookingCloud"));
			hashSet3.Add(Common.GetModItem(ModConditions.martainsOrderMod, "Catnip"));
			hashSet3.Add(Common.GetModItem(ModConditions.martainsOrderMod, "CarnageSuspiciousRazor"));
			hashSet3.Add(Common.GetModItem(ModConditions.martainsOrderMod, "VoidWorm"));
			hashSet3.Add(Common.GetModItem(ModConditions.martainsOrderMod, "LuminiteSlimeCrown"));
			hashSet3.Add(Common.GetModItem(ModConditions.martainsOrderMod, "LuminiteEye"));
			hashSet3.Add(Common.GetModItem(ModConditions.martainsOrderMod, "JunglesLastTreasure"));
			hashSet3.Add(Common.GetModItem(ModConditions.martainsOrderMod, "TeslaRemote"));
			hashSet3.Add(Common.GetModItem(ModConditions.martainsOrderMod, "BloodyNight"));
			hashSet3.Add(Common.GetModItem(ModConditions.martainsOrderMod, "LucidDay"));
			hashSet3.Add(Common.GetModItem(ModConditions.martainsOrderMod, "LucidFestival"));
			hashSet3.Add(Common.GetModItem(ModConditions.martainsOrderMod, "LucidNight"));
			hashSet3.Add(Common.GetModItem(ModConditions.medialRiftMod, "RemoteOfTheMetalHeads"));
			hashSet3.Add(Common.GetModItem(ModConditions.metroidMod, "GoldenTorizoSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.metroidMod, "KraidSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.metroidMod, "NightmareSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.metroidMod, "OmegaPirateSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.metroidMod, "PhantoonSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.metroidMod, "SerrisSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.metroidMod, "TorizoSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.ophioidMod, "DeadFungusbug"));
			hashSet3.Add(Common.GetModItem(ModConditions.ophioidMod, "InfestedCompost"));
			hashSet3.Add(Common.GetModItem(ModConditions.ophioidMod, "LivingCarrion"));
			hashSet3.Add(Common.GetModItem(ModConditions.qwertyMod, "AncientEmblem"));
			hashSet3.Add(Common.GetModItem(ModConditions.qwertyMod, "B4Summon"));
			hashSet3.Add(Common.GetModItem(ModConditions.qwertyMod, "BladeBossSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.qwertyMod, "DinoEgg"));
			hashSet3.Add(Common.GetModItem(ModConditions.qwertyMod, "HydraSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.qwertyMod, "RitualInterupter"));
			hashSet3.Add(Common.GetModItem(ModConditions.qwertyMod, "SummoningRune"));
			hashSet3.Add(Common.GetModItem(ModConditions.redemptionMod, "EaglecrestSpelltome"));
			hashSet3.Add(Common.GetModItem(ModConditions.redemptionMod, "EggCrown"));
			hashSet3.Add(Common.GetModItem(ModConditions.redemptionMod, "FowlWarHorn"));
			hashSet3.Add(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ElectromagneticLure"));
			hashSet3.Add(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "SuspiciousLookingCandle"));
			hashSet3.Add(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "JarOfPeanuts"));
			hashSet3.Add(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "CatalystBomb"));
			hashSet3.Add(Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "PumpkinLantern"));
			hashSet3.Add(Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "PrimordiaSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "AbaddonSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "SerpentSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "SoranEmblem"));
			hashSet3.Add(Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "HeirsAuthority"));
			hashSet3.Add(Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "PigmanBanner"));
			hashSet3.Add(Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "SandstormMedallion"));
			hashSet3.Add(Common.GetModItem(ModConditions.spiritMod, "DistressJellyItem"));
			hashSet3.Add(Common.GetModItem(ModConditions.spiritMod, "GladeWreath"));
			hashSet3.Add(Common.GetModItem(ModConditions.spiritMod, "ReachBossSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.spiritMod, "JewelCrown"));
			hashSet3.Add(Common.GetModItem(ModConditions.spiritMod, "BlackPearl"));
			hashSet3.Add(Common.GetModItem(ModConditions.spiritMod, "BlueMoonSpawn"));
			hashSet3.Add(Common.GetModItem(ModConditions.spiritMod, "DuskCrown"));
			hashSet3.Add(Common.GetModItem(ModConditions.spiritMod, "CursedCloth"));
			hashSet3.Add(Common.GetModItem(ModConditions.spiritMod, "StoneSkin"));
			hashSet3.Add(Common.GetModItem(ModConditions.spiritMod, "MartianTransmitter"));
			hashSet3.Add(Common.GetModItem(ModConditions.spookyMod, "Fertilizer"));
			hashSet3.Add(Common.GetModItem(ModConditions.spookyMod, "RottenSeed"));
			hashSet3.Add(Common.GetModItem(ModConditions.stormsAdditionsMod, "AridBossSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.stormsAdditionsMod, "MoonlingSummoner"));
			hashSet3.Add(Common.GetModItem(ModConditions.stormsAdditionsMod, "StormBossSummoner"));
			hashSet3.Add(Common.GetModItem(ModConditions.stormsAdditionsMod, "UltimateBossSummoner"));
			hashSet3.Add(Common.GetModItem(ModConditions.supernovaMod, "BugOnAStick"));
			hashSet3.Add(Common.GetModItem(ModConditions.supernovaMod, "EerieCrystal"));
			hashSet3.Add(Common.GetModItem(ModConditions.thoriumMod, "StormFlare"));
			hashSet3.Add(Common.GetModItem(ModConditions.thoriumMod, "JellyfishResonator"));
			hashSet3.Add(Common.GetModItem(ModConditions.thoriumMod, "UnstableCore"));
			hashSet3.Add(Common.GetModItem(ModConditions.thoriumMod, "AncientBlade"));
			hashSet3.Add(Common.GetModItem(ModConditions.thoriumMod, "StarCaller"));
			hashSet3.Add(Common.GetModItem(ModConditions.thoriumMod, "StriderTear"));
			hashSet3.Add(Common.GetModItem(ModConditions.thoriumMod, "VoidLens"));
			hashSet3.Add(Common.GetModItem(ModConditions.thoriumMod, "AromaticBulb"));
			hashSet3.Add(Common.GetModItem(ModConditions.thoriumMod, "AbyssalShadow2"));
			hashSet3.Add(Common.GetModItem(ModConditions.thoriumMod, "DoomSayersCoin"));
			hashSet3.Add(Common.GetModItem(ModConditions.thoriumMod, "FreshBrain"));
			hashSet3.Add(Common.GetModItem(ModConditions.thoriumMod, "RottingSpore"));
			hashSet3.Add(Common.GetModItem(ModConditions.thoriumMod, "IllusionaryGlass"));
			hashSet3.Add(Common.GetModItem(ModConditions.uhtricMod, "RareGeode"));
			hashSet3.Add(Common.GetModItem(ModConditions.uhtricMod, "SnowyCharcoal"));
			hashSet3.Add(Common.GetModItem(ModConditions.uhtricMod, "CosmicLure"));
			hashSet3.Add(Common.GetModItem(ModConditions.universeOfSwordsMod, "SwordBossSummon"));
			hashSet3.Add(Common.GetModItem(ModConditions.valhallaMod, "HeavensSeal"));
			hashSet3.Add(Common.GetModItem(ModConditions.valhallaMod, "HellishRadish"));
			hashSet3.Add(Common.GetModItem(ModConditions.vitalityMod, "CloudCore"));
			hashSet3.Add(Common.GetModItem(ModConditions.vitalityMod, "AncientCrown"));
			hashSet3.Add(Common.GetModItem(ModConditions.vitalityMod, "MultigemCluster"));
			hashSet3.Add(Common.GetModItem(ModConditions.vitalityMod, "MoonlightLotusFlower"));
			hashSet3.Add(Common.GetModItem(ModConditions.vitalityMod, "Dreadcandle"));
			hashSet3.Add(Common.GetModItem(ModConditions.vitalityMod, "MeatyMushroom"));
			hashSet3.Add(Common.GetModItem(ModConditions.vitalityMod, "AnarchyCrystal"));
			hashSet3.Add(Common.GetModItem(ModConditions.vitalityMod, "TotemofChaos"));
			hashSet3.Add(Common.GetModItem(ModConditions.vitalityMod, "MartianRadio"));
			hashSet3.Add(Common.GetModItem(ModConditions.vitalityMod, "SpiritBox"));
			hashSet3.Add(Common.GetModItem(ModConditions.wayfairContentMod, "MagicFertilizer"));
			hashSet3.Add(Common.GetModItem(ModConditions.zylonMod, "ForgottenFlame"));
			hashSet3.Add(Common.GetModItem(ModConditions.zylonMod, "SlimyScepter"));
			HashSet<int> TempModdedBossAndEventSummons = hashSet3;
			Common.ModdedBossAndEventSummons.UnionWith(TempModdedBossAndEventSummons);
			HashSet<int> hashSet4 = new HashSet<int>();
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "Anemometer"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "BatteredClub"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "BetsyEgg"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "FestiveOrnament"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "ForbiddenScarab"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "ForbiddenTome"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "HeadofMan"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "IceKingsRemains"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "MartianMemoryStick"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "MatsuriLantern"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "NaughtyList"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "PartyInvite"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "PillarSummon"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "RunawayProbe"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "SlimyBarometer"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "SpentLantern"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "SpookyBranch"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "SuspiciousLookingScythe"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "WeatherBalloon"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "AmalgamatedSkull"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "AmalgamatedSpirit"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "AthenianIdol"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "AttractiveOre"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "BloodSushiPlatter"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "BloodUrchin"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "CloudSnack"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "ClownLicense"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "CoreoftheFrostCore"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "CorruptChest"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "CrimsonChest"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "DemonicPlushie"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "DilutedRainbowMatter"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "Eggplant"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "ForbiddenForbiddenFragment"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "GnomeHat"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "GoblinScrap"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "GoldenSlimeCrown"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "GrandCross"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "HallowChest"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "HeartChocolate"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "HemoclawCrab"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "HolyGrail"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "JungleChest"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "LeesHeadband"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "MothLamp"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "MothronEgg"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "Pincushion"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "PinkSlimeCrown"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "PirateFlag"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "PlunderedBooty"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "RuneOrb"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "ShadowflameIcon"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "SlimyLockBox"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "SuspiciousLookingChest"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "SuspiciousLookingLure"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "WormSnack"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "Abeemination2"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "AncientSeal"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "CelestialSigil2"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "CultistSummon"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "DeathBringerFairy"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "DeerThing2"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "FleshyDoll"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "GoreySpine"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "JellyCrystal"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "LihzahrdPowerCell2"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "MechanicalAmalgam"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "MechEye"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "MechSkull"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "MechWorm"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "MutantVoodoo"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "PlanterasFruit"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "PrismaticPrimrose"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "SlimyCrown"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "SuspiciousEye"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "SuspiciousSkull"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "TruffleWorm2"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosMutantMod, "WormyFood"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsMod, "AbomsCurse"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsMod, "ChampionySigil"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsMod, "CoffinSummon"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsMod, "DevisCurse"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsMod, "FragilePixieLamp"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsMod, "MechLure"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsMod, "MutantsCurse"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsMod, "SquirrelCoatofArms"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "AbandonedRemote"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "ABombInMyNation"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "AstrumCor"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "BirbPheromones"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "BlightedEye"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "BloodyWorm"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "ChunkyStardust"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "ClamPearl"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "ColossalTentacle"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "CryingKey"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "DeepseaProteinShake"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "DefiledCore"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "DefiledShard"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "DragonEgg"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "EyeofExtinction"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "FriedDoll"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "HiveTumor"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "LetterofKos"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "MaulerSkull"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "MedallionoftheDesert"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "MurkySludge"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "NoisyWhistle"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "NuclearChunk"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "OphiocordycipitaceaeSprout"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "PlaguedWalkieTalkie"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "PolterplasmicBeacon"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "PortableCodebreaker"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "QuakeIdol"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "RedStainedWormFood"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "RiftofKos"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "SeeFood"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "SirensPearl"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "SomeKindofSpaceWorm"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "StormIdol"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "SulphurBearTrap"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "WormFoodofKos"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsDLCMod, "WyrmTablet"));
			hashSet4.Add(Common.GetModItem(ModConditions.fargosSoulsExtrasMod, "PandorasBox"));
			HashSet<int> TempFargosBossAndEventSummons = hashSet4;
			Common.FargosBossAndEventSummons.UnionWith(TempFargosBossAndEventSummons);
			HashSet<int> hashSet5 = new HashSet<int>();
			hashSet5.Add(Common.GetModPrefix(ModConditions.calamityMod, "Flawless"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.calamityMod, "Silent"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.clickerClassMod, "Elite"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.clickerClassMod, "ClickerRadius"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.martainsOrderMod, "StrikerPrefix"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.orchidMod, "EmpyreanPrefix"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.orchidMod, "EtherealPrefix"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.orchidMod, "BlockingPrefix"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.orchidMod, "BrewingPrefix"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.orchidMod, "LoadedPrefix"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.orchidMod, "SpiritualPrefix"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.secretsOfTheShadowsMod, "Omnipotent"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.secretsOfTheShadowsMod, "Omniscient"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.secretsOfTheShadowsMod, "Soulbound"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.thoriumMod, "Fabled"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.thoriumMod, "Engrossing"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.thoriumMod, "Lucrative"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.vitalityMod, "MalevolentPrefix"));
			hashSet5.Add(Common.GetModPrefix(ModConditions.vitalityMod, "RelentlessPrefix"));
			HashSet<int> ModPrefixes = hashSet5;
			Common.Prefixes.UnionWith(ModPrefixes);
			HashSet<int> hashSet6 = new HashSet<int>();
			hashSet6.Add(Common.GetModItem(ModConditions.aequusMod, "CosmicChest"));
			hashSet6.Add(Common.GetModItem(ModConditions.aequusMod, "TinkerersGuidebook"));
			hashSet6.Add(Common.GetModItem(ModConditions.aequusMod, "MoneyTrashcan"));
			hashSet6.Add(Common.GetModItem(ModConditions.aequusMod, "VictorsReward"));
			hashSet6.Add(Common.GetModItem(ModConditions.calamityMod, "MushroomPlasmaRoot"));
			hashSet6.Add(Common.GetModItem(ModConditions.calamityMod, "InfernalBlood"));
			hashSet6.Add(Common.GetModItem(ModConditions.calamityMod, "RedLightningContainer"));
			hashSet6.Add(Common.GetModItem(ModConditions.calamityMod, "ElectrolyteGelPack"));
			hashSet6.Add(Common.GetModItem(ModConditions.calamityMod, "StarlightFuelCell"));
			hashSet6.Add(Common.GetModItem(ModConditions.calamityMod, "Ectoheart"));
			hashSet6.Add(Common.GetModItem(ModConditions.calamityMod, "CelestialOnion"));
			hashSet6.Add(Common.GetModItem(ModConditions.clickerClassMod, "HeavenlyChip"));
			hashSet6.Add(Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "EnergyCrystal"));
			hashSet6.Add(Common.GetModItem(ModConditions.fargosSoulsMod, "DeerSinew"));
			hashSet6.Add(Common.GetModItem(ModConditions.fargosSoulsMod, "MutantsCreditCard"));
			hashSet6.Add(Common.GetModItem(ModConditions.fargosSoulsMod, "MutantsDiscountCard"));
			hashSet6.Add(Common.GetModItem(ModConditions.fargosSoulsMod, "MutantsPact"));
			hashSet6.Add(Common.GetModItem(ModConditions.fargosSoulsMod, "RabiesVaccine"));
			hashSet6.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "Americano"));
			hashSet6.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "Latte"));
			hashSet6.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "Mocha"));
			hashSet6.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "Cappuccino"));
			hashSet6.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "AirHandcanon"));
			hashSet6.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "HotCase"));
			hashSet6.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "GreatCrystal"));
			hashSet6.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "WhimInABottle"));
			hashSet6.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "SunsHeart"));
			hashSet6.Add(Common.GetModItem(ModConditions.homewardJourneyMod, "TheSwitch"));
			hashSet6.Add(Common.GetModItem(ModConditions.martainsOrderMod, "FishOfPurity"));
			hashSet6.Add(Common.GetModItem(ModConditions.martainsOrderMod, "FishOfSpirit"));
			hashSet6.Add(Common.GetModItem(ModConditions.martainsOrderMod, "FishOfWrath"));
			hashSet6.Add(Common.GetModItem(ModConditions.martainsOrderMod, "ShimmerFish"));
			hashSet6.Add(Common.GetModItem(ModConditions.martainsOrderMod, "MerchantBag"));
			hashSet6.Add(Common.GetModItem(ModConditions.martainsOrderMod, "FirstAidTreatments"));
			hashSet6.Add(Common.GetModItem(ModConditions.martainsOrderMod, "MartiniteBless"));
			hashSet6.Add(Common.GetModItem(ModConditions.redemptionMod, "GalaxyHeart"));
			hashSet6.Add(Common.GetModItem(ModConditions.redemptionMod, "MedicKit"));
			hashSet6.Add(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ScarletStar"));
			hashSet6.Add(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "VioletStar"));
			hashSet6.Add(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "SoulHeart"));
			hashSet6.Add(Common.GetModItem(ModConditions.thoriumMod, "AstralWave"));
			hashSet6.Add(Common.GetModItem(ModConditions.thoriumMod, "InspirationGem"));
			HashSet<int> ModPermanentUpgrades = hashSet6;
			Common.PermanentUpgrades.UnionWith(ModPermanentUpgrades);
			HashSet<int> hashSet7 = new HashSet<int>();
			hashSet7.Add(Common.GetModItem(ModConditions.calamityMod, "EnchantedStarfish"));
			hashSet7.Add(Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "StaminaCrystal"));
			hashSet7.Add(Common.GetModItem(ModConditions.ragnarokMod, "InspirationEssence"));
			hashSet7.Add(Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "VoidenAnkh"));
			hashSet7.Add(Common.GetModItem(ModConditions.thoriumMod, "CrystalWave"));
			hashSet7.Add(Common.GetModItem(ModConditions.thoriumMod, "InspirationFragment"));
			hashSet7.Add(Common.GetModItem(ModConditions.thoriumMod, "InspirationShard"));
			hashSet7.Add(Common.GetModItem(ModConditions.thoriumMod, "InspirationCrystalNew"));
			HashSet<int> ModPermanentMultiUseUpgrades = hashSet7;
			Common.PermanentMultiUseUpgrades.UnionWith(ModPermanentMultiUseUpgrades);
			HashSet<int> hashSet8 = new HashSet<int>();
			hashSet8.Add(Common.GetModItem(ModConditions.calamityMod, "RogueEmblem"));
			hashSet8.Add(Common.GetModItem(ModConditions.clickerClassMod, "ClickerEmblem"));
			hashSet8.Add(Common.GetModItem(ModConditions.martainsOrderMod, "ThrowerEmblem"));
			hashSet8.Add(Common.GetModItem(ModConditions.martainsOrderMod, "NeutralEmblem"));
			hashSet8.Add(Common.GetModItem(ModConditions.orchidMod, "GuardianEmblem"));
			hashSet8.Add(Common.GetModItem(ModConditions.orchidMod, "ShamanEmblem"));
			hashSet8.Add(Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "NinjaEmblem"));
			hashSet8.Add(Common.GetModItem(ModConditions.thoriumMod, "BardEmblem"));
			hashSet8.Add(Common.GetModItem(ModConditions.thoriumMod, "ClericEmblem"));
			hashSet8.Add(Common.GetModItem(ModConditions.thoriumMod, "NinjaEmblem"));
			HashSet<int> ModEmblems = hashSet8;
			Common.Emblems.UnionWith(ModEmblems);
			HashSet<int> hashSet9 = new HashSet<int>();
			hashSet9.Add(Common.GetModTile(ModConditions.aequusMod, "Manacle"));
			hashSet9.Add(Common.GetModTile(ModConditions.aequusMod, "Mistral"));
			hashSet9.Add(Common.GetModTile(ModConditions.aequusMod, "Moonflower"));
			hashSet9.Add(Common.GetModTile(ModConditions.aequusMod, "Moray"));
			hashSet9.Add(Common.GetModTile(ModConditions.thoriumMod, "IllumiteChunk"));
			hashSet9.Add(Common.GetModTile(ModConditions.thoriumMod, "LifeQuartz"));
			hashSet9.Add(Common.GetModTile(ModConditions.thoriumMod, "LodeStone"));
			hashSet9.Add(Common.GetModTile(ModConditions.thoriumMod, "SmoothCoal"));
			hashSet9.Add(Common.GetModTile(ModConditions.thoriumMod, "ThoriumOre"));
			hashSet9.Add(Common.GetModTile(ModConditions.thoriumMod, "ValadiumChunk"));
			HashSet<int> ModIgnoredTiles = hashSet9;
			Common.IgnoredTilesForExplosives.UnionWith(ModIgnoredTiles);
			HashSet<Mod> TempIgnoredModsForExplosives = new HashSet<Mod>();
			if (ModConditions.confectionRebakedLoaded)
			{
				TempIgnoredModsForExplosives.Add(ModConditions.confectionRebakedMod);
			}
			if (ModConditions.depthsLoaded)
			{
				TempIgnoredModsForExplosives.Add(ModConditions.depthsMod);
			}
			if (ModConditions.infectedQualitiesLoaded)
			{
				TempIgnoredModsForExplosives.Add(ModConditions.infectedQualitiesMod);
			}
			if (ModConditions.martainsOrderLoaded)
			{
				TempIgnoredModsForExplosives.Add(ModConditions.martainsOrderMod);
			}
			if (ModConditions.orchidLoaded)
			{
				TempIgnoredModsForExplosives.Add(ModConditions.orchidMod);
			}
			if (ModConditions.remnantsLoaded)
			{
				TempIgnoredModsForExplosives.Add(ModConditions.remnantsMod);
			}
			Common.IgnoredModsForExplosives.UnionWith(TempIgnoredModsForExplosives);
			for (int i = BuffID.Count; i < BuffLoader.BuffCount; i++)
			{
				if (BuffID.Sets.IsAFlaskBuff[BuffLoader.GetBuff(i).Type] && !Common.FlaskBuffs.Contains(BuffLoader.GetBuff(i).Type))
				{
					Common.FlaskBuffs.Add(BuffLoader.GetBuff(i).Type);
				}
			}
			if (ModConditions.thoriumLoaded)
			{
				HashSet<int> hashSet10 = new HashSet<int>();
				hashSet10.Add(Common.GetModBuff(ModConditions.thoriumMod, "DeepFreezeCoatingBuff"));
				hashSet10.Add(Common.GetModBuff(ModConditions.thoriumMod, "ExplosiveCoatingBuff"));
				hashSet10.Add(Common.GetModBuff(ModConditions.thoriumMod, "GorgonCoatingBuff"));
				hashSet10.Add(Common.GetModBuff(ModConditions.thoriumMod, "SporeCoatingBuff"));
				hashSet10.Add(Common.GetModBuff(ModConditions.thoriumMod, "ToxicCoatingBuff"));
				HashSet<int> TempThoriumCoatings = hashSet10;
				Common.ThoriumCoatings.UnionWith(TempThoriumCoatings);
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0004CCBF File Offset: 0x0004AEBF
		public static void Reset()
		{
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0004CCC4 File Offset: 0x0004AEC4
		public static Color ColorSwap(Color firstColor, Color secondColor, float seconds)
		{
			float num = (float)((Math.Sin(6.283185307179586 / (double)seconds * (double)Main.GlobalTimeWrappedHourly) + 1.0) * 0.5);
			return Color.Lerp(firstColor, secondColor, num);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0004CD07 File Offset: 0x0004AF07
		public static bool IsCoin(int type)
		{
			return type >= 71 && type <= 74;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0004CD18 File Offset: 0x0004AF18
		public static ulong CalculateCoinValue(int type, uint stack)
		{
			ulong result;
			switch (type)
			{
			case 71:
				result = (ulong)stack;
				break;
			case 72:
				result = (ulong)stack * 100UL;
				break;
			case 73:
				result = (ulong)stack * 10000UL;
				break;
			case 74:
				result = (ulong)stack * 1000000UL;
				break;
			default:
				result = 0UL;
				break;
			}
			return result;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0004CD6C File Offset: 0x0004AF6C
		public static List<Item> ConvertCopperValueToCoins(ulong value)
		{
			ValueTuple<ulong, ulong> valueTuple = Math.DivRem(value, 1000000UL);
			ulong plat = valueTuple.Item1;
			ValueTuple<ulong, ulong> valueTuple2 = Math.DivRem(valueTuple.Item2, 10000UL);
			ulong gold = valueTuple2.Item1;
			ValueTuple<ulong, ulong> valueTuple3 = Math.DivRem(valueTuple2.Item2, 100UL);
			ulong silver = valueTuple3.Item1;
			ulong copper = valueTuple3.Item2;
			List<Item> toReturn = new List<Item>();
			while (plat > 0UL)
			{
				toReturn.Add(new Item(74, Math.Min((int)plat, Common.PlatinumMaxStack), 0));
				plat -= Math.Min(plat, (ulong)((long)Common.PlatinumMaxStack));
			}
			toReturn.Add(new Item(73, (int)gold, 0));
			toReturn.Add(new Item(72, (int)silver, 0));
			toReturn.Add(new Item(71, (int)copper, 0));
			return toReturn;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0004CE2C File Offset: 0x0004B02C
		public static int GetModItem(Mod mod, string itemName)
		{
			ModItem currItem;
			if (mod != null && mod.TryFind<ModItem>(itemName, out currItem) && currItem != null)
			{
				return currItem.Type;
			}
			return 0;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0004CE54 File Offset: 0x0004B054
		public static int GetModProjectile(Mod mod, string projName)
		{
			ModProjectile currProj;
			if (mod != null && mod.TryFind<ModProjectile>(projName, out currProj) && currProj != null)
			{
				return currProj.Type;
			}
			return 0;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0004CE7C File Offset: 0x0004B07C
		public static int GetModNPC(Mod mod, string npcName)
		{
			ModNPC currNPC;
			if (mod != null && mod.TryFind<ModNPC>(npcName, out currNPC) && currNPC != null)
			{
				return currNPC.Type;
			}
			return 0;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0004CEA4 File Offset: 0x0004B0A4
		public static int GetModTile(Mod mod, string tileName)
		{
			ModTile currTile;
			if (mod != null && mod.TryFind<ModTile>(tileName, out currTile) && currTile != null)
			{
				return (int)currTile.Type;
			}
			return -1;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0004CECC File Offset: 0x0004B0CC
		public static int GetModWall(Mod mod, string wallName)
		{
			ModWall currWall;
			if (mod != null && mod.TryFind<ModWall>(wallName, out currWall) && currWall != null)
			{
				return (int)currWall.Type;
			}
			return 0;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0004CEF4 File Offset: 0x0004B0F4
		public static int GetModBuff(Mod mod, string buffName)
		{
			ModBuff currBuff;
			if (mod != null && mod.TryFind<ModBuff>(buffName, out currBuff) && currBuff != null)
			{
				return currBuff.Type;
			}
			return -1;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0004CF1C File Offset: 0x0004B11C
		public static int GetModPrefix(Mod mod, string prefixName)
		{
			ModPrefix currPrefix;
			if (mod != null && mod.TryFind<ModPrefix>(prefixName, out currPrefix) && currPrefix != null)
			{
				return currPrefix.Type;
			}
			return -1;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0004CF44 File Offset: 0x0004B144
		public static DamageClass GetModDamageClass(Mod mod, string className)
		{
			DamageClass currClass;
			if (mod != null && mod.TryFind<DamageClass>(className, out currClass) && currClass != null)
			{
				return currClass;
			}
			return DamageClass.Default;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0004CF69 File Offset: 0x0004B169
		public static string ModBuffAsset(Mod mod, int buffType)
		{
			if (mod != null && BuffLoader.GetBuff(buffType) != null)
			{
				return BuffLoader.GetBuff(buffType).Texture;
			}
			return "QoLCompendium/Assets/Items/PermanentBuff";
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0004CF88 File Offset: 0x0004B188
		public static void CreateBagRecipe(int[] items, int bagID)
		{
			foreach (int resultID in items)
			{
				int tileID = 18;
				int ingredientAmount = 1;
				int resultAmount = 1;
				bool disableDecraft = true;
				bool usesRecipeGroup = false;
				Condition[] array = new Condition[1];
				array[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.BossBags", () => QoLCompendium.mainConfig.BossBagRecipes);
				Common.CreateSimpleRecipe(bagID, resultID, tileID, ingredientAmount, resultAmount, disableDecraft, usesRecipeGroup, array);
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0004CFE4 File Offset: 0x0004B1E4
		public static void CreateCrateRecipe(int result, int crateID, int crateHardmodeID, int crateCount, params Condition[] conditions)
		{
			Recipe recipe = ModConditions.GetItemRecipe(() => QoLCompendium.mainConfig.CrateRecipes, result, 1, "Mods.QoLCompendium.ItemToggledConditions.Crates");
			recipe.AddIngredient(crateID, crateCount);
			foreach (Condition condition in conditions)
			{
				recipe.AddCondition(condition);
			}
			recipe.AddTile(18);
			recipe.DisableDecraft();
			recipe.Register();
			recipe = ModConditions.GetItemRecipe(() => QoLCompendium.mainConfig.CrateRecipes, result, 1, "Mods.QoLCompendium.ItemToggledConditions.Crates");
			recipe.AddIngredient(crateHardmodeID, crateCount);
			foreach (Condition condition2 in conditions)
			{
				recipe.AddCondition(condition2);
			}
			recipe.AddTile(18);
			recipe.DisableDecraft();
			recipe.Register();
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0004D0C8 File Offset: 0x0004B2C8
		public static void CreateCrateHardmodeRecipe(int result, int crateHardmodeID, int crateCount, params Condition[] conditions)
		{
			Recipe recipe = ModConditions.GetItemRecipe(() => QoLCompendium.mainConfig.CrateRecipes, result, 1, "Mods.QoLCompendium.ItemToggledConditions.Crates");
			recipe.AddIngredient(crateHardmodeID, crateCount);
			foreach (Condition condition in conditions)
			{
				recipe.AddCondition(condition);
			}
			recipe.AddTile(18);
			recipe.DisableDecraft();
			recipe.Register();
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0004D140 File Offset: 0x0004B340
		public static void CreateCrateWithKeyRecipe(int item, int crateID, int crateHardmodeID, int crateCount, int keyID, params Condition[] conditions)
		{
			Recipe recipe = ModConditions.GetItemRecipe(() => QoLCompendium.mainConfig.CrateRecipes, item, 1, "Mods.QoLCompendium.ItemToggledConditions.Crates");
			recipe.AddIngredient(crateID, crateCount);
			recipe.AddIngredient(keyID, 1);
			foreach (Condition condition in conditions)
			{
				recipe.AddCondition(condition);
			}
			recipe.AddTile(18);
			recipe.DisableDecraft();
			recipe.Register();
			recipe = ModConditions.GetItemRecipe(() => QoLCompendium.mainConfig.CrateRecipes, item, 1, "Mods.QoLCompendium.ItemToggledConditions.Crates");
			recipe.AddIngredient(crateHardmodeID, crateCount);
			recipe.AddIngredient(keyID, 1);
			foreach (Condition condition2 in conditions)
			{
				recipe.AddCondition(condition2);
			}
			recipe.AddTile(18);
			recipe.DisableDecraft();
			recipe.Register();
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0004D238 File Offset: 0x0004B438
		public static void ConversionRecipe(int item1, int item2, int station)
		{
			int ingredientAmount = 1;
			int resultAmount = 1;
			bool disableDecraft = false;
			bool usesRecipeGroup = false;
			Condition[] array = new Condition[1];
			array[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.ItemConversions", () => QoLCompendium.mainConfig.ItemConversions);
			Common.CreateSimpleRecipe(item1, item2, station, ingredientAmount, resultAmount, disableDecraft, usesRecipeGroup, array);
			int ingredientAmount2 = 1;
			int resultAmount2 = 1;
			bool disableDecraft2 = false;
			bool usesRecipeGroup2 = false;
			Condition[] array2 = new Condition[1];
			array2[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.ItemConversions", () => QoLCompendium.mainConfig.ItemConversions);
			Common.CreateSimpleRecipe(item2, item1, station, ingredientAmount2, resultAmount2, disableDecraft2, usesRecipeGroup2, array2);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0004D2C1 File Offset: 0x0004B4C1
		public static void AddBannerGroupToItemRecipe(int recipeGroupID, int resultID, int resultAmount = 1, int groupAmount = 1, params Condition[] conditions)
		{
			Common.CreateSimpleRecipe(recipeGroupID, resultID, 18, groupAmount, resultAmount, true, true, conditions);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0004D2D2 File Offset: 0x0004B4D2
		public static void AddBannerToItemRecipe(int bannerItemID, int resultID, int bannerAmount = 1, int resultAmount = 1, params Condition[] conditions)
		{
			Common.CreateSimpleRecipe(bannerItemID, resultID, 18, bannerAmount, resultAmount, true, false, conditions);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0004D2E4 File Offset: 0x0004B4E4
		public static void AddBannerSetToItemRecipe(bool[] set, int resultID)
		{
			for (int i = 0; i < (int)NPCID.Count; i++)
			{
				if (set[i])
				{
					int num = Item.NPCtoBanner(i);
					if (num > 0)
					{
						int ingredientID = Item.BannerToItem(num);
						int tileID = 18;
						int ingredientAmount = 1;
						int resultAmount = 1;
						bool disableDecraft = true;
						bool usesRecipeGroup = false;
						Condition[] array = new Condition[1];
						array[0] = ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", () => QoLCompendium.mainConfig.BannerRecipes);
						Common.CreateSimpleRecipe(ingredientID, resultID, tileID, ingredientAmount, resultAmount, disableDecraft, usesRecipeGroup, array);
					}
				}
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0004D358 File Offset: 0x0004B558
		public static void CreateSimpleRecipe(int ingredientID, int resultID, int tileID, int ingredientAmount = 1, int resultAmount = 1, bool disableDecraft = false, bool usesRecipeGroup = false, params Condition[] conditions)
		{
			Recipe recipe = Recipe.Create(resultID, resultAmount);
			if (usesRecipeGroup)
			{
				recipe.AddRecipeGroup(ingredientID, ingredientAmount);
			}
			else
			{
				recipe.AddIngredient(ingredientID, ingredientAmount);
			}
			recipe.AddTile(tileID);
			foreach (Condition condition in conditions)
			{
				recipe.AddCondition(condition);
			}
			if (disableDecraft)
			{
				recipe.DisableDecraft();
			}
			recipe.Register();
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0004D3C0 File Offset: 0x0004B5C0
		public static void SpawnBoss(Player player, int bossType)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
				Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
				int npc = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int)spawnPosition.X, (int)spawnPosition.Y, bossType, 0, 0f, 0f, 0f, 0f, 255);
				if (Main.netMode != 0)
				{
					NetMessage.SendData(61, -1, -1, null, player.whoAmI, (float)bossType, 0f, 0f, 0, 0, 0);
				}
				if (npc != Main.maxNPCs && Main.netMode == 2)
				{
					NetMessage.SendData(23, -1, -1, null, npc, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0004D49C File Offset: 0x0004B69C
		public static Asset<Texture2D> GetAsset(string location, string filename, int fileNumber = -1)
		{
			if (fileNumber > -1)
			{
				return ModContent.Request<Texture2D>(string.Concat(new string[]
				{
					"QoLCompendium/Assets/",
					location,
					"/",
					filename,
					fileNumber.ToString()
				}), 2);
			}
			return ModContent.Request<Texture2D>("QoLCompendium/Assets/" + location + "/" + filename, 2);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0004D4F8 File Offset: 0x0004B6F8
		public static void TransmuteItems(int[] items)
		{
			for (int i = 0; i < items.Length; i++)
			{
				if (i >= items.Length - 1)
				{
					ItemID.Sets.ShimmerTransformToItem[items.Last<int>()] = items.First<int>();
				}
				else
				{
					ItemID.Sets.ShimmerTransformToItem[items[i]] = items[i + 1];
				}
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0004D540 File Offset: 0x0004B740
		public static bool CheckToActivateGlintEffect(Item item)
		{
			return !Main.gameMenu && Main.LocalPlayer.active && ((QoLCompendium.mainConfig.ActiveBuffsHaveEnchantedEffects && !item.IsAir && Main.LocalPlayer.GetModPlayer<QoLCPlayer>().activeBuffItems.Contains(item.type)) || (QoLCompendium.mainConfig.GoodPrefixesHaveEnchantedEffects && !item.IsAir && Common.Prefixes.Contains(item.prefix)));
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0004D5C0 File Offset: 0x0004B7C0
		public static int GetSlotItemIsIn(Item lookForThis, Item[] inventory)
		{
			for (int i = 0; i < inventory.Length; i++)
			{
				if (inventory[i].type == lookForThis.type)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0004D5F0 File Offset: 0x0004B7F0
		public static void SetDefaultsToPermanentBuff(Item item)
		{
			item.width = (item.height = 16);
			item.consumable = false;
			item.maxStack = 1;
			item.SetShopValues(ItemRarityColor.Blue1, 0);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0004D624 File Offset: 0x0004B824
		public static void HandleFlaskBuffs(Player player)
		{
			foreach (int buff in Common.FlaskBuffs)
			{
				player.buffImmune[buff] = true;
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0004D678 File Offset: 0x0004B878
		public static void HandleCoatingBuffs(Player player)
		{
			foreach (int buff in Common.ThoriumCoatings)
			{
				player.buffImmune[buff] = true;
			}
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0004D6CC File Offset: 0x0004B8CC
		public static StatModifier GetBestClassDamage(this Player player)
		{
			StatModifier ret = StatModifier.Default;
			StatModifier classless = player.GetTotalDamage<GenericDamageClass>();
			ret.Base = classless.Base;
			ret *= classless.Multiplicative;
			ret.Flat = classless.Flat;
			float best = 1f;
			float melee = player.GetTotalDamage<MeleeDamageClass>().Additive;
			if (melee > best)
			{
				best = melee;
			}
			float ranged = player.GetTotalDamage<RangedDamageClass>().Additive;
			if (ranged > best)
			{
				best = ranged;
			}
			float magic = player.GetTotalDamage<MagicDamageClass>().Additive;
			if (magic > best)
			{
				best = magic;
			}
			float summon = player.GetTotalDamage<SummonDamageClass>().Additive;
			if (summon > best)
			{
				best = summon;
			}
			for (int i = 0; i < DamageClassLoader.DamageClassCount; i++)
			{
				float dClass = player.GetTotalDamage(DamageClassLoader.GetDamageClass(i)).Additive;
				if (dClass > best)
				{
					best = dClass;
				}
			}
			return ret + (best - 1f);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0004D7B8 File Offset: 0x0004B9B8
		public static int ToFrames(float seconds, int extraUpdates = 0)
		{
			return (int)(seconds * 60f * (float)(extraUpdates + 1));
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0004D7C7 File Offset: 0x0004B9C7
		public static int ToPixels(float blocks)
		{
			return (int)(blocks * 16f);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0004D7D1 File Offset: 0x0004B9D1
		public static float ToSeconds(float frames, int extraUpdates = 0)
		{
			return frames / (float)(60 * (extraUpdates + 1));
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0004D7DC File Offset: 0x0004B9DC
		public static float ToBlocks(float pixels)
		{
			return pixels / 16f;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0004D7E5 File Offset: 0x0004B9E5
		public static Point16 PlayerCenterTile(Player player)
		{
			return new Point16((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f));
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0004D810 File Offset: 0x0004BA10
		public static int PlayerCenterTileX(Player player)
		{
			return (int)(player.Center.X / 16f);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0004D824 File Offset: 0x0004BA24
		public static int PlayerCenterTileY(Player player)
		{
			return (int)(player.Center.Y / 16f);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0004D838 File Offset: 0x0004BA38
		public static bool InGameWorldLeft(int x)
		{
			return x > 39;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0004D83F File Offset: 0x0004BA3F
		public static bool InGameWorldRight(int x)
		{
			return x < Main.maxTilesX - 39;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0004D838 File Offset: 0x0004BA38
		public static bool InGameWorldTop(int y)
		{
			return y > 39;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0004D84C File Offset: 0x0004BA4C
		public static bool InGameWorldBottom(int y)
		{
			return y < Main.maxTilesY - 39;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0004D859 File Offset: 0x0004BA59
		public static bool InGameWorld(int x, int y)
		{
			return x > 39 && x < Main.maxTilesX - 39 && y > 39 && y < Main.maxTilesY - 39;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0004D87D File Offset: 0x0004BA7D
		public static bool InWorldLeft(int x)
		{
			return x >= 0;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0004D886 File Offset: 0x0004BA86
		public static bool InWorldRight(int x)
		{
			return x < Main.maxTilesX;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0004D87D File Offset: 0x0004BA7D
		public static bool InWorldTop(int y)
		{
			return y >= 0;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0004D890 File Offset: 0x0004BA90
		public static bool InWorldBottom(int y)
		{
			return y < Main.maxTilesY;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0004D89A File Offset: 0x0004BA9A
		public static bool InWorld(int x, int y)
		{
			return x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0004D8B6 File Offset: 0x0004BAB6
		public static int CoordsX(int x)
		{
			return x * 2 - Main.maxTilesX;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0004D8C1 File Offset: 0x0004BAC1
		public static int CoordsY(int y)
		{
			return y * 2 - (int)Main.worldSurface * 2;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0004D8D0 File Offset: 0x0004BAD0
		public static string CoordsString(int x, int y)
		{
			x = x * 2 - Main.maxTilesX;
			y = y * 2 - (int)Main.worldSurface * 2;
			string text = (x < 0) ? " west, " : " east, ";
			string text2 = (y < 0) ? " surface." : " underground.";
			x = ((x < 0) ? (x * -1) : x);
			y = ((y < 0) ? (y * -1) : y);
			return x.ToString() + text + y.ToString() + text2;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0004D948 File Offset: 0x0004BB48
		public static void TileSafe(int x, int y)
		{
			if (x < 0 || y < 0 || x > Main.ActiveWorldFileData.WorldSizeX || y > Main.ActiveWorldFileData.WorldSizeY)
			{
				return;
			}
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y].ResetToType(0);
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0004D9A1 File Offset: 0x0004BBA1
		public static bool TileNull(int x, int y)
		{
			return Main.tile[x, y] == null;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0004D9B8 File Offset: 0x0004BBB8
		public unsafe static bool SolidTile(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return !Common.TileNull(x, y) && tile.HasTile && Main.tileSolid[(int)(*tile.TileType)] && !Main.tileSolidTop[(int)(*tile.TileType)] && !tile.IsHalfBlock && tile.Slope == SlopeType.Solid && !tile.IsActuated;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0004DA24 File Offset: 0x0004BC24
		public static bool SearchBelow(Player player, Func<int, int, bool> toSearch, int gap)
		{
			int centerX = Common.PlayerCenterTileX(player);
			int centerY = Common.PlayerCenterTileY(player);
			int num3 = 0;
			while (Common.InGameWorldLeft(centerX - num3) || Common.InGameWorldRight(centerX + num3))
			{
				int tempY = centerY;
				while (Common.InGameWorldBottom(tempY))
				{
					int nX = centerX - num3;
					int pX = centerX + num3;
					if (Common.InGameWorldLeft(nX))
					{
						Common.TileSafe(nX, tempY);
						if (toSearch(nX, tempY))
						{
							return true;
						}
					}
					if (Common.InGameWorldRight(pX))
					{
						Common.TileSafe(pX, tempY);
						if (toSearch(pX, tempY))
						{
							return true;
						}
					}
					tempY += gap;
				}
				num3 += gap;
			}
			return false;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0004DAB4 File Offset: 0x0004BCB4
		public static void AddAfter<T>(this List<T> list, T element, T item)
		{
			int idx = list.IndexOf(element);
			list.Insert(idx + 1, item);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0004DAD3 File Offset: 0x0004BCD3
		public static string GetTooltipValue(string suffix, params object[] args)
		{
			return Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips." + suffix, args);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0004DAE8 File Offset: 0x0004BCE8
		public static void AddLastTooltip(List<TooltipLine> tooltips, TooltipLine tooltip)
		{
			TooltipLine last = tooltips.FindLast((TooltipLine t) => t.Mod == "Terraria");
			tooltips.AddAfter(last, tooltip);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0004DB24 File Offset: 0x0004BD24
		public static string GetFullNameById(int id, int subtype = -1)
		{
			ModTile modTile = TileLoader.GetTile(id);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			if (modTile != null)
			{
				string name = modTile.Mod.Name;
				string str = ":";
				string name2 = modTile.Name;
				string str2;
				if (subtype < 0)
				{
					str2 = "";
				}
				else
				{
					defaultInterpolatedStringHandler..ctor(1, 1);
					defaultInterpolatedStringHandler.AppendLiteral(":");
					defaultInterpolatedStringHandler.AppendFormatted<int>(subtype);
					str2 = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				return name + str + name2 + str2;
			}
			if (id < (int)TileID.Count)
			{
				string str3 = "Terraria:";
				string name3 = TileID.Search.GetName(id);
				string str4;
				if (subtype < 0)
				{
					str4 = "";
				}
				else
				{
					defaultInterpolatedStringHandler..ctor(1, 1);
					defaultInterpolatedStringHandler.AppendLiteral(":");
					defaultInterpolatedStringHandler.AppendFormatted<int>(subtype);
					str4 = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				return str3 + name3 + str4;
			}
			return null;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0004DBD4 File Offset: 0x0004BDD4
		public static void UpdateWhitelist(int typeId, string name, int style = -1, bool remove = false)
		{
			if (!remove)
			{
				if (!QoLCompendium.mainConfig.VeinMinerTileWhitelist.Contains(name))
				{
					QoLCompendium.mainConfig.VeinMinerTileWhitelist.Add(name);
					Common.SaveConfig(QoLCompendium.mainConfig);
				}
				if (Main.netMode == 0)
				{
					if (KeybindPlayer.timeout <= 0)
					{
						KeybindPlayer.timeout = byte.MaxValue;
						return;
					}
					KeybindPlayer.timeout = 0;
					QoLCompendium.mainConfig.VeinMinerTileWhitelist.Add(name);
					Common.SaveConfig(QoLCompendium.mainConfig);
					return;
				}
			}
			else if (QoLCompendium.mainConfig.VeinMinerTileWhitelist.Contains(name))
			{
				QoLCompendium.mainConfig.VeinMinerTileWhitelist.Remove(name);
				Common.SaveConfig(QoLCompendium.mainConfig);
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0004DC7A File Offset: 0x0004BE7A
		public static bool TryAcceptChanges(int whoAmI, ref NetworkText message)
		{
			if (NetMessage.DoesPlayerSlotCountAsAHost(whoAmI))
			{
				return true;
			}
			message = NetworkText.FromKey(Language.GetTextValue("Mods.QoLCompendium.Messages.HostOnly"), Array.Empty<object>());
			return false;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0004DCA0 File Offset: 0x0004BEA0
		public static void SaveConfig(ModConfig config)
		{
			MethodInfo saveMethodInfo = typeof(ConfigManager).GetMethod("Save", BindingFlags.Static | BindingFlags.NonPublic);
			if (saveMethodInfo != null)
			{
				saveMethodInfo.Invoke(null, new object[]
				{
					config
				});
				return;
			}
			throw new Exception("Config file could not be created or updated at:\n'{path}'");
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0004DCE4 File Offset: 0x0004BEE4
		// Note: this type is marked as 'beforefieldinit'.
		static Common()
		{
			HashSet<int> hashSet = new HashSet<int>(Enumerable.Range(678, 10));
			hashSet.Add(670);
			Common.TownSlimeIDs = hashSet;
			HashSet<int> hashSet2 = new HashSet<int>();
			hashSet2.Add(507);
			hashSet2.Add(517);
			hashSet2.Add(493);
			hashSet2.Add(422);
			Common.LunarPillarIDs = hashSet2;
			HashSet<int> hashSet3 = new HashSet<int>();
			hashSet3.Add(71);
			hashSet3.Add(72);
			hashSet3.Add(73);
			hashSet3.Add(74);
			Common.CoinIDs = hashSet3;
			HashSet<int> hashSet4 = new HashSet<int>();
			hashSet4.Add(1);
			hashSet4.Add(2);
			hashSet4.Add(3);
			hashSet4.Add(5);
			hashSet4.Add(6);
			hashSet4.Add(7);
			hashSet4.Add(8);
			hashSet4.Add(9);
			hashSet4.Add(16);
			hashSet4.Add(105);
			hashSet4.Add(17);
			hashSet4.Add(114);
			hashSet4.Add(113);
			hashSet4.Add(116);
			hashSet4.Add(104);
			hashSet4.Add(115);
			hashSet4.Add(117);
			hashSet4.Add(111);
			Common.RedPotionBuffs = hashSet4;
			Common.PlatinumMaxStack = 9999;
			HashSet<int> hashSet5 = new HashSet<int>();
			hashSet5.Add(5337);
			hashSet5.Add(5339);
			hashSet5.Add(5338);
			hashSet5.Add(5342);
			hashSet5.Add(5341);
			hashSet5.Add(5340);
			hashSet5.Add(5343);
			hashSet5.Add(5326);
			hashSet5.Add(4382);
			hashSet5.Add(5336);
			hashSet5.Add(5043);
			hashSet5.Add(5289);
			Common.PermanentUpgrades = hashSet5;
			HashSet<int> hashSet6 = new HashSet<int>();
			hashSet6.Add(29);
			hashSet6.Add(109);
			hashSet6.Add(1291);
			Common.PermanentMultiUseUpgrades = hashSet6;
			Common.BossIDs = new int[]
			{
				50,
				4,
				13,
				266,
				222,
				668,
				35,
				113,
				657,
				125,
				126,
				134,
				127,
				262,
				245,
				370,
				636,
				439,
				398
			};
			HashSet<int> hashSet7 = new HashSet<int>();
			hashSet7.Add(560);
			hashSet7.Add(43);
			hashSet7.Add(70);
			hashSet7.Add(1331);
			hashSet7.Add(1133);
			hashSet7.Add(5120);
			hashSet7.Add(4988);
			hashSet7.Add(556);
			hashSet7.Add(544);
			hashSet7.Add(557);
			hashSet7.Add(5334);
			hashSet7.Add(3601);
			hashSet7.Add(4271);
			hashSet7.Add(361);
			hashSet7.Add(1315);
			hashSet7.Add(2767);
			hashSet7.Add(602);
			hashSet7.Add(1844);
			hashSet7.Add(1958);
			Common.VanillaBossAndEventSummons = hashSet7;
			HashSet<int> hashSet8 = new HashSet<int>();
			hashSet8.Add(1293);
			hashSet8.Add(3828);
			Common.VanillaRightClickBossAndEventSummons = hashSet8;
			Common.ModdedBossAndEventSummons = new HashSet<int>();
			Common.FargosBossAndEventSummons = new HashSet<int>();
			Common.EvilWallIDs = new ushort[]
			{
				264,
				69,
				268,
				81,
				265,
				70,
				246,
				3,
				269,
				83,
				248,
				305,
				217,
				306,
				218,
				307,
				219,
				308,
				220,
				309,
				221,
				310,
				222,
				276,
				188,
				277,
				189,
				278,
				190,
				279,
				191,
				280,
				192,
				281,
				193,
				282,
				194,
				283,
				195,
				288,
				200,
				289,
				201,
				290,
				202,
				291,
				203
			};
			Common.PureWallIDs = new ushort[]
			{
				66,
				63,
				66,
				63,
				66,
				63,
				1,
				1,
				1,
				1,
				1,
				304,
				216,
				304,
				216,
				304,
				216,
				275,
				187,
				275,
				187,
				275,
				187,
				284,
				196,
				285,
				197,
				286,
				198,
				287,
				199,
				284,
				196,
				285,
				197,
				286,
				198,
				287,
				199,
				284,
				196,
				285,
				197,
				286,
				198,
				287,
				199
			};
			HashSet<int> hashSet9 = new HashSet<int>();
			hashSet9.Add(31);
			hashSet9.Add(56);
			hashSet9.Add(241);
			hashSet9.Add(67);
			hashSet9.Add(71);
			hashSet9.Add(179);
			Common.FallingBlocks = hashSet9;
			HashSet<int> hashSet10 = new HashSet<int>();
			hashSet10.Add(490);
			hashSet10.Add(491);
			hashSet10.Add(489);
			hashSet10.Add(2998);
			Common.Emblems = hashSet10;
			HashSet<int> hashSet11 = new HashSet<int>();
			hashSet11.Add(58);
			hashSet11.Add(1734);
			hashSet11.Add(1867);
			hashSet11.Add(184);
			hashSet11.Add(1735);
			hashSet11.Add(1868);
			hashSet11.Add(3453);
			hashSet11.Add(3454);
			hashSet11.Add(3455);
			Common.PowerUpItems = hashSet11;
			Common.VanillaFountains = new int[]
			{
				909,
				942,
				940,
				944,
				941,
				910,
				4417,
				943
			};
			HashSet<int> hashSet12 = new HashSet<int>();
			hashSet12.Add(525);
			hashSet12.Add(734);
			hashSet12.Add(ModContent.ProjectileType<FlyingSafeProjectile>());
			hashSet12.Add(ModContent.ProjectileType<EtherianConstructProjectile>());
			Common.MobileStorages = hashSet12;
			Common.NormalBunnies = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				46,
				647,
				648,
				650,
				649,
				651,
				646,
				652,
				614,
				303,
				337,
				47,
				464,
				540
			});
			Common.NormalSquirrels = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				299,
				538,
				640,
				641,
				643,
				642,
				644,
				639,
				645
			});
			Common.NormalButterflies = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				356,
				653,
				661
			});
			Common.NormalBirds = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				74,
				297,
				298
			});
			HashSet<int> hashSet13 = new HashSet<int>();
			hashSet13.Add(81);
			hashSet13.Add(84);
			hashSet13.Add(59);
			hashSet13.Add(15);
			hashSet13.Add(17);
			hashSet13.Add(60);
			hashSet13.Add(82);
			hashSet13.Add(83);
			hashSet13.Add(57);
			hashSet13.Add(65);
			hashSet13.Add(66);
			hashSet13.Add(68);
			hashSet13.Add(72);
			hashSet13.Add(76);
			hashSet13.Add(80);
			Common.Prefixes = hashSet13;
			HashSet<int> hashSet14 = new HashSet<int>();
			hashSet14.Add(854);
			hashSet14.Add(855);
			hashSet14.Add(3033);
			hashSet14.Add(3034);
			hashSet14.Add(3035);
			hashSet14.Add(3619);
			hashSet14.Add(2799);
			hashSet14.Add(3611);
			hashSet14.Add(3123);
			hashSet14.Add(3124);
			hashSet14.Add(5437);
			hashSet14.Add(5358);
			hashSet14.Add(5359);
			hashSet14.Add(5360);
			hashSet14.Add(5361);
			hashSet14.Add(395);
			hashSet14.Add(3122);
			hashSet14.Add(3121);
			hashSet14.Add(3036);
			hashSet14.Add(15);
			hashSet14.Add(707);
			hashSet14.Add(16);
			hashSet14.Add(708);
			hashSet14.Add(17);
			hashSet14.Add(709);
			hashSet14.Add(18);
			hashSet14.Add(393);
			hashSet14.Add(3084);
			hashSet14.Add(3118);
			hashSet14.Add(3095);
			hashSet14.Add(3102);
			hashSet14.Add(3099);
			hashSet14.Add(3119);
			hashSet14.Add(3120);
			hashSet14.Add(3037);
			hashSet14.Add(3096);
			hashSet14.Add(4056);
			hashSet14.Add(407);
			hashSet14.Add(1923);
			hashSet14.Add(2215);
			hashSet14.Add(2217);
			hashSet14.Add(2216);
			hashSet14.Add(2214);
			hashSet14.Add(3061);
			hashSet14.Add(5126);
			hashSet14.Add(3624);
			hashSet14.Add(2373);
			hashSet14.Add(2374);
			hashSet14.Add(2375);
			hashSet14.Add(4881);
			hashSet14.Add(3721);
			hashSet14.Add(5064);
			hashSet14.Add(5139);
			hashSet14.Add(5140);
			hashSet14.Add(5141);
			hashSet14.Add(5142);
			hashSet14.Add(5143);
			hashSet14.Add(5144);
			hashSet14.Add(5145);
			hashSet14.Add(5146);
			hashSet14.Add(5010);
			hashSet14.Add(3090);
			hashSet14.Add(4409);
			hashSet14.Add(4767);
			hashSet14.Add(5309);
			hashSet14.Add(5323);
			hashSet14.Add(5355);
			hashSet14.Add(5113);
			hashSet14.Add(4346);
			Common.BankItems = hashSet14;
			HashSet<int> hashSet15 = new HashSet<int>();
			hashSet15.Add(ModContent.TileType<AsphaltPlatformTile>());
			Common.IgnoredTilesForExplosives = hashSet15;
			Common.IgnoredModsForExplosives = new HashSet<Mod>();
			HashSet<int> hashSet16 = new HashSet<int>();
			hashSet16.Add(78);
			hashSet16.Add(73);
			hashSet16.Add(74);
			hashSet16.Add(75);
			hashSet16.Add(76);
			hashSet16.Add(77);
			hashSet16.Add(79);
			hashSet16.Add(71);
			Common.FlaskBuffs = hashSet16;
			Common.ThoriumCoatings = new HashSet<int>();
			Common.kingSlimeDrops = new int[]
			{
				2430,
				256,
				257,
				258,
				2585,
				2610
			};
			Common.eyeOfCthulhuDrops = new int[]
			{
				1299
			};
			Common.eaterOfWorldsDrops = new int[]
			{
				994
			};
			Common.brainOfCthulhuDrops = new int[]
			{
				3060
			};
			Common.queenBeeDrops = new int[]
			{
				1121,
				1123,
				2888,
				1129,
				842,
				843,
				844,
				1132,
				1170,
				2502
			};
			Common.deerclopsDrops = new int[]
			{
				5098,
				5101,
				5113,
				5385,
				5117,
				5118,
				5119,
				5095
			};
			Common.skeletronDrops = new int[]
			{
				1273,
				1313,
				4993
			};
			Common.wallOfFleshDrops = new int[]
			{
				426,
				434,
				514,
				4912,
				490,
				491,
				489,
				2998
			};
			Common.queenSlimeDrops = new int[]
			{
				4982,
				4983,
				4984,
				4758,
				4981,
				4980
			};
			Common.planteraDrops = new int[]
			{
				758,
				1255,
				788,
				1178,
				1259,
				1155,
				3018,
				1157,
				3021,
				1305,
				1182
			};
			Common.golemDrops = new int[]
			{
				1294,
				1258,
				1122,
				899,
				1248,
				1295,
				1296,
				1297
			};
			Common.betsyDrops = new int[]
			{
				3883,
				3859,
				3858,
				3870,
				3827
			};
			Common.dukeFishronDrops = new int[]
			{
				2609,
				2623,
				2611,
				2622,
				2621,
				2624
			};
			Common.empressOfLightDrops = new int[]
			{
				4952,
				4923,
				4914,
				4953,
				4823,
				4715,
				5075
			};
			Common.moonLordDrops = new int[]
			{
				3063,
				3389,
				3065,
				1553,
				3930,
				3541,
				3570,
				3571,
				3569,
				4469
			};
		}

		// Token: 0x04000050 RID: 80
		public static readonly BindingFlags UniversalBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000051 RID: 81
		public static List<Hook> detours = new List<Hook>();

		// Token: 0x04000052 RID: 82
		public static readonly HashSet<int> TownSlimeIDs;

		// Token: 0x04000053 RID: 83
		public static readonly HashSet<int> LunarPillarIDs;

		// Token: 0x04000054 RID: 84
		public static readonly HashSet<int> CoinIDs;

		// Token: 0x04000055 RID: 85
		public static readonly HashSet<int> RedPotionBuffs;

		// Token: 0x04000056 RID: 86
		public static int PlatinumMaxStack;

		// Token: 0x04000057 RID: 87
		public const ulong CopperValue = 1UL;

		// Token: 0x04000058 RID: 88
		public const ulong SilverValue = 100UL;

		// Token: 0x04000059 RID: 89
		public const ulong GoldValue = 10000UL;

		// Token: 0x0400005A RID: 90
		public const ulong PlatinumValue = 1000000UL;

		// Token: 0x0400005B RID: 91
		public static HashSet<int> PermanentUpgrades;

		// Token: 0x0400005C RID: 92
		public static HashSet<int> PermanentMultiUseUpgrades;

		// Token: 0x0400005D RID: 93
		public static readonly int[] BossIDs;

		// Token: 0x0400005E RID: 94
		public static readonly HashSet<int> VanillaBossAndEventSummons;

		// Token: 0x0400005F RID: 95
		public static readonly HashSet<int> VanillaRightClickBossAndEventSummons;

		// Token: 0x04000060 RID: 96
		public static HashSet<int> ModdedBossAndEventSummons;

		// Token: 0x04000061 RID: 97
		public static HashSet<int> FargosBossAndEventSummons;

		// Token: 0x04000062 RID: 98
		public static readonly ushort[] EvilWallIDs;

		// Token: 0x04000063 RID: 99
		public static readonly ushort[] PureWallIDs;

		// Token: 0x04000064 RID: 100
		public static readonly HashSet<int> FallingBlocks;

		// Token: 0x04000065 RID: 101
		public static HashSet<int> Emblems;

		// Token: 0x04000066 RID: 102
		public static HashSet<int> PowerUpItems;

		// Token: 0x04000067 RID: 103
		public static readonly int[] VanillaFountains;

		// Token: 0x04000068 RID: 104
		public static readonly HashSet<int> MobileStorages;

		// Token: 0x04000069 RID: 105
		public static readonly bool[] NormalBunnies;

		// Token: 0x0400006A RID: 106
		public static readonly bool[] NormalSquirrels;

		// Token: 0x0400006B RID: 107
		public static readonly bool[] NormalButterflies;

		// Token: 0x0400006C RID: 108
		public static readonly bool[] NormalBirds;

		// Token: 0x0400006D RID: 109
		public static HashSet<int> Prefixes;

		// Token: 0x0400006E RID: 110
		public static HashSet<int> BankItems;

		// Token: 0x0400006F RID: 111
		public static HashSet<int> IgnoredTilesForExplosives;

		// Token: 0x04000070 RID: 112
		public static HashSet<Mod> IgnoredModsForExplosives;

		// Token: 0x04000071 RID: 113
		public static HashSet<int> FlaskBuffs;

		// Token: 0x04000072 RID: 114
		public static HashSet<int> ThoriumCoatings;

		// Token: 0x04000073 RID: 115
		public static readonly int[] kingSlimeDrops;

		// Token: 0x04000074 RID: 116
		public static readonly int[] eyeOfCthulhuDrops;

		// Token: 0x04000075 RID: 117
		public static readonly int[] eaterOfWorldsDrops;

		// Token: 0x04000076 RID: 118
		public static readonly int[] brainOfCthulhuDrops;

		// Token: 0x04000077 RID: 119
		public static readonly int[] queenBeeDrops;

		// Token: 0x04000078 RID: 120
		public static readonly int[] deerclopsDrops;

		// Token: 0x04000079 RID: 121
		public static readonly int[] skeletronDrops;

		// Token: 0x0400007A RID: 122
		public static readonly int[] wallOfFleshDrops;

		// Token: 0x0400007B RID: 123
		public static readonly int[] queenSlimeDrops;

		// Token: 0x0400007C RID: 124
		public static readonly int[] planteraDrops;

		// Token: 0x0400007D RID: 125
		public static readonly int[] golemDrops;

		// Token: 0x0400007E RID: 126
		public static readonly int[] betsyDrops;

		// Token: 0x0400007F RID: 127
		public static readonly int[] dukeFishronDrops;

		// Token: 0x04000080 RID: 128
		public static readonly int[] empressOfLightDrops;

		// Token: 0x04000081 RID: 129
		public static readonly int[] moonLordDrops;

		// Token: 0x04000082 RID: 130
		public static int AnyPirateBanner;

		// Token: 0x04000083 RID: 131
		public static int AnyArmoredBonesBanner;

		// Token: 0x04000084 RID: 132
		public static int AnySlimeBanner;

		// Token: 0x04000085 RID: 133
		public static int AnyBatBanner;

		// Token: 0x04000086 RID: 134
		public static int AnyHallowBanner;

		// Token: 0x04000087 RID: 135
		public static int AnyCorruptionBanner;

		// Token: 0x04000088 RID: 136
		public static int AnyCrimsonBanner;

		// Token: 0x04000089 RID: 137
		public static int AnyJungleBanner;

		// Token: 0x0400008A RID: 138
		public static int AnySnowBanner;

		// Token: 0x0400008B RID: 139
		public static int AnyDesertBanner;

		// Token: 0x0400008C RID: 140
		public static int AnyUnderworldBanner;

		// Token: 0x02000525 RID: 1317
		public enum PlacedPlatformStyles
		{
			// Token: 0x04000AB0 RID: 2736
			Wood,
			// Token: 0x04000AB1 RID: 2737
			Ebonwood,
			// Token: 0x04000AB2 RID: 2738
			RichMahogany,
			// Token: 0x04000AB3 RID: 2739
			Pearlwood,
			// Token: 0x04000AB4 RID: 2740
			Bone,
			// Token: 0x04000AB5 RID: 2741
			Shadewood,
			// Token: 0x04000AB6 RID: 2742
			BlueBrick,
			// Token: 0x04000AB7 RID: 2743
			PinkBrick,
			// Token: 0x04000AB8 RID: 2744
			GreenBrick,
			// Token: 0x04000AB9 RID: 2745
			MetalShelf,
			// Token: 0x04000ABA RID: 2746
			BrassShelf,
			// Token: 0x04000ABB RID: 2747
			WoodShelf,
			// Token: 0x04000ABC RID: 2748
			DungeonShelf,
			// Token: 0x04000ABD RID: 2749
			Obsidian,
			// Token: 0x04000ABE RID: 2750
			Glass,
			// Token: 0x04000ABF RID: 2751
			Pumpkin,
			// Token: 0x04000AC0 RID: 2752
			SpookyWood,
			// Token: 0x04000AC1 RID: 2753
			PalmWood,
			// Token: 0x04000AC2 RID: 2754
			Mushroom,
			// Token: 0x04000AC3 RID: 2755
			BorealWood,
			// Token: 0x04000AC4 RID: 2756
			Slime,
			// Token: 0x04000AC5 RID: 2757
			Steampunk,
			// Token: 0x04000AC6 RID: 2758
			Skyware,
			// Token: 0x04000AC7 RID: 2759
			LivingWood,
			// Token: 0x04000AC8 RID: 2760
			Honey,
			// Token: 0x04000AC9 RID: 2761
			Cactus,
			// Token: 0x04000ACA RID: 2762
			Martian,
			// Token: 0x04000ACB RID: 2763
			Meteorite,
			// Token: 0x04000ACC RID: 2764
			Granite,
			// Token: 0x04000ACD RID: 2765
			Marble,
			// Token: 0x04000ACE RID: 2766
			Crystal,
			// Token: 0x04000ACF RID: 2767
			Golden,
			// Token: 0x04000AD0 RID: 2768
			DynastyWood,
			// Token: 0x04000AD1 RID: 2769
			Lihzahrd,
			// Token: 0x04000AD2 RID: 2770
			Flesh,
			// Token: 0x04000AD3 RID: 2771
			Frozen,
			// Token: 0x04000AD4 RID: 2772
			Spider,
			// Token: 0x04000AD5 RID: 2773
			Lesion,
			// Token: 0x04000AD6 RID: 2774
			Solar,
			// Token: 0x04000AD7 RID: 2775
			Vortex,
			// Token: 0x04000AD8 RID: 2776
			Nebula,
			// Token: 0x04000AD9 RID: 2777
			Stardust,
			// Token: 0x04000ADA RID: 2778
			Sandstone,
			// Token: 0x04000ADB RID: 2779
			Stone,
			// Token: 0x04000ADC RID: 2780
			Bamboo,
			// Token: 0x04000ADD RID: 2781
			Reef,
			// Token: 0x04000ADE RID: 2782
			Balloon,
			// Token: 0x04000ADF RID: 2783
			AshWood,
			// Token: 0x04000AE0 RID: 2784
			Echo
		}

		// Token: 0x02000526 RID: 1318
		public enum PlacedTableStyles1
		{
			// Token: 0x04000AE2 RID: 2786
			Wooden,
			// Token: 0x04000AE3 RID: 2787
			Ebonwood,
			// Token: 0x04000AE4 RID: 2788
			RichMahogany,
			// Token: 0x04000AE5 RID: 2789
			Pearlwood,
			// Token: 0x04000AE6 RID: 2790
			Bone,
			// Token: 0x04000AE7 RID: 2791
			Flesh,
			// Token: 0x04000AE8 RID: 2792
			LivingWood,
			// Token: 0x04000AE9 RID: 2793
			Skyware,
			// Token: 0x04000AEA RID: 2794
			Shadewood,
			// Token: 0x04000AEB RID: 2795
			Lihzahrd,
			// Token: 0x04000AEC RID: 2796
			BlueDungeon,
			// Token: 0x04000AED RID: 2797
			GreenDungeon,
			// Token: 0x04000AEE RID: 2798
			PinkDungeon,
			// Token: 0x04000AEF RID: 2799
			Obsidian,
			// Token: 0x04000AF0 RID: 2800
			Gothic,
			// Token: 0x04000AF1 RID: 2801
			Glass,
			// Token: 0x04000AF2 RID: 2802
			Banquet,
			// Token: 0x04000AF3 RID: 2803
			Bar,
			// Token: 0x04000AF4 RID: 2804
			Golden,
			// Token: 0x04000AF5 RID: 2805
			Honey,
			// Token: 0x04000AF6 RID: 2806
			Steampunk,
			// Token: 0x04000AF7 RID: 2807
			Pumpkin,
			// Token: 0x04000AF8 RID: 2808
			Spooky,
			// Token: 0x04000AF9 RID: 2809
			Pine,
			// Token: 0x04000AFA RID: 2810
			Frozen,
			// Token: 0x04000AFB RID: 2811
			Dynasty,
			// Token: 0x04000AFC RID: 2812
			PalmWood,
			// Token: 0x04000AFD RID: 2813
			Mushroom,
			// Token: 0x04000AFE RID: 2814
			BorealWood,
			// Token: 0x04000AFF RID: 2815
			Slime,
			// Token: 0x04000B00 RID: 2816
			Cactus,
			// Token: 0x04000B01 RID: 2817
			Martian,
			// Token: 0x04000B02 RID: 2818
			Meteorite,
			// Token: 0x04000B03 RID: 2819
			Granite,
			// Token: 0x04000B04 RID: 2820
			Marble
		}

		// Token: 0x02000527 RID: 1319
		public enum PlacedTableStyles2
		{
			// Token: 0x04000B06 RID: 2822
			Crystal,
			// Token: 0x04000B07 RID: 2823
			Spider,
			// Token: 0x04000B08 RID: 2824
			Lesion,
			// Token: 0x04000B09 RID: 2825
			Solar,
			// Token: 0x04000B0A RID: 2826
			Vortex,
			// Token: 0x04000B0B RID: 2827
			Nebula,
			// Token: 0x04000B0C RID: 2828
			Stardust,
			// Token: 0x04000B0D RID: 2829
			Sandstone,
			// Token: 0x04000B0E RID: 2830
			Bamboo,
			// Token: 0x04000B0F RID: 2831
			Reef,
			// Token: 0x04000B10 RID: 2832
			Balloon,
			// Token: 0x04000B11 RID: 2833
			AshWood
		}

		// Token: 0x02000528 RID: 1320
		public enum PlacedChairStyles
		{
			// Token: 0x04000B13 RID: 2835
			Wooden,
			// Token: 0x04000B14 RID: 2836
			Tiolet,
			// Token: 0x04000B15 RID: 2837
			Ebonwood,
			// Token: 0x04000B16 RID: 2838
			RichMahogany,
			// Token: 0x04000B17 RID: 2839
			Pearlwood,
			// Token: 0x04000B18 RID: 2840
			LivingWood,
			// Token: 0x04000B19 RID: 2841
			Cactus,
			// Token: 0x04000B1A RID: 2842
			Bone,
			// Token: 0x04000B1B RID: 2843
			Flesh,
			// Token: 0x04000B1C RID: 2844
			Mushroom,
			// Token: 0x04000B1D RID: 2845
			Skyware,
			// Token: 0x04000B1E RID: 2846
			Shadewood,
			// Token: 0x04000B1F RID: 2847
			Lihzahrd,
			// Token: 0x04000B20 RID: 2848
			BlueDungeon,
			// Token: 0x04000B21 RID: 2849
			GreenDungeon,
			// Token: 0x04000B22 RID: 2850
			PinkDungeon,
			// Token: 0x04000B23 RID: 2851
			Obsidian,
			// Token: 0x04000B24 RID: 2852
			Gothic,
			// Token: 0x04000B25 RID: 2853
			Glass,
			// Token: 0x04000B26 RID: 2854
			Golden,
			// Token: 0x04000B27 RID: 2855
			GoldenToilet,
			// Token: 0x04000B28 RID: 2856
			BarStool,
			// Token: 0x04000B29 RID: 2857
			Honey,
			// Token: 0x04000B2A RID: 2858
			Steampunk,
			// Token: 0x04000B2B RID: 2859
			Pumpkin,
			// Token: 0x04000B2C RID: 2860
			Spooky,
			// Token: 0x04000B2D RID: 2861
			Pine,
			// Token: 0x04000B2E RID: 2862
			Dynasty,
			// Token: 0x04000B2F RID: 2863
			Frozen,
			// Token: 0x04000B30 RID: 2864
			PalmWood,
			// Token: 0x04000B31 RID: 2865
			BorealWood,
			// Token: 0x04000B32 RID: 2866
			Slime,
			// Token: 0x04000B33 RID: 2867
			Martian,
			// Token: 0x04000B34 RID: 2868
			Meteorite,
			// Token: 0x04000B35 RID: 2869
			Granite,
			// Token: 0x04000B36 RID: 2870
			Marble,
			// Token: 0x04000B37 RID: 2871
			Crystal,
			// Token: 0x04000B38 RID: 2872
			Spider,
			// Token: 0x04000B39 RID: 2873
			Lesion,
			// Token: 0x04000B3A RID: 2874
			Solar,
			// Token: 0x04000B3B RID: 2875
			Vortex,
			// Token: 0x04000B3C RID: 2876
			Nebula,
			// Token: 0x04000B3D RID: 2877
			Stardust,
			// Token: 0x04000B3E RID: 2878
			Sandstone,
			// Token: 0x04000B3F RID: 2879
			Bamboo
		}

		// Token: 0x02000529 RID: 1321
		public enum PlacedDoorStyles
		{
			// Token: 0x04000B41 RID: 2881
			Wooden,
			// Token: 0x04000B42 RID: 2882
			Ebonwood,
			// Token: 0x04000B43 RID: 2883
			RichMahogany,
			// Token: 0x04000B44 RID: 2884
			Pearlwood,
			// Token: 0x04000B45 RID: 2885
			Cactus,
			// Token: 0x04000B46 RID: 2886
			Flesh,
			// Token: 0x04000B47 RID: 2887
			Mushroom,
			// Token: 0x04000B48 RID: 2888
			LivingWood,
			// Token: 0x04000B49 RID: 2889
			Bone,
			// Token: 0x04000B4A RID: 2890
			Skyware,
			// Token: 0x04000B4B RID: 2891
			Shadewood,
			// Token: 0x04000B4C RID: 2892
			LockedLihzahrd,
			// Token: 0x04000B4D RID: 2893
			Lihzahrd,
			// Token: 0x04000B4E RID: 2894
			Dungeon,
			// Token: 0x04000B4F RID: 2895
			Lead,
			// Token: 0x04000B50 RID: 2896
			Iron,
			// Token: 0x04000B51 RID: 2897
			BlueDungeon,
			// Token: 0x04000B52 RID: 2898
			GreenDungeon,
			// Token: 0x04000B53 RID: 2899
			PinkDungeon,
			// Token: 0x04000B54 RID: 2900
			Obsidian,
			// Token: 0x04000B55 RID: 2901
			Glass,
			// Token: 0x04000B56 RID: 2902
			Golden,
			// Token: 0x04000B57 RID: 2903
			Honey,
			// Token: 0x04000B58 RID: 2904
			Steampunk,
			// Token: 0x04000B59 RID: 2905
			Pumpkin,
			// Token: 0x04000B5A RID: 2906
			Spooky,
			// Token: 0x04000B5B RID: 2907
			Pine,
			// Token: 0x04000B5C RID: 2908
			Frozen,
			// Token: 0x04000B5D RID: 2909
			Dynasty,
			// Token: 0x04000B5E RID: 2910
			PalmWood,
			// Token: 0x04000B5F RID: 2911
			BorealWood,
			// Token: 0x04000B60 RID: 2912
			Slime,
			// Token: 0x04000B61 RID: 2913
			Martian,
			// Token: 0x04000B62 RID: 2914
			Meteorite,
			// Token: 0x04000B63 RID: 2915
			Granite,
			// Token: 0x04000B64 RID: 2916
			Marble,
			// Token: 0x04000B65 RID: 2917
			Crystal,
			// Token: 0x04000B66 RID: 2918
			Spider,
			// Token: 0x04000B67 RID: 2919
			Lesion,
			// Token: 0x04000B68 RID: 2920
			Solar,
			// Token: 0x04000B69 RID: 2921
			Vortex,
			// Token: 0x04000B6A RID: 2922
			Nebula,
			// Token: 0x04000B6B RID: 2923
			Stardust,
			// Token: 0x04000B6C RID: 2924
			Sandstone,
			// Token: 0x04000B6D RID: 2925
			Stone,
			// Token: 0x04000B6E RID: 2926
			Bamboo
		}

		// Token: 0x0200052A RID: 1322
		public enum PlacedTorchStyles
		{
			// Token: 0x04000B70 RID: 2928
			Torch,
			// Token: 0x04000B71 RID: 2929
			BlueTorch,
			// Token: 0x04000B72 RID: 2930
			RedTorch,
			// Token: 0x04000B73 RID: 2931
			GreenTorch,
			// Token: 0x04000B74 RID: 2932
			PurpleTorch,
			// Token: 0x04000B75 RID: 2933
			WhiteTorch,
			// Token: 0x04000B76 RID: 2934
			YellowTorch,
			// Token: 0x04000B77 RID: 2935
			DemonTorch,
			// Token: 0x04000B78 RID: 2936
			CursedTorch,
			// Token: 0x04000B79 RID: 2937
			IceTorch,
			// Token: 0x04000B7A RID: 2938
			OrangeTorch,
			// Token: 0x04000B7B RID: 2939
			IchorTorch,
			// Token: 0x04000B7C RID: 2940
			UltrabrightTorch,
			// Token: 0x04000B7D RID: 2941
			BoneTorch,
			// Token: 0x04000B7E RID: 2942
			RainbowTorch,
			// Token: 0x04000B7F RID: 2943
			PinkTorch,
			// Token: 0x04000B80 RID: 2944
			DesertTorch,
			// Token: 0x04000B81 RID: 2945
			CoralTorch,
			// Token: 0x04000B82 RID: 2946
			CorruptTorch,
			// Token: 0x04000B83 RID: 2947
			CrimsonTorch,
			// Token: 0x04000B84 RID: 2948
			HallowedTorch,
			// Token: 0x04000B85 RID: 2949
			JungleTorch,
			// Token: 0x04000B86 RID: 2950
			MushroomTorch,
			// Token: 0x04000B87 RID: 2951
			AetherTorch
		}

		// Token: 0x0200052B RID: 1323
		public enum AlchemyHerbStyles
		{
			// Token: 0x04000B89 RID: 2953
			Daybloom,
			// Token: 0x04000B8A RID: 2954
			Moonglow,
			// Token: 0x04000B8B RID: 2955
			Blinkroot,
			// Token: 0x04000B8C RID: 2956
			Deathweed,
			// Token: 0x04000B8D RID: 2957
			Waterleaf,
			// Token: 0x04000B8E RID: 2958
			Fireblossom,
			// Token: 0x04000B8F RID: 2959
			Shiverthorn
		}

		// Token: 0x0200052C RID: 1324
		public interface IRightClickOverrideWhenHeld
		{
			// Token: 0x06001823 RID: 6179
			bool RightClickOverrideWhileHeld(ref Item heldItem, Item[] inv, int context, int slot, Player player, QoLCPlayer qPlayer);
		}
	}
}
