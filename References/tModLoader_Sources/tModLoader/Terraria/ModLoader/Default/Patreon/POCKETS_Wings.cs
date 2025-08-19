using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000319 RID: 793
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Wings
	})]
	internal class POCKETS_Wings : PatreonItem
	{
		// Token: 0x06002EB5 RID: 11957 RVA: 0x00532510 File Offset: 0x00530710
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(150, 7f, 1f, false, -1f, 1f);
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x0053254C File Offset: 0x0053074C
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
