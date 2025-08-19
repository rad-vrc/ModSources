using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ItemChanges
{
	// Token: 0x0200025F RID: 607
	public class ItemTransmutation : GlobalItem
	{
		// Token: 0x06000E20 RID: 3616 RVA: 0x000711A4 File Offset: 0x0006F3A4
		public override void SetStaticDefaults()
		{
			if (QoLCompendium.mainConfig.BossItemTransmutation)
			{
				Common.TransmuteItems(new int[]
				{
					256,
					257,
					258
				});
				Common.TransmuteItems(new int[]
				{
					1123,
					2888,
					1121
				});
				Common.TransmuteItems(new int[]
				{
					842,
					843,
					844
				});
				Common.TransmuteItems(new int[]
				{
					5095,
					5117,
					5118,
					5119
				});
				Common.TransmuteItems(new int[]
				{
					426,
					434,
					514,
					4912
				});
				Common.TransmuteItems(new int[]
				{
					4982,
					4983,
					4984
				});
				Common.TransmuteItems(new int[]
				{
					3018,
					1259,
					758,
					1255,
					788,
					1178,
					1155
				});
				Common.TransmuteItems(new int[]
				{
					1297,
					1122,
					1258,
					1295,
					1296,
					899,
					1248
				});
				Common.TransmuteItems(new int[]
				{
					2611,
					2624,
					2622,
					2623,
					2621
				});
				Common.TransmuteItems(new int[]
				{
					4923,
					4953,
					4952,
					4914
				});
				Common.TransmuteItems(new int[]
				{
					3389,
					3063,
					3065,
					3930,
					1553,
					3541,
					3570,
					3571,
					3569
				});
				Common.TransmuteItems(new int[]
				{
					3823,
					3835,
					3836,
					3854,
					3852
				});
				Common.TransmuteItems(new int[]
				{
					3827,
					3858,
					3859,
					3870
				});
				Common.TransmuteItems(new int[]
				{
					1829,
					1831,
					1835,
					1837,
					1845
				});
				Common.TransmuteItems(new int[]
				{
					1782,
					1784,
					1811,
					1826,
					1801,
					1802,
					4680,
					1798
				});
				Common.TransmuteItems(new int[]
				{
					1928,
					1916,
					1930,
					1871
				});
				Common.TransmuteItems(new int[]
				{
					1910,
					1929
				});
				Common.TransmuteItems(new int[]
				{
					1947,
					1946,
					1931,
					1959
				});
				Common.TransmuteItems(new int[]
				{
					2880,
					2797,
					2796,
					2795,
					2749,
					2769
				});
				Common.TransmuteItems(new int[]
				{
					4381,
					4273
				});
				Common.TransmuteItems(new int[]
				{
					4270,
					4272
				});
				Common.TransmuteItems(new int[]
				{
					3054,
					3052,
					3053
				});
				Common.TransmuteItems(new int[]
				{
					1571,
					1569
				});
				Common.TransmuteItems(new int[]
				{
					3008,
					3014,
					3012,
					3015,
					3023
				});
				Common.TransmuteItems(new int[]
				{
					3006,
					3007,
					3013,
					3016,
					3020
				});
				Common.TransmuteItems(new int[]
				{
					3029,
					3030,
					3051,
					3022
				});
				Common.TransmuteItems(new int[]
				{
					3290,
					3282
				});
				Common.TransmuteItems(new int[]
				{
					3187,
					3188,
					3189
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "Didgeridoo"),
					Common.GetModItem(ModConditions.thoriumMod, "StormHatchlingStaff"),
					Common.GetModItem(ModConditions.thoriumMod, "TalonBurst"),
					Common.GetModItem(ModConditions.thoriumMod, "ThunderTalon")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "BuccaneerBlunderBuss"),
					Common.GetModItem(ModConditions.thoriumMod, "ConchShell"),
					Common.GetModItem(ModConditions.thoriumMod, "GiantGlowstick"),
					Common.GetModItem(ModConditions.thoriumMod, "JellyPondWand"),
					Common.GetModItem(ModConditions.thoriumMod, "SparkingJellyBall")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "BatScythe"),
					Common.GetModItem(ModConditions.thoriumMod, "BatWing"),
					Common.GetModItem(ModConditions.thoriumMod, "GuanoGunner"),
					Common.GetModItem(ModConditions.thoriumMod, "SonarCannon"),
					Common.GetModItem(ModConditions.thoriumMod, "VampireScepter"),
					Common.GetModItem(ModConditions.thoriumMod, "ViscountCane")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "BoulderProbeStaff"),
					Common.GetModItem(ModConditions.thoriumMod, "EnergyProjector"),
					Common.GetModItem(ModConditions.thoriumMod, "EnergyStormBolter"),
					Common.GetModItem(ModConditions.thoriumMod, "EnergyStormPartisan"),
					Common.GetModItem(ModConditions.thoriumMod, "ShockAbsorber")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "ChampionBomberStaff"),
					Common.GetModItem(ModConditions.thoriumMod, "ChampionsGodHand"),
					Common.GetModItem(ModConditions.thoriumMod, "ChampionsRebuttal"),
					Common.GetModItem(ModConditions.thoriumMod, "ChampionSwiftBlade"),
					Common.GetModItem(ModConditions.thoriumMod, "ChampionsTrifectaShot")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "DistressCaller"),
					Common.GetModItem(ModConditions.thoriumMod, "GaussFlinger"),
					Common.GetModItem(ModConditions.thoriumMod, "HitScanner"),
					Common.GetModItem(ModConditions.thoriumMod, "ParticleWhip"),
					Common.GetModItem(ModConditions.thoriumMod, "Roboboe"),
					Common.GetModItem(ModConditions.thoriumMod, "StarRod"),
					Common.GetModItem(ModConditions.thoriumMod, "StarTrail")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "BoreanFangStaff"),
					Common.GetModItem(ModConditions.thoriumMod, "FreezeRay"),
					Common.GetModItem(ModConditions.thoriumMod, "GlacialSting"),
					Common.GetModItem(ModConditions.thoriumMod, "Glacier"),
					Common.GetModItem(ModConditions.thoriumMod, "TheCryoFang")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "BeholderGaze"),
					Common.GetModItem(ModConditions.thoriumMod, "BeholderStaff"),
					Common.GetModItem(ModConditions.thoriumMod, "Cello"),
					Common.GetModItem(ModConditions.thoriumMod, "HellishHalberd"),
					Common.GetModItem(ModConditions.thoriumMod, "HellRoller"),
					Common.GetModItem(ModConditions.thoriumMod, "Obliterator"),
					Common.GetModItem(ModConditions.thoriumMod, "PyroclastStaff"),
					Common.GetModItem(ModConditions.thoriumMod, "VoidPlanter")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "CadaverCornet"),
					Common.GetModItem(ModConditions.thoriumMod, "PhantomWand"),
					Common.GetModItem(ModConditions.thoriumMod, "SoulCleaver"),
					Common.GetModItem(ModConditions.thoriumMod, "SoulRender"),
					Common.GetModItem(ModConditions.thoriumMod, "WitherStaff"),
					Common.GetModItem(ModConditions.thoriumMod, "TheLostCross")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "MantisShrimpPunch"),
					Common.GetModItem(ModConditions.thoriumMod, "OldGodsVision"),
					Common.GetModItem(ModConditions.thoriumMod, "RlyehLostRod"),
					Common.GetModItem(ModConditions.thoriumMod, "SirensLyre"),
					Common.GetModItem(ModConditions.thoriumMod, "TheIncubator"),
					Common.GetModItem(ModConditions.thoriumMod, "TrenchSpitter")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "WhisperingHood"),
					Common.GetModItem(ModConditions.thoriumMod, "WhisperingTabard"),
					Common.GetModItem(ModConditions.thoriumMod, "WhisperingLeggings")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "DeathEssence"),
					Common.GetModItem(ModConditions.thoriumMod, "InfernoEssence"),
					Common.GetModItem(ModConditions.thoriumMod, "OceanEssence")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "AncientFlame"),
					Common.GetModItem(ModConditions.thoriumMod, "AncientFrost"),
					Common.GetModItem(ModConditions.thoriumMod, "AncientSpark"),
					Common.GetModItem(ModConditions.thoriumMod, "EclipseFang"),
					Common.GetModItem(ModConditions.thoriumMod, "CosmicFluxStaff")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "GraveBuster"),
					Common.GetModItem(ModConditions.thoriumMod, "TheGoodBook")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "BloomGuard"),
					Common.GetModItem(ModConditions.thoriumMod, "WeedEater")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "MageHand"),
					Common.GetModItem(ModConditions.thoriumMod, "ScryingGlass")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "DarkMageStaff"),
					Common.GetModItem(ModConditions.thoriumMod, "DarkTome"),
					Common.GetModItem(ModConditions.thoriumMod, "TabooWand")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "Hippocraticrossbow"),
					Common.GetModItem(ModConditions.thoriumMod, "OgreSnotGun"),
					Common.GetModItem(ModConditions.thoriumMod, "OgreSandal")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "TwentyFourCaratTuba"),
					Common.GetModItem(ModConditions.thoriumMod, "DutchmansAvarice"),
					Common.GetModItem(ModConditions.thoriumMod, "GreedfulGurdy"),
					Common.GetModItem(ModConditions.thoriumMod, "GreedyMagnet"),
					Common.GetModItem(ModConditions.thoriumMod, "HandCannon"),
					Common.GetModItem(ModConditions.thoriumMod, "ShipsHelm"),
					Common.GetModItem(ModConditions.thoriumMod, "TheJuggernaut")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "CosmicDagger"),
					Common.GetModItem(ModConditions.thoriumMod, "Kinetoscythe"),
					Common.GetModItem(ModConditions.thoriumMod, "LivewireCrasher"),
					Common.GetModItem(ModConditions.thoriumMod, "MolecularStabilizer"),
					Common.GetModItem(ModConditions.thoriumMod, "SuperPlasmaCannon"),
					Common.GetModItem(ModConditions.thoriumMod, "TheTriangle"),
					Common.GetModItem(ModConditions.thoriumMod, "Turntable")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "DevilDagger"),
					Common.GetModItem(ModConditions.thoriumMod, "DevilsReach"),
					Common.GetModItem(ModConditions.thoriumMod, "Schmelze"),
					Common.GetModItem(ModConditions.thoriumMod, "Scorn")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "FungalCane"),
					Common.GetModItem(ModConditions.thoriumMod, "FungalHook"),
					Common.GetModItem(ModConditions.thoriumMod, "Funggat"),
					Common.GetModItem(ModConditions.thoriumMod, "MyceliumWhip")
				});
				Common.TransmuteItems(new int[]
				{
					Common.GetModItem(ModConditions.thoriumMod, "HydromancerCatalyst"),
					Common.GetModItem(ModConditions.thoriumMod, "NeptuneGrasp"),
					Common.GetModItem(ModConditions.thoriumMod, "PoseidonCharge"),
					Common.GetModItem(ModConditions.thoriumMod, "SSDevastator")
				});
			}
			if (QoLCompendium.mainConfig.ItemConversions)
			{
				Common.TransmuteItems(new int[]
				{
					2214,
					2215,
					2216,
					2217
				});
				Common.TransmuteItems(new int[]
				{
					407,
					1923
				});
				Common.TransmuteItems(new int[]
				{
					3031,
					4820,
					5302
				});
				Common.TransmuteItems(new int[]
				{
					3032,
					4872,
					5303
				});
			}
		}
	}
}
