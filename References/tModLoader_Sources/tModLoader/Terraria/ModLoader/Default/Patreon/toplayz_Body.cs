using System;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000332 RID: 818
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class toplayz_Body : PatreonItem
	{
		// Token: 0x06002EF5 RID: 12021 RVA: 0x00532D8B File Offset: 0x00530F8B
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Body.Sets.HidesTopSkin[base.Item.bodySlot] = true;
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x00532DA5 File Offset: 0x00530FA5
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 30;
			base.Item.height = 20;
		}
	}
}
