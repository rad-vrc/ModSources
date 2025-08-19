using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002E1 RID: 737
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class AetherBreaker_Legs : PatreonItem
	{
		// Token: 0x06002E21 RID: 11809 RVA: 0x005310C5 File Offset: 0x0052F2C5
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
