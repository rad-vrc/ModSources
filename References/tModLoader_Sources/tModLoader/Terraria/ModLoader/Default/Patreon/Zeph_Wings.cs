using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000340 RID: 832
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Wings
	})]
	internal class Zeph_Wings : PatreonItem
	{
		// Token: 0x06002F22 RID: 12066 RVA: 0x005335B2 File Offset: 0x005317B2
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(150, 7f, 1f, false, -1f, 1f);
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x005335EE File Offset: 0x005317EE
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
