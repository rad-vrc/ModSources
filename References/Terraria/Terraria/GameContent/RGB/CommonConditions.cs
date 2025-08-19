using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000252 RID: 594
	public static class CommonConditions
	{
		// Token: 0x04004663 RID: 18019
		public static readonly ChromaCondition InMenu = new CommonConditions.SimpleCondition((Player player) => Main.gameMenu && !Main.drunkWorld);

		// Token: 0x04004664 RID: 18020
		public static readonly ChromaCondition DrunkMenu = new CommonConditions.SimpleCondition((Player player) => Main.gameMenu && Main.drunkWorld);

		// Token: 0x02000631 RID: 1585
		public abstract class ConditionBase : ChromaCondition
		{
			// Token: 0x170003CC RID: 972
			// (get) Token: 0x060033B8 RID: 13240 RVA: 0x0033FD65 File Offset: 0x0033DF65
			protected Player CurrentPlayer
			{
				get
				{
					return Main.player[Main.myPlayer];
				}
			}
		}

		// Token: 0x02000632 RID: 1586
		private class SimpleCondition : CommonConditions.ConditionBase
		{
			// Token: 0x060033BA RID: 13242 RVA: 0x00606AA5 File Offset: 0x00604CA5
			public SimpleCondition(Func<Player, bool> condition)
			{
				this._condition = condition;
			}

			// Token: 0x060033BB RID: 13243 RVA: 0x00606AB4 File Offset: 0x00604CB4
			public override bool IsActive()
			{
				return this._condition(base.CurrentPlayer);
			}

			// Token: 0x040060F2 RID: 24818
			private Func<Player, bool> _condition;
		}

		// Token: 0x02000633 RID: 1587
		public static class SurfaceBiome
		{
			// Token: 0x040060F3 RID: 24819
			public static readonly ChromaCondition Ocean = new CommonConditions.SimpleCondition((Player player) => player.ZoneBeach && player.ZoneOverworldHeight);

			// Token: 0x040060F4 RID: 24820
			public static readonly ChromaCondition Desert = new CommonConditions.SimpleCondition((Player player) => player.ZoneDesert && !player.ZoneBeach && player.ZoneOverworldHeight);

			// Token: 0x040060F5 RID: 24821
			public static readonly ChromaCondition Jungle = new CommonConditions.SimpleCondition((Player player) => player.ZoneJungle && player.ZoneOverworldHeight);

			// Token: 0x040060F6 RID: 24822
			public static readonly ChromaCondition Snow = new CommonConditions.SimpleCondition((Player player) => player.ZoneSnow && player.ZoneOverworldHeight);

			// Token: 0x040060F7 RID: 24823
			public static readonly ChromaCondition Mushroom = new CommonConditions.SimpleCondition((Player player) => player.ZoneGlowshroom && player.ZoneOverworldHeight);

			// Token: 0x040060F8 RID: 24824
			public static readonly ChromaCondition Corruption = new CommonConditions.SimpleCondition((Player player) => player.ZoneCorrupt && player.ZoneOverworldHeight);

			// Token: 0x040060F9 RID: 24825
			public static readonly ChromaCondition Hallow = new CommonConditions.SimpleCondition((Player player) => player.ZoneHallow && player.ZoneOverworldHeight);

			// Token: 0x040060FA RID: 24826
			public static readonly ChromaCondition Crimson = new CommonConditions.SimpleCondition((Player player) => player.ZoneCrimson && player.ZoneOverworldHeight);
		}

		// Token: 0x02000634 RID: 1588
		public static class MiscBiome
		{
			// Token: 0x040060FB RID: 24827
			public static readonly ChromaCondition Meteorite = new CommonConditions.SimpleCondition((Player player) => player.ZoneMeteor);
		}

		// Token: 0x02000635 RID: 1589
		public static class UndergroundBiome
		{
			// Token: 0x060033BE RID: 13246 RVA: 0x00606BC4 File Offset: 0x00604DC4
			private static bool InTemple(Player player)
			{
				int num = (int)(player.position.X + (float)(player.width / 2)) / 16;
				int num2 = (int)(player.position.Y + (float)(player.height / 2)) / 16;
				return WorldGen.InWorld(num, num2, 0) && Main.tile[num, num2] != null && Main.tile[num, num2].wall == 87;
			}

			// Token: 0x060033BF RID: 13247 RVA: 0x00606C34 File Offset: 0x00604E34
			private static bool InIce(Player player)
			{
				return player.ZoneSnow && !player.ZoneOverworldHeight;
			}

			// Token: 0x060033C0 RID: 13248 RVA: 0x00606C49 File Offset: 0x00604E49
			private static bool InDesert(Player player)
			{
				return player.ZoneDesert && !player.ZoneOverworldHeight;
			}

			// Token: 0x040060FC RID: 24828
			public static readonly ChromaCondition Hive = new CommonConditions.SimpleCondition((Player player) => player.ZoneHive);

			// Token: 0x040060FD RID: 24829
			public static readonly ChromaCondition Jungle = new CommonConditions.SimpleCondition((Player player) => player.ZoneJungle && !player.ZoneOverworldHeight);

			// Token: 0x040060FE RID: 24830
			public static readonly ChromaCondition Mushroom = new CommonConditions.SimpleCondition((Player player) => player.ZoneGlowshroom && !player.ZoneOverworldHeight);

			// Token: 0x040060FF RID: 24831
			public static readonly ChromaCondition Ice = new CommonConditions.SimpleCondition(new Func<Player, bool>(CommonConditions.UndergroundBiome.InIce));

			// Token: 0x04006100 RID: 24832
			public static readonly ChromaCondition HallowIce = new CommonConditions.SimpleCondition((Player player) => CommonConditions.UndergroundBiome.InIce(player) && player.ZoneHallow);

			// Token: 0x04006101 RID: 24833
			public static readonly ChromaCondition CrimsonIce = new CommonConditions.SimpleCondition((Player player) => CommonConditions.UndergroundBiome.InIce(player) && player.ZoneCrimson);

			// Token: 0x04006102 RID: 24834
			public static readonly ChromaCondition CorruptIce = new CommonConditions.SimpleCondition((Player player) => CommonConditions.UndergroundBiome.InIce(player) && player.ZoneCorrupt);

			// Token: 0x04006103 RID: 24835
			public static readonly ChromaCondition Hallow = new CommonConditions.SimpleCondition((Player player) => player.ZoneHallow && !player.ZoneOverworldHeight);

			// Token: 0x04006104 RID: 24836
			public static readonly ChromaCondition Crimson = new CommonConditions.SimpleCondition((Player player) => player.ZoneCrimson && !player.ZoneOverworldHeight);

			// Token: 0x04006105 RID: 24837
			public static readonly ChromaCondition Corrupt = new CommonConditions.SimpleCondition((Player player) => player.ZoneCorrupt && !player.ZoneOverworldHeight);

			// Token: 0x04006106 RID: 24838
			public static readonly ChromaCondition Desert = new CommonConditions.SimpleCondition(new Func<Player, bool>(CommonConditions.UndergroundBiome.InDesert));

			// Token: 0x04006107 RID: 24839
			public static readonly ChromaCondition HallowDesert = new CommonConditions.SimpleCondition((Player player) => CommonConditions.UndergroundBiome.InDesert(player) && player.ZoneHallow);

			// Token: 0x04006108 RID: 24840
			public static readonly ChromaCondition CrimsonDesert = new CommonConditions.SimpleCondition((Player player) => CommonConditions.UndergroundBiome.InDesert(player) && player.ZoneCrimson);

			// Token: 0x04006109 RID: 24841
			public static readonly ChromaCondition CorruptDesert = new CommonConditions.SimpleCondition((Player player) => CommonConditions.UndergroundBiome.InDesert(player) && player.ZoneCorrupt);

			// Token: 0x0400610A RID: 24842
			public static readonly ChromaCondition Temple = new CommonConditions.SimpleCondition(new Func<Player, bool>(CommonConditions.UndergroundBiome.InTemple));

			// Token: 0x0400610B RID: 24843
			public static readonly ChromaCondition Dungeon = new CommonConditions.SimpleCondition((Player player) => player.ZoneDungeon);

			// Token: 0x0400610C RID: 24844
			public static readonly ChromaCondition Marble = new CommonConditions.SimpleCondition((Player player) => player.ZoneMarble);

			// Token: 0x0400610D RID: 24845
			public static readonly ChromaCondition Granite = new CommonConditions.SimpleCondition((Player player) => player.ZoneGranite);

			// Token: 0x0400610E RID: 24846
			public static readonly ChromaCondition GemCave = new CommonConditions.SimpleCondition((Player player) => player.ZoneGemCave);

			// Token: 0x0400610F RID: 24847
			public static readonly ChromaCondition Shimmer = new CommonConditions.SimpleCondition((Player player) => player.ZoneShimmer);
		}

		// Token: 0x02000636 RID: 1590
		public static class Boss
		{
			// Token: 0x04006110 RID: 24848
			public static int HighestTierBossOrEvent;

			// Token: 0x04006111 RID: 24849
			public static readonly ChromaCondition EaterOfWorlds = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 13);

			// Token: 0x04006112 RID: 24850
			public static readonly ChromaCondition Destroyer = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 134);

			// Token: 0x04006113 RID: 24851
			public static readonly ChromaCondition KingSlime = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 50);

			// Token: 0x04006114 RID: 24852
			public static readonly ChromaCondition QueenSlime = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 657);

			// Token: 0x04006115 RID: 24853
			public static readonly ChromaCondition BrainOfCthulhu = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 266);

			// Token: 0x04006116 RID: 24854
			public static readonly ChromaCondition DukeFishron = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 370);

			// Token: 0x04006117 RID: 24855
			public static readonly ChromaCondition QueenBee = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 222);

			// Token: 0x04006118 RID: 24856
			public static readonly ChromaCondition Plantera = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 262);

			// Token: 0x04006119 RID: 24857
			public static readonly ChromaCondition Empress = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 636);

			// Token: 0x0400611A RID: 24858
			public static readonly ChromaCondition EyeOfCthulhu = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 4);

			// Token: 0x0400611B RID: 24859
			public static readonly ChromaCondition TheTwins = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 126);

			// Token: 0x0400611C RID: 24860
			public static readonly ChromaCondition MoonLord = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 398);

			// Token: 0x0400611D RID: 24861
			public static readonly ChromaCondition WallOfFlesh = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 113);

			// Token: 0x0400611E RID: 24862
			public static readonly ChromaCondition Golem = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 245);

			// Token: 0x0400611F RID: 24863
			public static readonly ChromaCondition Cultist = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 439);

			// Token: 0x04006120 RID: 24864
			public static readonly ChromaCondition Skeletron = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 35);

			// Token: 0x04006121 RID: 24865
			public static readonly ChromaCondition SkeletronPrime = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 127);

			// Token: 0x04006122 RID: 24866
			public static readonly ChromaCondition Deerclops = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 668);
		}

		// Token: 0x02000637 RID: 1591
		public static class Weather
		{
			// Token: 0x04006123 RID: 24867
			public static readonly ChromaCondition Rain = new CommonConditions.SimpleCondition((Player player) => player.ZoneRain && !player.ZoneSnow && !player.ZoneSandstorm);

			// Token: 0x04006124 RID: 24868
			public static readonly ChromaCondition Sandstorm = new CommonConditions.SimpleCondition((Player player) => player.ZoneSandstorm);

			// Token: 0x04006125 RID: 24869
			public static readonly ChromaCondition Blizzard = new CommonConditions.SimpleCondition((Player player) => player.ZoneSnow && player.ZoneRain);

			// Token: 0x04006126 RID: 24870
			public static readonly ChromaCondition SlimeRain = new CommonConditions.SimpleCondition((Player player) => Main.slimeRain && player.ZoneOverworldHeight);
		}

		// Token: 0x02000638 RID: 1592
		public static class Depth
		{
			// Token: 0x060033C4 RID: 13252 RVA: 0x006070C8 File Offset: 0x006052C8
			private static bool IsPlayerInFrontOfDirtWall(Player player)
			{
				Point point = player.Center.ToTileCoordinates();
				if (!WorldGen.InWorld(point.X, point.Y, 0))
				{
					return false;
				}
				if (Main.tile[point.X, point.Y] == null)
				{
					return false;
				}
				ushort wall = Main.tile[point.X, point.Y].wall;
				if (wall <= 61)
				{
					if (wall <= 16)
					{
						if (wall != 2 && wall != 16)
						{
							return false;
						}
					}
					else if (wall - 54 > 5 && wall != 61)
					{
						return false;
					}
				}
				else if (wall <= 185)
				{
					if (wall - 170 > 1 && wall != 185)
					{
						return false;
					}
				}
				else if (wall - 196 > 3 && wall - 212 > 3)
				{
					return false;
				}
				return true;
			}

			// Token: 0x04006127 RID: 24871
			public static readonly ChromaCondition Sky = new CommonConditions.SimpleCondition((Player player) => (double)(player.position.Y / 16f) < Main.worldSurface * 0.44999998807907104);

			// Token: 0x04006128 RID: 24872
			public static readonly ChromaCondition Surface = new CommonConditions.SimpleCondition((Player player) => player.ZoneOverworldHeight && (double)(player.position.Y / 16f) >= Main.worldSurface * 0.44999998807907104 && !CommonConditions.Depth.IsPlayerInFrontOfDirtWall(player));

			// Token: 0x04006129 RID: 24873
			public static readonly ChromaCondition Vines = new CommonConditions.SimpleCondition((Player player) => player.ZoneOverworldHeight && (double)(player.position.Y / 16f) >= Main.worldSurface * 0.44999998807907104 && CommonConditions.Depth.IsPlayerInFrontOfDirtWall(player));

			// Token: 0x0400612A RID: 24874
			public static readonly ChromaCondition Underground = new CommonConditions.SimpleCondition((Player player) => player.ZoneDirtLayerHeight);

			// Token: 0x0400612B RID: 24875
			public static readonly ChromaCondition Caverns = new CommonConditions.SimpleCondition((Player player) => player.ZoneRockLayerHeight && player.position.ToTileCoordinates().Y <= Main.maxTilesY - 400);

			// Token: 0x0400612C RID: 24876
			public static readonly ChromaCondition Magma = new CommonConditions.SimpleCondition((Player player) => player.ZoneRockLayerHeight && player.position.ToTileCoordinates().Y > Main.maxTilesY - 400);

			// Token: 0x0400612D RID: 24877
			public static readonly ChromaCondition Underworld = new CommonConditions.SimpleCondition((Player player) => player.ZoneUnderworldHeight);
		}

		// Token: 0x02000639 RID: 1593
		public static class Events
		{
			// Token: 0x0400612E RID: 24878
			public static readonly ChromaCondition BloodMoon = new CommonConditions.SimpleCondition((Player player) => Main.bloodMoon && !Main.snowMoon && !Main.pumpkinMoon);

			// Token: 0x0400612F RID: 24879
			public static readonly ChromaCondition FrostMoon = new CommonConditions.SimpleCondition((Player player) => Main.snowMoon);

			// Token: 0x04006130 RID: 24880
			public static readonly ChromaCondition PumpkinMoon = new CommonConditions.SimpleCondition((Player player) => Main.pumpkinMoon);

			// Token: 0x04006131 RID: 24881
			public static readonly ChromaCondition SolarEclipse = new CommonConditions.SimpleCondition((Player player) => Main.eclipse);

			// Token: 0x04006132 RID: 24882
			public static readonly ChromaCondition SolarPillar = new CommonConditions.SimpleCondition((Player player) => player.ZoneTowerSolar);

			// Token: 0x04006133 RID: 24883
			public static readonly ChromaCondition NebulaPillar = new CommonConditions.SimpleCondition((Player player) => player.ZoneTowerNebula);

			// Token: 0x04006134 RID: 24884
			public static readonly ChromaCondition VortexPillar = new CommonConditions.SimpleCondition((Player player) => player.ZoneTowerVortex);

			// Token: 0x04006135 RID: 24885
			public static readonly ChromaCondition StardustPillar = new CommonConditions.SimpleCondition((Player player) => player.ZoneTowerStardust);

			// Token: 0x04006136 RID: 24886
			public static readonly ChromaCondition PirateInvasion = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == -3);

			// Token: 0x04006137 RID: 24887
			public static readonly ChromaCondition DD2Event = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == -6);

			// Token: 0x04006138 RID: 24888
			public static readonly ChromaCondition FrostLegion = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == -2);

			// Token: 0x04006139 RID: 24889
			public static readonly ChromaCondition MartianMadness = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == -4);

			// Token: 0x0400613A RID: 24890
			public static readonly ChromaCondition GoblinArmy = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == -1);
		}

		// Token: 0x0200063A RID: 1594
		public static class Alert
		{
			// Token: 0x0400613B RID: 24891
			public static readonly ChromaCondition MoonlordComing = new CommonConditions.SimpleCondition((Player player) => NPC.MoonLordCountdown > 0);

			// Token: 0x0400613C RID: 24892
			public static readonly ChromaCondition Drowning = new CommonConditions.SimpleCondition((Player player) => player.breath != player.breathMax);

			// Token: 0x0400613D RID: 24893
			public static readonly ChromaCondition Keybinds = new CommonConditions.SimpleCondition((Player player) => Main.InGameUI.CurrentState == Main.ManageControlsMenu || Main.MenuUI.CurrentState == Main.ManageControlsMenu);

			// Token: 0x0400613E RID: 24894
			public static readonly ChromaCondition LavaIndicator = new CommonConditions.SimpleCondition((Player player) => player.lavaWet);
		}

		// Token: 0x0200063B RID: 1595
		public static class CriticalAlert
		{
			// Token: 0x0400613F RID: 24895
			public static readonly ChromaCondition LowLife = new CommonConditions.SimpleCondition((Player player) => Main.ChromaPainter.PotionAlert);

			// Token: 0x04006140 RID: 24896
			public static readonly ChromaCondition Death = new CommonConditions.SimpleCondition((Player player) => player.dead);
		}
	}
}
