using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002F0 RID: 752
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class dschosen_Body : PatreonItem
	{
		// Token: 0x06002E44 RID: 11844 RVA: 0x00531474 File Offset: 0x0052F674
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 42;
			base.Item.height = 24;
		}
	}
}
