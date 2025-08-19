using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200030F RID: 783
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Wings
	})]
	internal class Mayne_Wings : PatreonItem
	{
		// Token: 0x06002E94 RID: 11924 RVA: 0x00531FB1 File Offset: 0x005301B1
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(150, 7f, 1f, false, -1f, 1f);
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x00531FED File Offset: 0x005301ED
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.vanity = false;
			base.Item.width = 34;
			base.Item.height = 20;
			base.Item.accessory = true;
		}
	}
}
