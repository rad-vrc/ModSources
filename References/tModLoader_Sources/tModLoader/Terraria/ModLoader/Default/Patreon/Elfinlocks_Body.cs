using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002F4 RID: 756
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Elfinlocks_Body : PatreonItem
	{
		// Token: 0x06002E4D RID: 11853 RVA: 0x0053156F File Offset: 0x0052F76F
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 42;
			base.Item.height = 24;
		}
	}
}
