using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002EA RID: 746
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Wings
	})]
	internal class Coolmike5000_Wings : PatreonItem
	{
		// Token: 0x06002E34 RID: 11828 RVA: 0x0053129E File Offset: 0x0052F49E
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(150, 7f, 1f, false, -1f, 1f);
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x005312DA File Offset: 0x0052F4DA
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
