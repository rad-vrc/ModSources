using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x02000236 RID: 566
	public class DownedFlags : GlobalNPC
	{
		// Token: 0x06000D96 RID: 3478 RVA: 0x000691CC File Offset: 0x000673CC
		public override bool PreAI(NPC npc)
		{
			if (npc.type == Common.GetModNPC(ModConditions.starlightRiverMod, "Glassweaver") && npc.life <= 1)
			{
				NPC.SetEventFlagCleared(ref ModConditions.downedGlassweaver, -1);
			}
			return base.PreAI(npc);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00069200 File Offset: 0x00067400
		public override void OnKill(NPC npc)
		{
			if (npc.type == 618)
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[0], -1);
			}
			if (npc.type == 395 || npc.type == 392)
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[1], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "SlayerofEvil"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[5], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "SATLA001"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[6], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "DrFetusThirdPhase"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[7], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "MechanicalSlime"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[8], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "PoliticianSlime"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[9], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "FlagCarrier"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[10], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "LavalGolem"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[11], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "NecromancerDummy"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[12], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "BunnyZepplin"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[13], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "Okiku"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[14], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "StormTinkererH"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[15], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "DemonIsaac"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[16], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "AncientGuardian"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[17], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "HeroicSlime"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[18], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "HolographicSlime"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[19], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "SecurityBot"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[20], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "UndeadChef"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[21], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.afkpetsMod, "IceGuardian"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[22], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.assortedCrazyThingsMod, "HarvesterBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[23], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.awfulGarbageMod, "TreeToad"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[24], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.awfulGarbageMod, "SeseKitsugai"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[25], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.awfulGarbageMod, "EyeOfTheStorm"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[26], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.awfulGarbageMod, "FrigidiusHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[27], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.blocksCoreBossMod, "CoreBoss") || npc.type == Common.GetModNPC(ModConditions.blocksCoreBossMod, "CoreBossCrim"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[28], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.calamityCommunityRemixMod, "WulfwyrmHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[32], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.calamityEntropyMod, "Luminaris"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[33], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.calamityEntropyMod, "TheProphet"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[34], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.calamityEntropyMod, "NihilityActeriophage"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[35], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.calamityEntropyMod, "CruiserHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[36], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.catalystMod, "Astrageldon"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[37], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.clamityAddonMod, "ClamitasBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[38], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.clamityAddonMod, "PyrogenBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[39], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.clamityAddonMod, "WallOfBronze"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[40], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.consolariaMod, "Lepus"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[41], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.consolariaMod, "TurkortheUngrateful"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[42], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.consolariaMod, "Ocram"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[43], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.coraliteMod, "Rediancie"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[44], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.coraliteMod, "BabyIceDragon"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[45], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.coraliteMod, "SlimeEmperor"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[46], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.coraliteMod, "Bloodiancie"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[47], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.coraliteMod, "ThunderveinDragon"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[48], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.coraliteMod, "NightmarePlantera"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[49], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.depthsMod, "ChasmeHeart"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[50], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.echoesOfTheAncientsMod, "Galahis"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[58], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.echoesOfTheAncientsMod, "AquaWormHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[59], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.echoesOfTheAncientsMod, "PumpkinWormHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[60], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.edorbisMod, "BlightKing"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[61], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.edorbisMod, "TheGardener"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[62], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.edorbisMod, "GlaciationHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[63], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.edorbisMod, "HandofCthulhu"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[64], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.edorbisMod, "CursedLord"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[65], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.exaltMod, "Effulgence"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[66], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.exaltMod, "IceLich"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[67], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.excelsiorMod, "Niflheim"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[68], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.excelsiorMod, "StellarStarship"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[69], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.exxoAvalonOriginsMod, "BacteriumPrime"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[70], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.exxoAvalonOriginsMod, "DesertBeak"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[71], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.exxoAvalonOriginsMod, "KingSting"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[72], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.exxoAvalonOriginsMod, "Mechasting"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[73], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.exxoAvalonOriginsMod, "Phantasm"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[74], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.fargosSoulsMod, "TrojanSquirrel"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[75], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.fargosSoulsMod, "CursedCoffin"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[76], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.fargosSoulsMod, "DeviBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[77], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.fargosSoulsMod, "BanishedBaron"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[79], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.fargosSoulsMod, "LifeChallenger"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[78], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.fargosSoulsMod, "CosmosChampion"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[80], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.fargosSoulsMod, "AbomBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[81], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.fargosSoulsMod, "MutantBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[82], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.fracturesOfPenumbraMod, "AlphaFrostjawHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[83], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.fracturesOfPenumbraMod, "SanguineElemental"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[84], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gameTerrariaMod, "Lad"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[85], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gameTerrariaMod, "Hornlitz"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[86], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gameTerrariaMod, "SnowDonCore"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[87], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gameTerrariaMod, "Stoffie"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[88], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "LilyWhite"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[89], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "Rumia"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[90], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "EternityLarva"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[91], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "Nazrin"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[92], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "HinaKagiyama"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[93], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "Sekibanki"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[94], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "Seiran"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[95], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "NitoriKawashiro"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[96], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "MedicineMelancholy"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[97], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "Cirno"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[98], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "MinamitsuMurasa"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[99], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "AliceMargatroid"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[100], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "SakuyaIzayoi"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[101], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "SeijaKijin"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[102], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "MayumiJoutouguu"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[103], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "ToyosatomimiNoMiko"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[104], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "KaguyaHouraisan"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[105], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "UtsuhoReiuji"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[106], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "TenshiHinanawi"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[107], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gensokyoMod, "Kisume"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[108], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gerdsLabMod, "Trerios"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[109], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gerdsLabMod, "MagmaEye"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[110], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gerdsLabMod, "Jack"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[111], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.gerdsLabMod, "Acheron"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[112], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "MarquisMoonsquid"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[113], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "PriestessRod"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[114], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "Diver"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[115], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "TheMotherbrain"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[116], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "WallofShadow"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[117], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "SlimeGod"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[118], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "TheOverwatcher"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[119], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "TheLifebringerHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[120], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "TheMaterealizer"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[121], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "ScarabBelief"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[122], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "WorldsEndEverlastingFallingWhale"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[123], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "TheSon"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[124], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "Trial_Cave"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[125], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "Trial_Corruption"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[126], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "Trial_Crimson"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[127], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "Trial_Desert"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[128], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "Trial_Forest"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[129], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "Trial_Hallow"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[130], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "Trial_Jungle"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[131], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "Trial_Pure"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[132], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "Trial_Snow"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[133], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.homewardJourneyMod, "Trial_Underworld"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[134], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.huntOfTheOldGodMod, "Goozma"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[135], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.infernumMod, "BereftVassal"))
			{
				SubworldModConditions.downedBereftVassal = true;
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[136], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "StarrVeriplant"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[137], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "CommanderGintzia"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[138], -1);
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[155], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "SunStalker"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[139], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "Jack"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[140], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "DaedusR"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[141], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "DreadMire"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[142], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "SingularityFragment"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[143], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "VerliaB"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[144], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "Gothiviab"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[145], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "Sylia"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[146], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "Fenix"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[147], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "BlazingSerpentHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[148], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "Cogwork"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[149], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "WaterCogwork"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[150], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "WaterJellyfish"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[151], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "Sparn"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[152], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "PandorasFlamebox"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[153], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.lunarVeilMod, "STARBOMBER"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[154], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.martainsOrderMod, "Britzz"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[156], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.martainsOrderMod, "Alchemist"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[157], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.martainsOrderMod, "CarnagePillar"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[158], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.martainsOrderMod, "VoidDiggerHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[159], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.martainsOrderMod, "PrinceSlime"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[160], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.martainsOrderMod, "Evocornator") && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.martainsOrderMod, "Retinator")) && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.martainsOrderMod, "Spazmator")))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[161], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.martainsOrderMod, "Retinator") && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.martainsOrderMod, "Evocornator")) && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.martainsOrderMod, "Spazmator")))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[161], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.martainsOrderMod, "Spazmator") && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.martainsOrderMod, "Evocornator")) && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.martainsOrderMod, "Retinator")))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[161], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.martainsOrderMod, "MechPlantera"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[162], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.mechReworkMod, "Mechclops"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[163], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.mechReworkMod, "TheTerminator"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[164], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.mechReworkMod, "Caretaker"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[165], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.mechReworkMod, "SiegeEngine"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[166], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.medialRiftMod, "SuperVoltaicMotherSlime"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[167], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.metroidMod, "Torizo"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[168], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.metroidMod, "Serris_Head"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[169], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.metroidMod, "Kraid_Head"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[170], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.metroidMod, "Phantoon"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[171], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.metroidMod, "OmegaPirate"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[172], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.metroidMod, "Nightmare"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[173], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.metroidMod, "GoldenTorizo"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[174], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.ophioidMod, "OphiopedeHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[175], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.ophioidMod, "Ophiocoon"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[176], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.ophioidMod, "Ophiofly"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[177], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.polaritiesMod, "StormCloudfish"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[178], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.polaritiesMod, "StarConstruct"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[179], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.polaritiesMod, "Gigabat"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[180], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.polaritiesMod, "SunPixie"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[181], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.polaritiesMod, "Esophage"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[182], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.polaritiesMod, "ConvectiveWanderer"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[183], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.projectZeroMod, "ForestGuardian"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[184], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.projectZeroMod, "CryoGuardian"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[185], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.projectZeroMod, "PrimordialWormHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[186], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.projectZeroMod, "HellGuardian"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[187], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.projectZeroMod, "Void"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[188], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.projectZeroMod, "ArmagemHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[189], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.qwertyMod, "PolarBear"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[190], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.qwertyMod, "FortressBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[191], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.qwertyMod, "AncientMachine"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[192], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.qwertyMod, "CloakedDarkBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[193], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.qwertyMod, "Hydra"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[194], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.qwertyMod, "Imperious"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[195], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.qwertyMod, "RuneGhost"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[196], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.qwertyMod, "InvaderBattleship"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[197], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.qwertyMod, "InvaderNoehtnap"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[198], -1);
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[202], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.qwertyMod, "OLORDv2"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[199], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.qwertyMod, "TheGreatTyrannosaurus"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[200], -1);
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[201], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "Thorn"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[203], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "Erhan"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[204], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "Keeper"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[205], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "SoI"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[206], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "KS3"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[207], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "OmegaCleaver"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[208], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "Gigapora"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[209], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "OO"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[210], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "PZ"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[211], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "Akka") && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.redemptionMod, "Ukko")))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[212], -1);
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[214], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "Ukko") && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.redemptionMod, "Akka")))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[213], -1);
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[214], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "Nebuleus"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[215], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "Nebuleus2"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[215], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "FowlEmperor"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[216], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "Cockatrice"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[217], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "Basan"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[218], -1);
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[227], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "SkullDigger"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[219], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "EaglecrestGolem"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[220], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "Calavia"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[221], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "JanitorBot"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[222], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "IrradiatedBehemoth"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[223], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "Blisterface"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[224], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "ProtectorVolt"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[225], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.redemptionMod, "MACEProject"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[226], -1);
			}
			List<int> list = new List<int>();
			list.Add(Common.GetModNPC(ModConditions.redemptionMod, "CorpseWalkerPriest"));
			list.Add(Common.GetModNPC(ModConditions.redemptionMod, "DancingSkeleton"));
			list.Add(Common.GetModNPC(ModConditions.redemptionMod, "EpidotrianSkeleton"));
			list.Add(Common.GetModNPC(ModConditions.redemptionMod, "JollyMadman"));
			list.Add(Common.GetModNPC(ModConditions.redemptionMod, "RaveyardSkeleton"));
			list.Add(Common.GetModNPC(ModConditions.redemptionMod, "SkeletonAssassin"));
			list.Add(Common.GetModNPC(ModConditions.redemptionMod, "SkeletonDuelist"));
			list.Add(Common.GetModNPC(ModConditions.redemptionMod, "SkeletonFlagbearer"));
			list.Add(Common.GetModNPC(ModConditions.redemptionMod, "SkeletonNoble"));
			list.Add(Common.GetModNPC(ModConditions.redemptionMod, "SkeletonWanderer"));
			list.Add(Common.GetModNPC(ModConditions.redemptionMod, "SkeletonWarden"));
			if (list.Contains(npc.type))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[228], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "Glowmoth"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[230], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "PutridPinkyPhase2"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[229], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "PharaohsCurse"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[231], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "TheAdvisorHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[232], -1);
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[248], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "Polaris") || npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "NewPolaris"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[233], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "Lux"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[234], -1);
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[251], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "SubspaceSerpentHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[235], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "NatureConstruct"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[236], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "EarthenConstruct"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[237], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "PermafrostConstruct"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[238], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "TidalConstruct"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[239], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "OtherworldlyConstructHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[240], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "OtherworldlyConstructHead2"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[240], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "EvilConstruct"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[241], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "InfernoConstruct"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[242], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "ChaosConstruct"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[243], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "NatureSpirit"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[244], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "EarthenSpirit"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[245], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "PermafrostSpirit"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[246], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "TidalSpirit"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[247], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "EvilSpirit"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[249], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.secretsOfTheShadowsMod, "InfernoSpirit"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[250], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.shadowsOfAbaddonMod, "Decree"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[252], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.shadowsOfAbaddonMod, "Ralnek") && Condition.InClassicMode.IsMet())
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[253], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.shadowsOfAbaddonMod, "RalnekPhase3") && ModConditions.expertOrMaster.IsMet())
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[253], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.shadowsOfAbaddonMod, "ZombiePigmanBrute"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[254], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.shadowsOfAbaddonMod, "Jensen"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[255], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.shadowsOfAbaddonMod, "Araneas"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[256], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.shadowsOfAbaddonMod, "Raynare"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[257], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.shadowsOfAbaddonMod, "Primordia2"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[258], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.shadowsOfAbaddonMod, "Abaddon"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[259], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.shadowsOfAbaddonMod, "AraghurHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[260], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.shadowsOfAbaddonMod, "Novaniel"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[261], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.shadowsOfAbaddonMod, "ErazorBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[262], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.shadowsOfAbaddonMod, "Nihilus2"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[263], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.sloomeMod, "ExoSlimeGod"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[264], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spiritMod, "Scarabeus"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[265], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spiritMod, "MoonWizard"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[266], -1);
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[273], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spiritMod, "ReachBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[267], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spiritMod, "AncientFlyer"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[268], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spiritMod, "SteamRaiderHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[269], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spiritMod, "Infernon"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[270], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spiritMod, "Dusking"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[271], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spiritMod, "Atlas"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[272], -1);
			}
			List<int> list2 = new List<int>();
			list2.Add(Common.GetModNPC(ModConditions.spiritMod, "MoonlightPreserver"));
			list2.Add(Common.GetModNPC(ModConditions.spiritMod, "ExplodingMoonjelly"));
			list2.Add(Common.GetModNPC(ModConditions.spiritMod, "MoonjellyGiant"));
			if (list2.Contains(npc.type))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[273], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spiritMod, "Rylheian"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[274], -1);
			}
			List<int> list3 = new List<int>();
			list3.Add(Common.GetModNPC(ModConditions.spiritMod, "Bloomshroom"));
			list3.Add(Common.GetModNPC(ModConditions.spiritMod, "Glitterfly"));
			list3.Add(Common.GetModNPC(ModConditions.spiritMod, "GlowToad"));
			list3.Add(Common.GetModNPC(ModConditions.spiritMod, "Lumantis"));
			list3.Add(Common.GetModNPC(ModConditions.spiritMod, "LunarSlime"));
			list3.Add(Common.GetModNPC(ModConditions.spiritMod, "MadHatter"));
			if (list3.Contains(npc.type))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[275], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spookyMod, "RotGourd"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[277], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spookyMod, "SpookySpirit"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[276], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spookyMod, "Moco"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[278], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spookyMod, "DaffodilEye"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[279], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spookyMod, "OrroHead") && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.spookyMod, "BoroHead")))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[280], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spookyMod, "BoroHead") && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.spookyMod, "OrroHead")))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[280], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.spookyMod, "BigBone"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[281], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.starlightRiverMod, "SquidBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[282], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.starlightRiverMod, "VitricBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[283], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.stormsAdditionsMod, "AridBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[294], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.stormsAdditionsMod, "StormBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[295], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.stormsAdditionsMod, "TheUltimateBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[296], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.supernovaMod, "HarbingerOfAnnihilation"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[297], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.supernovaMod, "FlyingTerror"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[298], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.supernovaMod, "StoneMantaRay"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[299], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.supernovaMod, "Bloodweaver"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[300], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.terrorbornMod, "InfectedIncarnate"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[301], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.terrorbornMod, "TidalTitan"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[302], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.terrorbornMod, "Dunestock"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[303], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.terrorbornMod, "Shadowcrawler"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[305], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.terrorbornMod, "HexedConstructor"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[304], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.terrorbornMod, "PrototypeI"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[306], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.traeMod, "GraniteOvergrowthNPC"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[307], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.traeMod, "BeholderNPC"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[308], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.uhtricMod, "Dredger"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[309], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.uhtricMod, "CharcoolSnowman"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[310], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.uhtricMod, "CosmicMenace"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[311], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.universeOfSwordsMod, "SwordBossNPC"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[312], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.valhallaMod, "Emperor"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[314], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.valhallaMod, "PirateSquid"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[313], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.vitalityMod, "StormCloudBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[315], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.vitalityMod, "GrandAntlionBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[316], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.vitalityMod, "GemstoneElementalBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[317], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.vitalityMod, "MoonlightDragonflyBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[318], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.vitalityMod, "DreadnaughtBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[319], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.vitalityMod, "MosquitoMonarchBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[320], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.vitalityMod, "AnarchulesBeetleBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[321], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.vitalityMod, "ChaosbringerBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[322], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.vitalityMod, "PaladinSpiritBoss"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[323], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.wayfairContentMod, "Lifebloom"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[324], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.zylonMod, "Dirtball"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[327], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.zylonMod, "MetelordHead"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[328], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.zylonMod, "Adeneb"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[329], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.zylonMod, "EldritchJellyfish"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[330], -1);
			}
			if (npc.type == Common.GetModNPC(ModConditions.zylonMod, "SaburRex"))
			{
				NPC.SetEventFlagCleared(ref ModConditions.DownedBoss[331], -1);
			}
		}
	}
}
