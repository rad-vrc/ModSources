using System;

namespace Terraria
{
	// Token: 0x02000013 RID: 19
	public struct ShoppingSettings
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000B7E4 File Offset: 0x000099E4
		public static ShoppingSettings NotInShop
		{
			get
			{
				return new ShoppingSettings
				{
					PriceAdjustment = 1.0,
					HappinessReport = ""
				};
			}
		}

		// Token: 0x04000066 RID: 102
		public double PriceAdjustment;

		// Token: 0x04000067 RID: 103
		public string HappinessReport;
	}
}
