using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002F2 RID: 754
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Wings
	})]
	internal class dschosen_Wings : PatreonItem
	{
		// Token: 0x06002E48 RID: 11848 RVA: 0x005314C8 File Offset: 0x0052F6C8
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(150, 7f, 1f, false, -1f, 1f);
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x00531504 File Offset: 0x0052F704
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.vanity = false;
			base.Item.width = 24;
			base.Item.height = 8;
			base.Item.accessory = true;
		}
	}
}
