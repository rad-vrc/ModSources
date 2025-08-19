using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002E8 RID: 744
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Coolmike5000_Body : PatreonItem
	{
		// Token: 0x06002E30 RID: 11824 RVA: 0x0053124A File Offset: 0x0052F44A
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 42;
			base.Item.height = 24;
		}
	}
}
