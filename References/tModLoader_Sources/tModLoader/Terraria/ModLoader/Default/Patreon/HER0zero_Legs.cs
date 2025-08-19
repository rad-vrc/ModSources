using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000305 RID: 773
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class HER0zero_Legs : PatreonItem
	{
		// Token: 0x06002E79 RID: 11897 RVA: 0x00531C06 File Offset: 0x0052FE06
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 24;
			base.Item.height = 16;
		}
	}
}
