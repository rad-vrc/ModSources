using System;

namespace Terraria.ID
{
	// Token: 0x020001B5 RID: 437
	public static class MountID
	{
		// Token: 0x04001DB6 RID: 7606
		public const int None = -1;

		// Token: 0x04001DB7 RID: 7607
		public const int Rudolph = 0;

		// Token: 0x04001DB8 RID: 7608
		public const int Bunny = 1;

		// Token: 0x04001DB9 RID: 7609
		public const int Pigron = 2;

		// Token: 0x04001DBA RID: 7610
		public const int Slime = 3;

		// Token: 0x04001DBB RID: 7611
		public const int Turtle = 4;

		// Token: 0x04001DBC RID: 7612
		public const int Bee = 5;

		// Token: 0x04001DBD RID: 7613
		public const int Minecart = 6;

		// Token: 0x04001DBE RID: 7614
		public const int UFO = 7;

		// Token: 0x04001DBF RID: 7615
		public const int Drill = 8;

		// Token: 0x04001DC0 RID: 7616
		public const int Scutlix = 9;

		// Token: 0x04001DC1 RID: 7617
		public const int Unicorn = 10;

		// Token: 0x04001DC2 RID: 7618
		public const int MinecartMech = 11;

		// Token: 0x04001DC3 RID: 7619
		public const int CuteFishron = 12;

		// Token: 0x04001DC4 RID: 7620
		public const int MinecartWood = 13;

		// Token: 0x04001DC5 RID: 7621
		public const int Basilisk = 14;

		// Token: 0x04001DC6 RID: 7622
		public const int DesertMinecart = 15;

		// Token: 0x04001DC7 RID: 7623
		public const int FishMinecart = 16;

		// Token: 0x04001DC8 RID: 7624
		public const int GolfCartSomebodySaveMe = 17;

		// Token: 0x04001DC9 RID: 7625
		public const int BeeMinecart = 18;

		// Token: 0x04001DCA RID: 7626
		public const int LadybugMinecart = 19;

		// Token: 0x04001DCB RID: 7627
		public const int PigronMinecart = 20;

		// Token: 0x04001DCC RID: 7628
		public const int SunflowerMinecart = 21;

		// Token: 0x04001DCD RID: 7629
		public const int HellMinecart = 22;

		// Token: 0x04001DCE RID: 7630
		public const int WitchBroom = 23;

		// Token: 0x04001DCF RID: 7631
		public const int ShroomMinecart = 24;

		// Token: 0x04001DD0 RID: 7632
		public const int AmethystMinecart = 25;

		// Token: 0x04001DD1 RID: 7633
		public const int TopazMinecart = 26;

		// Token: 0x04001DD2 RID: 7634
		public const int SapphireMinecart = 27;

		// Token: 0x04001DD3 RID: 7635
		public const int EmeraldMinecart = 28;

		// Token: 0x04001DD4 RID: 7636
		public const int RubyMinecart = 29;

		// Token: 0x04001DD5 RID: 7637
		public const int DiamondMinecart = 30;

		// Token: 0x04001DD6 RID: 7638
		public const int AmberMinecart = 31;

		// Token: 0x04001DD7 RID: 7639
		public const int BeetleMinecart = 32;

		// Token: 0x04001DD8 RID: 7640
		public const int MeowmereMinecart = 33;

		// Token: 0x04001DD9 RID: 7641
		public const int PartyMinecart = 34;

		// Token: 0x04001DDA RID: 7642
		public const int PirateMinecart = 35;

		// Token: 0x04001DDB RID: 7643
		public const int SteampunkMinecart = 36;

		// Token: 0x04001DDC RID: 7644
		public const int Flamingo = 37;

		// Token: 0x04001DDD RID: 7645
		public const int CoffinMinecart = 38;

		// Token: 0x04001DDE RID: 7646
		public const int DiggingMoleMinecart = 39;

		// Token: 0x04001DDF RID: 7647
		public const int PaintedHorse = 40;

		// Token: 0x04001DE0 RID: 7648
		public const int MajesticHorse = 41;

		// Token: 0x04001DE1 RID: 7649
		public const int DarkHorse = 42;

		// Token: 0x04001DE2 RID: 7650
		public const int PogoStick = 43;

		// Token: 0x04001DE3 RID: 7651
		public const int PirateShip = 44;

		// Token: 0x04001DE4 RID: 7652
		public const int SpookyWood = 45;

		// Token: 0x04001DE5 RID: 7653
		public const int Santank = 46;

		// Token: 0x04001DE6 RID: 7654
		public const int WallOfFleshGoat = 47;

		// Token: 0x04001DE7 RID: 7655
		public const int DarkMageBook = 48;

		// Token: 0x04001DE8 RID: 7656
		public const int LavaShark = 49;

		// Token: 0x04001DE9 RID: 7657
		public const int QueenSlime = 50;

		// Token: 0x04001DEA RID: 7658
		public const int FartMinecart = 51;

		// Token: 0x04001DEB RID: 7659
		public const int Wolf = 52;

		// Token: 0x04001DEC RID: 7660
		public const int TerraFartMinecart = 53;

		// Token: 0x04001DED RID: 7661
		public static int Count = 54;

		// Token: 0x020005E6 RID: 1510
		public static class Sets
		{
			// Token: 0x04005EAB RID: 24235
			public static SetFactory Factory = new SetFactory(MountID.Count);

			// Token: 0x04005EAC RID: 24236
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

			// Token: 0x04005EAD RID: 24237
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
