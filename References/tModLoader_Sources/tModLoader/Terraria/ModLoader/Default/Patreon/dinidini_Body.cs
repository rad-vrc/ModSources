using System;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002EC RID: 748
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class dinidini_Body : PatreonItem
	{
		// Token: 0x06002E39 RID: 11833 RVA: 0x00531345 File Offset: 0x0052F545
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Body.Sets.HidesTopSkin[base.Item.bodySlot] = true;
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x0053135F File Offset: 0x0052F55F
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 28;
			base.Item.height = 24;
		}
	}
}
