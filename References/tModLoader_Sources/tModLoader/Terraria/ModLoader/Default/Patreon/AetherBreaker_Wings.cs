using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002E2 RID: 738
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Wings
	})]
	internal class AetherBreaker_Wings : PatreonItem
	{
		// Token: 0x06002E23 RID: 11811 RVA: 0x005310EF File Offset: 0x0052F2EF
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(150, 7f, 1f, false, -1f, 1f);
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x0053112B File Offset: 0x0052F32B
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
