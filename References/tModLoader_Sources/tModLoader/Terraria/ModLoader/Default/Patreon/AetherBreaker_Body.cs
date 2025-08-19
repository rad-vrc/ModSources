using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002E0 RID: 736
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class AetherBreaker_Body : PatreonItem
	{
		// Token: 0x06002E1F RID: 11807 RVA: 0x0053109B File Offset: 0x0052F29B
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 42;
			base.Item.height = 24;
		}
	}
}
