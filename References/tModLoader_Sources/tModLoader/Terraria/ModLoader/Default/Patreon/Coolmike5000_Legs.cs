using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002E9 RID: 745
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Coolmike5000_Legs : PatreonItem
	{
		// Token: 0x06002E32 RID: 11826 RVA: 0x00531274 File Offset: 0x0052F474
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
