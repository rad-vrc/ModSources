using System;
using ReLogic.Reflection;
using Terraria.ModLoader;

namespace Terraria.ID
{
	// Token: 0x02000416 RID: 1046
	public class MountID
	{
		// Token: 0x04003E8C RID: 16012
		public const int None = -1;

		// Token: 0x04003E8D RID: 16013
		public const int Rudolph = 0;

		// Token: 0x04003E8E RID: 16014
		public const int Bunny = 1;

		// Token: 0x04003E8F RID: 16015
		public const int Pigron = 2;

		// Token: 0x04003E90 RID: 16016
		public const int Slime = 3;

		// Token: 0x04003E91 RID: 16017
		public const int Turtle = 4;

		// Token: 0x04003E92 RID: 16018
		public const int Bee = 5;

		// Token: 0x04003E93 RID: 16019
		public const int Minecart = 6;

		// Token: 0x04003E94 RID: 16020
		public const int UFO = 7;

		// Token: 0x04003E95 RID: 16021
		public const int Drill = 8;

		// Token: 0x04003E96 RID: 16022
		public const int Scutlix = 9;

		// Token: 0x04003E97 RID: 16023
		public const int Unicorn = 10;

		// Token: 0x04003E98 RID: 16024
		public const int MinecartMech = 11;

		// Token: 0x04003E99 RID: 16025
		public const int CuteFishron = 12;

		// Token: 0x04003E9A RID: 16026
		public const int MinecartWood = 13;

		// Token: 0x04003E9B RID: 16027
		public const int Basilisk = 14;

		// Token: 0x04003E9C RID: 16028
		public const int DesertMinecart = 15;

		// Token: 0x04003E9D RID: 16029
		public const int FishMinecart = 16;

		// Token: 0x04003E9E RID: 16030
		public const int GolfCartSomebodySaveMe = 17;

		// Token: 0x04003E9F RID: 16031
		public const int BeeMinecart = 18;

		// Token: 0x04003EA0 RID: 16032
		public const int LadybugMinecart = 19;

		// Token: 0x04003EA1 RID: 16033
		public const int PigronMinecart = 20;

		// Token: 0x04003EA2 RID: 16034
		public const int SunflowerMinecart = 21;

		// Token: 0x04003EA3 RID: 16035
		public const int HellMinecart = 22;

		// Token: 0x04003EA4 RID: 16036
		public const int WitchBroom = 23;

		// Token: 0x04003EA5 RID: 16037
		public const int ShroomMinecart = 24;

		// Token: 0x04003EA6 RID: 16038
		public const int AmethystMinecart = 25;

		// Token: 0x04003EA7 RID: 16039
		public const int TopazMinecart = 26;

		// Token: 0x04003EA8 RID: 16040
		public const int SapphireMinecart = 27;

		// Token: 0x04003EA9 RID: 16041
		public const int EmeraldMinecart = 28;

		// Token: 0x04003EAA RID: 16042
		public const int RubyMinecart = 29;

		// Token: 0x04003EAB RID: 16043
		public const int DiamondMinecart = 30;

		// Token: 0x04003EAC RID: 16044
		public const int AmberMinecart = 31;

		// Token: 0x04003EAD RID: 16045
		public const int BeetleMinecart = 32;

		// Token: 0x04003EAE RID: 16046
		public const int MeowmereMinecart = 33;

		// Token: 0x04003EAF RID: 16047
		public const int PartyMinecart = 34;

		// Token: 0x04003EB0 RID: 16048
		public const int PirateMinecart = 35;

		// Token: 0x04003EB1 RID: 16049
		public const int SteampunkMinecart = 36;

		// Token: 0x04003EB2 RID: 16050
		public const int Flamingo = 37;

		// Token: 0x04003EB3 RID: 16051
		public const int CoffinMinecart = 38;

		// Token: 0x04003EB4 RID: 16052
		public const int DiggingMoleMinecart = 39;

		// Token: 0x04003EB5 RID: 16053
		public const int PaintedHorse = 40;

		// Token: 0x04003EB6 RID: 16054
		public const int MajesticHorse = 41;

		// Token: 0x04003EB7 RID: 16055
		public const int DarkHorse = 42;

		// Token: 0x04003EB8 RID: 16056
		public const int PogoStick = 43;

		// Token: 0x04003EB9 RID: 16057
		public const int PirateShip = 44;

		// Token: 0x04003EBA RID: 16058
		public const int SpookyWood = 45;

		// Token: 0x04003EBB RID: 16059
		public const int Santank = 46;

		// Token: 0x04003EBC RID: 16060
		public const int WallOfFleshGoat = 47;

		// Token: 0x04003EBD RID: 16061
		public const int DarkMageBook = 48;

		// Token: 0x04003EBE RID: 16062
		public const int LavaShark = 49;

		// Token: 0x04003EBF RID: 16063
		public const int QueenSlime = 50;

		// Token: 0x04003EC0 RID: 16064
		public const int FartMinecart = 51;

		// Token: 0x04003EC1 RID: 16065
		public const int Wolf = 52;

		// Token: 0x04003EC2 RID: 16066
		public const int TerraFartMinecart = 53;

		// Token: 0x04003EC3 RID: 16067
		public static int Count = 54;

		// Token: 0x04003EC4 RID: 16068
		public static IdDictionary Search = IdDictionary.Create<MountID, int>();

		// Token: 0x02000B56 RID: 2902
		public static class Sets
		{
			// Token: 0x04007489 RID: 29833
			public static SetFactory Factory = new SetFactory(MountLoader.MountCount, "MountID", MountID.Search);

			/// <summary>
			/// If <see langword="true" /> for a given <see cref="T:Terraria.ID.MountID" />, then that mount is categorized as a minecart.
			/// <br /> Minecarts have a dedicated equipment slot, will be auto-used if the player interacts with a <see cref="F:Terraria.ID.TileID.MinecartTrack" />, and are buffed if <see cref="P:Terraria.Player.UsingSuperCart" /> is <see langword="true" />.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x0400748A RID: 29834
			public static bool[] Cart = MountID.Sets.Factory.CreateBoolSet(new int[]
			{
				6,
				11,
				13,
				15,
				16,
				18,
				19,
				20,
				21,
				22,
				24,
				25,
				26,
				27,
				28,
				29,
				30,
				31,
				32,
				33,
				34,
				35,
				36,
				38,
				39,
				51,
				53
			});

			/// <summary>
			/// If <see langword="true" /> for a given <see cref="T:Terraria.ID.MountID" />, then that mount will face the player's velocity instead of their <see cref="F:Terraria.Entity.direction" />.
			/// <br /> Vanilla uses this set for many minecarts.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x0400748B RID: 29835
			public static bool[] FacePlayersVelocity = MountID.Sets.Factory.CreateBoolSet(new int[]
			{
				15,
				16,
				11,
				18,
				19,
				20,
				21,
				22,
				24,
				25,
				26,
				27,
				28,
				29,
				30,
				31,
				32,
				33,
				34,
				35,
				36,
				38,
				39,
				51,
				53
			});
		}
	}
}
