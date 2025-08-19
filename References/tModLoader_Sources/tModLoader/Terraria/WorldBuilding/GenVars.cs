using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.DataStructures;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200007A RID: 122
	public static class GenVars
	{
		// Token: 0x04000FF5 RID: 4085
		public static WorldGenConfiguration configuration;

		/// <inheritdoc cref="T:Terraria.WorldBuilding.StructureMap" />
		// Token: 0x04000FF6 RID: 4086
		public static StructureMap structures;

		// Token: 0x04000FF7 RID: 4087
		public static int copper;

		// Token: 0x04000FF8 RID: 4088
		public static int iron;

		// Token: 0x04000FF9 RID: 4089
		public static int silver;

		// Token: 0x04000FFA RID: 4090
		public static int gold;

		// Token: 0x04000FFB RID: 4091
		public static int copperBar = 20;

		// Token: 0x04000FFC RID: 4092
		public static int ironBar = 22;

		// Token: 0x04000FFD RID: 4093
		public static int silverBar = 21;

		// Token: 0x04000FFE RID: 4094
		public static int goldBar = 19;

		// Token: 0x04000FFF RID: 4095
		public static ushort mossTile = 179;

		// Token: 0x04001000 RID: 4096
		public static ushort mossWall = 54;

		// Token: 0x04001001 RID: 4097
		public static int lavaLine;

		// Token: 0x04001002 RID: 4098
		public static int waterLine;

		// Token: 0x04001003 RID: 4099
		public static double worldSurfaceLow;

		// Token: 0x04001004 RID: 4100
		public static double worldSurface;

		// Token: 0x04001005 RID: 4101
		public static double worldSurfaceHigh;

		// Token: 0x04001006 RID: 4102
		public static double rockLayerLow;

		// Token: 0x04001007 RID: 4103
		public static double rockLayer;

		// Token: 0x04001008 RID: 4104
		public static double rockLayerHigh;

		// Token: 0x04001009 RID: 4105
		public static int snowTop;

		// Token: 0x0400100A RID: 4106
		public static int snowBottom;

		// Token: 0x0400100B RID: 4107
		public static int snowOriginLeft;

		// Token: 0x0400100C RID: 4108
		public static int snowOriginRight;

		// Token: 0x0400100D RID: 4109
		public static int[] snowMinX;

		// Token: 0x0400100E RID: 4110
		public static int[] snowMaxX;

		// Token: 0x0400100F RID: 4111
		public static int leftBeachEnd;

		// Token: 0x04001010 RID: 4112
		public static int rightBeachStart;

		// Token: 0x04001011 RID: 4113
		public static int beachBordersWidth;

		// Token: 0x04001012 RID: 4114
		public static int beachSandRandomCenter;

		// Token: 0x04001013 RID: 4115
		public static int beachSandRandomWidthRange;

		// Token: 0x04001014 RID: 4116
		public static int beachSandDungeonExtraWidth;

		// Token: 0x04001015 RID: 4117
		public static int beachSandJungleExtraWidth;

		// Token: 0x04001016 RID: 4118
		public static int shellStartXLeft;

		// Token: 0x04001017 RID: 4119
		public static int shellStartYLeft;

		// Token: 0x04001018 RID: 4120
		public static int shellStartXRight;

		// Token: 0x04001019 RID: 4121
		public static int shellStartYRight;

		// Token: 0x0400101A RID: 4122
		public static int oceanWaterStartRandomMin;

		// Token: 0x0400101B RID: 4123
		public static int oceanWaterStartRandomMax;

		// Token: 0x0400101C RID: 4124
		public static int oceanWaterForcedJungleLength;

		// Token: 0x0400101D RID: 4125
		public static int evilBiomeBeachAvoidance;

		// Token: 0x0400101E RID: 4126
		public static int evilBiomeAvoidanceMidFixer;

		// Token: 0x0400101F RID: 4127
		public static int lakesBeachAvoidance;

		// Token: 0x04001020 RID: 4128
		public static int smallHolesBeachAvoidance;

		// Token: 0x04001021 RID: 4129
		public static int surfaceCavesBeachAvoidance;

		// Token: 0x04001022 RID: 4130
		public static int surfaceCavesBeachAvoidance2;

		// Token: 0x04001023 RID: 4131
		public static readonly int maxOceanCaveTreasure = 2;

		// Token: 0x04001024 RID: 4132
		public static int numOceanCaveTreasure = 0;

		// Token: 0x04001025 RID: 4133
		public static Point[] oceanCaveTreasure = new Point[GenVars.maxOceanCaveTreasure];

		// Token: 0x04001026 RID: 4134
		public static bool skipDesertTileCheck = false;

		// Token: 0x04001027 RID: 4135
		public static Rectangle UndergroundDesertLocation = Rectangle.Empty;

		// Token: 0x04001028 RID: 4136
		public static Rectangle UndergroundDesertHiveLocation = Rectangle.Empty;

		// Token: 0x04001029 RID: 4137
		public static int desertHiveHigh;

		// Token: 0x0400102A RID: 4138
		public static int desertHiveLow;

		// Token: 0x0400102B RID: 4139
		public static int desertHiveLeft;

		// Token: 0x0400102C RID: 4140
		public static int desertHiveRight;

		// Token: 0x0400102D RID: 4141
		public static int numLarva;

		// Token: 0x0400102E RID: 4142
		public static int[] larvaY = new int[100];

		// Token: 0x0400102F RID: 4143
		public static int[] larvaX = new int[100];

		// Token: 0x04001030 RID: 4144
		public static int numPyr;

		// Token: 0x04001031 RID: 4145
		public static int[] PyrX;

		// Token: 0x04001032 RID: 4146
		public static int[] PyrY;

		// Token: 0x04001033 RID: 4147
		public static int extraBastStatueCount;

		// Token: 0x04001034 RID: 4148
		public static int extraBastStatueCountMax;

		// Token: 0x04001035 RID: 4149
		public static int jungleOriginX;

		// Token: 0x04001036 RID: 4150
		public static int jungleMinX;

		// Token: 0x04001037 RID: 4151
		public static int jungleMaxX;

		// Token: 0x04001038 RID: 4152
		public static int JungleX;

		// Token: 0x04001039 RID: 4153
		public static ushort jungleHut;

		// Token: 0x0400103A RID: 4154
		public static bool mudWall;

		// Token: 0x0400103B RID: 4155
		public static int JungleItemCount;

		// Token: 0x0400103C RID: 4156
		public static int[] JChestX = new int[100];

		// Token: 0x0400103D RID: 4157
		public static int[] JChestY = new int[100];

		// Token: 0x0400103E RID: 4158
		public static int numJChests;

		// Token: 0x0400103F RID: 4159
		public static int tLeft;

		// Token: 0x04001040 RID: 4160
		public static int tRight;

		// Token: 0x04001041 RID: 4161
		public static int tTop;

		// Token: 0x04001042 RID: 4162
		public static int tBottom;

		// Token: 0x04001043 RID: 4163
		public static int tRooms;

		// Token: 0x04001044 RID: 4164
		public static int lAltarX;

		// Token: 0x04001045 RID: 4165
		public static int lAltarY;

		// Token: 0x04001046 RID: 4166
		public static int dungeonSide;

		// Token: 0x04001047 RID: 4167
		public static int dungeonLocation;

		// Token: 0x04001048 RID: 4168
		public static bool dungeonLake;

		// Token: 0x04001049 RID: 4169
		public static ushort crackedType = 481;

		// Token: 0x0400104A RID: 4170
		public static int dungeonX;

		// Token: 0x0400104B RID: 4171
		public static int dungeonY;

		// Token: 0x0400104C RID: 4172
		public static Vector2D lastDungeonHall = Vector2D.Zero;

		// Token: 0x0400104D RID: 4173
		public static readonly int maxDRooms = 100;

		// Token: 0x0400104E RID: 4174
		public static int numDRooms;

		// Token: 0x0400104F RID: 4175
		public static int[] dRoomX = new int[GenVars.maxDRooms];

		// Token: 0x04001050 RID: 4176
		public static int[] dRoomY = new int[GenVars.maxDRooms];

		// Token: 0x04001051 RID: 4177
		public static int[] dRoomSize = new int[GenVars.maxDRooms];

		// Token: 0x04001052 RID: 4178
		public static bool[] dRoomTreasure = new bool[GenVars.maxDRooms];

		// Token: 0x04001053 RID: 4179
		public static int[] dRoomL = new int[GenVars.maxDRooms];

		// Token: 0x04001054 RID: 4180
		public static int[] dRoomR = new int[GenVars.maxDRooms];

		// Token: 0x04001055 RID: 4181
		public static int[] dRoomT = new int[GenVars.maxDRooms];

		// Token: 0x04001056 RID: 4182
		public static int[] dRoomB = new int[GenVars.maxDRooms];

		// Token: 0x04001057 RID: 4183
		public static int numDDoors;

		// Token: 0x04001058 RID: 4184
		public static int[] DDoorX = new int[500];

		// Token: 0x04001059 RID: 4185
		public static int[] DDoorY = new int[500];

		// Token: 0x0400105A RID: 4186
		public static int[] DDoorPos = new int[500];

		// Token: 0x0400105B RID: 4187
		public static int numDungeonPlatforms;

		// Token: 0x0400105C RID: 4188
		public static int[] dungeonPlatformX = new int[500];

		// Token: 0x0400105D RID: 4189
		public static int[] dungeonPlatformY = new int[500];

		// Token: 0x0400105E RID: 4190
		public static int dEnteranceX;

		// Token: 0x0400105F RID: 4191
		public static bool dSurface;

		// Token: 0x04001060 RID: 4192
		public static double dxStrength1;

		// Token: 0x04001061 RID: 4193
		public static double dyStrength1;

		// Token: 0x04001062 RID: 4194
		public static double dxStrength2;

		// Token: 0x04001063 RID: 4195
		public static double dyStrength2;

		// Token: 0x04001064 RID: 4196
		public static int dMinX;

		// Token: 0x04001065 RID: 4197
		public static int dMaxX;

		// Token: 0x04001066 RID: 4198
		public static int dMinY;

		// Token: 0x04001067 RID: 4199
		public static int dMaxY;

		// Token: 0x04001068 RID: 4200
		public static int skyLakes;

		// Token: 0x04001069 RID: 4201
		public static bool generatedShadowKey;

		// Token: 0x0400106A RID: 4202
		public static int numIslandHouses;

		// Token: 0x0400106B RID: 4203
		public static int skyIslandHouseCount;

		// Token: 0x0400106C RID: 4204
		public static bool[] skyLake = new bool[30];

		// Token: 0x0400106D RID: 4205
		public static int[] floatingIslandHouseX = new int[30];

		// Token: 0x0400106E RID: 4206
		public static int[] floatingIslandHouseY = new int[30];

		// Token: 0x0400106F RID: 4207
		public static int[] floatingIslandStyle = new int[30];

		// Token: 0x04001070 RID: 4208
		public static int numMCaves;

		// Token: 0x04001071 RID: 4209
		public static int[] mCaveX = new int[30];

		// Token: 0x04001072 RID: 4210
		public static int[] mCaveY = new int[30];

		// Token: 0x04001073 RID: 4211
		public static readonly int maxTunnels = 50;

		// Token: 0x04001074 RID: 4212
		public static int numTunnels;

		// Token: 0x04001075 RID: 4213
		public static int[] tunnelX = new int[GenVars.maxTunnels];

		// Token: 0x04001076 RID: 4214
		public static readonly int maxOrePatch = 50;

		// Token: 0x04001077 RID: 4215
		public static int numOrePatch;

		// Token: 0x04001078 RID: 4216
		public static int[] orePatchX = new int[GenVars.maxOrePatch];

		// Token: 0x04001079 RID: 4217
		public static readonly int maxMushroomBiomes = 50;

		// Token: 0x0400107A RID: 4218
		public static int numMushroomBiomes = 0;

		// Token: 0x0400107B RID: 4219
		public static Point[] mushroomBiomesPosition = new Point[GenVars.maxMushroomBiomes];

		// Token: 0x0400107C RID: 4220
		public static int logX;

		// Token: 0x0400107D RID: 4221
		public static int logY;

		// Token: 0x0400107E RID: 4222
		public static readonly int maxLakes = 50;

		// Token: 0x0400107F RID: 4223
		public static int numLakes = 0;

		// Token: 0x04001080 RID: 4224
		public static int[] LakeX = new int[GenVars.maxLakes];

		// Token: 0x04001081 RID: 4225
		public static readonly int maxOasis = 20;

		// Token: 0x04001082 RID: 4226
		public static int numOasis = 0;

		// Token: 0x04001083 RID: 4227
		public static Point[] oasisPosition = new Point[GenVars.maxOasis];

		// Token: 0x04001084 RID: 4228
		public static int[] oasisWidth = new int[GenVars.maxOasis];

		// Token: 0x04001085 RID: 4229
		public static readonly int oasisHeight = 20;

		// Token: 0x04001086 RID: 4230
		public static int hellChest;

		// Token: 0x04001087 RID: 4231
		public static int[] hellChestItem;

		/// <summary>
		/// Statue options for natural world generation placement. Each Point16 corresponds to a tile type and tile style pair.
		/// </summary>
		// Token: 0x04001088 RID: 4232
		public static Point16[] statueList;

		/// <summary>
		/// Statues in this list will be generated with a connecting wire terminating in a pressure plate tile. The values in this list correspond to the indexes of the statue options within <see cref="F:Terraria.WorldBuilding.GenVars.statueList" />. Make sure to also set <see cref="F:Terraria.ID.TileID.Sets.IsAMechanism" /> for the tile type, otherwise the trap and wires will be removed during the "Remove Broken Traps" world generation step.
		/// </summary>
		// Token: 0x04001089 RID: 4233
		public static List<int> StatuesWithTraps = new List<int>(new int[]
		{
			4,
			7,
			10,
			18
		});

		// Token: 0x0400108A RID: 4234
		public static bool crimsonLeft = true;

		// Token: 0x0400108B RID: 4235
		public static Vector2D shimmerPosition;
	}
}
