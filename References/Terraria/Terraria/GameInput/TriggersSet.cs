using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Terraria.GameInput
{
	// Token: 0x0200013A RID: 314
	public class TriggersSet
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x004D7758 File Offset: 0x004D5958
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x004D776A File Offset: 0x004D596A
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

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x004D777D File Offset: 0x004D597D
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x004D778F File Offset: 0x004D598F
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

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x004D77A2 File Offset: 0x004D59A2
		// (set) Token: 0x060017F0 RID: 6128 RVA: 0x004D77B4 File Offset: 0x004D59B4
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

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x004D77C7 File Offset: 0x004D59C7
		// (set) Token: 0x060017F2 RID: 6130 RVA: 0x004D77D9 File Offset: 0x004D59D9
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

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x004D77EC File Offset: 0x004D59EC
		// (set) Token: 0x060017F4 RID: 6132 RVA: 0x004D77FE File Offset: 0x004D59FE
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

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x004D7811 File Offset: 0x004D5A11
		// (set) Token: 0x060017F6 RID: 6134 RVA: 0x004D7823 File Offset: 0x004D5A23
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

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x004D7836 File Offset: 0x004D5A36
		// (set) Token: 0x060017F8 RID: 6136 RVA: 0x004D7848 File Offset: 0x004D5A48
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

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x004D785B File Offset: 0x004D5A5B
		// (set) Token: 0x060017FA RID: 6138 RVA: 0x004D786D File Offset: 0x004D5A6D
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

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x004D7880 File Offset: 0x004D5A80
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x004D7892 File Offset: 0x004D5A92
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

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x004D78A5 File Offset: 0x004D5AA5
		// (set) Token: 0x060017FE RID: 6142 RVA: 0x004D78B7 File Offset: 0x004D5AB7
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

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x004D78CA File Offset: 0x004D5ACA
		// (set) Token: 0x06001800 RID: 6144 RVA: 0x004D78DC File Offset: 0x004D5ADC
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

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x004D78EF File Offset: 0x004D5AEF
		// (set) Token: 0x06001802 RID: 6146 RVA: 0x004D7901 File Offset: 0x004D5B01
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

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x004D7914 File Offset: 0x004D5B14
		// (set) Token: 0x06001804 RID: 6148 RVA: 0x004D7926 File Offset: 0x004D5B26
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

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x004D7939 File Offset: 0x004D5B39
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x004D794B File Offset: 0x004D5B4B
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

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x004D795E File Offset: 0x004D5B5E
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x004D7970 File Offset: 0x004D5B70
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

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x004D7983 File Offset: 0x004D5B83
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x004D7995 File Offset: 0x004D5B95
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

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x004D79A8 File Offset: 0x004D5BA8
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x004D79BA File Offset: 0x004D5BBA
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

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x004D79CD File Offset: 0x004D5BCD
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x004D79DF File Offset: 0x004D5BDF
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

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x004D79F2 File Offset: 0x004D5BF2
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x004D7A04 File Offset: 0x004D5C04
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

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x004D7A17 File Offset: 0x004D5C17
		// (set) Token: 0x06001812 RID: 6162 RVA: 0x004D7A29 File Offset: 0x004D5C29
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

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x004D7A3C File Offset: 0x004D5C3C
		// (set) Token: 0x06001814 RID: 6164 RVA: 0x004D7A4E File Offset: 0x004D5C4E
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

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x004D7A61 File Offset: 0x004D5C61
		// (set) Token: 0x06001816 RID: 6166 RVA: 0x004D7A73 File Offset: 0x004D5C73
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

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x004D7A86 File Offset: 0x004D5C86
		// (set) Token: 0x06001818 RID: 6168 RVA: 0x004D7A98 File Offset: 0x004D5C98
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

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x004D7AAB File Offset: 0x004D5CAB
		// (set) Token: 0x0600181A RID: 6170 RVA: 0x004D7ABD File Offset: 0x004D5CBD
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

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x004D7AD0 File Offset: 0x004D5CD0
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x004D7AE2 File Offset: 0x004D5CE2
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

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x004D7AF5 File Offset: 0x004D5CF5
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x004D7B07 File Offset: 0x004D5D07
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

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x004D7B1A File Offset: 0x004D5D1A
		// (set) Token: 0x06001820 RID: 6176 RVA: 0x004D7B2C File Offset: 0x004D5D2C
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

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x004D7B3F File Offset: 0x004D5D3F
		// (set) Token: 0x06001822 RID: 6178 RVA: 0x004D7B51 File Offset: 0x004D5D51
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

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x004D7B64 File Offset: 0x004D5D64
		// (set) Token: 0x06001824 RID: 6180 RVA: 0x004D7B76 File Offset: 0x004D5D76
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

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x004D7B89 File Offset: 0x004D5D89
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x004D7B9B File Offset: 0x004D5D9B
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

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x004D7BAE File Offset: 0x004D5DAE
		// (set) Token: 0x06001828 RID: 6184 RVA: 0x004D7BC0 File Offset: 0x004D5DC0
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

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x004D7BD3 File Offset: 0x004D5DD3
		// (set) Token: 0x0600182A RID: 6186 RVA: 0x004D7BE5 File Offset: 0x004D5DE5
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

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x004D7BF8 File Offset: 0x004D5DF8
		// (set) Token: 0x0600182C RID: 6188 RVA: 0x004D7C0A File Offset: 0x004D5E0A
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

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x004D7C1D File Offset: 0x004D5E1D
		// (set) Token: 0x0600182E RID: 6190 RVA: 0x004D7C2F File Offset: 0x004D5E2F
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

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x004D7C42 File Offset: 0x004D5E42
		// (set) Token: 0x06001830 RID: 6192 RVA: 0x004D7C54 File Offset: 0x004D5E54
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

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x004D7C67 File Offset: 0x004D5E67
		// (set) Token: 0x06001832 RID: 6194 RVA: 0x004D7C79 File Offset: 0x004D5E79
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

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x004D7C8C File Offset: 0x004D5E8C
		// (set) Token: 0x06001834 RID: 6196 RVA: 0x004D7C9E File Offset: 0x004D5E9E
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

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x004D7CB1 File Offset: 0x004D5EB1
		// (set) Token: 0x06001836 RID: 6198 RVA: 0x004D7CC3 File Offset: 0x004D5EC3
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

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x004D7CD6 File Offset: 0x004D5ED6
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x004D7CE8 File Offset: 0x004D5EE8
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

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x004D7CFB File Offset: 0x004D5EFB
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x004D7D0D File Offset: 0x004D5F0D
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

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x004D7D20 File Offset: 0x004D5F20
		// (set) Token: 0x0600183C RID: 6204 RVA: 0x004D7D32 File Offset: 0x004D5F32
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

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x004D7D45 File Offset: 0x004D5F45
		// (set) Token: 0x0600183E RID: 6206 RVA: 0x004D7D57 File Offset: 0x004D5F57
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

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x004D7D6A File Offset: 0x004D5F6A
		// (set) Token: 0x06001840 RID: 6208 RVA: 0x004D7D7C File Offset: 0x004D5F7C
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

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x004D7D8F File Offset: 0x004D5F8F
		// (set) Token: 0x06001842 RID: 6210 RVA: 0x004D7DA1 File Offset: 0x004D5FA1
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

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x004D7DB4 File Offset: 0x004D5FB4
		// (set) Token: 0x06001844 RID: 6212 RVA: 0x004D7DC6 File Offset: 0x004D5FC6
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

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x004D7DD9 File Offset: 0x004D5FD9
		// (set) Token: 0x06001846 RID: 6214 RVA: 0x004D7DEB File Offset: 0x004D5FEB
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x004D7DFE File Offset: 0x004D5FFE
		// (set) Token: 0x06001848 RID: 6216 RVA: 0x004D7E10 File Offset: 0x004D6010
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

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x004D7E23 File Offset: 0x004D6023
		// (set) Token: 0x0600184A RID: 6218 RVA: 0x004D7E35 File Offset: 0x004D6035
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

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x004D7E48 File Offset: 0x004D6048
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x004D7E5A File Offset: 0x004D605A
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

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x004D7E6D File Offset: 0x004D606D
		// (set) Token: 0x0600184E RID: 6222 RVA: 0x004D7E7F File Offset: 0x004D607F
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

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x004D7E92 File Offset: 0x004D6092
		// (set) Token: 0x06001850 RID: 6224 RVA: 0x004D7EA4 File Offset: 0x004D60A4
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

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x004D7EB7 File Offset: 0x004D60B7
		// (set) Token: 0x06001852 RID: 6226 RVA: 0x004D7EC9 File Offset: 0x004D60C9
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

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x004D7EDC File Offset: 0x004D60DC
		// (set) Token: 0x06001854 RID: 6228 RVA: 0x004D7EEE File Offset: 0x004D60EE
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

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x004D7F01 File Offset: 0x004D6101
		// (set) Token: 0x06001856 RID: 6230 RVA: 0x004D7F13 File Offset: 0x004D6113
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

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x004D7F26 File Offset: 0x004D6126
		// (set) Token: 0x06001858 RID: 6232 RVA: 0x004D7F38 File Offset: 0x004D6138
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

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x004D7F4B File Offset: 0x004D614B
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x004D7F5D File Offset: 0x004D615D
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

		// Token: 0x0600185B RID: 6235 RVA: 0x004D7F70 File Offset: 0x004D6170
		public void Reset()
		{
			string[] array = this.KeyStatus.Keys.ToArray<string>();
			for (int i = 0; i < array.Length; i++)
			{
				this.KeyStatus[array[i]] = false;
			}
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x004D7FAC File Offset: 0x004D61AC
		public void CloneFrom(TriggersSet other)
		{
			this.KeyStatus.Clear();
			this.LatestInputMode.Clear();
			foreach (KeyValuePair<string, bool> keyValuePair in other.KeyStatus)
			{
				this.KeyStatus.Add(keyValuePair.Key, keyValuePair.Value);
			}
			this.UsedMovementKey = other.UsedMovementKey;
			this.HotbarScrollCD = other.HotbarScrollCD;
			this.HotbarHoldTime = other.HotbarHoldTime;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x004D804C File Offset: 0x004D624C
		public void SetupKeys()
		{
			this.KeyStatus.Clear();
			foreach (string key in PlayerInput.KnownTriggers)
			{
				this.KeyStatus.Add(key, false);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x004D80B0 File Offset: 0x004D62B0
		public Vector2 DirectionsRaw
		{
			get
			{
				return new Vector2((float)(this.Right.ToInt() - this.Left.ToInt()), (float)(this.Down.ToInt() - this.Up.ToInt()));
			}
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x004D80E8 File Offset: 0x004D62E8
		public Vector2 GetNavigatorDirections()
		{
			bool flag = Main.gameMenu || Main.ingameOptionsWindow || Main.editChest || Main.editSign || ((Main.playerInventory || Main.LocalPlayer.talkNPC != -1) && PlayerInput.CurrentProfile.UsingDpadMovekeys());
			bool value = this.Up || (flag && this.MenuUp);
			bool value2 = this.Right || (flag && this.MenuRight);
			bool value3 = this.Down || (flag && this.MenuDown);
			bool value4 = this.Left || (flag && this.MenuLeft);
			return new Vector2((float)(value2.ToInt() - value4.ToInt()), (float)(value3.ToInt() - value.ToInt()));
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x004D81B8 File Offset: 0x004D63B8
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
			InputMode mode;
			if (this.SmartSelect && this.LatestInputMode.TryGetValue("SmartSelect", out mode) && this.IsInputFromGamepad(mode))
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
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x004D8420 File Offset: 0x004D6620
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

		// Token: 0x06001862 RID: 6242 RVA: 0x004D8504 File Offset: 0x004D6704
		private bool ShouldLockTileUsage()
		{
			return PlayerInput.LockGamepadTileUseButton && PlayerInput.UsingGamepad;
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x004D8514 File Offset: 0x004D6714
		private bool IsInputFromGamepad(InputMode mode)
		{
			return mode > InputMode.Mouse;
		}

		// Token: 0x040014BD RID: 5309
		public Dictionary<string, bool> KeyStatus = new Dictionary<string, bool>();

		// Token: 0x040014BE RID: 5310
		public Dictionary<string, InputMode> LatestInputMode = new Dictionary<string, InputMode>();

		// Token: 0x040014BF RID: 5311
		public bool UsedMovementKey = true;

		// Token: 0x040014C0 RID: 5312
		public int HotbarScrollCD;

		// Token: 0x040014C1 RID: 5313
		public int HotbarHoldTime;
	}
}
