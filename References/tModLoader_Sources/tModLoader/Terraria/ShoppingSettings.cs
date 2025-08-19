using System;

namespace Terraria
{
	// Token: 0x02000053 RID: 83
	public struct ShoppingSettings
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x003FB3B4 File Offset: 0x003F95B4
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

		// Token: 0x04000EAC RID: 3756
		public double PriceAdjustment;

		// Token: 0x04000EAD RID: 3757
		public string HappinessReport;
	}
}
