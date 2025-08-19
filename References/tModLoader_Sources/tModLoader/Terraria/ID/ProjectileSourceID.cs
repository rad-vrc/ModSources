using System;
using System.Runtime.CompilerServices;

namespace Terraria.ID
{
	// Token: 0x02000425 RID: 1061
	internal static class ProjectileSourceID
	{
		// Token: 0x06003552 RID: 13650 RVA: 0x00572568 File Offset: 0x00570768
		[NullableContext(2)]
		public static string ToContextString(int itemSourceId)
		{
			switch (itemSourceId)
			{
			case 1:
				return "SetBonus_SolarExplosion_WhenTakingDamage";
			case 2:
				return "SetBonus_SolarExplosion_WhenDashing";
			case 3:
				return "SetBonus_ForbiddenStorm";
			case 4:
				return "SetBonus_Titanium";
			case 5:
				return "SetBonus_Orichalcum";
			case 6:
				return "SetBonus_Chlorophyte";
			case 7:
				return "SetBonus_Stardust";
			case 8:
				return "WeaponEnchantment_Confetti";
			case 9:
				return "PlayerDeath_TombStone";
			case 10:
				return "TorchGod";
			case 11:
				return "FallingStar";
			case 12:
				return "PlayerHurt_DropFootball";
			case 13:
				return "StormTigerTierSwap";
			case 14:
				return "AbigailTierSwap";
			case 15:
				return "SetBonus_GhostHeal";
			case 16:
				return "SetBonus_GhostHurt";
			case 18:
				return "VampireKnives";
			}
			return null;
		}

		// Token: 0x0400485C RID: 18524
		public const int None = 0;

		// Token: 0x0400485D RID: 18525
		public const int SetBonus_SolarExplosion_WhenTakingDamage = 1;

		// Token: 0x0400485E RID: 18526
		public const int SetBonus_SolarExplosion_WhenDashing = 2;

		// Token: 0x0400485F RID: 18527
		public const int SetBonus_ForbiddenStorm = 3;

		// Token: 0x04004860 RID: 18528
		public const int SetBonus_Titanium = 4;

		// Token: 0x04004861 RID: 18529
		public const int SetBonus_Orichalcum = 5;

		// Token: 0x04004862 RID: 18530
		public const int SetBonus_Chlorophyte = 6;

		// Token: 0x04004863 RID: 18531
		public const int SetBonus_Stardust = 7;

		// Token: 0x04004864 RID: 18532
		public const int WeaponEnchantment_Confetti = 8;

		// Token: 0x04004865 RID: 18533
		public const int PlayerDeath_TombStone = 9;

		// Token: 0x04004866 RID: 18534
		public const int TorchGod = 10;

		// Token: 0x04004867 RID: 18535
		public const int FallingStar = 11;

		// Token: 0x04004868 RID: 18536
		public const int PlayerHurt_DropFootball = 12;

		// Token: 0x04004869 RID: 18537
		public const int StormTigerTierSwap = 13;

		// Token: 0x0400486A RID: 18538
		public const int AbigailTierSwap = 14;

		// Token: 0x0400486B RID: 18539
		public const int SetBonus_GhostHeal = 15;

		// Token: 0x0400486C RID: 18540
		public const int SetBonus_GhostHurt = 16;

		// Token: 0x0400486D RID: 18541
		public const int VampireKnives = 18;

		// Token: 0x0400486E RID: 18542
		public static readonly int Count = 19;
	}
}
