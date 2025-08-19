using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.DataStructures;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200006D RID: 109
	public static class GenVars
	{
		// Token: 0x04000F14 RID: 3860
		public static WorldGenConfiguration configuration;

		// Token: 0x04000F15 RID: 3861
		public static StructureMap structures;

		// Token: 0x04000F16 RID: 3862
		public static int copper;

		// Token: 0x04000F17 RID: 3863
		public static int iron;

		// Token: 0x04000F18 RID: 3864
		public static int silver;

		// Token: 0x04000F19 RID: 3865
		public static int gold;

		// Token: 0x04000F1A RID: 3866
		public static int copperBar = 20;

		// Token: 0x04000F1B RID: 3867
		public static int ironBar = 22;

		// Token: 0x04000F1C RID: 3868
		public static int silverBar = 21;

		// Token: 0x04000F1D RID: 3869
		public static int goldBar = 19;

		// Token: 0x04000F1E RID: 3870
		public static ushort mossTile = 179;

		// Token: 0x04000F1F RID: 3871
		public static ushort mossWall = 54;

		// Token: 0x04000F20 RID: 3872
		public static int lavaLine;

		// Token: 0x04000F21 RID: 3873
		public static int waterLine;

		// Token: 0x04000F22 RID: 3874
		public static double worldSurfaceLow;

		// Token: 0x04000F23 RID: 3875
		public static double worldSurface;

		// Token: 0x04000F24 RID: 3876
		public static double worldSurfaceHigh;

		// Token: 0x04000F25 RID: 3877
		public static double rockLayerLow;

		// Token: 0x04000F26 RID: 3878
		public static double rockLayer;

		// Token: 0x04000F27 RID: 3879
		public static double rockLayerHigh;

		// Token: 0x04000F28 RID: 3880
		public static int snowTop;

		// Token: 0x04000F29 RID: 3881
		public static int snowBottom;

		// Token: 0x04000F2A RID: 3882
		public static int snowOriginLeft;

		// Token: 0x04000F2B RID: 3883
		public static int snowOriginRight;

		// Token: 0x04000F2C RID: 3884
		public static int[] snowMinX;

		// Token: 0x04000F2D RID: 3885
		public static int[] snowMaxX;

		// Token: 0x04000F2E RID: 3886
		public static int leftBeachEnd;

		// Token: 0x04000F2F RID: 3887
		public static int rightBeachStart;

		// Token: 0x04000F30 RID: 3888
		public static int beachBordersWidth;

		// Token: 0x04000F31 RID: 3889
		public static int beachSandRandomCenter;

		// Token: 0x04000F32 RID: 3890
		public static int beachSandRandomWidthRange;

		// Token: 0x04000F33 RID: 3891
		public static int beachSandDungeonExtraWidth;

		// Token: 0x04000F34 RID: 3892
		public static int beachSandJungleExtraWidth;

		// Token: 0x04000F35 RID: 3893
		public static int shellStartXLeft;

		// Token: 0x04000F36 RID: 3894
		public static int shellStartYLeft;

		// Token: 0x04000F37 RID: 3895
		public static int shellStartXRight;

		// Token: 0x04000F38 RID: 3896
		public static int shellStartYRight;

		// Token: 0x04000F39 RID: 3897
		public static int oceanWaterStartRandomMin;

		// Token: 0x04000F3A RID: 3898
		public static int oceanWaterStartRandomMax;

		// Token: 0x04000F3B RID: 3899
		public static int oceanWaterForcedJungleLength;

		// Token: 0x04000F3C RID: 3900
		public static int evilBiomeBeachAvoidance;

		// Token: 0x04000F3D RID: 3901
		public static int evilBiomeAvoidanceMidFixer;

		// Token: 0x04000F3E RID: 3902
		public static int lakesBeachAvoidance;

		// Token: 0x04000F3F RID: 3903
		public static int smallHolesBeachAvoidance;

		// Token: 0x04000F40 RID: 3904
		public static int surfaceCavesBeachAvoidance;

		// Token: 0x04000F41 RID: 3905
		public static int surfaceCavesBeachAvoidance2;

		// Token: 0x04000F42 RID: 3906
		public static readonly int maxOceanCaveTreasure = 2;

		// Token: 0x04000F43 RID: 3907
		public static int numOceanCaveTreasure = 0;

		// Token: 0x04000F44 RID: 3908
		public static Point[] oceanCaveTreasure = new Point[GenVars.maxOceanCaveTreasure];

		// Token: 0x04000F45 RID: 3909
		public static bool skipDesertTileCheck = false;

		// Token: 0x04000F46 RID: 3910
		public static Rectangle UndergroundDesertLocation = Rectangle.Empty;

		// Token: 0x04000F47 RID: 3911
		public static Rectangle UndergroundDesertHiveLocation = Rectangle.Empty;

		// Token: 0x04000F48 RID: 3912
		public static int desertHiveHigh;

		// Token: 0x04000F49 RID: 3913
		public static int desertHiveLow;

		// Token: 0x04000F4A RID: 3914
		public static int desertHiveLeft;

		// Token: 0x04000F4B RID: 3915
		public static int desertHiveRight;

		// Token: 0x04000F4C RID: 3916
		public static int numLarva;

		// Token: 0x04000F4D RID: 3917
		public static int[] larvaY = new int[100];

		// Token: 0x04000F4E RID: 3918
		public static int[] larvaX = new int[100];

		// Token: 0x04000F4F RID: 3919
		public static int numPyr;

		// Token: 0x04000F50 RID: 3920
		public static int[] PyrX;

		// Token: 0x04000F51 RID: 3921
		public static int[] PyrY;

		// Token: 0x04000F52 RID: 3922
		public static int extraBastStatueCount;

		// Token: 0x04000F53 RID: 3923
		public static int extraBastStatueCountMax;

		// Token: 0x04000F54 RID: 3924
		public static int jungleOriginX;

		// Token: 0x04000F55 RID: 3925
		public static int jungleMinX;

		// Token: 0x04000F56 RID: 3926
		public static int jungleMaxX;

		// Token: 0x04000F57 RID: 3927
		public static int JungleX;

		// Token: 0x04000F58 RID: 3928
		public static ushort jungleHut;

		// Token: 0x04000F59 RID: 3929
		public static bool mudWall;

		// Token: 0x04000F5A RID: 3930
		public static int JungleItemCount;

		// Token: 0x04000F5B RID: 3931
		public static int[] JChestX = new int[100];

		// Token: 0x04000F5C RID: 3932
		public static int[] JChestY = new int[100];

		// Token: 0x04000F5D RID: 3933
		public static int numJChests;

		// Token: 0x04000F5E RID: 3934
		public static int tLeft;

		// Token: 0x04000F5F RID: 3935
		public static int tRight;

		// Token: 0x04000F60 RID: 3936
		public static int tTop;

		// Token: 0x04000F61 RID: 3937
		public static int tBottom;

		// Token: 0x04000F62 RID: 3938
		public static int tRooms;

		// Token: 0x04000F63 RID: 3939
		public static int lAltarX;

		// Token: 0x04000F64 RID: 3940
		public static int lAltarY;

		// Token: 0x04000F65 RID: 3941
		public static int dungeonSide;

		// Token: 0x04000F66 RID: 3942
		public static int dungeonLocation;

		// Token: 0x04000F67 RID: 3943
		public static bool dungeonLake;

		// Token: 0x04000F68 RID: 3944
		public static ushort crackedType = 481;

		// Token: 0x04000F69 RID: 3945
		public static int dungeonX;

		// Token: 0x04000F6A RID: 3946
		public static int dungeonY;

		// Token: 0x04000F6B RID: 3947
		public static Vector2D lastDungeonHall = Vector2D.Zero;

		// Token: 0x04000F6C RID: 3948
		public static readonly int maxDRooms = 100;

		// Token: 0x04000F6D RID: 3949
		public static int numDRooms;

		// Token: 0x04000F6E RID: 3950
		public static int[] dRoomX = new int[GenVars.maxDRooms];

		// Token: 0x04000F6F RID: 3951
		public static int[] dRoomY = new int[GenVars.maxDRooms];

		// Token: 0x04000F70 RID: 3952
		public static int[] dRoomSize = new int[GenVars.maxDRooms];

		// Token: 0x04000F71 RID: 3953
		public static bool[] dRoomTreasure = new bool[GenVars.maxDRooms];

		// Token: 0x04000F72 RID: 3954
		public static int[] dRoomL = new int[GenVars.maxDRooms];

		// Token: 0x04000F73 RID: 3955
		public static int[] dRoomR = new int[GenVars.maxDRooms];

		// Token: 0x04000F74 RID: 3956
		public static int[] dRoomT = new int[GenVars.maxDRooms];

		// Token: 0x04000F75 RID: 3957
		public static int[] dRoomB = new int[GenVars.maxDRooms];

		// Token: 0x04000F76 RID: 3958
		public static int numDDoors;

		// Token: 0x04000F77 RID: 3959
		public static int[] DDoorX = new int[500];

		// Token: 0x04000F78 RID: 3960
		public static int[] DDoorY = new int[500];

		// Token: 0x04000F79 RID: 3961
		public static int[] DDoorPos = new int[500];

		// Token: 0x04000F7A RID: 3962
		public static int numDungeonPlatforms;

		// Token: 0x04000F7B RID: 3963
		public static int[] dungeonPlatformX = new int[500];

		// Token: 0x04000F7C RID: 3964
		public static int[] dungeonPlatformY = new int[500];

		// Token: 0x04000F7D RID: 3965
		public static int dEnteranceX;

		// Token: 0x04000F7E RID: 3966
		public static bool dSurface;

		// Token: 0x04000F7F RID: 3967
		public static double dxStrength1;

		// Token: 0x04000F80 RID: 3968
		public static double dyStrength1;

		// Token: 0x04000F81 RID: 3969
		public static double dxStrength2;

		// Token: 0x04000F82 RID: 3970
		public static double dyStrength2;

		// Token: 0x04000F83 RID: 3971
		public static int dMinX;

		// Token: 0x04000F84 RID: 3972
		public static int dMaxX;

		// Token: 0x04000F85 RID: 3973
		public static int dMinY;

		// Token: 0x04000F86 RID: 3974
		public static int dMaxY;

		// Token: 0x04000F87 RID: 3975
		public static int skyLakes;

		// Token: 0x04000F88 RID: 3976
		public static bool generatedShadowKey;

		// Token: 0x04000F89 RID: 3977
		public static int numIslandHouses;

		// Token: 0x04000F8A RID: 3978
		public static int skyIslandHouseCount;

		// Token: 0x04000F8B RID: 3979
		public static bool[] skyLake = new bool[30];

		// Token: 0x04000F8C RID: 3980
		public static int[] floatingIslandHouseX = new int[30];

		// Token: 0x04000F8D RID: 3981
		public static int[] floatingIslandHouseY = new int[30];

		// Token: 0x04000F8E RID: 3982
		public static int[] floatingIslandStyle = new int[30];

		// Token: 0x04000F8F RID: 3983
		public static int numMCaves;

		// Token: 0x04000F90 RID: 3984
		public static int[] mCaveX = new int[30];

		// Token: 0x04000F91 RID: 3985
		public static int[] mCaveY = new int[30];

		// Token: 0x04000F92 RID: 3986
		public static readonly int maxTunnels = 50;

		// Token: 0x04000F93 RID: 3987
		public static int numTunnels;

		// Token: 0x04000F94 RID: 3988
		public static int[] tunnelX = new int[GenVars.maxTunnels];

		// Token: 0x04000F95 RID: 3989
		public static readonly int maxOrePatch = 50;

		// Token: 0x04000F96 RID: 3990
		public static int numOrePatch;

		// Token: 0x04000F97 RID: 3991
		public static int[] orePatchX = new int[GenVars.maxOrePatch];

		// Token: 0x04000F98 RID: 3992
		public static readonly int maxMushroomBiomes = 50;

		// Token: 0x04000F99 RID: 3993
		public static int numMushroomBiomes = 0;

		// Token: 0x04000F9A RID: 3994
		public static Point[] mushroomBiomesPosition = new Point[GenVars.maxMushroomBiomes];

		// Token: 0x04000F9B RID: 3995
		public static int logX;

		// Token: 0x04000F9C RID: 3996
		public static int logY;

		// Token: 0x04000F9D RID: 3997
		public static readonly int maxLakes = 50;

		// Token: 0x04000F9E RID: 3998
		public static int numLakes = 0;

		// Token: 0x04000F9F RID: 3999
		public static int[] LakeX = new int[GenVars.maxLakes];

		// Token: 0x04000FA0 RID: 4000
		public static readonly int maxOasis = 20;

		// Token: 0x04000FA1 RID: 4001
		public static int numOasis = 0;

		// Token: 0x04000FA2 RID: 4002
		public static Point[] oasisPosition = new Point[GenVars.maxOasis];

		// Token: 0x04000FA3 RID: 4003
		public static int[] oasisWidth = new int[GenVars.maxOasis];

		// Token: 0x04000FA4 RID: 4004
		public static readonly int oasisHeight = 20;

		// Token: 0x04000FA5 RID: 4005
		public static int hellChest;

		// Token: 0x04000FA6 RID: 4006
		public static int[] hellChestItem;

		// Token: 0x04000FA7 RID: 4007
		public static Point16[] statueList;

		// Token: 0x04000FA8 RID: 4008
		public static List<int> StatuesWithTraps = new List<int>(new int[]
		{
			4,
			7,
			10,
			18
		});

		// Token: 0x04000FA9 RID: 4009
		public static bool crimsonLeft = true;

		// Token: 0x04000FAA RID: 4010
		public static Vector2D shimmerPosition;
	}
}
