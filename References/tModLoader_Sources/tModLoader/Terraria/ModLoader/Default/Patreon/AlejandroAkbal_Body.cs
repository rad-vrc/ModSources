using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002E4 RID: 740
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class AlejandroAkbal_Body : PatreonItem
	{
		// Token: 0x06002E28 RID: 11816 RVA: 0x00531196 File Offset: 0x0052F396
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 42;
			base.Item.height = 24;
		}
	}
}
