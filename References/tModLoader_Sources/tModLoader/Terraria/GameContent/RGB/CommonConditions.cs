using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200057C RID: 1404
	public static class CommonConditions
	{
		// Token: 0x0400592C RID: 22828
		public static readonly ChromaCondition InMenu = new CommonConditions.SimpleCondition((Player player) => Main.gameMenu && !Main.drunkWorld);

		// Token: 0x0400592D RID: 22829
		public static readonly ChromaCondition DrunkMenu = new CommonConditions.SimpleCondition((Player player) => Main.gameMenu && Main.drunkWorld);

		// Token: 0x02000C53 RID: 3155
		public abstract class ConditionBase : ChromaCondition
		{
			// Token: 0x17000964 RID: 2404
			// (get) Token: 0x06005FC9 RID: 24521 RVA: 0x006D0E19 File Offset: 0x006CF019
			protected Player CurrentPlayer
			{
				get
				{
					return Main.player[Main.myPlayer];
				}
			}
		}

		// Token: 0x02000C54 RID: 3156
		private class SimpleCondition : CommonConditions.ConditionBase
		{
			// Token: 0x06005FCB RID: 24523 RVA: 0x006D0E2E File Offset: 0x006CF02E
			public SimpleCondition(Func<Player, bool> condition)
			{
				this._condition = condition;
			}

			// Token: 0x06005FCC RID: 24524 RVA: 0x006D0E3D File Offset: 0x006CF03D
			public override bool IsActive()
			{
				return this._condition(base.CurrentPlayer);
			}

			// Token: 0x0400791C RID: 31004
			private Func<Player, bool> _condition;
		}

		// Token: 0x02000C55 RID: 3157
		public static class SurfaceBiome
		{
			// Token: 0x0400791D RID: 31005
			public static readonly ChromaCondition Ocean = new CommonConditions.SimpleCondition((Player player) => player.ZoneBeach && player.ZoneOverworldHeight);

			// Token: 0x0400791E RID: 31006
			public static readonly ChromaCondition Desert = new CommonConditions.SimpleCondition((Player player) => player.ZoneDesert && !player.ZoneBeach && player.ZoneOverworldHeight);

			// Token: 0x0400791F RID: 31007
			public static readonly ChromaCondition Jungle = new CommonConditions.SimpleCondition((Player player) => player.ZoneJungle && player.ZoneOverworldHeight);

			// Token: 0x04007920 RID: 31008
			public static readonly ChromaCondition Snow = new CommonConditions.SimpleCondition((Player player) => player.ZoneSnow && player.ZoneOverworldHeight);

			// Token: 0x04007921 RID: 31009
			public static readonly ChromaCondition Mushroom = new CommonConditions.SimpleCondition((Player player) => player.ZoneGlowshroom && player.ZoneOverworldHeight);

			// Token: 0x04007922 RID: 31010
			public static readonly ChromaCondition Corruption = new CommonConditions.SimpleCondition((Player player) => player.ZoneCorrupt && player.ZoneOverworldHeight);

			// Token: 0x04007923 RID: 31011
			public static readonly ChromaCondition Hallow = new CommonConditions.SimpleCondition((Player player) => player.ZoneHallow && player.ZoneOverworldHeight);

			// Token: 0x04007924 RID: 31012
			public static readonly ChromaCondition Crimson = new CommonConditions.SimpleCondition((Player player) => player.ZoneCrimson && player.ZoneOverworldHeight);
		}

		// Token: 0x02000C56 RID: 3158
		public static class MiscBiome
		{
			// Token: 0x04007925 RID: 31013
			public static readonly ChromaCondition Meteorite = new CommonConditions.SimpleCondition((Player player) => player.ZoneMeteor);
		}

		// Token: 0x02000C57 RID: 3159
		public static class UndergroundBiome
		{
			// Token: 0x06005FCF RID: 24527 RVA: 0x006D0F4C File Offset: 0x006CF14C
			private unsafe static bool InTemple(Player player)
			{
				int num = (int)(player.position.X + (float)(player.width / 2)) / 16;
				int num2 = (int)(player.position.Y + (float)(player.height / 2)) / 16;
				return WorldGen.InWorld(num, num2, 0) && Main.tile[num, num2] != null && *Main.tile[num, num2].wall == 87;
			}

			// Token: 0x06005FD0 RID: 24528 RVA: 0x006D0FC6 File Offset: 0x006CF1C6
			private static bool InIce(Player player)
			{
				return player.ZoneSnow && !player.ZoneOverworldHeight;
			}

			// Token: 0x06005FD1 RID: 24529 RVA: 0x006D0FDB File Offset: 0x006CF1DB
			private static bool InDesert(Player player)
			{
				return player.ZoneDesert && !player.ZoneOverworldHeight;
			}

			// Token: 0x04007926 RID: 31014
			public static readonly ChromaCondition Hive = new CommonConditions.SimpleCondition((Player player) => player.ZoneHive);

			// Token: 0x04007927 RID: 31015
			public static readonly ChromaCondition Jungle = new CommonConditions.SimpleCondition((Player player) => player.ZoneJungle && !player.ZoneOverworldHeight);

			// Token: 0x04007928 RID: 31016
			public static readonly ChromaCondition Mushroom = new CommonConditions.SimpleCondition((Player player) => player.ZoneGlowshroom && !player.ZoneOverworldHeight);

			// Token: 0x04007929 RID: 31017
			public static readonly ChromaCondition Ice = new CommonConditions.SimpleCondition(new Func<Player, bool>(CommonConditions.UndergroundBiome.InIce));

			// Token: 0x0400792A RID: 31018
			public static readonly ChromaCondition HallowIce = new CommonConditions.SimpleCondition((Player player) => CommonConditions.UndergroundBiome.InIce(player) && player.ZoneHallow);

			// Token: 0x0400792B RID: 31019
			public static readonly ChromaCondition CrimsonIce = new CommonConditions.SimpleCondition((Player player) => CommonConditions.UndergroundBiome.InIce(player) && player.ZoneCrimson);

			// Token: 0x0400792C RID: 31020
			public static readonly ChromaCondition CorruptIce = new CommonConditions.SimpleCondition((Player player) => CommonConditions.UndergroundBiome.InIce(player) && player.ZoneCorrupt);

			// Token: 0x0400792D RID: 31021
			public static readonly ChromaCondition Hallow = new CommonConditions.SimpleCondition((Player player) => player.ZoneHallow && !player.ZoneOverworldHeight);

			// Token: 0x0400792E RID: 31022
			public static readonly ChromaCondition Crimson = new CommonConditions.SimpleCondition((Player player) => player.ZoneCrimson && !player.ZoneOverworldHeight);

			// Token: 0x0400792F RID: 31023
			public static readonly ChromaCondition Corrupt = new CommonConditions.SimpleCondition((Player player) => player.ZoneCorrupt && !player.ZoneOverworldHeight);

			// Token: 0x04007930 RID: 31024
			public static readonly ChromaCondition Desert = new CommonConditions.SimpleCondition(new Func<Player, bool>(CommonConditions.UndergroundBiome.InDesert));

			// Token: 0x04007931 RID: 31025
			public static readonly ChromaCondition HallowDesert = new CommonConditions.SimpleCondition((Player player) => CommonConditions.UndergroundBiome.InDesert(player) && player.ZoneHallow);

			// Token: 0x04007932 RID: 31026
			public static readonly ChromaCondition CrimsonDesert = new CommonConditions.SimpleCondition((Player player) => CommonConditions.UndergroundBiome.InDesert(player) && player.ZoneCrimson);

			// Token: 0x04007933 RID: 31027
			public static readonly ChromaCondition CorruptDesert = new CommonConditions.SimpleCondition((Player player) => CommonConditions.UndergroundBiome.InDesert(player) && player.ZoneCorrupt);

			// Token: 0x04007934 RID: 31028
			public static readonly ChromaCondition Temple = new CommonConditions.SimpleCondition(new Func<Player, bool>(CommonConditions.UndergroundBiome.InTemple));

			// Token: 0x04007935 RID: 31029
			public static readonly ChromaCondition Dungeon = new CommonConditions.SimpleCondition((Player player) => player.ZoneDungeon);

			// Token: 0x04007936 RID: 31030
			public static readonly ChromaCondition Marble = new CommonConditions.SimpleCondition((Player player) => player.ZoneMarble);

			// Token: 0x04007937 RID: 31031
			public static readonly ChromaCondition Granite = new CommonConditions.SimpleCondition((Player player) => player.ZoneGranite);

			// Token: 0x04007938 RID: 31032
			public static readonly ChromaCondition GemCave = new CommonConditions.SimpleCondition((Player player) => player.ZoneGemCave);

			// Token: 0x04007939 RID: 31033
			public static readonly ChromaCondition Shimmer = new CommonConditions.SimpleCondition((Player player) => player.ZoneShimmer);
		}

		// Token: 0x02000C58 RID: 3160
		public static class Boss
		{
			// Token: 0x0400793A RID: 31034
			public static int HighestTierBossOrEvent;

			// Token: 0x0400793B RID: 31035
			public static readonly ChromaCondition EaterOfWorlds = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 13);

			// Token: 0x0400793C RID: 31036
			public static readonly ChromaCondition Destroyer = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 134);

			// Token: 0x0400793D RID: 31037
			public static readonly ChromaCondition KingSlime = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 50);

			// Token: 0x0400793E RID: 31038
			public static readonly ChromaCondition QueenSlime = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 657);

			// Token: 0x0400793F RID: 31039
			public static readonly ChromaCondition BrainOfCthulhu = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 266);

			// Token: 0x04007940 RID: 31040
			public static readonly ChromaCondition DukeFishron = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 370);

			// Token: 0x04007941 RID: 31041
			public static readonly ChromaCondition QueenBee = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 222);

			// Token: 0x04007942 RID: 31042
			public static readonly ChromaCondition Plantera = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 262);

			// Token: 0x04007943 RID: 31043
			public static readonly ChromaCondition Empress = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 636);

			// Token: 0x04007944 RID: 31044
			public static readonly ChromaCondition EyeOfCthulhu = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 4);

			// Token: 0x04007945 RID: 31045
			public static readonly ChromaCondition TheTwins = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 126);

			// Token: 0x04007946 RID: 31046
			public static readonly ChromaCondition MoonLord = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 398);

			// Token: 0x04007947 RID: 31047
			public static readonly ChromaCondition WallOfFlesh = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 113);

			// Token: 0x04007948 RID: 31048
			public static readonly ChromaCondition Golem = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 245);

			// Token: 0x04007949 RID: 31049
			public static readonly ChromaCondition Cultist = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 439);

			// Token: 0x0400794A RID: 31050
			public static readonly ChromaCondition Skeletron = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 35);

			// Token: 0x0400794B RID: 31051
			public static readonly ChromaCondition SkeletronPrime = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 127);

			// Token: 0x0400794C RID: 31052
			public static readonly ChromaCondition Deerclops = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == 668);
		}

		// Token: 0x02000C59 RID: 3161
		public static class Weather
		{
			// Token: 0x0400794D RID: 31053
			public static readonly ChromaCondition Rain = new CommonConditions.SimpleCondition((Player player) => player.ZoneRain && !player.ZoneSnow && !player.ZoneSandstorm);

			// Token: 0x0400794E RID: 31054
			public static readonly ChromaCondition Sandstorm = new CommonConditions.SimpleCondition((Player player) => player.ZoneSandstorm);

			// Token: 0x0400794F RID: 31055
			public static readonly ChromaCondition Blizzard = new CommonConditions.SimpleCondition((Player player) => player.ZoneSnow && player.ZoneRain);

			// Token: 0x04007950 RID: 31056
			public static readonly ChromaCondition SlimeRain = new CommonConditions.SimpleCondition((Player player) => Main.slimeRain && player.ZoneOverworldHeight);
		}

		// Token: 0x02000C5A RID: 3162
		public static class Depth
		{
			// Token: 0x06005FD5 RID: 24533 RVA: 0x006D1458 File Offset: 0x006CF658
			private unsafe static bool IsPlayerInFrontOfDirtWall(Player player)
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
				ushort num = *Main.tile[point.X, point.Y].wall;
				if (num <= 61)
				{
					if (num <= 16)
					{
						if (num != 2 && num != 16)
						{
							return false;
						}
					}
					else if (num - 54 > 5 && num != 61)
					{
						return false;
					}
				}
				else if (num <= 185)
				{
					if (num - 170 > 1 && num != 185)
					{
						return false;
					}
				}
				else if (num - 196 > 3 && num - 212 > 3)
				{
					return false;
				}
				return true;
			}

			// Token: 0x04007951 RID: 31057
			public static readonly ChromaCondition Sky = new CommonConditions.SimpleCondition((Player player) => (double)(player.position.Y / 16f) < Main.worldSurface * 0.44999998807907104);

			// Token: 0x04007952 RID: 31058
			public static readonly ChromaCondition Surface = new CommonConditions.SimpleCondition((Player player) => player.ZoneOverworldHeight && (double)(player.position.Y / 16f) >= Main.worldSurface * 0.44999998807907104 && !CommonConditions.Depth.IsPlayerInFrontOfDirtWall(player));

			// Token: 0x04007953 RID: 31059
			public static readonly ChromaCondition Vines = new CommonConditions.SimpleCondition((Player player) => player.ZoneOverworldHeight && (double)(player.position.Y / 16f) >= Main.worldSurface * 0.44999998807907104 && CommonConditions.Depth.IsPlayerInFrontOfDirtWall(player));

			// Token: 0x04007954 RID: 31060
			public static readonly ChromaCondition Underground = new CommonConditions.SimpleCondition((Player player) => player.ZoneDirtLayerHeight);

			// Token: 0x04007955 RID: 31061
			public static readonly ChromaCondition Caverns = new CommonConditions.SimpleCondition((Player player) => player.ZoneRockLayerHeight && player.position.ToTileCoordinates().Y <= Main.maxTilesY - 400);

			// Token: 0x04007956 RID: 31062
			public static readonly ChromaCondition Magma = new CommonConditions.SimpleCondition((Player player) => player.ZoneRockLayerHeight && player.position.ToTileCoordinates().Y > Main.maxTilesY - 400);

			// Token: 0x04007957 RID: 31063
			public static readonly ChromaCondition Underworld = new CommonConditions.SimpleCondition((Player player) => player.ZoneUnderworldHeight);
		}

		// Token: 0x02000C5B RID: 3163
		public static class Events
		{
			// Token: 0x04007958 RID: 31064
			public static readonly ChromaCondition BloodMoon = new CommonConditions.SimpleCondition((Player player) => Main.bloodMoon && !Main.snowMoon && !Main.pumpkinMoon);

			// Token: 0x04007959 RID: 31065
			public static readonly ChromaCondition FrostMoon = new CommonConditions.SimpleCondition((Player player) => Main.snowMoon);

			// Token: 0x0400795A RID: 31066
			public static readonly ChromaCondition PumpkinMoon = new CommonConditions.SimpleCondition((Player player) => Main.pumpkinMoon);

			// Token: 0x0400795B RID: 31067
			public static readonly ChromaCondition SolarEclipse = new CommonConditions.SimpleCondition((Player player) => Main.eclipse);

			// Token: 0x0400795C RID: 31068
			public static readonly ChromaCondition SolarPillar = new CommonConditions.SimpleCondition((Player player) => player.ZoneTowerSolar);

			// Token: 0x0400795D RID: 31069
			public static readonly ChromaCondition NebulaPillar = new CommonConditions.SimpleCondition((Player player) => player.ZoneTowerNebula);

			// Token: 0x0400795E RID: 31070
			public static readonly ChromaCondition VortexPillar = new CommonConditions.SimpleCondition((Player player) => player.ZoneTowerVortex);

			// Token: 0x0400795F RID: 31071
			public static readonly ChromaCondition StardustPillar = new CommonConditions.SimpleCondition((Player player) => player.ZoneTowerStardust);

			// Token: 0x04007960 RID: 31072
			public static readonly ChromaCondition PirateInvasion = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == -3);

			// Token: 0x04007961 RID: 31073
			public static readonly ChromaCondition DD2Event = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == -6);

			// Token: 0x04007962 RID: 31074
			public static readonly ChromaCondition FrostLegion = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == -2);

			// Token: 0x04007963 RID: 31075
			public static readonly ChromaCondition MartianMadness = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == -4);

			// Token: 0x04007964 RID: 31076
			public static readonly ChromaCondition GoblinArmy = new CommonConditions.SimpleCondition((Player player) => CommonConditions.Boss.HighestTierBossOrEvent == -1);
		}

		// Token: 0x02000C5C RID: 3164
		public static class Alert
		{
			// Token: 0x04007965 RID: 31077
			public static readonly ChromaCondition MoonlordComing = new CommonConditions.SimpleCondition((Player player) => NPC.MoonLordCountdown > 0);

			// Token: 0x04007966 RID: 31078
			public static readonly ChromaCondition Drowning = new CommonConditions.SimpleCondition((Player player) => player.breath != player.breathMax);

			// Token: 0x04007967 RID: 31079
			public static readonly ChromaCondition Keybinds = new CommonConditions.SimpleCondition((Player player) => Main.InGameUI.CurrentState == Main.ManageControlsMenu || Main.MenuUI.CurrentState == Main.ManageControlsMenu);

			// Token: 0x04007968 RID: 31080
			public static readonly ChromaCondition LavaIndicator = new CommonConditions.SimpleCondition((Player player) => player.lavaWet);
		}

		// Token: 0x02000C5D RID: 3165
		public static class CriticalAlert
		{
			// Token: 0x04007969 RID: 31081
			public static readonly ChromaCondition LowLife = new CommonConditions.SimpleCondition((Player player) => Main.ChromaPainter.PotionAlert);

			// Token: 0x0400796A RID: 31082
			public static readonly ChromaCondition Death = new CommonConditions.SimpleCondition((Player player) => player.dead);
		}
	}
}
