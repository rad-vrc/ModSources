using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200030E RID: 782
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Mayne_Legs : PatreonItem
	{
		// Token: 0x06002E92 RID: 11922 RVA: 0x00531F87 File Offset: 0x00530187
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 20;
			base.Item.height = 16;
		}
	}
}
