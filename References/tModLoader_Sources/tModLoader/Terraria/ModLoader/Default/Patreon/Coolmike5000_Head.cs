using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002E7 RID: 743
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Coolmike5000_Head : PatreonItem
	{
		// Token: 0x06002E2E RID: 11822 RVA: 0x00531220 File Offset: 0x0052F420
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 34;
			base.Item.height = 22;
		}
	}
}
