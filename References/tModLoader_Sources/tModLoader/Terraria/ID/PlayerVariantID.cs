using System;
using ReLogic.Reflection;

namespace Terraria.ID
{
	// Token: 0x02000421 RID: 1057
	public class PlayerVariantID
	{
		// Token: 0x04004334 RID: 17204
		public const int MaleStarter = 0;

		// Token: 0x04004335 RID: 17205
		public const int MaleSticker = 1;

		// Token: 0x04004336 RID: 17206
		public const int MaleGangster = 2;

		// Token: 0x04004337 RID: 17207
		public const int MaleCoat = 3;

		// Token: 0x04004338 RID: 17208
		public const int FemaleStarter = 4;

		// Token: 0x04004339 RID: 17209
		public const int FemaleSticker = 5;

		// Token: 0x0400433A RID: 17210
		public const int FemaleGangster = 6;

		// Token: 0x0400433B RID: 17211
		public const int FemaleCoat = 7;

		// Token: 0x0400433C RID: 17212
		public const int MaleDress = 8;

		// Token: 0x0400433D RID: 17213
		public const int FemaleDress = 9;

		// Token: 0x0400433E RID: 17214
		public const int MaleDisplayDoll = 10;

		// Token: 0x0400433F RID: 17215
		public const int FemaleDisplayDoll = 11;

		// Token: 0x04004340 RID: 17216
		public static readonly int Count = 12;

		// Token: 0x04004341 RID: 17217
		public static IdDictionary Search = IdDictionary.Create<PlayerVariantID, sbyte>();

		// Token: 0x02000B5A RID: 2906
		public class Sets
		{
			// Token: 0x040074E5 RID: 29925
			public static SetFactory Factory = new SetFactory(PlayerVariantID.Count, "PlayerVariantID", PlayerVariantID.Search);

			/// <summary>
			/// If <see langword="true" /> for a given skin variant (<see cref="F:Terraria.Player.skinVariant" />), then that variant is for a male (<see cref="P:Terraria.Player.Male" />) player.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074E6 RID: 29926
			public static bool[] Male = PlayerVariantID.Sets.Factory.CreateBoolSet(new int[]
			{
				0,
				1,
				2,
				3,
				8,
				10
			});

			/// <summary>
			/// Links a skin variant (<see cref="F:Terraria.Player.skinVariant" />) to the corresponding skin variant of the opposite gender.
			/// </summary>
			// Token: 0x040074E7 RID: 29927
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

			/// <summary>
			/// The order of skin variants (<see cref="F:Terraria.Player.skinVariant" />) for male players.
			/// </summary>
			// Token: 0x040074E8 RID: 29928
			public static int[] VariantOrderMale = new int[]
			{
				0,
				1,
				2,
				3,
				8,
				10
			};

			/// <summary>
			/// The order of skin variants (<see cref="F:Terraria.Player.skinVariant" />) for female players.
			/// </summary>
			// Token: 0x040074E9 RID: 29929
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
