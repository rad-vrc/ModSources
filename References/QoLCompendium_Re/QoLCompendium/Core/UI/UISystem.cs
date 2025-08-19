using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Core.UI.Panels;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace QoLCompendium.Core.UI
{
	// Token: 0x0200026D RID: 621
	public class UISystem : ModSystem
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00073C6B File Offset: 0x00071E6B
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x00073C72 File Offset: 0x00071E72
		public static LocalizedText BMPotionText { get; private set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00073C7A File Offset: 0x00071E7A
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x00073C81 File Offset: 0x00071E81
		public static LocalizedText BMStationText { get; private set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x00073C89 File Offset: 0x00071E89
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x00073C90 File Offset: 0x00071E90
		public static LocalizedText BMMaterialText { get; private set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00073C98 File Offset: 0x00071E98
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x00073C9F File Offset: 0x00071E9F
		public static LocalizedText BMMovementAccessoryText { get; private set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00073CA7 File Offset: 0x00071EA7
		// (set) Token: 0x06000E9C RID: 3740 RVA: 0x00073CAE File Offset: 0x00071EAE
		public static LocalizedText BMCombatAccessoryText { get; private set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00073CB6 File Offset: 0x00071EB6
		// (set) Token: 0x06000E9E RID: 3742 RVA: 0x00073CBD File Offset: 0x00071EBD
		public static LocalizedText BMInformativeText { get; private set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x00073CC5 File Offset: 0x00071EC5
		// (set) Token: 0x06000EA0 RID: 3744 RVA: 0x00073CCC File Offset: 0x00071ECC
		public static LocalizedText BMBagText { get; private set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00073CD4 File Offset: 0x00071ED4
		// (set) Token: 0x06000EA2 RID: 3746 RVA: 0x00073CDB File Offset: 0x00071EDB
		public static LocalizedText BMCrateText { get; private set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00073CE3 File Offset: 0x00071EE3
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x00073CEA File Offset: 0x00071EEA
		public static LocalizedText BMOreText { get; private set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00073CF2 File Offset: 0x00071EF2
		// (set) Token: 0x06000EA6 RID: 3750 RVA: 0x00073CF9 File Offset: 0x00071EF9
		public static LocalizedText BMNaturalBlockText { get; private set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x00073D01 File Offset: 0x00071F01
		// (set) Token: 0x06000EA8 RID: 3752 RVA: 0x00073D08 File Offset: 0x00071F08
		public static LocalizedText BMBuildingBlockText { get; private set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x00073D10 File Offset: 0x00071F10
		// (set) Token: 0x06000EAA RID: 3754 RVA: 0x00073D17 File Offset: 0x00071F17
		public static LocalizedText BMHerbText { get; private set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00073D1F File Offset: 0x00071F1F
		// (set) Token: 0x06000EAC RID: 3756 RVA: 0x00073D26 File Offset: 0x00071F26
		public static LocalizedText BMFishText { get; private set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x00073D2E File Offset: 0x00071F2E
		// (set) Token: 0x06000EAE RID: 3758 RVA: 0x00073D35 File Offset: 0x00071F35
		public static LocalizedText BMMountText { get; private set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00073D3D File Offset: 0x00071F3D
		// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x00073D44 File Offset: 0x00071F44
		public static LocalizedText BMAmmoText { get; private set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x00073D4C File Offset: 0x00071F4C
		// (set) Token: 0x06000EB2 RID: 3762 RVA: 0x00073D53 File Offset: 0x00071F53
		public static LocalizedText ECPotionText { get; private set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x00073D5B File Offset: 0x00071F5B
		// (set) Token: 0x06000EB4 RID: 3764 RVA: 0x00073D62 File Offset: 0x00071F62
		public static LocalizedText ECStationText { get; private set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00073D6A File Offset: 0x00071F6A
		// (set) Token: 0x06000EB6 RID: 3766 RVA: 0x00073D71 File Offset: 0x00071F71
		public static LocalizedText ECMaterialText { get; private set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00073D79 File Offset: 0x00071F79
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x00073D80 File Offset: 0x00071F80
		public static LocalizedText ECMaterial2Text { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00073D88 File Offset: 0x00071F88
		// (set) Token: 0x06000EBA RID: 3770 RVA: 0x00073D8F File Offset: 0x00071F8F
		public static LocalizedText ECBagText { get; private set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00073D97 File Offset: 0x00071F97
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x00073D9E File Offset: 0x00071F9E
		public static LocalizedText ECCrateText { get; private set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00073DA6 File Offset: 0x00071FA6
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x00073DAD File Offset: 0x00071FAD
		public static LocalizedText ECOreText { get; private set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00073DB5 File Offset: 0x00071FB5
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x00073DBC File Offset: 0x00071FBC
		public static LocalizedText ECNaturalBlockText { get; private set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00073DC4 File Offset: 0x00071FC4
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x00073DCB File Offset: 0x00071FCB
		public static LocalizedText ECBuildingBlockText { get; private set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00073DD3 File Offset: 0x00071FD3
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x00073DDA File Offset: 0x00071FDA
		public static LocalizedText ECHerbText { get; private set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00073DE2 File Offset: 0x00071FE2
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x00073DE9 File Offset: 0x00071FE9
		public static LocalizedText ECFishText { get; private set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00073DF1 File Offset: 0x00071FF1
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x00073DF8 File Offset: 0x00071FF8
		public static LocalizedText PiggyBankText { get; private set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00073E00 File Offset: 0x00072000
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x00073E07 File Offset: 0x00072007
		public static LocalizedText SafeText { get; private set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00073E0F File Offset: 0x0007200F
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x00073E16 File Offset: 0x00072016
		public static LocalizedText DefendersForgeText { get; private set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00073E1E File Offset: 0x0007201E
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x00073E25 File Offset: 0x00072025
		public static LocalizedText VoidVaultText { get; private set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00073E2D File Offset: 0x0007202D
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x00073E34 File Offset: 0x00072034
		public static LocalizedText DesertText { get; private set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x00073E3C File Offset: 0x0007203C
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x00073E43 File Offset: 0x00072043
		public static LocalizedText SnowText { get; private set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00073E4B File Offset: 0x0007204B
		// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x00073E52 File Offset: 0x00072052
		public static LocalizedText JungleText { get; private set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00073E5A File Offset: 0x0007205A
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x00073E61 File Offset: 0x00072061
		public static LocalizedText GlowingMushroomText { get; private set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00073E69 File Offset: 0x00072069
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x00073E70 File Offset: 0x00072070
		public static LocalizedText CorruptionText { get; private set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00073E78 File Offset: 0x00072078
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x00073E7F File Offset: 0x0007207F
		public static LocalizedText CrimsonText { get; private set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00073E87 File Offset: 0x00072087
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x00073E8E File Offset: 0x0007208E
		public static LocalizedText HallowText { get; private set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00073E96 File Offset: 0x00072096
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x00073E9D File Offset: 0x0007209D
		public static LocalizedText PurityText { get; private set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00073EA5 File Offset: 0x000720A5
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x00073EAC File Offset: 0x000720AC
		public static LocalizedText BossText { get; private set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00073EB4 File Offset: 0x000720B4
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x00073EBB File Offset: 0x000720BB
		public static LocalizedText EventText { get; private set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00073EC3 File Offset: 0x000720C3
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x00073ECA File Offset: 0x000720CA
		public static LocalizedText MinibossText { get; private set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x00073ED2 File Offset: 0x000720D2
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x00073ED9 File Offset: 0x000720D9
		public static LocalizedText IncreaseSpawnText { get; private set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00073EE1 File Offset: 0x000720E1
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x00073EE8 File Offset: 0x000720E8
		public static LocalizedText DecreaseSpawnText { get; private set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00073EF0 File Offset: 0x000720F0
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x00073EF7 File Offset: 0x000720F7
		public static LocalizedText CancelSpawnText { get; private set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x00073EFF File Offset: 0x000720FF
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x00073F06 File Offset: 0x00072106
		public static LocalizedText CancelEventText { get; private set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00073F0E File Offset: 0x0007210E
		// (set) Token: 0x06000EEE RID: 3822 RVA: 0x00073F15 File Offset: 0x00072115
		public static LocalizedText CancelSpawnAndEventText { get; private set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00073F1D File Offset: 0x0007211D
		// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x00073F24 File Offset: 0x00072124
		public static LocalizedText RevertText { get; private set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00073F2C File Offset: 0x0007212C
		// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x00073F33 File Offset: 0x00072133
		public static LocalizedText FullMoonText { get; private set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00073F3B File Offset: 0x0007213B
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x00073F42 File Offset: 0x00072142
		public static LocalizedText WaningGibbousText { get; private set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00073F4A File Offset: 0x0007214A
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x00073F51 File Offset: 0x00072151
		public static LocalizedText ThirdQuarterText { get; private set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00073F59 File Offset: 0x00072159
		// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x00073F60 File Offset: 0x00072160
		public static LocalizedText WaningCrescentText { get; private set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00073F68 File Offset: 0x00072168
		// (set) Token: 0x06000EFA RID: 3834 RVA: 0x00073F6F File Offset: 0x0007216F
		public static LocalizedText NewMoonText { get; private set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00073F77 File Offset: 0x00072177
		// (set) Token: 0x06000EFC RID: 3836 RVA: 0x00073F7E File Offset: 0x0007217E
		public static LocalizedText WaxingCrescentText { get; private set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00073F86 File Offset: 0x00072186
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x00073F8D File Offset: 0x0007218D
		public static LocalizedText FirstQuarterText { get; private set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00073F95 File Offset: 0x00072195
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x00073F9C File Offset: 0x0007219C
		public static LocalizedText WaxingGibbousText { get; private set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00073FA4 File Offset: 0x000721A4
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x00073FAB File Offset: 0x000721AB
		public static LocalizedText VanillaText { get; private set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00073FB3 File Offset: 0x000721B3
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x00073FBA File Offset: 0x000721BA
		public static LocalizedText CalamityText { get; private set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00073FC2 File Offset: 0x000721C2
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x00073FC9 File Offset: 0x000721C9
		public static LocalizedText MartinsOrderText { get; private set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00073FD1 File Offset: 0x000721D1
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x00073FD8 File Offset: 0x000721D8
		public static LocalizedText SOTSText { get; private set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00073FE0 File Offset: 0x000721E0
		// (set) Token: 0x06000F0A RID: 3850 RVA: 0x00073FE7 File Offset: 0x000721E7
		public static LocalizedText SpiritClassicText { get; private set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x00073FEF File Offset: 0x000721EF
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x00073FF6 File Offset: 0x000721F6
		public static LocalizedText ThoriumText { get; private set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x00073FFE File Offset: 0x000721FE
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x00074005 File Offset: 0x00072205
		public static LocalizedText ArenaText { get; private set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0007400D File Offset: 0x0007220D
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x00074014 File Offset: 0x00072214
		public static LocalizedText PotionText { get; private set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0007401C File Offset: 0x0007221C
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x00074023 File Offset: 0x00072223
		public static LocalizedText StationText { get; private set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0007402B File Offset: 0x0007222B
		// (set) Token: 0x06000F14 RID: 3860 RVA: 0x00074032 File Offset: 0x00072232
		public static LocalizedText AddonText { get; private set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0007403A File Offset: 0x0007223A
		// (set) Token: 0x06000F16 RID: 3862 RVA: 0x00074041 File Offset: 0x00072241
		public static LocalizedText RepellentText { get; private set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00074049 File Offset: 0x00072249
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x00074050 File Offset: 0x00072250
		public static LocalizedText CloseText { get; private set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00074058 File Offset: 0x00072258
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x0007405F File Offset: 0x0007225F
		public static LocalizedText ResetText { get; private set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00074067 File Offset: 0x00072267
		// (set) Token: 0x06000F1C RID: 3868 RVA: 0x0007406E File Offset: 0x0007226E
		public static LocalizedText UnloadedText { get; private set; }

		// Token: 0x06000F1D RID: 3869 RVA: 0x00074078 File Offset: 0x00072278
		public override void Load()
		{
			UISystem.BMPotionText = base.Mod.GetLocalization("UIText.BMPotionText", null);
			UISystem.BMStationText = base.Mod.GetLocalization("UIText.BMStationText", null);
			UISystem.BMMaterialText = base.Mod.GetLocalization("UIText.BMMaterialText", null);
			UISystem.BMMovementAccessoryText = base.Mod.GetLocalization("UIText.BMMovementAccessoryText", null);
			UISystem.BMCombatAccessoryText = base.Mod.GetLocalization("UIText.BMCombatAccessoryText", null);
			UISystem.BMInformativeText = base.Mod.GetLocalization("UIText.BMInformativeText", null);
			UISystem.BMBagText = base.Mod.GetLocalization("UIText.BMBagText", null);
			UISystem.BMCrateText = base.Mod.GetLocalization("UIText.BMCrateText", null);
			UISystem.BMOreText = base.Mod.GetLocalization("UIText.BMOreText", null);
			UISystem.BMNaturalBlockText = base.Mod.GetLocalization("UIText.BMNaturalBlockText", null);
			UISystem.BMBuildingBlockText = base.Mod.GetLocalization("UIText.BMBuildingBlockText", null);
			UISystem.BMHerbText = base.Mod.GetLocalization("UIText.BMHerbText", null);
			UISystem.BMFishText = base.Mod.GetLocalization("UIText.BMFishText", null);
			UISystem.BMMountText = base.Mod.GetLocalization("UIText.BMMountText", null);
			UISystem.BMAmmoText = base.Mod.GetLocalization("UIText.BMAmmoText", null);
			UISystem.ECPotionText = base.Mod.GetLocalization("UIText.ECPotionText", null);
			UISystem.ECStationText = base.Mod.GetLocalization("UIText.ECStationText", null);
			UISystem.ECMaterialText = base.Mod.GetLocalization("UIText.ECMaterialText", null);
			UISystem.ECMaterial2Text = base.Mod.GetLocalization("UIText.ECMaterial2Text", null);
			UISystem.ECBagText = base.Mod.GetLocalization("UIText.ECBagText", null);
			UISystem.ECCrateText = base.Mod.GetLocalization("UIText.ECCrateText", null);
			UISystem.ECOreText = base.Mod.GetLocalization("UIText.ECOreText", null);
			UISystem.ECNaturalBlockText = base.Mod.GetLocalization("UIText.ECNaturalBlockText", null);
			UISystem.ECBuildingBlockText = base.Mod.GetLocalization("UIText.ECBuildingBlockText", null);
			UISystem.ECHerbText = base.Mod.GetLocalization("UIText.ECHerbText", null);
			UISystem.ECFishText = base.Mod.GetLocalization("UIText.ECFishText", null);
			UISystem.PiggyBankText = base.Mod.GetLocalization("UIText.PiggyBankText", null);
			UISystem.SafeText = base.Mod.GetLocalization("UIText.SafeText", null);
			UISystem.DefendersForgeText = base.Mod.GetLocalization("UIText.DefendersForgeText", null);
			UISystem.VoidVaultText = base.Mod.GetLocalization("UIText.VoidVaultText", null);
			UISystem.DesertText = base.Mod.GetLocalization("UIText.DesertText", null);
			UISystem.SnowText = base.Mod.GetLocalization("UIText.SnowText", null);
			UISystem.JungleText = base.Mod.GetLocalization("UIText.JungleText", null);
			UISystem.GlowingMushroomText = base.Mod.GetLocalization("UIText.GlowingMushroomText", null);
			UISystem.CorruptionText = base.Mod.GetLocalization("UIText.CorruptionText", null);
			UISystem.CrimsonText = base.Mod.GetLocalization("UIText.CrimsonText", null);
			UISystem.HallowText = base.Mod.GetLocalization("UIText.HallowText", null);
			UISystem.PurityText = base.Mod.GetLocalization("UIText.PurityText", null);
			UISystem.BossText = base.Mod.GetLocalization("UIText.BossText", null);
			UISystem.EventText = base.Mod.GetLocalization("UIText.EventText", null);
			UISystem.MinibossText = base.Mod.GetLocalization("UIText.MinibossText", null);
			UISystem.IncreaseSpawnText = base.Mod.GetLocalization("UIText.IncreaseSpawnText", null);
			UISystem.DecreaseSpawnText = base.Mod.GetLocalization("UIText.DecreaseSpawnText", null);
			UISystem.CancelSpawnText = base.Mod.GetLocalization("UIText.CancelSpawnText", null);
			UISystem.CancelEventText = base.Mod.GetLocalization("UIText.CancelEventText", null);
			UISystem.CancelSpawnAndEventText = base.Mod.GetLocalization("UIText.CancelSpawnAndEventText", null);
			UISystem.RevertText = base.Mod.GetLocalization("UIText.RevertText", null);
			UISystem.FullMoonText = base.Mod.GetLocalization("UIText.FullMoonText", null);
			UISystem.WaningGibbousText = base.Mod.GetLocalization("UIText.WaningGibbousText", null);
			UISystem.ThirdQuarterText = base.Mod.GetLocalization("UIText.ThirdQuarterText", null);
			UISystem.WaningCrescentText = base.Mod.GetLocalization("UIText.WaningCrescentText", null);
			UISystem.NewMoonText = base.Mod.GetLocalization("UIText.NewMoonText", null);
			UISystem.WaxingCrescentText = base.Mod.GetLocalization("UIText.WaxingCrescentText", null);
			UISystem.FirstQuarterText = base.Mod.GetLocalization("UIText.FirstQuarterText", null);
			UISystem.WaxingGibbousText = base.Mod.GetLocalization("UIText.WaxingGibbousText", null);
			UISystem.VanillaText = base.Mod.GetLocalization("UIText.VanillaText", null);
			UISystem.CalamityText = base.Mod.GetLocalization("UIText.CalamityText", null);
			UISystem.MartinsOrderText = base.Mod.GetLocalization("UIText.MartinsOrderText", null);
			UISystem.SOTSText = base.Mod.GetLocalization("UIText.SOTSText", null);
			UISystem.SpiritClassicText = base.Mod.GetLocalization("UIText.SpiritClassicText", null);
			UISystem.ThoriumText = base.Mod.GetLocalization("UIText.ThoriumText", null);
			UISystem.ArenaText = base.Mod.GetLocalization("UIText.ArenaText", null);
			UISystem.PotionText = base.Mod.GetLocalization("UIText.PotionText", null);
			UISystem.StationText = base.Mod.GetLocalization("UIText.StationText", null);
			UISystem.AddonText = base.Mod.GetLocalization("UIText.AddonText", null);
			UISystem.RepellentText = base.Mod.GetLocalization("UIText.RepellentText", null);
			UISystem.CloseText = base.Mod.GetLocalization("UIText.CloseText", null);
			UISystem.ResetText = base.Mod.GetLocalization("UIText.ResetText", null);
			UISystem.UnloadedText = base.Mod.GetLocalization("UIText.UnloadedText", null);
			if (!Main.dedServ)
			{
				this.blackMarketDealerNPCShopUI = new BlackMarketDealerNPCUI();
				this.blackMarketDealerNPCShopUI.Activate();
				this.blackMarketDealerNPCInterface = new UserInterface();
				this.blackMarketDealerNPCInterface.SetState(this.blackMarketDealerNPCShopUI);
				this.etherealCollectorNPCShopUI = new EtherealCollectorNPCUI();
				this.etherealCollectorNPCShopUI.Activate();
				this.etherealCollectorNPCInterface = new UserInterface();
				this.etherealCollectorNPCInterface.SetState(this.etherealCollectorNPCShopUI);
				this.allInOneAccessUI = new AllInOneAccessUI();
				this.allInOneAccessUI.Activate();
				this.allInOneAccessInterface = new UserInterface();
				this.allInOneAccessInterface.SetState(this.allInOneAccessUI);
				this.destinationGlobeUI = new DestinationGlobeUI();
				this.destinationGlobeUI.Activate();
				this.destinationGlobeInterface = new UserInterface();
				this.destinationGlobeInterface.SetState(this.destinationGlobeUI);
				this.entityManipulatorUI = new EntityManipulatorUI();
				this.entityManipulatorUI.Activate();
				this.entityManipulatorInterface = new UserInterface();
				this.entityManipulatorInterface.SetState(this.entityManipulatorUI);
				this.phaseInterrupterUI = new PhaseInterrupterUI();
				this.phaseInterrupterUI.Activate();
				this.phaseInterrupterInterface = new UserInterface();
				this.phaseInterrupterInterface.SetState(this.phaseInterrupterUI);
				this.summoningRemoteUI = new SummoningRemoteUI();
				this.summoningRemoteUI.Activate();
				this.summoningRemoteInterface = new UserInterface();
				this.summoningRemoteInterface.SetState(this.summoningRemoteUI);
				this.permanentBuffUI = new PermanentBuffUI();
				this.permanentBuffUI.Activate();
				this.permanentBuffInterface = new UserInterface();
				this.permanentBuffInterface.SetState(this.permanentBuffUI);
				this.permanentCalamityBuffUI = new PermanentCalamityBuffUI();
				this.permanentCalamityBuffUI.Activate();
				this.permanentCalamityBuffInterface = new UserInterface();
				this.permanentCalamityBuffInterface.SetState(this.permanentCalamityBuffUI);
				this.permanentMartinsOrderBuffUI = new PermanentMartinsOrderBuffUI();
				this.permanentMartinsOrderBuffUI.Activate();
				this.permanentMartinsOrderBuffInterface = new UserInterface();
				this.permanentMartinsOrderBuffInterface.SetState(this.permanentMartinsOrderBuffUI);
				this.permanentSOTSBuffUI = new PermanentSOTSBuffUI();
				this.permanentSOTSBuffUI.Activate();
				this.permanentSOTSBuffInterface = new UserInterface();
				this.permanentSOTSBuffInterface.SetState(this.permanentSOTSBuffUI);
				this.permanentSpiritClassicBuffUI = new PermanentSpiritClassicBuffUI();
				this.permanentSpiritClassicBuffUI.Activate();
				this.permanentSpiritClassicBuffInterface = new UserInterface();
				this.permanentSpiritClassicBuffInterface.SetState(this.permanentSpiritClassicBuffUI);
				this.permanentThoriumBuffUI = new PermanentThoriumBuffUI();
				this.permanentThoriumBuffUI.Activate();
				this.permanentThoriumBuffInterface = new UserInterface();
				this.permanentThoriumBuffInterface.SetState(this.permanentThoriumBuffUI);
				this.permanentBuffSelectorUI = new PermanentBuffSelectorUI();
				this.permanentBuffSelectorUI.Activate();
				this.permanentBuffSelectorInterface = new UserInterface();
				this.permanentBuffSelectorInterface.SetState(this.permanentBuffSelectorUI);
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0007493C File Offset: 0x00072B3C
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int MouseTextIndex = layers.FindIndex((GameInterfaceLayer layer) => layer.Name.Equals("Vanilla: Mouse Text"));
			if (MouseTextIndex != -1)
			{
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: Shop Selector", delegate()
				{
					if (BlackMarketDealerNPCUI.visible)
					{
						this.blackMarketDealerNPCShopUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: Modded Shop Selector", delegate()
				{
					if (EtherealCollectorNPCUI.visible)
					{
						this.etherealCollectorNPCShopUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: Storage Selector", delegate()
				{
					if (AllInOneAccessUI.visible)
					{
						this.allInOneAccessUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: Biome Selector", delegate()
				{
					if (DestinationGlobeUI.visible)
					{
						this.destinationGlobeUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: Spawn Selector", delegate()
				{
					if (EntityManipulatorUI.visible)
					{
						this.entityManipulatorUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: Moon Selector", delegate()
				{
					if (PhaseInterrupterUI.visible)
					{
						this.phaseInterrupterUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: Summon Selector", delegate()
				{
					if (SummoningRemoteUI.visible)
					{
						this.summoningRemoteUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: Buff Selector", delegate()
				{
					if (PermanentBuffSelectorUI.visible)
					{
						this.permanentBuffSelectorUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: Buff Toggles", delegate()
				{
					if (PermanentBuffUI.visible)
					{
						this.permanentBuffUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: Calamity Buff Toggles", delegate()
				{
					if (PermanentCalamityBuffUI.visible)
					{
						this.permanentCalamityBuffUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: Martins Order Buff Toggles", delegate()
				{
					if (PermanentMartinsOrderBuffUI.visible)
					{
						this.permanentMartinsOrderBuffUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: SOTS Buff Toggles", delegate()
				{
					if (PermanentSOTSBuffUI.visible)
					{
						this.permanentSOTSBuffUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: Spirit Classic Buff Toggles", delegate()
				{
					if (PermanentSpiritClassicBuffUI.visible)
					{
						this.permanentSpiritClassicBuffUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("QoLC: Thorium Buff Toggles", delegate()
				{
					if (PermanentThoriumBuffUI.visible)
					{
						this.permanentThoriumBuffUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00074B1C File Offset: 0x00072D1C
		public override void UpdateUI(GameTime gameTime)
		{
			this.oldUiGameTime = gameTime;
			if (this.blackMarketDealerNPCInterface.CurrentState != null && BlackMarketDealerNPCUI.visible)
			{
				this.blackMarketDealerNPCInterface.Update(gameTime);
			}
			if (this.etherealCollectorNPCInterface.CurrentState != null && EtherealCollectorNPCUI.visible)
			{
				this.etherealCollectorNPCInterface.Update(gameTime);
			}
			if (this.allInOneAccessInterface.CurrentState != null && AllInOneAccessUI.visible)
			{
				this.allInOneAccessInterface.Update(gameTime);
			}
			if (this.destinationGlobeInterface.CurrentState != null && DestinationGlobeUI.visible)
			{
				this.destinationGlobeInterface.Update(gameTime);
			}
			if (this.entityManipulatorInterface.CurrentState != null && EntityManipulatorUI.visible)
			{
				this.entityManipulatorInterface.Update(gameTime);
			}
			if (this.phaseInterrupterInterface.CurrentState != null && PhaseInterrupterUI.visible)
			{
				this.phaseInterrupterInterface.Update(gameTime);
			}
			if (this.summoningRemoteInterface.CurrentState != null && SummoningRemoteUI.visible)
			{
				this.summoningRemoteInterface.Update(gameTime);
			}
			if (this.permanentBuffInterface.CurrentState != null && PermanentBuffUI.visible)
			{
				this.permanentBuffInterface.Update(gameTime);
			}
			if (this.permanentCalamityBuffInterface.CurrentState != null && PermanentCalamityBuffUI.visible)
			{
				this.permanentCalamityBuffInterface.Update(gameTime);
			}
			if (this.permanentMartinsOrderBuffInterface.CurrentState != null && PermanentMartinsOrderBuffUI.visible)
			{
				this.permanentMartinsOrderBuffInterface.Update(gameTime);
			}
			if (this.permanentSOTSBuffInterface.CurrentState != null && PermanentSOTSBuffUI.visible)
			{
				this.permanentSOTSBuffInterface.Update(gameTime);
			}
			if (this.permanentSpiritClassicBuffInterface.CurrentState != null && PermanentSpiritClassicBuffUI.visible)
			{
				this.permanentSpiritClassicBuffInterface.Update(gameTime);
			}
			if (this.permanentThoriumBuffInterface.CurrentState != null && PermanentThoriumBuffUI.visible)
			{
				this.permanentThoriumBuffInterface.Update(gameTime);
			}
			if (this.permanentBuffSelectorInterface.CurrentState != null && PermanentBuffSelectorUI.visible)
			{
				this.permanentBuffSelectorInterface.Update(gameTime);
			}
		}

		// Token: 0x040005CA RID: 1482
		public GameTime oldUiGameTime;

		// Token: 0x040005CB RID: 1483
		public BlackMarketDealerNPCUI blackMarketDealerNPCShopUI;

		// Token: 0x040005CC RID: 1484
		public UserInterface blackMarketDealerNPCInterface;

		// Token: 0x040005CD RID: 1485
		public EtherealCollectorNPCUI etherealCollectorNPCShopUI;

		// Token: 0x040005CE RID: 1486
		public UserInterface etherealCollectorNPCInterface;

		// Token: 0x040005CF RID: 1487
		public AllInOneAccessUI allInOneAccessUI;

		// Token: 0x040005D0 RID: 1488
		public UserInterface allInOneAccessInterface;

		// Token: 0x040005D1 RID: 1489
		public DestinationGlobeUI destinationGlobeUI;

		// Token: 0x040005D2 RID: 1490
		public UserInterface destinationGlobeInterface;

		// Token: 0x040005D3 RID: 1491
		public EntityManipulatorUI entityManipulatorUI;

		// Token: 0x040005D4 RID: 1492
		public UserInterface entityManipulatorInterface;

		// Token: 0x040005D5 RID: 1493
		public PhaseInterrupterUI phaseInterrupterUI;

		// Token: 0x040005D6 RID: 1494
		public UserInterface phaseInterrupterInterface;

		// Token: 0x040005D7 RID: 1495
		public SummoningRemoteUI summoningRemoteUI;

		// Token: 0x040005D8 RID: 1496
		public UserInterface summoningRemoteInterface;

		// Token: 0x040005D9 RID: 1497
		public PermanentBuffSelectorUI permanentBuffSelectorUI;

		// Token: 0x040005DA RID: 1498
		public UserInterface permanentBuffSelectorInterface;

		// Token: 0x040005DB RID: 1499
		public PermanentBuffUI permanentBuffUI;

		// Token: 0x040005DC RID: 1500
		public UserInterface permanentBuffInterface;

		// Token: 0x040005DD RID: 1501
		public PermanentCalamityBuffUI permanentCalamityBuffUI;

		// Token: 0x040005DE RID: 1502
		public UserInterface permanentCalamityBuffInterface;

		// Token: 0x040005DF RID: 1503
		public PermanentMartinsOrderBuffUI permanentMartinsOrderBuffUI;

		// Token: 0x040005E0 RID: 1504
		public UserInterface permanentMartinsOrderBuffInterface;

		// Token: 0x040005E1 RID: 1505
		public PermanentSOTSBuffUI permanentSOTSBuffUI;

		// Token: 0x040005E2 RID: 1506
		public UserInterface permanentSOTSBuffInterface;

		// Token: 0x040005E3 RID: 1507
		public PermanentSpiritClassicBuffUI permanentSpiritClassicBuffUI;

		// Token: 0x040005E4 RID: 1508
		public UserInterface permanentSpiritClassicBuffInterface;

		// Token: 0x040005E5 RID: 1509
		public PermanentThoriumBuffUI permanentThoriumBuffUI;

		// Token: 0x040005E6 RID: 1510
		public UserInterface permanentThoriumBuffInterface;
	}
}
