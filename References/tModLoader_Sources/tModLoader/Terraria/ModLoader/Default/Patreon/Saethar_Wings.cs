using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000325 RID: 805
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Wings
	})]
	internal class Saethar_Wings : PatreonItem
	{
		// Token: 0x06002ED5 RID: 11989 RVA: 0x005329C3 File Offset: 0x00530BC3
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(150, 7f, 1f, false, -1f, 1f);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x005329FF File Offset: 0x00530BFF
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
