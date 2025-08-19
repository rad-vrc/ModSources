using System;
using System.Collections.Generic;
using Terraria.GameContent.Events;
using Terraria.ID;

namespace Terraria.ModLoader.Utilities
{
	/// <summary>
	/// This serves as a central class to help modders spawn their NPCs. It's basically the vanilla spawn code if-else chains condensed into objects. See ExampleMod for usages.
	/// </summary>
	// Token: 0x02000238 RID: 568
	public class SpawnCondition
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x0050D6E4 File Offset: 0x0050B8E4
		internal IEnumerable<SpawnCondition> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060028C3 RID: 10435 RVA: 0x0050D6EC File Offset: 0x0050B8EC
		internal float BlockWeight
		{
			get
			{
				return this.blockWeight;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x0050D6F4 File Offset: 0x0050B8F4
		public float Chance
		{
			get
			{
				return this.chance;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060028C5 RID: 10437 RVA: 0x0050D6FC File Offset: 0x0050B8FC
		public bool Active
		{
			get
			{
				return this.active;
			}
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x0050D704 File Offset: 0x0050B904
		internal SpawnCondition(Func<NPCSpawnInfo, bool> condition, float blockWeight = 1f)
		{
			this.condition = condition;
			this.children = new List<SpawnCondition>();
			this.blockWeight = blockWeight;
			NPCSpawnHelper.conditions.Add(this);
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x0050D730 File Offset: 0x0050B930
		internal SpawnCondition(SpawnCondition parent, Func<NPCSpawnInfo, bool> condition, float blockWeight = 1f)
		{
			this.condition = condition;
			this.children = new List<SpawnCondition>();
			this.blockWeight = blockWeight;
			parent.children.Add(this);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x0050D760 File Offset: 0x0050B960
		internal void Reset()
		{
			this.chance = 0f;
			this.active = false;
			foreach (SpawnCondition spawnCondition in this.children)
			{
				spawnCondition.Reset();
			}
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x0050D7C4 File Offset: 0x0050B9C4
		internal void Check(NPCSpawnInfo info, ref float remainingWeight)
		{
			if (this.WeightFunc != null)
			{
				this.blockWeight = this.WeightFunc();
			}
			this.active = true;
			if (this.condition(info))
			{
				this.chance = remainingWeight * this.blockWeight;
				float childWeight = this.chance;
				foreach (SpawnCondition spawnCondition in this.children)
				{
					spawnCondition.Check(info, ref childWeight);
					if ((double)Math.Abs(childWeight) < 5E-06)
					{
						break;
					}
				}
				remainingWeight -= this.chance;
			}
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x0050D87C File Offset: 0x0050BA7C
		unsafe static SpawnCondition()
		{
			SpawnCondition.Wraith.WeightFunc = delegate()
			{
				float inverseChance = 0.95f;
				if (Main.moonPhase == 4)
				{
					inverseChance *= 0.8f;
				}
				return 1f - inverseChance;
			};
			SpawnCondition.HoppinJack = new SpawnCondition((NPCSpawnInfo info) => Main.hardMode && Main.halloween && (double)info.SpawnTileY <= Main.worldSurface && !Main.dayTime, 0.1f);
			SpawnCondition.DoctorBones = new SpawnCondition((NPCSpawnInfo info) => info.SpawnTileType == 60 && !Main.dayTime, 0.002f);
			SpawnCondition.LacBeetle = new SpawnCondition((NPCSpawnInfo info) => info.SpawnTileType == 60 && (double)info.SpawnTileY > Main.worldSurface, 0.016666668f);
			SpawnCondition.WormCritter = new SpawnCondition((NPCSpawnInfo info) => (double)info.SpawnTileY > Main.worldSurface && info.SpawnTileY < Main.maxTilesY - 210 && !info.Player.ZoneSnow && !info.Player.ZoneCrimson && !info.Player.ZoneCorrupt && !info.Player.ZoneJungle && !info.Player.ZoneHallow, 0.125f);
			SpawnCondition.MouseCritter = new SpawnCondition((NPCSpawnInfo info) => (double)info.SpawnTileY > Main.worldSurface && info.SpawnTileY < Main.maxTilesY - 210 && !info.Player.ZoneSnow && !info.Player.ZoneCrimson && !info.Player.ZoneCorrupt && !info.Player.ZoneJungle && !info.Player.ZoneHallow, 0.07692308f);
			SpawnCondition.SnailCritter = new SpawnCondition((NPCSpawnInfo info) => (double)info.SpawnTileY > Main.worldSurface && (double)info.SpawnTileY < (Main.rockLayer + (double)Main.maxTilesY) / 2.0 && !info.Player.ZoneSnow && !info.Player.ZoneCrimson && !info.Player.ZoneCorrupt && !info.Player.ZoneHallow, 0.07692308f);
			SpawnCondition.FrogCritter = new SpawnCondition((NPCSpawnInfo info) => (double)info.SpawnTileY < Main.worldSurface && info.Player.ZoneJungle, 0.11111111f);
			SpawnCondition.HardmodeJungle = new SpawnCondition((NPCSpawnInfo info) => info.SpawnTileType == 60 && Main.hardMode, 0.6666667f);
			SpawnCondition.JungleTemple = new SpawnCondition((NPCSpawnInfo info) => info.SpawnTileType == 226 && info.Lihzahrd, 1f);
			SpawnCondition.UndergroundJungle = new SpawnCondition((NPCSpawnInfo info) => info.SpawnTileType == 60 && (double)info.SpawnTileY > (Main.worldSurface + Main.rockLayer) / 2.0, 1f);
			SpawnCondition.SurfaceJungle = new SpawnCondition((NPCSpawnInfo info) => info.SpawnTileType == 60, 0.34375f);
			SpawnCondition.SandstormEvent = new SpawnCondition((NPCSpawnInfo info) => Sandstorm.Happening && info.Player.ZoneSandstorm && TileID.Sets.Conversion.Sand[info.SpawnTileType] && NPC.Spawning_SandstoneCheck(info.SpawnTileX, info.SpawnTileY), 1f);
			SpawnCondition.Mummy = new SpawnCondition((NPCSpawnInfo info) => Main.hardMode && info.SpawnTileType == 53, 0.33333334f);
			SpawnCondition.DarkMummy = new SpawnCondition((NPCSpawnInfo info) => Main.hardMode && (info.SpawnTileType == 112 || info.SpawnTileType == 234), 0.5f);
			SpawnCondition.LightMummy = new SpawnCondition((NPCSpawnInfo info) => Main.hardMode && info.SpawnTileType == 116, 0.5f);
			SpawnCondition.OverworldHallow = new SpawnCondition((NPCSpawnInfo info) => Main.hardMode && !info.Water && (double)info.SpawnTileY < Main.rockLayer && (info.SpawnTileType == 116 || info.SpawnTileType == 117 || info.SpawnTileType == 109 || info.SpawnTileType == 164), 1f);
			SpawnCondition.EnchantedSword = new SpawnCondition((NPCSpawnInfo info) => !info.PlayerSafe && Main.hardMode && !info.Water && (double)info.SpawnTileY >= Main.rockLayer && (info.SpawnTileType == 116 || info.SpawnTileType == 117 || info.SpawnTileType == 109 || info.SpawnTileType == 164), 0.02f);
			SpawnCondition.Crimson = new SpawnCondition((NPCSpawnInfo info) => (info.SpawnTileType == 204 && info.Player.ZoneCrimson) || info.SpawnTileType == 199 || info.SpawnTileType == 200 || info.SpawnTileType == 203 || info.SpawnTileType == 234, 1f);
			SpawnCondition.Corruption = new SpawnCondition((NPCSpawnInfo info) => (info.SpawnTileType == 22 && info.Player.ZoneCorrupt) || info.SpawnTileType == 23 || info.SpawnTileType == 25 || info.SpawnTileType == 112 || info.SpawnTileType == 163, 1f);
			SpawnCondition.Overworld = new SpawnCondition((NPCSpawnInfo info) => (double)info.SpawnTileY <= Main.worldSurface, 1f);
			SpawnCondition.IceGolem = new SpawnCondition(SpawnCondition.Overworld, (NPCSpawnInfo info) => info.Player.ZoneSnow && Main.hardMode && Main.cloudAlpha > 0f && !NPC.AnyNPCs(243), 0.05f);
			SpawnCondition.RainbowSlime = new SpawnCondition(SpawnCondition.Overworld, (NPCSpawnInfo info) => info.Player.ZoneHallow && Main.hardMode && Main.cloudAlpha > 0f && !NPC.AnyNPCs(244), 0.05f);
			SpawnCondition.AngryNimbus = new SpawnCondition(SpawnCondition.Overworld, (NPCSpawnInfo info) => !info.Player.ZoneSnow && Main.hardMode && Main.cloudAlpha > 0f && NPC.CountNPCS(250) < 2, 0.1f);
			SpawnCondition.MartianProbe = new SpawnCondition(SpawnCondition.Overworld, (NPCSpawnInfo info) => SpawnCondition.MartianProbeHelper(info) && Main.hardMode && NPC.downedGolemBoss && !NPC.AnyNPCs(399), 0.0025f);
			SpawnCondition.MartianProbe.WeightFunc = delegate()
			{
				float inverseChance = 0.9975f;
				if (!NPC.downedMartians)
				{
					inverseChance *= 0.99f;
				}
				return 1f - inverseChance;
			};
			SpawnCondition.OverworldDay = new SpawnCondition(SpawnCondition.Overworld, (NPCSpawnInfo info) => Main.dayTime, 1f);
			SpawnCondition.OverworldDaySnowCritter = new SpawnCondition(SpawnCondition.OverworldDay, (NPCSpawnInfo info) => SpawnCondition.InnerThird(info) && (*SpawnCondition.GetTile(info).type == 147 || *SpawnCondition.GetTile(info).type == 161), 0.06666667f);
			SpawnCondition.OverworldDayGrassCritter = new SpawnCondition(SpawnCondition.OverworldDay, (NPCSpawnInfo info) => SpawnCondition.InnerThird(info) && (*SpawnCondition.GetTile(info).type == 2 || *SpawnCondition.GetTile(info).type == 109), 0.06666667f);
			SpawnCondition.OverworldDaySandCritter = new SpawnCondition(SpawnCondition.OverworldDay, (NPCSpawnInfo info) => SpawnCondition.InnerThird(info) && *SpawnCondition.GetTile(info).type == 53, 0.06666667f);
			SpawnCondition.OverworldMorningBirdCritter = new SpawnCondition(SpawnCondition.OverworldDay, (NPCSpawnInfo info) => SpawnCondition.InnerThird(info) && Main.time < 18000.0 && (*SpawnCondition.GetTile(info).type == 2 || *SpawnCondition.GetTile(info).type == 109), 0.25f);
			SpawnCondition.OverworldDayBirdCritter = new SpawnCondition(SpawnCondition.OverworldDay, (NPCSpawnInfo info) => SpawnCondition.InnerThird(info) && (*SpawnCondition.GetTile(info).type == 2 || *SpawnCondition.GetTile(info).type == 109 || *SpawnCondition.GetTile(info).type == 147), 0.06666667f);
			SpawnCondition.KingSlime = new SpawnCondition(SpawnCondition.OverworldDay, (NPCSpawnInfo info) => SpawnCondition.OuterThird(info) && *SpawnCondition.GetTile(info).type == 2 && !NPC.AnyNPCs(50), 0.0033333334f);
			SpawnCondition.OverworldDayDesert = new SpawnCondition(SpawnCondition.OverworldDay, (NPCSpawnInfo info) => *SpawnCondition.GetTile(info).type == 53 && !info.Water, 0.2f);
			SpawnCondition.GoblinScout = new SpawnCondition(SpawnCondition.OverworldDay, (NPCSpawnInfo info) => SpawnCondition.OuterThird(info), 0.06666667f);
			SpawnCondition.GoblinScout.WeightFunc = delegate()
			{
				float inverseChance = 0.93333334f;
				if (!NPC.downedGoblins && WorldGen.shadowOrbSmashed)
				{
					return inverseChance * 0.85714287f;
				}
				return 1f - inverseChance;
			};
			SpawnCondition.OverworldDayRain = new SpawnCondition(SpawnCondition.OverworldDay, (NPCSpawnInfo info) => Main.raining, 0.6666667f);
			SpawnCondition.OverworldDaySlime = new SpawnCondition(SpawnCondition.OverworldDay, (NPCSpawnInfo info) => true, 1f);
			SpawnCondition.OverworldNight = new SpawnCondition(SpawnCondition.Overworld, (NPCSpawnInfo info) => true, 1f);
			SpawnCondition.OverworldFirefly = new SpawnCondition(SpawnCondition.OverworldNight, (NPCSpawnInfo info) => *SpawnCondition.GetTile(info).type == 2 || *SpawnCondition.GetTile(info).type == 109, 0.1f);
			SpawnCondition.OverworldFirefly.WeightFunc = (() => 1f / (float)NPC.fireFlyChance);
			SpawnCondition.OverworldNightMonster = new SpawnCondition(SpawnCondition.OverworldNight, (NPCSpawnInfo info) => true, 1f);
			SpawnCondition.Underground = new SpawnCondition((NPCSpawnInfo info) => (double)info.SpawnTileY <= Main.rockLayer, 1f);
			SpawnCondition.Underworld = new SpawnCondition((NPCSpawnInfo info) => info.SpawnTileY > Main.maxTilesY - 190, 1f);
			SpawnCondition.Cavern = new SpawnCondition((NPCSpawnInfo info) => true, 1f);
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x0050E50E File Offset: 0x0050C70E
		private static Tile GetTile(NPCSpawnInfo info)
		{
			return Main.tile[info.SpawnTileX, info.SpawnTileY];
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x0050E528 File Offset: 0x0050C728
		private unsafe static bool WaterSurface(NPCSpawnInfo info)
		{
			if (info.SafeRangeX)
			{
				return false;
			}
			for (int i = info.SpawnTileY - 1; i > info.SpawnTileY - 50; i--)
			{
				if (*Main.tile[info.SpawnTileX, i].liquid == 0 && !WorldGen.SolidTile(info.SpawnTileX, i, false) && !WorldGen.SolidTile(info.SpawnTileX, i + 1, false) && !WorldGen.SolidTile(info.SpawnTileX, i + 2, false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x0050E5AA File Offset: 0x0050C7AA
		private static bool MartianProbeHelper(NPCSpawnInfo info)
		{
			return (float)Math.Abs(info.SpawnTileX - Main.maxTilesX / 2) / (float)(Main.maxTilesX / 2) > 0.33f && !NPC.AnyDanger(false, false);
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x0050E5DC File Offset: 0x0050C7DC
		private static bool InnerThird(NPCSpawnInfo info)
		{
			return Math.Abs(info.SpawnTileX - Main.spawnTileX) < Main.maxTilesX / 3;
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x0050E5F8 File Offset: 0x0050C7F8
		private static bool OuterThird(NPCSpawnInfo info)
		{
			return Math.Abs(info.SpawnTileX - Main.spawnTileX) > Main.maxTilesX / 3;
		}

		// Token: 0x04001958 RID: 6488
		private Func<NPCSpawnInfo, bool> condition;

		// Token: 0x04001959 RID: 6489
		private List<SpawnCondition> children;

		// Token: 0x0400195A RID: 6490
		private float blockWeight;

		// Token: 0x0400195B RID: 6491
		internal Func<float> WeightFunc;

		// Token: 0x0400195C RID: 6492
		private float chance;

		// Token: 0x0400195D RID: 6493
		private bool active;

		// Token: 0x0400195E RID: 6494
		public static readonly SpawnCondition NebulaTower = new SpawnCondition((NPCSpawnInfo info) => info.Player.ZoneTowerNebula, 1f);

		// Token: 0x0400195F RID: 6495
		public static readonly SpawnCondition VortexTower = new SpawnCondition((NPCSpawnInfo info) => info.Player.ZoneTowerVortex, 1f);

		// Token: 0x04001960 RID: 6496
		public static readonly SpawnCondition StardustTower = new SpawnCondition((NPCSpawnInfo info) => info.Player.ZoneTowerStardust, 1f);

		// Token: 0x04001961 RID: 6497
		public static readonly SpawnCondition SolarTower = new SpawnCondition((NPCSpawnInfo info) => info.Player.ZoneTowerSolar, 1f);

		// Token: 0x04001962 RID: 6498
		public static readonly SpawnCondition Sky = new SpawnCondition((NPCSpawnInfo info) => info.Sky, 1f);

		// Token: 0x04001963 RID: 6499
		public static readonly SpawnCondition Invasion = new SpawnCondition((NPCSpawnInfo info) => info.Invasion, 1f);

		// Token: 0x04001964 RID: 6500
		public static readonly SpawnCondition GoblinArmy = new SpawnCondition(SpawnCondition.Invasion, (NPCSpawnInfo info) => Main.invasionType == 1, 1f);

		// Token: 0x04001965 RID: 6501
		public static readonly SpawnCondition FrostLegion = new SpawnCondition(SpawnCondition.Invasion, (NPCSpawnInfo info) => Main.invasionType == 2, 1f);

		// Token: 0x04001966 RID: 6502
		public static readonly SpawnCondition Pirates = new SpawnCondition(SpawnCondition.Invasion, (NPCSpawnInfo info) => Main.invasionType == 3, 1f);

		// Token: 0x04001967 RID: 6503
		public static readonly SpawnCondition MartianMadness = new SpawnCondition(SpawnCondition.Invasion, (NPCSpawnInfo info) => Main.invasionType == 4, 1f);

		// Token: 0x04001968 RID: 6504
		public static readonly SpawnCondition Bartender = new SpawnCondition((NPCSpawnInfo info) => !NPC.savedBartender && DD2Event.ReadyToFindBartender && !NPC.AnyNPCs(579) && !info.Water, 0.0125f);

		// Token: 0x04001969 RID: 6505
		public static readonly SpawnCondition SpiderCave = new SpawnCondition((NPCSpawnInfo info) => *SpawnCondition.GetTile(info).wall == 62 || info.SpiderCave, 1f);

		// Token: 0x0400196A RID: 6506
		public static readonly SpawnCondition DesertCave = new SpawnCondition((NPCSpawnInfo info) => (WallID.Sets.Conversion.HardenedSand[(int)(*SpawnCondition.GetTile(info).wall)] || WallID.Sets.Conversion.Sandstone[(int)(*SpawnCondition.GetTile(info).wall)] || info.DesertCave) && WorldGen.checkUnderground(info.SpawnTileX, info.SpawnTileY), 1f);

		// Token: 0x0400196B RID: 6507
		public static readonly SpawnCondition HardmodeJungleWater = new SpawnCondition((NPCSpawnInfo info) => Main.hardMode && info.Water && info.Player.ZoneJungle, 0.6666667f);

		// Token: 0x0400196C RID: 6508
		public static readonly SpawnCondition HardmodeCrimsonWater = new SpawnCondition((NPCSpawnInfo info) => Main.hardMode && info.Water && info.Player.ZoneCrimson, 0.8888889f);

		// Token: 0x0400196D RID: 6509
		public static readonly SpawnCondition Ocean = new SpawnCondition((NPCSpawnInfo info) => info.Water && (info.SpawnTileX < 250 || info.SpawnTileX > Main.maxTilesX - 250) && Main.tileSand[info.SpawnTileType] && (double)info.SpawnTileY < Main.rockLayer, 1f);

		// Token: 0x0400196E RID: 6510
		public static readonly SpawnCondition OceanAngler = new SpawnCondition(SpawnCondition.Ocean, (NPCSpawnInfo info) => !NPC.savedAngler && !NPC.AnyNPCs(376) && SpawnCondition.WaterSurface(info), 1f);

		// Token: 0x0400196F RID: 6511
		public static readonly SpawnCondition OceanMonster = new SpawnCondition(SpawnCondition.Ocean, (NPCSpawnInfo info) => true, 1f);

		// Token: 0x04001970 RID: 6512
		public static readonly SpawnCondition BeachAngler = new SpawnCondition((NPCSpawnInfo info) => !info.Water && !NPC.savedAngler && !NPC.AnyNPCs(376) && (info.SpawnTileX < 340 || info.SpawnTileX > Main.maxTilesX - 340) && Main.tileSand[info.SpawnTileType] && (double)info.SpawnTileY < Main.worldSurface, 1f);

		// Token: 0x04001971 RID: 6513
		public static readonly SpawnCondition JungleWater = new SpawnCondition((NPCSpawnInfo info) => info.Water && info.SpawnTileType == 60, 1f);

		// Token: 0x04001972 RID: 6514
		public static readonly SpawnCondition CavePiranha = new SpawnCondition((NPCSpawnInfo info) => info.Water && (double)info.SpawnTileY > Main.rockLayer, 0.5f);

		// Token: 0x04001973 RID: 6515
		public static readonly SpawnCondition CaveJellyfish = new SpawnCondition((NPCSpawnInfo info) => info.Water && (double)info.SpawnTileY > Main.worldSurface, 0.33333334f);

		// Token: 0x04001974 RID: 6516
		public static readonly SpawnCondition WaterCritter = new SpawnCondition((NPCSpawnInfo info) => info.Water, 0.25f);

		// Token: 0x04001975 RID: 6517
		public static readonly SpawnCondition CorruptWaterCritter = new SpawnCondition(SpawnCondition.WaterCritter, (NPCSpawnInfo info) => info.Player.ZoneCorrupt, 1f);

		// Token: 0x04001976 RID: 6518
		public static readonly SpawnCondition OverworldWaterCritter = new SpawnCondition(SpawnCondition.WaterCritter, (NPCSpawnInfo info) => (double)info.SpawnTileY < Main.worldSurface && info.SpawnTileY > 50 && Main.dayTime, 0.6666667f);

		// Token: 0x04001977 RID: 6519
		public static readonly SpawnCondition OverworldWaterSurfaceCritter = new SpawnCondition(SpawnCondition.OverworldWaterCritter, new Func<NPCSpawnInfo, bool>(SpawnCondition.WaterSurface), 1f);

		// Token: 0x04001978 RID: 6520
		public static readonly SpawnCondition OverworldUnderwaterCritter = new SpawnCondition(SpawnCondition.OverworldWaterCritter, (NPCSpawnInfo info) => true, 1f);

		// Token: 0x04001979 RID: 6521
		public static readonly SpawnCondition DefaultWaterCritter = new SpawnCondition(SpawnCondition.WaterCritter, (NPCSpawnInfo info) => true, 1f);

		// Token: 0x0400197A RID: 6522
		public static readonly SpawnCondition BoundCaveNPC = new SpawnCondition((NPCSpawnInfo info) => !info.Water && (double)info.SpawnTileY >= Main.rockLayer && info.SpawnTileY < Main.maxTilesY - 210, 0.05f);

		// Token: 0x0400197B RID: 6523
		public static readonly SpawnCondition TownCritter = new SpawnCondition((NPCSpawnInfo info) => info.PlayerInTown, 1f);

		// Token: 0x0400197C RID: 6524
		public static readonly SpawnCondition TownWaterCritter = new SpawnCondition(SpawnCondition.TownCritter, (NPCSpawnInfo info) => info.Water, 1f);

		// Token: 0x0400197D RID: 6525
		public static readonly SpawnCondition TownOverworldWaterCritter = new SpawnCondition(SpawnCondition.TownWaterCritter, (NPCSpawnInfo info) => (double)info.SpawnTileY < Main.worldSurface && info.SpawnTileY > 50 && Main.dayTime, 0.6666667f);

		// Token: 0x0400197E RID: 6526
		public static readonly SpawnCondition TownOverworldWaterSurfaceCritter = new SpawnCondition(SpawnCondition.TownOverworldWaterCritter, new Func<NPCSpawnInfo, bool>(SpawnCondition.WaterSurface), 1f);

		// Token: 0x0400197F RID: 6527
		public static readonly SpawnCondition TownOverworldUnderwaterCritter = new SpawnCondition(SpawnCondition.TownOverworldWaterCritter, (NPCSpawnInfo info) => true, 1f);

		// Token: 0x04001980 RID: 6528
		public static readonly SpawnCondition TownDefaultWaterCritter = new SpawnCondition(SpawnCondition.TownWaterCritter, (NPCSpawnInfo info) => true, 1f);

		// Token: 0x04001981 RID: 6529
		public static readonly SpawnCondition TownSnowCritter = new SpawnCondition(SpawnCondition.TownCritter, (NPCSpawnInfo info) => info.SpawnTileType == 147 || info.SpawnTileType == 161, 1f);

		// Token: 0x04001982 RID: 6530
		public static readonly SpawnCondition TownJungleCritter = new SpawnCondition(SpawnCondition.TownCritter, (NPCSpawnInfo info) => info.SpawnTileType == 60, 1f);

		// Token: 0x04001983 RID: 6531
		public static readonly SpawnCondition TownGeneralCritter = new SpawnCondition(SpawnCondition.TownCritter, (NPCSpawnInfo info) => info.SpawnTileType == 2 || info.SpawnTileType == 109 || (double)info.SpawnTileY > Main.worldSurface, 1f);

		// Token: 0x04001984 RID: 6532
		public static readonly SpawnCondition Dungeon = new SpawnCondition((NPCSpawnInfo info) => info.Player.ZoneDungeon, 1f);

		// Token: 0x04001985 RID: 6533
		public static readonly SpawnCondition DungeonGuardian = new SpawnCondition(SpawnCondition.Dungeon, (NPCSpawnInfo info) => !NPC.downedBoss3, 1f);

		// Token: 0x04001986 RID: 6534
		public static readonly SpawnCondition DungeonNormal = new SpawnCondition(SpawnCondition.Dungeon, (NPCSpawnInfo info) => true, 1f);

		// Token: 0x04001987 RID: 6535
		public static readonly SpawnCondition Meteor = new SpawnCondition((NPCSpawnInfo info) => info.Player.ZoneMeteor, 1f);

		// Token: 0x04001988 RID: 6536
		public static readonly SpawnCondition OldOnesArmy = new SpawnCondition((NPCSpawnInfo info) => DD2Event.Ongoing && info.Player.ZoneOldOneArmy, 1f);

		// Token: 0x04001989 RID: 6537
		public static readonly SpawnCondition FrostMoon = new SpawnCondition((NPCSpawnInfo info) => (double)info.SpawnTileY <= Main.worldSurface && !Main.dayTime && Main.snowMoon, 1f);

		// Token: 0x0400198A RID: 6538
		public static readonly SpawnCondition PumpkinMoon = new SpawnCondition((NPCSpawnInfo info) => (double)info.SpawnTileY <= Main.worldSurface && !Main.dayTime && Main.pumpkinMoon, 1f);

		// Token: 0x0400198B RID: 6539
		public static readonly SpawnCondition SolarEclipse = new SpawnCondition((NPCSpawnInfo info) => (double)info.SpawnTileY <= Main.worldSurface && Main.dayTime && Main.eclipse, 1f);

		// Token: 0x0400198C RID: 6540
		public static readonly SpawnCondition HardmodeMushroomWater = new SpawnCondition((NPCSpawnInfo info) => Main.hardMode && info.SpawnTileType == 70 && info.Water, 1f);

		// Token: 0x0400198D RID: 6541
		public static readonly SpawnCondition OverworldMushroom = new SpawnCondition((NPCSpawnInfo info) => info.SpawnTileType == 70 && (double)info.SpawnTileY <= Main.worldSurface, 0.6666667f);

		// Token: 0x0400198E RID: 6542
		public static readonly SpawnCondition UndergroundMushroom = new SpawnCondition((NPCSpawnInfo info) => info.SpawnTileType == 70 && Main.hardMode && (double)info.SpawnTileY >= Main.worldSurface, 0.6666667f);

		// Token: 0x0400198F RID: 6543
		public static readonly SpawnCondition CorruptWorm = new SpawnCondition((NPCSpawnInfo info) => info.Player.ZoneCorrupt && !info.PlayerSafe, 0.015384615f);

		// Token: 0x04001990 RID: 6544
		public static readonly SpawnCondition UndergroundMimic = new SpawnCondition((NPCSpawnInfo info) => Main.hardMode && (double)info.SpawnTileY > Main.worldSurface, 0.014285714f);

		// Token: 0x04001991 RID: 6545
		public static readonly SpawnCondition OverworldMimic = new SpawnCondition((NPCSpawnInfo info) => Main.hardMode && *SpawnCondition.GetTile(info).wall == 2, 0.05f);

		// Token: 0x04001992 RID: 6546
		public static readonly SpawnCondition Wraith = new SpawnCondition((NPCSpawnInfo info) => Main.hardMode && (double)info.SpawnTileY <= Main.worldSurface && !Main.dayTime, 0.05f);

		// Token: 0x04001993 RID: 6547
		public static readonly SpawnCondition HoppinJack;

		// Token: 0x04001994 RID: 6548
		public static readonly SpawnCondition DoctorBones;

		// Token: 0x04001995 RID: 6549
		public static readonly SpawnCondition LacBeetle;

		// Token: 0x04001996 RID: 6550
		public static readonly SpawnCondition WormCritter;

		// Token: 0x04001997 RID: 6551
		public static readonly SpawnCondition MouseCritter;

		// Token: 0x04001998 RID: 6552
		public static readonly SpawnCondition SnailCritter;

		// Token: 0x04001999 RID: 6553
		public static readonly SpawnCondition FrogCritter;

		// Token: 0x0400199A RID: 6554
		public static readonly SpawnCondition HardmodeJungle;

		// Token: 0x0400199B RID: 6555
		public static readonly SpawnCondition JungleTemple;

		// Token: 0x0400199C RID: 6556
		public static readonly SpawnCondition UndergroundJungle;

		// Token: 0x0400199D RID: 6557
		public static readonly SpawnCondition SurfaceJungle;

		// Token: 0x0400199E RID: 6558
		public static readonly SpawnCondition SandstormEvent;

		// Token: 0x0400199F RID: 6559
		public static readonly SpawnCondition Mummy;

		// Token: 0x040019A0 RID: 6560
		public static readonly SpawnCondition DarkMummy;

		// Token: 0x040019A1 RID: 6561
		public static readonly SpawnCondition LightMummy;

		// Token: 0x040019A2 RID: 6562
		public static readonly SpawnCondition OverworldHallow;

		// Token: 0x040019A3 RID: 6563
		public static readonly SpawnCondition EnchantedSword;

		// Token: 0x040019A4 RID: 6564
		public static readonly SpawnCondition Crimson;

		// Token: 0x040019A5 RID: 6565
		public static readonly SpawnCondition Corruption;

		// Token: 0x040019A6 RID: 6566
		public static readonly SpawnCondition Overworld;

		// Token: 0x040019A7 RID: 6567
		public static readonly SpawnCondition IceGolem;

		// Token: 0x040019A8 RID: 6568
		public static readonly SpawnCondition RainbowSlime;

		// Token: 0x040019A9 RID: 6569
		public static readonly SpawnCondition AngryNimbus;

		// Token: 0x040019AA RID: 6570
		public static readonly SpawnCondition MartianProbe;

		// Token: 0x040019AB RID: 6571
		public static readonly SpawnCondition OverworldDay;

		// Token: 0x040019AC RID: 6572
		public static readonly SpawnCondition OverworldDaySnowCritter;

		// Token: 0x040019AD RID: 6573
		public static readonly SpawnCondition OverworldDayGrassCritter;

		// Token: 0x040019AE RID: 6574
		public static readonly SpawnCondition OverworldDaySandCritter;

		// Token: 0x040019AF RID: 6575
		public static readonly SpawnCondition OverworldMorningBirdCritter;

		// Token: 0x040019B0 RID: 6576
		public static readonly SpawnCondition OverworldDayBirdCritter;

		// Token: 0x040019B1 RID: 6577
		public static readonly SpawnCondition KingSlime;

		// Token: 0x040019B2 RID: 6578
		public static readonly SpawnCondition OverworldDayDesert;

		// Token: 0x040019B3 RID: 6579
		public static readonly SpawnCondition GoblinScout;

		// Token: 0x040019B4 RID: 6580
		public static readonly SpawnCondition OverworldDayRain;

		// Token: 0x040019B5 RID: 6581
		public static readonly SpawnCondition OverworldDaySlime;

		// Token: 0x040019B6 RID: 6582
		public static readonly SpawnCondition OverworldNight;

		// Token: 0x040019B7 RID: 6583
		public static readonly SpawnCondition OverworldFirefly;

		// Token: 0x040019B8 RID: 6584
		public static readonly SpawnCondition OverworldNightMonster;

		// Token: 0x040019B9 RID: 6585
		public static readonly SpawnCondition Underground;

		// Token: 0x040019BA RID: 6586
		public static readonly SpawnCondition Underworld;

		// Token: 0x040019BB RID: 6587
		public static readonly SpawnCondition Cavern;
	}
}
