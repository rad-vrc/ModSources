using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002F7 RID: 759
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Frosty_Hoodie : PatreonItem
	{
		// Token: 0x06002E54 RID: 11860 RVA: 0x00531607 File Offset: 0x0052F807
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 30;
			base.Item.height = 20;
		}
	}
}
