using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Terraria.GameInput
{
	// Token: 0x02000489 RID: 1161
	public class TriggersSet
	{
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06003835 RID: 14389 RVA: 0x005930A0 File Offset: 0x005912A0
		// (set) Token: 0x06003836 RID: 14390 RVA: 0x005930B2 File Offset: 0x005912B2
		public bool MouseLeft
		{
			get
			{
				return this.KeyStatus["MouseLeft"];
			}
			set
			{
				this.KeyStatus["MouseLeft"] = value;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06003837 RID: 14391 RVA: 0x005930C5 File Offset: 0x005912C5
		// (set) Token: 0x06003838 RID: 14392 RVA: 0x005930D7 File Offset: 0x005912D7
		public bool MouseRight
		{
			get
			{
				return this.KeyStatus["MouseRight"];
			}
			set
			{
				this.KeyStatus["MouseRight"] = value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06003839 RID: 14393 RVA: 0x005930EA File Offset: 0x005912EA
		// (set) Token: 0x0600383A RID: 14394 RVA: 0x005930FC File Offset: 0x005912FC
		public bool MouseMiddle
		{
			get
			{
				return this.KeyStatus["MouseMiddle"];
			}
			set
			{
				this.KeyStatus["MouseMiddle"] = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x0059310F File Offset: 0x0059130F
		// (set) Token: 0x0600383C RID: 14396 RVA: 0x00593121 File Offset: 0x00591321
		public bool MouseXButton1
		{
			get
			{
				return this.KeyStatus["MouseXButton1"];
			}
			set
			{
				this.KeyStatus["MouseXButton1"] = value;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x00593134 File Offset: 0x00591334
		// (set) Token: 0x0600383E RID: 14398 RVA: 0x00593146 File Offset: 0x00591346
		public bool MouseXButton2
		{
			get
			{
				return this.KeyStatus["MouseXButton2"];
			}
			set
			{
				this.KeyStatus["MouseXButton2"] = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600383F RID: 14399 RVA: 0x00593159 File Offset: 0x00591359
		// (set) Token: 0x06003840 RID: 14400 RVA: 0x0059316B File Offset: 0x0059136B
		public bool Up
		{
			get
			{
				return this.KeyStatus["Up"];
			}
			set
			{
				this.KeyStatus["Up"] = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06003841 RID: 14401 RVA: 0x0059317E File Offset: 0x0059137E
		// (set) Token: 0x06003842 RID: 14402 RVA: 0x00593190 File Offset: 0x00591390
		public bool Down
		{
			get
			{
				return this.KeyStatus["Down"];
			}
			set
			{
				this.KeyStatus["Down"] = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06003843 RID: 14403 RVA: 0x005931A3 File Offset: 0x005913A3
		// (set) Token: 0x06003844 RID: 14404 RVA: 0x005931B5 File Offset: 0x005913B5
		public bool Left
		{
			get
			{
				return this.KeyStatus["Left"];
			}
			set
			{
				this.KeyStatus["Left"] = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06003845 RID: 14405 RVA: 0x005931C8 File Offset: 0x005913C8
		// (set) Token: 0x06003846 RID: 14406 RVA: 0x005931DA File Offset: 0x005913DA
		public bool Right
		{
			get
			{
				return this.KeyStatus["Right"];
			}
			set
			{
				this.KeyStatus["Right"] = value;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06003847 RID: 14407 RVA: 0x005931ED File Offset: 0x005913ED
		// (set) Token: 0x06003848 RID: 14408 RVA: 0x005931FF File Offset: 0x005913FF
		public bool Jump
		{
			get
			{
				return this.KeyStatus["Jump"];
			}
			set
			{
				this.KeyStatus["Jump"] = value;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06003849 RID: 14409 RVA: 0x00593212 File Offset: 0x00591412
		// (set) Token: 0x0600384A RID: 14410 RVA: 0x00593224 File Offset: 0x00591424
		public bool Throw
		{
			get
			{
				return this.KeyStatus["Throw"];
			}
			set
			{
				this.KeyStatus["Throw"] = value;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x00593237 File Offset: 0x00591437
		// (set) Token: 0x0600384C RID: 14412 RVA: 0x00593249 File Offset: 0x00591449
		public bool Inventory
		{
			get
			{
				return this.KeyStatus["Inventory"];
			}
			set
			{
				this.KeyStatus["Inventory"] = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x0059325C File Offset: 0x0059145C
		// (set) Token: 0x0600384E RID: 14414 RVA: 0x0059326E File Offset: 0x0059146E
		public bool Grapple
		{
			get
			{
				return this.KeyStatus["Grapple"];
			}
			set
			{
				this.KeyStatus["Grapple"] = value;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x00593281 File Offset: 0x00591481
		// (set) Token: 0x06003850 RID: 14416 RVA: 0x00593293 File Offset: 0x00591493
		public bool SmartSelect
		{
			get
			{
				return this.KeyStatus["SmartSelect"];
			}
			set
			{
				this.KeyStatus["SmartSelect"] = value;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06003851 RID: 14417 RVA: 0x005932A6 File Offset: 0x005914A6
		// (set) Token: 0x06003852 RID: 14418 RVA: 0x005932B8 File Offset: 0x005914B8
		public bool SmartCursor
		{
			get
			{
				return this.KeyStatus["SmartCursor"];
			}
			set
			{
				this.KeyStatus["SmartCursor"] = value;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06003853 RID: 14419 RVA: 0x005932CB File Offset: 0x005914CB
		// (set) Token: 0x06003854 RID: 14420 RVA: 0x005932DD File Offset: 0x005914DD
		public bool QuickMount
		{
			get
			{
				return this.KeyStatus["QuickMount"];
			}
			set
			{
				this.KeyStatus["QuickMount"] = value;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06003855 RID: 14421 RVA: 0x005932F0 File Offset: 0x005914F0
		// (set) Token: 0x06003856 RID: 14422 RVA: 0x00593302 File Offset: 0x00591502
		public bool QuickHeal
		{
			get
			{
				return this.KeyStatus["QuickHeal"];
			}
			set
			{
				this.KeyStatus["QuickHeal"] = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06003857 RID: 14423 RVA: 0x00593315 File Offset: 0x00591515
		// (set) Token: 0x06003858 RID: 14424 RVA: 0x00593327 File Offset: 0x00591527
		public bool QuickMana
		{
			get
			{
				return this.KeyStatus["QuickMana"];
			}
			set
			{
				this.KeyStatus["QuickMana"] = value;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06003859 RID: 14425 RVA: 0x0059333A File Offset: 0x0059153A
		// (set) Token: 0x0600385A RID: 14426 RVA: 0x0059334C File Offset: 0x0059154C
		public bool QuickBuff
		{
			get
			{
				return this.KeyStatus["QuickBuff"];
			}
			set
			{
				this.KeyStatus["QuickBuff"] = value;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600385B RID: 14427 RVA: 0x0059335F File Offset: 0x0059155F
		// (set) Token: 0x0600385C RID: 14428 RVA: 0x00593371 File Offset: 0x00591571
		public bool Loadout1
		{
			get
			{
				return this.KeyStatus["Loadout1"];
			}
			set
			{
				this.KeyStatus["Loadout1"] = value;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600385D RID: 14429 RVA: 0x00593384 File Offset: 0x00591584
		// (set) Token: 0x0600385E RID: 14430 RVA: 0x00593396 File Offset: 0x00591596
		public bool Loadout2
		{
			get
			{
				return this.KeyStatus["Loadout2"];
			}
			set
			{
				this.KeyStatus["Loadout2"] = value;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x005933A9 File Offset: 0x005915A9
		// (set) Token: 0x06003860 RID: 14432 RVA: 0x005933BB File Offset: 0x005915BB
		public bool Loadout3
		{
			get
			{
				return this.KeyStatus["Loadout3"];
			}
			set
			{
				this.KeyStatus["Loadout3"] = value;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x005933CE File Offset: 0x005915CE
		// (set) Token: 0x06003862 RID: 14434 RVA: 0x005933E0 File Offset: 0x005915E0
		public bool MapZoomIn
		{
			get
			{
				return this.KeyStatus["MapZoomIn"];
			}
			set
			{
				this.KeyStatus["MapZoomIn"] = value;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06003863 RID: 14435 RVA: 0x005933F3 File Offset: 0x005915F3
		// (set) Token: 0x06003864 RID: 14436 RVA: 0x00593405 File Offset: 0x00591605
		public bool MapZoomOut
		{
			get
			{
				return this.KeyStatus["MapZoomOut"];
			}
			set
			{
				this.KeyStatus["MapZoomOut"] = value;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06003865 RID: 14437 RVA: 0x00593418 File Offset: 0x00591618
		// (set) Token: 0x06003866 RID: 14438 RVA: 0x0059342A File Offset: 0x0059162A
		public bool MapAlphaUp
		{
			get
			{
				return this.KeyStatus["MapAlphaUp"];
			}
			set
			{
				this.KeyStatus["MapAlphaUp"] = value;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06003867 RID: 14439 RVA: 0x0059343D File Offset: 0x0059163D
		// (set) Token: 0x06003868 RID: 14440 RVA: 0x0059344F File Offset: 0x0059164F
		public bool MapAlphaDown
		{
			get
			{
				return this.KeyStatus["MapAlphaDown"];
			}
			set
			{
				this.KeyStatus["MapAlphaDown"] = value;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06003869 RID: 14441 RVA: 0x00593462 File Offset: 0x00591662
		// (set) Token: 0x0600386A RID: 14442 RVA: 0x00593474 File Offset: 0x00591674
		public bool MapFull
		{
			get
			{
				return this.KeyStatus["MapFull"];
			}
			set
			{
				this.KeyStatus["MapFull"] = value;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600386B RID: 14443 RVA: 0x00593487 File Offset: 0x00591687
		// (set) Token: 0x0600386C RID: 14444 RVA: 0x00593499 File Offset: 0x00591699
		public bool MapStyle
		{
			get
			{
				return this.KeyStatus["MapStyle"];
			}
			set
			{
				this.KeyStatus["MapStyle"] = value;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600386D RID: 14445 RVA: 0x005934AC File Offset: 0x005916AC
		// (set) Token: 0x0600386E RID: 14446 RVA: 0x005934BE File Offset: 0x005916BE
		public bool Hotbar1
		{
			get
			{
				return this.KeyStatus["Hotbar1"];
			}
			set
			{
				this.KeyStatus["Hotbar1"] = value;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x005934D1 File Offset: 0x005916D1
		// (set) Token: 0x06003870 RID: 14448 RVA: 0x005934E3 File Offset: 0x005916E3
		public bool Hotbar2
		{
			get
			{
				return this.KeyStatus["Hotbar2"];
			}
			set
			{
				this.KeyStatus["Hotbar2"] = value;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x005934F6 File Offset: 0x005916F6
		// (set) Token: 0x06003872 RID: 14450 RVA: 0x00593508 File Offset: 0x00591708
		public bool Hotbar3
		{
			get
			{
				return this.KeyStatus["Hotbar3"];
			}
			set
			{
				this.KeyStatus["Hotbar3"] = value;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x0059351B File Offset: 0x0059171B
		// (set) Token: 0x06003874 RID: 14452 RVA: 0x0059352D File Offset: 0x0059172D
		public bool Hotbar4
		{
			get
			{
				return this.KeyStatus["Hotbar4"];
			}
			set
			{
				this.KeyStatus["Hotbar4"] = value;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06003875 RID: 14453 RVA: 0x00593540 File Offset: 0x00591740
		// (set) Token: 0x06003876 RID: 14454 RVA: 0x00593552 File Offset: 0x00591752
		public bool Hotbar5
		{
			get
			{
				return this.KeyStatus["Hotbar5"];
			}
			set
			{
				this.KeyStatus["Hotbar5"] = value;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x00593565 File Offset: 0x00591765
		// (set) Token: 0x06003878 RID: 14456 RVA: 0x00593577 File Offset: 0x00591777
		public bool Hotbar6
		{
			get
			{
				return this.KeyStatus["Hotbar6"];
			}
			set
			{
				this.KeyStatus["Hotbar6"] = value;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06003879 RID: 14457 RVA: 0x0059358A File Offset: 0x0059178A
		// (set) Token: 0x0600387A RID: 14458 RVA: 0x0059359C File Offset: 0x0059179C
		public bool Hotbar7
		{
			get
			{
				return this.KeyStatus["Hotbar7"];
			}
			set
			{
				this.KeyStatus["Hotbar7"] = value;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600387B RID: 14459 RVA: 0x005935AF File Offset: 0x005917AF
		// (set) Token: 0x0600387C RID: 14460 RVA: 0x005935C1 File Offset: 0x005917C1
		public bool Hotbar8
		{
			get
			{
				return this.KeyStatus["Hotbar8"];
			}
			set
			{
				this.KeyStatus["Hotbar8"] = value;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x005935D4 File Offset: 0x005917D4
		// (set) Token: 0x0600387E RID: 14462 RVA: 0x005935E6 File Offset: 0x005917E6
		public bool Hotbar9
		{
			get
			{
				return this.KeyStatus["Hotbar9"];
			}
			set
			{
				this.KeyStatus["Hotbar9"] = value;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x005935F9 File Offset: 0x005917F9
		// (set) Token: 0x06003880 RID: 14464 RVA: 0x0059360B File Offset: 0x0059180B
		public bool Hotbar10
		{
			get
			{
				return this.KeyStatus["Hotbar10"];
			}
			set
			{
				this.KeyStatus["Hotbar10"] = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06003881 RID: 14465 RVA: 0x0059361E File Offset: 0x0059181E
		// (set) Token: 0x06003882 RID: 14466 RVA: 0x00593630 File Offset: 0x00591830
		public bool HotbarMinus
		{
			get
			{
				return this.KeyStatus["HotbarMinus"];
			}
			set
			{
				this.KeyStatus["HotbarMinus"] = value;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06003883 RID: 14467 RVA: 0x00593643 File Offset: 0x00591843
		// (set) Token: 0x06003884 RID: 14468 RVA: 0x00593655 File Offset: 0x00591855
		public bool HotbarPlus
		{
			get
			{
				return this.KeyStatus["HotbarPlus"];
			}
			set
			{
				this.KeyStatus["HotbarPlus"] = value;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06003885 RID: 14469 RVA: 0x00593668 File Offset: 0x00591868
		// (set) Token: 0x06003886 RID: 14470 RVA: 0x0059367A File Offset: 0x0059187A
		public bool DpadRadial1
		{
			get
			{
				return this.KeyStatus["DpadRadial1"];
			}
			set
			{
				this.KeyStatus["DpadRadial1"] = value;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06003887 RID: 14471 RVA: 0x0059368D File Offset: 0x0059188D
		// (set) Token: 0x06003888 RID: 14472 RVA: 0x0059369F File Offset: 0x0059189F
		public bool DpadRadial2
		{
			get
			{
				return this.KeyStatus["DpadRadial2"];
			}
			set
			{
				this.KeyStatus["DpadRadial2"] = value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06003889 RID: 14473 RVA: 0x005936B2 File Offset: 0x005918B2
		// (set) Token: 0x0600388A RID: 14474 RVA: 0x005936C4 File Offset: 0x005918C4
		public bool DpadRadial3
		{
			get
			{
				return this.KeyStatus["DpadRadial3"];
			}
			set
			{
				this.KeyStatus["DpadRadial3"] = value;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600388B RID: 14475 RVA: 0x005936D7 File Offset: 0x005918D7
		// (set) Token: 0x0600388C RID: 14476 RVA: 0x005936E9 File Offset: 0x005918E9
		public bool DpadRadial4
		{
			get
			{
				return this.KeyStatus["DpadRadial4"];
			}
			set
			{
				this.KeyStatus["DpadRadial4"] = value;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x0600388D RID: 14477 RVA: 0x005936FC File Offset: 0x005918FC
		// (set) Token: 0x0600388E RID: 14478 RVA: 0x0059370E File Offset: 0x0059190E
		public bool RadialHotbar
		{
			get
			{
				return this.KeyStatus["RadialHotbar"];
			}
			set
			{
				this.KeyStatus["RadialHotbar"] = value;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x0600388F RID: 14479 RVA: 0x00593721 File Offset: 0x00591921
		// (set) Token: 0x06003890 RID: 14480 RVA: 0x00593733 File Offset: 0x00591933
		public bool RadialQuickbar
		{
			get
			{
				return this.KeyStatus["RadialQuickbar"];
			}
			set
			{
				this.KeyStatus["RadialQuickbar"] = value;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06003891 RID: 14481 RVA: 0x00593746 File Offset: 0x00591946
		// (set) Token: 0x06003892 RID: 14482 RVA: 0x00593758 File Offset: 0x00591958
		public bool DpadMouseSnap1
		{
			get
			{
				return this.KeyStatus["DpadSnap1"];
			}
			set
			{
				this.KeyStatus["DpadSnap1"] = value;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06003893 RID: 14483 RVA: 0x0059376B File Offset: 0x0059196B
		// (set) Token: 0x06003894 RID: 14484 RVA: 0x0059377D File Offset: 0x0059197D
		public bool DpadMouseSnap2
		{
			get
			{
				return this.KeyStatus["DpadSnap2"];
			}
			set
			{
				this.KeyStatus["DpadSnap2"] = value;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06003895 RID: 14485 RVA: 0x00593790 File Offset: 0x00591990
		// (set) Token: 0x06003896 RID: 14486 RVA: 0x005937A2 File Offset: 0x005919A2
		public bool DpadMouseSnap3
		{
			get
			{
				return this.KeyStatus["DpadSnap3"];
			}
			set
			{
				this.KeyStatus["DpadSnap3"] = value;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06003897 RID: 14487 RVA: 0x005937B5 File Offset: 0x005919B5
		// (set) Token: 0x06003898 RID: 14488 RVA: 0x005937C7 File Offset: 0x005919C7
		public bool DpadMouseSnap4
		{
			get
			{
				return this.KeyStatus["DpadSnap4"];
			}
			set
			{
				this.KeyStatus["DpadSnap4"] = value;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06003899 RID: 14489 RVA: 0x005937DA File Offset: 0x005919DA
		// (set) Token: 0x0600389A RID: 14490 RVA: 0x005937EC File Offset: 0x005919EC
		public bool MenuUp
		{
			get
			{
				return this.KeyStatus["MenuUp"];
			}
			set
			{
				this.KeyStatus["MenuUp"] = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600389B RID: 14491 RVA: 0x005937FF File Offset: 0x005919FF
		// (set) Token: 0x0600389C RID: 14492 RVA: 0x00593811 File Offset: 0x00591A11
		public bool MenuDown
		{
			get
			{
				return this.KeyStatus["MenuDown"];
			}
			set
			{
				this.KeyStatus["MenuDown"] = value;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600389D RID: 14493 RVA: 0x00593824 File Offset: 0x00591A24
		// (set) Token: 0x0600389E RID: 14494 RVA: 0x00593836 File Offset: 0x00591A36
		public bool MenuLeft
		{
			get
			{
				return this.KeyStatus["MenuLeft"];
			}
			set
			{
				this.KeyStatus["MenuLeft"] = value;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600389F RID: 14495 RVA: 0x00593849 File Offset: 0x00591A49
		// (set) Token: 0x060038A0 RID: 14496 RVA: 0x0059385B File Offset: 0x00591A5B
		public bool MenuRight
		{
			get
			{
				return this.KeyStatus["MenuRight"];
			}
			set
			{
				this.KeyStatus["MenuRight"] = value;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060038A1 RID: 14497 RVA: 0x0059386E File Offset: 0x00591A6E
		// (set) Token: 0x060038A2 RID: 14498 RVA: 0x00593880 File Offset: 0x00591A80
		public bool LockOn
		{
			get
			{
				return this.KeyStatus["LockOn"];
			}
			set
			{
				this.KeyStatus["LockOn"] = value;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060038A3 RID: 14499 RVA: 0x00593893 File Offset: 0x00591A93
		// (set) Token: 0x060038A4 RID: 14500 RVA: 0x005938A5 File Offset: 0x00591AA5
		public bool ViewZoomIn
		{
			get
			{
				return this.KeyStatus["ViewZoomIn"];
			}
			set
			{
				this.KeyStatus["ViewZoomIn"] = value;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060038A5 RID: 14501 RVA: 0x005938B8 File Offset: 0x00591AB8
		// (set) Token: 0x060038A6 RID: 14502 RVA: 0x005938CA File Offset: 0x00591ACA
		public bool ViewZoomOut
		{
			get
			{
				return this.KeyStatus["ViewZoomOut"];
			}
			set
			{
				this.KeyStatus["ViewZoomOut"] = value;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060038A7 RID: 14503 RVA: 0x005938DD File Offset: 0x00591ADD
		// (set) Token: 0x060038A8 RID: 14504 RVA: 0x005938EF File Offset: 0x00591AEF
		public bool OpenCreativePowersMenu
		{
			get
			{
				return this.KeyStatus["ToggleCreativeMenu"];
			}
			set
			{
				this.KeyStatus["ToggleCreativeMenu"] = value;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x00593902 File Offset: 0x00591B02
		// (set) Token: 0x060038AA RID: 14506 RVA: 0x00593914 File Offset: 0x00591B14
		public bool ToggleCameraMode
		{
			get
			{
				return this.KeyStatus["ToggleCameraMode"];
			}
			set
			{
				this.KeyStatus["ToggleCameraMode"] = value;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060038AB RID: 14507 RVA: 0x00593927 File Offset: 0x00591B27
		public Vector2 DirectionsRaw
		{
			get
			{
				return new Vector2((float)(this.Right.ToInt() - this.Left.ToInt()), (float)(this.Down.ToInt() - this.Up.ToInt()));
			}
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x00593960 File Offset: 0x00591B60
		public void Reset()
		{
			string[] array = this.KeyStatus.Keys.ToArray<string>();
			for (int i = 0; i < array.Length; i++)
			{
				this.KeyStatus[array[i]] = false;
			}
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x0059399C File Offset: 0x00591B9C
		public void CloneFrom(TriggersSet other)
		{
			this.KeyStatus.Clear();
			this.LatestInputMode.Clear();
			foreach (KeyValuePair<string, bool> item in other.KeyStatus)
			{
				this.KeyStatus.Add(item.Key, item.Value);
			}
			this.UsedMovementKey = other.UsedMovementKey;
			this.HotbarScrollCD = other.HotbarScrollCD;
			this.HotbarHoldTime = other.HotbarHoldTime;
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x00593A3C File Offset: 0x00591C3C
		public void SetupKeys()
		{
			this.KeyStatus.Clear();
			foreach (string knownTrigger in PlayerInput.KnownTriggers)
			{
				this.KeyStatus.Add(knownTrigger, false);
			}
			foreach (ModKeybind keybind in KeybindLoader.Keybinds)
			{
				this.KeyStatus.Add(keybind.FullName, false);
			}
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x00593AE8 File Offset: 0x00591CE8
		public Vector2 GetNavigatorDirections()
		{
			bool flag = Main.gameMenu || Main.ingameOptionsWindow || Main.editChest || Main.editSign || ((Main.playerInventory || Main.LocalPlayer.talkNPC != -1) && PlayerInput.CurrentProfile.UsingDpadMovekeys());
			bool value = this.Up || (flag && this.MenuUp);
			bool value4 = this.Right || (flag && this.MenuRight);
			bool value2 = this.Down || (flag && this.MenuDown);
			bool value3 = this.Left || (flag && this.MenuLeft);
			return new Vector2((float)(value4.ToInt() - value3.ToInt()), (float)(value2.ToInt() - value.ToInt()));
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x00593BB8 File Offset: 0x00591DB8
		public void CopyInto(Player p)
		{
			if (PlayerInput.CurrentInputMode != InputMode.XBoxGamepadUI && !PlayerInput.CursorIsBusy)
			{
				p.controlUp = this.Up;
				p.controlDown = this.Down;
				p.controlLeft = this.Left;
				p.controlRight = this.Right;
				p.controlJump = this.Jump;
				p.controlHook = this.Grapple;
				p.controlTorch = this.SmartSelect;
				p.controlSmart = this.SmartCursor;
				p.controlMount = this.QuickMount;
				p.controlQuickHeal = this.QuickHeal;
				p.controlQuickMana = this.QuickMana;
				p.controlCreativeMenu = this.OpenCreativePowersMenu;
				if (this.QuickBuff)
				{
					p.QuickBuff();
				}
				if (this.Loadout1)
				{
					p.TrySwitchingLoadout(0);
				}
				if (this.Loadout2)
				{
					p.TrySwitchingLoadout(1);
				}
				if (this.Loadout3)
				{
					p.TrySwitchingLoadout(2);
				}
			}
			p.controlInv = this.Inventory;
			p.controlThrow = this.Throw;
			p.mapZoomIn = this.MapZoomIn;
			p.mapZoomOut = this.MapZoomOut;
			p.mapAlphaUp = this.MapAlphaUp;
			p.mapAlphaDown = this.MapAlphaDown;
			p.mapFullScreen = this.MapFull;
			p.mapStyle = this.MapStyle;
			if (this.MouseLeft)
			{
				if (!Main.blockMouse && !p.mouseInterface)
				{
					p.controlUseItem = true;
				}
			}
			else
			{
				Main.blockMouse = false;
			}
			if (!this.MouseRight && !Main.playerInventory)
			{
				PlayerInput.LockGamepadTileUseButton = false;
			}
			if (this.MouseRight && !p.mouseInterface && !Main.blockMouse && !this.ShouldLockTileUsage() && !PlayerInput.InBuildingMode)
			{
				p.controlUseTile = true;
			}
			if (PlayerInput.InBuildingMode && this.MouseRight)
			{
				p.controlInv = true;
			}
			InputMode value;
			if (this.SmartSelect && this.LatestInputMode.TryGetValue("SmartSelect", out value) && this.IsInputFromGamepad(value))
			{
				PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Gamepad);
			}
			bool flag = PlayerInput.Triggers.Current.HotbarPlus || PlayerInput.Triggers.Current.HotbarMinus;
			if (flag)
			{
				this.HotbarHoldTime++;
			}
			else
			{
				this.HotbarHoldTime = 0;
			}
			if (this.HotbarScrollCD > 0 && (this.HotbarScrollCD != 1 || !flag || PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired <= 0))
			{
				this.HotbarScrollCD--;
			}
			PlayerLoader.ProcessTriggers(p, this);
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x00593E24 File Offset: 0x00592024
		public void CopyIntoDuringChat(Player p)
		{
			if (this.MouseLeft)
			{
				if (!Main.blockMouse && !p.mouseInterface)
				{
					p.controlUseItem = true;
				}
			}
			else
			{
				Main.blockMouse = false;
			}
			if (!this.MouseRight && !Main.playerInventory)
			{
				PlayerInput.LockGamepadTileUseButton = false;
			}
			if (this.MouseRight && !p.mouseInterface && !Main.blockMouse && !this.ShouldLockTileUsage() && !PlayerInput.InBuildingMode)
			{
				p.controlUseTile = true;
			}
			bool flag = PlayerInput.Triggers.Current.HotbarPlus || PlayerInput.Triggers.Current.HotbarMinus;
			if (flag)
			{
				this.HotbarHoldTime++;
			}
			else
			{
				this.HotbarHoldTime = 0;
			}
			if (this.HotbarScrollCD > 0 && (this.HotbarScrollCD != 1 || !flag || PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired <= 0))
			{
				this.HotbarScrollCD--;
			}
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x00593F08 File Offset: 0x00592108
		private bool ShouldLockTileUsage()
		{
			return PlayerInput.LockGamepadTileUseButton && PlayerInput.UsingGamepad;
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x00593F18 File Offset: 0x00592118
		private bool IsInputFromGamepad(InputMode mode)
		{
			return mode > InputMode.Mouse;
		}

		// Token: 0x040051FC RID: 20988
		public Dictionary<string, bool> KeyStatus = new Dictionary<string, bool>();

		// Token: 0x040051FD RID: 20989
		public Dictionary<string, InputMode> LatestInputMode = new Dictionary<string, InputMode>();

		// Token: 0x040051FE RID: 20990
		public bool UsedMovementKey = true;

		// Token: 0x040051FF RID: 20991
		public int HotbarScrollCD;

		// Token: 0x04005200 RID: 20992
		public int HotbarHoldTime;
	}
}
