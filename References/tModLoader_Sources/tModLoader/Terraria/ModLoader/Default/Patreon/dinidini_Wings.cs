using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002EE RID: 750
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Wings
	})]
	internal class dinidini_Wings : PatreonItem
	{
		// Token: 0x06002E3F RID: 11839 RVA: 0x005313CD File Offset: 0x0052F5CD
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(150, 7f, 1f, false, -1f, 1f);
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x00531409 File Offset: 0x0052F609
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
