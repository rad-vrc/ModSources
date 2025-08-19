using System;

namespace Terraria.ID
{
	// Token: 0x020001B6 RID: 438
	public static class PlayerVariantID
	{
		// Token: 0x04001DEE RID: 7662
		public const int MaleStarter = 0;

		// Token: 0x04001DEF RID: 7663
		public const int MaleSticker = 1;

		// Token: 0x04001DF0 RID: 7664
		public const int MaleGangster = 2;

		// Token: 0x04001DF1 RID: 7665
		public const int MaleCoat = 3;

		// Token: 0x04001DF2 RID: 7666
		public const int FemaleStarter = 4;

		// Token: 0x04001DF3 RID: 7667
		public const int FemaleSticker = 5;

		// Token: 0x04001DF4 RID: 7668
		public const int FemaleGangster = 6;

		// Token: 0x04001DF5 RID: 7669
		public const int FemaleCoat = 7;

		// Token: 0x04001DF6 RID: 7670
		public const int MaleDress = 8;

		// Token: 0x04001DF7 RID: 7671
		public const int FemaleDress = 9;

		// Token: 0x04001DF8 RID: 7672
		public const int MaleDisplayDoll = 10;

		// Token: 0x04001DF9 RID: 7673
		public const int FemaleDisplayDoll = 11;

		// Token: 0x04001DFA RID: 7674
		public static readonly int Count = 12;

		// Token: 0x020005E7 RID: 1511
		public class Sets
		{
			// Token: 0x04005EAE RID: 24238
			public static SetFactory Factory = new SetFactory(PlayerVariantID.Count);

			// Token: 0x04005EAF RID: 24239
			public static bool[] Male = PlayerVariantID.Sets.Factory.CreateBoolSet(new int[]
			{
				0,
				1,
				2,
				3,
				8,
				10
			});

			// Token: 0x04005EB0 RID: 24240
			public static int[] AltGenderReference = PlayerVariantID.Sets.Factory.CreateIntSet(0, new int[]
			{
				0,
				4,
				4,
				0,
				1,
				5,
				5,
				1,
				2,
				6,
				6,
				2,
				3,
				7,
				7,
				3,
				8,
				9,
				9,
				8,
				10,
				11,
				11,
				10
			});

			// Token: 0x04005EB1 RID: 24241
			public static int[] VariantOrderMale = new int[]
			{
				0,
				1,
				2,
				3,
				8,
				10
			};

			// Token: 0x04005EB2 RID: 24242
			public static int[] VariantOrderFemale = new int[]
			{
				4,
				5,
				6,
				7,
				9,
				11
			};
		}
	}
}
