using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000303 RID: 771
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class HER0zero_Head : PatreonItem
	{
		// Token: 0x06002E73 RID: 11891 RVA: 0x00531B64 File Offset: 0x0052FD64
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 30;
			base.Item.height = 20;
		}
	}
}
