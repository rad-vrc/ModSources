using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000330 RID: 816
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Wings
	})]
	internal class Tantamount_Wings : PatreonItem
	{
		// Token: 0x06002EF0 RID: 12016 RVA: 0x00532CE3 File Offset: 0x00530EE3
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(150, 7f, 1f, false, -1f, 1f);
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x00532D1F File Offset: 0x00530F1F
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.vanity = false;
			base.Item.Size = new Vector2(24f, 26f);
			base.Item.accessory = true;
		}
	}
}
