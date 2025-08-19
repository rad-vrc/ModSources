using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000329 RID: 809
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Wings
	})]
	internal class Sailing_Squid_Wings : PatreonItem
	{
		// Token: 0x06002EDE RID: 11998 RVA: 0x00532ABE File Offset: 0x00530CBE
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(150, 7f, 1f, false, -1f, 1f);
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x00532AFA File Offset: 0x00530CFA
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
