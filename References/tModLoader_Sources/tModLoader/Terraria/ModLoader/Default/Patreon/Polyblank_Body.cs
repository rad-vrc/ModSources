using System;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200031B RID: 795
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Polyblank_Body : PatreonItem
	{
		// Token: 0x06002EBA RID: 11962 RVA: 0x005325B7 File Offset: 0x005307B7
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Body.Sets.HidesTopSkin[base.Item.bodySlot] = true;
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x005325D1 File Offset: 0x005307D1
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 30;
			base.Item.height = 20;
		}
	}
}
