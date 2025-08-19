using System;
using System.Runtime.CompilerServices;

namespace Terraria.ID
{
	// Token: 0x02000410 RID: 1040
	internal static class ItemSourceID
	{
		// Token: 0x06003533 RID: 13619 RVA: 0x00570630 File Offset: 0x0056E830
		[NullableContext(2)]
		public static string ToContextString(int itemSourceId)
		{
			string result;
			switch (itemSourceId)
			{
			case 1:
				result = "SetBonus_Nebula";
				break;
			case 2:
				result = "LuckyCoin";
				break;
			case 3:
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Unexpected or invalid value: '");
				defaultInterpolatedStringHandler.AppendFormatted<int>(itemSourceId);
				defaultInterpolatedStringHandler.AppendLiteral("'.");
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			case 4:
				result = "ThrowItem";
				break;
			case 5:
				result = "GrandDesignOrMultiColorWrench";
				break;
			case 6:
				result = "TorchGod";
				break;
			case 7:
				result = "SortingWithNoSpace";
				break;
			case 8:
				result = "Shimmer";
				break;
			case 9:
				result = "Digesting";
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x04003D93 RID: 15763
		public const int None = 0;

		// Token: 0x04003D94 RID: 15764
		public const int SetBonus_Nebula = 1;

		// Token: 0x04003D95 RID: 15765
		public const int LuckyCoin = 2;

		// Token: 0x04003D96 RID: 15766
		public const int PlayerDeath = 3;

		// Token: 0x04003D97 RID: 15767
		public const int ThrowItem = 4;

		// Token: 0x04003D98 RID: 15768
		public const int GrandDesignOrMultiColorWrench = 5;

		// Token: 0x04003D99 RID: 15769
		public const int TorchGod = 6;

		// Token: 0x04003D9A RID: 15770
		public const int SortingWithNoSpace = 7;

		// Token: 0x04003D9B RID: 15771
		public const int Shimmer = 8;

		// Token: 0x04003D9C RID: 15772
		public const int Digesting = 9;

		// Token: 0x04003D9D RID: 15773
		public static readonly int Count = 10;
	}
}
